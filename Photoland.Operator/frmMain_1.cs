using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.Discont;
using Photoland.Security.User;
using Photoland.Forms.Admin;
using Photoland.Forms.Interface;
using Photoland.Lib;
using Photoland.Order;
using Photoland.Components.FilterRow;
using System.IO;
using C1.Win.C1FlexGrid;
using System.Globalization;
using PSA.Lib.Interface;
using PSA.Lib.Util;

namespace Photoland.Operator
{
	partial class frmMain : Form
	{
		private void FillTehAction()
		{
			if (CheckState(db_connection))
			{
				DateTime tm = new DateTime();
				tm = DateTime.Now.AddDays(-10);
				string y = tm.Year.ToString();
				string m = tm.Month < 10
							   ? "0" + tm.Month.ToString()
							   : tm.Month.ToString();
				string d = tm.Day < 10
							   ? "0" + tm.Day.ToString()
							   : tm.Day.ToString();

				db_table_tehaction.Rows.Clear();
				db_command_tehaction =
					new SqlCommand(
						"SELECT [id_orderbody], [datework], [mashine], [paper], [good], [defect_quantity], [tech_defect], [number] FROM [vwDefectFull] WHERE [tech_defect_code] > 0 AND CONVERT(datetime, [datework]) >= CONVERT(datetime, '" + y + "/" + m + "/" + d + " 00:00:00.000') AND [id_user_work] = " + usr.Id_user.ToString() + " AND [id_user_add] = " + usr.Id_user.ToString(),
						db_connection);
				db_command_tehaction.CommandTimeout = 9000;
				db_adapter_tehaction = new SqlDataAdapter(db_command_tehaction);
				db_adapter_tehaction.Fill(db_table_tehaction);

				gridTehAction.DataSource = db_table_tehaction;

				/*
				 *   1 [id_orderbody], 
				 *   2 [datework], 
				 *   3 [mashine], 
				 *   4 [paper], 
				 *   5 [good], 
				 *   6 [defect_quantity]
				 *   7 tech_defect
				 *   8 number
				 */

				gridTehAction.Cols[1].Visible = false;
				gridTehAction.Cols[2].Visible = false;
				gridTehAction.Cols[3].Visible = false;
				gridTehAction.Cols[4].Visible = false;
				gridTehAction.Cols[5].Visible = false;
				gridTehAction.Cols[6].Visible = false;
				gridTehAction.Cols[7].Visible = false;
				gridTehAction.Cols[8].Visible = false;


				gridTehAction.Cols[2].Visible = true;
				gridTehAction.Cols[2].Width = 105;
				gridTehAction.Cols[2].AllowDragging = false;
				gridTehAction.Cols[2].AllowEditing = false;
				gridTehAction.Cols[2].AllowMerging = false;
				gridTehAction.Cols[2].AllowResizing = true;
				gridTehAction.Cols[2].AllowSorting = true;
				gridTehAction.Cols[2].Caption = "Дата";
				gridTehAction.Cols[2].Format = "g";

				gridTehAction.Cols[3].Visible = true;
				gridTehAction.Cols[3].Width = 175;
				gridTehAction.Cols[3].AllowDragging = false;
				gridTehAction.Cols[3].AllowEditing = false;
				gridTehAction.Cols[3].AllowMerging = false;
				gridTehAction.Cols[3].AllowResizing = true;
				gridTehAction.Cols[3].AllowSorting = true;
				gridTehAction.Cols[3].Caption = "Машина";

				gridTehAction.Cols[4].Visible = true;
				gridTehAction.Cols[4].Width = 175;
				gridTehAction.Cols[4].AllowDragging = false;
				gridTehAction.Cols[4].AllowEditing = false;
				gridTehAction.Cols[4].AllowMerging = false;
				gridTehAction.Cols[4].AllowResizing = true;
				gridTehAction.Cols[4].AllowSorting = true;
				gridTehAction.Cols[4].Caption = "Бумага";

				gridTehAction.Cols[5].Visible = true;
				gridTehAction.Cols[5].Width = 175;
				gridTehAction.Cols[5].AllowDragging = false;
				gridTehAction.Cols[5].AllowEditing = false;
				gridTehAction.Cols[5].AllowMerging = false;
				gridTehAction.Cols[5].AllowResizing = true;
				gridTehAction.Cols[5].AllowSorting = true;
				gridTehAction.Cols[5].Caption = "Списание";

				gridTehAction.Cols[6].Visible = true;
				gridTehAction.Cols[6].Width = 60;
				gridTehAction.Cols[6].AllowDragging = false;
				gridTehAction.Cols[6].AllowEditing = false;
				gridTehAction.Cols[6].AllowMerging = false;
				gridTehAction.Cols[6].AllowResizing = true;
				gridTehAction.Cols[6].AllowSorting = true;
				gridTehAction.Cols[6].Caption = "Кол-во";
				gridTehAction.Cols[6].Format = "N2";

				gridTehAction.Cols[7].Visible = true;
				gridTehAction.Cols[7].Width = 100;
				gridTehAction.Cols[7].AllowDragging = false;
				gridTehAction.Cols[7].AllowEditing = false;
				gridTehAction.Cols[7].AllowMerging = false;
				gridTehAction.Cols[7].AllowResizing = true;
				gridTehAction.Cols[7].AllowSorting = true;
				gridTehAction.Cols[7].Caption = "Причина";

				/*
				gridTehAction.Cols[8].Visible = true;
				gridTehAction.Cols[8].Width = 100;
				gridTehAction.Cols[8].AllowDragging = false;
				gridTehAction.Cols[8].AllowEditing = false;
				gridTehAction.Cols[8].AllowMerging = false;
				gridTehAction.Cols[8].AllowResizing = true;
				gridTehAction.Cols[8].AllowSorting = true;
				gridTehAction.Cols[8].Caption = "Номер заказа";
				*/

			}
		}

		private void FillFilter()
		{
			if (CheckState(db_connection))
			{
				// заполняем фильтр услуг
				db_command_goods_filter = new SqlCommand("SELECT [id_good], [name] FROM [vwGoodListOperator] ORDER BY [name]", db_connection);
				db_adapter_goods_filter = new SqlDataAdapter(db_command_goods_filter);
				db_adapter_goods_filter.Fill(db_table_goods_filter);

				DataRow tmp_row;
				tmp_row = db_table_goods_filter.NewRow();
				tmp_row["id_good"] = "0";
				tmp_row["name"] = "< ВСЕ >";
				db_table_goods_filter.Rows.InsertAt(tmp_row, 0);

				txtFilterGoods.DataSource = db_table_goods_filter;
				txtFilterGoods.DataSource = db_table_goods_filter;
				txtFilterGoods.ValueMember = "id_good";
				txtFilterGoods.DisplayMember = "name";

				// Заполняем фильтр обрезки
				db_table_crop_filter.Columns.Add("code", System.Type.GetType("System.Int32"));
				db_table_crop_filter.Columns.Add("name", System.Type.GetType("System.String"));

				object[] r = new object[2];
				r[0] = 0;
				r[1] = "< ВСЕ >";
				db_table_crop_filter.Rows.Add(r);
				r[0] = 1;
				r[1] = "Обрезать под формат";
				db_table_crop_filter.Rows.Add(r);
				r[0] = 2;
				r[1] = "Сохранить пропорции";
				db_table_crop_filter.Rows.Add(r);
				r[0] = 3;
				r[1] = "Реальный размер";
				db_table_crop_filter.Rows.Add(r);

				txtFilterCrop.DataSource = db_table_crop_filter;
				txtFilterCrop.ValueMember = "code";
				txtFilterCrop.DisplayMember = "name";


				// Заполняем фильтр бумаги
				db_table_paper_filter.Columns.Add("code", System.Type.GetType("System.Int32"));
				db_table_paper_filter.Columns.Add("name", System.Type.GetType("System.String"));

				r[0] = 0;
				r[1] = "< ВСЕ >";
				db_table_paper_filter.Rows.Add(r);
				r[0] = 1;
				r[1] = "Глянцевая";
				db_table_paper_filter.Rows.Add(r);
				r[0] = 2;
				r[1] = "Матовая";
				db_table_paper_filter.Rows.Add(r);

				txtFilterPaper.DataSource = db_table_paper_filter;
				txtFilterPaper.ValueMember = "code";
				txtFilterPaper.DisplayMember = "name";
			}
		}

		private void FillOrderList()
		{
			try
			{
				if (CheckState(db_connection))
				{
					int selid = 0;
					try
					{
						selid = int.Parse(gridOrderList.GetData(gridOrderList.Row, 1).ToString());
					}
					catch
					{ }
					db_table_orders.Rows.Clear();
					string tmpF_Good = "", tmpF_Crop = "", tmpF_Paper = "";
					if ((int)txtFilterCrop.SelectedValue > 0)
						tmpF_Crop = " AND [crop] = " + txtFilterCrop.SelectedValue.ToString();
					else
						tmpF_Crop = "";

					if (txtFilterGoods.SelectedValue.ToString() != "0")
						tmpF_Good = " AND [id_good] = '" + txtFilterGoods.SelectedValue + "'";
					else
						tmpF_Good = "";

					if ((int)txtFilterPaper.SelectedValue > 0)
						tmpF_Paper = " AND [type] = " + txtFilterPaper.SelectedValue.ToString();
					else
						tmpF_Paper = "";

					//SELECT [id_order], [number], [status], [input_date], [expected_date] FROM [vwOrderListOperator] ORDER BY [expected_date]
					db_command_orders = new SqlCommand("SELECT DISTINCT [id_order], [number], [status], [input_date], [expected_date], [id_user_operator] FROM [vwOrderListOperator] WHERE (([id_order] > 0) " + tmpF_Good + "  " + tmpF_Crop + "  " + tmpF_Paper + ") ORDER BY [expected_date]", db_connection);
					db_command_orders.CommandTimeout = 900;
					db_adapter_orders = new SqlDataAdapter(db_command_orders);
					db_adapter_orders.Fill(db_table_orders);
					lblLoaded.Text = "Загружено " + db_table_orders.Rows.Count.ToString("N0", new CultureInfo("de-DE")) + " зак.";


					gridOrderList.DataSource = db_table_orders;
					/*
					 * 1 [id_order], 
					 * 2 [number], 
					 * 3 [status], 
					 * 4 [input_date], 
					 * 5 [expected_date]
					 */
					gridOrderList.Cols[1].Visible = false;
					gridOrderList.Cols[2].Visible = false;
					gridOrderList.Cols[3].Visible = false;
					gridOrderList.Cols[4].Visible = false;
					gridOrderList.Cols[5].Visible = false;
					gridOrderList.Cols[6].Visible = false;


					gridOrderList.Cols[2].Visible = true;
					gridOrderList.Cols[2].Width = 120;
					gridOrderList.Cols[2].AllowDragging = false;
					gridOrderList.Cols[2].AllowEditing = false;
					gridOrderList.Cols[2].AllowMerging = false;
					gridOrderList.Cols[2].AllowResizing = true;
					gridOrderList.Cols[2].AllowSorting = true;
					gridOrderList.Cols[2].DataType = Type.GetType("System.String");
					gridOrderList.Cols[2].Caption = "Заказ №";

					gridOrderList.Cols[4].Visible = true;
					gridOrderList.Cols[4].Width = 110;
					gridOrderList.Cols[4].AllowDragging = false;
					gridOrderList.Cols[4].AllowEditing = false;
					gridOrderList.Cols[4].AllowMerging = false;
					gridOrderList.Cols[4].AllowResizing = true;
					gridOrderList.Cols[4].AllowSorting = true;
					gridOrderList.Cols[4].DataType = Type.GetType("System.String");
					gridOrderList.Cols[4].Caption = "Прием";
					gridOrderList.Cols[4].Format = "g";

					gridOrderList.Cols[5].Visible = true;
					gridOrderList.Cols[5].Width = 110;
					gridOrderList.Cols[5].AllowDragging = false;
					gridOrderList.Cols[5].AllowEditing = false;
					gridOrderList.Cols[5].AllowMerging = false;
					gridOrderList.Cols[5].AllowResizing = true;
					gridOrderList.Cols[5].AllowSorting = true;
					gridOrderList.Cols[5].DataType = Type.GetType("System.String");
					gridOrderList.Cols[5].Caption = "Выдача";
					gridOrderList.Cols[5].Format = "g";

					if (fRow == null)
						fRow = new FilterRowLike(gridOrderList);
					int selrow = 2;
					for (int i = 2; i < gridOrderList.Rows.Count; i++)
					{
						try
						{
							if (selid == int.Parse(gridOrderList.GetData(i, 1).ToString()))
								selrow = i;
						}
						catch (Exception ex)
						{
							ErrorNfo.WriteErrorInfo(ex);
						}
						if (gridOrderList[i, 6].ToString().Trim() == "0")
							gridOrderList.Rows[i].Style = gridOrderList.Styles["MyNull"];
						else if (gridOrderList[i, 6].ToString().Trim() == usr.Id_user.ToString().Trim())
							gridOrderList.Rows[i].Style = gridOrderList.Styles["MyMy"];
						else if (gridOrderList[i, 6].ToString().Trim() != usr.Id_user.ToString().Trim())
							gridOrderList.Rows[i].Style = gridOrderList.Styles["MyNotMy"];
						if (gridOrderList[i, 3].ToString().Trim() == "000111")
							gridOrderList.Rows[i].Style = gridOrderList.Styles["MyOK"];
						if (gridOrderList[i, 3].ToString().Trim() == "000211")
							gridOrderList.Rows[i].Style = gridOrderList.Styles["In"];
					}

					if (selrow < gridOrderList.Rows.Count)
						gridOrderList.Select(selrow, 2);

				}
			}
			catch { }
		}

		private void QuickLoadOrder(bool barcode)
		{
			if (CheckState(db_connection))
			{
				tmr.Stop();
				try
				{
					if (((int.Parse(gridOrderList.GetData(gridOrderList.Row, 1).ToString()) > 0) && (!barcode)) || ((txtBarCode.Text.Length > 11) && (barcode)))
					{

						if (barcode)
							db_command = new SqlCommand("SELECT [id_order], [client], [category], [name_accept], [name_designer],[number], [input_date], [expected_date], [comment], [crop], [type], [id_user_operator], [status] FROM [vwOrderQuickListOperator] WHERE RTRIM([number]) = RTRIM('" + txtBarCode.Text.Substring(0, 12) + "')", db_connection);
						else
							db_command = new SqlCommand("SELECT [id_order], [client], [category], [name_accept], [name_designer], [number], [input_date], [expected_date], [comment], [crop], [type], [id_user_operator], [status] FROM [vwOrderQuickListOperator] WHERE [id_order] = " + int.Parse(gridOrderList.GetData(gridOrderList.Row, 1).ToString()), db_connection);
						db_command.CommandTimeout = 900;
						SqlDataReader tmp = db_command.ExecuteReader();
						if (tmp.Read())
						{
							if ((!tmp.IsDBNull(11)) && (!tmp.IsDBNull(12)))
							{
								bool load = false;
								if ((tmp.GetString(12).Trim() == "000100") || (tmp.GetString(12).Trim() == "000110") || (tmp.GetString(12).Trim() == "000111") || (tmp.GetString(12).Trim() == "000211"))
								{
									if ((tmp.GetInt32(11) == usr.Id_user) || (tmp.GetInt32(11) == 0))
									{
										load = true;
									}
									else
									{
										if (MessageBox.Show("Заказ уже обрабатывается другим оператором! Открыть?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
											load = true;
										else
											load = false;
									}
								}
								else
								{
									MessageBox.Show("Заказ не может быть открыт, он находится вне зоны Вашей отвественности!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
									load = false;
								}
								if (load)
								{
									if (!tmp.IsDBNull(0))
										lblFInfoOrderID.Text = tmp.GetInt32(0).ToString();
									else
										lblFInfoOrderID.Text = "";

									//if (!tmp.IsDBNull(1))
									//	lblFInfoOrderClient.Text = tmp.GetString(1);
									//else
									//    lblFInfoOrderClient.Text = "";

									//if (!tmp.IsDBNull(2))
									//    lblFInfoOrderClient.Text += " " + tmp.GetString(2);
									//else
									//    lblFInfoOrderClient.Text += "";

									//if (!tmp.IsDBNull(3))
									//	lblFInfoOrderAcceptance.Text = tmp.GetString(3);
									//else
									//    lblFInfoOrderAcceptance.Text = "";

									if (!tmp.IsDBNull(4))
										lblFInfoOrderDesigner.Text = tmp.GetString(4);
									else
										lblFInfoOrderDesigner.Text = "";

									if (!tmp.IsDBNull(5))
										lblFInfoOrderNo.Text = tmp.GetString(5);
									else
										lblFInfoOrderNo.Text = "";

									if (!tmp.IsDBNull(6))
										lblFInfoOrderDateIn.Text = tmp.GetDateTime(6).ToShortDateString() + " " + tmp.GetDateTime(6).ToShortTimeString();
									else
										lblFInfoOrderDateIn.Text = "";

									//if (!tmp.IsDBNull(7))
									//	lblFInfoOrderDateOut.Text = tmp.GetDateTime(7).ToShortDateString() + " " + tmp.GetDateTime(7).ToShortTimeString();
									//else
									//    lblFInfoOrderDateOut.Text = "";

									//if (!tmp.IsDBNull(8))
									//	lblFInfoOrderComment.Text = tmp.GetString(8);
									//else
									//    lblFInfoOrderComment.Text = "";

									try
									{
										string y = DateTime.Parse(lblFInfoOrderDateIn.Text).Year.ToString();
										string m = DateTime.Parse(lblFInfoOrderDateIn.Text).Month < 10
													? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString()
													: DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString();
										string d = DateTime.Parse(lblFInfoOrderDateIn.Text).Day < 10
													? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString()
													: DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString();
										string f = prop.Dir_print + "\\" + y + "\\" + m + "\\" + d + "\\" + lblFInfoOrderNo.Text.Trim() + "\\" +
												   lblFInfoOrderNo.Text.Trim() + ".txt";
										txtFileInfo.Text = "";
										if (File.Exists(f))
										{
											System.IO.StreamReader s = new System.IO.StreamReader(f, System.Text.Encoding.GetEncoding(1251));
											while (s.Peek() >= 0)
											{
												txtFileInfo.Text += s.ReadLine() + "\n";
											}
											s.Close();

										}
									}
									catch (Exception ex)
									{
										ErrorNfo.WriteErrorInfo(ex);
										MessageBox.Show("Ошибка при открытии файла информации", "Модуль оператора", MessageBoxButtons.OK,
														MessageBoxIcon.Warning);
									}

									if (!tmp.IsDBNull(9))
										switch (tmp.GetInt32(9))
										{
											case 1:
												{
													lblFInfoOrderCrop.Text = "Обрезать под формат";
													break;
												}
											case 2:
												{
													lblFInfoOrderCrop.Text = "Сохранить пропорции";
													break;
												}
											case 3:
												{
													lblFInfoOrderCrop.Text = "Реальный размер";
													break;
												}
										}
									else
										lblFInfoOrderCrop.Text = "";

									if (!tmp.IsDBNull(10))
										switch (tmp.GetInt32(10))
										{
											case 1:
												{
													lblFInfoOrderPaper.Text = "Глянцевая";
													break;
												}
											case 2:
												{
													lblFInfoOrderPaper.Text = "Матовая";
													break;
												}
										}
									else
										lblFInfoOrderPaper.Text = "";

									//btnAvdInfo.Enabled = true;
								}
								else
								{
									ClearOrders();
								}
							}
							else
							{
								if (!tmp.IsDBNull(0))
									lblFInfoOrderID.Text = tmp.GetInt32(0).ToString();
								else
									lblFInfoOrderID.Text = "";

								//if (!tmp.IsDBNull(1))
								//	lblFInfoOrderClient.Text = tmp.GetString(1);
								//else
								//	lblFInfoOrderClient.Text = "";

								//if (!tmp.IsDBNull(2))
								//	lblFInfoOrderClient.Text += " " + tmp.GetString(2);
								//else
								//	lblFInfoOrderClient.Text += "";

								//if (!tmp.IsDBNull(3))
								//	lblFInfoOrderAcceptance.Text = tmp.GetString(3);
								//else
								//	lblFInfoOrderAcceptance.Text = "";

								if (!tmp.IsDBNull(4))
									lblFInfoOrderDesigner.Text = tmp.GetString(4);
								else
									lblFInfoOrderDesigner.Text = "";

								if (!tmp.IsDBNull(5))
									lblFInfoOrderNo.Text = tmp.GetString(5);
								else
									lblFInfoOrderNo.Text = "";

								if (!tmp.IsDBNull(6))
									lblFInfoOrderDateIn.Text = tmp.GetDateTime(6).ToShortDateString() + " " + tmp.GetDateTime(6).ToShortTimeString();
								else
									lblFInfoOrderDateIn.Text = "";

								//if (!tmp.IsDBNull(7))
								//	lblFInfoOrderDateOut.Text = tmp.GetDateTime(7).ToShortDateString() + " " + tmp.GetDateTime(7).ToShortTimeString();
								//else
								//	lblFInfoOrderDateOut.Text = "";

								//if (!tmp.IsDBNull(8))
								//	lblFInfoOrderComment.Text = tmp.GetString(8);
								//else
								//	lblFInfoOrderComment.Text = "";

								if (!tmp.IsDBNull(9))
									switch (tmp.GetInt32(9))
									{
										case 1:
											{
												lblFInfoOrderCrop.Text = "Обрезать под формат";
												break;
											}
										case 2:
											{
												lblFInfoOrderCrop.Text = "Сохранить пропорции";
												break;
											}
										case 3:
											{
												lblFInfoOrderCrop.Text = "Реальный размер";
												break;
											}
									}
								else
									lblFInfoOrderCrop.Text = "";

								if (!tmp.IsDBNull(10))
									switch (tmp.GetInt32(10))
									{
										case 1:
											{
												lblFInfoOrderPaper.Text = "Глянцевая";
												break;
											}
										case 2:
											{
												lblFInfoOrderPaper.Text = "Матовая";
												break;
											}
									}
								else
									lblFInfoOrderPaper.Text = "";

								//btnAvdInfo.Enabled = true;
							}
						}
						tmp.Close();

						db_command = new SqlCommand("SELECT [id_orderbody], [id_order], [id_good], RTRIM([name]) AS [name], [quantity], '=' AS [AS], [actual_quantity], '+' AS [PLUS], '-' AS [SUB], [dateadd], '...' AS [paper], [id_material] FROM [vwOrderBodyOperator] WHERE [id_order] = " + lblFInfoOrderID.Text + " ORDER BY [id_good]", db_connection);
						SqlDataAdapter tmp_a = new SqlDataAdapter(db_command);
						DataTable tmp_t = new DataTable("orderbody");
						tmp_a.Fill(tmp_t);

						/*
						 *  1 [id_orderbody]
						 *  2 [id_order], 
						 *  3 [id_good], 
						 *  4 [name], 
						 *  5 [quantity],
						 *  6 =
						 *  7 [actual_quantity]
						 *  8 +
						 *  9 -
						 *  10 [datework]
						 *  11 [paper]
						 */
						gridEditOrder.DataSource = tmp_t;

						gridEditOrder.Cols[1].Visible = false;
						gridEditOrder.Cols[2].Visible = false;
						gridEditOrder.Cols[3].Visible = false;
						gridEditOrder.Cols[4].Visible = false;
						gridEditOrder.Cols[5].Visible = false;
						gridEditOrder.Cols[6].Visible = false;
						gridEditOrder.Cols[7].Visible = false;
						gridEditOrder.Cols[8].Visible = false;
						gridEditOrder.Cols[9].Visible = false;
						gridEditOrder.Cols[10].Visible = false;
						gridEditOrder.Cols[11].Visible = false;
						gridEditOrder.Cols[12].Visible = false;

						gridEditOrder.Cols[4].Visible = true;
						gridEditOrder.Cols[4].Width = 230;
						gridEditOrder.Cols[4].AllowDragging = false;
						gridEditOrder.Cols[4].AllowEditing = false;
						gridEditOrder.Cols[4].AllowMerging = false;
						gridEditOrder.Cols[4].AllowResizing = true;
						gridEditOrder.Cols[4].AllowSorting = true;
						gridEditOrder.Cols[4].Caption = "Наименование";

						gridEditOrder.Cols[5].Visible = true;
						gridEditOrder.Cols[5].Width = 60;
						gridEditOrder.Cols[5].AllowDragging = false;
						gridEditOrder.Cols[5].AllowEditing = false;
						gridEditOrder.Cols[5].AllowMerging = false;
						gridEditOrder.Cols[5].AllowResizing = true;
						gridEditOrder.Cols[5].AllowSorting = true;
						gridEditOrder.Cols[5].Caption = "Кол-во";
						if (prop.Round3)
							gridEditOrder.Cols[5].Format = "N3";
						else
							gridEditOrder.Cols[5].Format = "N2";

						gridEditOrder.Cols[7].Visible = true;
						gridEditOrder.Cols[7].Width = 60;
						gridEditOrder.Cols[7].AllowDragging = false;
						gridEditOrder.Cols[7].AllowEditing = true;
						gridEditOrder.Cols[7].AllowMerging = false;
						gridEditOrder.Cols[7].AllowResizing = true;
						gridEditOrder.Cols[7].AllowSorting = true;
						gridEditOrder.Cols[7].Caption = "Реально";
						if (prop.Round3)
							gridEditOrder.Cols[7].Format = "N3";
						else
							gridEditOrder.Cols[7].Format = "N2";

						gridEditOrder.Cols[6].Visible = true;
						gridEditOrder.Cols[6].Width = 40;
						gridEditOrder.Cols[6].AllowDragging = false;
						gridEditOrder.Cols[6].AllowEditing = false;
						gridEditOrder.Cols[6].AllowMerging = false;
						gridEditOrder.Cols[6].AllowResizing = true;
						gridEditOrder.Cols[6].AllowSorting = false;
						gridEditOrder.Cols[6].Caption = "=";
						gridEditOrder.Cols[6].Style = gridEditOrder.Styles["Keys"];

						gridEditOrder.Cols[8].Visible = true;
						gridEditOrder.Cols[8].Width = 40;
						gridEditOrder.Cols[8].AllowDragging = false;
						gridEditOrder.Cols[8].AllowEditing = false;
						gridEditOrder.Cols[8].AllowMerging = false;
						gridEditOrder.Cols[8].AllowResizing = true;
						gridEditOrder.Cols[8].AllowSorting = false;
						gridEditOrder.Cols[8].Caption = "+";
						gridEditOrder.Cols[8].Style = gridEditOrder.Styles["Keys"];

						gridEditOrder.Cols[9].Visible = true;
						gridEditOrder.Cols[9].Width = 40;
						gridEditOrder.Cols[9].AllowDragging = false;
						gridEditOrder.Cols[9].AllowEditing = false;
						gridEditOrder.Cols[9].AllowMerging = false;
						gridEditOrder.Cols[9].AllowResizing = true;
						gridEditOrder.Cols[9].AllowSorting = false;
						gridEditOrder.Cols[9].Caption = "-";
						gridEditOrder.Cols[9].Style = gridEditOrder.Styles["Keys"];

						gridEditOrder.Cols[11].Visible = true;
						gridEditOrder.Cols[11].Width = 60;
						gridEditOrder.Cols[11].AllowDragging = false;
						gridEditOrder.Cols[11].AllowEditing = false;
						gridEditOrder.Cols[11].AllowMerging = false;
						gridEditOrder.Cols[11].AllowResizing = true;
						gridEditOrder.Cols[11].AllowSorting = false;
						gridEditOrder.Cols[11].Caption = "Бумага";
						gridEditOrder.Cols[11].Style = gridEditOrder.Styles["Keys2"];

						DateTime tmp_dateend = DateTime.Now.AddYears(-20);
						db_command = new SqlCommand("SELECT MAX(event_date) AS event_date, event_status, id_order FROM orderevent WHERE (event_text LIKE 'Блокирование строчек%') GROUP BY event_status, id_order HAVING (id_order = " + lblFInfoOrderID.Text + ")", db_connection);
						SqlDataReader tmp_r = db_command.ExecuteReader();
						if (tmp_r.Read())
						{
							try
							{
								tmp_dateend = tmp_r.GetDateTime(0);
							}
							catch (Exception)
							{
								//throw;
							}
						}
						tmp_r.Close();

						for (int i = 1; i < gridEditOrder.Rows.Count; i++)
						{
							if (gridEditOrder.GetData(i, 12) != null)
							{
								if ((gridEditOrder.GetData(i, 12).ToString().Trim() == "0") | (gridEditOrder.GetData(i, 12).ToString().Trim() == ""))
									gridEditOrder.SetData(i, 11, "...");
								else
									gridEditOrder.SetData(i, 11, "Выбрано");
							}
							else
							{
								gridEditOrder.SetData(i, 11, "Выбрано");
							}
							if (gridEditOrder.Rows[i][3].ToString() == txtFilterGoods.SelectedValue.ToString())
							{

								gridEditOrder.Rows[i].Style = gridEditOrder.Styles["ActiveGood"];
								if (i == 1)
								{
									gridEditOrder.Styles["Highlight"].ForeColor = Color.MediumBlue;
									gridEditOrder.Styles["Focus"].ForeColor = Color.MediumBlue;
								}
								else
								{
									gridEditOrder.Styles["Highlight"].ForeColor = Color.DarkGray;
									gridEditOrder.Styles["Focus"].ForeColor = Color.DarkGray;
								}
								break;
							}
							if (decimal.Parse(gridEditOrder.Rows[i][7].ToString()) < 0)
							{
								gridEditOrder.Rows[i].Style = gridEditOrder.Styles["Discarding"];
								gridEditOrder.Rows[i].AllowEditing = false;
							}
							if ((gridEditOrder.GetData(i, 10) != null) && (gridEditOrder.GetData(i, 10).ToString() != ""))
							{
								if (
									(
									(System.DateTime.Parse(System.DateTime.Parse(gridEditOrder.GetData(i, 10).ToString()).ToShortDateString() + " " + System.DateTime.Parse(gridEditOrder.GetData(i, 10).ToString()).ToShortTimeString())
									<
									System.DateTime.Parse(tmp_dateend.ToShortDateString() + " " + tmp_dateend.ToShortTimeString())
									) && (System.DateTime.Now.AddYears(-20).Year != tmp_dateend.Year))
									||
									((decimal.Parse(gridEditOrder.GetData(i, 5).ToString()) == 0) && (decimal.Parse(gridEditOrder.GetData(i, 7).ToString()) < 0))
									)
								{
									gridEditOrder.Rows[i].AllowEditing = false;
									gridEditOrder.Rows[i][6] = "";
									gridEditOrder.Rows[i][8] = "";
									gridEditOrder.Rows[i][9] = "";
									//gridEditOrder.Rows[i][11] = "";
								}
							}
							else
							{
								gridEditOrder.Rows[i].AllowEditing = true;
							}

						}
						gridEditOrder.Enabled = false;
					}
				}
				catch (Exception ex)
				{
					//ErrorNfo.WriteErrorInfo(ex);
				}
				finally
				{
				}
				tmr.Start();
			}
		}

		private void FullLoadOrder()
		{
			if (CheckState(db_connection))
			{
				tmr.Stop();
				try
				{
					if (int.Parse(lblFInfoOrderID.Text) > 0)
					{
						db_command = new SqlCommand("UPDATE [order] SET [id_user_operator] = " + usr.Id_user.ToString() + ", [name_operator] = '" + usr.Name + "', [status] = '000110', exported = 0 WHERE [id_order] = " + lblFInfoOrderID.Text, db_connection);
						db_command.ExecuteNonQuery();

						AddEvent("Оператор начал работу над заказом", int.Parse(lblFInfoOrderID.Text));

						groupFilter.Enabled = false;
						groupOrderList.Enabled = false;
						gridEditOrder.Enabled = true;

						btnSaveOrder.Enabled = true;
						//btnCancelOrder.Enabled = true;
						btnEndOrder.Enabled = true;
						btnOpenOrder.Enabled = false;

						gridEditOrder.Styles["Highlight"].ForeColor = Color.Green;
						gridEditOrder.Styles["Focus"].ForeColor = Color.Green;

						txtFileInfo.ReadOnly = false;

						txtBarCode.Enabled = false;

						for (int i = 1; i < gridEditOrder.Rows.Count; i++)
						{
							if (gridEditOrder.Rows[i][7].ToString() == "")
								gridEditOrder.Rows[i][7] = 0;
							_c_open += (int)decimal.Parse(gridEditOrder.Rows[i][7].ToString());
						}

					}
				}
				catch (Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);
				}
				finally
				{
				}
				tmr.Start();
			}
		}

		private void ClearOrders()
		{

			//lblFInfoOrderAcceptance.Text = "";
			//lblFInfoOrderClient.Text = "";
			//lblFInfoOrderComment.Text = "";
			lblFInfoOrderCrop.Text = "";
			//lblFInfoOrderDateOut.Text = "";
			lblFInfoOrderDateIn.Text = "";
			lblFInfoOrderDesigner.Text = "";
			lblFInfoOrderID.Text = "";
			lblFInfoOrderNo.Text = "";
			lblFInfoOrderPaper.Text = "";
			txtFileInfo.Text = "";

			gridEditOrder.DataSource = "";
			int tmp = gridEditOrder.Rows.Count;
			for (int i = tmp; i > 1; i--)
				gridEditOrder.Rows.Remove(gridEditOrder.Rows.Count - 1);

			gridEditOrder.Enabled = false;

			//btnAvdInfo.Enabled = false;
		}

		private void SaveOrder()
		{
			if (CheckState(db_connection))
			{
				tmr.Stop();
				string query = "";
				int order_id = 0;
				try
				{
					for (int i = 1; i < gridEditOrder.Rows.Count; i++)
					{
						if (gridEditOrder.Rows[i][7].ToString() == "")
							gridEditOrder.Rows[i][7] = 0;
						_c_close += (int)decimal.Parse(gridEditOrder.Rows[i][7].ToString());
						if ((decimal.Parse(gridEditOrder.Rows[i][7].ToString()) > 0) &&
							(gridEditOrder.Rows[i][7].ToString().ToLower() != "null") && (gridEditOrder.Rows[i][7] != null))
						{
							string yin, min, din;
							yin = DateTime.Now.Year.ToString();

							if (DateTime.Now.Month < 10)
								min = "0" + DateTime.Now.Month.ToString();
							else
								min = DateTime.Now.Month.ToString();

							if (DateTime.Now.Day < 10)
								din = "0" + DateTime.Now.Day.ToString();
							else
								din = DateTime.Now.Day.ToString();
							SqlCommand t =
								new SqlCommand(
									"SELECT [id_mashine], [id_material], [actual_quantity], [id_order] FROM [orderbody] WHERE [id_orderbody] = " +
									gridEditOrder.Rows[i][1].ToString(), db_connection);
							SqlDataReader r = t.ExecuteReader();
							if (r.Read())
							{
								query += "UPDATE [order] SET [exported] = 0 WHERE [id_order] = " + r.GetInt32(3) + ";";
								order_id = r.GetInt32(3);
								if (!r.IsDBNull(2))
								{
									//if ((r.GetString(0).Trim() == "") && (r.GetString(1).Trim() == ""))
									if (r.GetDecimal(2) != decimal.Parse(gridEditOrder.Rows[i][7].ToString()))
									{
										query += "UPDATE [orderbody] SET [actual_quantity] = " + gridEditOrder.Rows[i][7].ToString().Replace(",", ".") +
												 ", [datework] = getdate() " +
												 ", [id_mashine] = '" + usr.Id_Mashine + "', [id_material] = '" + gridEditOrder.Rows[i][12].ToString() +
												 "', [id_user_work] = " + usr.Id_user + ", [name_work] = '" + usr.Name +
												 "', exported = 0 WHERE [id_orderbody] = " + gridEditOrder.Rows[i][1].ToString();
										//"', [id_mashine] = '" + usr.Id_Mashine + "', [id_material] = '" + usr.Id_Paper +
									}

									else
									{
										/*
										query += "UPDATE [orderbody] SET [actual_quantity] = " + gridEditOrder.Rows[i][7].ToString().Replace(",", ".") +
												 ", [datework] = '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() +
												 "', [id_user_work] = " + usr.Id_user + ", [name_work] = '" + usr.Name +
												 "', exported = 0 WHERE [id_orderbody] = " + gridEditOrder.Rows[i][1].ToString();
										 */
									}

								}
								else
								{
									query += "UPDATE [orderbody] SET [actual_quantity] = " + gridEditOrder.Rows[i][7].ToString().Replace(",", ".") +
											 ", [datework] = getdate()" +
											 ", [id_mashine] = '" + usr.Id_Mashine + "', [id_material] = '" + gridEditOrder.Rows[i][12].ToString() +
												 "', [id_user_work] = " +
											 usr.Id_user + ", [name_work] = '" + usr.Name + "', exported = 0 WHERE [id_orderbody] = " +
											 gridEditOrder.Rows[i][1].ToString();
									//"', [id_mashine] = '" + usr.Id_Mashine + "', [id_material] = '" + usr.Id_Paper + "', [id_user_work] = " +
								}
							}
							r.Close();
						}
						else
						{
							query += "UPDATE [orderbody] SET [actual_quantity] = " + gridEditOrder.Rows[i][7].ToString().Replace(",", ".") +
									 ", exported = 0 WHERE [id_orderbody] = " + gridEditOrder.Rows[i][1].ToString() + ";\n";
						}
					}
				}
				catch (Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);
				}

				if (query != "")
				{
					db_command = new SqlCommand(query, db_connection);
					db_command.ExecuteNonQuery();
					if (order_id > 0)
					{
						AddEvent("Оператор сохранил изменения в заказе", order_id);
					}
				}

				try
				{
					string y = DateTime.Parse(lblFInfoOrderDateIn.Text).Year.ToString();
					string m = DateTime.Parse(lblFInfoOrderDateIn.Text).Month < 10
								? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString()
								: DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString();
					string d = DateTime.Parse(lblFInfoOrderDateIn.Text).Day < 10
								? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString()
								: DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString();
					string f = prop.Dir_print + "\\" + y + "\\" + m + "\\" + d + "\\" + lblFInfoOrderNo.Text.Trim() + "\\" +
							   lblFInfoOrderNo.Text.Trim() + ".txt";
					if (File.Exists(f))
					{
						txtFileInfo.SaveFile(f, RichTextBoxStreamType.PlainText);
					}
				}
				catch (Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);
					MessageBox.Show("Ошибка при сохранении файла информации", "Модуль оператора", MessageBoxButtons.OK,
									MessageBoxIcon.Warning);
				}

				tmr.Start();
			}
			recalcCounter(true);
		}

		private void FinishOrder()
		{
			if (CheckState(db_connection))
			{
				tmr.Stop();
				SqlCommand tmp = new SqlCommand("SELECT COUNT(*) AS CNT " +
												"FROM " +
												"(SELECT dbo.orderbody.id_order, " +
												"dbo.orderbody.id_good, " +
												"dbo.good.type, " +
												"dbo.orderbody.quantity, " +
												"dbo.orderbody.actual_quantity " +
												"FROM dbo.orderbody INNER JOIN " +
												"dbo.good ON dbo.orderbody.id_good = dbo.good.id_good " +
												"WHERE (dbo.good.type = 2) AND " +
												"(dbo.orderbody.actual_quantity <> dbo.orderbody.quantity) AND " +
												"(dbo.orderbody.id_order = " + lblFInfoOrderID.Text + ")) AS TBL", db_connection);
				if ((int)tmp.ExecuteScalar() > 0)
				{
					db_command = new SqlCommand("UPDATE [order] SET [id_user_operator] = " + usr.Id_user.ToString() + ", [name_operator] = '" + usr.Name + "', [status] = '000111', exported = 0 WHERE [id_order] = " + lblFInfoOrderID.Text, db_connection);
					db_command.ExecuteNonQuery();
					AddEvent("Оператор завершил работу над заказом", int.Parse(lblFInfoOrderID.Text));
				}
				else
				{
					db_command = new SqlCommand("UPDATE [order] SET [id_user_operator] = " + usr.Id_user.ToString() + ", [name_operator] = '" + usr.Name + "', [status] = '000111', exported = 0 WHERE [id_order] = " + lblFInfoOrderID.Text, db_connection);
					db_command.ExecuteNonQuery();
					AddEvent("Оператор завершил работу над заказом", int.Parse(lblFInfoOrderID.Text));
				}


				tmr.Start();
			}

		}
	}
}
