using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Forms.Interface;
using System.IO;
using Photoland.Lib;
using PSA.Lib.Interface;
using PSA.Lib.Util;

namespace Photoland.Robot
{
	public partial class frmMain : Form
	{
		public SqlConnection db_connection = new SqlConnection();
		public PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		private bool hidewin = true;
		private bool globalexit = false;

		private int img = 0;

		public frmMain()
		{
			InitializeComponent();
			this.Text = "Робот";
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			//MessageBox.Show("Используйте службу PSA.Robot!");
			//this.Close();
			
            try
            {
                StreamWriter sw = new StreamWriter(prop.Dir_export + "\\Robot_" + Environment.MachineName + "_" + Environment.UserName + ".info", false, Encoding.GetEncoding(1251));
                sw.Write("Date:      " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                         "\nMashine:   " + Environment.MachineName +
                         "\nUser:      " + Environment.UserName +
                         "\nRobot mod: " + Application.ProductVersion);
                sw.Close();
            }
            catch
            {
            }

			if (!Checking.checkVersion(Modules.Robot, Application.ProductVersion))
				if (MessageBox.Show("Внимание! Существует более новая версия модуля!\nУстановить обновление?", "Контроль обновлений", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
													0,
													System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
													) + "\\PSA.Update.cmd");

            bool tmp_exit = false;

			// Проверяем, если есть ограничение на запуск одной копии и программа уже запущена
			if ((app.Search_twin()) && (prop.Run_one_copy_robot))
			{
				// То выдаем сообщение и закрываем программу
				MessageBox.Show("Программа уже запущена!");
				hidewin = false;
				globalexit = true;
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
					ErrorNfo.WriteErrorInfo(ex);
					// Если не удалось подключиться к базе, то
					// выдаем сообщение об ошибке и открываем форму
					// подключения к базе данных
					MessageBox.Show("Ошибка подключения к базе данных!\n" + ex.Message + "\n" + ex.Source + "\nПроверьте настройки подключения!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					try
					{
						frmOptions fOptions = new frmOptions();
						fOptions.ShowDialog();
						// Опять пробуем подключиться к базе
						db_connection.ConnectionString = prop.Connection_string;
						db_connection.Open();
					}
					catch (Exception exc)
					{
						ErrorNfo.WriteErrorInfo(ex);
						// Если после второй попытки не удалось подключиться, то закрываем программу.
						MessageBox.Show("Ошибка подключения к базе данных!\n" + exc.Message + "\n" + exc.Source, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
						tmp_exit = true;
					}
				}

				if (!tmp_exit)
				{
                    try
                    {
                        tmrRobot.Interval = prop.Import_time * 1000 * 60;
                    }
                    catch(Exception ex)
                    {
						ErrorNfo.WriteErrorInfo(ex);
                        MessageBox.Show("Ошибка назначения интервала запуска импорта данных, проверьте настройки!",
                                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    try
                    {
                        tmrRobotExport.Interval = prop.Export_time * 1000 * 60;
                    }
                    catch(Exception ex)
                    {
						ErrorNfo.WriteErrorInfo(ex);
                        MessageBox.Show("Ошибка назначения интервала запуска экспорта данных, проверьте настройки!",
                                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    tmr.Start();
					tmrRobot.Start();
					tmrRobotExport.Start();
					this.Visible = false;
					wtl("\n[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
						"] # Начало работы робота \n");
				}
				else
				{
					Application.Exit();
				}
			}
		}

		private void OpenSettings()
		{
			frmSetup fOptions = new frmSetup();
			fOptions.ShowDialog();
			prop = new PSA.Lib.Util.Settings();
		}

		private void mnuSetup_Click(object sender, EventArgs e)
		{
			OpenSettings();
		}

		private void HideWindow()
		{
			this.Visible = false;
		}

		private void ShowWindow()
		{
			this.Visible = true;
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
						MessageBox.Show("выйти из программы?", "Робот", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
						DialogResult.OK)
					{
						e.Cancel = false;
					}
					else
					{
						e.Cancel = true;
						hidewin = true;
					}
				}
				else
				{
					e.Cancel = false;
				}
			}
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			hidewin = false;
			this.Close();
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			if (prop.Robot_animation_icon)
			{
				if (img > 9)
				{
					img = 0;
				}
				switch (img)
				{
					case 0:
						{
							icoTray.Icon = icoTray1.Icon;
							break;
						}
					case 1:
						{
							icoTray.Icon = icoTray2.Icon;
							break;
						}
					case 2:
						{
							icoTray.Icon = icoTray3.Icon;
							break;
						}
					case 3:
						{
							icoTray.Icon = icoTray4.Icon;
							break;
						}
					case 4:
						{
							icoTray.Icon = icoTray5.Icon;
							break;
						}
					case 5:
						{
							icoTray.Icon = icoTray6.Icon;
							break;
						}
					case 6:
						{
							icoTray.Icon = icoTray7.Icon;
							break;
						}
					case 7:
						{
							icoTray.Icon = icoTray8.Icon;
							break;
						}
					case 8:
						{
							icoTray.Icon = icoTray9.Icon;
							break;
						}
				}
				img++;
			}
		}

		private void icoTray_Click(object sender, EventArgs e)
		{
			ShowWindow();
		}

		private void icoTray_Click(object sender, MouseEventArgs e)
		{
			ShowWindow();
		}


		private void mnuHandImport_Click(object sender, EventArgs e)
		{
			tmrRobot_Tick(this, new EventArgs());
		}

		private void mnuHandExport_Click(object sender, EventArgs e)
		{
			DoExport();
		}

		
		private bool checkFiles(string file1, string file2)
		{
			bool ok = true;
			try
			{
				using(StreamReader f1 = new StreamReader(file1, Encoding.GetEncoding(1251)))
				{
					using(StreamReader f2 = new StreamReader(file2, Encoding.GetEncoding(1251)))
					{
					
						while((f1.Peek() > 0) && (ok))
						{
							try
							{
								if(f1.ReadLine() != f2.ReadLine())
									ok = false;
							}
							catch
							{
								ok = false;
							}
						}
					}
				}
			}
			catch
			{
				ok = false;
			}
			
			return ok;
		}

		private void tmrRobotExport_Tick(object sender, EventArgs e)
		{
			tmrRobotExport.Stop();
			DoExport();
			tmrRobotExport.Start();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Photoland.Forms.Interface.frmAbout f= new Photoland.Forms.Interface.frmAbout();
			f.ShowDialog();
		}

		private void wtl(string str)
		{
			try
			{
				string f = "Import_";
				f += DateTime.Now.Year.ToString() + "-";
				f += DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() + "-" : DateTime.Now.Month.ToString() + "-";
				f += DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
				f += ".log";

				StreamWriter l = new StreamWriter(prop.Dir_export + "\\" + f, true, Encoding.GetEncoding(1251));
				l.Write(str);
                if (txtLog.Text.Length > 500)
                    txtLog.Text = txtLog.Text.Substring(txtLog.Text.Length - 500);
				txtLog.Text += str;
				l.Close();
			}
			catch
			{
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DoClear();
		}

		private void DoClear()
		{
		}

		private void tmrCls_Tick(object sender, EventArgs e)
		{
			txtLog.Text = "";
		}

		private void tmrClear_Tick(object sender, EventArgs e)
		{
			DoClear();
		}

		private void semaphoresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSemaphores fOptions = new frmSemaphores();
			fOptions.ShowDialog();
			prop = new PSA.Lib.Util.Settings();
		}
	}
}