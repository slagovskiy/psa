using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PSA.Lib.Interface;
using PSA.Lib.Util;
using PSA.Lib.BLL;
using Photoland.Order;
using System.Data.SqlClient;
using System.Resources;
using System.Reflection;
using FirebirdSql.Data.FirebirdClient;

namespace PSA.Kiosk
{
	public partial class frmMain : Form
	{
		private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		private bool Hide
		{
			get
			{
				return this.Visible;
			}
			set
			{
				this.Visible = !value;
			}
		}

		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			lblVer.Text = "PSA version " + Application.ProductVersion;
			MoveStartPosition();
			
			Hide = true;

			try
			{
				StreamWriter sw = new StreamWriter(prop.Dir_export + "\\Kiosk_" + Environment.MachineName + "_" + Environment.UserName + ".info", false, Encoding.GetEncoding(1251));
				sw.Write("Date:          " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
						 "\nMashine:       " + Environment.MachineName +
						 "\nUser:          " + Environment.UserName +
						 "\nKiosk mod: " + Application.ProductVersion);
				sw.Close();
			}
			catch
			{
			}

			if (!Checking.checkVersion(Modules.Kiosk, Application.ProductVersion))
				if (MessageBox.Show("Внимание! Существует более новая версия модуля!\nУстановить обновление?", "Контроль обновлений", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
													0,
													System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
													) + "\\PSA.Update.cmd");
		}

		private void MoveStartPosition()
		{
			this.Top = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Height - 160;
			this.Left = System.Windows.Forms.SystemInformation.PrimaryMonitorSize.Width - 230;
		}


		private void btnClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.Close();
		}

		private void btnMinimize_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Hide = true;
		}

		private void icon_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			MoveStartPosition();
			Hide = false;
		}

		private void showToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Hide = false;
		}


		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (MessageBox.Show("Завершить работу робота опроса фотокиосков?", "Завершение работы", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				e.Cancel = false;
			else
				e.Cancel = true;

		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSetup f = new frmSetup();
			f.ShowDialog();
			prop = new PSA.Lib.Util.Settings();
		}

		private void semaphoresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSemaphores f = new frmSemaphores();
			f.Show();
			prop = new PSA.Lib.Util.Settings();
		}

		private void toolStripMenuKiosk_Click(object sender, EventArgs e)
		{
			frmKioskList f = new frmKioskList();
			f.ShowDialog();
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			/*
			if (sec == 0)
			{
				SearchOrders();
				sec = 600;
			}
			else
			{
				sec--;
				lblInfo.Text = "Опрос киосков через: " + sec.ToString("D2") + "с.";
			}
			 */
		}




		private void SearchOrders()
		{
			//tmr.Stop();
			icon.Icon = wicog.Icon;
			try
			{
				using (SqlConnection con = new SqlConnection(prop.Connection_string))
				{
					con.Open();
					// проверяем на 9-й категорию прайса
					SqlCommand cmd = new SqlCommand("DECLARE @C int; SET @c = (SELECT COUNT(*) FROM [dbo].[category] WHERE [id_category] = 9); IF(@C = 0) BEGIN; INSERT INTO [dbo].[category]([id_category], [name], [input]) VALUES (9, 'Фототерминалы', 0); END;", con);
					cmd.ExecuteNonQuery();

					//Проверяем на наличие клиента с имененм "Фототерминал" и 9-ой категории прайса
					cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_client] FROM [dbo].[client] WHERE RTRIM([name]) = 'Фототерминал'); IF(@ID > 0) BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[client] ([id_category], [id_dcard], [guid], [del], [name], [phone_1], [phone_2], [address], [email], [icq], [addon]) VALUES (9, 0, newid(), 0, 'Фототерминал', '', '', '', '', '', ''); SET @ID = scope_identity(); SELECT @ID; END;", con);
					int client_id = (int)cmd.ExecuteScalar();

					//Проверяем на наличие услуги с кодом -1
					cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_good] FROM [dbo].[good] WHERE RTRIM([id_good]) = '-1'); IF(@ID = '-1') BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[good] ([id_good], [guid], [del], [name], [description], [prefix], [folder], [type], [checked], [zero], [sign], [apply_form], [EI], [bonustype], [kiosk_name]) VALUES('-1', newid(), 0, 'УСЛУГА НЕ НАЙДЕНА', 'УСЛУГА НЕ НАЙДЕНА', '', '-1', '1', 0, 1, 'none-1', '', 0, '', ''); SET @ID = scope_identity(); SELECT @ID; END;", con);
					cmd.ExecuteNonQuery();

					//Проверяем наличие пользователя "Фототерминал"
					cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_user] FROM [dbo].[user] WHERE RTRIM([name]) = 'Фототерминал'); IF(@ID > 0) BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[user]([name], [сontact], [login], [password], [permission]) VALUES ('Фототерминал','','Фототерминал','ajnjnthvbyfk',0); SET @ID = scope_identity(); SELECT @ID; END;", con);
					int user_id = (int)cmd.ExecuteScalar();


					foreach (PSA.Lib.BLL.Kiosk k in PSA.Lib.BLL.Kiosk.getList())
					{
						#region old idea
						/*
					if (Directory.Exists(k.Path.Trim()))
					{
						// проходим первый уровень (папки дней)
						DirectoryInfo d = new DirectoryInfo(k.Path.Trim());
						foreach (DirectoryInfo _d in d.GetDirectories())
						{
							//Если пустой день, то удаляем эту папку
							if (_d.GetDirectories().Length == 0)
							{
								try
								{
									// ничего не удалять!
									//_d.Delete();
								}
								catch
								{
								}
							}
							else
							{
								// Нашли день, ищем в нем заказы
								foreach (DirectoryInfo o in _d.GetDirectories())
								{
									// найден заказ, ищем его описание
									if (File.Exists(o.FullName + "\\comment.txt"))
									{
										string query = "";
										string order_comment = "Терминальный киоск: " + k.Code + ", ";
										string client_name = "";
										string client_phone = "";
										string order_number = "";
										List<string[]> order_body = new List<string[]>();
										decimal order_payment = 0;

										// Найдено описание, читаем его.
										using (StreamReader r = new StreamReader(o.FullName + "\\comment.txt", Encoding.GetEncoding(1251), true))
										{
											// читаем пока не кончится файло
											// ведем подсчет строк
											int i = 0;
											while (r.Peek() >= 0)
											{
												string data = "";
												// это номер заказа
												if (i == 0)
												{
													data = r.ReadLine();
													data = data.Split('\t')[1];
													order_number = prop.Order_terminal_prefics + k.Code.ToString("D3") + int.Parse(data).ToString("D7");
													i++;
												}
												// это фамилия заказчика
												else if (i == 1)
												{
													data = r.ReadLine();
													data = data.Split('\t')[1];
													if (prop.Terminal_client_one)
														order_comment += " Клиент: " + data + "; ";
													else
														client_name = data.ToUpper();
													i++;
												}
												// это телефон
												else if (i == 2)
												{
													data = r.ReadLine();
													data = data.Split('\t')[1];
													if (prop.Terminal_client_one)
														order_comment += " Телефон: " + data + "; ";
													else
														client_phone = data.ToUpper();
													i++;
												}
												// читаем строки
												else if (i > 2)
												{
													data = r.ReadLine();
													if (data != "")
													{
														string[] body = data.Split('\t');
														string[] _order_body = new string[7];
														_order_body[0] = "";
														_order_body[1] = body[0].Trim() + " " + body[1].Trim();
														_order_body[2] = body[2];
														_order_body[3] = body[3];
														_order_body[4] = body[4];
														_order_body[5] = "";
														_order_body[6] = body[0].Trim() + " " + body[1].Trim() + " " + body[2] + "*" + body[3];
														order_body.Add(_order_body);
													}
													i++;
												}


											}

											// построение строки для списания материалов
											string materials = "Материалы: ";
											foreach (DirectoryInfo _o in o.GetDirectories())
											{
												materials += _o.Name + ":";
												foreach (DirectoryInfo __o in _o.GetDirectories())
												{
													materials += __o.Name + ";";
												}
												materials = materials.Substring(0, materials.Length - 1) + ", ";
											}
											materials = materials.Substring(0, materials.Length - 2);

											for (int j = 0; j < order_body.Count; j++)
											{
												// определяем ключи для услуг
												cmd = new SqlCommand("SELECT id_good, type FROM dbo.good WHERE (kiosk_name LIKE '%|" + order_body[j][1].Trim() + "|%')", con);
												SqlDataAdapter da = new SqlDataAdapter(cmd);
												DataSet ds = new DataSet();
												da.Fill(ds, "goods");
												if(ds.Tables[0].Rows.Count > 0)
												{
													
													order_body[j][0] = ds.Tables[0].Rows[0][0].ToString().Trim();
													order_body[j][5] = ds.Tables[0].Rows[0][1].ToString().Trim();
												}
												else
												{
													order_body[j][0] = "-1";
													order_body[j][5] = "1";
												}
												order_payment += 0; //decimal.Parse(order_body[j][4].Replace(',', '.'));
											}

											// теперь собираем запрос на добавление
											// не выпендриваемся и делаем с транзакцией
											// сперва шапка


											// статус (назначение) определяется услугами
											string order_status = "";
											// если в папке заказа нет подпапок с файлами, 
											// то считаем, что прошла моментальная печать
											if (o.GetDirectories().Length == 0)
												order_status = "100000";
											else
											{
												// печать
												order_status = "000100";
												for (int j = 0; j < order_body.Count; j++)
												{
													if (order_body[j][5] == "2")
													{
														// обработка
														order_status = "000200";
														break;
													}
												}
											}

											query += "BEGIN TRANSACTION;\n\n";

											//клиент
											if (prop.Terminal_client_one)
												query += "DECLARE @CLIENTID int;\n" +
													"SET @CLIENTID = " + client_id + ";\n" +
													"DECLARE @CLIENTNAME nchar(255);\n" +
													"SET @CLIENTNAME = 'фототерминал'\n";
											else
												query += "DECLARE @name nchar(255)\n" +
														"DECLARE @phone nchar(255)\n" +
														"SET @name = '" + client_name + "%'\n" +
														"SET @phone = '" + client_phone + "'\n" +
														"DECLARE @CNT int\n" +
														"DECLARE @CLIENTID int;\n" +
														"SET @CLIENTID = 0;\n" +
														"SET @CNT = (\n" +
														"SELECT COUNT(*)\n" +
														"FROM [dbo].[client]\n" +
														"WHERE [name] like @name\n" +
														"  AND [id_category] = 9\n" +
														"  AND [phone_1] = @phone\n" +
														")\n" +
														"IF (@CNT = 0)\n" +
														"BEGIN\n" +
														"	INSERT INTO [dbo].[client]\n" +
														"			   ([id_category]\n" +
														"			   ,[guid]\n" +
														"			   ,[del]\n" +
														"			   ,[name]\n" +
														"			   ,[phone_1]\n" +
														"			   )\n" +
														"		 VALUES\n" +
														"			   (9\n" +
														"			   ,newid()\n" +
														"			   ,0\n" +
														"			   ," + client_name + "\n" +
														"			   ," + client_phone + ")\n" +
														"	SET @CLIENTID = scope_identity()\n" +
														"END\n" +
														"IF (@CNT = 1)\n" +
														"BEGIN\n" +
														"	SET @CLIENTID = (\n" +
														"		SELECT [id_client]\n" +
														"		FROM [dbo].[client]\n" +
														"		WHERE [name] like @name\n" +
														"		  AND [id_category] = 9\n" +
														"		  AND [phone_1] = @phone\n" +
														"	)\n" +
														"END\n" +
														"IF (@CNT > 1)\n" +
														"BEGIN\n" +
														"	SET @CLIENTID = (\n" +
														"		SELECT MAX([id_client]) AS id_client\n" +
														"		FROM dbo.client\n" +
														"		WHERE [name] like @name\n" +
														"		  AND [id_category] = 9\n" +
														"		  AND [phone_1] = @phone\n" +
														"	)\n" +
														"END\n" +
														"SELECT @CLIENTID;\n" +
														"DECLARE @CLIENTNAME nchar(255);\n" +
														"SET @CLIENTNAME = " + client_name + "\n";

											// Шапка
											query += "INSERT INTO [dbo].[order]\n" +
												   "([id_user_accept] \n" +
												   ",[id_user_operator] \n" +
												   ",[id_user_designer] \n" +
												   ",[id_user_delivery] \n" +
												   ",[id_client] \n" +
												   ",[guid] \n" +
												   ",[del] \n" +
												   ",[name_accept] \n" +
												   ",[name_operator] \n" +
												   ",[name_designer] \n" +
												   ",[name_delivery] \n" +
												   ",[status] \n" +
												   ",[number] \n" +
												   ",[input_date] \n" +
												   ",[expected_date] \n" +
												   ",[advanced_payment] \n" +
												   ",[final_payment] \n" +
												   ",[discont_percent] \n" +
												   ",[discont_code] \n" +
												   ",[preview] \n" +
												   ",[comment] \n" +
												   ",[crop] \n" +
												   ",[type] \n" +
												   ",[exported] \n" +
												   ",[bonus]) \n" +
											 "VALUES \n" +
												   "(" + user_id + " \n" +
												   ",0 \n" +
												   ",0 \n" +
												   ",0 \n" +
												   ",@CLIENTID \n" +
												   ",NEWID() \n" +
												   ",0 \n" +
												   ",'Фототерминал' \n" +
												   ",'' \n" +
												   ",'' \n" +
												   ",'' \n" +
												   ",'" + order_status + "' \n" +
												   ",'" + order_number + "' \n" +
												   ",GETDATE() \n" +
												   ",DATEADD(hour, 1, GETDATE()) \n" +
												   "," + order_payment.ToString().Replace(',', '.') + " \n" +
												   ",0 \n" +
												   ",0 \n" +
												   ",'' \n" +
												   ",0 \n" +
												   ",'" + order_comment + " " + materials + "' \n" +
												   ",1 \n" +
												   ",1 \n" +
												   ",0 \n" +
												   ",0);\n";
											// Код нового заказа
											query += "DECLARE @ID int;\n";
											query += "SET @ID = SCOPE_IDENTITY();\n";

											for (int j = 0; j < order_body.Count; j++)
											{
												//Табличная часть
												query += "INSERT INTO [dbo].[orderbody] \n" +
													   "([id_order] \n" +
													   ",[id_mashine] \n" +
													   ",[id_material] \n" +
													   ",[id_good] \n" +
													   ",[guid] \n" +
													   ",[del] \n" +
													   ",[quantity] \n" +
													   ",[actual_quantity] \n" +
													   ",[sign] \n" +
													   ",[price] \n" +
													   ",[dateadd] \n" +
													   ",[id_user_add] \n" +
													   ",[name_add] \n" +
													   "" + ((order_status == "100000") ? ",[datework] \n" : "\n") +
													   "" + ((order_status == "100000") ? ",[id_user_work] \n" : "\n") +
													   "" + ((order_status == "100000") ? ",[name_work] \n" : "\n") +
													   ",[exported] \n" +
													   ",[comment]) \n" +
												 "VALUES \n" +
													   "(@ID \n" +
													   ",0 \n" +
													   ",0 \n" +
													   ",'" + order_body[j][0] + "' \n" +
													   ",newid() \n" +
													   ",0 \n" +
													   "," + order_body[j][2].Replace(',', '.') + " \n" +
													   "," + order_body[j][2].Replace(',', '.') + " \n" +
													   ",'+' \n" +
													   "," + order_body[j][3].Replace(',', '.') + " \n" +
													   ",getdate() \n" +
													   "," + user_id + " \n" +
													   ",'Фототерминал' \n" +
													   "" + ((order_status == "100000") ? ",getdate() \n" : " \n") +
													   "" + ((order_status == "100000") ? "," + user_id + " \n" : " \n") +
													   "" + ((order_status == "100000") ? ",'Фототерминал' \n" : " \n") +
													   ",0\n" +
													   ",'" + order_body[j][6] + "');\n";
											}
											//query += "SELECT @ID";
											query += "COMMIT;\n\n";

										}

										// запрос придумали, теперь пытаемся скопировать данные
										try
										{
											if (!Directory.Exists(prop.Dir_print + "\\" + DateTime.Now.Year.ToString("D4")))
												Directory.CreateDirectory(prop.Dir_print + "\\" + DateTime.Now.Year.ToString("D4"));
											if (!Directory.Exists(prop.Dir_print + "\\" + DateTime.Now.Year.ToString("D4") + "\\" + DateTime.Now.Month.ToString("D2")))
												Directory.CreateDirectory(prop.Dir_print + "\\" + DateTime.Now.Year.ToString("D4") + "\\" + DateTime.Now.Month.ToString("D2"));
											if (!Directory.Exists(prop.Dir_print + "\\" + DateTime.Now.Year.ToString("D4") + "\\" + DateTime.Now.Month.ToString("D2") + "\\" + DateTime.Now.Day.ToString("D2")))
												Directory.CreateDirectory(prop.Dir_print + "\\" + DateTime.Now.Year.ToString("D4") + "\\" + DateTime.Now.Month.ToString("D2") + "\\" + DateTime.Now.Day.ToString("D2"));
											Directory.Move(o.FullName, prop.Dir_print + "\\" + DateTime.Now.Year.ToString("D4") + "\\" + DateTime.Now.Month.ToString("D2") + "\\" + DateTime.Now.Day.ToString("D2") + "\\" + order_number);
											while(rep.IsBusy)
											{
												Application.DoEvents();
											}
											cmd = new SqlCommand(query, con);
											cmd.ExecuteNonQuery();
											if (PrintCheck(con, order_number))
											{
												icon.BalloonTipText = "Принят заказ № " + order_number;
												icon.ShowBalloonTip(10000);
											}

										}
										catch(Exception ex)
										{
											MessageBox.Show(ex.Message);
										}

									}
									//папка заказа свободна
								}
							}

						}
					}
					else
					{
						icon.BalloonTipText = "Папка " + k.Path.Trim() + " не найдена!";
						icon.ShowBalloonTip(5000);
					}
					 */
						#endregion
						SqlCommand cmd_orders = new SqlCommand("SELECT * FROM kiosk_orders WHERE id_kiosk = " + k.Code, con);
						SqlDataAdapter da_orders = new SqlDataAdapter(cmd_orders);
						DataTable t_orders = new DataTable("orders");
						da_orders.Fill(t_orders);
						string t_orders_list = "";
						//for (int i = 0; i < t_orders.Rows.Count; i++)
						//{
						//	t_orders_list += t_orders.Rows[i]["orderno"].ToString() + ", ";
						//}
						t_orders_list += "0";
						FbConnectionStringBuilder cnString = new FbConnectionStringBuilder();
						cnString.DataSource = k.Path.Split('@')[0];
						cnString.Database = k.Path.Split('@')[1];
						cnString.UserID = "SYSDBA";
						cnString.Password = "masterkey";
						cnString.Charset = "win1251";
						cnString.Dialect = 3;
						try
						{
							using (FbConnection cn = new FbConnection(cnString.ToString()))
							{
								icon.Icon = wicob.Icon;
								cn.Open();
								FbCommand cmd_o = new FbCommand("CREATE TABLE SESSIONS_OK (ID INTEGER);", cn);
								try
								{
									cmd_o.ExecuteNonQuery();
									FbCommand cmd_oo = new FbCommand();
									cmd_oo.Connection = cn;
									for (int i = 0; i < t_orders.Rows.Count; i++)
									{
										cmd_oo.CommandText = "INSERT INTO SESSIONS_OK (SESSIONS_OK.ID) VALUES (" + 
															t_orders.Rows[i]["orderno"].ToString() + 
															")";
										try
										{
											cmd_o.ExecuteNonQuery();
										}
										catch (Exception ex)
										{
											icon.BalloonTipText = "Ошибка: " + ex.Message + "\n" + ex.Source;
											icon.ShowBalloonTip(10000);
											return;
										}
									}
								}
								catch { }
								/*
								cmd_o.CommandText = "DELETE FROM SESSIONS_OK";
								try
								{
									cmd_o.ExecuteNonQuery();
								}
								catch (Exception ex)
								{
									icon.BalloonTipText = "Ошибка: " + ex.Message + "\n" + ex.Source;
									icon.ShowBalloonTip(10000);
									return;
								}
								pb.Minimum = 0;
								pb.Maximum = t_orders.Rows.Count;
								pb.Value = 0;
								for (int i = 0; i < t_orders.Rows.Count; i++)
								{
									cmd_o.CommandText = "INSERT INTO SESSIONS_OK (SESSIONS_OK.ID) VALUES (" + t_orders.Rows[i]["orderno"].ToString() + ")";
									try
									{
										cmd_o.ExecuteNonQuery();
									}
									catch (Exception ex)
									{
										icon.BalloonTipText = "Ошибка: " + ex.Message + "\n" + ex.Source;
										icon.ShowBalloonTip(10000);
										return;
									}
									pb.Value++;
									Application.DoEvents();
								}
								pb.Value = 0;*/
								FbCommand cmd_korders = new FbCommand("SELECT * FROM SESSIONS WHERE SESSIONS.ID NOT IN (SELECT SESSIONS_OK.ID FROM SESSIONS_OK) AND (SESSIONS.APPROVED = 1" + ((prop.LoadNotApproved) ? " OR SESSIONS.APPROVED = -1" : "") + ((prop.Load1000) ? " OR SESSIONS.APPROVED = -1000" : "") + ")", cn);
								FbDataAdapter da_korders = new FbDataAdapter(cmd_korders);
								DataTable t_korders = new DataTable("korders");
								da_korders.Fill(t_korders);

								string order_comment = "Терминальный киоск: " + k.Code + ", ";
								string client_name = "";
								string client_phone = "";
								string order_number = "";
								decimal order_payment = 0;
								DataTable od = new DataTable("orderdetail");
								od.Columns.Add("ACTION_ID");
								od.Columns.Add("SUBACTION_ID");
								od.Columns.Add("QTY");
								od.Columns.Add("PRICE");
								od.Columns.Add("ACTION_NAME");
								od.Columns.Add("ACTION_HEADER");
								od.Columns.Add("ACTION_PRICE");
								od.Columns.Add("SUBACTION_NAME");
								od.Columns.Add("SUBACTION_HEADER");
								od.Columns.Add("SUBACTION_PRICE");
								od.Columns.Add("KEY");
								od.Columns.Add("COMMENT");
								od.Columns.Add("PATH");




								for (int ko = 0; ko < t_korders.Rows.Count; ko++)
								{
									od.Rows.Clear();
									order_number = prop.Order_terminal_prefics +
										k.Code.ToString("D3") +
										int.Parse(t_korders.Rows[ko]["ID"].ToString()).ToString("D7");

									client_name = ((prop.Terminal_client_one) ? "Фототерминал" : (((t_korders.Rows[ko]["CUSTOMER"].ToString().Trim()) == "") ? "Клиент фототерминала" : t_korders.Rows[ko]["CUSTOMER"].ToString().Trim()));
									client_phone = ((prop.Terminal_client_one) ? "" : t_korders.Rows[ko]["PHONE"].ToString().Trim());

									FbCommand cmd_temp = new FbCommand("SELECT SESSION_DETAIL.ACTION_ID, SESSION_DETAIL.SUBACTION_ID, SUM(SESSION_DETAIL.QTY) AS QTY, SESSION_DETAIL.PRICE FROM SESSION_DETAIL WHERE (SESSION_DETAIL.SID = " + t_korders.Rows[ko]["ID"].ToString() + ") GROUP BY SESSION_DETAIL.ACTION_ID, SESSION_DETAIL.SUBACTION_ID, SESSION_DETAIL.PRICE", cn);
									FbDataAdapter da = new FbDataAdapter(cmd_temp);
									DataTable t1 = new DataTable("orderdetail");
									da.Fill(t1);

									cmd_temp = new FbCommand("SELECT PRICE_LISTS.ACTION_NAME, PRICE_LISTS.PRICE, PRICEHEADER.HEADER, PRICE_LISTS.ID FROM PRICEHEADER INNER JOIN PRICE_LISTS ON (PRICEHEADER.ID = PRICE_LISTS.HID) WHERE (PRICE_LISTS.GID = 0)", cn);
									da = new FbDataAdapter(cmd_temp);
									DataTable t2 = new DataTable("price");
									da.Fill(t2);

									for (int i = 0; i < t1.Rows.Count; i++)
									{
										DataRow r = od.NewRow();
										for (int j = 0; j < t2.Rows.Count; j++)
										{
											if (t1.Rows[i]["ACTION_ID"].ToString().Trim() == t2.Rows[j]["ID"].ToString().Trim())
											{
												r["ACTION_NAME"] = t2.Rows[j]["ACTION_NAME"].ToString();
												r["ACTION_HEADER"] = t2.Rows[j]["HEADER"].ToString();
												r["ACTION_PRICE"] = t2.Rows[j]["PRICE"].ToString();
												break;
											}
										}
										for (int j = 0; j < t2.Rows.Count; j++)
										{
											if (t1.Rows[i]["SUBACTION_ID"].ToString().Trim() == t2.Rows[j]["ID"].ToString().Trim())
											{
												r["SUBACTION_NAME"] = t2.Rows[j]["ACTION_NAME"].ToString();
												r["SUBACTION_HEADER"] = t2.Rows[j]["HEADER"].ToString();
												r["SUBACTION_PRICE"] = t2.Rows[j]["PRICE"].ToString();
												break;
											}
										}
										r["ACTION_ID"] = t1.Rows[i]["ACTION_ID"];
										r["SUBACTION_ID"] = t1.Rows[i]["SUBACTION_ID"];
										r["QTY"] = t1.Rows[i]["QTY"];
										r["PRICE"] = t1.Rows[i]["PRICE"];
										od.Rows.Add(r);
									}

									for (int j = 0; j < od.Rows.Count; j++)
									{
										// определяем ключи для услуг
										cmd = new SqlCommand("SELECT id_good, type FROM dbo.good WHERE (kiosk_name LIKE '%|" + od.Rows[j]["ACTION_HEADER"].ToString().Trim() + " " + od.Rows[j]["ACTION_NAME"].ToString().Trim() + "|%')", con);
										SqlDataAdapter da_tmp = new SqlDataAdapter(cmd);
										DataSet ds = new DataSet();
										da_tmp.Fill(ds, "goods");
										if (ds.Tables[0].Rows.Count > 0)
										{

											od.Rows[j]["KEY"] = ds.Tables[0].Rows[0][0].ToString().Trim();
											od.Rows[j]["PATH"] = ds.Tables[0].Rows[0][1].ToString().Trim();
										}
										else
										{
											od.Rows[j]["KEY"] = "-1";
											od.Rows[j]["PATH"] = "1";
										}
										order_payment += 0; //decimal.Parse(order_body[j][4].Replace(',', '.'));
									}

									// теперь собираем запрос на добавление
									// не выпендриваемся и делаем с транзакцией
									// сперва шапка


									// статус (назначение) определяется услугами
									string order_status = "";
									// если в папке заказа нет подпапок с файлами, 
									// то считаем, что прошла моментальная печать
									//if (o.GetDirectories().Length == 0)
									//	order_status = "100000";
									//else
									//{
									// печать
									order_status = "000100";
									for (int j = 0; j < od.Rows.Count; j++)
									{
										if ((("_" + od.Rows[j]["ACTION_HEADER"].ToString()).ToUpper().IndexOf("МОМЕНТАЛЬ") > 0) ||
											(("_" + od.Rows[j]["ACTION_NAME"].ToString()).ToUpper().IndexOf("ЗАПИСЬ CD") > 0))
										{
											order_status = "000000";
										}
										else if (od.Rows[j]["PATH"].ToString() == "2")
										{
											// обработка
											order_status = "000200";
											break;
										}
									}
									//}
									string query = "";
									query += "BEGIN TRANSACTION;\n\n";

									//клиент
									if (prop.Terminal_client_one)
										query += "DECLARE @CLIENTID int;\n" +
											"SET @CLIENTID = " + client_id + ";\n" +
											"DECLARE @CLIENTNAME nchar(255);\n" +
											"SET @CLIENTNAME = 'фототерминал'\n";
									else
										query += "DECLARE @name nchar(255)\n" +
												"DECLARE @phone nchar(255)\n" +
												"SET @name = '" + client_name + "%'\n" +
												"SET @phone = '" + client_phone + "'\n" +
												"DECLARE @CNT int\n" +
												"DECLARE @CLIENTID int;\n" +
												"SET @CLIENTID = 0;\n" +
												"SET @CNT = (\n" +
												"SELECT COUNT(*)\n" +
												"FROM [dbo].[client]\n" +
												"WHERE [name] like @name\n" +
												"  AND [id_category] = 9\n" +
												"  AND [phone_1] = @phone\n" +
												")\n" +
												"IF (@CNT = 0)\n" +
												"BEGIN\n" +
												"	INSERT INTO [dbo].[client]\n" +
												"			   ([id_category]\n" +
												"			   ,[guid]\n" +
												"			   ,[del]\n" +
												"			   ,[name]\n" +
												"			   ,[phone_1]\n" +
												"			   )\n" +
												"		 VALUES\n" +
												"			   (9\n" +
												"			   ,newid()\n" +
												"			   ,0\n" +
												"			   ,'" + client_name + "'\n" +
												"			   ,'" + client_phone + "')\n" +
												"	SET @CLIENTID = scope_identity()\n" +
												"END\n" +
												"IF (@CNT = 1)\n" +
												"BEGIN\n" +
												"	SET @CLIENTID = (\n" +
												"		SELECT [id_client]\n" +
												"		FROM [dbo].[client]\n" +
												"		WHERE [name] like @name\n" +
												"		  AND [id_category] = 9\n" +
												"		  AND [phone_1] = @phone\n" +
												"	)\n" +
												"END\n" +
												"IF (@CNT > 1)\n" +
												"BEGIN\n" +
												"	SET @CLIENTID = (\n" +
												"		SELECT MAX([id_client]) AS id_client\n" +
												"		FROM dbo.client\n" +
												"		WHERE [name] like @name\n" +
												"		  AND [id_category] = 9\n" +
												"		  AND [phone_1] = @phone\n" +
												"	)\n" +
												"END\n" +
												"SELECT @CLIENTID;\n" +
												"DECLARE @CLIENTNAME nchar(255);\n" +
												"SET @CLIENTNAME = '" + client_name + "'\n";

									// Шапка
									query += "DECLARE @F int;\n";
									query += "SET @F = (SELECT COUNT(*) FROM [dbo].[order] WHERE [order].[number] like '%" + order_number + "%');\n";
									query += "IF (@F = 0)\n";
									query += "BEGIN\n";
									query += "INSERT INTO [dbo].[order]\n" +
										   "([id_user_accept] \n" +
										   ",[id_user_operator] \n" +
										   ",[id_user_designer] \n" +
										   ",[id_user_delivery] \n" +
										   ",[id_client] \n" +
										   ",[guid] \n" +
										   ",[del] \n" +
										   ",[name_accept] \n" +
										   ",[name_operator] \n" +
										   ",[name_designer] \n" +
										   ",[name_delivery] \n" +
										   ",[status] \n" +
										   ",[number] \n" +
										   ",[input_date] \n" +
										   ",[expected_date] \n" +
										   ",[advanced_payment] \n" +
										   ",[final_payment] \n" +
										   ",[discont_percent] \n" +
										   ",[discont_code] \n" +
										   ",[preview] \n" +
										   ",[comment] \n" +
										   ",[crop] \n" +
										   ",[type] \n" +
										   ",[exported] \n" +
										   ",[bonus]) \n" +
									 "VALUES \n" +
										   "(" + user_id + " \n" +
										   ",0 \n" +
										   ",0 \n" +
										   ",0 \n" +
										   ",@CLIENTID \n" +
										   ",NEWID() \n" +
										   ",0 \n" +
										   ",'Фототерминал' \n" +
										   ",'' \n" +
										   ",'' \n" +
										   ",'' \n" +
										   ",'" + order_status + "' \n" +
										   ",'" + order_number + "' \n" +
										   ",GETDATE() \n" +
										   ",DATEADD(hour, 1, GETDATE()) \n" +
										   "," + order_payment.ToString().Replace(',', '.') + " \n" +
										   ",0 \n" +
										   ",0 \n" +
										   ",'' \n" +
										   ",0 \n" +
										   ",'" + order_comment + "' \n" +
										   ",1 \n" +
										   ",1 \n" +
										   ",0 \n" +
										   ",0);\n";
									// Код нового заказа
									query += "DECLARE @ID int;\n";
									query += "SET @ID = SCOPE_IDENTITY();\n";

									for (int j = 0; j < od.Rows.Count; j++)
									{
										//Табличная часть
										query += "INSERT INTO [dbo].[orderbody] \n" +
											   "([id_order] \n" +
											   ",[id_mashine] \n" +
											   ",[id_material] \n" +
											   ",[id_good] \n" +
											   ",[guid] \n" +
											   ",[del] \n" +
											   ",[quantity] \n" +
											   ",[actual_quantity] \n" +
											   ",[sign] \n" +
											   ",[price] \n" +
											   ",[dateadd] \n" +
											   ",[id_user_add] \n" +
											   ",[name_add] \n" +
											   "" + (((order_status == "100000") || (order_status == "000000")) ? ",[datework] \n" : "\n") +
											   "" + (((order_status == "100000") || (order_status == "000000")) ? ",[id_user_work] \n" : "\n") +
											   "" + (((order_status == "100000") || (order_status == "000000")) ? ",[name_work] \n" : "\n") +
											   ",[exported] \n" +
											   ",[comment]) \n" +
										 "VALUES \n" +
											   "(@ID \n" +
											   ",0 \n" +
											   ",0 \n" +
											   ",'" + od.Rows[j]["KEY"].ToString().Trim() + "' \n" +
											   ",newid() \n" +
											   ",0 \n" +
											   "," + od.Rows[j]["QTY"].ToString().Trim().Replace(',', '.') + " \n" +
											   "," + od.Rows[j]["QTY"].ToString().Trim().Replace(',', '.') + " \n" +
											   ",'+' \n" +
											   "," + od.Rows[j]["PRICE"].ToString().Trim().Replace(',', '.') + " \n" +
											   ",getdate() \n" +
											   "," + user_id + " \n" +
											   ",'Фототерминал' \n" +
											   "" + (((order_status == "100000") || (order_status == "000000")) ? ",getdate() \n" : " \n") +
											   "" + (((order_status == "100000") || (order_status == "000000")) ? "," + user_id + " \n" : " \n") +
											   "" + (((order_status == "100000") || (order_status == "000000")) ? ",'Фототерминал' \n" : " \n") +
											   ",0\n" +
											   ",'" + od.Rows[j]["ACTION_HEADER"].ToString().Trim() + " " + od.Rows[j]["ACTION_NAME"].ToString().Trim() + " " + od.Rows[j]["QTY"].ToString().Trim() + "*" + od.Rows[j]["ACTION_PRICE"].ToString().Trim() + "р; " + od.Rows[j]["SUBACTION_HEADER"].ToString().Trim() + " " + od.Rows[j]["SUBACTION_NAME"].ToString().Trim() + " " + od.Rows[j]["SUBACTION_PRICE"].ToString().Trim() + "р');\n";
									}
									//query += "SELECT @ID";
									query += "INSERT INTO [dbo].[kiosk_orders]\n" +
											"([id_kiosk]\n" +
											",[orderno])\n" +
											"VALUES \n" +
											"(" + k.Code.ToString() + "\n" +
											"," + t_korders.Rows[ko]["ID"].ToString() + ");\n";
									string _query = "INSERT INTO [dbo].[kiosk_orders]\n" +
											"([id_kiosk]\n" +
											",[orderno])\n" +
											"VALUES \n" +
											"(" + k.Code.ToString() + "\n" +
											"," + t_korders.Rows[ko]["ID"].ToString() + ");\n";
									query += "INSERT INTO [dbo].[orderevent] \n" +
											   "([del] \n" +
											   ",[guid] \n" +
											   ",[id_order] \n" +
											   ",[event_date] \n" +
											   ",[event_user] \n" +
											   ",[event_status] \n" +
											   ",[event_point] \n" +
											   ",[event_text]) \n" +
										 "VALUES \n" +
											   "(0 \n" +
											   ",newid() \n" +
											   ",@ID \n" +
											   ",getdate() \n" +
											   "," + user_id + " \n" +
											   ",'" + order_status + "' \n" +
											   ",'" + prop.Order_prefics + "' \n" +
											   ",'Заказ автоматически принят роботом для фототерминалов.');\n";
									query += "END\n";
									query += "COMMIT;\n\n";
									while (rep.IsBusy)
									{
										Application.DoEvents();
									}
									icon.Icon = wicor.Icon;
									//SqlCommand cmd_addorder = new SqlCommand(query, con);
									//cmd_addorder.CommandTimeout = 9000;
									//cmd_addorder.ExecuteNonQuery();
									SqlCommand cmd_add = new SqlCommand();
									cmd_add.Connection = con;
									cmd_add.CommandTimeout = 9000;
									cmd_add.CommandText = "SELECT COUNT(*) FROM [order] WHERE [order].[number] = '" + order_number + "'";
									int order_count = (int)cmd_add.ExecuteScalar();
									if((order_count) > 0)
									{
										cmd_add.CommandText = _query;
										cmd_add.ExecuteNonQuery();
										icon.BalloonTipText = "Заказ № " + order_number + " уже был импортирован ранее";
										icon.ShowBalloonTip(10000);
										FbCommand fin = new FbCommand("INSERT INTO SESSIONS_OK (SESSIONS_OK.ID) VALUES (" +
															int.Parse(t_korders.Rows[ko]["ID"].ToString()).ToString("D7") +
															")", cn);
										fin.ExecuteNonQuery();
									}
									else
									{
										cmd_add.CommandText = query;
										cmd_add.ExecuteNonQuery();
										icon.Icon = wicoe.Icon;
										if (PrintCheck(con, order_number))
										{
											icon.BalloonTipText = "Принят заказ № " + order_number;
											icon.ShowBalloonTip(10000);
											FbCommand fin = new FbCommand("INSERT INTO SESSIONS_OK (SESSIONS_OK.ID) VALUES (" +
															int.Parse(t_korders.Rows[ko]["ID"].ToString()).ToString("D7") +
															")", cn);
											fin.ExecuteNonQuery();
										}
									}


								}
								cn.Close();


							}
						}
						catch (Exception ex)
						{
							icon.BalloonTipText = "Ошибка: " + ex.Message + "\n" + ex.Source;
							icon.ShowBalloonTip(10000);
						}

					}


				}
			}
			catch (Exception ex)
			{
				icon.BalloonTipText = "Ошибка: " + ex.Message + "\n" + ex.Source;
				icon.ShowBalloonTip(10000);
			}
			icon.Icon = (System.Drawing.Icon)resources.GetObject("icon.Icon");
			//tmr.Start();

		}

		private void search1ToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SearchOrders();
		}

		private bool PrintCheck(SqlConnection db_connection, string num)
		{

			// Печатаем чек
			bool ret = true;
			if (prop.Terminal_print_check)
			{
				OrderInfo prnOrder = new OrderInfo(db_connection, num, true);
				try
				{
					if (prop.PathReportsTemplates != "")
					{
						rep.Load(prop.PathReportsTemplates, "Check2");
						rep.DataSource.Recordset = prnOrder.OrderBody;
						decimal itog = 0;
						decimal iitog = 0;
						for (int i = 0; i < prnOrder.OrderBody.Rows.Count; i++)
						{
							itog += decimal.Parse(prnOrder.OrderBody.Rows[i]["price"].ToString()) *
									decimal.Parse(prnOrder.OrderBody.Rows[i]["actual_quantity"].ToString());
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
						rep.Fields["TypePaper"].Text = "";
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
					MessageBox.Show("Ошибка вывода чека\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			return ret;

		}

        private void AddEvent(string Event, int id, SqlConnection db_connection)
        {
            try
            {
                OrderInfo order = new OrderInfo(db_connection, id);
                string body = "";
                for (int i = 0; i < order.OrderBody.Rows.Count; i++)
                {
                    body += order.OrderBody.Rows[i][9].ToString() + "|" +
                            order.OrderBody.Rows[i][1].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][3].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][4].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][5].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][6].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][7].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][18].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][13].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][11].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][16].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][17].ToString().Trim();
                    body += "#";
                }
                body = body.Substring(0, body.Length - 1);
                body = "$$" + body + "$$" + order.AdvancedPayment + "$$" + order.FinalPayment + "$$" + order.Bonus;
                SqlCommand _cmd = new SqlCommand("INSERT INTO [dbo].[orderevent] ([id_order], [event_user], [event_status], [event_point], [event_text]) VALUES (" +
                            order.Id + ", 'Фототерминал', '" + order.Distanation + "', '" + prop.Order_prefics.Trim() +
                            "', '" + Event + body + "')", db_connection);
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }

		private void btnCallTerminal_Click(object sender, EventArgs e)
		{
			SearchOrders();
		}


	}
}
