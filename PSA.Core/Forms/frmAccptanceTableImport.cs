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

namespace Photoland.Forms.Interface
{
	public partial class frmAccptanceTableImport : Form
	{
		//////////////////////////////////////////////////////////////////////////
		private bool CheckState(SqlConnection c)
		{
			bool r = false;
			if (!TestCommand(c))
			{
				frmWaitConnection w = new frmWaitConnection();
				while (!TestCommand(c))
				{
					Application.DoEvents();
					w = new frmWaitConnection();
					w.ShowDialog();
					if (w.DialogResult == DialogResult.Cancel)
					{
						r = false;
						break;
					}
				}
				if (TestCommand(c))
					r = true;
			}
			else
			{
				r = true;
			}
			return r;
		}

		private bool TestCommand(SqlConnection c)
		{
			bool r = false;
			try
			{
				if (c.State != ConnectionState.Open)
					c.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = c;
				cmd.CommandType = CommandType.Text;
				cmd.CommandText = "select getdate()";
				DateTime _r = (DateTime)cmd.ExecuteScalar();
				r = true;
			}
			catch (Exception ex)
			{
				r = false;
			}
			return r;
		}
		//////////////////////////////////////////////////////////////////////////

		public frmAccptanceTableImport(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Журнал импортированных заказов";
			this.db_connection = db_connection;
			this.usr = usr;
		}

		public SqlConnection db_connection;
		public UserInfo usr;

		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private DataTable tblOrders = new DataTable("O");
		private FilterRowLike FilterR;
		private Util.Settings prop = new Util.Settings();


		private void frmAcceptanceTable_Load(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Maximized;

			txtDateBegin.Value = DateTime.Now.AddDays(-7);
			txtDateEnd.Value = DateTime.Now.AddDays(3);
			txtDateBeginPr.Value = DateTime.Now.AddDays(-7);
			txtDateEndPr.Value = DateTime.Now.AddDays(3);

			checkFilterOutput.Checked = true;
			checkFilterInput.Checked = true;

			checkFilter();

			FilterR = new FilterRowLike(GridOder);

			if (prop.UpdateOrderTableInAcceptance > 0)
			{
				tmr.Interval = 1000 * prop.UpdateOrderTableInAcceptance;
				checkAutoUpdate.Checked = true;
				tmr.Start();
			}
			else
			{
				tmr.Interval = 10000;
				checkAutoUpdate.Checked = false;
				tmr.Stop();
			}

			LoadGrid();

			tmrcls.Start();
		}

		private void checkFilter()
		{
			if (checkFilterInput.Checked)
			{
				txtDateBeginPr.Enabled = true;
				txtDateEndPr.Enabled = true;
			}
			else
			{
				txtDateBeginPr.Enabled = false;
				txtDateEndPr.Enabled = false;
			}

			if (checkFilterOutput.Checked)
			{
				txtDateBegin.Enabled = true;
				txtDateEnd.Enabled = true;
			}
			else
			{
				txtDateBegin.Enabled = false;
				txtDateEnd.Enabled = false;
			}

		}


		private void LoadGrid()
		{
			try
			{
                int selid = 0;
                try
                {
                    selid = int.Parse(GridOder.GetData(GridOder.Row, 1).ToString());
                }
                catch
                { }
                if ((prop.UpdateOrderTableInAcceptance > 0) || (checkAutoUpdate.Checked))
					tmr.Stop();

				btnFilterApply.Enabled = false;
				btnUpdate.Enabled = false;

				tblOrders.Clear();
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

				string ybi, mbi, dbi, yei, mei, dei;
				ybi = txtDateBeginPr.Value.Year.ToString();
				if (txtDateBeginPr.Value.Month < 10)
					mbi = "0" + txtDateBeginPr.Value.Month.ToString();
				else
					mbi = txtDateBeginPr.Value.Month.ToString();
				if (txtDateBeginPr.Value.Day < 10)
					dbi = "0" + txtDateBeginPr.Value.Day.ToString();
				else
					dbi = txtDateBeginPr.Value.Day.ToString();
				yei = txtDateEndPr.Value.Year.ToString();
				if (txtDateEndPr.Value.Month < 10)
					mei = "0" + txtDateEndPr.Value.Month.ToString();
				else
					mei = txtDateEndPr.Value.Month.ToString();
				if (txtDateEndPr.Value.Day < 10)
					dei = "0" + txtDateEndPr.Value.Day.ToString();
				else
					dei = txtDateEndPr.Value.Day.ToString();

				string txtFilter = "";
				if (checkFilterOutput.Checked)
				{
					txtFilter += " AND (CONVERT(datetime, [dbo].[vwOrderEventsImport].[event_date], 120)>=CONVERT(DATETIME, '" + yb + "." + mb + "." + db + " 00:00:00.000" + "', 120) AND CONVERT(datetime, [dbo].[vwOrderEventsImport].[event_date], 120)<=CONVERT(DATETIME, '" + ye + "." + me + "." + de + " 23:59:59.999" + "', 120)) ";
				}
				if (checkFilterInput.Checked)
				{
					txtFilter += " AND (CONVERT(datetime, [dbo].[vwOrderEventsExport].[event_date], 120)>=CONVERT(DATETIME, '" + ybi + "." + mbi + "." + dbi + " 00:00:00.000" + "', 120) AND CONVERT(datetime, [dbo].[vwOrderEventsExport].[event_date], 120)<=CONVERT(DATETIME, '" + yei + "." + mei + "." + dei + " 23:59:59.999" + "', 120)) ";
				}

				db_command = new SqlCommand();
				db_command.Connection = db_connection;
				db_command.CommandType = CommandType.StoredProcedure;
				db_command.CommandText = "spOrderExportTabel";
				db_command.Parameters.Add("@di1", SqlDbType.DateTime).Value = txtDateBegin.Value;
				db_command.Parameters.Add("@di2", SqlDbType.DateTime).Value = txtDateEnd.Value;
				db_command.Parameters.Add("@de1", SqlDbType.DateTime).Value = txtDateBegin.Value;
				db_command.Parameters.Add("@de2", SqlDbType.DateTime).Value = txtDateEnd.Value;
				db_command.Parameters.Add("@p", SqlDbType.Char).Value = prop.Order_prefics;
				/*
				 * 1  [id_order], 
				 * 2  [number], 
				 * 3  [name], 
				 * 4  [status], 
				 * 5  [status_desc], 
				 * 6  [input_date], 
				 * 7  [expected_date], 
				 * 8  [ordersum], 
				 * 9  [advanced_payment], 
				 * 10 [final_payment], 
				 * 11 [ordersumdiscont], 
				 * 12 [discont_percent]
				 */
				db_command.CommandTimeout = 9000;
				db_adapter = new SqlDataAdapter(db_command);
				db_adapter.Fill(tblOrders);
				GridOder.DataSource = tblOrders;

				GridOder.Rows.DefaultSize = 21;

				GridOder.Cols[1].Visible = false;
				GridOder.Cols[2].Visible = false;
				GridOder.Cols[3].Visible = false;
				GridOder.Cols[4].Visible = false;
				GridOder.Cols[5].Visible = false;
				GridOder.Cols[6].Visible = false;
				GridOder.Cols[7].Visible = false;
				GridOder.Cols[8].Visible = false;
				//GridOder.Cols[9].Visible = false;

				GridOder.Cols[2].Visible = true;
				GridOder.Cols[2].Width = 110;
				GridOder.Cols[2].AllowDragging = false;
				GridOder.Cols[2].AllowEditing = false;
				GridOder.Cols[2].AllowMerging = false;
				GridOder.Cols[2].AllowResizing = true;
				GridOder.Cols[2].AllowSorting = true;
				GridOder.Cols[2].Caption = "Заказ";

				GridOder.Cols[3].Visible = true;
				GridOder.Cols[3].Width = 350;
				GridOder.Cols[3].AllowDragging = false;
				GridOder.Cols[3].AllowEditing = false;
				GridOder.Cols[3].AllowMerging = false;
				GridOder.Cols[3].AllowResizing = true;
				GridOder.Cols[3].AllowSorting = true;
				GridOder.Cols[3].Caption = "Имя";

				GridOder.Cols[4].Visible = true;
				GridOder.Cols[4].Width = 140;
				GridOder.Cols[4].AllowDragging = false;
				GridOder.Cols[4].AllowEditing = false;
				GridOder.Cols[4].AllowMerging = false;
				GridOder.Cols[4].AllowResizing = true;
				GridOder.Cols[4].AllowSorting = true;
				GridOder.Cols[4].Caption = "Статус";

				GridOder.Cols[5].Visible = true;
				GridOder.Cols[5].Width = 115;
				GridOder.Cols[5].AllowDragging = false;
				GridOder.Cols[5].AllowEditing = false;
				GridOder.Cols[5].AllowMerging = false;
				GridOder.Cols[5].AllowResizing = true;
				GridOder.Cols[5].AllowSorting = true;
				GridOder.Cols[5].Caption = "Принят";
				GridOder.Cols[5].Format = "g";

				GridOder.Cols[6].Visible = true;
				GridOder.Cols[6].Width = 115;
				GridOder.Cols[6].AllowDragging = false;
				GridOder.Cols[6].AllowEditing = false;
				GridOder.Cols[6].AllowMerging = false;
				GridOder.Cols[6].AllowResizing = true;
				GridOder.Cols[6].AllowSorting = true;
				GridOder.Cols[6].Caption = "Выполнен";
				GridOder.Cols[6].Format = "g";

				GridOder.Cols[7].Visible = true;
				GridOder.Cols[7].Width = 115;
				GridOder.Cols[7].AllowDragging = false;
				GridOder.Cols[7].AllowEditing = false;
				GridOder.Cols[7].AllowMerging = false;
				GridOder.Cols[7].AllowResizing = true;
				GridOder.Cols[7].AllowSorting = true;
				GridOder.Cols[7].Caption = "Отправлен";
				GridOder.Cols[7].Format = "g";

				GridOder.Cols[8].Visible = true;
				GridOder.Cols[8].Width = 130;
				GridOder.Cols[8].AllowDragging = false;
				GridOder.Cols[8].AllowEditing = false;
				GridOder.Cols[8].AllowMerging = false;
				GridOder.Cols[8].AllowResizing = true;
				GridOder.Cols[8].AllowSorting = true;
				GridOder.Cols[8].Caption = "Приемщик";

				// Цветовая раскраска
                int selrow = 2;
                for (int i = 2; i < GridOder.Rows.Count; i++)
                {

                    try
                    {
                        if (selid == int.Parse(GridOder.GetData(i, 1).ToString()))
                            selrow = i;
                    }
                    catch (Exception ex)
                    {
                        //ErrorNfo.WriteErrorInfo(ex);
                    }
                }
                if (selrow < GridOder.Rows.Count)
                    GridOder.Select(selrow, 2);


				if ((prop.UpdatePaymentTable > 0) || (checkAutoUpdate.Checked))
					tmr.Start();

			}
			catch(Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
			}
			finally
			{
				btnFilterApply.Enabled = true;
				btnUpdate.Enabled = true;
			}

		}

		private void checkFilterOutput_CheckedChanged(object sender, EventArgs e)
		{
			checkFilter();
		}

		private void checkFilterInput_CheckedChanged(object sender, EventArgs e)
		{
			checkFilter();
		}

		private void btnFilterApply_Click(object sender, EventArgs e)
		{
			LoadGrid();
		}

		private void checkAutoUpdate_CheckedChanged(object sender, EventArgs e)
		{
			if (checkAutoUpdate.Checked)
			{
				if (prop.UpdateOrderTableInAcceptance > 0)
					tmr.Interval = 1000 * prop.UpdateOrderTableInAcceptance;
				else
					tmr.Interval = 10000;
				tmr.Start();
			}
			else
			{
				tmr.Stop();
			}
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			if (checkAutoUpdate.Checked)
				LoadGrid();
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			LoadGrid();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnOpenTable_Click(object sender, EventArgs e)
		{
			if (GridOder.GetData(GridOder.Row, 2) != null)
			{
				OpenOrder(GridOder.GetData(GridOder.Row, 2).ToString());
			}
		}


		private void OpenOrder(string orderno)
		{
			if ((prop.UpdatePaymentTable > 0) || (checkAutoUpdate.Checked))
				tmr.Stop();
			
			SqlCommand tmp_cmd = new SqlCommand("SELECT [id_order] FROM [vwOrderNoList] WHERE RTRIM([number]) LIKE '%' + RTRIM('" + orderno + "') + '%'", db_connection);
			SqlDataReader tmp_rdr = tmp_cmd.ExecuteReader();
			int tmp_id = 0;
			if (tmp_rdr.Read())
			{
				if(!tmp_rdr.IsDBNull(0))
				{
					tmp_id = tmp_rdr.GetInt32(0);
				}
			}
			tmp_rdr.Close();
			if (tmp_id > 0)
			{
				using (frmOrderClose fOrder = new frmOrderClose(db_connection, usr, tmp_id))
				{

					fOrder.ShowDialog();
				}
			}

			if ((prop.UpdatePaymentTable > 0) || (checkAutoUpdate.Checked))
				tmr.Start();
		}

		private void GridOder_DoubleClick(object sender, EventArgs e)
		{
			if (GridOder.GetData(GridOder.Row, 2) != null)
			{
				OpenOrder(GridOder.GetData(GridOder.Row, 2).ToString());
			}
		}

		private void frmAcceptanceTable_KeyPress(object sender, KeyPressEventArgs e)
		{
            if ((!GridOder.IsCellCursor(1,2)) && (!GridOder.IsCellCursor(1,3)))
            {
                if (true)// ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape)) // || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back)
                {
                    if (e.KeyChar == (char)Keys.Return)
                    {
                        // открыть ордер
                        //if (txtOrderNoBarCode.Text.Length == 13)
                        //{
                        //    OpenOrder(txtOrderNoBarCode.Text.Substring(0, 12));
                        //    txtOrderNoBarCode.Text = "";
                        //    e.Handled = true;
                        //}
                        // открыть ордер
                        //else if (txtOrderNoBarCode.Text.Length == 12)
                        //{
                        //    OpenOrder(txtOrderNoBarCode.Text.Substring(0, 12));
                        //    txtOrderNoBarCode.Text = "";
                        //    e.Handled = true;
                        //}
                        //else
                        //{
                        //    GridOder.SetData(1, 1, txtOrderNoBarCode.Text);
                        //    txtOrderNoBarCode.Text = "";
                        //}
						if (txtOrderNoBarCode.Text.Length > 0)
						{
							string code = "";
							if (txtOrderNoBarCode.Text.Split('-').Length == 3)
							{
								if (txtOrderNoBarCode.Text.Split('-')[0] == "k")
								{
									OpenOrder(prop.Order_terminal_prefics.Substring(0, 1) + "0" +
										int.Parse(txtOrderNoBarCode.Text.Split('-')[1]).ToString("D3") +
										int.Parse(txtOrderNoBarCode.Text.Split('-')[2]).ToString("D7"));
									txtOrderNoBarCode.Text = "";
									e.Handled = true;
								}
								else if (txtOrderNoBarCode.Text.Split('-')[0] == "ir")
								{
									OpenOrder(prop.Order_terminal_prefics.Substring(0, 1) + "1" +
										int.Parse(txtOrderNoBarCode.Text.Split('-')[2]).ToString("D10"));
									txtOrderNoBarCode.Text = "";
									e.Handled = true;
								}
								else if (txtOrderNoBarCode.Text.Split('-')[0] == "io")
								{
									OpenOrder(prop.Order_terminal_prefics.Substring(0, 1) + "2" +
										int.Parse(txtOrderNoBarCode.Text.Split('-')[2]).ToString("D10"));
									txtOrderNoBarCode.Text = "";
									e.Handled = true;
								}
								else
								{
									txtOrderNoBarCode.Text = "";
									e.Handled = true;
								}
							}
							else if ((txtOrderNoBarCode.Text.Split('-').Length == 1) && (txtOrderNoBarCode.Text.Length >= 12))
							{
								OpenOrder(txtOrderNoBarCode.Text.Substring(0, 12));
								txtOrderNoBarCode.Text = "";
								e.Handled = true;
							}
							else
							{
								txtOrderNoBarCode.Text = "";
								e.Handled = true;
							}
						}
                    }
                    else if (e.KeyChar == (char)Keys.Escape)
                    {
                        txtOrderNoBarCode.Text = "";
                        e.Handled = true;
                    }
                    else
                    {
                        if (true)// (txtOrderNoBarCode.Text.Length < 13)
                        {
                            txtOrderNoBarCode.Text += e.KeyChar.ToString();
                            e.Handled = true;
                        }
                    }
                }
                else
                {
                    //e.Handled = true;
                }
            }
		}

		private void tmrcls_Tick(object sender, EventArgs e)
		{
			txtOrderNoBarCode.Text = "";
		}

		private void ShowReport(string rep_name, bool export, C1.Win.C1Report.FileFormatEnum format)
		{
			if (CheckState(db_connection))
			{
				try
				{
					if (prop.PathReportsTemplates != "")
					{
						{
							bool ok = false;
							DataTable r = new DataTable("ImportOrder");
							SqlCommand c;
							SqlDataAdapter a;
							switch (rep_name)
							{
								case "ImportOrder":
									{
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

										string ybi, mbi, dbi, yei, mei, dei;
										ybi = txtDateBeginPr.Value.Year.ToString();
										if (txtDateBeginPr.Value.Month < 10)
											mbi = "0" + txtDateBeginPr.Value.Month.ToString();
										else
											mbi = txtDateBeginPr.Value.Month.ToString();
										if (txtDateBeginPr.Value.Day < 10)
											dbi = "0" + txtDateBeginPr.Value.Day.ToString();
										else
											dbi = txtDateBeginPr.Value.Day.ToString();
										yei = txtDateEndPr.Value.Year.ToString();
										if (txtDateEndPr.Value.Month < 10)
											mei = "0" + txtDateEndPr.Value.Month.ToString();
										else
											mei = txtDateEndPr.Value.Month.ToString();
										if (txtDateEndPr.Value.Day < 10)
											dei = "0" + txtDateEndPr.Value.Day.ToString();
										else
											dei = txtDateEndPr.Value.Day.ToString();

										string txtFilter = "";
										if (checkFilterOutput.Checked)
										{
											txtFilter += " AND (CONVERT(datetime, [dbo].[vwOrderEventsImport].[event_date], 120)>=CONVERT(DATETIME, '" + yb + "." + mb + "." + db + " 00:00:00.000" + "', 120) AND CONVERT(datetime, [dbo].[vwOrderEventsImport].[event_date], 120)<=CONVERT(DATETIME, '" + ye + "." + me + "." + de + " 23:59:59.999" + "', 120)) ";
										}
										if (checkFilterInput.Checked)
										{
											txtFilter += " AND (CONVERT(datetime, [dbo].[vwOrderEventsExport].[event_date], 120)>=CONVERT(DATETIME, '" + ybi + "." + mbi + "." + dbi + " 00:00:00.000" + "', 120) AND CONVERT(datetime, [dbo].[vwOrderEventsExport].[event_date], 120)<=CONVERT(DATETIME, '" + yei + "." + mei + "." + dei + " 23:59:59.999" + "', 120)) ";
										}

										string query =
										"SELECT dbo.[order].id_order, dbo.[order].number, dbo.client.name, dbo.order_status.status_desc, dbo.vwOrderEventsImport.event_date AS ImportDate, dbo.vwOrderEventsWorkOk.event_date AS WorkDate, dbo.vwOrderEventsExport.event_date AS ExportDate, dbo.[order].name_accept FROM dbo.[order] INNER JOIN dbo.client ON dbo.[order].id_client = dbo.client.id_client INNER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status LEFT OUTER JOIN dbo.vwOrderEventsWorkOk ON dbo.[order].id_order = dbo.vwOrderEventsWorkOk.id_order LEFT OUTER JOIN dbo.vwOrderEventsExport ON dbo.[order].id_order = dbo.vwOrderEventsExport.id_order LEFT OUTER JOIN dbo.vwOrderEventsImport ON dbo.[order].id_order = dbo.vwOrderEventsImport.id_order WHERE (NOT (dbo.[order].number LIKE N'" + prop.Order_prefics + "%' OR dbo.[order].number LIKE N'40%' OR dbo.[order].number LIKE N'41%' OR dbo.[order].number LIKE N'42%' OR dbo.[order].number LIKE N'49%')) " + txtFilter;
										SqlCommand db_command = new SqlCommand();
										db_command.CommandTimeout = 9000;
										db_command.Connection = db_connection;
										db_command.CommandType = CommandType.StoredProcedure;
										db_command.CommandText = "spOrderExportTabel";
										db_command.Parameters.Add("@di1", SqlDbType.DateTime).Value = txtDateBegin.Value;
										db_command.Parameters.Add("@di2", SqlDbType.DateTime).Value = txtDateEnd.Value;
										db_command.Parameters.Add("@de1", SqlDbType.DateTime).Value = txtDateBegin.Value;
										db_command.Parameters.Add("@de2", SqlDbType.DateTime).Value = txtDateEnd.Value;
										db_command.Parameters.Add("@p", SqlDbType.Char).Value = prop.Order_prefics;
										a = new SqlDataAdapter(db_command);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										ok = true;
										break;
									}
								default:
									{
										ok = false;
										break;
									}
							}

							if (ok)
							{
								if (export)
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
								else
								{
									PrintPreviewDialog pd = new PrintPreviewDialog();
									pd.Document = rep.Document;
									pd.ShowDialog();
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
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			ShowReport("ImportOrder", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

	}
}
