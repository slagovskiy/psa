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


namespace Photoland.Forms.Admin
{
	public partial class frmEditPayment : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;

		private SqlCommand db_command;
		private int id_payment;

		public frmEditPayment(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Новый платеж";
			this.db_connection = db_connection;
			this.usr = usr;
		}

		public frmEditPayment(SqlConnection db_connection, UserInfo usr, int id_payment)
		{
			InitializeComponent();
			this.Text = "Платеж";
			this.db_connection = db_connection;
			this.usr = usr;
			this.id_payment = id_payment;

			db_command = new SqlCommand("SELECT [id_payment], [guid], [date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way] FROM [vwPaymentFull] WHERE [id_payment] = " + this.id_payment.ToString(), db_connection);
			SqlDataReader db_reader = db_command.ExecuteReader();
			if (db_reader.Read())
			{
				if (!db_reader.IsDBNull(2))
					txtDate.Value = db_reader.GetDateTime(2);

				if (!db_reader.IsDBNull(6))
					txtOrder.Text = db_reader.GetString(6).Trim();

				if (!db_reader.IsDBNull(7))
					txtPayment.Text = db_reader.GetDecimal(7).ToString();

				if (!db_reader.IsDBNull(9))
					txtComment.Text = db_reader.GetString(9).Trim();

				db_reader.Close();
			}
			else
			{
				db_reader.Close();
				MessageBox.Show("Запись не найдена в базе!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.DialogResult = DialogResult.Cancel;
			}
		}

		private void frmEditPayment_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (txtPayment.Focused)
			{
				if ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == '.') || (e.KeyChar == ',') || (e.KeyChar == '-') || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))
				{
					if (e.KeyChar == (char)Keys.Return)
					{
						e.Handled = true;
					}
					else if (e.KeyChar == (char)Keys.Escape)
					{
						txtPayment.Text = "0";
						e.Handled = true;
					}
					else if ((e.KeyChar == '.') || (e.KeyChar == ','))
					{
						if (txtPayment.Text.Length > 0)
						{
							if ((txtPayment.Text.IndexOf(',') > 0) || (txtPayment.Text.IndexOf('.') > 0))
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

		private void btnOK_Click(object sender, EventArgs e)
		{
			string yin, min, din;

			yin = txtDate.Value.Year.ToString();

			if (txtDate.Value.Month < 10)
				min = "0" + txtDate.Value.Month.ToString();
			else
				min = txtDate.Value.Month.ToString();

			if (txtDate.Value.Day < 10)
				din = "0" + txtDate.Value.Day.ToString();
			else
				din = txtDate.Value.Day.ToString();


			if (this.id_payment > 0)
			{
				db_command = new SqlCommand("UPDATE [payments] SET [date] = CONVERT(DATETIME, '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() + "', 120), [time] = '" + DateTime.Now.ToShortTimeString() + "', [id_user] = " + usr.Id_user.ToString() + ", [name_user] = '" + usr.Name + "', [number] = '" + txtOrder.Text + "', [payment] = " + txtPayment.Text.Replace(',', '.') + ", [type] = 4, [comment] = '" + txtComment.Text + "', [payment_way] = 2, exported = 0 WHERE [id_payment] = " + this.id_payment.ToString(), db_connection);
				try
				{
					db_command.ExecuteNonQuery();
					this.DialogResult = DialogResult.OK;
				}
				catch(Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);
					MessageBox.Show("Ошибка при сохранении платежа!\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.DialogResult = DialogResult.Cancel;
				}
			}
			else
			{
				db_command = new SqlCommand("INSERT INTO [payments] ([guid], [date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way]) VALUES ('" + System.Guid.NewGuid().ToString() + "', CONVERT(DATETIME, '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() + "', 120), '" + DateTime.Now.ToShortTimeString() + "', " + usr.Id_user.ToString() + ", '" + usr.Name + "', '" + txtOrder.Text + "', " + txtPayment.Text.Replace(',', '.') + ", 3, '" + txtComment.Text + "', 2)", db_connection);
				try
				{
					db_command.ExecuteNonQuery();
					this.DialogResult = DialogResult.OK;
				}
				catch (Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);
					MessageBox.Show("Ошибка при сохранении платежа!\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.DialogResult = DialogResult.Cancel;
				}
			}
		}
	}
}