using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PSA.Lib.Interface
{
	public partial class frmWasteEdit : PSA.Lib.Interface.Templates.frmTDialog
	{
		public Photoland.Security.User.UserInfo usr;
		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
		public int id = 0;


		public frmWasteEdit()
		{
			InitializeComponent();
			this.Title = "Расход";
		}

		private void frmWasteEdit_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void LoadData()
		{
			try
			{
				DataRow r;
				DataTable tblmaterial = new DataTable();
				using (SqlConnection con = new SqlConnection(prop.Connection_string))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT id_material, material FROM material ORDER BY material", con))
					{
						cmd.CommandTimeout = 9000;
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						da.Fill(tblmaterial);
					}
				}

				r = tblmaterial.NewRow();
				r["id_material"] = "0";
				r["material"] = "< не выбрано >";
				tblmaterial.Rows.InsertAt(r, 0);

				txtMaterial.DataSource = tblmaterial;
				txtMaterial.DisplayMember = "material";
				txtMaterial.ValueMember = "id_material";

				DataTable tblmashine = new DataTable();
				using (SqlConnection con = new SqlConnection(prop.Connection_string))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT id_mashine, mashine FROM mashine ORDER BY mashine", con))
					{
						cmd.CommandTimeout = 9000;
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						da.Fill(tblmashine);
					}
				}

				r = tblmashine.NewRow();
				r["id_mashine"] = "0";
				r["mashine"] = "< не выбрано >";
				tblmashine.Rows.InsertAt(r, 0);

				txtMashine.DataSource = tblmashine;
				txtMashine.DisplayMember = "mashine";
				txtMashine.ValueMember = "id_mashine";

				if (id == 0)
				{
					txtComment.Text = "";
					txtMashine.SelectedValue = "0";
					txtMaterial.SelectedValue = "0";
					txtQuantity.Text = "0";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace + "\n", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				if ((txtMaterial.SelectedValue.ToString() != "0") &&
					((decimal.Parse(txtQuantity.Text) * 0) == 0))
				{
					using (SqlConnection con = new SqlConnection(prop.Connection_string))
					{
						con.Open();
						using (SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[discard] ([datediscard], [id_material], [quantity], [comment], [id_user], [user_name], [orderno], [id_mashine]) VALUES (getdate(), '" + txtMaterial.SelectedValue.ToString() + "', " + txtQuantity.Text.Replace(",", ".") + ", '" + txtComment.Text.Trim() + "', " + usr.Id_user.ToString() + ", '" + usr.Name + "', '', '" + txtMashine.SelectedValue.ToString() + "')", con))
						{
							cmd.CommandTimeout = 9000;
							cmd.ExecuteNonQuery();
						}
					}
					this.DialogResult = DialogResult.OK;
					this.Close();
				}
				else
				{
					MessageBox.Show("Не все поля заполнены!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace + "\n", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}
	}

}
