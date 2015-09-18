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


namespace Photoland.Forms.Interface
{
	public partial class frmClient : Form
	{
		private string id = "";
		public SqlConnection db_connection;
		public UserInfo usr;
		private DataTable tbl = new DataTable("Client");
		SqlDataAdapter db_adapter = new SqlDataAdapter();
		public string guid = Guid.NewGuid().ToString();
	    public int input = 0;

		public frmClient()
		{
			InitializeComponent();
			this.Text = "Новый клиент";

			this.id = "";
		}

		public frmClient(string id)
		{
			InitializeComponent();
			this.Text = "Клиент";

			this.id = id;

		}

		private void frmClient_Load(object sender, EventArgs e)
		{
			tbl.Clear();
			SqlCommand db_command = new SqlCommand();
			db_command.Connection = db_connection;
            if (this.id != "")
            {
                db_command.CommandText =
                    "SELECT [id_category], [guid], [del], [name] FROM [vwCategoryFull] ORDER BY [name]";
            }
            else
            {
                if (input == 0)
                    db_command.CommandText =
                        "SELECT [id_category], [guid], [del], [name] FROM [vwCategoryFull] ORDER BY [name]";
                else
                    db_command.CommandText =
                        "SELECT [id_category], [guid], [del], [name] FROM [vwCategoryFull] WHERE [input] = 1 ORDER BY [name]";
            }
		    db_adapter.SelectCommand = db_command;
			db_adapter.Fill(tbl);
			txtCategory.DataSource = tbl;
			txtCategory.DisplayMember = "name";
			txtCategory.ValueMember = "id_category";
			if (this.id != "")
			{
				db_command.CommandText = "SELECT [id_client], [id_category], [guid], [del], [name], [phone_1], [phone_2], [address], [email], [icq], [addon], [categoryguid], [categoryname], [input] FROM [vwClientFull] WHERE [id_client] = " + this.id;
				SqlDataReader db_reader1;
				db_reader1 = db_command.ExecuteReader();
				while (db_reader1.Read())
				{
					if (!db_reader1.IsDBNull(4))
						txtName.Text = db_reader1.GetString(4).Trim();
					else
						txtName.Text = "";
					if (!db_reader1.IsDBNull(5))
						txtPhone1.Text = db_reader1.GetString(5).Trim();
					else
						txtPhone1.Text = "";
					if (!db_reader1.IsDBNull(6))
						txtPhone2.Text = db_reader1.GetString(6).Trim();
					else
						txtPhone2.Text = "";
					if (!db_reader1.IsDBNull(7))
						txtAddress.Text = db_reader1.GetString(7).Trim();
					else
						txtAddress.Text = "";
					if (!db_reader1.IsDBNull(8))
						txtEmail.Text = db_reader1.GetString(8).Trim();
					else
						txtEmail.Text = "";
					if (!db_reader1.IsDBNull(9))
						txtICQ.Text = db_reader1.GetString(9).Trim();
					else
						txtICQ.Text = "";
					if (!db_reader1.IsDBNull(10))
						txtAddon.Text = db_reader1.GetString(10).Trim();
					else
						txtAddon.Text = "";
					if (!db_reader1.IsDBNull(1))
						txtCategory.SelectedValue = db_reader1.GetInt32(1);

                    if (!db_reader1.IsDBNull(13))
                    {
                        if (db_reader1.GetInt32(13) != 1)
                        {
                            btnSave.Enabled = false;
                            btnClear.Enabled = false;
                            btnCancel.Enabled = false;
                            MessageBox.Show("Вы не можете редактированть параметры клиента из данной категории!",
                                            "Клиент", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    txtCategory.Enabled = false;
				}
				db_reader1.Close();
			}
			else
			{
				txtName.Text = "";
				txtPhone1.Text = "";
				txtPhone2.Text = "";
				txtAddress.Text = "";
				txtEmail.Text = "";
				txtICQ.Text = "";
				txtAddon.Text = "";
			}
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			SqlCommand db_command = new SqlCommand();
			db_command.Connection = db_connection;
			if (this.id != "")
			{
				db_command.CommandText = "UPDATE [client] SET [id_category] = " + txtCategory.SelectedValue.ToString() + ", [name] = '" + txtName.Text + "', [phone_1] = '" + txtPhone1.Text + "', [phone_2] = '" + txtPhone2.Text + "', [address] = '" + txtAddress.Text + "', [email] = '" + txtEmail.Text + "', [icq] = '" + txtICQ.Text + "', [addon] = '" + txtAddon.Text + "' WHERE [id_client] = " + this.id;
				db_command.ExecuteNonQuery();
				this.DialogResult = DialogResult.OK;
			}
			else
			{
				db_command.CommandText = "INSERT INTO [client] ([id_category], [id_dcard], [guid], [del], [name], [phone_1], [phone_2], [address], [email], [icq], [addon]) VALUES(" + txtCategory.SelectedValue.ToString() + ", 0, '" + this.guid + "', 0, '" + txtName.Text + "', '" + txtPhone1.Text + "', '" + txtPhone2.Text + "', '" + txtAddress.Text + "', '" + txtEmail.Text + "', '" + txtICQ.Text + "', '" + txtAddon.Text + "')";
				db_command.ExecuteNonQuery();
				this.DialogResult = DialogResult.OK;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			this.frmClient_Load(sender, e);
		}

	}
}