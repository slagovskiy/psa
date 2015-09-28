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
	public partial class frmAcceptanceTable : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;

		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private DataTable tblOrders = new DataTable("O");
		private FilterRowLike FilterR;
		private Util.Settings prop = new Util.Settings();


		public frmAcceptanceTable(SqlConnection db_connection, UserInfo usr)
		{
			InitializeComponent();
			this.Text = "Основной журнал заказов";
			this.db_connection = db_connection;
			this.usr = usr;
		}

		private void frmAcceptanceTable_Load(object sender, EventArgs e)
		{
			this.WindowState = FormWindowState.Maximized;

			txtDateBegin.Value = DateTime.Now.AddDays(-7);
			txtDateEnd.Value = DateTime.Now.AddDays(3);
			txtDateBeginPr.Value = DateTime.Now.AddDays(-7);
			txtDateEndPr.Value = DateTime.Now.AddDays(3);

			SqlCommand cmd_status = new SqlCommand("SELECT [order_status], [status_desc] FROM [order_status] ORDER BY [status_desc]", db_connection);
			SqlDataAdapter da_status = new SqlDataAdapter(cmd_status);
			DataTable dt_status = new DataTable("status");
			da_status.Fill(dt_status);
			checkStatus.DataSource = dt_status;
			checkStatus.DisplayMember = "status_desc";
			checkStatus.ValueMember = "order_status";
			for (int i = 0; i < checkStatus.Items.Count; i++)
			{
				checkStatus.SetItemChecked(i, true);
			}

			checkFilterOutput.Checked = true;

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
					txtFilter += " AND (CONVERT(datetime, [expected_date], 120)>=CONVERT(DATETIME, '" + yb + "." + mb + "." + db + " 00:00:00.000" + "', 120) AND CONVERT(datetime, [expected_date], 120)<=CONVERT(DATETIME, '" + ye + "." + me + "." + de + " 23:59:59.999" + "', 120)) ";
				}
				if (checkFilterInput.Checked)
				{
					txtFilter += " AND (CONVERT(datetime, [input_date], 120)>=CONVERT(DATETIME, '" + ybi + "." + mbi + "." + dbi + " 00:00:00.000" + "', 120) AND CONVERT(datetime, [input_date], 120)<=CONVERT(DATETIME, '" + yei + "." + mei + "." + dei + " 23:59:59.999" + "', 120)) ";
				}

				string txtStatus = "''";
				for (int j = 0; j < checkStatus.CheckedItems.Count; j++)
				{
					txtStatus += ", '" + ((System.Data.DataRowView)(checkStatus.CheckedItems[j])).Row.ItemArray[0].ToString().Trim() + "'";
				}
				if (checkDouble.Checked)
				{
					db_command = new SqlCommand("SELECT [id_order], [number], [name], [status], [status_desc], [input_date], [expected_date], [ordersum], [advanced_payment], [final_payment], [ordersumdiscont], [discont_percent] FROM [vwOrderTableFull] WHERE [id_order]  IN (SELECT id_order FROM dbo.[order] WHERE (number IN (SELECT number FROM dbo.[order] AS order_1 WHERE number not like '49%' AND number not like '59%' GROUP BY number HAVING (COUNT(*) > 1)))) ORDER BY [expected_date], [number]", db_connection);
				}
				else
				{
					db_command = new SqlCommand("SELECT [id_order], [number], [name], [status], [status_desc], [input_date], [expected_date], [ordersum], [advanced_payment], [final_payment], [ordersumdiscont], [discont_percent] FROM [vwOrderTableFull] WHERE [id_order] > 0 AND [status] IN (" + txtStatus + ") " + txtFilter + " ORDER BY [expected_date], [number]", db_connection);
				}
				
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
				GridOder.Cols[9].Visible = false;
				GridOder.Cols[10].Visible = false;
				GridOder.Cols[11].Visible = false;
				GridOder.Cols[12].Visible = false;

				GridOder.Cols[2].Visible = true;
				GridOder.Cols[2].Width = 110;
				GridOder.Cols[2].AllowDragging = false;
				GridOder.Cols[2].AllowEditing = false;
				GridOder.Cols[2].AllowMerging = false;
				GridOder.Cols[2].AllowResizing = true;
				GridOder.Cols[2].AllowSorting = true;
				GridOder.Cols[2].Caption = "Заказ";

				GridOder.Cols[3].Visible = true;
				GridOder.Cols[3].Width = 430;
				GridOder.Cols[3].AllowDragging = false;
				GridOder.Cols[3].AllowEditing = false;
				GridOder.Cols[3].AllowMerging = false;
				GridOder.Cols[3].AllowResizing = true;
				GridOder.Cols[3].AllowSorting = true;
				GridOder.Cols[3].Caption = "Имя";

				GridOder.Cols[5].Visible = true;
				GridOder.Cols[5].Width = 160;
				GridOder.Cols[5].AllowDragging = false;
				GridOder.Cols[5].AllowEditing = false;
				GridOder.Cols[5].AllowMerging = false;
				GridOder.Cols[5].AllowResizing = true;
				GridOder.Cols[5].AllowSorting = true;
				GridOder.Cols[5].Caption = "Статус";

				GridOder.Cols[6].Visible = true;
				GridOder.Cols[6].Width = 110;
				GridOder.Cols[6].AllowDragging = false;
				GridOder.Cols[6].AllowEditing = false;
				GridOder.Cols[6].AllowMerging = false;
				GridOder.Cols[6].AllowResizing = true;
				GridOder.Cols[6].AllowSorting = true;
				GridOder.Cols[6].Caption = "Поступление";
				GridOder.Cols[6].Format = "g";

				GridOder.Cols[7].Visible = true;
				GridOder.Cols[7].Width = 110;
				GridOder.Cols[7].AllowDragging = false;
				GridOder.Cols[7].AllowEditing = false;
				GridOder.Cols[7].AllowMerging = false;
				GridOder.Cols[7].AllowResizing = true;
				GridOder.Cols[7].AllowSorting = true;
				GridOder.Cols[7].Caption = "Выдача";
				GridOder.Cols[7].Format = "g";

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

                if (prop.Color_rows_in_order)
				{
					for (int i = 2; i < GridOder.Rows.Count; i++)
					{

						if (((GridOder.Rows[i][4].ToString().Trim() == "000000") || (GridOder.Rows[i][4].ToString().Trim() == "000010") || (GridOder.Rows[i][4].ToString().Trim() == "000001") || (GridOder.Rows[i][4].ToString().Trim() == "000200") || (GridOder.Rows[i][4].ToString().Trim() == "000100")) && (DateTime.Parse(GridOder.Rows[i][7].ToString()) < DateTime.Now))
						{
							GridOder.Rows[i].Style = GridOder.Styles["MyRed"];
						}

						if (((GridOder.Rows[i][4].ToString().Trim() == "000000") || (GridOder.Rows[i][4].ToString().Trim() == "000010") || (GridOder.Rows[i][4].ToString().Trim() == "000001") || (GridOder.Rows[i][4].ToString().Trim() == "000200") || (GridOder.Rows[i][4].ToString().Trim() == "000100")) && (DateTime.Parse(GridOder.Rows[i][7].ToString()) >= DateTime.Now))
						{
							GridOder.Rows[i].Style = GridOder.Styles["MyYellow"];
						}

						if ((GridOder.Rows[i][4].ToString().Trim() == "000210") || (GridOder.Rows[i][4].ToString().Trim() == "000211") || (GridOder.Rows[i][4].ToString().Trim() == "000212"))
						{
							GridOder.Rows[i].Style = GridOder.Styles["MyBlue"];
						}

						if ((GridOder.Rows[i][4].ToString().Trim() == "100000") || (GridOder.Rows[i][4].ToString().Trim() == "200000") || (GridOder.Rows[i][4].ToString().Trim() == "300000") || (GridOder.Rows[i][4].ToString().Trim() == "010000"))
						{
							GridOder.Rows[i].Style = GridOder.Styles["MyGreen"];
						}

						if ((GridOder.Rows[i][4].ToString().Trim() == "000110") || (GridOder.Rows[i][4].ToString().Trim() == "000111"))
						{
							GridOder.Rows[i].Style = GridOder.Styles["MyOrange"];
						}

					}
				}

				if ((prop.UpdatePaymentTable > 0) || (checkAutoUpdate.Checked))
					tmr.Start();

			}
			catch(Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
			}
			finally
			{
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
			if (GridOder.GetData(GridOder.Row, 1) != null)
			{
				OpenOrder(GridOder.GetData(GridOder.Row, 1).ToString());
			}
		}


		public void OpenOrder(string orderno)
		{
			if ((prop.UpdatePaymentTable > 0) || (checkAutoUpdate.Checked))
				tmr.Stop();

			SqlCommand tmp_cmd = new SqlCommand("SELECT [id_order] FROM [vwOrderNoList] WHERE [id_order] = " + orderno + "", db_connection);
			SqlDataReader tmp_rdr = tmp_cmd.ExecuteReader();
			int tmp_id = 0;
			if (tmp_rdr.Read())
			{
				if (!tmp_rdr.IsDBNull(0))
				{
					tmp_id = tmp_rdr.GetInt32(0);
				}
			}
			tmp_rdr.Close();
			if (tmp_id > 0)
			{
				using (frmOrderClose fOrder = new frmOrderClose(db_connection, usr, tmp_id))
				{
					fOrder.fixDouble = checkDouble.Checked;
					fOrder.ShowDialog();
				}
			}

			if ((prop.UpdatePaymentTable > 0) || (checkAutoUpdate.Checked))
				tmr.Start();
		}

		private void OpenOrder2(string orderno)
		{
			if ((prop.UpdatePaymentTable > 0) || (checkAutoUpdate.Checked))
				tmr.Stop();

			SqlCommand tmp_cmd = new SqlCommand("SELECT [id_order] FROM [vwOrderNoList] WHERE RTRIM([number]) LIKE '%' + RTRIM('" + orderno + "') + '%'", db_connection);
			SqlDataReader tmp_rdr = tmp_cmd.ExecuteReader();
			int tmp_id = 0;
			if (tmp_rdr.Read())
			{
				if (!tmp_rdr.IsDBNull(0))
				{
					tmp_id = tmp_rdr.GetInt32(0);
				}
			}
			tmp_rdr.Close();
			if (tmp_id > 0)
			{
				using (frmOrderClose fOrder = new frmOrderClose(db_connection, usr, tmp_id))
				{
					fOrder.fixDouble = checkDouble.Checked;
					fOrder.ShowDialog();
				}
			}

			if ((prop.UpdatePaymentTable > 0) || (checkAutoUpdate.Checked))
				tmr.Start();
		}

		private void GridOder_DoubleClick(object sender, EventArgs e)
		{
			if (GridOder.GetData(GridOder.Row, 1) != null)
			{
				OpenOrder(GridOder.GetData(GridOder.Row, 1).ToString());
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
									OpenOrder2(prop.Order_terminal_prefics.Substring(0, 1) + "0" +
										int.Parse(txtOrderNoBarCode.Text.Split('-')[1]).ToString("D3") +
										int.Parse(txtOrderNoBarCode.Text.Split('-')[2]).ToString("D7"));
									txtOrderNoBarCode.Text = "";
									e.Handled = true;
								}
								else if (txtOrderNoBarCode.Text.Split('-')[0] == "ir")
								{
									OpenOrder2(prop.Order_terminal_prefics.Substring(0, 1) + "1" +
										int.Parse(txtOrderNoBarCode.Text.Split('-')[2]).ToString("D10"));
									txtOrderNoBarCode.Text = "";
									e.Handled = true;
								}
								else if (txtOrderNoBarCode.Text.Split('-')[0] == "io")
								{
									OpenOrder2(prop.Order_terminal_prefics.Substring(0, 1) + "2" +
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
								OpenOrder2(txtOrderNoBarCode.Text.Substring(0, 12));
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

		private void checkDouble_CheckedChanged(object sender, EventArgs e)
		{
			if (checkDouble.Checked)
			{
				checkFilterOutput.Checked = false;
				checkFilterOutput.Enabled = false;
				checkFilterInput.Checked = false;
				checkFilterInput.Enabled = false;
				checkStatus.Enabled = false;
			}
			else
			{
				checkFilterOutput.Checked = true;
				checkFilterOutput.Enabled = true;
				checkFilterInput.Checked = false;
				checkFilterInput.Enabled = true;
				checkStatus.Enabled = true;
			}
		}

	
	}
}