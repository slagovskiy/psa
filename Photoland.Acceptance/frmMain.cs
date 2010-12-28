using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.Discont;
using Photoland.Security.User;
using Photoland.Forms.Admin;
using Photoland.Forms.Interface;
using Photoland.Lib;
using Photoland.Acceptance.Wizard;
using Photoland.Acceptance.NumGenerator;
using Photoland.Order;
using PSA.Lib.Interface;
using PSA.Lib.Util;


namespace Photoland.Acceptance
{
	public partial class frmMain : Form
	{
		public SqlConnection db_connection = new SqlConnection();
		public UserInfo usr;
		public PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		private OrderNum num = new OrderNum();


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

		public frmMain()
		{
			InitializeComponent();
			this.Text = "Приемка - Photoland System Automation " + Application.ProductVersion;
		}

		private void StartWizard(bool auto)
		{
			// Данные из мастера!
			DataTable tblOrder;

			string OrderNum = num.NewOrderNum(db_connection, usr);

			// Начинаем прием
			// Первый шаг мастера
			Wizard.frmStep1 fStep1 = new frmStep1();
			Wizard.frmStep2 fStep2 = new frmStep2();
			Wizard.frmStep2a fStep2a = new frmStep2a();
			Wizard.frmStep3 fStep3 = new frmStep3();
			// Передаем данные о пользователе
			fStep1.usr = this.usr;
			fStep2.usr = this.usr;
			fStep2a.usr = this.usr;
			fStep3.usr = this.usr;
			// Передаем соединение с базой
			fStep1.db_connection = this.db_connection;
			fStep2.db_connection = this.db_connection;
			fStep2a.db_connection = this.db_connection;
			fStep3.db_connection = this.db_connection;
			// Задаем номер заказа
			fStep1.lblOrderNo.Text = OrderNum;
			fStep2.lblOrderNo.Text = OrderNum;
			fStep2a.lblOrderNo.Text = OrderNum;
			fStep3.lblOrderNo.Text = OrderNum;
			// если auto истина, то выставляем галку на третьем шаге
			fStep3.checkOpenOrder.Checked = !auto;
			// Открываем модально окно первого шага
			bool wizardok = false;
			string step = "";
			fStep1.ShowDialog();
			step = "step1";
			while (!wizardok)
			{
				switch (step)
				{
					case "step1":
						{
							switch (fStep1.DialogResult)
							{
								case DialogResult.Cancel:
									{
										wizardok = true;
										step = "cancel";
										break;
									}
								case DialogResult.Retry:
									{
										break;
									}
								case DialogResult.OK:
									{
										tblOrder = fStep1.tblOrder;
										fStep2a.tblOrder = tblOrder;
										step = "step2";
										fStep2.ShowDialog();
										break;
									}
							}
							break;
						}

					case "step2":
						{
							switch (fStep2.DialogResult)
							{
								case DialogResult.Cancel:
									{
										wizardok = true;
										step = "cancel";
										break;
									}
								case DialogResult.Retry:
									{
										step = "step1";
										fStep1.ShowDialog();
										break;
									}
								case DialogResult.OK:
									{
										step = "step2a";
										fStep2a.client = fStep2.client;
										fStep2a.ShowDialog();
										break;
									}
							}
							break;
						}

					case "step2a":
						{
							switch (fStep2a.DialogResult)
							{
								case DialogResult.Cancel:
									{
										wizardok = true;
										step = "cancel";
										break;
									}
								case DialogResult.Retry:
									{
										step = "step2";
										fStep2.ShowDialog();
										break;
									}
								case DialogResult.OK:
									{
										step = "step3";
										fStep3.ShowDialog();
										break;
									}
							}
							break;
						}

					case "step3":
						{
							switch (fStep3.DialogResult)
							{
								case DialogResult.Cancel:
									{
										wizardok = true;
										step = "cancel";
										break;
									}
								case DialogResult.Retry:
									{
										step = "step2a";
										fStep2a.ShowDialog();
										break;
									}
								case DialogResult.OK:
									{
										step = "end";
										wizardok = true;
										break;
									}
							}
							break;
						}
				}
			}

			// визард с горем пополам окончил работу
			// если не нажата отмена
			if ((step != "cancel") && (step != "") && (step == "end"))
			{
				OrderInfo Order = new OrderInfo(db_connection);

				Order.Usr = this.usr;
				Order.Client = fStep2.client;
				Order.Discont = fStep2a.discont;
				Order.OrderBody = fStep1.tblOrder;
				
				if (fStep1.radioCrop1.Checked)
					Order.Crop = 1;
				if (fStep1.radioCrop2.Checked)
					Order.Crop = 2;
				if (fStep1.radioCrop3.Checked)
					Order.Crop = 3;
				
				if (fStep1.radioPapperType1.Checked)
					Order.Type = 1;
				if (fStep1.radioPapperType2.Checked)
					Order.Type = 2;

				Order.Datein = DateTime.Now.ToShortDateString();
				Order.Timein = DateTime.Now.ToShortTimeString();
				Order.Dateout = fStep3.cldrDate.SelectedDates[0].ToShortDateString();
				Order.Timeout = fStep3.gridTime.GetData(fStep3.gridTime.Row, fStep3.gridTime.ColSel).ToString();

				Order.AdvancedPayment = fStep2a.AdvancedPayment;
				Order.FinalPayment = fStep2a.FinalPayment;

				Order.Orderno = OrderNum;


				if (fStep3.checkOpenOrder.Checked)
				{
					frmOrder fOrder = new frmOrder(usr, true, Order);
					fOrder.db_connection = db_connection;
					fOrder.ShowDialog();
					if (fOrder.DialogResult == DialogResult.Cancel)
					{
						fStep1.tmr.Stop();
						fStep1.Close();
						fStep2.Close();
						fStep2a.Close();
						fStep3.Close();
						fOrder.Close();
						try
						{
							if (!prop.DenyDelete)
							{
								if (prop.QueryForDelete)
								{
									if (MessageBox.Show("Удалить папку " + prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
									{
										Directory.Delete(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum, true);
									}
								}
								else
								{
									Directory.Delete(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum, true);
								}
							}
							if (!prop.DenyDelete)
							{
								if (prop.QueryForDelete)
								{
									if (MessageBox.Show("Удалить папку " + prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
									{
										Directory.Delete(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum, true);
									}
								}
								else
								{
									Directory.Delete(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum, true);
								}
							}
						}
						catch(Exception ex)
						{
							ErrorNfo.WriteErrorInfo(ex);
						}
						MessageBox.Show("Заказ отменен!");
					}
					else
					{
						Order = fOrder.order;
						fStep1.tmr.Stop();
						fStep1.Close();
						fStep2.Close();
						fStep2a.Close();
						fStep3.Close();
						fOrder.Close();
						// Сохраняем из расшир формы
						if (!Order.SaveAdvOrder())
						{
							MessageBox.Show("Внимание! Ошибка при сохранении заказа!\nЗАКАЗ НЕ БУДЕТ СОХРАНЕН!!!\nДополнительная информация: " + Order.Err, "Сохранение заказа", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
						else
						{
							if (!Order.SaveAdvTextFile())
							{
								MessageBox.Show("Ошибка сохранения информации о заказе в текстовый файл!\nДополнительная информация: " + Order.Err, "Сохранение дополнительной информации", MessageBoxButtons.OK, MessageBoxIcon.Information);
							}
							PrintCheck(Order.Orderno);
						}
					}
				}
				else
				{
					fStep1.tmr.Stop();
					fStep1.Close();
					fStep2.Close();
					fStep2a.Close();
					fStep3.Close();
					// Сохраняем из мастера
					if (!Order.SaveEasyOrder())
					{
						MessageBox.Show("Внимание! Ошибка при сохранении заказа!\nЗАКАЗ НЕ БУДЕТ СОХРАНЕН!!!\nДополнительная информация: " + Order.Err, "Сохранение заказа", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						if (!Order.SaveEasyTextFile())
						{
							MessageBox.Show("Ошибка сохранения информации о заказе в текстовый файл!\nДополнительная информация: " + Order.Err, "Сохранение дополнительной информации", MessageBoxButtons.OK, MessageBoxIcon.Information);
						}
						PrintCheck(Order.Orderno);
					}

				}
				fStep1.tmr.Stop();
				fStep1.Close();
				fStep2.Close();
				fStep2a.Close();
				fStep3.Close();
			}
			else
			{
				fStep1.tmr.Stop();
				fStep1.Close();
				fStep2.Close();
				fStep2a.Close();
				fStep3.Close();
				try
				{
					if (!prop.DenyDelete)
					{
						if (prop.QueryForDelete)
						{
							if (MessageBox.Show("Удалить папку " + prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								Directory.Delete(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum, true);
							}
						}
						else
						{
							Directory.Delete(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum, true);
						}
					}
					if (!prop.DenyDelete)
					{
						if (prop.QueryForDelete)
						{
							if (MessageBox.Show("Удалить папку " + prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								Directory.Delete(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum, true);
							}
						}
						else
						{
							Directory.Delete(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + OrderNum, true);
						}
					}
				}
				catch(Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);
				}
				MessageBox.Show("Заказ отменен!");
			}
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
            try
            {
                StreamWriter sw = new StreamWriter(prop.Dir_export + "\\Acceptance_" + Environment.MachineName + "_" + Environment.UserName + ".info", false, Encoding.GetEncoding(1251));
                sw.Write("Date:           " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                         "\nMashine:        " + Environment.MachineName +
                         "\nUser:           " + Environment.UserName +
                         "\nAcceptance mod: " + Application.ProductVersion);
                sw.Close();
            }
            catch
            {
            }

			if (!Checking.checkVersion(Modules.Acceptance, Application.ProductVersion))
				if (MessageBox.Show("Внимание! Существует более новая версия модуля!\nУстановить обновление?", "Контроль обновлений", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
													0,
													System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
													) + "\\PSA.Update.cmd");

			if (!PSA.Lib.Util.Semaphore.semInventory)
			{

				bool tmp_login_ok = false;
				bool tmp_exit = false;

				// Проверяем, если есть ограничение на запуск одной копии и программа уже запущена
				if ((app.Search_twin()) && (prop.Run_one_copy_acceptance))
				{
					// То выдаем сообщение и закрываем программу
					MessageBox.Show("Программа уже запущена!");
					Application.Exit();
				}
				else
				{
					// Если ограничение на запуск одной копии пройдено, то продолжаем...
					// Открываем соединение с базой
					try
					{
						db_connection.ConnectionString = prop.Connection_string;
						db_connection.Open();
					}
					catch (Exception ex)
					{
						// Если не удалось подключиться к базе, то
						// выдаем сообщение об ошибке и открываем форму
						// подключения к базе данных
						ErrorNfo.WriteErrorInfo(ex);
						MessageBox.Show("Ошибка подключения к базе данных!\n" + ex.Message + "\n" + ex.Source + "\nПроверьте настройки подключения!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
						try
						{
							frmSetup fOptions = new frmSetup();
							fOptions.ShowDialog();
							// Опять пробуем подключиться к базе
							prop = new PSA.Lib.Util.Settings();
							db_connection.ConnectionString = prop.Connection_string;
							db_connection.Open();
						}
						catch (Exception exc)
						{
							ErrorNfo.WriteErrorInfo(exc);
							// Если после второй попытки не удалось подключиться, то закрываем программу.
							MessageBox.Show("Ошибка подключения к базе данных!\n" + exc.Message + "\n" + exc.Source, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
							tmp_exit = true;
						}
					}

					if (!tmp_exit)
					{
						// открываем окно запроса пользователя
						PSA.Lib.Interface.frmLogin fLogin = new PSA.Lib.Interface.frmLogin();
						// спрашиваем пока не угадает пароль или не надоест угадывать
						while (!tmp_login_ok)
						{
							switch (fLogin.ShowDialog())
							{
								case DialogResult.Cancel:
									{
										tmp_login_ok = true;
										tmp_exit = true;
										break;
									}
								case DialogResult.OK:
									{
										tmp_login_ok = true;
										if (fLogin.usr.prmCanLoginAcceptance)
										{
											this.Show();
										}
										else
										{
											tmp_exit = true;
											MessageBox.Show("Доступ в модуль приемка заперщен!", "Вход в программу", MessageBoxButtons.OK, MessageBoxIcon.Error);
										}
										break;
									}
							}
						}
						if (!tmp_exit)
						{
							// если мы уже тут, значит пароль все же угадали
							// Получаем данные о пользователе
							usr = fLogin.usr;
							// Показываем окно
							this.Focus();
							this.Text = "Приемка - " + usr.Name + " - Photoland System Automation " + Application.ProductVersion;
							if (prop.ShowQuickOrder)
							{
								toolQuick.Visible = true;
								toolStripQuick.Visible = true;
							}
							else
							{
								toolQuick.Visible = false;
								toolStripQuick.Visible = false;
							}
							if (prop.ShowMFoto)
							{
								toolMFoto.Visible = true;
								toolStripQuick.Visible = true;
							}
							else
							{
								toolMFoto.Visible = false;
								toolStripQuick.Visible = false;
							}
						}
						else
						{
							Application.Exit();
						}
					}
					else
					{
						Application.Exit();
					}
				}
			}
			else
			{
				MessageBox.Show("В момент проведения инвентаризации вход в модуль запрещен!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				Application.Exit();
			}
		}

		private void OpenNewOrder()
		{
			frmOrder fOrder = new frmOrder(usr, true);

			fOrder.db_connection = db_connection;
			fOrder.ShowDialog();
			if (fOrder.DialogResult == DialogResult.OK)
			{
				OrderInfo Order = new OrderInfo(db_connection);
				Order = fOrder.order;
				fOrder.Close();
				// Сохраняем из расшир формы
				if (!Order.SaveAdvOrder())
				{
					MessageBox.Show("Внимание! Ошибка при сохранении заказа!\nЗАКАЗ НЕ БУДЕТ СОХРАНЕН!!!\nДополнительная информация: " + Order.Err, "Сохранение заказа", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					if (!Order.SaveAdvTextFile())
					{
						MessageBox.Show("Ошибка сохранения информации о заказе в текстовый файл!\nДополнительная информация: " + Order.Err, "Сохранение дополнительной информации", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					PrintCheck(Order.Orderno);
				}
			}
			else
			{
				OrderInfo Order = new OrderInfo(db_connection);
				Order = fOrder.order;
				fOrder.Close();
				try
				{
					if (!prop.DenyDelete)
					{
						if (prop.QueryForDelete)
						{
							if (MessageBox.Show("Удалить папку " + prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								Directory.Delete(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno, true);
							}
						}
						else
						{
							Directory.Delete(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno, true);
						}
					}
					if (!prop.DenyDelete)
					{
						if (prop.QueryForDelete)
						{
							if (MessageBox.Show("Удалить папку " + prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								Directory.Delete(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno, true);
							}
						}
						else
						{
							Directory.Delete(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno, true);
						}
					}
				}
				catch (Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);

				}
				MessageBox.Show("Заказ отменен!");
			}
		}

		private void OpenNewQuickOrder()
		{
			frmOrder fOrder = new frmOrder(usr, true);

			fOrder.db_connection = db_connection;
			fOrder.Momental.Visible = true;
			fOrder.ShowDialog();
			if (fOrder.DialogResult == DialogResult.OK)
			{
				OrderInfo Order = new OrderInfo(db_connection);
				Order = fOrder.order;
				fOrder.Close();
				Order.Distanation = "000000";
				// Сохраняем из расшир формы
				if (!Order.SaveAdvOrder(true))
				{
					MessageBox.Show("Внимание! Ошибка при сохранении заказа!\nЗАКАЗ НЕ БУДЕТ СОХРАНЕН!!!\nДополнительная информация: " + Order.Err, "Сохранение заказа", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					if (!Order.SaveAdvTextFile())
					{
						MessageBox.Show("Ошибка сохранения информации о заказе в текстовый файл!\nДополнительная информация: " + Order.Err, "Сохранение дополнительной информации", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
					PrintCheck(Order.Orderno);
				}
			}
			else
			{
				OrderInfo Order = new OrderInfo(db_connection);
				Order = fOrder.order;
				fOrder.Close();
				try
				{
					if (!prop.DenyDelete)
					{
						if (prop.QueryForDelete)
						{
							if (MessageBox.Show("Удалить папку " + prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								Directory.Delete(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno, true);
							}
						}
						else
						{
							Directory.Delete(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno, true);
						}
					}
					if (!prop.DenyDelete)
					{
						if (prop.QueryForDelete)
						{
							if (MessageBox.Show("Удалить папку " + prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno + " ?", "Debug", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
							{
								Directory.Delete(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno, true);
							}
						}
						else
						{
							Directory.Delete(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + Order.Orderno, true);
						}
					}
				}
				catch (Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);

				}
				MessageBox.Show("Заказ отменен!");
			}
		}

		private void PrintCheck(string Orderno)
		{
			// Печатаем чек
			OrderInfo prnOrder = new OrderInfo(db_connection, Orderno, true);
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
					rep.Fields["advstr1"].Text = prop.CheckString1;
					rep.Fields["advstr2"].Text = prop.CheckString2;
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
						for (int j = 0; j < prop.CheckCount; j++)
						{
							rep.Document.Print();
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
				MessageBox.Show("Ошибка вывода чека\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void OpenSettings()
		{
			if (usr.prmCanSetup)
			{
				frmSetup fOptions = new frmSetup();
				fOptions.ShowDialog();
				prop = new PSA.Lib.Util.Settings();
			}
			else
			{
				MessageBox.Show("Нет доступа", "Доступ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void OpenAcceptanceTable()
		{
			try
			{
				bool open = true;
				foreach (Form f in this.MdiChildren)
				{
					if (f.Name == "frmAcceptanceTable")
					{
						f.MdiParent = this;
						f.Show();
						f.WindowState = FormWindowState.Maximized;
						open = false;
					}
				}
				if (open)
				{
					frmAcceptanceTable fATable = new frmAcceptanceTable(db_connection, usr);
					fATable.MdiParent = this;
					fATable.Show();
				}
			}
			catch { }
		}

		private void OpenAcceptanceTable2()
		{
			bool open = true;
			foreach (Form f in this.MdiChildren)
			{
				if (f.Name == "frmAccptanceTableImport")
				{
					f.MdiParent = this;
					f.Show();
					f.WindowState = FormWindowState.Maximized;
					open = false;
				}
			}
			if (open)
			{
				frmAccptanceTableImport fATable = new frmAccptanceTableImport(db_connection, usr);
				fATable.MdiParent = this;
				fATable.Show();
			}
		}

		private void OpenPaymentsTable(string id_user)
		{
			bool open = true;
			foreach (Form f in this.MdiChildren)
			{
				if (f.Name == "frmPaymentTable")
				{
					f.MdiParent = this;
					f.Show();
					f.WindowState = FormWindowState.Maximized;
					open = false;
				}
			}
			if (open)
			{
				if (id_user != "")
				{
					if (usr.prmCanSeeMyPayments)
					{
						frmPaymentTable fPTable = new frmPaymentTable(db_connection, usr, true);
						fPTable.MdiParent = this;
						fPTable.Show();
						fPTable.WindowState = FormWindowState.Maximized;
					}
					else
					{
						MessageBox.Show("Нет доступа!", "Доступ", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
				else
				{
					if (usr.prmCanSeeAllPayments)
					{
						frmPaymentTable fPTable = new frmPaymentTable(db_connection, usr);
						fPTable.MdiParent = this;
						fPTable.Show();
						fPTable.WindowState = FormWindowState.Maximized;
					}
					else
					{
						MessageBox.Show("Нет доступа!", "Доступ", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}
				}
			}
		}


		private void tbtnWizardOnly_Click(object sender, EventArgs e)
		{
			StartWizard(true);
		}

		private void tbtnWizardMaster_Click(object sender, EventArgs e)
		{
			StartWizard(false);
		}

		private void mnuAcceptanceAuto_Click(object sender, EventArgs e)
		{
			StartWizard(true);
		}

		private void mnuAcceptanceManual_Click(object sender, EventArgs e)
		{
			StartWizard(false);
		}

		private void mnuSetup_Click(object sender, EventArgs e)
		{
			OpenSettings();
		}

		private void tbtnOptions_Click(object sender, EventArgs e)
		{
			OpenSettings();
		}

		private void tbtnMaster_Click(object sender, EventArgs e)
		{
			OpenNewOrder();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			frmSelectFrame fFrame = new frmSelectFrame();
			fFrame.ShowDialog();
		}

		private void tbtnOpenAcceptabceTable_Click(object sender, EventArgs e)
		{
			OpenAcceptanceTable();
		}

		private void mnuOpenAcceptanceTable_Click(object sender, EventArgs e)
		{
			OpenAcceptanceTable();
		}

		private void mnuPaymentsAll_Click(object sender, EventArgs e)
		{
			OpenPaymentsTable("");
		}

		private void mnuPaymentsCyr_Click(object sender, EventArgs e)
		{
			OpenPaymentsTable(usr.Id_user.ToString());
		}

		private void mnuAcceptanceHand_Click(object sender, EventArgs e)
		{
			OpenNewOrder();
		}

		private void tbtnLogoff_Click(object sender, EventArgs e)
		{
			Logoff();
		}

		private void Logoff()
		{
			foreach (Form f in this.MdiChildren)
			{
				f.Close();
			}
			db_connection.Close();
			frmMain_Load(this, new EventArgs());
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void LogoffStripMenuItem1_Click(object sender, EventArgs e)
		{
			Logoff();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Photoland.Forms.Interface.frmAbout f = new Photoland.Forms.Interface.frmAbout();
			f.ShowDialog();
		}

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            bool open = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmClientTable")
                {
                    f.MdiParent = this;
                    f.Show();
                    f.WindowState = FormWindowState.Maximized;
                    open = false;
                }
            }
            if (open)
            {
                frmClientTable fATable = new frmClientTable(db_connection, usr);
                fATable.MdiParent = this;
                fATable.Show();
                fATable.WindowState = FormWindowState.Maximized;
            }
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
							DataTable r = new DataTable("Report");
							SqlCommand c;
							SqlDataAdapter a;
							switch (rep_name)
							{
								case "Kassa":
									{
										frmSelectDateIntervalKassa f = new frmSelectDateIntervalKassa();
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											string filter2 = "";
											filter =
												" (dbo.payments.date >= CONVERT(DATETIME, '" +
												f.dateBegin.Value.Year.ToString("D4") + "-" +
												f.dateBegin.Value.Month.ToString("D2") + "-" +
												f.dateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND dbo.payments.date <= CONVERT(DATETIME, '" +
												f.dateEnd.Value.Year.ToString("D4") + "-" +
												f.dateEnd.Value.Month.ToString("D2") + "-" +
												f.dateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT SUM(dbo.payments.payment) AS SM, dbo.category.name FROM dbo.payments INNER JOIN dbo.[order] ON dbo.payments.number = dbo.[order].number INNER JOIN dbo.client ON dbo.[order].id_client = dbo.client.id_client INNER JOIN dbo.category ON dbo.client.id_category = dbo.category.id_category WHERE " + filter + " GROUP BY dbo.category.name";
											c = new SqlCommand(query, db_connection);
											c.CommandTimeout = 9000;
											a = new SqlDataAdapter(c);
											a.Fill(r);
											rep.Load(prop.PathReportsTemplates, rep_name);
											rep.DataSource.Recordset = r;
											ok = true;
										}
										break;
									}
								case "Kassa full":
									{
										frmSelectDateIntervalKassa f = new frmSelectDateIntervalKassa();
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											string filter2 = "";
											filter =
												" (dbo.payments.date >= CONVERT(DATETIME, '" +
												f.dateBegin.Value.Year.ToString("D4") + "-" +
												f.dateBegin.Value.Month.ToString("D2") + "-" +
												f.dateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND dbo.payments.date <= CONVERT(DATETIME, '" +
												f.dateEnd.Value.Year.ToString("D4") + "-" +
												f.dateEnd.Value.Month.ToString("D2") + "-" +
												f.dateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT dbo.payments.id_payment, dbo.payments.date, dbo.payments.time, dbo.payments.id_user, dbo.payments.name_user, dbo.payments.number, dbo.payments.payment, dbo.payments.type, dbo.payments.comment, dbo.payments.payment_way, dbo.payments.exported, dbo.category.name FROM dbo.payments INNER JOIN dbo.[order] ON dbo.payments.number = dbo.[order].number INNER JOIN dbo.client ON dbo.[order].id_client = dbo.client.id_client INNER JOIN dbo.category ON dbo.client.id_category = dbo.category.id_category WHERE " + filter;
											c = new SqlCommand(query, db_connection);
											c.CommandTimeout = 9000;
											a = new SqlDataAdapter(c);
											a.Fill(r);
											rep.Load(prop.PathReportsTemplates, rep_name);
											rep.DataSource.Recordset = r;
											ok = true;
										}
										break;
									}
								case "Debet":
									{
										frmSelectDateIntervalKassa f = new frmSelectDateIntervalKassa();
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											string filter2 = "";
											filter =
												" (dbo.[order].output_date >= CONVERT(DATETIME, '" +
												f.dateBegin.Value.Year.ToString("D4") + "-" +
												f.dateBegin.Value.Month.ToString("D2") + "-" +
												f.dateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND dbo.[order].output_date <= CONVERT(DATETIME, '" +
												f.dateEnd.Value.Year.ToString("D4") + "-" +
												f.dateEnd.Value.Month.ToString("D2") + "-" +
												f.dateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT TOP (100) PERCENT BODYSUM.id_order, BODYSUM.sm, dbo.[order].number, dbo.client.name AS client, dbo.client.id_client,  dbo.order_status.status_desc, dbo.order_status.order_status, dbo.[order].input_date, dbo.[order].output_date, dbo.[order].advanced_payment, dbo.[order].final_payment FROM (SELECT TOP (100) PERCENT id_order, SUM(actual_quantity * price) AS sm FROM dbo.orderbody GROUP BY id_order HAVING (id_order > 0)) AS BODYSUM INNER JOIN dbo.[order] ON BODYSUM.id_order = dbo.[order].id_order INNER JOIN dbo.client ON dbo.[order].id_client = dbo.client.id_client INNER JOIN dbo.category ON dbo.client.id_category = dbo.category.id_category INNER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status WHERE (dbo.category.id_category > 2) AND (dbo.order_status.order_status = N'100000' OR dbo.order_status.order_status = N'200000') AND " + filter + " ORDER BY client";
											c = new SqlCommand(query, db_connection);
											c.CommandTimeout = 9000;
											a = new SqlDataAdapter(c);
											a.Fill(r);
											rep.Load(prop.PathReportsTemplates, rep_name);
											rep.DataSource.Recordset = r;
											ok = true;
										}
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

		
		void ToolStripMenuItem17Click(object sender, EventArgs e)
		{
			ShowReport("Kassa", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa full", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem5_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa full", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem6_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa full", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem18_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem19_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem8_Click(object sender, EventArgs e)
		{
			ShowReport("Debet", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem9_Click(object sender, EventArgs e)
		{
			ShowReport("Debet", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem10_Click(object sender, EventArgs e)
		{
			ShowReport("Debet", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void semaphoresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSemaphores f = new frmSemaphores();
			f.ShowDialog();
			prop = new PSA.Lib.Util.Settings();
		}

		private void toolStripMenuItem12_Click(object sender, EventArgs e)
		{
			frmVerificationDoc f = new frmVerificationDoc(usr);
			f.ShowDialog();
		}

		private void toolStripMenuItem13_Click(object sender, EventArgs e)
		{
			bool open = true;
			foreach (Form f in this.MdiChildren)
			{
				if (f.Name == "frmVerificationList")
				{
					f.MdiParent = this;
					f.Show();
					f.WindowState = FormWindowState.Maximized;
					open = false;
				}
			}
			if (open)
			{
				frmVerificationList f = new frmVerificationList();
				f.usr = usr;
				f.MdiParent = this;
				f.Show();
				f.WindowState = FormWindowState.Maximized;
			}
		}

		private void mnuRobotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (frmRobotManager f = new frmRobotManager())
			{
				f.ShowDialog();
			}
		}

		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			using (frmGetBarCode f = new frmGetBarCode())
			{
				if (f.ShowDialog() == DialogResult.OK)
				{
					//MessageBox.Show(f.BarCode);
					RemoteQuery rq = new RemoteQuery();
					rq.GetInfo(f.BarCode);
				}
			}
		}

		private void mnuSetupLastVersion_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Установить обновление?", "Контроль обновлений",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
												0,
												System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
												) + "\\PSA.Update.cmd");
		}

		private void toolOpenAccepTable2_Click(object sender, EventArgs e)
		{
			OpenAcceptanceTable2();
		}

		private void toolQuick_Click(object sender, EventArgs e)
		{
			OpenNewQuickOrder();
		}

		private void newQuickOrderToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenNewQuickOrder();
		}

		private void toolMFoto_Click(object sender, EventArgs e)
		{
			try
			{
				frmGetBarCodeEdit fscan = new frmGetBarCodeEdit();
				fscan.ShowDialog();
				if (fscan.DialogResult == DialogResult.OK)
				{
					try
					{
						Application.DoEvents();
						RemoteQuery rq = new RemoteQuery(usr);
						rq.GetMFotoData(fscan.barcode);
						OpenOrder2("44" + int.Parse(fscan.barcode).ToString("D10"));
					}
					catch { }
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
		}

		private void OpenOrder2(string orderno)
		{
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
					//fOrder.fixDouble = checkDouble.Checked;
					fOrder.ShowDialog();
				}
			}
			
		}
	
	}
}