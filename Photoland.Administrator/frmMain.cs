using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Forms.Admin;
using Photoland.Forms.Interface;
using Photoland.Lib;
using Photoland.Security.User;
using System.IO;
using PSA.Lib.Interface;
using PSA.Lib.Util;
using PSA.Core.Admin;

namespace Photoland.Administrator
{
	public partial class frmMain : Form
	{
		public SqlConnection db_connection = new SqlConnection();
		public UserInfo usr;
		public PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();


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
			this.Text = "Администратор - Photoland System Automation " + Application.ProductVersion;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
            try
            {
                StreamWriter sw = new StreamWriter(prop.Dir_net_export + "\\Admin_" + Environment.MachineName + "_" + Environment.UserName + ".info", false, Encoding.GetEncoding(1251));
                sw.Write("Date:         " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                         "\nMashine:      " + Environment.MachineName +
                         "\nUser:         " + Environment.UserName +
                         "\nAdmin mod:    " + Application.ProductVersion);
                sw.Close();
            }
            catch
            {
            }

			if (!Checking.checkVersion(Modules.Administrator, Application.ProductVersion))
				if (MessageBox.Show("Внимание! Существует более новая версия модуля!\nУстановить обновление?", "Контроль обновлений", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
													0,
													System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
													) + "\\PSA.Update.cmd");

            bool tmp_login_ok = false;
			bool tmp_exit = false;

			// Проверяем, если есть ограничение на запуск одной копии и программа уже запущена
			if ((app.Search_twin()) && (prop.Run_one_copy_admin))
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
					ErrorNfo.WriteErrorInfo(ex);
					// Если не удалось подключиться к базе, то
					// выдаем сообщение об ошибке и открываем форму
					// подключения к базе данных
					MessageBox.Show("Ошибка подключения к базе данных!\n" + ex.Message + "\n" + ex.Source + "\nПроверьте настройки подключения!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
					try
					{
						frmSetup fOptions = new frmSetup();
						fOptions.ShowDialog();
						prop = new PSA.Lib.Util.Settings();
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
									if (fLogin.usr.prmCanLoginAdmin)
									{
										this.Show();
									}
									else
									{
										tmp_exit = true;
										MessageBox.Show("Доступ в модуль администратора заперщен!", "Вход в программу", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        this.Text = "Администратор - " + usr.Name + " - Photoland System Automation " + Application.ProductVersion;

                        // запрос на не поддтвержденные списания
                        SqlCommand _cmd = new SqlCommand("SELECT COUNT(*) AS cnt FROM (SELECT TOP (100) PERCENT dbo.orderbody.datework, dbo.[order].number, dbo.defect.defect_name, dbo.orderbody.name_work, dbo.orderbody.user_defect, dbo.orderbody.defect_quantity, dbo.orderbody.defect_ok, dbo.good.name AS good FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good LEFT OUTER JOIN dbo.defect ON dbo.orderbody.tech_defect = dbo.defect.defect_code LEFT OUTER JOIN dbo.[order] ON dbo.orderbody.id_order = dbo.[order].id_order WHERE (dbo.orderbody.tech_defect > 0) AND dbo.orderbody.id_user_work > 0) AS derivedtbl_1", db_connection);
						_cmd.CommandTimeout = 9000;
                        if ((int)_cmd.ExecuteScalar() > 0)
                            MessageBox.Show("Внимание!\nЕсть не подтвержденные списания!", "Внимание",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);

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

		private void mnuPayment_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                frmPaymentTable fPTable = new frmPaymentTable(db_connection, usr);
                fPTable.MdiParent = this;
                fPTable.Show();
                fPTable.WindowState = FormWindowState.Maximized;
            }
		}

		private void mnuClientCategory_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmCategoryTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmCategoryTable frm = new frmCategoryTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

		private void mnuClient_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
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
                    frmClientTable frm = new frmClientTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

		private void mnuUsers_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmUserTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmUserTable frm = new frmUserTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

		private void mnuPost_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmPostTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmPostTable frm = new frmPostTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

		private void mnuPoints_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmPointTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmPointTable frm = new frmPointTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

		private void mnuDCard_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmDiscontTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmDiscontTable frm = new frmDiscontTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

		private void операторскиеМашиныToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmMashineTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmMashineTable frm = new frmMashineTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

		private void mnuMaterial_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmMaterialTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmMaterialTable frm = new frmMaterialTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

		private void mnuGood_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmGoodTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmGoodTable frm = new frmGoodTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

        private void mnuPrice_Click(object sender, EventArgs e)
        {
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmPriceTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmPriceTable frm = new frmPriceTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

		private void mnuSetup_Click(object sender, EventArgs e)
		{
			frmSetup f = new frmSetup();
			f.ShowDialog();
			prop = new PSA.Lib.Util.Settings();
		}

		private void mnuLocks_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmOrderLockTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmOrderLockTable frm = new frmOrderLockTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

		private void OrderTableToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                frmAcceptanceTable frm = new frmAcceptanceTable(db_connection, usr);
                frm.MdiParent = this;
                frm.WindowState = FormWindowState.Maximized;
                frm.Show();
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
                                case "Admin full order":
                                    {
                                        frmGetDateIntervalTypeDate f = new frmGetDateIntervalTypeDate();
                                        f.ShowDialog();
                                        if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                                        {
                                            string filter = "";
                                            switch (f.txtDateType.SelectedValue.ToString())
                                            {
                                                case "1":
                                                    {
                                                        filter = "WHERE CONVERT(datetime, [input_date], 120)>=CONVERT(DATETIME, '" + f.Y1 + "." + f.M1 + "." + f.D1 +
                                                                 " 00:00:00.000" + "', 120) AND CONVERT(datetime, [input_date], 120)<=CONVERT(DATETIME, '" + f.Y2 +
                                                                 "." +
                                                                 f.M2 + "." + f.D2 + " 23:59:59.999" + "', 120) ";
                                                        break;
                                                    }
                                                case "2":
                                                    {
                                                        filter = "WHERE CONVERT(datetime, [output_date], 120)>=CONVERT(DATETIME, '" + f.Y1 + "." + f.M1 + "." +
                                                                 f.D1 +
                                                                 " 00:00:00.000" + "', 120) AND CONVERT(datetime, [output_date], 120)<=CONVERT(DATETIME, '" + f.Y2 +
                                                                 "." +
                                                                 f.M2 + "." + f.D2 + " 23:59:59.999" + "', 120) ";
                                                        break;
                                                    }
                                                case "3":
                                                    {
                                                        filter = "WHERE CONVERT(datetime, [expected_date], 120)>=CONVERT(DATETIME, '" + f.Y1 + "." + f.M1 + "." +
                                                                 f.D1 +
                                                                 " 00:00:00.000" + "', 120) AND CONVERT(datetime, [expected_date], 120)<=CONVERT(DATETIME, '" +
                                                                 f.Y2 +
                                                                 "." +
                                                                 f.M2 + "." + f.D2 + " 23:59:59.999" + "', 120) ";
                                                        break;
                                                    }
                                            }
                                            string query =
                                                "SELECT [number], [input_date], [expected_date], [output_date], [client], [category], [status], [date_add], [id_user_add], [name_add], [datework], [name_accept], [name_operator], [name_designer], [name_delivery], [good], [quantity], [actual_quantity], [price], [name_work], [defect_quantity], [user_defect], [defect] FROM [vwAdminFullLog] " +
                                                filter + " ORDER BY [number]";
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
								case "Discont percent":
									{
										/*
										 SELECT dbo.[order].input_date, dbo.[order].output_date, dbo.[order].number, dbo.[order].discont_percent, dbo.[order].discont_code, dbo.dcard.name, dbo.[order].name_accept, dbo.[order].name_delivery FROM dbo.[order] LEFT OUTER JOIN dbo.dcard ON dbo.[order].discont_code = dbo.dcard.code WHERE (dbo.[order].status = N'100000') AND dbo.[order].discont_percent > 0 AND (dbo.[order].input_date > CONVERT(DATETIME, '2000-01-01 00:00:00', 102) AND dbo.[order].input_date < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
										 */
										frmReportSelectDate f = new frmReportSelectDate();
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											filter =
												"AND (dbo.[order].input_date >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND dbo.[order].input_date <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT dbo.[order].input_date, dbo.[order].output_date, dbo.[order].number, dbo.[order].discont_percent, dbo.[order].discont_code, dbo.dcard.name, dbo.[order].name_accept, dbo.[order].name_delivery FROM dbo.[order] LEFT OUTER JOIN dbo.dcard ON dbo.[order].discont_code = dbo.dcard.code WHERE (dbo.[order].status = N'100000') AND dbo.[order].discont_percent > 0 " + filter;
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
								case "Bonus":
									{
										/*
										 SELECT dbo.[order].input_date, dbo.[order].output_date, dbo.[order].number, dbo.dcard.name, dbo.[order].name_accept, dbo.[order].name_delivery, dbo.[order].bonus, dbo.dcard.code FROM dbo.[order] LEFT OUTER JOIN dbo.dcard ON dbo.[order].discont_code = dbo.dcard.code WHERE (dbo.[order].status = N'100000') AND (dbo.[order].bonus > 0) AND (dbo.dcard.typebonus IN ('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M')) AND (dbo.[order].input_date > CONVERT(DATETIME, '2000-01-01 00:00:00', 102) AND dbo.[order].input_date < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
										 */
										frmReportSelectDate f = new frmReportSelectDate();
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											filter =
												"AND (dbo.[order].input_date >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND dbo.[order].input_date <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT dbo.[order].input_date, dbo.[order].output_date, dbo.[order].number, dbo.dcard.name, dbo.[order].name_accept, dbo.[order].name_delivery, dbo.[order].bonus, dbo.dcard.code FROM dbo.[order] LEFT OUTER JOIN dbo.dcard ON dbo.[order].discont_code = dbo.dcard.code WHERE (dbo.[order].status = N'100000') AND (dbo.[order].bonus > 0) AND (dbo.dcard.typebonus IN ('A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M')) " + filter;
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
								case "Cupon":
									{
										/*
										 SELECT dbo.[order].input_date, dbo.[order].output_date, dbo.[order].number, dbo.dcard.name, dbo.[order].name_accept, dbo.[order].name_delivery, dbo.[order].bonus, dbo.dcard.code FROM dbo.[order] LEFT OUTER JOIN dbo.dcard ON dbo.[order].discont_code = dbo.dcard.code WHERE (dbo.[order].status = N'100000') AND (dbo.[order].bonus > 0) AND (dbo.dcard.typebonus IN ('N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z')) AND (dbo.[order].input_date > CONVERT(DATETIME, '2000-01-01 00:00:00', 102) AND dbo.[order].input_date < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
										 */
										frmReportSelectDate f = new frmReportSelectDate();
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											filter =
												"AND (dbo.[order].input_date >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND dbo.[order].input_date <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT dbo.[order].input_date, dbo.[order].output_date, dbo.[order].number, dbo.dcard.name, dbo.[order].name_accept, dbo.[order].name_delivery, dbo.[order].bonus, dbo.dcard.code FROM dbo.[order] LEFT OUTER JOIN dbo.dcard ON dbo.[order].discont_code = dbo.dcard.code WHERE (dbo.[order].status = N'100000') AND (dbo.[order].bonus > 0) AND (dbo.dcard.typebonus IN ('N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z')) " + filter;
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
								case "Defect":
									{
										/*
										 SELECT TOP (100) PERCENT dbo.orderbody.datework, dbo.[order].number, dbo.defect.defect_name, dbo.orderbody.name_work, dbo.orderbody.user_defect, dbo.orderbody.defect_quantity, dbo.orderbody.defect_ok FROM dbo.orderbody LEFT OUTER JOIN dbo.defect ON dbo.orderbody.tech_defect = dbo.defect.defect_code LEFT OUTER JOIN dbo.[order] ON dbo.orderbody.id_order = dbo.[order].id_order WHERE (dbo.orderbody.tech_defect > 0) AND (dbo.orderbody.datework > CONVERT(DATETIME, '2000-01-01 00:00:00', 102) AND dbo.orderbody.datework < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
										 */
                                        frmReportSelectDateDefect f = new frmReportSelectDateDefect();
									    f.db_connection = db_connection;
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											filter =
												"AND (dbo.orderbody.datework >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND dbo.orderbody.datework <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
                                            if (f.txtDefect.SelectedValue.ToString().Trim() != "-1")
                                                filter += " AND (dbo.orderbody.tech_defect = " +
                                                          f.txtDefect.SelectedValue.ToString().Trim() + ")";
											string query =
												"SELECT TOP (100) PERCENT dbo.orderbody.datework, dbo.[order].number, dbo.defect.defect_name, dbo.orderbody.name_work, dbo.orderbody.user_defect, dbo.orderbody.defect_quantity, dbo.orderbody.defect_ok, dbo.good.name AS good FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good LEFT OUTER JOIN dbo.defect ON dbo.orderbody.tech_defect = dbo.defect.defect_code LEFT OUTER JOIN dbo.[order] ON dbo.orderbody.id_order = dbo.[order].id_order WHERE (dbo.orderbody.tech_defect > 0) " + filter;
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
								case "Input work":
									{
										/*
										 SELECT datecnt.dated, order_1.name_accept, COUNT(order_1.id_order) AS cnt, datecnt.cntd FROM (SELECT DATEADD(dd, 0, DATEDIFF(dd, 0, input_date)) AS dated, COUNT(id_order) AS cntd FROM dbo.[order] GROUP BY DATEADD(dd, 0, DATEDIFF(dd, 0, input_date))) AS datecnt INNER JOIN dbo.[order] AS order_1 ON datecnt.dated = DATEADD(dd, 0, DATEDIFF(dd, 0, order_1.input_date)) GROUP BY datecnt.dated, datecnt.cntd, order_1.name_accept HAVING (datecnt.dated > CONVERT(DATETIME, '2000-01-01 00:00:00', 102) AND datecnt.dated < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
										 */
										frmReportSelectDate f = new frmReportSelectDate();
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											filter =
												" (datecnt.dated >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND datecnt.dated <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT datecnt.dated, order_1.name_accept, COUNT(order_1.id_order) AS cnt, datecnt.cntd FROM (SELECT DATEADD(dd, 0, DATEDIFF(dd, 0, input_date)) AS dated, COUNT(id_order) AS cntd FROM dbo.[order] GROUP BY DATEADD(dd, 0, DATEDIFF(dd, 0, input_date))) AS datecnt INNER JOIN dbo.[order] AS order_1 ON datecnt.dated = DATEADD(dd, 0, DATEDIFF(dd, 0, order_1.input_date)) GROUP BY datecnt.dated, datecnt.cntd, order_1.name_accept HAVING " + filter;
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
								case "Input work service":
									{
										/*
										 SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_add, dbo.good.name, SUM(orderbody_1.quantity) AS cnt, datecnt.cntd FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.dateadd)) AS dated, good_1.id_good, SUM(dbo.orderbody.quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.dateadd)) HAVING (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.dateadd)) > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.dateadd)) < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.dateadd)) = datecnt.dated GROUP BY datecnt.dated, orderbody_1.name_add, dbo.good.name, datecnt.cntd HAVING (datecnt.dated > CONVERT(DATETIME, '2000-01-01 00:00:00', 102) AND datecnt.dated < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))										 */
										frmReportSelectDate f = new frmReportSelectDate();
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											string filter2 = "";
											filter =
												" (datecnt.dated >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND datecnt.dated <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											filter2 =
												" (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.dateadd)) >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.dateadd)) <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_add, dbo.good.name, SUM(orderbody_1.quantity) AS cnt, datecnt.cntd FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.dateadd)) AS dated, good_1.id_good, SUM(dbo.orderbody.quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.dateadd)) HAVING " + filter2 + " ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.dateadd)) = datecnt.dated GROUP BY datecnt.dated, orderbody_1.name_add, dbo.good.name, datecnt.cntd HAVING " + filter;
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
								case "Work by service":
									{
										/*
										 SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) < CONVERT(DATETIME, '2009-01-01 00:00:00', 102)) ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated WHERE (dbo.good.type = N'1') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd HAVING (SUM(orderbody_1.actual_quantity) > 0) AND (datecnt.dated > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (datecnt.dated < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
										 */
										frmReportSelectDate f = new frmReportSelectDate();
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											string filter2 = "";
											filter =
												" (datecnt.dated >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND datecnt.dated <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											filter2 =
												" (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING " + filter2 + " ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated WHERE (dbo.good.type <> N'2') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd HAVING (SUM(orderbody_1.actual_quantity) > 0) AND " + filter;
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
								case "Work by service 1":
									{
										/*
										 SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) < CONVERT(DATETIME, '2009-01-01 00:00:00', 102)) ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated WHERE (dbo.good.type = N'1') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd HAVING (SUM(orderbody_1.actual_quantity) > 0) AND (datecnt.dated > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (datecnt.dated < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
										 */
										frmReportSelectDate f = new frmReportSelectDate();
										f.ShowDialog();
										if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
										{
											string filter = "";
											string filter2 = "";
											filter =
												" (datecnt.dated >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND datecnt.dated <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											filter2 =
												" (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) >= CONVERT(DATETIME, '" +
												f.txtDateBegin.Value.Year.ToString("D4") + "-" +
												f.txtDateBegin.Value.Month.ToString("D2") + "-" +
												f.txtDateBegin.Value.Day.ToString("D2") +
												" 00:00:00', 102) AND DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) <= CONVERT(DATETIME, '" +
												f.txtDateEnd.Value.Year.ToString("D4") + "-" +
												f.txtDateEnd.Value.Month.ToString("D2") + "-" +
												f.txtDateEnd.Value.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING " + filter2 + " ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated WHERE (dbo.good.type = N'2') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd HAVING (SUM(orderbody_1.actual_quantity) > 0) AND " + filter;
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
												"SELECT dbo.payments.id_payment, dbo.payments.date as d, dbo.payments.time as t, dbo.payments.id_user, dbo.payments.name_user, dbo.payments.number, dbo.payments.payment, dbo.payments.type, dbo.payments.comment, dbo.payments.payment_way, dbo.payments.exported, dbo.category.name FROM dbo.payments INNER JOIN dbo.[order] ON dbo.payments.number = dbo.[order].number INNER JOIN dbo.client ON dbo.[order].id_client = dbo.client.id_client INNER JOIN dbo.category ON dbo.client.id_category = dbo.category.id_category WHERE " + filter;
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

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			ShowReport("Admin full order", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			ShowReport("Admin full order", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			ShowReport("Admin full order", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void mnuExit_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Photoland.Forms.Interface.frmAbout f = new Photoland.Forms.Interface.frmAbout();
			f.ShowDialog();
		}

		private void ortherDCardToolStripMenuItem_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmODiscontTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmODiscontTable frm = new frmODiscontTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

        private void defectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckState(db_connection))
            {
                bool open = true;
                foreach (Form f in this.MdiChildren)
                {
                    if (f.Name == "frmDefectTable")
                    {
                        f.MdiParent = this;
                        f.Show();
                        f.WindowState = FormWindowState.Maximized;
                        open = false;
                    }
                }
                if (open)
                {
                    frmDefectTable frm = new frmDefectTable(db_connection, usr);
                    frm.MdiParent = this;
                    frm.WindowState = FormWindowState.Maximized;
                    frm.Show();
                }
            }
        }

        private void ToolStripMenuItem6_Click(object sender, EventArgs e)
        {
            ShowReport("Discont percent", false, C1.Win.C1Report.FileFormatEnum.Text);
        }

        private void ToolStripMenuItem7_Click(object sender, EventArgs e)
        {
            ShowReport("Discont percent", true, C1.Win.C1Report.FileFormatEnum.PDF);
        }

        private void ToolStripMenuItem8_Click(object sender, EventArgs e)
        {
            ShowReport("Discont percent", true, C1.Win.C1Report.FileFormatEnum.Excel);
        }

		private void toolStripMenuItem9_Click(object sender, EventArgs e)
		{
			ShowReport("Bonus", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem10_Click(object sender, EventArgs e)
		{
			ShowReport("Bonus", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem11_Click(object sender, EventArgs e)
		{
			ShowReport("Bonus", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem13_Click(object sender, EventArgs e)
		{
			ShowReport("Cupon", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem14_Click(object sender, EventArgs e)
		{
			ShowReport("Cupon", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem15_Click(object sender, EventArgs e)
		{
			ShowReport("Cupon", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem17_Click(object sender, EventArgs e)
		{
			ShowReport("Defect", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem18_Click(object sender, EventArgs e)
		{
			ShowReport("Defect", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem19_Click(object sender, EventArgs e)
		{
			ShowReport("Defect", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem21_Click(object sender, EventArgs e)
		{
			ShowReport("Input work", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem22_Click(object sender, EventArgs e)
		{
			ShowReport("Input work", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem23_Click(object sender, EventArgs e)
		{
			ShowReport("Input work", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem25_Click(object sender, EventArgs e)
		{
			ShowReport("Input work service", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem26_Click(object sender, EventArgs e)
		{
			ShowReport("Input work service", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem27_Click(object sender, EventArgs e)
		{
			ShowReport("Input work service", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem29_Click(object sender, EventArgs e)
		{
			ShowReport("Work by service", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem30_Click(object sender, EventArgs e)
		{
			ShowReport("Work by service", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem31_Click(object sender, EventArgs e)
		{
			ShowReport("Work by service", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem33_Click(object sender, EventArgs e)
		{
			ShowReport("Work by service 1", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem34_Click(object sender, EventArgs e)
		{
			ShowReport("Work by service 1", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem35_Click(object sender, EventArgs e)
		{
			ShowReport("Work by service 1", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem41_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem42_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem43_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem37_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa full", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem38_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa full", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem39_Click(object sender, EventArgs e)
		{
			ShowReport("Kassa full", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem45_Click(object sender, EventArgs e)
		{
			ShowReport("Debet", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem46_Click(object sender, EventArgs e)
		{
			ShowReport("Debet", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem47_Click(object sender, EventArgs e)
		{
			ShowReport("Debet", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void semaphoresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSemaphores f = new frmSemaphores();
			f.ShowDialog();
			prop = new PSA.Lib.Util.Settings();
		}

        private void newVerificationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVerificationDoc f = new frmVerificationDoc(usr);
            f.ShowDialog();
        }

        private void jverificationToolStripMenuItem_Click(object sender, EventArgs e)
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

        private void inventoryToolStripMenuItem1_Click(object sender, EventArgs e)
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

		private void deleteOldToolStripMenuItem_Click(object sender, EventArgs e)
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

		private void descardingToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool open = true;
			foreach (Form f in this.MdiChildren)
			{
				if (f.Name == "frmOrderDiscardList")
				{
					f.MdiParent = this;
					f.Show();
					f.WindowState = FormWindowState.Maximized;
					open = false;
				}
			}
			if (open)
			{
				frmOrderDiscardList f = new frmOrderDiscardList();
				f.usr = usr;
				f.MdiParent = this;
				f.Show();
				f.WindowState = FormWindowState.Maximized;
			}
		}

		private void DeleteOldPaymentToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmDeletePayment frm = new frmDeletePayment();
			frm.usr = usr;
			frm.ShowDialog();
		}

		private void списанияПоФактуToolStripMenuItem_Click(object sender, EventArgs e)
		{
			bool open = true;
			foreach (Form f in this.MdiChildren)
			{
				if (f.Name == "frmWasteList")
				{
					f.MdiParent = this;
					f.Show();
					f.WindowState = FormWindowState.Maximized;
					open = false;
				}
			}
			if (open)
			{
				frmWasteList f = new frmWasteList();
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

		private void sToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Установить обновление?", "Контроль обновлений",
				MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
				System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
												0,
												System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
												) + "\\PSA.Update.cmd");
		}

		private void importJToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (CheckState(db_connection))
			{
				frmAccptanceTableImport frm = new frmAccptanceTableImport(db_connection, usr);
				frm.MdiParent = this;
				frm.WindowState = FormWindowState.Maximized;
				frm.Show();
			}
		}

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void полноеУдалениеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDeleteFromDate f = new frmDeleteFromDate();
            f.db_connection = db_connection;
            f.ShowDialog();
        }

        private void перестройкаИндексовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRebuildIndex f = new frmRebuildIndex();
            f.db_connection = db_connection;
            f.ShowDialog();

        }




	}
}