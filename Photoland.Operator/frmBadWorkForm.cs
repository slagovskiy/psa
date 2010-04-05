using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security;
using Photoland.Security.User;

namespace Photoland.Operator
{
    public partial class frmBadWorkForm : Form
    {
        public SqlConnection db_connection = new SqlConnection();
        public UserInfo usr;
		public PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

        private DataTable mashine = new DataTable("Mashine");
        private DataTable paper = new DataTable("Paper");
        private DataTable goodt = new DataTable("Good");
        private DataTable typed = new DataTable("Type");

        private string _orderno = "";
        public string OrderNo
        {
            get { return txtOrderNo.Text; }
            set { _orderno = value; }
        }

        private int id = 0;
        public string id_mashine = "";
        public string id_paper = "";
        public string id_good = "";
        public int id_order = 0;

        public frmBadWorkForm()
        {
            InitializeComponent();
            this.Text = "Списание";

        }

        public frmBadWorkForm(int id)
        {
            InitializeComponent();
            this.Text = "Списание";
            this.id = id;
        }


        private void frmBadWorkForm_Load(object sender, EventArgs e)
        {

            object[] r = new object[2];

            SqlCommand tmp =
                new SqlCommand("SELECT [id_mashine], [mashine] FROM [vwMashine] ORDER BY [mashine]", db_connection);
            SqlDataAdapter ad = new SqlDataAdapter(tmp);
            ad.Fill(mashine);

            tmp =
                new SqlCommand("SELECT [id_material], [material] FROM [vwPaper] ORDER BY [material]", db_connection);
            ad = new SqlDataAdapter(tmp);
            ad.Fill(paper);

            tmp =
                new SqlCommand("SELECT [id_good], [name] FROM [vwDefectList] ORDER BY [type], [name]", db_connection);
            ad = new SqlDataAdapter(tmp);
            ad.Fill(goodt);

            txtPaper.DataSource = paper;
            txtPaper.DisplayMember = "material";
            txtPaper.ValueMember = "id_material";
            txtPaper.SelectedValue = "0";

            txtMashine.DataSource = mashine;
            txtMashine.DisplayMember = "mashine";
            txtMashine.ValueMember = "id_mashine";
            txtMashine.SelectedValue = "0";

            

            txtGood.DataSource = goodt;
            txtGood.DisplayMember = "name";
            txtGood.ValueMember = "id_good";
            txtGood.SelectedValue = "0";

            DataRow tmp_row;
            tmp_row = goodt.NewRow();
            tmp_row["id_good"] = "0";
            tmp_row["name"] = "";
            goodt.Rows.InsertAt(tmp_row, 0);

            // Заполняем брак
            typed.Columns.Add("code", System.Type.GetType("System.Int32"));
            typed.Columns.Add("name", System.Type.GetType("System.String"));

            r[0] = 0;
            r[1] = "< ! не выбрано ! >";
            typed.Rows.Add(r);
            r[0] = 1;
            r[1] = "Брак";
            typed.Rows.Add(r);
            r[0] = 2;
            r[1] = "Тех. Брак";
            typed.Rows.Add(r);
            r[0] = 3;
            r[1] = "Setup";
            typed.Rows.Add(r);
            r[0] = 4;
            r[1] = "Обрезка";
            typed.Rows.Add(r);
			r[0] = 9;
			r[1] = "Не стандартная обрезка";
			typed.Rows.Add(r);
            r[0] = 5;
            r[1] = "Индекс принт";
            typed.Rows.Add(r);
			r[0] = 6;
			r[1] = "Металик";
			typed.Rows.Add(r);
			r[0] = 8;
			r[1] = "S Print";
			typed.Rows.Add(r);

            txtType.DataSource = typed;
            txtType.ValueMember = "code";
            txtType.DisplayMember = "name";

            if (id_mashine != "")
            {
                txtMashine.SelectedValue = id_mashine;
            }
            if (id_paper != "")
            {
                txtPaper.SelectedValue = id_paper;
            }
            if (id_good != "")
            {
                txtGood.SelectedValue = id_good;
            }

            if (this.id > 0)
            {
                SqlCommand tmprec = new SqlCommand("SELECT [id_order], [number], [id_mashine], [id_material], [id_good], [defect_quantity], [tech_defect] FROM [vwDefectEdit] WHERE [id_orderbody] = " + this.id.ToString(), db_connection);
                SqlDataReader tmprdr = tmprec.ExecuteReader();
                if (tmprdr.Read())
                {
                    if (!tmprdr.IsDBNull(0))
                        id_order = tmprdr.GetInt32(0);
                    else
                        id_order = 0;

                    if (!tmprdr.IsDBNull(1))
                        txtOrderNo.Text = tmprdr.GetString(1);
                    else
                        txtOrderNo.Text = "";

                    if (!tmprdr.IsDBNull(2))
                        txtMashine.SelectedValue = tmprdr.GetString(2);
                    else
                        txtMashine.SelectedValue = "";

                    if (!tmprdr.IsDBNull(3))
                        txtPaper.SelectedValue = tmprdr.GetString(3);
                    else
                        txtPaper.SelectedValue = "";

                    if (!tmprdr.IsDBNull(4))
                        txtGood.SelectedValue = tmprdr.GetString(4);
                    else
                        txtGood.SelectedValue = "";

                    if (!tmprdr.IsDBNull(5))
                        txtQuantity.Text = tmprdr.GetDecimal(5).ToString();
                    else
                        txtQuantity.Text = "0";

                    if (!tmprdr.IsDBNull(6))
                    {
                        txtType.SelectedValue = tmprdr.GetInt32(6);
                    }
                }
                tmprdr.Close();
            }
        }

        private void frmBadWorkForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.ActiveControl.Name == "txtQuantity")
            {
                if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') ||
                    (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') ||
                    (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == '.') || (e.KeyChar == ',') ||
                    (e.KeyChar == (char) Keys.Delete) || (e.KeyChar == (char) Keys.Back) || (e.KeyChar == (char) Keys.Return) ||
                    (e.KeyChar == (char) Keys.Escape))
                {
                    if (e.KeyChar == (char) Keys.Return)
                    {
                        /*
                        this._payment = decimal.Parse(txtPayment.Text);
                        this.DialogResult = DialogResult.OK;
                        */
                        e.Handled = true;
                    }
                    else if (e.KeyChar == (char) Keys.Escape)
                    {
                        /*
                        this._payment = 0;
                        this.DialogResult = DialogResult.Cancel;
                        */
                        e.Handled = true;
                    }
                    else if ((e.KeyChar == '.') || (e.KeyChar == ','))
                    {
                        if (txtQuantity.Text.Length > 0)
                        {
                            if ((txtQuantity.Text.IndexOf(',') > 0) || (txtQuantity.Text.IndexOf('.') > 0))
                            {
                                e.Handled = true;
                            }
                            else
                            {
                                if (e.KeyChar == '.')
                                    e.KeyChar = ',';
                                e.Handled = false;
                            }
                        }
                        else
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        e.Handled = false;
                    }
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void FormOK()
        {
            try
            {
                if (this.id == 0)
                {
                    if (((decimal.Parse(txtQuantity.Text) > 0) && (txtMashine.SelectedValue.ToString() != "") &&
                         (txtPaper.SelectedValue.ToString() != "")) &&
                        ((((txtGood.SelectedValue.ToString() != "0")) &&
						  (((txtType.SelectedValue.ToString() != "3")) && ((txtType.SelectedValue.ToString() != "4")) && ((txtType.SelectedValue.ToString() != "9")))) ||
                         (((txtGood.SelectedValue.ToString() == "0")) &&
						  (((txtType.SelectedValue.ToString() == "3")) || ((txtType.SelectedValue.ToString() == "4")) || ((txtType.SelectedValue.ToString() == "9"))))))
                    {
                        try
                        {
                            string m = DateTime.Now.Month < 10
                                           ? "0" + DateTime.Now.Month.ToString()
                                           : DateTime.Now.Month.ToString();
                            string d = DateTime.Now.Day < 10
                                           ? "0" + DateTime.Now.Day.ToString()
                                           : DateTime.Now.Day.ToString();
                        	SqlCommand t =
                        		new SqlCommand(
                        			"INSERT INTO [orderbody] ([id_order], [id_mashine], [id_material], [id_good], [guid], [del], [datework], [id_user_work], [name_work], [defect_quantity], [id_user_defect], [user_defect], [tech_defect], [id_user_add], [name_add]) VALUES (" +
                        			id_order.ToString() + ", '" +
                        			txtMashine.SelectedValue.ToString() + "', '" + txtPaper.SelectedValue.ToString() +
                        			"', '" + txtGood.SelectedValue.ToString() + "', '" +
									System.Guid.NewGuid().ToString() + "', 0, getdate(), " + usr.Id_user + ", '" +
                        			usr.Name + "', " + txtQuantity.Text.Replace(",", ".") + ", " + usr.Id_user + ", '" + usr.Name + "', " +
                        			txtType.SelectedValue.ToString() + ", " +
                        			usr.Id_user + ", '" + usr.Name + "')", db_connection);
                            t.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
							ErrorNfo.WriteErrorInfo(ex);
                        }
                        finally
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не указаны обязательные поля!", "Ошибка", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
                else
                {
                    if (((decimal.Parse(txtQuantity.Text) > 0) && (txtMashine.SelectedValue.ToString() != "") &&
                         (txtPaper.SelectedValue.ToString() != "")) &&
                        ((((txtGood.SelectedValue.ToString() != "0")) &&
						  (((txtType.SelectedValue.ToString() != "3")) && ((txtType.SelectedValue.ToString() != "4")) && ((txtType.SelectedValue.ToString() != "9")))) ||
                         (((txtGood.SelectedValue.ToString() == "0")) &&
						  (((txtType.SelectedValue.ToString() == "3")) || ((txtType.SelectedValue.ToString() == "4")) && ((txtType.SelectedValue.ToString() != "9"))))))
                    {
                        try
                        {
                            SqlCommand t =
                                new SqlCommand(
                                    "UPDATE [orderbody] SET [id_order] = " + id_order + ", [id_mashine] = '" +
                                    txtMashine.SelectedValue.ToString() + "', [id_material] = '" +
                                    txtPaper.SelectedValue.ToString() + "', [id_good] = '" +
                                    txtGood.SelectedValue.ToString() + "', [defect_quantity] = " +
                                    txtQuantity.Text.Replace(",", ".") + ", [tech_defect] = " +
                                    txtType.SelectedValue.ToString() + ", exported = 0 " +
									", [id_user_add] = " + usr.Id_user +
									", [name_add] = '" + usr.Name + "' " +
									", [id_user_work] = " + usr.Id_user +
									", [name_work] = '" + usr.Name + "' " +
									" WHERE [id_orderbody] = " + this.id,
                                    db_connection);
                            t.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
							ErrorNfo.WriteErrorInfo(ex);
                        }
                        finally
                        {
                            this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: Не указаны обязательные поля!", "Ошибка", MessageBoxButtons.OK,
                                        MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex)
            {
				ErrorNfo.WriteErrorInfo(ex);
            }
        }

        private void FormCancel()
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            FormOK();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FormCancel();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text != "")
                txtQuantity.Text = (decimal.Parse(txtQuantity.Text) + 1).ToString();
            else
                txtQuantity.Text = "0";
        }

        private void btnSub_Click(object sender, EventArgs e)
        {
            if (txtQuantity.Text != "")
                if (decimal.Parse(txtQuantity.Text) > 0)
                {
                    txtQuantity.Text = (decimal.Parse(txtQuantity.Text) - 1).ToString();
                }
                else
                {
                }
            else
            {
                txtQuantity.Text = "0";
            }
        }

        private void txtType_SelectedIndexChanged(object sender, EventArgs e)
        {
			if ((txtType.SelectedValue.ToString() == "3") || (txtType.SelectedValue.ToString() == "4") || (txtType.SelectedValue.ToString() == "9"))
			{
				txtGood.SelectedValue = "0";
				txtGood.Enabled = false;
			}
			else
			{
				txtGood.Enabled = true;
			}
			if (txtType.SelectedValue.ToString() == "9")
			{
				label5.Text = "Длинна (м)";
			}
			else
			{
				label5.Text = "Количество";
			}
		}

    }
}