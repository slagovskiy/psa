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
	public partial class frmDiscard : Form
	{
		public SqlConnection db_connection = new SqlConnection();
		public UserInfo usr;
		public PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
		private DataTable material = new DataTable("material");
		public string mashine = "";

		private string _orderno = "";
		public string OrderNo
		{
			get { return txtOrderNo.Text; }
			set { _orderno = value; }
		}

		private int _id = 0;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}


		public frmDiscard()
		{
			InitializeComponent();
			this.Text = "Списание материала";
		}

		private void frmBadWorkForm_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (this.ActiveControl.Name == "txtQuantity")
			{
				if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') ||
					(e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') ||
					(e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == '.') || (e.KeyChar == ',') ||
					(e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) ||
					(e.KeyChar == (char)Keys.Escape))
				{
					if (e.KeyChar == (char)Keys.Return)
					{
						/*
						this._payment = decimal.Parse(txtPayment.Text);
						this.DialogResult = DialogResult.OK;
						*/
						e.Handled = true;
					}
					else if (e.KeyChar == (char)Keys.Escape)
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

		private void FillMaterial()
		{
			material.Rows.Clear();
			SqlCommand m = new SqlCommand("SELECT [id_material], [material] FROM [vwMaterialListDesigner] WHERE sklad = (SELECT [sklad] FROM [mashine] WHERE [id_mashine] = '" + txtMashine.SelectedValue.ToString() + "')", db_connection);
			SqlDataAdapter a = new SqlDataAdapter(m);
			a.Fill(material);

			DataRow r;
			r = material.NewRow();
			r["id_material"] = "0";
			r["material"] = "< ! не выбрано ! >";
			material.Rows.InsertAt(r, 0);

			txtMaterial.DataSource = material;
			txtMaterial.ValueMember = "id_material";
			txtMaterial.DisplayMember = "material";
			txtMaterial.SelectedValue = 0;
		}

		private void frmDiscard_Load(object sender, EventArgs e)
		{
			/*
			SqlCommand m = new SqlCommand("SELECT [id_material], [material], [sklad] FROM [vwMaterialListDesigner] ORDER BY [material]", db_connection);
			SqlDataAdapter a = new SqlDataAdapter(m);
			a.Fill(material);

			DataRow r;
			r = material.NewRow();
			r["id_material"] = "0";
			r["material"] = "< ! не выбрано ! >";
			material.Rows.InsertAt(r, 0);

			txtMaterial.DataSource = material;
			txtMaterial.ValueMember = "id_material";
			txtMaterial.DisplayMember = "material";
			txtMaterial.SelectedValue = 0;
			*/
			DataRow r;
			SqlCommand m;
			m = new SqlCommand("SELECT [id_mashine], [mashine], [sklad] FROM [vwMashine] ORDER BY [mashine]", db_connection);
			SqlDataAdapter am = new SqlDataAdapter(m);
			DataTable mashine = new DataTable("mashine");
			am.Fill(mashine);

			r = mashine.NewRow();
			r["id_mashine"] = "0";
			r["mashine"] = "< ! не выбрано ! >";
			mashine.Rows.InsertAt(r, 0);

			txtMashine.DataSource = mashine;
			txtMashine.DisplayMember = "mashine";
			txtMashine.ValueMember = "id_mashine";

			if (this.mashine != "")
			{
				txtMashine.SelectedValue = this.mashine;
			}
			else
			{
				txtMashine.SelectedValue = 0;
			}
			

			txtQuantity.Text = "0";

			if (_id != 0)
			{
				SqlCommand tmprec =
					new SqlCommand(
						"SELECT [id_discard], [id_material], [quantity], [comment], [orderno] FROM [discard] WHERE [id_discard] = " +
						this._id.ToString(), db_connection);
				SqlDataReader tmprdr = tmprec.ExecuteReader();
				if (tmprdr.Read())
				{
					if (!tmprdr.IsDBNull(1))
						txtMaterial.SelectedValue = tmprdr.GetString(1);
					else
						txtMaterial.SelectedValue = "0";

					if (!tmprdr.IsDBNull(2))
						txtQuantity.Text = tmprdr.GetDecimal(2).ToString();
					else
						txtQuantity.Text = "0";

					if (!tmprdr.IsDBNull(3))
						txtComment.Text = tmprdr.GetString(3);
					else
						txtComment.Text = "";

					if (!tmprdr.IsDBNull(4))
						txtOrderNo.Text = tmprdr.GetString(4);
					else
						txtOrderNo.Text = "";

					txtOrderNo.Text = txtOrderNo.Text.Trim();
					txtComment.Text = txtComment.Text.Trim();
				}
				tmprdr.Close();
			}
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

		private void btnOK_Click(object sender, EventArgs e)
		{
			try
			{
				if (this._id == 0)
				{
					if ((decimal.Parse(txtQuantity.Text) != 0) && (txtMaterial.SelectedValue.ToString() != "0") && (txtMashine.SelectedValue.ToString() != "0"))
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
									"INSERT INTO [discard] ([del], [guid], [datediscard], [id_material], [quantity], [comment], [id_user], [user_name], [orderno], [id_mashine]) VALUES (0,'" +
									System.Guid.NewGuid().ToString() + "','" + DateTime.Now.Year.ToString() + "/" + m + "/" + d + " " +
									DateTime.Now.ToShortTimeString() + "','" + txtMaterial.SelectedValue.ToString() + "'," +
									txtQuantity.Text.Replace(",", ".") + ",'" + txtComment.Text + "'," + usr.Id_user.ToString() + ",'" + usr.Name +
									"','" + txtOrderNo.Text + "', '" + txtMashine.SelectedValue.ToString() + "')", db_connection);
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
					if ((decimal.Parse(txtQuantity.Text) != 0) && (txtMaterial.SelectedValue.ToString() != "0"))
					{
						try
						{
							SqlCommand t =
								new SqlCommand(
									"UPDATE [discard] SET [id_material] = " + txtMaterial.SelectedValue.ToString() + ", [quantity] = " +
									txtQuantity.Text.Replace(",", ".") + ", [comment] = '" + txtComment.Text + "', [id_user] = " + usr.Id_user +
									", [user_name] = '" + usr.Name + "', [orderno] = '" + txtOrderNo.Text + "' WHERE [id_discard] = " +
									this._id.ToString(), db_connection);
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
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}

		private void txtMashine_SelectedIndexChanged(object sender, EventArgs e)
		{
			FillMaterial();
		}




	}
}