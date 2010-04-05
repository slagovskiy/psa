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
using Photoland.Components.FilterRow;
using Photoland.Forms.Interface;
using Photoland.Lib;
using Photoland.Order;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.User;
using Photoland.Security.Discont;

namespace Photoland.Exchanger
{
	public partial class frmLoadOrders : Form
	{
		private DataTable _order = new DataTable();
		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
		public DataTable load = new DataTable("load");

		public frmLoadOrders(DataTable _o)
		{
			InitializeComponent();
			this._order = _o;
			load.Columns.Add(new DataColumn("id"));
		}

		private void frmLoadOrders_Load(object sender, EventArgs e)
		{
			GridOder.DataSource = _order;
			for (int i = 1; i < GridOder.Cols.Count; i++)
			{
				GridOder.Cols[i].Visible = false;
			}
			GridOder.Cols[1].Visible = true;
			GridOder.Cols[14].Visible = true;
			GridOder.Cols[15].Visible = true;
			GridOder.Cols[16].Visible = true;
			GridOder.Cols[17].Visible = true;

			GridOder.Cols[1].Caption = "?";
			GridOder.Cols[14].Caption = "Номер";
			GridOder.Cols[15].Caption = "Статус";
			GridOder.Cols[16].Caption = "Прием";
			GridOder.Cols[17].Caption = "Выдача";

		}

		private void btnSelectAll_Click(object sender, EventArgs e)
		{
			for (int i = 1; i < GridOder.Rows.Count; i++)
			{
				GridOder.Rows[i][1] = true;
			}
		}

		private void btnClear_Click(object sender, EventArgs e)
		{
			for (int i = 1; i < GridOder.Rows.Count; i++)
			{
				GridOder.Rows[i][1] = false;
			}
		}

		private void btnPrint_Click(object sender, EventArgs e)
		{
			if (prop.PathReportsTemplates != "")
			{
				rep.Load(prop.PathReportsTemplates, "Load Order");
				rep.DataSource.Recordset = _order;
				rep.Document.Print();
			}
			else
			{
				MessageBox.Show("Не выбран файл шаблонов отчетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void PrintCheck()
		{
			// Печатаем чек
			using (SqlConnection con = new SqlConnection(prop.Connection_string))
			{
				con.Open();
				foreach (DataRow r in _order.Rows)
				{
					OrderInfo prnOrder = new OrderInfo(con, r[14].ToString().Trim(), true);
					try
					{
						if (prop.PathReportsTemplates != "")
						{
							rep.Load(prop.PathReportsTemplates, "Check");
							rep.DataSource.Recordset = prnOrder.OrderBody;
							decimal itog = 0;
							decimal iitog = 0;
							for (int i = 0; i < prnOrder.OrderBody.Rows.Count; i++)
							{
								itog += decimal.Parse(prnOrder.OrderBody.Rows[i]["price"].ToString()) *
										decimal.Parse(prnOrder.OrderBody.Rows[i]["quantity"].ToString());
							}
							rep.Fields["Total"].Text = itog.ToString().Replace(",", ".");
							rep.Fields["BarCode"].Text = prnOrder.Orderno.Trim();
							rep.Fields["OrderNo"].Text = prnOrder.Orderno.Trim();
							rep.Fields["DateOut"].Text = prnOrder.Dateout + " " + prnOrder.Timeout;
							rep.Fields["Client"].Text = prnOrder.Client.Name.Trim();
							rep.Fields["AddonInfo"].Text = prop.ReklamBlock1;
							rep.Fields["Priemka"].Text = "Заказ принят: " + prnOrder.Datein + " " + prnOrder.Timein + "\nЗаказ принял: " + prnOrder.Name_accept;
							string tp = "";
							switch (prnOrder.Crop)
							{
								case 1:
									{
										tp = "Обрезать под формат; ";
										break;
									}
								case 2:
									{
										tp = "Сохранить пропорции; ";
										break;
									}
								case 3:
									{
										tp = "Реальный размер; ";
										break;
									}
							}
							if (prnOrder.Preview > 0)
								rep.Fields["PreView"].Visible = true;
							if (prnOrder.Type == 1) tp += "Глянцевая бумага;";
							if (prnOrder.Type == 2) tp += "Матовая бумага;";
							rep.Fields["TypePaper"].Text = tp;
							if (prnOrder.Discont != null)
							{
								rep.Fields["Discont"].Text = prnOrder.Discont.Discserv.ToString().Replace(",", ".");
								iitog = itog - ((itog * prnOrder.Discont.Discserv) / 100);
							}
							else
							{
								rep.Fields["Discont"].Text = "0";
								iitog = itog;
							}
							switch (prop.ModelRound)
							{
								case 0:
									{
										break;
									}
								case 1:
									{
										if (((iitog - ((int)iitog)) <= (decimal)0.25) && ((iitog - ((int)iitog)) > 0))
											iitog = ((int)iitog);
										else if (((iitog - ((int)iitog)) > (decimal)0.25) && ((iitog - ((int)iitog)) <= (decimal)0.75))
											iitog = ((int)iitog) + (decimal)0.5;
										else if ((iitog - ((int)iitog)) > (decimal)0.75)
											iitog = ((int)iitog) + 1;
										break;
									}
								case 2:
									{
										if (((iitog - ((int)iitog)) <= (decimal)0.45) && ((iitog - ((int)iitog)) > 0))
											iitog = ((int)iitog);
										else if (((iitog - ((int)iitog)) > (decimal)0.45) && ((iitog - ((int)iitog)) <= (decimal)0.95))
											iitog = ((int)iitog) + (decimal)0.5;
										else if ((iitog - ((int)iitog)) > (decimal)0.95)
											iitog = ((int)iitog) + 1;
										break;
									}
								case 3:
									{
										if (((iitog - ((int)iitog)) <= (decimal)0.15) && ((iitog - ((int)iitog)) > 0))
											iitog = (int)iitog;
										else if (((iitog - ((int)iitog)) > (decimal)0.15) && ((iitog - ((int)iitog)) <= (decimal)0.65))
											iitog = ((int)iitog) + (decimal)0.5;
										else if ((iitog - ((int)iitog)) > (decimal)0.65)
											iitog = ((int)iitog) + 1;
										break;
									}
								case 4:
									{
										if (((iitog - ((int)iitog)) <= (decimal)0.49) && ((iitog - ((int)iitog)) > 0))
											iitog = (int)iitog;
										else if ((iitog - ((int)iitog)) > (decimal)0.49)
											iitog = ((int)iitog) + 1;
										break;
									}
								case 5:
									{
										if (((iitog - ((int)iitog)) <= (decimal)0.5) && ((iitog - ((int)iitog)) > 0))
											iitog = ((int)iitog) + (decimal)0.5;
										else if ((iitog - ((int)iitog)) > (decimal)0.5)
											iitog = ((int)iitog) + 1;
										break;
									}
							}
							rep.Fields["Itogo"].Text = iitog.ToString().Replace(",", ".");
							decimal p = prnOrder.FinalPayment + prnOrder.AdvancedPayment;
							rep.Fields["Payment"].Text = p.ToString().Replace(",", ".");
							rep.Fields["EndPayment"].Text = (iitog - p).ToString().Replace(",", ".");
							if (prop.CheckPreview)
							{
								PrintPreviewDialog pd = new PrintPreviewDialog();
								pd.ClientSize = new Size(465, 680);
								pd.StartPosition = FormStartPosition.CenterScreen;
								pd.PrintPreviewControl.Zoom = 1.5;
								pd.Document = rep.Document;
								pd.ShowDialog();
							}
							else
							{
								rep.Document.Print();
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
						MessageBox.Show("Ошибка вывода чека\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void btnLoad_Click(object sender, EventArgs e)
		{
			for (int i = 1; i < GridOder.Rows.Count; i++)
			{
				if ((bool)GridOder.Rows[i][1])
				{
					load.Rows.Add(GridOder.Rows[i][2].ToString());
				}
			}
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			PrintCheck();
		}
	}
}