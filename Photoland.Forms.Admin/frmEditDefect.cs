using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security.User;
using Photoland.Order;

namespace Photoland.Forms.Admin
{
    public partial class frmEditDefect : Form
    {
        private DataTable typed = new DataTable("Type");
        private DataTable users = new DataTable("Users");
        private DataTable mashine = new DataTable("Mashine");
        private DataTable paper = new DataTable("Paper");
        private DataTable goods = new DataTable("Goods");

		PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

        public SqlConnection db_connection;
        public int order = 0;
        public int body = 0;
        public int type = 0;
        public int who = 0;
        public string whoname = "";
        public bool ok = false;
        public string good = "0";
        public string comment = "";
        public UserInfo usr;

        public decimal count = 0;

        public frmEditDefect(SqlConnection db_connection, UserInfo user, int order, int body)
        {
            InitializeComponent();
            this.Text = "Возврат/Списание";
            this.db_connection = db_connection;
            this.order = order;
            this.body = body;
            this.usr = user;
        }

        private void frmQueryCount_KeyPress(object sender, KeyPressEventArgs e)
        {

            if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == '.') || (e.KeyChar == ',') || (e.KeyChar == '-') || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    try
                    {
                        count = decimal.Parse(txtCount.Text);
                        this.DialogResult = DialogResult.OK;
                        e.Handled = true;
                    }
                    catch
                    {
                    }
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    count = 0;
                    this.DialogResult = DialogResult.Cancel;
                    e.Handled = true;
                }
                else if ((e.KeyChar == '.') || (e.KeyChar == ','))
                {
                    if (txtCount.Text.Length > 0)
                    {
                        if ((txtCount.Text.IndexOf(',') > 0) || (txtCount.Text.IndexOf('.') > 0))
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

        private void frmEditDefect_Load(object sender, EventArgs e)
        {

            SqlCommand tg = new SqlCommand("SELECT [id_good], RTRIM([name]) + CASE WHEN [type] = 2 THEN ' *' ELSE '' END AS [name] FROM [vwGoodList] ORDER BY [type], [name]", db_connection);
            SqlDataAdapter ta = new SqlDataAdapter(tg);
            ta.Fill(goods);

            DataRow rw;
            rw = goods.NewRow();
            rw["id_good"] = 0;
            rw["name"] = "< ! не выбрано ! >";
            goods.Rows.InsertAt(rw, 0);


            txtGood.DataSource = goods;
            txtGood.ValueMember = "id_good";
            txtGood.DisplayMember = "name";

            txtGood.SelectedValue = good;
            
            // Заполняем брак
            typed.Columns.Add("code", System.Type.GetType("System.Int32"));
            typed.Columns.Add("name", System.Type.GetType("System.String"));

            object[] r = new object[2];

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
            r[0] = 5;
            r[1] = "Индекс принт";
            typed.Rows.Add(r);
            r[0] = 6;
            r[1] = "Металик";
            typed.Rows.Add(r);
            r[0] = 7;
            r[1] = "Отмена (корректировка ввода)";
            typed.Rows.Add(r);
            r[0] = 8;
            r[1] = "S Print";
            typed.Rows.Add(r);
			r[0] = 9;
			r[1] = "Не стандартная обрезка";
			typed.Rows.Add(r);
			r[0] = 10;
			r[1] = "Возврат";
			typed.Rows.Add(r);

            txtType.DataSource = typed;
            txtType.ValueMember = "code";
            txtType.DisplayMember = "name";

            txtType.SelectedValue = type;

            txtCount.Text = count.ToString();


            checkOK.Checked = ok;


            SqlCommand tmpcmd = new SqlCommand("SELECT [id_user], [name] FROM [user] ORDER BY [id_post], [name]", db_connection);
            SqlDataAdapter adapter = new SqlDataAdapter(tmpcmd);
            adapter.Fill(users);

            rw = users.NewRow();
            rw["id_user"] = 0;
            rw["name"] = "< ! не выбрано ! >";
            users.Rows.InsertAt(rw, 0);

            txtComment.Text = comment.Trim();

            txtUser.DataSource = users;
            txtUser.ValueMember = "id_user";
            txtUser.DisplayMember = "name";

            txtUser.SelectedValue = who;
            if (who == 0)
            {
                btnOK.Enabled = false;
                lblwho.Text = whoname;
                lblwho.Visible = true;
            }

            SqlCommand tcmd = new SqlCommand("SELECT [name_accept], [name_operator], [name_designer], [name_delivery], [id_order] FROM [order] WHERE [id_order] = " + order.ToString(), db_connection);
            SqlDataReader rdr = tcmd.ExecuteReader();
            if (rdr.Read())
            {
                if (!rdr.IsDBNull(0))
                    lblInfoInput.Text = rdr.GetString(0).Trim();
                else
                    lblInfoInput.Text = "";

                if (!rdr.IsDBNull(1))
                    lblInfoOperator.Text = rdr.GetString(1).Trim();
                else
                    lblInfoOperator.Text = "";

                if (!rdr.IsDBNull(2))
                    lblInfoDesigner.Text = rdr.GetString(2).Trim();
                else
                    lblInfoDesigner.Text = "";

            }
            rdr.Close();

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string _ok = checkOK.Checked ? "1" : "0";
				
                SqlCommand save = new SqlCommand(
                                    "UPDATE [dbo].[orderbody]" +
                                    "SET  [id_user_defect] = " + txtUser.SelectedValue.ToString() +
                                    "    ,[user_defect] = '" + ((System.Data.DataRowView)(txtUser.SelectedItem)).Row.ItemArray[1].ToString().Trim() + "'" +
                                    "    ,[tech_defect] = " + txtType.SelectedValue.ToString() +
                                    "    ,[exported] = 0" +
									"	 ,[defect_quantity] = " + txtCount.Text.Replace(",", ".") +
									(((txtType.SelectedValue.ToString() == "10") || (txtType.SelectedValue.ToString() == "7")) ? 
										",[actual_quantity] = " + (decimal.Parse(txtCount.Text) * -1).ToString().Replace(",", ".") :
										""
									) +
                                    "    ,[defect_ok] = " + _ok +
                                    "    ,[comment] = '" + txtComment.Text.Trim() + "'" +
                                    "WHERE [id_orderbody] = " + body.ToString(), db_connection);
                save.ExecuteNonQuery();
                if (order > 0)
                {
                    save = new SqlCommand(
                                        "UPDATE [dbo].[order]" +
                                        "SET [exported] = 0" +
                                        "WHERE [id_order] = " + order, db_connection);
                    save.ExecuteNonQuery();
                }
				if (order > 0)
					AddEvent("Изменения в списании", order);
            }
            catch (Exception ex)
            {
                ErrorNfo.WriteErrorInfo(ex);
            }
            finally
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

		private void AddEvent(string Event, int id)
		{
			try
			{
				OrderInfo order = new OrderInfo(db_connection, id);
				string body = "";
				for (int i = 0; i < order.OrderBody.Rows.Count; i++)
				{
					body += order.OrderBody.Rows[i][9].ToString() + "|" +
							order.OrderBody.Rows[i][1].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][3].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][4].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][5].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][6].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][7].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][18].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][13].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][11].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][16].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][17].ToString().Trim();
					body += "#";
				}
				body = body.Substring(0, body.Length - 1);
				body = "$$" + body + "$$" + order.AdvancedPayment + "$$" + order.FinalPayment + "$$" + order.Bonus;
				SqlCommand _cmd = new SqlCommand("INSERT INTO [dbo].[orderevent] ([id_order], [event_user], [event_status], [event_point], [event_text]) VALUES (" +
							order.Id + ", '" + usr.Name.Trim() + "', '" + order.Distanation + "', '" + prop.Order_prefics.Trim() +
							"', '" + Event + body + "')", db_connection);
				_cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
			}
		}

    }
}
