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
	public partial class frmMain : Form
	{
		public SqlConnection db_connection = new SqlConnection();
		public UserInfo usr;
		public PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
        private FilterRowLike fRow;

		private SqlCommand db_command_goods_filter;
		private SqlDataAdapter db_adapter_goods_filter;
		
		private DataTable db_table_goods_filter = new DataTable("goods");
		private DataTable db_table_paper_filter = new DataTable("paper");
		private DataTable db_table_crop_filter = new DataTable("crop");

        private SqlCommand db_command_orders;
        private SqlDataAdapter db_adapter_orders;
        private DataTable db_table_orders = new DataTable("orders");

        private SqlCommand db_command_tehaction;
        private SqlDataAdapter db_adapter_tehaction;
        private DataTable db_table_tehaction = new DataTable("tehaction");

		private SqlCommand db_command_discard;
		private SqlDataAdapter db_adapter_discard;
		private DataTable db_discard = new DataTable("discard");

        private SqlCommand db_command;

	    private bool hidewin = true;
		private bool globalexit = false;

		private long _counter1 = 0;
		private long _counter2 = 0;
		private DateTime _counter1Date;
		private DateTime _counter2Date;

		private int _c_open = 0;
		private int _c_close = 0;


		public frmMain()
		{
			InitializeComponent();

			this.Text = "Оператор - Photoland System Automation " + Application.ProductVersion;
		}

        //////////////////////////////////////////////////////////////////////////
        private bool CheckState(SqlConnection c)
        {
            tmr.Stop();
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
            tmr.Start();
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


		private void OpenSettings()
		{
			this.TopMost = false;
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
			this.TopMost = prop.Mod_operator_top_most;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
            try
            {
                StreamWriter sw = new StreamWriter(prop.Dir_export + "\\Operator_" + Environment.MachineName + "_" + Environment.UserName + ".info", false, Encoding.GetEncoding(1251));
                sw.Write("Date:         " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + 
                         "\nMashine:      " + Environment.MachineName + 
                         "\nUser:         " + Environment.UserName + 
                         "\nOperator mod: " + Application.ProductVersion);
                sw.Close();
            }
            catch
            {
            }

			if (!Checking.checkVersion(Modules.Operator, Application.ProductVersion))
				if (MessageBox.Show("Внимание! Существует более новая версия модуля!\nУстановить обновление?", "Контроль обновлений", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
													0,
													System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
													) + "\\PSA.Update.cmd");

			bool tmp_login_ok = false;
			bool tmp_exit = false;
			if(!PSA.Lib.Util.Semaphore.semInventory)
			{
		    tmr.Interval = prop.UpdateOrderTableInOperator*1000;
            tmr.Stop();

		    hidewin = false;

			this.TopMost = prop.Mod_operator_top_most;

			// Проверяем, если есть ограничение на запуск одной копии и программа уже запущена
			if ((app.Search_twin()) && (prop.Run_one_copy_oprator))
			{
				// То выдаем сообщение и закрываем программу
				MessageBox.Show("Программа уже запущена!");
				globalexit = true;
				Application.Exit();
			}
			else
			{
                this.TopMost = false;
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
                        this.TopMost = false;
                        frmSetup fOptions = new frmSetup();
						fOptions.ShowDialog();
						prop = new PSA.Lib.Util.Settings();
						// Опять пробуем подключиться к базе
						db_connection.ConnectionString = prop.Connection_string;
						db_connection.Open();
                        this.TopMost = prop.Mod_operator_top_most;
                        
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
									if (fLogin.usr.prmCanLoginOperator)
									{
										this.Show();
									}
									else
									{
										tmp_exit = true;
										MessageBox.Show("Доступ в модуль оператора заперщен!", "Вход в программу", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        this.Text = "Оператор - " + usr.Name + " - Photoland System Automation " + Application.ProductVersion;

						txtDateCounter1.Value = DateTime.Now.AddDays(-10);
						txtDateCounter2.Value = DateTime.Now;

                        //запрашиваем машину и бумагу
                        this.TopMost = false;
                        frmSelectMashineAndPaper f = new frmSelectMashineAndPaper();
					    f.db_connection = this.db_connection;
					    f.ShowDialog();
                        this.TopMost = prop.Mod_operator_top_most;
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
							bool stop = false;
							if (_counter_tbl.Rows.Count > 0)
							{
								if ((_counter_tbl.Rows[0]["c2"].ToString() == "") && (_counter_tbl.Rows[0]["c2date"].ToString() == ""))
								{
									if (MessageBox.Show("Прошлый счетчик, открытый " + _counter_tbl.Rows[0]["c1date"].ToString() +
										" пользователем " + _counter_tbl.Rows[0]["name_user"].ToString().Trim() + " не был закрыт!\n" +
										"Автоматически закрываем значением " + f.txtCounter.Value.ToString() + "?", "Закрытие счетчика",
										MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
									{
										_counter_cmd.CommandText = "UPDATE [counters] SET c2 = " + f.txtCounter.Value.ToString() +
											", c2date = getdate(), c0 = (SELECT(ISNULL((" +
								"SELECT SUM(CASE tech_defect " +
											"WHEN 0 THEN actual_quantity " +
											"WHEN 1 THEN defect_quantity " +
											"WHEN 2 THEN defect_quantity " +
											"WHEN 3 THEN defect_quantity " +
											"WHEN 4 THEN 0 " +
											"WHEN 5 THEN defect_quantity " +
											"WHEN 6 THEN defect_quantity " +
											"WHEN 7 THEN 0 " +
											"WHEN 8 THEN defect_quantity " +
											"WHEN 9 THEN 0 " +
											"WHEN 10 THEN 0 " +
											"END)" +
													"FROM dbo.orderbody " +
													"WHERE (datework >= " +
														"(SELECT c1date " +
														"FROM dbo.counters " +
														"WHERE (id_counter = " + _counter_tbl.Rows[0]["id_counter"].ToString() +
														"))) AND (datework <= GETDATE()) AND (id_user_work = " +
															usr.Id_user + ") AND (id_mashine = N'" + usr.Id_Mashine.Trim() + "')" +
													"), 0))) " +
											"WHERE id_counter = " + _counter_tbl.Rows[0]["id_counter"].ToString();
										_counter_cmd.ExecuteNonQuery();
									}
									else
									{
										stop = true;
									}
								}
							}
							if (!stop)
							{
								_counter_cmd.CommandText = "INSERT INTO [counters] (id_mashine, id_user, name_user, c1, c1date) VALUES ('" +
									usr.Id_Mashine + "', " + usr.Id_user + ", '" + usr.Name.Trim() + "', " + f.txtCounter.Value.ToString() +
									", getdate())";
								_counter_cmd.ExecuteNonQuery();
								_counter1 = long.Parse(f.txtCounter.Value.ToString());
								_counter1Date = DateTime.Now;
								recalcCounter(true);

								lblMashine.Text = usr.Mashine.Trim();
								//lblPaper.Text = usr.Paper.Trim();
								// Показываем окно
								this.Focus();

								if (!usr.prmCanDelEditBrak)
								{
									btnDelBrak.Enabled = false;
									btnEditBrak.Enabled = false;
									btnEditBrak1.Enabled = false;
									btnDelBrak1.Enabled = false;
								}

								if (prop.Fly_window_operator)
								{
									this.Top = (-1) * (this.Height - 10);
								}

								// заполняем фильтры
								FillFilter();

								// заполняем таблицу заказов
								FillOrderList();

								// заполняем таблицу "браков"
								FillTehAction();
								FillDiscardTable();

								FillCounters();


								hidewin = true;

								ClearOrders();

								tmr.Start();
							}
							else
							{
								globalexit = true;
								Application.Exit();
							}
                        }
                        else
                        {
							globalexit = true;
							Application.Exit();
                        }
					}
					else
					{
						globalexit = true;
						Application.Exit();
					}
				}
				else
				{
					globalexit = true;
					Application.Exit();
				}
                this.TopMost = prop.Mod_operator_top_most;
            }
			}
			else
			{

				MessageBox.Show("В момент проведения инвентаризации вход в модуль запрещен!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				globalexit = true;
				hidewin = false;
				Application.Exit();
			}
		}

		private void FillCounters()
		{
			using (SqlConnection cn = new SqlConnection(prop.Connection_string))
			{
				cn.Open();
				SqlCommand cmd = new SqlCommand();
				cmd.Connection = cn;
				cmd.CommandTimeout = 9000;
				cmd.CommandText = "SELECT dbo.counters.id_counter, dbo.counters.id_mashine, dbo.mashine.mashine, dbo.counters.name_user, dbo.counters.c1, dbo.counters.c1date, dbo.counters.c2, dbo.counters.c2date, dbo.counters.c0, dbo.counters.c00 FROM dbo.counters INNER JOIN dbo.mashine ON dbo.counters.id_mashine = dbo.mashine.id_mashine WHERE (dbo.counters.c1date >= CONVERT(DATETIME, '" + txtDateCounter1.Value.Year.ToString("D4") + "-" + txtDateCounter1.Value.Month.ToString("D2") + "-" + txtDateCounter1.Value.Day.ToString("D2") + " 00:00:00', 102) AND c1date <= CONVERT(DATETIME, '" + txtDateCounter2.Value.Year.ToString("D4") + "-" + txtDateCounter2.Value.Month.ToString("D2") + "-" + txtDateCounter2.Value.Day.ToString("D2") + " 23:59:59', 102)) order by dbo.counters.c1date";
				DataTable tbl = new DataTable();
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(tbl);
				gridCounters.DataSource = tbl;

				gridCounters.Cols[1].Visible = false;

				gridCounters.Cols[2].Visible = false;

				gridCounters.Cols[3].Visible = true;
				gridCounters.Cols[3].Caption = "Машина";
				gridCounters.Cols[3].Width = 120;
				gridCounters.Cols[3].AllowDragging = false;
				gridCounters.Cols[3].AllowEditing = false;

				gridCounters.Cols[4].Visible = true;
				gridCounters.Cols[4].Caption = "Пользователь";
				gridCounters.Cols[4].Width = 100;
				gridCounters.Cols[4].AllowDragging = false;
				gridCounters.Cols[4].AllowEditing = false;

				gridCounters.Cols[5].Visible = true;
				gridCounters.Cols[5].Caption = "Начальный";
				gridCounters.Cols[5].Width = 90;
				gridCounters.Cols[5].AllowDragging = false;
				gridCounters.Cols[5].AllowEditing = false;

				gridCounters.Cols[6].Visible = true;
				gridCounters.Cols[6].Caption = "Открыт";
				gridCounters.Cols[6].Width = 110;
				gridCounters.Cols[6].Format = "dd/MM/yy HH:mm:ss";
				gridCounters.Cols[6].AllowDragging = false;
				gridCounters.Cols[6].AllowEditing = false;

				gridCounters.Cols[7].Visible = true;
				gridCounters.Cols[7].Caption = "Конечный";
				gridCounters.Cols[7].Width = 90;
				gridCounters.Cols[7].AllowDragging = false;
				gridCounters.Cols[7].AllowEditing = false;

				gridCounters.Cols[8].Visible = true;
				gridCounters.Cols[8].Caption = "Закрыт";
				gridCounters.Cols[8].Width = 110;
				gridCounters.Cols[8].Format = "dd/MM/yy HH:mm:ss";
				gridCounters.Cols[8].AllowDragging = false;
				gridCounters.Cols[8].AllowEditing = false;

				gridCounters.Cols[9].Visible = true;
				gridCounters.Cols[9].Caption = "Сделано";
				gridCounters.Cols[9].Width = 80;
				gridCounters.Cols[9].AllowDragging = false;
				gridCounters.Cols[9].AllowEditing = false;

				gridCounters.Cols[10].Visible = true;
				gridCounters.Cols[10].Caption = "Погрешность";
				gridCounters.Cols[10].Width = 80;
				gridCounters.Cols[10].AllowDragging = false;
				gridCounters.Cols[10].AllowEditing = false;


				cmd = new SqlCommand();
				cmd.Connection = cn;
				cmd.CommandTimeout = 9000;
				cmd.CommandText = "SELECT [id_mcounter], [date_mcounter], [mcounter], [id_user], [name_user] FROM [dbo].[mcounters] WHERE (date_mcounter >= CONVERT(DATETIME, '" + txtDateCounter1.Value.Year.ToString("D4") + "-" + txtDateCounter1.Value.Month.ToString("D2") + "-" + txtDateCounter1.Value.Day.ToString("D2") + " 00:00:00', 102) AND date_mcounter <= CONVERT(DATETIME, '" + txtDateCounter2.Value.Year.ToString("D4") + "-" + txtDateCounter2.Value.Month.ToString("D2") + "-" + txtDateCounter2.Value.Day.ToString("D2") + " 23:59:59', 102)) order by date_mcounter";
				DataTable tbl1 = new DataTable();
				SqlDataAdapter da1 = new SqlDataAdapter(cmd);
				da1.Fill(tbl1);
				gridCounter2.DataSource = tbl1;

				gridCounter2.Cols[1].Visible = false;

				gridCounter2.Cols[4].Visible = false;

				gridCounter2.Cols[2].Visible = true;
				gridCounter2.Cols[2].Caption = "Дата";
				gridCounter2.Cols[2].Width = 120;
				gridCounter2.Cols[2].AllowDragging = false;
				gridCounter2.Cols[2].AllowEditing = false;
				gridCounter2.Cols[2].Format = "dd/MM/yy HH:mm:ss";

				gridCounter2.Cols[3].Visible = true;
				gridCounter2.Cols[3].Caption = "Счетчик";
				gridCounter2.Cols[3].Width = 100;
				gridCounter2.Cols[3].AllowDragging = false;
				gridCounter2.Cols[3].AllowEditing = false;

				gridCounter2.Cols[5].Visible = true;
				gridCounter2.Cols[5].Caption = "Пользователь";
				gridCounter2.Cols[5].Width = 150;
				gridCounter2.Cols[5].AllowDragging = false;
				gridCounter2.Cols[5].AllowEditing = false;

			}
		}

		private string recalcCounter(bool update)
		{
			try
			{
				using (SqlConnection cn = new SqlConnection(prop.Connection_string))
				{
					cn.Open();
					SqlCommand cmd = new SqlCommand();
					cmd.CommandTimeout = 9000;
					cmd.Connection = cn;
					cmd.CommandText = "SELECT SUM(CASE tech_defect " +
											"WHEN 0 THEN actual_quantity " +
											"WHEN 1 THEN defect_quantity " +
											"WHEN 2 THEN defect_quantity " +
											"WHEN 3 THEN defect_quantity " +
											"WHEN 4 THEN 0 " +
											"WHEN 5 THEN defect_quantity " +
											"WHEN 6 THEN defect_quantity " +
											"WHEN 7 THEN 0 " +
											"WHEN 8 THEN defect_quantity " +
											"WHEN 9 THEN 0 " +
											"WHEN 10 THEN 0 " +
											"END)" +
										"FROM dbo.orderbody " +
										"WHERE (datework >= " +
											"(SELECT c1date " +
											"FROM dbo.counters " +
											"WHERE (id_counter = " +
												"(SELECT MAX(id_counter) AS id " +
												"FROM dbo.counters AS counters_1 " +
												"WHERE (id_mashine = N'" + usr.Id_Mashine.Trim() +
												"'))))) AND (datework <= GETDATE()) AND (id_user_work = " + usr.Id_user + 
												") AND (id_mashine = N'" + usr.Id_Mashine.Trim() + "')" +
										"";
					int c = 0;
					try
					{
						c = (int)decimal.Parse(cmd.ExecuteScalar().ToString());
					}
					catch 
					{
						c = 0;
					}
					cmd = new SqlCommand();
					cmd.CommandTimeout = 9000;
					cmd.Connection = cn;
					cmd.CommandText = "SELECT SUM(actual_quantity) " +
										"FROM dbo.orderbody " +
										"WHERE (datework >= " +
											"(SELECT c1date " +
											"FROM dbo.counters " +
											"WHERE (id_counter = " +
												"(SELECT MAX(id_counter) AS id " +
												"FROM dbo.counters AS counters_1 " +
												"WHERE (id_mashine = N'" + usr.Id_Mashine.Trim() +
												"'))))) AND (datework <= GETDATE()) AND (id_user_work = " + 
												usr.Id_user + ") AND (id_mashine = N'" + usr.Id_Mashine.Trim() + "')" +
										"";
					int c1 = 0;
					try
					{
						c1 = (int)decimal.Parse(cmd.ExecuteScalar().ToString());
					}
					catch
					{
						c1 = 0;
					}
					lblCounters.Text = "Начальное значение счетчика: " + _counter1 +
									" (" + _counter1Date +
									")\nСделано отпечатков: " + c.ToString() + " (погрешность " +
									  (c1 - (_c_close - _c_open)).ToString() + ")";
					if (update)
					{
						cmd.Connection = cn;
						cmd.CommandText = "UPDATE [counters] SET " +
											" c0 = (" + c.ToString() + "), " +
											" c00 = (" + (c1 - (_c_close - _c_open)).ToString() + ") " +
											"WHERE id_counter = (SELECT MAX(id_counter) AS id " +
											"FROM dbo.counters AS counters_1 " +
											"WHERE (id_mashine = N'" + usr.Id_Mashine.Trim() + "'))";
						cmd.ExecuteNonQuery();
					}
				}
			}
			catch { }
			return lblCounters.Text;
		}

		private bool closeCounter()
		{
			bool ret = false;
			try
			{
				long newCounter = 0;
				using (frmCloseCounter f = new frmCloseCounter())
				{
					f.lblInfo.Text = recalcCounter(false);
					if(usr.Id_Mashine.Trim() != "-1")
						f.ShowDialog();
					if ((f.DialogResult == DialogResult.OK) || (usr.Id_Mashine.Trim() == "-1"))
					{
						if (usr.Id_Mashine.Trim() != "-1")
							newCounter = (long)f.txtCounter.Value;
						else
							newCounter = 0;
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
								_counter_cmd.CommandText = "DECLARE @p int; " +
									"SET @p = (SELECT(" +
													"(SELECT SUM(actual_quantity) " +
													"FROM dbo.orderbody " +
													"WHERE (datework >= " +
														"(SELECT c1date " +
														"FROM dbo.counters " +
														"WHERE (id_counter = " + _counter_tbl.Rows[0]["id_counter"].ToString() +
														"))) AND (datework <= GETDATE()) AND (id_user_work = " + usr.Id_user + ") AND (id_mashine = N'" +
														usr.Id_Mashine.Trim() + "')) - " +
													((int)(_c_close - _c_open)).ToString() + ")); " +
									"UPDATE [counters] SET c2 = " + newCounter +
									", c2date = getdate(), c0 = (SELECT(ISNULL((" +
								"SELECT SUM(CASE tech_defect " +
											"WHEN 0 THEN actual_quantity " +
											"WHEN 1 THEN defect_quantity " +
											"WHEN 2 THEN defect_quantity " +
											"WHEN 3 THEN defect_quantity " +
											"WHEN 4 THEN 0 " +
											"WHEN 5 THEN defect_quantity " +
											"WHEN 6 THEN defect_quantity " +
											"WHEN 7 THEN 0 " +
											"WHEN 8 THEN defect_quantity " +
											"WHEN 9 THEN 0 " +
											"WHEN 10 THEN 0 " +
											"END)" +
													"FROM dbo.orderbody " +
													"WHERE (datework >= " +
														"(SELECT c1date " +
														"FROM dbo.counters " +
														"WHERE (id_counter = " + _counter_tbl.Rows[0]["id_counter"].ToString() +
														"))) AND (datework <= GETDATE()) AND (id_user_work = " +
															usr.Id_user + ") AND (id_mashine = N'" + usr.Id_Mashine.Trim() + "')" +
													"), 0))) " +
													", c00 = @p " +  
									"WHERE id_counter = " + _counter_tbl.Rows[0]["id_counter"].ToString();
								_counter_cmd.ExecuteNonQuery();
								_c_open = 0;
								_c_close = 0;
							}
						}
						recalcCounter(false);
						ret = true;
					}
				}
			}
			catch { }
			return ret;
		}


        private void LogoffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tmr.Stop();
            bool tmp_login_ok = false;
            bool tmp_exit = false;

            hidewin = false;

            this.TopMost = prop.Mod_operator_top_most;

            this.TopMost = false;

			if (closeCounter())
			{

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
									if (fLogin.usr.prmCanLoginOperator)
									{
										this.Show();
									}
									else
									{
										tmp_exit = true;
										MessageBox.Show("Доступ в модуль оператора заперщен!", "Вход в программу", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
						this.Text = "Оператор - " + usr.Name + " - Photoland System Automation " + Application.ProductVersion;

						//запрашиваем машину и бумагу
						this.TopMost = false;
						frmSelectMashineAndPaper f = new frmSelectMashineAndPaper();
						f.db_connection = this.db_connection;
						f.ShowDialog();
						this.TopMost = prop.Mod_operator_top_most;
						if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
						{
							usr.Id_Mashine = f.txtMashine.SelectedValue.ToString();
							usr.Mashine = f.txtMashine.Text;
							//usr.Id_Paper = f.txtPaper.SelectedValue.ToString();
							//usr.Paper = f.txtPaper.Text;



							lblMashine.Text = usr.Mashine.Trim();
							//lblPaper.Text = usr.Paper.Trim();
							// Показываем окно
							this.Focus();


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
							bool stop = false;
							if (_counter_tbl.Rows.Count > 0)
							{
								if ((_counter_tbl.Rows[0]["c2"].ToString() == "") && (_counter_tbl.Rows[0]["c2date"].ToString() == ""))
								{
									if (MessageBox.Show("Прошлый счетчик, открытый " + _counter_tbl.Rows[0]["c1date"].ToString() +
										" пользователем " + _counter_tbl.Rows[0]["name_user"].ToString().Trim() + " не был закрыт!\n" +
										"Автоматически закрываем значением " + f.txtCounter.Value.ToString() + "?", "Закрытие счетчика",
										MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
									{
										_counter_cmd.CommandText = "UPDATE [counters] SET c2 = " + f.txtCounter.Value.ToString() +
											", c2date = getdate(), c0 = 0 " +
											"WHERE id_counter = " + _counter_tbl.Rows[0]["id_counter"].ToString();
										_counter_cmd.ExecuteNonQuery();
									}
									else
									{
										stop = true;
									}
								}
							}
							if (!stop)
							{
								_counter_cmd.CommandText = "INSERT INTO [counters] (id_mashine, id_user, name_user, c1, c1date) VALUES ('" +
									usr.Id_Mashine + "', " + usr.Id_user + ", '" + usr.Name.Trim() + "', " + f.txtCounter.Value.ToString() +
									", getdate())";
								_counter_cmd.ExecuteNonQuery();
								_counter1 = long.Parse(f.txtCounter.Value.ToString());
								_counter1Date = DateTime.Now;
								recalcCounter(true);

								if (!usr.prmCanDelEditBrak)
								{
									btnDelBrak.Enabled = false;
									btnEditBrak.Enabled = false;
								}

								if (prop.Fly_window_operator)
								{
									this.Top = (-1) * (this.Height - 10);
								}


								// заполняем таблицу заказов
								FillOrderList();

								// заполняем таблицу "браков"
								FillTehAction();

								FillCounters();

								ClearOrders();

								hidewin = true;
							}
							else
							{
								globalexit = true;
								Application.Exit();
							}
						}
						else
						{
							globalexit = true;
							Application.Exit();
						}
					}
					else
					{
						globalexit = true;
						Application.Exit();
					}
				}
				else
				{
					globalexit = true;
					Application.Exit();
				}
			}
            this.TopMost = prop.Mod_operator_top_most;
            tmr.Start();
        }

        private void tmr_Tick(object sender, EventArgs e)
        {
            tmr.Stop();
            FillOrderList();
            tmr.Start();
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

		private void button1_Click(object sender, EventArgs e)
		{
			this.TopMost = false;
			if (closeCounter())
			{
				globalexit = true;
				hidewin = false;
				this.Close();
			}
			this.TopMost = prop.Mod_operator_top_most;
		}

		private void btnRecalcCounters_Click(object sender, EventArgs e)
		{
			FillCounters();
		}

		private void btnAddCounter2_Click(object sender, EventArgs e)
		{
			this.TopMost = false;
			try
			{
				using (frmMomentalCounter f = new frmMomentalCounter())
				{
					if (f.ShowDialog() == DialogResult.OK)
					{
						using (SqlConnection cn = new SqlConnection(prop.Connection_string))
						{
							cn.Open();
							SqlCommand cmd = new SqlCommand();
							cmd.Connection = cn;
							cmd.CommandTimeout = 9000;
							cmd.CommandText = "INSERT INTO [dbo].[mcounters] ([mcounter],[date_mcounter],[id_user],[name_user]) VALUES (" + ((long)f.txtCounter.Value).ToString() + ",getdate()," + usr.Id_user + ",'" + usr.Name + "')";
							cmd.ExecuteNonQuery();
						}
						FillCounters();
					}
				}
			}
			catch { }
			this.TopMost = prop.Mod_operator_top_most;
		}

		private void btnMomentalEdit_Click(object sender, EventArgs e)
		{
			this.TopMost = false;
			if (gridCounter2.Row > 0)
			{
				if (gridCounter2.GetData(gridCounter2.Row, gridCounter2.Selection.LeftCol) != null)
				{
					if (int.Parse(gridCounter2.Rows[gridCounter2.Row][1].ToString()) > 0)
					{
						using(frmMomentalCounter f = new frmMomentalCounter(int.Parse(gridCounter2.Rows[gridCounter2.Row][3].ToString())))
						{
							if(f.ShowDialog() == DialogResult.OK)
							{
								using (SqlConnection cn = new SqlConnection(prop.Connection_string))
								{
									cn.Open();
									SqlCommand cmd = new SqlCommand();
									cmd.Connection = cn;
									cmd.CommandTimeout = 9000;
									cmd.CommandText = "UPDATE [dbo].[mcounters] SET [mcounter] = " + ((long)f.txtCounter.Value).ToString() + ", [id_user] = " + usr.Id_user + ", [name_user] = '" + usr.Name + "', [exported] = 0 WHERE id_mcounter  = " +int.Parse(gridCounter2.Rows[gridCounter2.Row][1].ToString());
									cmd.ExecuteNonQuery();
								}
							FillCounters();
							
							}
						}
					}
				}
			}
			this.TopMost = prop.Mod_operator_top_most;
		}

		private void gridCounter2_DoubleClick(object sender, EventArgs e)
		{
			this.TopMost = false;
			if (gridCounter2.Row > 0)
			{
				if (gridCounter2.GetData(gridCounter2.Row, gridCounter2.Selection.LeftCol) != null)
				{
					if (int.Parse(gridCounter2.Rows[gridCounter2.Row][1].ToString()) > 0)
					{
						using (frmMomentalCounter f = new frmMomentalCounter(int.Parse(gridCounter2.Rows[gridCounter2.Row][3].ToString())))
						{
							if (f.ShowDialog() == DialogResult.OK)
							{
								using (SqlConnection cn = new SqlConnection(prop.Connection_string))
								{
									cn.Open();
									SqlCommand cmd = new SqlCommand();
									cmd.Connection = cn;
									cmd.CommandTimeout = 9000;
									cmd.CommandText = "UPDATE [dbo].[mcounters] SET [mcounter] = " + ((long)f.txtCounter.Value).ToString() + ", [id_user] = " + usr.Id_user + ", [name_user] = '" + usr.Name + "', [exported] = 0 WHERE id_mcounter  = " + int.Parse(gridCounter2.Rows[gridCounter2.Row][1].ToString());
									cmd.ExecuteNonQuery();
								}
								FillCounters();

							}
						}
					}
				}
			}
			this.TopMost = prop.Mod_operator_top_most;
		}



	}
}