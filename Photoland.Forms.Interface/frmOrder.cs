using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.Discont;
using Photoland.Security.User;
using Photoland.Lib;
using Photoland.Acceptance.NumGenerator;
using Photoland.Order;
using System.IO;
using System.Net;


namespace Photoland.Forms.Interface
{
	public partial class frmOrder : Form
	{
		public SqlConnection db_connection;
		public OrderInfo order;
		public bool NewOrder;
		public UserInfo usr;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
		private DataTable tblOrder = new DataTable("Order");
		private DataTable tblPrintOrder = new DataTable("Print");
		private DataTable tblServOrder = new DataTable("Serv");
		private OrderNum num = new OrderNum();


		public frmOrder(UserInfo usr, bool New_Order, OrderInfo order)
		{
			InitializeComponent();
			this.Text = "Заказ";
			this.order = order;
			this.NewOrder = New_Order;
			this.usr = usr;
		}

		public frmOrder(UserInfo usr, bool New_Order)
		{
			InitializeComponent();
			this.Text = "Заказ";
			this.NewOrder = New_Order;
			this.usr = usr;
		}

		private void frmOrder_Load(object sender, EventArgs e)
		{
			gridOrder.Rows.DefaultSize = 25;
			tblOrder.Columns.Add("check", System.Type.GetType("System.Boolean"));
			tblOrder.Columns.Add("Name", System.Type.GetType("System.String"));
			tblOrder.Columns.Add("Count", System.Type.GetType("System.Decimal"));
			tblOrder.Columns.Add("Price", System.Type.GetType("System.Decimal"));
			tblOrder.Columns.Add("Sum", System.Type.GetType("System.Decimal"));
			tblOrder.Columns.Add("id", System.Type.GetType("System.String"));
			tblOrder.Columns.Add("Folder", System.Type.GetType("System.String"));
			tblOrder.Columns.Add("Sign", System.Type.GetType("System.String"));
			tblOrder.Columns.Add("del", System.Type.GetType("System.Boolean"));
			tblOrder.Columns.Add("Content", System.Type.GetType("System.Object"));
			tblOrder.Columns.Add("guid", System.Type.GetType("System.String"));
            tblOrder.Columns.Add("adduser", System.Type.GetType("System.String"));
            tblOrder.Columns.Add("comment", System.Type.GetType("System.String"));

			tblPrintOrder.Columns.Add("check", System.Type.GetType("System.Boolean"));
			tblPrintOrder.Columns.Add("Name", System.Type.GetType("System.String"));
			tblPrintOrder.Columns.Add("Count", System.Type.GetType("System.Decimal"));
			tblPrintOrder.Columns.Add("Folder", System.Type.GetType("System.String"));
			tblPrintOrder.Columns.Add("id", System.Type.GetType("System.String"));

			tblServOrder.Columns.Add("id_serv", System.Type.GetType("System.String"));
			tblServOrder.Columns.Add("Name", System.Type.GetType("System.String"));
			tblServOrder.Columns.Add("SName", System.Type.GetType("System.String"));
			tblServOrder.Columns.Add("Count", System.Type.GetType("System.Decimal"));
			tblServOrder.Columns.Add("Content", System.Type.GetType("System.Object"));
			tblServOrder.Columns.Add("guid", System.Type.GetType("System.String"));
            tblServOrder.Columns.Add("adduser", System.Type.GetType("System.String"));
            tblServOrder.Columns.Add("comment", System.Type.GetType("System.String"));

			SqlCommand _ptype_cmd = new SqlCommand();
			_ptype_cmd.Connection = db_connection;
			_ptype_cmd.CommandText = "SELECT * FROM PTYPE ORDER BY ID_PTYPE";
			_ptype_cmd.CommandTimeout = 9000;
			SqlDataAdapter _ptype_da = new SqlDataAdapter(_ptype_cmd);
			DataTable _ptype_tbl = new DataTable();
			_ptype_da.Fill(_ptype_tbl);
			DataRow _ptype_r = _ptype_tbl.NewRow();
			_ptype_r["id_ptype"] = -1;
			_ptype_r["name_ptype"] = "Не выбрано!";
			_ptype_tbl.Rows.InsertAt(_ptype_r, 0);
			txtPType.DataSource = _ptype_tbl;
			txtPType.DisplayMember = "name_ptype";
			txtPType.ValueMember = "id_ptype";
			txtPType.SelectedValue = -1;


			tmr.Interval = prop.Dir_rescan * 1000;

			FillQuickButtons();

			ReBild();


		}

		private void ReBild()
		{
			// Если заполняем из новой
			if (this.NewOrder)
			{
				// Если объект передан из визарда
				if (this.order != null)
				{
					//Номер
					lblOrderNo.Text = order.Orderno;
					// дата поступления заказа
					lblDateOrderInput.Text = order.Datein + " " + order.Timein;
					// дата выдачи заказа
					txtOrderDateOutput.Value = DateTime.Parse(order.Dateout);

					double seltime = DateTime.Parse(order.Dateout.ToString()).Hour;

					double t = prop.Time_begin_work;
					while (t <= double.Parse(prop.Time_end_work.ToString()))
					{
						if (t.ToString().LastIndexOf(",5") > 0)
						{
							txtOrderTimeOutput.Items.Add(t.ToString().Replace(",5", "") + ":30");
							if (seltime == t)
								txtOrderTimeOutput.Text = t.ToString().Replace(",5", "") + ":30";
						}
						else
						{
							txtOrderTimeOutput.Items.Add(t.ToString() + ":00");
							if (seltime == t)
								txtOrderTimeOutput.Text = t.ToString() + ":00";
						}
					t += 0.5;
					}


					txtOrderTimeOutput.Text = order.Timeout;
					// Клиент
					if (this.order.Client != null)
					{
						lblClientName.Text = order.Client.Name;
					}
					else
					{
						lblClientName.Text = "";
					}

					//скидка
					if (order.Discont != null)
						lblOrderDiscont.Text = order.Discont.Discserv.ToString() + "%";
					// предоплата
					lblOrderAdvancedPayment.Text = order.AdvancedPayment.ToString();
					// обрезка
					switch (order.Crop)
					{
						case 1:
							{
								radioCrop1.Checked = true;
								radioCrop2.Checked = false;
								radioCrop3.Checked = false;
								break;
							}
						case 2:
							{
								radioCrop2.Checked = true;
								radioCrop1.Checked = false;
								radioCrop3.Checked = false;
								break;
							}
						case 3:
							{
								radioCrop3.Checked = true;
								radioCrop1.Checked = false;
								radioCrop2.Checked = false;
								break;
							}
					}
					//формат бумаги
					switch (order.Type)
					{
						case 1:
							{
								radioPapperType1.Checked = true;
								radioPapperType2.Checked = false;
								break;
							}
						case 2:
							{
								radioPapperType2.Checked = true;
								radioPapperType1.Checked = false;
								break;
							}
					}
					//загружаем таблицу
					if (order.OrderBody != null)
					{
						tblPrintOrder = order.OrderBody;
						gridFormats.Rows.Count = 1;
						for (int i = 0; i < tblPrintOrder.Rows.Count; i++)
						{
							object[] r = new object[9];
							r[0] = tblPrintOrder.Rows[i][0];
							r[1] = tblPrintOrder.Rows[i][1];
							r[2] = tblPrintOrder.Rows[i][2];
							r[3] = tblPrintOrder.Rows[i][3];
							r[4] = tblPrintOrder.Rows[i][4];
							gridFormats.AddItem("\t" + r[0] + "\t" + r[1] + "\t" + r[2] + "\t" + r[3] + "\t" + r[4]);
						}
						UpdateFormatTable();
					}


				}
				else
				{
					order = new OrderInfo(db_connection);
					this.order.Orderno = num.NewOrderNum(db_connection, usr);
					//Номер
					lblOrderNo.Text = order.Orderno;
					// дата поступления заказа
					order.Datein = DateTime.Now.ToShortDateString();
					order.Timein = DateTime.Now.ToShortTimeString();
					lblDateOrderInput.Text = order.Datein + " " + order.Timein;
					// дата выдачи заказа

					txtOrderDateOutput.Value = DateTime.Now;
					if (DateTime.Now.AddHours(prop.Time_for_output).Hour > prop.Time_end_work)
						txtOrderDateOutput.Value = DateTime.Now.AddDays(1);
					else
						txtOrderDateOutput.Value = DateTime.Now.AddHours(prop.Time_for_output);

					double seltime = txtOrderDateOutput.Value.Hour;
					if ((txtOrderDateOutput.Value.Minute > 15) && (txtOrderDateOutput.Value.Minute < 30))
						seltime += 0.5;
					if (txtOrderDateOutput.Value.Minute > 30)
						seltime += 1;
					if (seltime < prop.Time_begin_work)
						seltime = prop.Time_begin_work;
					if (seltime > prop.Time_end_work)
						seltime = prop.Time_begin_work;

					double t = prop.Time_begin_work;
					while (t <= double.Parse(prop.Time_end_work.ToString()))
					{
						if (t.ToString().Replace(".", ",").LastIndexOf(",5") > 0)
						{
							txtOrderTimeOutput.Items.Add(t.ToString().Replace(".", ",").Replace(",5", "") + ":30");
							if (seltime == t)
								txtOrderTimeOutput.Text = t.ToString().Replace(".", ",").Replace(",5", "") + ":30";
						}
						else
						{
							txtOrderTimeOutput.Items.Add(t.ToString() + ":00");
							if (seltime == t)
								txtOrderTimeOutput.Text = t.ToString() + ":00";
						}

						t += 0.5;
					}
					order.Timeout = txtOrderTimeOutput.Text;


					// Клиент
					if (this.order.Client != null)
					{
						lblClientName.Text = order.Client.Name;
					}
					else
					{
						lblClientName.Text = "";
					}
					//скидка
					if (order.Discont != null)
						lblOrderDiscont.Text = order.Discont.Discserv.ToString() + "%";
					// предоплата
					lblOrderAdvancedPayment.Text = order.AdvancedPayment.ToString();
					// обрезка
					radioCrop1.Checked = false;
					radioCrop2.Checked = false;
					radioCrop3.Checked = false;
					//формат бумаги
					radioPapperType1.Checked = false;
					radioPapperType2.Checked = false;
					ReBildFormatTable();
				}
				tmr.Start();
			}
		}

		// Основной пересчет сводит вместе таблицы форматов и услуг
		private void RebildTable()
		{
			tblOrder.Rows.Clear();

			// Перегружаем данные по форматам
			for (int i = 0; i < tblPrintOrder.Rows.Count; i++)
			{
				if (decimal.Parse(tblPrintOrder.Rows[i][2].ToString()) > 0)
				{
					object[] r = new object[13];
					r[1] = tblPrintOrder.Rows[i][1]; // name
					r[2] = tblPrintOrder.Rows[i][2]; // count
					if (order.Client != null)
					{
                        r[3] = decimal.Parse(GetPrice(tblPrintOrder.Rows[i][4].ToString(), decimal.Parse(r[2].ToString())).ToString()); // цена
						r[4] = (decimal)r[2] * (decimal)r[3]; // сумма
					}
					else
					{
						r[3] = 0; // цена
						r[4] = 0; // сумма
					}
					r[6] = tblPrintOrder.Rows[i][3]; // folder
					r[5] = tblPrintOrder.Rows[i][4]; // id
					r[8] = false; // признак услуги
					r[7] = "+";
				    r[11] = usr.Name;
				    r[12] = "Найдено автоматически";
					tblOrder.Rows.Add(r);
				}
			}

			// Перегружаем данные по услугам
			for (int i = 0; i < tblServOrder.Rows.Count; i++)
			{
				if (decimal.Parse(tblServOrder.Rows[i][3].ToString()) > 0)
				{
					object[] r = new object[13];
					r[1] = tblServOrder.Rows[i][1]; // name
					r[2] = decimal.Parse(tblServOrder.Rows[i][3].ToString()); // count
					if (order.Client != null)
					{
						r[3] = decimal.Parse(GetPrice(tblServOrder.Rows[i][0].ToString(), decimal.Parse(r[2].ToString())).ToString()); // цена
						r[4] = (decimal)r[2] * (decimal)r[3]; // сумма
					}
					else
					{
						r[3] = 0; // цена
						r[4] = 0; // сумма
					}
					r[9] = tblServOrder.Rows[i][4]; // content
					r[5] = tblServOrder.Rows[i][0]; // id
					r[8] = true; // признак услуги
					r[10] = tblServOrder.Rows[i][5]; // код услуги, добавленной вручную
					r[7] = "+";
				    r[11] = tblServOrder.Rows[i][6];
				    r[12] = tblServOrder.Rows[i][7];
					tblOrder.Rows.Add(r);
				}
			}

			// пересуммируем строки по Id
			DataTable _t = new DataTable("tmp");
			_t.Columns.Add("kol", Type.GetType("System.Decimal"));		// 0
			_t.Columns.Add("prc", Type.GetType("System.Decimal"));		// 1
			_t.Columns.Add("sum", Type.GetType("System.Decimal"));		// 2
			_t.Columns.Add("id", Type.GetType("System.String"));		// 3

			for (int i = 0; i < tblOrder.Rows.Count; i++)
			{
				bool found = false;
				for (int j = 0; j < _t.Rows.Count; j++)
				{
					if (tblOrder.Rows[i][5].ToString() == _t.Rows[j][3].ToString())
					{
						_t.Rows[j][0] = decimal.Parse(_t.Rows[j][0].ToString()) + decimal.Parse(tblOrder.Rows[i][2].ToString());
						found = true;
					}
				}
				if (!found)
				{
					_t.Rows.Add(new object[4] { decimal.Parse(tblOrder.Rows[i][2].ToString()), 0, 0, tblOrder.Rows[i][5].ToString() });
				}
			}

			for (int i = 0; i < _t.Rows.Count; i++)
			{
				_t.Rows[i][1] = decimal.Parse(GetPrice(_t.Rows[i][3].ToString(), decimal.Parse(_t.Rows[i][0].ToString())).ToString()); // цена
				//_t.Rows[i][2] = decimal.Parse(_t.Rows[i][1].ToString()) * decimal.Parse(_t.Rows[i][0].ToString());
			}

			for (int i = 0; i < tblOrder.Rows.Count; i++)
			{
				for (int j = 0; j < _t.Rows.Count; j++)
				{
					if (tblOrder.Rows[i][5].ToString() == _t.Rows[j][3].ToString())
					{
						tblOrder.Rows[i][3] = _t.Rows[j][1];
						tblOrder.Rows[i][4] = decimal.Parse(_t.Rows[j][1].ToString()) * decimal.Parse(tblOrder.Rows[i][2].ToString());
					}
				}
			}

			// Обновляем общую таблицу
			gridOrder.Rows.Count = 1;
			for (int i = 0; i < tblOrder.Rows.Count; i++)
			{
				object[] rw = new object[13];
				rw[0] = tblOrder.Rows[i][7];
				rw[1] = tblOrder.Rows[i][1];
				rw[2] = tblOrder.Rows[i][2];
				rw[3] = tblOrder.Rows[i][3];
				rw[4] = tblOrder.Rows[i][4];
				rw[6] = tblOrder.Rows[i][3];
				rw[8] = tblOrder.Rows[i][8];
				rw[10] = tblOrder.Rows[i][10];
                rw[11] = tblOrder.Rows[i][11];
                rw[12] = tblOrder.Rows[i][12];
				gridOrder.AddItem(rw);
			}

			order.OrderBody = tblOrder;

			ReCalcOrder();
		}


		// Создает папки форматов при первом запуске
		private void ReBildFormatTable()
		{
			SqlCommand db_command = new SqlCommand();
			db_command.Connection = db_connection;
			db_command.CommandText = "SELECT [id_good] ,[name] ,[description] ,[folder] ,[type] ,[checked] FROM [vwWizardStep1Good] WHERE [folder] <> ''";
			SqlDataReader db_reader = db_command.ExecuteReader();
			// очищаем таблицу
			gridFormats.Rows.Count = 1;
			// проверяем значение пути к папкам для печати и обработки
			while ((prop.Dir_edit == "") || (prop.Dir_print == ""))
			{
				MessageBox.Show("Не определены каталоги для заказов на печатьи на обработку! Проверьте настройки программы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				frmOptions fOption = new frmOptions();
				fOption.ShowDialog();
			}

			// Об'ект папки для печати
			if (!Directory.Exists(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno))
				Directory.CreateDirectory(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno);
			DirectoryInfo DirPrint = new DirectoryInfo(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno);

			if (!Directory.Exists(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno))
				Directory.CreateDirectory(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno);
			DirectoryInfo DirEdit = new DirectoryInfo(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno);

			while (db_reader.Read())
			{
				string _tmp_id_good;
				string _tmp_name;
				string _tmp_description;
				string _tmp_folder;
				string _tmp_type;
				bool _tmp_checked;

				if (!db_reader.IsDBNull(0))
					_tmp_id_good = db_reader.GetString(0);
				else
					_tmp_id_good = "0";
				if (!db_reader.IsDBNull(1))
					_tmp_name = db_reader.GetString(1);
				else
					_tmp_name = "+";
				if (!db_reader.IsDBNull(2))
					_tmp_description = db_reader.GetString(2);
				else
					_tmp_description = "";
				if (!db_reader.IsDBNull(3))
					_tmp_folder = db_reader.GetString(3);
				else
					_tmp_folder = _tmp_name.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("?", "").Replace("*", "");
				if (!db_reader.IsDBNull(4))
					_tmp_type = db_reader.GetString(4);
				else
					_tmp_type = "";
				if (!db_reader.IsDBNull(5))
					_tmp_checked = db_reader.GetBoolean(5);
				else
					_tmp_checked = false;

				// создаем папку с именем формата если он отмечена галкой
				if (_tmp_checked)
				{
					// если папка для печати с форматом не существует, то создаем ее
					if (!Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
					{
						Directory.CreateDirectory(DirPrint.FullName + "\\" + _tmp_folder);
					}

					// если папка для обработки с форматом не существует, то создаем ее
					if (!Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
					{
						Directory.CreateDirectory(DirEdit.FullName + "\\" + _tmp_folder);
					}
				}
				else
				{
					// для печати: если существует, то проверяем, если она пустая, то удаляем ее, если не пустая, то спрашиваем
					if (Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
					{
						DirectoryInfo tmpDir = new DirectoryInfo(DirPrint.FullName + "\\" + _tmp_folder);
						if (fso.Directory_Size(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true) == 0)
						{
							try
							{
								if (!prop.DenyDelete)
								{
									if (prop.QueryForDelete)
									{
										if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
											tmpDir.Delete(true);
									}
									else
									{
										tmpDir.Delete(true);
									}
								}
							}
							catch(Exception ex)
							{
								ErrorNfo.WriteErrorInfo(ex);
							}
						}
						else
						{
							if (MessageBox.Show("Внимание! Вы отменили в заказе формат " + _tmp_name.Trim() + ", направленный на печать, но в нем находятся файлы (" + fso.Count_of_files(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true).ToString() + " шт.)!\nУдалить?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								try
								{
									if (!prop.DenyDelete)
									{
										if (prop.QueryForDelete)
										{
											if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
												tmpDir.Delete(true);
										}
										else
										{
											tmpDir.Delete(true);
										}
									}
								}
								catch(Exception ex)
								{
									ErrorNfo.WriteErrorInfo(ex);
								}
							}
							else
							{
								_tmp_checked = true;
							}
						}
					}

					// для обработки: если существует, то проверяем, если она пустая, то удаляем ее, если не пустая, то спрашиваем
					if (Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
					{
						DirectoryInfo tmpDir = new DirectoryInfo(DirEdit.FullName + "\\" + _tmp_folder);
						if (fso.Directory_Size(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true) == 0)
						{
							try
							{
								if (!prop.DenyDelete)
								{
									if (prop.QueryForDelete)
									{
										if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
											tmpDir.Delete(true);
									}
									else
									{
										tmpDir.Delete(true);
									}
								}
							}
							catch(Exception ex)
							{
								ErrorNfo.WriteErrorInfo(ex);
							}
						}
						else
						{
							if (MessageBox.Show("Внимание! Вы отменили в заказе формат " + _tmp_name.Trim() + ", направленный на обработку, но в нем находятся файлы (" + fso.Count_of_files(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true).ToString() + " шт.)!\nУдалить?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								try
								{
									if (!prop.DenyDelete)
									{
										if (prop.QueryForDelete)
										{
											if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
												tmpDir.Delete(true);
										}
										else
										{
											tmpDir.Delete(true);
										}
									}
								}
								catch(Exception ex)
								{
									ErrorNfo.WriteErrorInfo(ex);
								}
							}
							else
							{
								_tmp_checked = true;
							}
						}
					}
				}

				// выводим строку с галкой, форматом и количеством фотографий в папке
				int tmp_count_files = 0;
				if (Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
					tmp_count_files += fso.Count_of_files(DirPrint.FullName + "\\" + _tmp_folder, prop.List_of_files, prop.Search_SubDir, true);
				if (Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
					tmp_count_files += fso.Count_of_files(DirEdit.FullName + "\\" + _tmp_folder, prop.List_of_files, prop.Search_SubDir, true);

				gridFormats.AddItem("\t" + _tmp_checked + "\t" + _tmp_name + "\t" + tmp_count_files.ToString() + "\t" + _tmp_folder + "\t" + _tmp_id_good);
			}
			db_reader.Close();

			// Перегружает из грида на форме данные о фрматах во внутреннюю таблицу
			ReloadFormatTable();
		}

		// Обновляет папки форматов, работает по таймеру
		private void UpdateFormatTable()
		{
			tmr.Stop();

			while ((prop.Dir_edit == "") || (prop.Dir_print == ""))
			{
				MessageBox.Show("Не определены каталоги для заказов на печатьи на обработку! Проверьте настройки программы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				frmOptions fOption = new frmOptions();
				fOption.ShowDialog();
			}

			// Об'ект папки для печати
			if (!Directory.Exists(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno));
				Directory.CreateDirectory(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno);
			DirectoryInfo DirPrint = new DirectoryInfo(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno);

			if (!Directory.Exists(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno));
				Directory.CreateDirectory(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno);
			DirectoryInfo DirEdit = new DirectoryInfo(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + order.Orderno);

			for (int i = 1; i < gridFormats.Rows.Count; i++)
			{
				string _tmp_name = gridFormats.GetData(i, 2).ToString();
				string _tmp_folder = gridFormats.GetData(i, 4).ToString();
				if (_tmp_folder == "")
					_tmp_folder = _tmp_name.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("?", "").Replace("*", "");
				bool _tmp_checked = Convert.ToBoolean(gridFormats.GetData(i, 1));
				// создаем папку с именем формата если он отмечена галкой
				if (_tmp_checked)
				{
					// если папка для печати с форматом не существует, то создаем ее
					if (!Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
					{
						Directory.CreateDirectory(DirPrint.FullName + "\\" + _tmp_folder);
					}

					// если папка для обработки с форматом не существует, то создаем ее
					if (!Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
					{
						Directory.CreateDirectory(DirEdit.FullName + "\\" + _tmp_folder);
					}
				}
				else
				{
					// для печати: если существует, то проверяем, если она пустая, то удаляем ее, если не пустая, то спрашиваем
					if (Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
					{
						DirectoryInfo tmpDir = new DirectoryInfo(DirPrint.FullName + "\\" + _tmp_folder);
						if (fso.Directory_Size(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true) == 0)
						{
							try
							{
								if (!prop.DenyDelete)
								{
									if (prop.QueryForDelete)
									{
										if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
											tmpDir.Delete(true);
									}
									else
									{
										tmpDir.Delete(true);
									}
								}
							}
							catch(Exception ex)
							{
								ErrorNfo.WriteErrorInfo(ex);
							}
						}
						else
						{
							if (MessageBox.Show("Внимание! Вы отменили в заказе формат " + _tmp_name.Trim() + ", направленный на печать, но в нем находятся файлы (" + fso.Count_of_files(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true).ToString() + " шт.)!\nУдалить?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								try
								{
									if (!prop.DenyDelete)
									{
										if (prop.QueryForDelete)
										{
											if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
												tmpDir.Delete(true);
										}
										else
										{
											tmpDir.Delete(true);
										}
									}
								}
								catch(Exception ex)
								{
									ErrorNfo.WriteErrorInfo(ex);
								}
							}
							else
							{
								_tmp_checked = true;
							}
						}
					}

					// для обработки: если существует, то проверяем, если она пустая, то удаляем ее, если не пустая, то спрашиваем
					if (Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
					{
						DirectoryInfo tmpDir = new DirectoryInfo(DirEdit.FullName + "\\" + _tmp_folder);
						if (fso.Directory_Size(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true) == 0)
						{
							try
							{
								if (!prop.DenyDelete)
								{
									if (prop.QueryForDelete)
									{
										if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
											tmpDir.Delete(true);
									}
									else
									{
										tmpDir.Delete(true);
									}
								}
							}
							catch(Exception ex)
							{
								ErrorNfo.WriteErrorInfo(ex);
							}
						}
						else
						{
							if (MessageBox.Show("Внимание! Вы отменили в заказе формат " + _tmp_name.Trim() + ", направленный на обработку, но в нем находятся файлы (" + fso.Count_of_files(tmpDir.FullName, prop.List_of_files, prop.Search_SubDir, true).ToString() + " шт.)!\nУдалить?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								try
								{
									if (!prop.DenyDelete)
									{
										if (prop.QueryForDelete)
										{
											if (MessageBox.Show("Удалить папку " + tmpDir.FullName + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
												tmpDir.Delete(true);
										}
										else
										{
											tmpDir.Delete(true);
										}
									}
								}
								catch(Exception ex)
								{
									ErrorNfo.WriteErrorInfo(ex);
								}
							}
							else
							{
								_tmp_checked = true;
							}
						}
					}
				}

				// выводим строку с галкой, форматом и количеством фотографий в папке
				int tmp_count_files = 0;
				if (Directory.Exists(DirPrint.FullName + "\\" + _tmp_folder))
					tmp_count_files += fso.Count_of_files(DirPrint.FullName + "\\" + _tmp_folder, prop.List_of_files, prop.Search_SubDir, true);
				if (Directory.Exists(DirEdit.FullName + "\\" + _tmp_folder))
					tmp_count_files += fso.Count_of_files(DirEdit.FullName + "\\" + _tmp_folder, prop.List_of_files, prop.Search_SubDir, true);

				gridFormats.SetData(i, 1, _tmp_checked);
				gridFormats.SetData(i, 3, tmp_count_files);
			}

			// Перегружает из грида на форме данные о фрматах во внутреннюю таблицу
			ReloadFormatTable();

			// Пересчитываем основную таблицу
			RebildTable();

			// Определяется место назначения
			UpdateDistanation();

			// Доступность кнопки завершения
			CanFinish();

			// запускаем таймер дальше
			tmr.Start();
		}

		// Перегружает из грида на форме данные о фрматах во внутреннюю таблицу
		private void ReloadFormatTable()
		{
			tblPrintOrder.Rows.Clear();
			for (int i = 1; i < gridFormats.Rows.Count; i++)
			{
				object[] row = new object[5];
				row[0] = gridFormats.GetData(i, 1);
				row[1] = gridFormats.GetData(i, 2);
				row[2] = gridFormats.GetData(i, 3);
				row[3] = gridFormats.GetData(i, 4);
				row[4] = gridFormats.GetData(i, 5);
				tblPrintOrder.Rows.Add(row);
			}
		}

		// Добавляет строку в таблицу услуг
		private void AddToServTable(string id, string name, string sname, decimal count, List<string> content)
		{
			tmr.Stop();
			object[] row = new object[8];
			row[0] = id;
			row[1] = name;
			row[2] = sname;
			row[3] = count;
			row[4] = content;
			row[5] = System.Guid.NewGuid().ToString();
		    row[6] = usr.Name;
            row[7] = "";
            for (int i = 0; i < content.Count; i++)
                row[7] = content[i].Replace("\r", "").Replace("\n", " ").Trim();
            if (row[7].ToString().Length > 1020)
                row[7].ToString().Substring(0, 1020);
            tblServOrder.Rows.Add(row);
			tmr.Start();
		}

		// Удаляет из указанной позиции строку
		private void DelFromServTable(int row)
		{
			tmr.Stop();
			try
			{
				string tmp = tblOrder.Rows[row][10].ToString();
				if (bool.Parse(tblOrder.Rows[row][8].ToString()))
				{
					if (MessageBox.Show("Удалить: " + tblOrder.Rows[row][1].ToString().Trim() + "?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
					{
						tblOrder.Rows.RemoveAt(row);
						for (int i = 0; i < tblServOrder.Rows.Count; i++)
						{
							if (tblServOrder.Rows[i][5].ToString() == tmp)
							{
								tblServOrder.Rows.RemoveAt(i);
							}
						}

						RebildTable();
					}
				}
				else
				{
					MessageBox.Show("Нельзя удалить эту строку!\nУдалите необходимые файлы в папках форматов соответствующего заказа!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}
			catch(Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
			}
			tmr.Start();
		}


		// Привязка кнопок быстрого вызова
		private void FillQuickButtons()
		{
			btnQuickServ1.Text = prop.Qbtn01_stext.Trim();
			btnQuickServ2.Text = prop.Qbtn02_stext.Trim();
			btnQuickServ3.Text = prop.Qbtn03_stext.Trim();
			btnQuickServ4.Text = prop.Qbtn04_stext.Trim();
			btnQuickServ5.Text = prop.Qbtn05_stext.Trim();
			btnQuickServ6.Text = prop.Qbtn06_stext.Trim();
			btnQuickServ7.Text = prop.Qbtn07_stext.Trim();
			btnQuickServ8.Text = prop.Qbtn08_stext.Trim();
			btnQuickServ9.Text = prop.Qbtn09_stext.Trim();
			btnQuickServ10.Text = prop.Qbtn10_stext.Trim();

			if (prop.Qbtn01_id != "0")
				btnQuickServ1.Enabled = true;
			else
				btnQuickServ1.Enabled = false;

            if (prop.Qbtn02_id != "0")
				btnQuickServ2.Enabled = true;
			else
				btnQuickServ2.Enabled = false;

            if (prop.Qbtn03_id != "0")
				btnQuickServ3.Enabled = true;
			else
				btnQuickServ3.Enabled = false;

            if (prop.Qbtn04_id != "0")
				btnQuickServ4.Enabled = true;
			else
				btnQuickServ4.Enabled = false;

            if (prop.Qbtn05_id != "0")
				btnQuickServ5.Enabled = true;
			else
				btnQuickServ5.Enabled = false;

            if (prop.Qbtn06_id != "0")
				btnQuickServ6.Enabled = true;
			else
				btnQuickServ6.Enabled = false;

            if (prop.Qbtn07_id != "0")
				btnQuickServ7.Enabled = true;
			else
				btnQuickServ7.Enabled = false;

            if (prop.Qbtn08_id != "0")
				btnQuickServ8.Enabled = true;
			else
				btnQuickServ8.Enabled = false;

            if (prop.Qbtn09_id != "0")
				btnQuickServ9.Enabled = true;
			else
				btnQuickServ9.Enabled = false;

            if (prop.Qbtn10_id != "0")
				btnQuickServ10.Enabled = true;
			else
				btnQuickServ10.Enabled = false;

		}


		// Получить стоимость по коду
		private decimal GetPrice(string id, decimal cnt)
		{
			if (order.Client != null)
			{
				string tmp_id_good = id;
				int tmp_id_client_category = int.Parse(order.Client.Id_category.ToString());
				decimal pr = 0;
				bool err = false;
				if (cnt < 0) cnt *= -1;
				//SqlCommand db_command = new SqlCommand("SELECT [id_good], [id_category], [amount] FROM [vwPriceFull] WHERE [id_good] = '" + tmp_id_good + "' AND [id_category] = " + tmp_id_client_category.ToString(), db_connection);
				SqlCommand db_command = new SqlCommand("spAdvPrice", db_connection);
				db_command.Parameters.Add(new SqlParameter("@id_good", id));
				db_command.Parameters.Add(new SqlParameter("@id_category", tmp_id_client_category));
				db_command.Parameters.Add(new SqlParameter("@threshold", cnt));
                db_command.Parameters.Add(new SqlParameter("@ondate", DateTime.Parse(order.Datein)));
                db_command.CommandType = CommandType.StoredProcedure;
				decimal price;
				try
				{
					price = (decimal) db_command.ExecuteScalar();
					{
						if (price > 0)
						{
							pr = price;
						}
						else
						{
							SqlCommand tmp =
								new SqlCommand("SELECT CAST([zero] as BIT) AS [zero] FROM [good] WHERE [id_good] = '" + tmp_id_good + "'",
								               db_connection);
							bool z = (bool) tmp.ExecuteScalar();
							if (!z)
							{
                                MessageBox.Show("Внимание! Не найдена цена в прайсе!\nПроверьте заполние прайса!", "Ошибка получения цены",
								                MessageBoxButtons.OK, MessageBoxIcon.Error);
								err = true;
							}
							else
							{
								err = false;
							}
						}
					}
				}
				catch (Exception ex)
				{
                    SqlCommand tmp =
                        new SqlCommand("SELECT CAST([zero] as BIT) AS [zero] FROM [good] WHERE [id_good] = '" + tmp_id_good + "'",
                                       db_connection);
                    bool z = (bool)tmp.ExecuteScalar();
                    if (!z)
                    {
                        MessageBox.Show("Внимание! Не найдена цена в прайсе!\nПроверьте заполние прайса!", "Ошибка получения цены",
                                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                        err = true;
                    }
                    else
                    {
                        err = false;
                    }
                }
				if (!err)
					return pr;
				else
					return 0;
			}
			else
			{
				return 0;
			}
		}

		// Пересчет заказа
		private void ReCalcOrder()
		{
			try
			{
				if (this.order != null)
				{

					decimal sum = 0;

					for (int i = 0; i < this.order.OrderBody.Rows.Count; i++)
					{
						if (this.order.OrderBody.Rows[i][7].ToString() == "+")
							sum += decimal.Parse(this.order.OrderBody.Rows[i][4].ToString());
						if (this.order.OrderBody.Rows[i][7].ToString() == "-")
							sum -= decimal.Parse(this.order.OrderBody.Rows[i][4].ToString());
					}
					lblOrderSum.Text = decimal.Round(sum, 2).ToString();
					if (this.order.Discont != null)
					{
						lblOrderDiscont.Text = this.order.Discont.Discserv.ToString() + "%";
						sum -= sum*(this.order.Discont.Discserv/100);
					}
					switch (prop.ModelRound)
					{
						case 0:
							{
								break;
							}
						case 1:
							{
								if (((sum - ((int)sum)) <= (decimal)0.25) && ((sum - ((int)sum)) > 0))
									sum = ((int)sum);
								else if (((sum - ((int)sum)) > (decimal)0.25) && ((sum - ((int)sum)) <= (decimal)0.75))
									sum = ((int)sum) + (decimal)0.5;
								else if ((sum - ((int)sum)) > (decimal)0.75)
									sum = ((int)sum) + 1;
								break;
							}
						case 2:
							{
								if (((sum - ((int)sum)) <= (decimal)0.45) && ((sum - ((int)sum)) > 0))
									sum = ((int)sum);
								else if (((sum - ((int)sum)) > (decimal)0.45) && ((sum - ((int)sum)) <= (decimal)0.95))
									sum = ((int)sum) + (decimal)0.5;
								else if ((sum - ((int)sum)) > (decimal)0.95)
									sum = ((int)sum) + 1;
								break;
							}
						case 3:
							{
								if (((sum - ((int)sum)) <= (decimal)0.15) && ((sum - ((int)sum)) > 0))
									sum = (int)sum;
								else if (((sum - ((int)sum)) > (decimal)0.15) && ((sum - ((int)sum)) <= (decimal)0.65))
									sum = ((int)sum) + (decimal)0.5;
								else if ((sum - ((int)sum)) > (decimal)0.65)
									sum = ((int)sum) + 1;
								break;
							}
						case 4:
							{
								if (((sum - ((int)sum)) <= (decimal)0.49) && ((sum - ((int)sum)) > 0))
									sum = (int)sum;
								else if ((sum - ((int)sum)) > (decimal)0.49)
									sum = ((int)sum) + 1;
								break;
							}
						case 5:
							{
								if (((sum - ((int)sum)) <= (decimal)0.5) && ((sum - ((int)sum)) > 0))
									sum = ((int)sum) + (decimal)0.5;
								else if ((sum - ((int)sum)) > (decimal)0.5)
									sum = ((int)sum) + 1;
								break;
							}
					}
					order.Order_price = sum;
					lblOrderAdvancedPayment.Text = decimal.Round(this.order.AdvancedPayment, 2).ToString();
					sum -= this.order.AdvancedPayment;
					sum -= this.order.FinalPayment;
					//if (sum < 0)
					//	sum = 0;
					lblOrderSumFinal.Text = decimal.Round(prop.DoRound(sum), 2).ToString();
					decimal tmp = this.order.AdvancedPayment + this.order.FinalPayment;
					lblTotalPayment.Text = tmp.ToString();
				}
			}
			catch(Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
			}
		}

		// Определяется доступность кнопки завершения
		private void CanFinish()
		{
			bool ok = false;

			if (order != null)
			{
				if (order.Client == null)
					ok = false;
				else if (order.OrderBody == null)
					ok = false;
				else if (order.Crop == 0)
					ok = false;
				else if (order.Type == 0)
					ok = false;
				else if (order.OrderBody.Rows.Count == 0)
					ok = false;
				else
					ok = true;
			}
			else
			{
				ok = false;
			}

			if (ok)
			{
				btnOK.Enabled = true;
				if (!Momental.Visible)
					btnBeznal.Enabled = true;
				else
					btnBeznal.Enabled = false;
			}
			else
			{
				btnOK.Enabled = false;
                btnBeznal.Enabled = false;
			}
		}


		private void button13_Click(object sender, EventArgs e)
		{
			RebildTable();
		}

		private void btnSelectClient_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			frmSelectClient fClient = new frmSelectClient(db_connection, usr);
			fClient.ShowDialog();
			if (fClient.DialogResult == DialogResult.OK)
			{
				if ((fClient.client.Category_name.Trim().ToLower() == "терминал") ||
					(fClient.client.Category_name.Trim().ToLower() == "фототерминал"))
				{
					MessageBox.Show("Внимание! Выбран клиент фототерминала");
				}
				else
				{
					this.order.Client = fClient.client;
					lblClientName.Text = this.order.Client.Name;
					RebildTable();
				}
			}
			tmr.Start();
		}

		private void btnReCalc_Click(object sender, EventArgs e)
		{
			//ReCalcOrder();
			UpdateFormatTable();
		}

		private void btnAdvancedPayment_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			try
			{
				frmAdvancePayment fPayment = new frmAdvancePayment(double.Parse(lblOrderSum.Text));
				fPayment.ShowDialog();
				this.order.AdvancedPayment = (decimal)fPayment.Payment;
				ReCalcOrder();
			}
			catch(Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
			}
			tmr.Start();
		}

		private void btnDescont_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			frmGetDiscont fGetDiscont = new frmGetDiscont();
			fGetDiscont.db_connection = db_connection;
			fGetDiscont.ShowDialog();
			if (fGetDiscont.DialogResult == DialogResult.OK)
			{
				if (fGetDiscont.discont.Code_dcard != "")
				{
					this.order.Discont = fGetDiscont.discont;
				}
				else
				{
					MessageBox.Show("Дисконтная карта не найдена в базе!", "Скидка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					this.order.Discont = new DiscontInfo();
				}
			}
			ReCalcOrder();
			tmr.Start();
		}

		// Добавляет услугу из списка
		private void SelectService()
		{
			tmr.Stop();
			frmSelectService fSelServ = new frmSelectService(db_connection, true, this.order.Orderno);
			fSelServ.ShowDialog();
			if (fSelServ.DialogResult == DialogResult.OK)
			{
				AddToServTable(fSelServ.id_serv, fSelServ.text_serv, fSelServ.stext_serv, fSelServ.Content_count, fSelServ.Content);
			}
			tmr.Start();
		}

		// Добавляет услугу из быстрой кнопки
		private void SelectQuickService(string id)
		{
			tmr.Stop();
			List<string> _content = new List<string>();
			decimal _content_count = 0;
			string _id_serv = "0";
			string _text_serv = "";
			string _stext_serv = "";

			SqlCommand db_command = new SqlCommand("SELECT [id_good], [guid], [name], [description], [type], [apply_form], [sign] FROM [vwGoodList] WHERE [id_good] = '" + id + "'", db_connection);
			SqlDataReader db_reader = db_command.ExecuteReader();
			if (db_reader.Read())
			{
				if (!db_reader.IsDBNull(5))
				{
					switch (db_reader.GetString(5))
					{
						case "00001":
							{
								frmApplyService fApply = new frmApplyService(order.Orderno);
								fApply.ShowDialog();
								if (fApply.DialogResult == DialogResult.OK)
								{
									_content = fApply.Content;
									_content_count = (decimal)_content.Count;
								}
								else
								{
									frmQueryCount fCount = new frmQueryCount();
									fCount.ShowDialog();
									if (fCount.DialogResult == DialogResult.OK)
									{
										_content_count = fCount.Count;
									}
									else
									{
										_content_count = 0;
									}
								}
								fApply.Close();
								break;
							}
						case "00002":
							{
								frmSelectFrame fApply = new frmSelectFrame();
								fApply.ShowDialog();
								if (fApply.DialogResult == DialogResult.OK)
								{
									_content = fApply.Content;
									_content_count = fApply.Content_count;
								}
								else
								{
									frmQueryCount fCount = new frmQueryCount();
									fCount.ShowDialog();
									if (fCount.DialogResult == DialogResult.OK)
									{
										_content_count = fCount.Count;
									}
									else
									{
										_content_count = 0;
									}
								}
								fApply.Close();
								break;
							}
						case "00003":
							{
								frmQueryFrameParam fApply = new frmQueryFrameParam();
								fApply.ShowDialog();
								if (fApply.DialogResult == DialogResult.OK)
								{
									_content.Add("\r\nИнформация к заказу:\r\nФайл: " + fApply.txtFile.Text + "\r\nШирина: " + fApply.txtW.Text + "\r\nВысота: " + fApply.txtH.Text + "\r\nПлощадь: " + fApply.txtS.Text + "\r\nКомментарий: \r\n" + fApply.txtComment.Text);
									if(prop.Round3)
										_content_count = decimal.Round(decimal.Parse(fApply.txtS.Text),3);
									else
										_content_count = decimal.Round(decimal.Parse(fApply.txtS.Text), 2);
								}
								else
								{
									frmQueryCount fCount = new frmQueryCount();
									fCount.ShowDialog();
									if (fCount.DialogResult == DialogResult.OK)
									{
										_content_count = fCount.Count;
									}
									else
									{
										_content_count = 0;
									}
								}
								fApply.Close();
								break;
							}
						default:
							{
								frmQueryCount fCount = new frmQueryCount();
								fCount.ShowDialog();
								if (fCount.DialogResult == DialogResult.OK)
								{
									_content_count = fCount.Count;
								}
								else
								{
									_content_count = 0;
								}
								break;
							}
					}
				}
				else
				{
					frmQueryCount fCount = new frmQueryCount();
					fCount.ShowDialog();
					if (fCount.DialogResult == DialogResult.OK)
					{
						_content_count = fCount.Count;
					}
					else
					{
						_content_count = 0;
					}
				}
				if (!db_reader.IsDBNull(0))
					_id_serv = db_reader.GetString(0);
				if (!db_reader.IsDBNull(2))
					_text_serv = db_reader.GetString(2);
				if (!db_reader.IsDBNull(6))
					_stext_serv = db_reader.GetString(6);
			}
			if ((_id_serv != "0") && (_content_count > 0))
			{
				AddToServTable(_id_serv, _text_serv, _stext_serv, _content_count, _content);
			}

			db_reader.Close();

			tmr.Start();

		}

		// Заполняет данные по обрезре и типе бумаги
		private void UpdateRadioButtons()
		{
			if (radioCrop1.Checked)
				order.Crop = 1;
			if (radioCrop2.Checked)
				order.Crop = 2;
			if (radioCrop3.Checked)
				order.Crop = 3;

			if (radioPapperType1.Checked)
				order.Type = 1;
			if (radioPapperType2.Checked)
				order.Type = 2;
		}

		// Определяет назначение
		private void UpdateDistanation()
		{
			string t = "000100";

			for (int i = 0; i < this.order.OrderBody.Rows.Count; i++)
			{
				if ((bool)this.order.OrderBody.Rows[i][8])
				{
                    if (this.order.OrderBody.Rows[i][5].ToString() != "")
					{
						SqlCommand tmp_cmd = new SqlCommand("SELECT RTRIM([type]) as [type] FROM [vwGoodFull] WHERE [id_good] = '" + this.order.OrderBody.Rows[i][5].ToString() + "'", db_connection);
						SqlDataReader tmp_rdr = tmp_cmd.ExecuteReader();
						if (tmp_rdr.Read())
						{
							// Если это не для печати (2)
							if (tmp_rdr.GetString(0) == "2")
							{
								t = "000200";
							}
							if(tmp_rdr.GetString(0) == "3")
							{
								t = "000100";
								tmp_rdr.Close();
								break;
							}
						}
						tmp_rdr.Close();
					}
				}
			}

			this.order.Distanation = t;

			switch (this.order.Distanation)
			{
				case "000100":
					{
						checkPreview.Visible = false;
						checkPreview.Checked = false;
						lblDistanation.Text = "Оператор печати";
						break;
					}
				case "000200":
					{
						checkPreview.Visible = true;
						lblDistanation.Text = "Дизайнер";
						break;
					}
			}
			if (this.order.OrderBody.Rows.Count == 0)
			{
				lblDistanation.Text = "";
			}

		}

		private void button11_Click(object sender, EventArgs e)
		{
			SelectService();
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			UpdateFormatTable();

		}

		private void btnAdvancedPaymentClear_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			this.order.AdvancedPayment = 0;
			ReCalcOrder();
			tmr.Start();
		}

		private void btnDescontClear_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			this.order.Discont = null;
			lblOrderDiscont.Text = "0%";
			ReCalcOrder();
			tmr.Start();
		}

		private void btnQuickServ1_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn01_id);
		}

		private void btnQuickServ2_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn02_id);
		}

		private void btnQuickServ3_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn03_id);
		}

		private void btnQuickServ4_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn04_id);
		}

		private void btnQuickServ5_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn05_id);
		}

		private void btnQuickServ6_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn06_id);
		}

		private void btnQuickServ7_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn07_id);
		}

		private void btnQuickServ8_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn08_id);
		}

		private void btnQuickServ9_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn09_id);
		}

		private void btnQuickServ10_Click(object sender, EventArgs e)
		{
			SelectQuickService(prop.Qbtn10_id);
		}

		private void gridOrder_DoubleClick(object sender, EventArgs e)
		{
			DelFromServTable(gridOrder.Row - 1);
		}

		private void radioCrop1_CheckedChanged(object sender, EventArgs e)
		{
			UpdateRadioButtons();
		}

		private void radioCrop2_CheckedChanged(object sender, EventArgs e)
		{
			UpdateRadioButtons();
		}

		private void radioCrop3_CheckedChanged(object sender, EventArgs e)
		{
			UpdateRadioButtons();
		}

		private void radioPapperType1_CheckedChanged(object sender, EventArgs e)
		{
			UpdateRadioButtons();
		}

		private void radioPapperType2_CheckedChanged(object sender, EventArgs e)
		{
			UpdateRadioButtons();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Отменить заказ?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				this.DialogResult = DialogResult.Cancel;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			bool ok = true;
			if ((this.order.AdvancedPayment != 0) && (txtPType.SelectedValue.ToString() == "-1"))
			{
				MessageBox.Show("Внимание! Укажите тип оплаты!", "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				ok = false;
			}
			if (Momental.Visible == true)
			{
				if (MessageBox.Show("Внимание! Вы сохраняете моментальный заказ!\nСохранить?",
					"Сохранение заказа", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
				{
					ok = true;
				}
				else
				{
					ok = false;
				}
			}
			if (order.Discont != null)
			{
				if (order.Bonus != 0)
				{
					ok = false;
					try
					{
						string key = DateTime.Now.Year.ToString("D4") +
									DateTime.Now.Month.ToString("D2") +
									DateTime.Now.Day.ToString("D2") +
									DateTime.Now.Hour.ToString("D2") +
									DateTime.Now.Minute.ToString("D2") +
									DateTime.Now.Second.ToString("D2") +
									DateTime.Now.Millisecond.ToString("D2");
						HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + prop.DiscontServerAddress + "/card.get.php?code=" + order.Discont.Code_dcard + "&key=" + key + "&format=csv");
						HttpWebResponse response = (HttpWebResponse)request.GetResponse();
						Stream resStream = response.GetResponseStream();
						byte[] buf = new byte[255];
						if (resStream.Read(buf, 0, 255) > 0)
						{
							ok = true;
						}
						else
						{
							ok = false;
							MessageBox.Show("Списание бонусов не возможно т.к. нет связи с сервером!", "Ошибка списания бонусов", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}
					catch
					{
						ok = false;
						MessageBox.Show("Списание бонусов не возможно т.к. нет связи с сервером!", "Ошибка списания бонусов", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}

			}
			if (ok)
			{
				if (MessageBox.Show("Выдача: " + txtOrderDateOutput.Value.ToShortDateString() + " " + txtOrderTimeOutput.Text + "\nВерно?", "Сохранение заказа", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					this.order.Datein = DateTime.Parse(lblDateOrderInput.Text).ToShortDateString();
					this.order.Timein = DateTime.Parse(lblDateOrderInput.Text).ToShortTimeString();
					this.order.Dateout = txtOrderDateOutput.Value.ToShortDateString();
					this.order.Timeout = txtOrderTimeOutput.Text;
					this.order.Usr = this.usr;
					this.order.Comment = txtComment.Text;
					this.order.PType = int.Parse(txtPType.SelectedValue.ToString());

					this.DialogResult = DialogResult.OK;
				}
			}
		}

		private void checkPreview_CheckedChanged(object sender, EventArgs e)
		{
			if (checkPreview.Checked)
				this.order.Preview = 1;
			else
				this.order.Preview = 0;
		}

		private void btnFinalPaymentClear_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			this.order.FinalPayment = 0;
			ReCalcOrder();
			tmr.Start();
		}

		private void btnFinalPayment_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			this.order.FinalPayment = 0;
			ReCalcOrder();
			frmFinalPayment fPayment = new frmFinalPayment(decimal.Parse(lblOrderSumFinal.Text));
			fPayment.ShowDialog();
			this.order.FinalPayment = fPayment.Payment;
			ReCalcOrder();
			tmr.Start();
		}

		private void btnBeznal_Click(object sender, EventArgs e)
		{
			if(order.Client.Name.Substring(0, 2).ToUpper() == "БН")
			{
				order.Distanation = "000001";
				checkPreview.Checked = true;

				this.order.Datein = DateTime.Parse(lblDateOrderInput.Text).ToShortDateString();
				this.order.Timein = DateTime.Parse(lblDateOrderInput.Text).ToShortTimeString();
				this.order.Dateout = txtOrderDateOutput.Value.ToShortDateString();
				this.order.Timeout = txtOrderTimeOutput.Text;
				this.order.Usr = this.usr;
				this.order.Comment = txtComment.Text;

				this.DialogResult = DialogResult.OK;
			}
			else
			{
				MessageBox.Show("Для оформления заказа с оплатой по безналу необходимо, что бы имя клиента начиналось с префикса \"БН\"", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

        private void gridOrder_Click(object sender, EventArgs e)
        {

        }


	}
}