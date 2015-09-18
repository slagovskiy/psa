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
using Photoland.Security;

namespace Photoland.Forms.Interface
{
	public partial class frmLogin : Form
	{
		private SqlConnection db_connection;
		private SqlCommand db_command;
		private SqlDataReader db_reader;
		private Util.Settings prop = new Util.Settings();
		public UserInfo usr;

		

		public frmLogin()
		{
			InitializeComponent();
			this.Text = "Вход в программу";
			db_connection = new SqlConnection();
			db_connection.ConnectionString = prop.Connection_string;
			db_connection.Open();
			db_command = new SqlCommand();
			db_command.Connection = db_connection;
			db_command.CommandText = "SELECT * FROM [vwUserList]";
			db_reader = db_command.ExecuteReader();
			while (db_reader.Read())
			{
				txtLogin.Items.Add(db_reader[1].ToString());
			}
			db_reader.Close();

		}

		public bool FormOK = false;

		private void btnLogin_Click(object sender, EventArgs e)
		{
			db_command.CommandText = "SELECT [id_user], [id_post], [id_point], [guid], [name], [сontact], [login], [password], [permission], [postname], [pointname] FROM [vwUserFull] WHERE [login] = '" + txtLogin.Text + "' AND [password] = '" + txtPassword.Text + "'";
			db_reader = db_command.ExecuteReader();
			if (db_reader.Read())
			{
				this.FormOK = true;
				this.DialogResult = DialogResult.OK;
				usr = new UserInfo();
				if(!db_reader.IsDBNull(0))
					usr.Id_user = Convert.ToInt32(db_reader[0]);
				if (!db_reader.IsDBNull(1))
					usr.Id_point = Convert.ToInt32(db_reader[1]);
				if (!db_reader.IsDBNull(2))
					usr.Id_post = Convert.ToInt32(db_reader[2]);
				if (!db_reader.IsDBNull(3))
					usr.Guid = Convert.ToString(db_reader[3]);
				if (!db_reader.IsDBNull(4))
					usr.Name = Convert.ToString(db_reader[4]);
				if (!db_reader.IsDBNull(5))
					usr.Contact = Convert.ToString(db_reader[5]);
				if (!db_reader.IsDBNull(6))
					usr.Login = Convert.ToString(db_reader[6]);
				if (!db_reader.IsDBNull(7))
					usr.Password = Convert.ToString(db_reader[7]);
				if (!db_reader.IsDBNull(8))
					usr.Permission = Convert.ToInt64(db_reader[8]);
				if (!db_reader.IsDBNull(9))
					usr.Post = Convert.ToString(db_reader[9]);
				if (!db_reader.IsDBNull(10))
					usr.Point = Convert.ToString(db_reader[10]);
				if (!usr.prmCanLogin)
				{
					this.FormOK = false;
					this.DialogResult = DialogResult.Retry;
					MessageBox.Show("Доступ в программу запрещен!", "Вход в программу", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			else
			{
				this.FormOK = false;
				this.DialogResult = DialogResult.Retry;
				MessageBox.Show("Неверный пароль!", "Вход в программу", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			db_reader.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.FormOK = false;
			this.DialogResult = DialogResult.Cancel;
		}
	}
}