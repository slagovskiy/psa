using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib;
using PSA.Lib.Interface;
using PSA.Lib.Util;
using System.Data.SqlClient;
using System.IO;

namespace PSA.Inventory
{
	public partial class frmMain : PSA.Lib.Interface.Templates.frmTMdiParent
	{
		private PSA.Lib.Util.Settings settings = new PSA.Lib.Util.Settings();
        private Photoland.Security.User.UserInfo usr = new Photoland.Security.User.UserInfo();
	
		public frmMain()
		{
			InitializeComponent();
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			this.Title = "Inventory";
			this.InfoString = "Загружен модуль инвентаризации";
            try
            {
                StreamWriter sw = new StreamWriter(settings.Dir_export + "\\Inventory_" + Environment.MachineName + "_" + Environment.UserName + ".info", false, Encoding.GetEncoding(1251));
                sw.Write("Date:          " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                         "\nMashine:       " + Environment.MachineName +
                         "\nUser:          " + Environment.UserName +
                         "\nExchanger mod: " + Application.ProductVersion);
                sw.Close();
            }
            catch
            {
            }

			if (!Checking.checkVersion(Modules.Inventory, Application.ProductVersion))
				if (MessageBox.Show("Внимание! Существует более новая версия модуля!\nУстановить обновление?", "Контроль обновлений", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
													0,
													System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
													) + "\\PSA.Update.cmd");


            bool tmp_login_ok = false;
            bool tmp_exit = false;

            using (SqlConnection db_connection = new SqlConnection())
            {
                // Если ограничение на запуск одной копии пройдено, то продолжаем...
                // Открываем соединение с базой
                try
                {
                    db_connection.ConnectionString = settings.Connection_string;
                    db_connection.Open();
                }
                catch (Exception ex)
                {
                    //ErrorNfo.WriteErrorInfo(ex);
                    // Если не удалось подключиться к базе, то
                    // выдаем сообщение об ошибке и открываем форму
                    // подключения к базе данных
                    MessageBox.Show("Ошибка подключения к базе данных!\n" + ex.Message + "\n" + ex.Source + "\nПроверьте настройки подключения!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    try
                    {
                        frmSetup fOptions = new frmSetup();
                        fOptions.ShowDialog();
                        settings = new PSA.Lib.Util.Settings();
                        // Опять пробуем подключиться к базе
                        db_connection.ConnectionString = settings.Connection_string;
                        db_connection.Open();
                    }
                    catch (Exception exc)
                    {
                        //ErrorNfo.WriteErrorInfo(exc);
                        // Если после второй попытки не удалось подключиться, то закрываем программу.
                        MessageBox.Show("Ошибка подключения к базе данных!\n" + exc.Message + "\n" + exc.Source, "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tmp_exit = true;
                    }
                }

                if (!tmp_exit)
                {
                    // открываем окно запроса пользователя
                    frmLogin fLogin = new frmLogin();
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
                                    if (fLogin.usr.prmCanInventory)
                                    {
                                        this.Show();
                                    }
                                    else
                                    {
                                        tmp_exit = true;
                                        MessageBox.Show("Доступ в модуль инвентаризации заперщен!", "Вход в программу", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

		private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			PSA.Lib.Interface.frmSetup f = new frmSetup();
			f.ShowDialog();
			settings = new PSA.Lib.Util.Settings();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmDialogYesNo f = new frmDialogYesNo();
			f.Message = "Вы действительно хотите выйти из программы?";
			f.ShowDialog();
			if (f.DialogResult == DialogResult.Yes)
			{
				doExit();
			}
		}

		private void doExit()
		{
			Application.Exit();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmAbout f = new frmAbout();
			f.ShowDialog();
		}

		private void semaphoresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSemaphores f = new frmSemaphores();
			f.ShowDialog();
			settings = new PSA.Lib.Util.Settings();
		}

		private void NewInventorytoolStripMenuItem_Click(object sender, EventArgs e)
		{
			NewInventory();
		}

		private void NewInventory()
		{
			frmDialogYesNo f = new frmDialogYesNo();
			f.Message = "Начать инвентаризацию?\nВход в другие модули будет запрещен!";
			f.Title = "Инвентаризация";
			f.ShowDialog();
			if (f.DialogResult == DialogResult.Yes)
			{
				PSA.Lib.Util.Semaphore.semInventory = true;
				settings = new Settings();
				frmInventoryDoc fi = new frmInventoryDoc(usr);
				fi.Title = "Новый документ инвентаризации";
				fi.ShowDialog();
			}

		}

		private void NewVerification()
		{
			frmVerificationDoc fi = new frmVerificationDoc(usr);
			fi.Title = "Новый документ сверки";
			fi.ShowDialog();
		}

		private void invjStripMenuItem1_Click(object sender, EventArgs e)
		{
			bool open = true;
			foreach (Form f in this.MdiChildren)
			{
				if (f.Name == "frmInventoryList")
				{
					f.MdiParent = this;
					f.Show();
					f.WindowState = FormWindowState.Maximized;
					open = false;
				}
			}
			if (open)
			{
				frmInventoryList f = new frmInventoryList();
				f.usr = usr;
				f.MdiParent = this;
				f.Show();
				f.WindowState = FormWindowState.Maximized;
			}
		}

		private void NewInvStripMenuItem1_Click(object sender, EventArgs e)
		{
			NewInventory();
		}

		private void NewVerificationStripMenuItem_Click(object sender, EventArgs e)
		{
			NewVerification();
		}

		private void NewVerificationStripMenuItem1_Click(object sender, EventArgs e)
		{
			NewVerification();
		}

		private void verificationjStripMenuItem1_Click(object sender, EventArgs e)
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

        private void lostOrdersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool open = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmLostOrders")
                {
                    f.MdiParent = this;
                    f.Show();
                    f.WindowState = FormWindowState.Maximized;
                    open = false;
                }
            }
            if (open)
            {
                frmLostOrders f = new frmLostOrders();
                f.usr = usr;
                f.MdiParent = this;
                f.Show();
                f.WindowState = FormWindowState.Maximized;
            }
        }

        private void DeleteOldСтарыхToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool open = true;
            foreach (Form f in this.MdiChildren)
            {
                if (f.Name == "frmDeleteOld")
                {
                    f.MdiParent = this;
                    f.Show();
                    f.WindowState = FormWindowState.Maximized;
                    open = false;
                }
            }
            if (open)
            {
                frmDeleteOld f = new frmDeleteOld();
                f.usr = usr;
                f.MdiParent = this;
                f.Show();
                f.WindowState = FormWindowState.Maximized;
            }
        }

		private void loadNewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Установить обновление?", "Контроль обновлений",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
												0,
												System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
												) + "\\PSA.Update.cmd");

		}


	}
}
