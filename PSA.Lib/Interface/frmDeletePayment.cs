using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using PSA.Lib.Util;
using Photoland.Security.User;
using System.IO;

namespace PSA.Lib.Interface
{
	public partial class frmDeletePayment : PSA.Lib.Interface.Templates.frmTDialog
	{
		private UserInfo _usr;
		public UserInfo usr
		{
			get { return _usr; }
			set { _usr = value; }
		}

		private Settings prop = new Settings();

		public frmDeletePayment()
		{
			InitializeComponent();
			this.Title = "Удаление старых платежей";
		}

		private void frmDeletePayment_Load(object sender, EventArgs e)
		{
			txtDate.Value = DateTime.Now.AddDays(-93);
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnDelete_Click(object sender, EventArgs e)
		{
			using (SqlConnection db_connection = new SqlConnection(prop.Connection_string))
			{
				DataTable tbl = new DataTable("payment");

				db_connection.Open();
				using (SqlCommand cmd = new SqlCommand())
				{
					cmd.CommandTimeout = 9000;
					cmd.Connection = db_connection;
					cmd.CommandText = "SELECT [id_payment], [date], [time], [payment] FROM [payments] WHERE ([date] < CONVERT(DATETIME, '" + txtDate.Value.Year.ToString("D4") + "-" + txtDate.Value.Month.ToString("D2") + "-" + txtDate.Value.Day.ToString("D2") + " 00:00:00', 102))";
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(tbl);
				}
				if (tbl.Rows.Count > 0)
				{
					if (MessageBox.Show("До указанной даты найдено " + tbl.Rows.Count + " платежей.\nУдалить?", "Удаление платежей", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
					{
						pb.Minimum = 0;
						pb.Maximum = tbl.Rows.Count;
						pb.Value = 0;
						StreamWriter f = new StreamWriter(prop.Dir_export + "\\clear_payment_" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + "-" + DateTime.Now.Hour.ToString("D2") + "-" + DateTime.Now.Minute.ToString("D2") + "-" + DateTime.Now.Second.ToString("D2") + ".info", true, Encoding.GetEncoding(1251));
						foreach (DataRow r in tbl.Rows)
						{
							using (SqlCommand cmd = new SqlCommand("DELETE FROM [payments] WHERE [id_payment] = " + r["id_payment"].ToString(), db_connection))
							{
								try
								{
									cmd.ExecuteNonQuery();
									lblInfo.Text = "Платеж от " + r["date"].ToString().Trim() + " на сумму " + r["payment"].ToString() + " удален";
									f.WriteLine("+ Удален платеж от " + r["date"].ToString().Trim() + " на сумму " + r["payment"].ToString());
								}
								catch 
								{
									lblInfo.Text = "Ошибка при удалении платежа от " + r["date"].ToString().Trim() + " на сумму " + r["payment"].ToString() + "!";
								}
							}
							pb.Value++;
							Application.DoEvents();
						}
						f.Close();
						lblInfo.Text = "";
						pb.Value = 0;
					}
				}
				else
				{
					MessageBox.Show("Нет платежей для удаления");
				}
			}
		}
	}
}
