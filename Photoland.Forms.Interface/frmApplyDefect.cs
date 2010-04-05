using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Interface
{
	public partial class frmApplyDefect : Form
	{
		private DataTable typed = new DataTable("Type");
		private DataTable users = new DataTable("Users");
		private DataTable mashine = new DataTable("Mashine");
		private DataTable paper = new DataTable("Paper");
		private DataTable goods = new DataTable("Goods");

		public SqlConnection db_connection;
		public string OrderNo = "";
		public string GoodsFilter = "";
		public string status = "";
        public List<string> goodsfromorder = new List<string>();

		private decimal _count = 0;
		public decimal Count
		{
			get { return _count; }
			set { _count = value; }
		}

		public frmApplyDefect()
		{
			InitializeComponent();
			this.Text = "Возврат/Списание";
		}

		private void frmQueryCount_KeyPress(object sender, KeyPressEventArgs e)
		{

			if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == '.') || (e.KeyChar == ',') || (e.KeyChar == '-') || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
			{
				if (e.KeyChar == (char)Keys.Return)
				{
					try
					{
						_count = decimal.Parse(txtCount.Text);
						this.DialogResult = DialogResult.OK;
						e.Handled = true;
					}
					catch
					{
					}
				}
				else if (e.KeyChar == (char)Keys.Escape)
				{
					_count = 0;
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

		private void frmApplyDefect_Load(object sender, EventArgs e)
		{
			txtCount.Text = "0";

			SqlCommand tg = new SqlCommand("SELECT [id_good], RTRIM([name]) + CASE WHEN [type] = 2 THEN ' *' ELSE '' END AS [name] FROM [vwGoodList] WHERE [id_good] IN (" + GoodsFilter + ") ORDER BY [type], [name]", db_connection);
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


			object[] r = new object[2];

			// Заполняем брак
			typed.Columns.Add("code", System.Type.GetType("System.Int32"));
			typed.Columns.Add("name", System.Type.GetType("System.String"));

			r[0] = 0;
			r[1] = "< ! не выбрано ! >";
			typed.Rows.Add(r);
			//r[0] = 1;
			//r[1] = "Брак";
			//typed.Rows.Add(r);
			//r[0] = 2;
			//r[1] = "Тех. Брак";
			//typed.Rows.Add(r);
			//r[0] = 3;
			//r[1] = "Setup";
			//typed.Rows.Add(r);
			//r[0] = 4;
			//r[1] = "Обрезка";
			//typed.Rows.Add(r);
			//r[0] = 5;
			//r[1] = "Индекс принт";
			//typed.Rows.Add(r);
			//r[0] = 6;
			//r[1] = "Металик";
			//typed.Rows.Add(r);
			if((status != "300000") && (status != "200000") && (status != "100000"))
			{
				r[0] = 7;
				r[1] = "Отмена (корректировка ввода)";
				typed.Rows.Add(r);
			}
			r[0] = 10;
			r[1] = "Возврат";
			typed.Rows.Add(r);

			txtType.DataSource = typed;
			txtType.ValueMember = "code";
			txtType.DisplayMember = "name";

			SqlCommand tmpcmd = new SqlCommand("SELECT [id_user], [name] FROM [vwUserPostList] ORDER BY [post], [user]", db_connection);
			SqlDataAdapter adapter = new SqlDataAdapter(tmpcmd);
			adapter.Fill(users);

			rw = users.NewRow();
			rw["id_user"] = 0;
			rw["name"] = "< ! не выбрано ! >";
			users.Rows.InsertAt(rw, 0);

			txtUser.DataSource = users;
			txtUser.ValueMember = "id_user";
			txtUser.DisplayMember = "name";

			int orderid = 0;
			SqlCommand tcmd = new SqlCommand("SELECT [name_accept], [name_operator], [name_designer], [name_delivery], [id_order] FROM [order] WHERE [number] = '" + OrderNo + "'", db_connection);
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

				if (!rdr.IsDBNull(4))
					orderid = rdr.GetInt32(4);
				else
					orderid = 0;
			}
			rdr.Close();


			SqlCommand tmpcmd1 = new SqlCommand("SELECT dbo.orderbody.id_mashine, dbo.mashine.mashine FROM dbo.orderbody INNER JOIN dbo.material ON dbo.orderbody.id_material = dbo.material.id_material INNER JOIN dbo.mashine ON dbo.orderbody.id_mashine = dbo.mashine.id_mashine GROUP BY dbo.orderbody.id_order, dbo.orderbody.id_mashine, dbo.mashine.mashine HAVING (dbo.orderbody.id_order = " + orderid.ToString() + ")", db_connection);
			SqlDataAdapter adapter1 = new SqlDataAdapter(tmpcmd1);
			adapter1.Fill(mashine);

			rw = mashine.NewRow();
			rw["id_mashine"] = 0;
			rw["mashine"] = "< ? не выбрано ? >";
			mashine.Rows.InsertAt(rw, 0);

			txtMashine.DataSource = mashine;
			txtMashine.ValueMember = "id_mashine";
			txtMashine.DisplayMember = "mashine";

			SqlCommand tmpcmd2 = new SqlCommand("SELECT dbo.orderbody.id_material, dbo.material.material FROM dbo.orderbody INNER JOIN dbo.material ON dbo.orderbody.id_material = dbo.material.id_material INNER JOIN dbo.mashine ON dbo.orderbody.id_mashine = dbo.mashine.id_mashine GROUP BY dbo.orderbody.id_order, dbo.orderbody.id_material, dbo.material.material HAVING (dbo.orderbody.id_order = " + orderid.ToString() + ")", db_connection);
			SqlDataAdapter adapter2 = new SqlDataAdapter(tmpcmd2);
			adapter2.Fill(paper);

			rw = paper.NewRow();
			rw["id_material"] = 0;
			rw["material"] = "< ? не выбрано ? >";
			paper.Rows.InsertAt(rw, 0);

			txtPaper.DataSource = paper;
			txtPaper.ValueMember = "id_material";
			txtPaper.DisplayMember = "material";

		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				if ((txtCount.Text != "") && (txtType.SelectedValue != "0") && (txtUser.SelectedValue != "0"))
				{
					decimal t = decimal.Parse(txtCount.Text);
					//((t != 0) && (txtUser.SelectedValue.ToString() != "0") && (txtType.SelectedValue.ToString() != "0") &&
					//	(((txtGood.Text.Substring(txtGood.Text.Length - 2, 2) != " *") &&
					//	  ((txtPaper.SelectedValue.ToString() != "0") && (txtMashine.SelectedValue.ToString() != "0"))) ||
					//	 ((txtGood.Text.Substring(txtGood.Text.Length - 2, 2) == " *") &&
					//	  ((txtPaper.SelectedValue.ToString() == "0") && (txtMashine.SelectedValue.ToString() == "0"))) ||
					//	  (txtType.SelectedValue.ToString() == "7")))
				    bool ok = true;
				    bool found = false;
				    string mess = "";
                    for (int i = 0; ((i < goodsfromorder.Count) && (!found)); i++)
                    {
                        decimal tmp_c = decimal.Parse(goodsfromorder[i].Split(';')[1]);
                        string tmp_g = goodsfromorder[i].Split(';')[0];

						if ((txtGood.SelectedValue.ToString().Trim() == tmp_g.Trim()) && (tmp_c > 0))
						{
							found = true;
							if (decimal.Parse(txtCount.Text) > tmp_c)
							{
								ok = false;
								mess = "Списание не может превышать количество выполненного!";
								break;
							}
							else if ((txtType.SelectedValue.ToString() != "10") && (tmp_c == 0))
							{
								ok = false;
								mess = "На не выполненные услуги корректировку сделать нельзя!";
								break;
							}
						}
						else if ((txtGood.SelectedValue.ToString().Trim() == tmp_g.Trim()) && (tmp_c == 0))
						{
							ok = false;
							mess = "На не выполненные услуги корректировку сделать нельзя!";
							break;
						}
                    }
                    if ((txtType.SelectedValue.ToString() == "10") && (!found))
                    {
                        ok = false;
                        mess = "На не выполненные услуги делать возвраты нельзя!";
                    }

                    if (ok)
                    {

                        if ((t != 0) && (txtUser.SelectedValue.ToString() != "0") &&
                            (txtType.SelectedValue.ToString() != "0") &&
                            ((txtGood.Text.Substring(txtGood.Text.Length - 2, 2) != " *") ||
                             (txtGood.Text.Substring(txtGood.Text.Length - 2, 2) == " *") ||
                             (txtType.SelectedValue.ToString() == "7")))
                        {
                            DialogResult = System.Windows.Forms.DialogResult.OK;
                        }
                        else
                        {
                            MessageBox.Show("Заполните все поля!", "Возврат/Списание", MessageBoxButtons.OK,
                                            MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show(mess, "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
				}
			}
			catch(Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				DialogResult = System.Windows.Forms.DialogResult.Cancel;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
		}

		private void txtGood_SelectedIndexChanged(object sender, EventArgs e)
		{
			if ((txtGood.Text != "") && (txtGood.Text.Length > 1))
			{
				if (txtGood.Text.Substring(txtGood.Text.Length - 2, 2) == " *")
				{
					txtPaper.SelectedValue = 0;
					txtPaper.Enabled = false;
					txtMashine.SelectedValue = 0;
					txtMashine.Enabled = false;
				}
				else
				{
					//txtPaper.Enabled = true;
					//txtMashine.Enabled = true;
				}
			}
		}

		private void txtType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (txtType.SelectedValue.ToString() == "7")
			{
				txtPaper.Enabled = false;
				txtMashine.Enabled = false;
			}
		}
	}
}