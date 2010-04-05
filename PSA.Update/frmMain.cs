using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Net;
using PSA.Update.Util;

namespace PSA.Update
{
    public partial class frmMain : Form
    {
		private string root = System.Environment.GetCommandLineArgs()[0].Substring(
				0,
				System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
				);
		private bool busy = false;
		private string date = DateTime.Now.Year.ToString("D4") + "-" +
							  DateTime.Now.Month.ToString("D2") + "-" +
							  DateTime.Now.Day.ToString("D2") + "_" +
							  DateTime.Now.Hour.ToString("D2") + "-" +
							  DateTime.Now.Minute.ToString("D2") + "-" +
							  DateTime.Now.Second.ToString("D2");

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
			this.Text = "Обновление - Photoland System Automation " + Application.ProductVersion;
			SetInfo("Начинаем обновление");
			btnOK.Enabled = false;
			btnInfo.Enabled = false;
			busy = true;
			try
			{
				
				SetInfo("Создание временной папки");
				if (CheckTempDir())
				{
					SetInfo("Установление связи с сервером обновлений");
					try
					{
						string _server = "", _port = "", _user = "", _password = "", _dir = "";
						ini updateIni = new ini(root + "\\PSA.Update.ini");
						_server = updateIni.IniReadValue("update", "server", "int.fotoland.ru");
						_port = updateIni.IniReadValue("update", "port", "21");
						_user = updateIni.IniReadValue("update", "user", "anonymous");
						_password = updateIni.IniReadValue("update", "password", "none@none");
						_dir = updateIni.IniReadValue("update", "dir", "/PSA/_Update/");

						string _kill = "", _version = "";
						bool _backup = false, _feedback = false, _new = false, found = false;
						ftpClient ftp = new ftpClient(_server + ":" + _port, _user, _password);
						string[] files = ftp.GetFileList(_dir);
						foreach (string ftpe in ftp.Errors)
						{
							SetInfo("Ftp: " + ftpe);
						}
						ftp.Errors.Clear();
						foreach (string _file in files)
						{
							if (_file == "Update.ini")
							{
								bool ok = true;
								SetInfo("Получаем информацию об обновлении");
								if (!ftp.Download(root + "\\tmp\\", _file, _dir, _file))
								{
									System.Threading.Thread.Sleep(2000);
									if (!ftp.Download(root + "\\tmp\\", _file, _dir, _file))
									{
										System.Threading.Thread.Sleep(2000);
										if (!ftp.Download(root + "\\tmp\\", _file, _dir, _file))
										{
											ok = false;
										}
									}
								}
								if (ok)
								{
									ini infoIni = new ini(root + "\\tmp\\" + _file);
									_kill = infoIni.IniReadValue("update", "kill", "Photoland.Acceptance,Photoland.Administrator," +
																					"Photoland.Designer,Photoland.Exchanger," +
																					"Photoland.GiveMe,Photoland.Operator," +
																					"Photoland.Robot,PSA.Inventory,PSA.Kiosk," +
																					"PSA.Robot,PSA.Tools");
									_backup = bool.Parse(infoIni.IniReadValue("update", "backup", "true"));
									_feedback = bool.Parse(infoIni.IniReadValue("update", "feedback", "true"));
									_new = bool.Parse(infoIni.IniReadValue("update", "shownew", "true"));
									_version = infoIni.IniReadValue("update", "version", "0.0");
									found = true;
									SetInfo("Информация об обновлении получена");
									SetInfo("Найдена версия " + _version);
									ftp.Errors.Clear();
								}
								else
								{
									foreach (string ftpe in ftp.Errors)
									{
										SetInfo("Ftp: " + ftpe);
									}
									ftp.Errors.Clear();
									SetInfo("Не удалось получить информацию об обновлении");
								}
							}
						}
						if ((found) && (_version != "0.0"))
						{
							SetInfo("Найдено обновление до версии " + _version);
							SetInfo("Начинаем получение обновления");
							string[] ufiles = ftp.GetFileList(_dir + "//UpdateFiles");
							foreach (string ftpe in ftp.Errors)
							{
								SetInfo("Ftp: " + ftpe);
							}
							ftp.Errors.Clear();
							p.Minimum = 0;
							p.Maximum = ufiles.Length;
							p.Value = 0;
							bool okinstall = true;
							foreach (string _file in ufiles)
							{
								SetInfo("Получение файла " + _file);
								bool ok = true;
								if (!ftp.Download(root + "\\tmp\\", _file, _dir + "//UpdateFiles", _file))
								{
									System.Threading.Thread.Sleep(2000);
									if (!ftp.Download(root + "\\tmp\\", _file, _dir + "//UpdateFiles", _file))
									{
										System.Threading.Thread.Sleep(2000);
										if (!ftp.Download(root + "\\tmp\\", _file, _dir + "//UpdateFiles", _file))
										{
											ok = false;
											okinstall = false;
										}
									}
								}
								p.Value++;
								if (!ok)
								{
									foreach (string ftpe in ftp.Errors)
									{
										SetInfo("Ftp: " + ftpe);
									}
									ftp.Errors.Clear();
									SetInfo("Ошибка получения файла " + _file);
									try
									{
										File.Delete(root + "\\tmp\\" +_file);
									}
									catch
									{
									}
								}
								ftp.Errors.Clear();
							}
							p.Value = 0;
							if (okinstall)
							{
								SetInfo("Подготовка к установке обновления");
								p.Maximum = _kill.Split(',').Length;
								p.Value = 0;
								foreach (string pr in _kill.Split(','))
								{
									Process[] plist = Process.GetProcessesByName(pr);
									foreach (Process _pr in plist)
									{
										SetInfo("Подготовка к установке обновления, завершаем процесс " + _pr.ProcessName);
										_pr.Kill();
									}
									p.Value++;
								}
								p.Value = 0;
								if (_backup)
								{
									try
									{
										SetInfo("Создаем резервную копию");
										if (!Directory.Exists(root + "\\backup"))
											Directory.CreateDirectory(root + "\\backup");
										if (!Directory.Exists(root + "\\backup\\" + date))
											Directory.CreateDirectory(root + "\\backup\\" + date);
										DirectoryInfo _root = new DirectoryInfo(root);
										p.Maximum = _root.GetFiles().Length;
										p.Value = 0;
										foreach (FileInfo _f in _root.GetFiles())
										{
											SetInfo("Создаем резервную копию, копируется файл " + _f.Name);
											File.Copy(_f.FullName, root + "\\backup\\" + date + "\\" + _f.Name, true);
											p.Value++;
										}
										p.Value = 0;
									}
									catch (Exception ex)
									{
										MessageBox.Show("Ошибка создания резервной копии.\n" + ex.Message + "\n" + ex.Source);
									}
								}
								try
								{
									SetInfo("Устанавливаем обновление");
									DirectoryInfo _tmp = new DirectoryInfo(root + "\\tmp\\");
									p.Maximum = _tmp.GetFiles().Length;
									p.Value = 0;
									foreach (FileInfo _f in _tmp.GetFiles())
									{
										SetInfo("Устанавливаем обновление, копируется файл " + _f.Name);
										if (_f.Length > 0)
										{
											File.Copy(_f.FullName, root + "\\" + _f.Name, true);
										}
										else
										{
											SetInfo("Размер файла " + _f.Name + ", 0 байт, возможно он ошибочный, устанавливаться не будет");
										}
										p.Value++;
									}
									p.Value = 0;
								}
								catch (Exception ex)
								{
									MessageBox.Show("Ошибка установка обновление.\n" + ex.Message + "\n" + ex.Source);
								}
								SetInfo("Обновление завершено");
								if (_feedback)
								{
									try
									{
										SetInfo("Отправляем журнал установки обновления");
										using (StreamWriter w = new StreamWriter(root + "\\tmp\\Update.log"))
										{
											for (int i = 0; i < txtInfo.Items.Count; i++)
											{
												w.WriteLine(txtInfo.Items[i]);
											}
											w.Close();
										}
										IPAddress[] ipa = Dns.GetHostAddresses(Dns.GetHostName());
										string hostname = "";
										foreach (IPAddress _ipa in ipa)
										{
											if (_ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
												hostname = _ipa.ToString();
										}
										if (hostname == "")
											hostname = Dns.GetHostName();
										if (!ftp.Upload(root + "\\tmp\\Update.log", _dir + "/Feedback/" + _version + "_" + hostname + ".log"))
										{
											System.Threading.Thread.Sleep(2000);
											if (!ftp.Upload(root + "\\tmp\\Update.log", _dir + "/Feedback/" + _version + "_" + hostname + ".log"))
											{
												System.Threading.Thread.Sleep(2000);
												if (!ftp.Upload(root + "\\tmp\\Update.log", _dir + "/Feedback/" + _version + "_" + hostname + ".log"))
												{
													SetInfo("Ошибка копирования журнала на сервер");
													foreach (string ftpe in ftp.Errors)
													{
														SetInfo("Ftp: " + ftpe);
													}
													ftp.Errors.Clear();
												}
											}
										}
									}
									catch (Exception ex)
									{
										MessageBox.Show("Ошибка отправки журнала обновлений.\n" + ex.Message + "\n" + ex.Source);
									}
								}
								if (_new)
								{
									if (File.Exists(root + "\\read.txt"))
									{
										using (StreamReader _f = new StreamReader(root + "\\read.txt", Encoding.GetEncoding(1251)))
										{
											string _info = _f.ReadToEnd();
											using (frmInfo frm = new frmInfo(_info))
											{
												frm.ShowDialog();
											}
										}
									}
								}
							}
							else
							{
								SetInfo("Ошибка при получении обновления");
							}
						}
					}
					catch (Exception ex)
					{
						MessageBox.Show("Ошибка соединения с сервером.\n" + ex.Message + "\n" + ex.Source);
					}
				}
				else
				{
					MessageBox.Show("Ошибка создания временной папки");
				}
            }
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
			btnOK.Enabled = true;
			btnInfo.Enabled = true;
			busy = false;
        }

        private bool CheckTempDir()
        {
            bool o = false;
            try
            {
                if (!Directory.Exists(root + "\\tmp"))
                {
                    Directory.CreateDirectory(root + "\\tmp");
                }
                o = true;
            }
            catch
            {
                o = false;
            }
            return o;
        }

		private void SetInfo(string info)
		{
			txtInfo.Items.Add(DateTime.Now.Year.ToString("D4") + "/" +
							  DateTime.Now.Month.ToString("D2") + "/" +
							  DateTime.Now.Day.ToString("D2") + " " +
							  DateTime.Now.Hour.ToString("D2") + ":" +
							  DateTime.Now.Minute.ToString("D2") + ":" +
							  DateTime.Now.Second.ToString("D2") + " " + info);
			txtInfo.SelectedIndex = txtInfo.Items.Count - 1;
			Application.DoEvents();
		}

		private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (busy)
				e.Cancel = true;
			else
				e.Cancel = false;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnInfo_Click(object sender, EventArgs e)
		{
			if (File.Exists(root + "\\read.txt"))
			{
				using (StreamReader _f = new StreamReader(root + "\\read.txt", Encoding.GetEncoding(1251)))
				{
					string _info = _f.ReadToEnd();
					using (frmInfo frm = new frmInfo(_info))
					{
						frm.ShowDialog();
					}
				}
			}
		}

		private void btnSaveLog_Click(object sender, EventArgs e)
		{
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				using (StreamWriter w = new StreamWriter(dlg.FileName, true, Encoding.GetEncoding(1251)))
				{
					for (int i = 0; i < txtInfo.Items.Count; i++)
					{
						w.WriteLine(txtInfo.Items[i].ToString());
					}
					w.Close();
				}

			}
		}
    }
}
