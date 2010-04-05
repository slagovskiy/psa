using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Components.FilterRow;
using Photoland.Lib;
using Photoland.Order;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.Discont;
using Photoland.Security.User;

namespace Photoland.Forms.Admin
{
	public partial class frmPaymentTable : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;

		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private DataTable tblPayments = new DataTable("p");
		private FilterRow FilterR;

		private SqlCommand db_user_cmd;
		private SqlDataAdapter db_user_adapter;
		private DataTable db_user_tbl = new DataTable();
		private bool only = false;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		public frmPaymentTable(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Платежи";
			this.db_connection = db_connection;
			this.usr = usr;
			if (prop.UpdatePaymentTable > 0)
			{
				checkAutoUpdate.Checked = true;
				tmr.Interval = prop.UpdatePaymentTable * 1000;
				tmr.Start();
			}
			else
			{
				tmr.Interval = 10000;
				tmr.Stop();
				checkAutoUpdate.Checked = false;
			}
		}
		
		public frmPaymentTable(SqlConnection db_connection, UserInfo usr, bool only)
		{
			InitializeComponent();
			this.Text = "Платежи";
			this.db_connection = db_connection;
			this.usr = usr;
			this.only = only;
			if (prop.UpdatePaymentTable > 0)
			{
				checkAutoUpdate.Checked = true;
				tmr.Interval = prop.UpdatePaymentTable * 1000;
				tmr.Start();
			}
			else
			{
				tmr.Interval = 10000;
				tmr.Stop();
				checkAutoUpdate.Checked = false;
			}
		}

		private void frmPaymentTable_Load(object sender, EventArgs e)
		{
			if (usr.prmCanEditPayments)
			{
				btnAddPayment.Enabled = true;
				btnDeletePayment.Enabled = true;
				btnEditPayment.Enabled = true;
			}
			else
			{
				btnAddPayment.Enabled = false;
				btnDeletePayment.Enabled = false;
				btnEditPayment.Enabled = false;
			}

			ResetFilter();
			FilterR = new FilterRow(GridPayment);

			LoadGrid();
			if (this.only)
			{
				txtUser.Enabled = false;
			}
			else
			{
				txtUser.Enabled = true;
			}
		}

		private void ResetFilter()
		{
			db_user_cmd = new SqlCommand("SELECT [id_user], [name] + ' (' + [pointname] + ')' as [name] FROM [vwUserFull]", db_connection);
			db_user_adapter = new SqlDataAdapter(db_user_cmd);
			db_user_adapter.Fill(db_user_tbl);

			DataRow b;
			b = db_user_tbl.NewRow();
			b["id_user"] = -1;
			b["name"] = "< Все >";
			db_user_tbl.Rows.InsertAt(b, 0);

			txtUser.DataSource = db_user_tbl;
			txtUser.DisplayMember = "name";
			txtUser.ValueMember = "id_user";

			if (this.only)
				txtUser.SelectedValue = usr.Id_user;
			else
				txtUser.SelectedIndex = 0;

			txtDateBegin.Value = DateTime.Now.AddDays(-1);
			txtDateEnd.Value = DateTime.Now;
			
		}

		private void LoadGrid()
		{
			try
			{
				if ((prop.UpdatePaymentTable > 0) || (checkAutoUpdate.Checked))
					tmr.Stop();

				btnUpdate.Enabled = false;
				btnFilterApply.Enabled = false;
				tblPayments.Clear();
				string usr_filter = "";
				if ((int)txtUser.SelectedValue != -1)
					usr_filter = " AND [id_user] = " + txtUser.SelectedValue.ToString();
				string yb, mb, db, ye, me, de;
				yb = txtDateBegin.Value.Year.ToString();
				if (txtDateBegin.Value.Month < 10)
					mb = "0" + txtDateBegin.Value.Month.ToString();
				else
					mb = txtDateBegin.Value.Month.ToString();
				if (txtDateBegin.Value.Day < 10)
					db = "0" + txtDateBegin.Value.Day.ToString();
				else
					db = txtDateBegin.Value.Day.ToString();
				ye = txtDateEnd.Value.Year.ToString();
				if (txtDateEnd.Value.Month < 10)
					me = "0" + txtDateEnd.Value.Month.ToString();
				else
					me = txtDateEnd.Value.Month.ToString();
				if (txtDateEnd.Value.Day < 10)
					de = "0" + txtDateEnd.Value.Day.ToString();
				else
					de = txtDateEnd.Value.Day.ToString();
				db_command = new SqlCommand("SELECT [id_payment], [guid], CONVERT(DATETIME, CAST([date] AS NCHAR(11)), 111) as [dt], [time] as [tm], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way], [name] FROM [vwPaymentFull] WHERE CONVERT(datetime, [date], 120)>=CONVERT(DATETIME, '" + yb + "." + mb + "." + db + " 00:00:00.000" + "', 120) AND CONVERT(datetime, [date], 120)<=CONVERT(DATETIME, '" + ye + "." + me + "." + de + " 23:59:59.999" + "', 120) " + usr_filter + " ORDER BY [date]", db_connection);
				db_adapter = new SqlDataAdapter(db_command);
				db_adapter.Fill(tblPayments);
				GridPayment.DataSource = tblPayments;

				GridPayment.Rows.DefaultSize = 21;

				GridPayment.Cols[1].Visible = false;
				GridPayment.Cols[2].Visible = false;
				GridPayment.Cols[5].Visible = false;
				GridPayment.Cols[9].Visible = false;
				GridPayment.Cols[11].Visible = false;

				GridPayment.Cols[3].Width = 85;
				GridPayment.Cols[3].AllowDragging = false;
				GridPayment.Cols[3].AllowEditing = false;
				GridPayment.Cols[3].AllowMerging = false;
				GridPayment.Cols[3].AllowResizing = true;
				GridPayment.Cols[3].AllowSorting = true;
				GridPayment.Cols[3].Caption = "Дата";


				GridPayment.Cols[4].Width = 85;
				GridPayment.Cols[4].AllowDragging = false;
				GridPayment.Cols[4].AllowEditing = false;
				GridPayment.Cols[4].AllowMerging = false;
				GridPayment.Cols[4].AllowResizing = true;
				GridPayment.Cols[4].AllowSorting = true;
				GridPayment.Cols[4].Caption = "Время";


				GridPayment.Cols[6].Width = 150;
				GridPayment.Cols[6].AllowDragging = false;
				GridPayment.Cols[6].AllowEditing = false;
				GridPayment.Cols[6].AllowMerging = false;
				GridPayment.Cols[6].AllowResizing = true;
				GridPayment.Cols[6].AllowSorting = true;
				GridPayment.Cols[6].Caption = "Пользователь";


				GridPayment.Cols[7].Width = 120;
				GridPayment.Cols[7].AllowDragging = false;
				GridPayment.Cols[7].AllowEditing = false;
				GridPayment.Cols[7].AllowMerging = false;
				GridPayment.Cols[7].AllowResizing = true;
				GridPayment.Cols[7].AllowSorting = true;
				GridPayment.Cols[7].Caption = "Заказ №";


				GridPayment.Cols[8].Width = 100;
				GridPayment.Cols[8].AllowDragging = false;
				GridPayment.Cols[8].AllowEditing = false;
				GridPayment.Cols[8].AllowMerging = false;
				GridPayment.Cols[8].AllowResizing = true;
				GridPayment.Cols[8].AllowSorting = true;
				GridPayment.Cols[8].Caption = "Платеж";


				GridPayment.Cols[10].Width = 380;
				GridPayment.Cols[10].AllowDragging = false;
				GridPayment.Cols[10].AllowEditing = false;
				GridPayment.Cols[10].AllowMerging = false;
				GridPayment.Cols[10].AllowResizing = true;
				GridPayment.Cols[10].AllowSorting = true;
				GridPayment.Cols[10].Caption = "Комментарии";

				GridPayment.Cols[12].Width = 380;
				GridPayment.Cols[12].AllowDragging = false;
				GridPayment.Cols[12].AllowEditing = false;
				GridPayment.Cols[12].AllowMerging = false;
				GridPayment.Cols[12].AllowResizing = true;
				GridPayment.Cols[12].AllowSorting = true;
				GridPayment.Cols[12].Caption = "Категория клиента";

				decimal tmp_sum = 0;
				for (int i = 2; i < GridPayment.Rows.Count; i++)
				{
					tmp_sum += decimal.Parse(GridPayment.Rows[i][8].ToString());
				}
				lblSum.Text = decimal.Round(tmp_sum, 2).ToString() + " p.";
				if ((prop.UpdatePaymentTable > 0) || (checkAutoUpdate.Checked))
					tmr.Start();
			}
			catch(Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
			}
			finally
			{
				btnUpdate.Enabled = true;
				btnFilterApply.Enabled = true;
			}
		}


		private void btnUpdate_Click(object sender, EventArgs e)
		{
			LoadGrid();
		}

		private void btnFilterApply_Click(object sender, EventArgs e)
		{
			LoadGrid();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnFilterReset_Click(object sender, EventArgs e)
		{
			ResetFilter();
			LoadGrid();
		}

		private void btnEditPayment_Click(object sender, EventArgs e)
		{
			try
			{
				if (int.Parse(GridPayment.Rows[GridPayment.Row][1].ToString()) > 0)
				{
					if (int.Parse(GridPayment.Rows[GridPayment.Row][11].ToString()) != 1)
					{
						frmEditPayment fEdit =
							new frmEditPayment(db_connection, usr, int.Parse(GridPayment.Rows[GridPayment.Row][1].ToString()));
						fEdit.ShowDialog();
						if (fEdit.DialogResult == DialogResult.OK)
							LoadGrid();
						fEdit.Close();
					}
					else
					{
						MessageBox.Show("Этот платеж внесен автоматической системой и не может быть изменен!", "Ошибка",
						                MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			catch(Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
			}
		}

		private void btnAddPayment_Click(object sender, EventArgs e)
		{
			try
			{
				frmEditPayment fEdit = new frmEditPayment(db_connection, usr);
				fEdit.ShowDialog();
				if (fEdit.DialogResult == DialogResult.OK)
					LoadGrid();
				fEdit.Close();
			}
			catch(Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
			}
		}

		private void btnDeletePayment_Click(object sender, EventArgs e)
		{
			try
			{
				if (int.Parse(GridPayment.Rows[GridPayment.Row][1].ToString()) > 0)
				{
					if (int.Parse(GridPayment.Rows[GridPayment.Row][11].ToString()) != 1)
					{
						if (
							MessageBox.Show("Вы действительно хотите удалить эту запись?", "Удаление", MessageBoxButtons.OKCancel,
							                MessageBoxIcon.Question) == DialogResult.OK)
						{
							if (
								MessageBox.Show("Хотошо все обдумав Вы настаиваете на удалении этой записи?", "Удаление",
								                MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								SqlCommand db_c =
									new SqlCommand(
										"UPDATE [payments] SET [del] = 1, exported = 0 WHERE [id_payment] = " + GridPayment.Rows[GridPayment.Row][1].ToString(),
										db_connection);
								try
								{
									db_c.ExecuteNonQuery();
								}
								catch (Exception ex)
								{
									ErrorNfo.WriteErrorInfo(ex);
									MessageBox.Show("Ошибка при удалении платежа!\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace,
									                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
								}
							}
						}
					}
					else
					{
						MessageBox.Show("Этот платеж внесен автоматической системой и не может быть удален!", "Ошибка",
						                MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Ошибка при удалении платежа!\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			ShowReport("Payments group", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void ShowReport(string rep_name, bool export, C1.Win.C1Report.FileFormatEnum format)
		{
			try
			{
				if (prop.PathReportsTemplates != "")
				{
					rep.Load(prop.PathReportsTemplates, rep_name);
					rep.DataSource.Recordset = tblPayments;
					if (!export)
					{
						PrintPreviewDialog pd = new PrintPreviewDialog();
						pd.Document = rep.Document;
						pd.ShowDialog();
					}
					else
					{
						switch (format)
						{
							case C1.Win.C1Report.FileFormatEnum.PDF:
								{
									sdlg.Filter = "Adobe PDF (*.pdf)|*.pdf";
									sdlg.ShowDialog();
									if (sdlg.FileName != "")
									{
										rep.RenderToFile(sdlg.FileName, C1.Win.C1Report.FileFormatEnum.PDF);
									}
									break;
								}
							case C1.Win.C1Report.FileFormatEnum.Excel:
								{
									sdlg.Filter = "Microsoft Excel (*.xls)|*.xls";
									sdlg.ShowDialog();
									if (sdlg.FileName != "")
									{
										rep.RenderToFile(sdlg.FileName, C1.Win.C1Report.FileFormatEnum.Excel);
									}
									break;
								}
						}
					}
				}
				else
				{
					MessageBox.Show("Не выбран файл шаблонов отчетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Ошибка вывода отчета\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnExportPDF_Click(object sender, EventArgs e)
		{
			ShowReport("Payments group", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void btnExportExcel_Click(object sender, EventArgs e)
		{
			ShowReport("Payments group", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			LoadGrid();
		}

		private void checkAutoUpdate_CheckedChanged(object sender, EventArgs e)
		{
			if (checkAutoUpdate.Checked)
			{
				if (prop.UpdatePaymentTable == 0)
					tmr.Interval = 10000;
				else
					tmr.Interval = prop.UpdatePaymentTable * 1000;

				tmr.Start();
			}
			else
			{
				tmr.Stop();
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			mnuReportSelect.Show(button1, new Point(0, button1.Width));
		}

		private void mnubtmNoGroup_Click(object sender, EventArgs e)
		{
			ShowReport("Payments nogroup", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void mnubtnGroup_Click(object sender, EventArgs e)
		{
			ShowReport("Payments group", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void mnuExportPDFNoGroup_Click(object sender, EventArgs e)
		{
			ShowReport("Payments nogroup", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void mnuExportPDFGroup_Click(object sender, EventArgs e)
		{
			ShowReport("Payments group", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void mnuExportExcelNoGroup_Click(object sender, EventArgs e)
		{
			ShowReport("Payments nogroup", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void mnuExportExcelGroup_Click(object sender, EventArgs e)
		{
			ShowReport("Payments group", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void button2_Click(object sender, EventArgs e)
		{
			mnuExportPDF.Show(button2, new Point(0, button2.Width));
		}

		private void button3_Click(object sender, EventArgs e)
		{
			mnuExportExcel.Show(button3, new Point(0, button3.Width));
		}

	}
}