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
		private void HideWindow()
		{
			this.Top = (-1) * (this.Height - 10);
		}

		private void mnuSetup_Click(object sender, EventArgs e)
		{
			OpenSettings();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			hidewin = false;
			this.Close();
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			this.Top = 0;
			this.Focus();
			this.Activate();
		}

		private void btnHide_Click(object sender, EventArgs e)
		{
			HideWindow();
		}

		private void btnApplyFilter_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			FillOrderList();
			tmr.Start();
		}

		private void gridOrderList_DoubleClick(object sender, EventArgs e)
		{
			QuickLoadOrder(false);
		}

		private void gridOrderList_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				QuickLoadOrder(false);
			}
		}

		private void gridEditOrder_Click(object sender, EventArgs e)
		{
			if (gridEditOrder.Row > 0)
			{
				if (gridEditOrder.GetData(gridEditOrder.Row, gridEditOrder.Selection.LeftCol) != null)
				{
					if (decimal.Parse(gridEditOrder.Rows[gridEditOrder.Row][7].ToString()) >= 0)
					{
						if (gridEditOrder.GetData(gridEditOrder.Row, gridEditOrder.Selection.LeftCol).ToString() == "=")
							gridEditOrder.Rows[gridEditOrder.Row][7] = gridEditOrder.Rows[gridEditOrder.Row][5];
						if (gridEditOrder.GetData(gridEditOrder.Row, gridEditOrder.Selection.LeftCol).ToString() == "+")
							gridEditOrder.Rows[gridEditOrder.Row][7] = decimal.Parse(gridEditOrder.Rows[gridEditOrder.Row][7].ToString()) + 1;
						if ((gridEditOrder.GetData(gridEditOrder.Row, gridEditOrder.Selection.LeftCol).ToString() == "-") &&
							((decimal)gridEditOrder.Rows[gridEditOrder.Row][7] > 0))
							gridEditOrder.Rows[gridEditOrder.Row][7] = decimal.Parse(gridEditOrder.Rows[gridEditOrder.Row][7].ToString()) - 1;
						if ((gridEditOrder.Selection.LeftCol == 11) &&
							((decimal)gridEditOrder.Rows[gridEditOrder.Row][7] > 0) && gridEditOrder.Rows[gridEditOrder.Row][6].ToString() == "=")
						{
							if (CheckState(db_connection))
							{
								this.TopMost = false;
								SelectPaper sp = new SelectPaper(db_connection);
								if (gridEditOrder.GetData(gridEditOrder.Row, 12) != null)
									sp._id_paper = gridEditOrder.GetData(gridEditOrder.Row, 12).ToString().Trim();
								sp.ShowDialog();
								if ((sp.DialogResult == DialogResult.OK) && (sp.txtPaper.SelectedValue.ToString() != "0"))
								{
									gridEditOrder.SetData(gridEditOrder.Row, 12, sp.txtPaper.SelectedValue.ToString());
									gridEditOrder.SetData(gridEditOrder.Row, 11, "Выбрано");
								}
								if (sp.txtPaper.SelectedValue.ToString() == "0")
								{
									gridEditOrder.SetData(gridEditOrder.Row, 12, sp.txtPaper.SelectedValue.ToString());
									gridEditOrder.SetData(gridEditOrder.Row, 11, "...");
								}
								this.TopMost = prop.Mod_operator_top_most;
							}

						}
					}
				}
			}
		}

		private void gridEditOrder_DoubleClick(object sender, EventArgs e)
		{
			if (gridEditOrder.Row > 0)
			{
				if (gridEditOrder.GetData(gridEditOrder.Row, gridEditOrder.Selection.LeftCol) != null)
				{
					if (decimal.Parse(gridEditOrder.Rows[gridEditOrder.Row][7].ToString()) >= 0)
					{
						if (gridEditOrder.GetData(gridEditOrder.Row, gridEditOrder.Selection.LeftCol).ToString() == "=")
							gridEditOrder.Rows[gridEditOrder.Row][7] = gridEditOrder.Rows[gridEditOrder.Row][5];
						if (gridEditOrder.GetData(gridEditOrder.Row, gridEditOrder.Selection.LeftCol).ToString() == "+")
							gridEditOrder.Rows[gridEditOrder.Row][7] = decimal.Parse(gridEditOrder.Rows[gridEditOrder.Row][7].ToString()) + 1;
						if ((gridEditOrder.GetData(gridEditOrder.Row, gridEditOrder.Selection.LeftCol).ToString() == "-") &&
							((decimal)gridEditOrder.Rows[gridEditOrder.Row][7] > 0))
							gridEditOrder.Rows[gridEditOrder.Row][7] = decimal.Parse(gridEditOrder.Rows[gridEditOrder.Row][7].ToString()) - 1;
						if ((gridEditOrder.Selection.LeftCol == 11) &&
	((decimal)gridEditOrder.Rows[gridEditOrder.Row][7] > 0) && gridEditOrder.Rows[gridEditOrder.Row][6].ToString() == "=")
						{
							if (CheckState(db_connection))
							{
								this.TopMost = false;
								SelectPaper sp = new SelectPaper(db_connection);
								if (gridEditOrder.GetData(gridEditOrder.Row, 12) != null)
									sp._id_paper = gridEditOrder.GetData(gridEditOrder.Row, 12).ToString().Trim();
								sp.ShowDialog();
								if ((sp.DialogResult == DialogResult.OK) && (sp.txtPaper.SelectedValue.ToString() != "0"))
								{
									gridEditOrder.SetData(gridEditOrder.Row, 12, sp.txtPaper.SelectedValue.ToString());
									gridEditOrder.SetData(gridEditOrder.Row, 11, "Выбрано");
								}
								if (sp.txtPaper.SelectedValue.ToString() == "0")
								{
									gridEditOrder.SetData(gridEditOrder.Row, 12, sp.txtPaper.SelectedValue.ToString());
									gridEditOrder.SetData(gridEditOrder.Row, 11, "...");
								}
								this.TopMost = prop.Mod_operator_top_most;
							}

						}

					}
				}
			}
		}

		private void btnSaveOrder_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			SaveOrder();

			ClearOrders();

			groupFilter.Enabled = true;
			groupOrderList.Enabled = true;

			gridEditOrder.Enabled = false;

			btnSaveOrder.Enabled = false;
			//btnCancelOrder.Enabled = false;
			btnEndOrder.Enabled = false;
			btnOpenOrder.Enabled = true;

			txtFileInfo.ReadOnly = true;

			gridEditOrder.Styles["Highlight"].ForeColor = Color.DarkGray;
			gridEditOrder.Styles["Focus"].ForeColor = Color.DarkGray;

			FillOrderList();

			txtBarCode.Enabled = true;

			tmr.Start();
		}

		private void btnEndOrder_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			SaveOrder();

			FinishOrder();

			ClearOrders();

			groupFilter.Enabled = true;
			groupOrderList.Enabled = true;

			btnSaveOrder.Enabled = false;
			//btnCancelOrder.Enabled = false;
			btnEndOrder.Enabled = false;
			btnOpenOrder.Enabled = true;

			gridEditOrder.Enabled = false;

			txtFileInfo.ReadOnly = true;

			gridEditOrder.Styles["Highlight"].ForeColor = Color.DarkGray;
			gridEditOrder.Styles["Focus"].ForeColor = Color.DarkGray;

			FillOrderList();


			txtBarCode.Enabled = true;

			tmr.Start();
		}

		private void btnCancelOrder_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			ClearOrders();

			groupFilter.Enabled = true;
			groupOrderList.Enabled = true;

			gridEditOrder.Enabled = false;


			btnSaveOrder.Enabled = false;
			//btnCancelOrder.Enabled = false;
			btnEndOrder.Enabled = false;
			btnOpenOrder.Enabled = true;

			gridEditOrder.Styles["Highlight"].ForeColor = Color.DarkGray;
			gridEditOrder.Styles["Focus"].ForeColor = Color.DarkGray;

			FillOrderList();
			tmr.Start();
		}

		private void gridOrderList_Click(object sender, EventArgs e)
		{

		}

		private void btnOpenOrder_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			FullLoadOrder();
			tmr.Start();
		}

		private void btnAddBrak_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			this.TopMost = false;
			frmBadWorkForm f = new frmBadWorkForm();
			f.db_connection = db_connection;
			f.usr = usr;
			//f.id_paper = usr.Id_Paper;
			f.id_mashine = usr.Id_Mashine;
			f.id_good = txtFilterGoods.SelectedValue.ToString();
			if (!btnOpenOrder.Enabled)
			{
				f.id_order = int.Parse(lblFInfoOrderID.Text);
				f.txtOrderNo.Text = lblFInfoOrderNo.Text;
			}
			f.OrderNo = lblFInfoOrderNo.Text;

			f.ShowDialog();
			if (f.DialogResult == DialogResult.OK)
			{
				FillTehAction();
				recalcCounter(true);
			}
			this.TopMost = prop.Mod_operator_top_most;
			tmr.Start();
		}

		private void btnEditBrak_Click(object sender, EventArgs e)
		{
			if (CheckState(db_connection))
			{
				tmr.Stop();
				try
				{
					if (usr.prmCanDelEditBrak)
					{
						if (int.Parse(gridTehAction.GetData(gridTehAction.Row, 1).ToString()) > 0)
						{
							this.TopMost = false;
							frmBadWorkForm f = new frmBadWorkForm(int.Parse(gridTehAction.GetData(gridTehAction.Row, 1).ToString()));
							f.db_connection = db_connection;
							f.usr = usr;
							f.ShowDialog();
							if (f.DialogResult == DialogResult.OK)
							{
								FillTehAction();
								recalcCounter(true);
							}
							this.TopMost = prop.Mod_operator_top_most;
						}
					}
					else
					{
						MessageBox.Show("Отказано в доступе", "Доступ", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
					}
				}
				catch (Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);
				}
				tmr.Start();
			}
		}

		private void btnDelBrak_Click(object sender, EventArgs e)
		{
			if (CheckState(db_connection))
			{
				tmr.Stop();
				if (usr.prmCanDelEditBrak)
				{
					if (
						MessageBox.Show("Удалить запись о списании?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
						DialogResult.OK)
					{
						try
						{
							if (int.Parse(gridTehAction.GetData(gridTehAction.Row, 1).ToString()) > 0)
							{
								SqlCommand t =
									new SqlCommand(
										"UPDATE [orderbody] SET [del] = 1, exported = 0 WHERE [id_orderbody] = " +
										int.Parse(gridTehAction.GetData(gridTehAction.Row, 1).ToString()), db_connection);
								t.ExecuteNonQuery();
							}
						}
						catch (Exception ex)
						{
							ErrorNfo.WriteErrorInfo(ex);
						}
						finally
						{
							FillTehAction();
							recalcCounter(true);
						}
					}
				}
				else
				{
					MessageBox.Show("Отказано в доступе", "Доступ", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
				}
				tmr.Start();
			}
		}

		private void ShowDiscardingReport()
		{
			if (CheckState(db_connection))
			{
				tmr.Stop();
				this.TopMost = false;
				frmDiscardingReportPrep frm = new frmDiscardingReportPrep(usr, db_connection);
				frm.ShowDialog();
				this.TopMost = prop.Mod_operator_top_most;
				tmr.Start();
			}
		}

		private void btnReportSpisan_Click(object sender, EventArgs e)
		{
			ShowDiscardingReport();
		}

		private void btnChangePaper_Click(object sender, EventArgs e)
		{
			if (CheckState(db_connection))
			{
				tmr.Stop();
				this.TopMost = false;
				if (closeCounter())
				{
					frmSelectMashineAndPaper f = new frmSelectMashineAndPaper();
					f.db_connection = db_connection;
					f._id_mashine = usr.Id_Mashine;
					//f._id_paper = usr.Id_Paper;
					f.ShowDialog();
					if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
					{
						usr.Id_Mashine = f.txtMashine.SelectedValue.ToString();
						usr.Mashine = f.txtMashine.Text;
						//usr.Id_Paper = f.txtPaper.SelectedValue.ToString();
						//usr.Paper = f.txtPaper.Text;

						SqlCommand _counter_cmd = new SqlCommand();
						_counter_cmd.Connection = db_connection;
						_counter_cmd.CommandTimeout = 9000;
						_counter_cmd.CommandText = "SELECT id_counter, id_mashine, id_user, name_user, c1, c1date, c2, c2date, c0 " +
													"FROM dbo.counters " +
													"WHERE (id_counter = " +
													"(SELECT MAX(id_counter) AS id " +
													"FROM dbo.counters AS counters_1 " +
													"WHERE (id_mashine = '" + usr.Id_Mashine.Trim() + "')))";
						SqlDataAdapter _counter_da = new SqlDataAdapter(_counter_cmd);
						DataTable _counter_tbl = new DataTable();
						_counter_da.Fill(_counter_tbl);
						if (_counter_tbl.Rows.Count > 0)
						{
							if ((_counter_tbl.Rows[0]["c2"].ToString() == "") && (_counter_tbl.Rows[0]["c2date"].ToString() == ""))
							{
								MessageBox.Show("Прошлый счетчик, открытый " + _counter_tbl.Rows[0]["c1date"].ToString() +
									" пользователем " + _counter_tbl.Rows[0]["name_user"].ToString().Trim() + " не был закрыт!\n" +
									"Автоматически закрываем значением " + f.txtCounter.Value.ToString());
								_counter_cmd.CommandText = "UPDATE [counters] SET c2 = " + f.txtCounter.Value.ToString() +
									", c2date = getdate(), c0 = 0 " +
									"WHERE id_counter = " + _counter_tbl.Rows[0]["id_counter"].ToString();
								_counter_cmd.ExecuteNonQuery();
							}
						}
						_counter_cmd.CommandText = "INSERT INTO [counters] (id_mashine, id_user, name_user, c1, c1date) VALUES ('" +
							usr.Id_Mashine + "', " + usr.Id_user + ", '" + usr.Name.Trim() + "', " + f.txtCounter.Value.ToString() +
							", getdate())";
						_counter_cmd.ExecuteNonQuery();
						_counter1 = long.Parse(f.txtCounter.Value.ToString());
						_counter1Date = DateTime.Now;
						recalcCounter(true);

						lblMashine.Text = usr.Mashine.Trim();
						//lblPaper.Text = usr.Paper.Trim();
					}
					else
					{
						hidewin = false;
						globalexit = true;
						this.Close();
					}
				}
				this.TopMost = prop.Mod_operator_top_most;
				tmr.Start();
			}
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (hidewin)
			{
				e.Cancel = true;
				HideWindow();
			}
			else
			{
				if (!globalexit)
				{
					if (
						MessageBox.Show("выйти из программы?", "Модуль оператора", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
						DialogResult.OK)
					{
						if (MessageBox.Show("Закрыть счетчик?", "Модуль оператора", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
							DialogResult.OK)
						{
							this.TopMost = false;
							if (closeCounter())
							{
								e.Cancel = false;
							}
							else
							{
								e.Cancel = true;
							}
							this.TopMost = prop.Mod_operator_top_most;
						}
						else
						{
							e.Cancel = true;
						}
					}
					else
						e.Cancel = true;
				}
				else
				{
					e.Cancel = false;
				}
			}
		}

	}
}
