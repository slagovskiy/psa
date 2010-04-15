using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Components.FilterRow;
using Photoland.Security;
using Photoland.Security.User;
using Photoland.Forms.Interface;

namespace Photoland.Forms.Admin
{
    public partial class frmDefectTable : Form
    {
        public SqlConnection db_connection;
        public UserInfo usr;

        private SqlCommand db_command;
        private SqlDataAdapter db_adapter;
        private DataTable tbl = new DataTable("data");
        private FilterRow FilterR;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

        //////////////////////////////////////////////////////////////////////////
        private bool CheckState(SqlConnection c)
        {
            bool r = false;
            if (!TestCommand(c))
            {
                frmWaitConnection w = new frmWaitConnection();
                while (!TestCommand(c))
                {
                    Application.DoEvents();
                    w = new frmWaitConnection();
                    w.ShowDialog();
                    if (w.DialogResult == DialogResult.Cancel)
                    {
                        r = false;
                        break;
                    }
                }
                if (TestCommand(c))
                    r = true;
            }
            else
            {
                r = true;
            }
            return r;
        }

        private bool TestCommand(SqlConnection c)
        {
            bool r = false;
            try
            {
                if (c.State != ConnectionState.Open)
                    c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select getdate()";
                DateTime _r = (DateTime)cmd.ExecuteScalar();
                r = true;
            }
            catch (Exception ex)
            {
                r = false;
            }
            return r;
        }
        //////////////////////////////////////////////////////////////////////////

        public frmDefectTable(SqlConnection db_connection, UserInfo usr)
        {
            InitializeComponent();
            this.Text = "Контроль списаний";

            this.db_connection = db_connection;
            this.usr = usr;
        }

        private void frmDefectTable_Load(object sender, EventArgs e)
        {
            FilterR = new FilterRow(gridData);

			date1.Value = DateTime.Now.AddMonths(-1);
			date2.Value = DateTime.Now.AddDays(5);

			SqlCommand t = new SqlCommand("Select [id_user], [name] FROM [user] ORDER BY [name]", db_connection);
			DataTable dt1 = new DataTable("t");
			DataTable dt2 = new DataTable("t");
			SqlDataAdapter tda = new SqlDataAdapter(t);
			tda.Fill(dt1);
			tda.Fill(dt2);
			DataRow _dr1 = dt1.NewRow();
			_dr1[0] = "-1";
			_dr1[1] = "< не выбрано >";
			DataRow _dr2 = dt2.NewRow();
			_dr2[0] = "-1";
			_dr2[1] = "< не выбрано >";
			dt1.Rows.InsertAt(_dr1, 0);
			dt2.Rows.InsertAt(_dr2, 0);

			txtU2.DataSource = dt1;
			txtU1.DataSource = dt2;
			txtU2.DisplayMember = "name";
			txtU1.DisplayMember = "name";
			txtU2.ValueMember = "id_user";
			txtU1.ValueMember = "id_user";

			t = new SqlCommand("SELECT defect_code, defect_name FROM defect ORDER BY defect_name", db_connection);
			tda = new SqlDataAdapter(t);
			DataTable dt3 = new DataTable("t");
			tda.Fill(dt3);

			DataRow _dr3 = dt3.NewRow();
			_dr3[0] = "-1";
			_dr3[1] = "< не выбрано >";
			dt3.Rows.InsertAt(_dr3, 0);

			txtType.DataSource = dt3;
			txtType.DisplayMember = "defect_name";
			txtType.ValueMember = "defect_code";


			t = new SqlCommand("SELECT id_good, name FROM good ORDER BY name", db_connection);
			tda = new SqlDataAdapter(t);
			DataTable dt4 = new DataTable("t");
			tda.Fill(dt4);

			DataRow _dr4 = dt4.NewRow();
			_dr4[0] = "-1";
			_dr4[1] = "< не выбрано >";
			dt4.Rows.InsertAt(_dr4, 0);

			txtServ.DataSource = dt4;
			txtServ.DisplayMember = "name";
			txtServ.ValueMember = "id_good";



            LoadData(0);
        }

        private void LoadData(int id)
        {
            if (CheckState(db_connection))
            {
                tbl.Rows.Clear();

				db_command = new SqlCommand("SELECT [id_orderbody], [id_order], [datework], [name], [defect_quantity], [name_work], [user_defect], CASE [tech_defect] WHEN 1 THEN 'Брак' WHEN 2 THEN 'Тех брак' WHEN 3 THEN 'Setup' WHEN 4 THEN 'Обрезка' WHEN 5 THEN 'Index print' WHEN 6 THEN 'Металлик' WHEN 7 THEN 'Отмена' WHEN 8 THEN 'S-Print' WHEN 9 THEN 'Н. Обрезка' WHEN 10 THEN 'Возврат' END AS [tech_def], [defect_ok], [tech_defect], [id_good], [id_user_defect], [number], [comment] FROM [dbo].[vwAdmin_Defect] " +
					"WHERE (datework >= CONVERT(DATETIME, '" + date1.Value.Year.ToString("D4") + "-" + date1.Value.Month.ToString("D2") + "-" + date1.Value.Day.ToString("D2") + " 00:00:00', 102) AND datework <= CONVERT(DATETIME, '" + date2.Value.Year.ToString("D4") + "-" + date2.Value.Month.ToString("D2") + "-" + date2.Value.Day.ToString("D2") + " 23:59:59', 102)) " +
					((txtServ.SelectedValue.ToString().Trim() == "-1") ? ("") : (" AND (id_good = N'" + txtServ.SelectedValue.ToString().Trim() + "') ")) +
					((txtType.SelectedValue.ToString().Trim() == "-1") ? ("") : (" AND (tech_defect = " + txtType.SelectedValue.ToString().Trim() + ") ")) +
					((txtU1.SelectedValue.ToString().Trim() == "-1") ? ("") : (" AND (id_user_defect = " + txtU1.SelectedValue.ToString().Trim() + ") ")) +
                    ((checkOK.Checked) ? (" AND (defect_ok = 1) ") : (" AND (defect_ok = 0) ")) +
                    ((checkHideOtherDefect.Checked) ? (" AND (id_user_defect > 1) ") : "") +
                    " ORDER BY [datework]", db_connection);
                /*
                 * 1 [id_orderbody], 
                 * 2 [id_order], 
                 * 4 [name], 
                 * 3 [datework], 
                 * 6 [name_work],
                 * 5 [defect_quantity], 
                 * 7 [user_defect], 
                 * 8 [tech_defect] 
                 * 9 [defect_ok]
                 */
                db_adapter = new SqlDataAdapter(db_command);
                db_adapter.Fill(tbl);

                gridData.DataSource = tbl;

                gridData.Cols[1].Visible = false;
                gridData.Cols[2].Visible = false;
                gridData.Cols[3].Visible = false;
                gridData.Cols[4].Visible = false;
                gridData.Cols[5].Visible = false;
                gridData.Cols[6].Visible = false;
                gridData.Cols[7].Visible = false;
                gridData.Cols[8].Visible = false;
                gridData.Cols[9].Visible = false;
                gridData.Cols[10].Visible = false;
                gridData.Cols[11].Visible = false;
                gridData.Cols[12].Visible = false;
				gridData.Cols[13].Visible = false;
				gridData.Cols[14].Visible = false;

                gridData.Cols[3].Visible = true;
                gridData.Cols[3].Width = 90;
                gridData.Cols[3].AllowDragging = false;
                gridData.Cols[3].AllowEditing = false;
                gridData.Cols[3].AllowMerging = false;
                gridData.Cols[3].AllowResizing = true;
                gridData.Cols[3].AllowSorting = true;
                gridData.Cols[3].TextAlign = C1.Win.C1FlexGrid.TextAlignEnum.RightCenter;
                gridData.Cols[3].Caption = "Дата";

                gridData.Cols[4].Visible = true;
                gridData.Cols[4].Width = 300;
                gridData.Cols[4].AllowDragging = false;
                gridData.Cols[4].AllowEditing = false;
                gridData.Cols[4].AllowMerging = false;
                gridData.Cols[4].AllowResizing = true;
                gridData.Cols[4].AllowSorting = true;
                gridData.Cols[4].Caption = "Услуга";

                gridData.Cols[5].Visible = true;
                gridData.Cols[5].Width = 60;
                gridData.Cols[5].AllowDragging = false;
                gridData.Cols[5].AllowEditing = false;
                gridData.Cols[5].AllowMerging = false;
                gridData.Cols[5].AllowResizing = true;
                gridData.Cols[5].AllowSorting = true;
                gridData.Cols[5].Caption = "Кол-во";

                gridData.Cols[6].Visible = true;
                gridData.Cols[6].Width = 210;
                gridData.Cols[6].AllowDragging = false;
                gridData.Cols[6].AllowEditing = false;
                gridData.Cols[6].AllowMerging = false;
                gridData.Cols[6].AllowResizing = true;
                gridData.Cols[6].AllowSorting = true;
                gridData.Cols[6].Caption = "Добавил";

                gridData.Cols[7].Visible = true;
                gridData.Cols[7].Width = 210;
                gridData.Cols[7].AllowDragging = false;
                gridData.Cols[7].AllowEditing = false;
                gridData.Cols[7].AllowMerging = false;
                gridData.Cols[7].AllowResizing = true;
                gridData.Cols[7].AllowSorting = true;
                gridData.Cols[7].Caption = "На кого";

				gridData.Cols[8].Visible = true;
				gridData.Cols[8].Width = 80;
				gridData.Cols[8].AllowDragging = false;
				gridData.Cols[8].AllowEditing = false;
				gridData.Cols[8].AllowMerging = false;
				gridData.Cols[8].AllowResizing = true;
				gridData.Cols[8].AllowSorting = true;
				gridData.Cols[8].Caption = "Причина";

				gridData.Cols[13].Visible = true;
				gridData.Cols[13].Width = 100;
				gridData.Cols[13].AllowDragging = false;
				gridData.Cols[13].AllowEditing = false;
				gridData.Cols[13].AllowMerging = false;
				gridData.Cols[13].AllowResizing = true;
				gridData.Cols[13].AllowSorting = true;
				gridData.Cols[13].Caption = "№ заказа";

				gridData.Cols[14].Visible = true;
                gridData.Cols[14].Width = 500;
                gridData.Cols[14].AllowDragging = false;
                gridData.Cols[14].AllowEditing = false;
                gridData.Cols[14].AllowMerging = false;
                gridData.Cols[14].AllowResizing = true;
                gridData.Cols[14].AllowSorting = true;
                gridData.Cols[14].Caption = "Комментарий";

                // выделяеем цветом удаленые записи
                int sel = 2;
                for (int i = 2; i < gridData.Rows.Count; i++)
                {
                    if (bool.Parse(gridData.GetData(i, 9).ToString().Trim()))
                    {
                        gridData.Rows[i].Style = gridData.Styles["Green"];
                    }
                    else
                    {
                        gridData.Rows[i].Style = gridData.Styles["Red"];
                    }
                    if (id == int.Parse(gridData.GetData(i, 1).ToString()))
                    {
                        sel = i;
                    }
                }
				try
				{
					gridData.Select(sel, 1);
				}
				catch
				{ }

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                frmEditDiscont f = new frmEditDiscont(db_connection, usr);
                f.ShowDialog();
                if (f.DialogResult == DialogResult.OK)
                    LoadData(0);
                f.Close();
            }
            catch (Exception ex)
            {
                ErrorNfo.WriteErrorInfo(ex);
                MessageBox.Show(
                    "Во время работы произошла ошибка.\nИнформация для разработчика:\n" + ex.Message + "\n" + ex.Source, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridData.Rows[gridData.Row][1].ToString() != "")
                {
                    frmEditDefect f = new frmEditDefect(db_connection, usr, int.Parse(gridData.Rows[gridData.Row][2].ToString()), int.Parse(gridData.Rows[gridData.Row][1].ToString()));
                    f.type = int.Parse(gridData.Rows[gridData.Row][10].ToString());
                    f.good = gridData.Rows[gridData.Row][11].ToString();
                    f.count = decimal.Parse(gridData.Rows[gridData.Row][5].ToString());
                    f.ok = bool.Parse(gridData.Rows[gridData.Row][9].ToString());
                    f.who = int.Parse(gridData.Rows[gridData.Row][12].ToString());
                    f.whoname = gridData.Rows[gridData.Row][7].ToString();
                    f.comment = gridData.Rows[gridData.Row][14].ToString();
                    f.ShowDialog();
                    if (f.DialogResult == DialogResult.OK)
                        LoadData(int.Parse(gridData.Rows[gridData.Row][1].ToString()));
                    f.Close();
                }
            }
            catch (Exception ex)
            {
                ErrorNfo.WriteErrorInfo(ex);
                MessageBox.Show(
                    "Во время работы произошла ошибка.\nИнформация для разработчика:\n" + ex.Message + "\n" + ex.Source, "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (CheckState(db_connection))
            {
                try
                {
                    if (MessageBox.Show("Удалить эту запись?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
                    {
                        SqlCommand c =
                            new SqlCommand(
                                "UPDATE [dcard] SET [del] = 1" +
                                " WHERE id_dcard = " + gridData.Rows[gridData.Row][1].ToString(), db_connection);
                        c.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    ErrorNfo.WriteErrorInfo(ex);
                    MessageBox.Show(
                        "Во время работы произошла ошибка.\nИнформация для разработчика:\n" + ex.Message + "\n" + ex.Source, "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    LoadData(0);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void btnApply_Click(object sender, EventArgs e)
		{
			LoadData(0);
		}

    
    }
}
