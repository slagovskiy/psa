using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.Util;
using Photoland.Security.User;
using System.Data.SqlClient;

namespace PSA.Lib.Interface
{
	public partial class frmMovePayment : PSA.Lib.Interface.Templates.frmTDialog
	{
		private Settings prop = new Settings();
		public UserInfo usr;
		public string number = "";

		public frmMovePayment(UserInfo user, string OrderNumber)
		{
			InitializeComponent();
			usr = user;
			number = OrderNumber;
			this.Title = "Перенос платежа";
		}

		private void frmMovePayment_Load(object sender, EventArgs e)
		{
			try
			{
				using (SqlConnection cn = new SqlConnection(prop.Connection_string))
				{
					cn.Open();
					SqlCommand cmd = new SqlCommand();
					cmd.CommandTimeout = 9000;
					cmd.Connection = cn;
					cmd.CommandText = "SELECT [date], [payment], [name_user], [comment] FROM [payments] WHERE [number] like '" + number.Trim() + "%'";
					DataTable tbl = new DataTable();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(tbl);
					data.DataSource = tbl;

					

					data.Cols[1].Caption = "Дата";
					data.Cols[2].Caption = "Платеж";
					data.Cols[3].Caption = "Пользователь";
					data.Cols[4].Caption = "Комментарий";

					data.Cols[1].AllowEditing = false;
					data.Cols[2].AllowEditing = false;
					data.Cols[3].AllowEditing = false;
					data.Cols[4].AllowEditing = false;

					

					data.Cols[4].Format = "dd/MM/yyyy HH:mm:ss";

				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка получения информации о платежах.\n" + ex.Message + "\n" + ex.Source, 
					"Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void data_DoubleClick(object sender, EventArgs e)
		{
			try
			{
				if (data.Row > 0)
				{
					dateFrom.Value = DateTime.Parse(data.Rows[data.Row][1].ToString());
					dateTo.Value = DateTime.Parse(data.Rows[data.Row][1].ToString());
					textSum.Text = data.Rows[data.Row][2].ToString();
				}
			}
			catch
			{
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			try
			{
				using (SqlConnection cn = new SqlConnection(prop.Connection_string))
				{
					cn.Open();
					SqlCommand cmd = new SqlCommand();
					cmd.CommandTimeout = 9000;
					cmd.Connection = cn;
					string query = "";
					query += "BEGIN TRANSACTION;\n";
					query += "INSERT INTO [dbo].[payments] ([date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way], [exported]) VALUES (CONVERT(DATETIME, '" + dateTo.Value.Year.ToString("D4") + "." + dateTo.Value.Month.ToString("D2") + "." + dateTo.Value.Day.ToString("D2") + " " + dateTo.Value.ToLongTimeString() + "'), '00:00', " + usr.Id_user + ", '" + usr.Name.Trim() + "', '" + number + "', " + decimal.Parse(textSum.Text).ToString().Replace(",", ".") + ", 2, 'Перенос платежа', 1, 0);\n";
					query += "INSERT INTO [dbo].[payments] ([date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way], [exported]) VALUES (CONVERT(DATETIME, '" + dateFrom.Value.Year.ToString("D4") + "." + dateFrom.Value.Month.ToString("D2") + "." + dateFrom.Value.Day.ToString("D2") + " " + dateFrom.Value.ToLongTimeString() + "'), '00:00', " + usr.Id_user + ", '" + usr.Name.Trim() + "', '" + number + "', " + (decimal.Parse(textSum.Text)*(-1)).ToString().Replace(",", ".") + ", 2, 'Перенос платежа', 1, 0);\n";
					query += "COMMIT;\n";
					cmd.CommandText = query;
					cmd.ExecuteNonQuery();	
				}
				this.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка сохранения.\n" + ex.Message + "\n" + ex.Source);
			}
		}
	}
}
