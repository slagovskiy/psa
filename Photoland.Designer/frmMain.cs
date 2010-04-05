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
using Photoland.Forms.Admin;
using Photoland.Forms.Interface;
using Photoland.Lib;
using Photoland.Order;
using System.IO;
using System.Globalization;
using Photoland.Components.FilterRow;
using PSA.Lib.Interface;
using PSA.Lib.Util;

namespace Photoland.Designer
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

		private SqlCommand db_command_discard;
		private SqlDataAdapter db_adapter_discard;
		private DataTable db_discard = new DataTable("discard");

		private SqlCommand db_command;

		private bool hidewin = true;
		private bool globalexit = false;

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
        
        public frmMain()
		{
			InitializeComponent();

			this.Text = "Дизайнер - Photoland System Automation " + Application.ProductVersion;
		}

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
			this.TopMost = prop.Mod_designer_top_most;
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
            try
            {
                StreamWriter sw = new StreamWriter(prop.Dir_export + "\\Designer_" + Environment.MachineName + "_" + Environment.UserName + ".info", false, Encoding.GetEncoding(1251));
                sw.Write("Date:         " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                         "\nMashine:      " + Environment.MachineName +
                         "\nUser:         " + Environment.UserName +
                         "\nDesigner mod: " + Application.ProductVersion);
                sw.Close();
            }
            catch
            {
            }
			if (!Checking.checkVersion(Modules.Designer, Application.ProductVersion))
				if (MessageBox.Show("Внимание! Существует более новая версия модуля!\nУстановить обновление?", "Контроль обновлений", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
													0,
													System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
													) + "\\PSA.Update.cmd");
			bool tmp_login_ok = false;
			bool tmp_exit = false;

			tmr.Interval = prop.UpdateOrderTableInDesigner * 1000;
			tmr.Stop();

			hidewin = false;

			this.TopMost = prop.Mod_designer_top_most;

			// Проверяем, если есть ограничение на запуск одной копии и программа уже запущена
			if ((app.Search_twin()) && (prop.Run_one_copy_designer))
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
						this.TopMost = prop.Mod_designer_top_most;

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
									if (fLogin.usr.prmCanLoginDesigner)
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
                        this.Text = "Дизайнер - " + usr.Name + " - Photoland System Automation " + Application.ProductVersion;


						//запрашиваем машину и бумагу
						this.TopMost = false;
						//frmSelectMashineAndPaper f = new frmSelectMashineAndPaper();
						//f.db_connection = this.db_connection;
						//f.ShowDialog();
						this.TopMost = prop.Mod_designer_top_most;

						// Показываем окно
						this.Focus();

						if (!usr.prmCanDelEditBrak)
						{
							btnDelBrak.Enabled = false;
							btnEditBrak.Enabled = false;
						}

						if (prop.Fly_window_designer)
						{
							HideWindow();
						}

						// заполняем фильтры
						FillFilter();

						// заполняем таблицу заказов
						FillOrderList();

						// заполняем таблицу "браков"
						FillDiscardTable();

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
				this.TopMost = prop.Mod_designer_top_most;
			}
		}


		private void FillDiscardTable()
		{
            if (CheckState(db_connection))
            {
                DateTime tm = new DateTime();
                tm = DateTime.Now.AddDays(-10);
                string y = tm.Year.ToString();
                string m = tm.Month < 10
                               ? "0" + tm.Month.ToString()
                               : tm.Month.ToString();
                string d = tm.Day < 10
                               ? "0" + tm.Day.ToString()
                               : tm.Day.ToString();

                db_discard.Rows.Clear();
                db_command_discard =
                    new SqlCommand(
                        "SELECT [id_discard], [datediscard], [material], [quantity], [comment], [user_name], [orderno] FROM [vwDiscardList] WHERE [datediscard] > 0 AND CONVERT(datetime, [datediscard]) >= CONVERT(datetime, '" +
                        y + "/" + m + "/" + d + " 00:00:00.000') AND [id_user] = " + usr.Id_user.ToString() + " ORDER BY [datediscard]",
                        db_connection);
                db_adapter_discard = new SqlDataAdapter(db_command_discard);
                db_adapter_discard.Fill(db_discard);

                gridTehAction.DataSource = db_discard;

                /*
                 * 1 [id_discard], 
                 * 2 [datediscard], 
                 * 3 [material], 
                 * 4 [quantity], 
                 * 5 [comment], 
                 * 6 [user_name], 
                 * 7 [orderno]
                 */

                gridTehAction.Cols[1].Visible = false;
                gridTehAction.Cols[2].Visible = false;
                gridTehAction.Cols[3].Visible = false;
                gridTehAction.Cols[4].Visible = false;
                gridTehAction.Cols[5].Visible = false;
                gridTehAction.Cols[6].Visible = false;
                gridTehAction.Cols[7].Visible = false;



                gridTehAction.Cols[2].Visible = true;
                gridTehAction.Cols[2].Width = 105;
                gridTehAction.Cols[2].AllowDragging = false;
                gridTehAction.Cols[2].AllowEditing = false;
                gridTehAction.Cols[2].AllowMerging = false;
                gridTehAction.Cols[2].AllowResizing = true;
                gridTehAction.Cols[2].AllowSorting = true;
                gridTehAction.Cols[2].Caption = "Дата";
                gridTehAction.Cols[2].Format = "g";

                gridTehAction.Cols[3].Visible = true;
                gridTehAction.Cols[3].Width = 205;
                gridTehAction.Cols[3].AllowDragging = false;
                gridTehAction.Cols[3].AllowEditing = false;
                gridTehAction.Cols[3].AllowMerging = false;
                gridTehAction.Cols[3].AllowResizing = true;
                gridTehAction.Cols[3].AllowSorting = true;
                gridTehAction.Cols[3].Caption = "Материал";

                gridTehAction.Cols[4].Visible = true;
                gridTehAction.Cols[4].Width = 60;
                gridTehAction.Cols[4].AllowDragging = false;
                gridTehAction.Cols[4].AllowEditing = false;
                gridTehAction.Cols[4].AllowMerging = false;
                gridTehAction.Cols[4].AllowResizing = true;
                gridTehAction.Cols[4].AllowSorting = true;
                gridTehAction.Cols[4].Caption = "Кол-во";
                gridTehAction.Cols[4].Format = "N2";

                gridTehAction.Cols[5].Visible = true;
                gridTehAction.Cols[5].Width = 205;
                gridTehAction.Cols[5].AllowDragging = false;
                gridTehAction.Cols[5].AllowEditing = false;
                gridTehAction.Cols[5].AllowMerging = false;
                gridTehAction.Cols[5].AllowResizing = true;
                gridTehAction.Cols[5].AllowSorting = true;
                gridTehAction.Cols[5].Caption = "Причина";

                gridTehAction.Cols[6].Visible = true;
                gridTehAction.Cols[6].Width = 125;
                gridTehAction.Cols[6].AllowDragging = false;
                gridTehAction.Cols[6].AllowEditing = false;
                gridTehAction.Cols[6].AllowMerging = false;
                gridTehAction.Cols[6].AllowResizing = true;
                gridTehAction.Cols[6].AllowSorting = true;
                gridTehAction.Cols[6].Caption = "Пользователь";

                gridTehAction.Cols[7].Visible = true;
                gridTehAction.Cols[7].Width = 100;
                gridTehAction.Cols[7].AllowDragging = false;
                gridTehAction.Cols[7].AllowEditing = false;
                gridTehAction.Cols[7].AllowMerging = false;
                gridTehAction.Cols[7].AllowResizing = true;
                gridTehAction.Cols[7].AllowSorting = true;
                gridTehAction.Cols[7].Caption = "Заказ №";
            }
		}
		private void FillFilter()
		{
            if (CheckState(db_connection))
            {

                // заполняем фильтр услуг
                db_command_goods_filter = new SqlCommand("SELECT [id_good], [name] FROM [vwGoodListDesigner] ORDER BY [name]", db_connection);
                db_adapter_goods_filter = new SqlDataAdapter(db_command_goods_filter);
                db_adapter_goods_filter.Fill(db_table_goods_filter);

                DataRow tmp_row;
                tmp_row = db_table_goods_filter.NewRow();
                tmp_row["id_good"] = "0";
                tmp_row["name"] = "< ВСЕ >";
                db_table_goods_filter.Rows.InsertAt(tmp_row, 0);

                txtFilterGoods.DataSource = db_table_goods_filter;
                txtFilterGoods.DataSource = db_table_goods_filter;
                txtFilterGoods.ValueMember = "id_good";
                txtFilterGoods.DisplayMember = "name";
            }
		}

		private void FillOrderList()
		{
            if (CheckState(db_connection))
            {
                int selid = 0;
                try
                {
                    selid = int.Parse(gridOrderList.GetData(gridOrderList.Row, 1).ToString());
                }
                catch (Exception ex)
                {
                    //ErrorNfo.WriteErrorInfo(ex);
                }

                db_table_orders.Rows.Clear();
                string tmpF_Good = "", tmpF_Crop = "", tmpF_Paper = "";

                if (txtFilterGoods.SelectedValue.ToString() != "0")
                    tmpF_Good = " AND [id_good] = '" + txtFilterGoods.SelectedValue + "'";
                else
                    tmpF_Good = "";

                db_command_orders = new SqlCommand("SELECT DISTINCT [id_order], [number], [status], [input_date], [expected_date], [id_user_designer] FROM [vwOrderListDesigner] WHERE (([id_order] > 0) " + tmpF_Good + ") ORDER BY [expected_date]", db_connection);
            	db_command_orders.CommandTimeout = 900;
                db_adapter_orders = new SqlDataAdapter(db_command_orders);
                db_adapter_orders.Fill(db_table_orders);
                lblLoaded.Text = "Загружено " + db_table_orders.Rows.Count.ToString("N0", new CultureInfo("de-DE")) + " зак.";


                gridOrderList.DataSource = db_table_orders;
                /*
                 * 1 [id_order], 
                 * 2 [number], 
                 * 3 [status], 
                 * 4 [input_date], 
                 * 5 [expected_date]
                 */
                gridOrderList.Cols[1].Visible = false;
                gridOrderList.Cols[2].Visible = false;
                gridOrderList.Cols[3].Visible = false;
                gridOrderList.Cols[4].Visible = false;
                gridOrderList.Cols[5].Visible = false;
                gridOrderList.Cols[6].Visible = false;


                gridOrderList.Cols[2].Visible = true;
                gridOrderList.Cols[2].Width = 120;
                gridOrderList.Cols[2].AllowDragging = false;
                gridOrderList.Cols[2].AllowEditing = false;
                gridOrderList.Cols[2].AllowMerging = false;
                gridOrderList.Cols[2].AllowResizing = true;
                gridOrderList.Cols[2].AllowSorting = true;
                gridOrderList.Cols[2].Caption = "Заказ №";

                gridOrderList.Cols[4].Visible = true;
                gridOrderList.Cols[4].Width = 110;
                gridOrderList.Cols[4].AllowDragging = false;
                gridOrderList.Cols[4].AllowEditing = false;
                gridOrderList.Cols[4].AllowMerging = false;
                gridOrderList.Cols[4].AllowResizing = true;
                gridOrderList.Cols[4].AllowSorting = true;
                gridOrderList.Cols[4].Caption = "Прием";
                gridOrderList.Cols[4].Format = "g";

                gridOrderList.Cols[5].Visible = true;
                gridOrderList.Cols[5].Width = 110;
                gridOrderList.Cols[5].AllowDragging = false;
                gridOrderList.Cols[5].AllowEditing = false;
                gridOrderList.Cols[5].AllowMerging = false;
                gridOrderList.Cols[5].AllowResizing = true;
                gridOrderList.Cols[5].AllowSorting = true;
                gridOrderList.Cols[5].Caption = "Выдача";
                gridOrderList.Cols[5].Format = "g";

                if (fRow == null)
                    fRow = new FilterRowLike(gridOrderList);
                int selrow = 2;
                for (int i = 2; i < gridOrderList.Rows.Count; i++)
                {
                    try
                    {
                        if (selid == int.Parse(gridOrderList.GetData(i, 1).ToString()))
                            selrow = i;
                    }
                    catch (Exception ex)
                    {
                        ErrorNfo.WriteErrorInfo(ex);
                    }
                    if (gridOrderList[i, 6].ToString().Trim() == "0")
                        gridOrderList.Rows[i].Style = gridOrderList.Styles["MyNull"];
                    else if (gridOrderList[i, 6].ToString().Trim() == usr.Id_user.ToString().Trim())
                        gridOrderList.Rows[i].Style = gridOrderList.Styles["MyMy"];
                    else if (gridOrderList[i, 6].ToString().Trim() != usr.Id_user.ToString().Trim())
                        gridOrderList.Rows[i].Style = gridOrderList.Styles["MyNotMy"];
                    if ((gridOrderList[i, 3].ToString().Trim() == "000211") || (gridOrderList[i, 3].ToString().Trim() == "000212"))
                        gridOrderList.Rows[i].Style = gridOrderList.Styles["MyOK"];
                    if (gridOrderList[i, 3].ToString().Trim() == "000111")
                        gridOrderList.Rows[i].Style = gridOrderList.Styles["In"];

                }

                if (selrow < gridOrderList.Rows.Count)
                    gridOrderList.Select(selrow, 2);
            }
		}


        private void QuickLoadOrder(bool barcode)
        {
            if (CheckState(db_connection))
            {
                tmr.Stop();
                try
                {
                    if (((int.Parse(gridOrderList.GetData(gridOrderList.Row, 1).ToString()) > 0) && (!barcode)) || ((txtBarCode.Text.Length > 11) && (barcode)))
                    {
                    	
                        if (barcode)
                            db_command = new SqlCommand("SELECT [id_order], [client], [category], [name_accept], [name_designer], [number], [input_date], [expected_date], [comment], [crop], [type], [id_user_designer], [preview], [status] FROM [vwOrderQuickListDesigner] WHERE RTRIM([number]) = RTRIM('" + txtBarCode.Text.Substring(0, 12) + "')", db_connection);
                        else
                            db_command = new SqlCommand("SELECT [id_order], [client], [category], [name_accept], [name_designer], [number], [input_date], [expected_date], [comment], [crop], [type], [id_user_designer], [preview], [status] FROM [vwOrderQuickListDesigner] WHERE [id_order] = " + int.Parse(gridOrderList.GetData(gridOrderList.Row, 1).ToString()), db_connection);

						db_command.CommandTimeout = 900;
                        SqlDataReader tmp = db_command.ExecuteReader();
                        if (tmp.Read())
                        {
                            if ((!tmp.IsDBNull(11)) && (!tmp.IsDBNull(13)))
                            {
                                bool load = false;
								if ((tmp.GetString(13).Trim() == "000200") || (tmp.GetString(13).Trim() == "000210") || (tmp.GetString(13).Trim() == "000212") || (tmp.GetString(13).Trim() == "000211") || (tmp.GetString(13).Trim() == "000111"))
                                {
                                    if ((tmp.GetInt32(11) == usr.Id_user) || (tmp.GetInt32(11) == 0))
                                    {
                                        load = true;
                                    }
                                    else
                                    {
                                        if (MessageBox.Show("Заказ уже обрабатывается другим дизайнером! Открыть?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                                            load = true;
                                        else
                                            load = false;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Заказ не может быть открыт, он находится вне зоны Вашей отвественности!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    load = false;
                                }
                                if (load)
                                {
                                    if (!tmp.IsDBNull(0))
                                        lblFInfoOrderID.Text = tmp.GetInt32(0).ToString();
                                    else
                                        lblFInfoOrderID.Text = "";

                                    //if (!tmp.IsDBNull(1))
                                    //	lblFInfoOrderClient.Text = tmp.GetString(1);
                                    //else
                                    //	lblFInfoOrderClient.Text = "";

                                    //if (!tmp.IsDBNull(2))
                                    //	lblFInfoOrderClient.Text += " " + tmp.GetString(2);
                                    //else
                                    //	lblFInfoOrderClient.Text += "";

                                    if (!tmp.IsDBNull(3))
                                        lblFInfoOrderAcceptance.Text = tmp.GetString(3);
                                    else
                                        lblFInfoOrderAcceptance.Text = "";

                                    if (!tmp.IsDBNull(5))
                                        lblFInfoOrderNo.Text = tmp.GetString(5);
                                    else
                                        lblFInfoOrderNo.Text = "";

                                    if (!tmp.IsDBNull(6))
                                        lblFInfoOrderDateIn.Text = tmp.GetDateTime(6).ToShortDateString() + " " + tmp.GetDateTime(6).ToShortTimeString();
                                    else
                                        lblFInfoOrderDateIn.Text = "";

                                    if (!tmp.IsDBNull(7))
                                        lblFInfoOrderDateOut.Text = tmp.GetDateTime(7).ToShortDateString() + " " + tmp.GetDateTime(7).ToShortTimeString();
                                    else
                                        lblFInfoOrderDateOut.Text = "";

                                    //if (!tmp.IsDBNull(8))
                                    //	lblFInfoOrderComment.Text = tmp.GetString(8);
                                    //else
                                    //	lblFInfoOrderComment.Text = "";

                                    try
                                    {
                                        string y = DateTime.Parse(lblFInfoOrderDateIn.Text).Year.ToString();
                                        string m = DateTime.Parse(lblFInfoOrderDateIn.Text).Month < 10
                                                    ? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString()
                                                    : DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString();
                                        string d = DateTime.Parse(lblFInfoOrderDateIn.Text).Day < 10
                                                    ? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString()
                                                    : DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString();
                                        string f = prop.Dir_edit + "\\" + y + "\\" + m + "\\" + d + "\\" + lblFInfoOrderNo.Text.Trim() + "\\" +
                                                   lblFInfoOrderNo.Text.Trim() + ".txt";
                                        txtFileInfo.Text = "";
                                        if (File.Exists(f))
                                        {
                                            System.IO.StreamReader s = new System.IO.StreamReader(f, System.Text.Encoding.GetEncoding(1251));
                                            while (s.Peek() >= 0)
                                            {
                                                txtFileInfo.Text += s.ReadLine() + "\n";
                                            }
                                            s.Close();

                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        ErrorNfo.WriteErrorInfo(ex);
                                        MessageBox.Show("Ошибка при открытии файла информации", "Модуль оператора", MessageBoxButtons.OK,
                                                        MessageBoxIcon.Warning);
                                    }

                                    if (!tmp.IsDBNull(9))
                                        switch (tmp.GetInt32(9))
                                        {
                                            case 1:
                                                {
                                                    lblFInfoOrderCrop.Text = "Обрезать под формат";
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    lblFInfoOrderCrop.Text = "Сохранить пропорции";
                                                    break;
                                                }
                                            case 3:
                                                {
                                                    lblFInfoOrderCrop.Text = "Реальный размер";
                                                    break;
                                                }
                                        }
                                    else
                                        lblFInfoOrderCrop.Text = "";

                                    if (!tmp.IsDBNull(10))
                                        switch (tmp.GetInt32(10))
                                        {
                                            case 1:
                                                {
                                                    lblFInfoOrderPaper.Text = "Глянцевая";
                                                    break;
                                                }
                                            case 2:
                                                {
                                                    lblFInfoOrderPaper.Text = "Матовая";
                                                    break;
                                                }
                                        }
                                    else
                                        lblFInfoOrderPaper.Text = "";

                                    if (!tmp.IsDBNull(12))
                                        if (tmp.GetBoolean(12))
                                            checkPreview.Checked = true;
                                        else
                                            checkPreview.Checked = false;
                                    else
                                        checkPreview.Checked = false;

                                    checkPreview.Enabled = false;

                                    //btnAdvInfo.Enabled = true;
                                }
                                else
                                {
                                    ClearOrders();
                                }
                            }
                            else
                            {
                                if (!tmp.IsDBNull(0))
                                    lblFInfoOrderID.Text = tmp.GetInt32(0).ToString();
                                else
                                    lblFInfoOrderID.Text = "";

                                //if (!tmp.IsDBNull(1))
                                //	lblFInfoOrderClient.Text = tmp.GetString(1);
                                //else
                                //	lblFInfoOrderClient.Text = "";

                                //if (!tmp.IsDBNull(2))
                                //	lblFInfoOrderClient.Text += " " + tmp.GetString(2);
                                //else
                                //	lblFInfoOrderClient.Text += "";

                                if (!tmp.IsDBNull(3))
                                    lblFInfoOrderAcceptance.Text = tmp.GetString(3);
                                else
                                    lblFInfoOrderAcceptance.Text = "";

                                if (!tmp.IsDBNull(5))
                                    lblFInfoOrderNo.Text = tmp.GetString(5);
                                else
                                    lblFInfoOrderNo.Text = "";

                                if (!tmp.IsDBNull(6))
                                    lblFInfoOrderDateIn.Text = tmp.GetDateTime(6).ToShortDateString() + " " + tmp.GetDateTime(6).ToShortTimeString();
                                else
                                    lblFInfoOrderDateIn.Text = "";

                                if (!tmp.IsDBNull(7))
                                    lblFInfoOrderDateOut.Text = tmp.GetDateTime(7).ToShortDateString() + " " + tmp.GetDateTime(7).ToShortTimeString();
                                else
                                    lblFInfoOrderDateOut.Text = "";

                                //if (!tmp.IsDBNull(8))
                                //	lblFInfoOrderComment.Text = tmp.GetString(8);
                                //else
                                //	lblFInfoOrderComment.Text = "";

                                if (!tmp.IsDBNull(9))
                                    switch (tmp.GetInt32(9))
                                    {
                                        case 1:
                                            {
                                                lblFInfoOrderCrop.Text = "Обрезать под формат";
                                                break;
                                            }
                                        case 2:
                                            {
                                                lblFInfoOrderCrop.Text = "Сохранить пропорции";
                                                break;
                                            }
                                        case 3:
                                            {
                                                lblFInfoOrderCrop.Text = "Реальный размер";
                                                break;
                                            }
                                    }
                                else
                                    lblFInfoOrderCrop.Text = "";

                                if (!tmp.IsDBNull(10))
                                    switch (tmp.GetInt32(10))
                                    {
                                        case 1:
                                            {
                                                lblFInfoOrderPaper.Text = "Глянцевая";
                                                break;
                                            }
                                        case 2:
                                            {
                                                lblFInfoOrderPaper.Text = "Матовая";
                                                break;
                                            }
                                    }
                                else
                                    lblFInfoOrderPaper.Text = "";

                                if (!tmp.IsDBNull(12))
                                    if (tmp.GetBoolean(12))
                                        checkPreview.Checked = true;
                                    else
                                        checkPreview.Checked = false;
                                else
                                    checkPreview.Checked = false;

                                checkPreview.Enabled = false;

                                //btnAdvInfo.Enabled = true;
                            }
                        }
                        tmp.Close();

                        db_command = new SqlCommand("SELECT [id_orderbody], [id_order], [id_good], RTRIM([name]) AS [name], [quantity], '=' AS [AS], [actual_quantity], '+' AS [PLUS], '-' AS [SUB], [dateadd] FROM [vwOrderBodyDesigner] WHERE [id_order] = " + lblFInfoOrderID.Text + " ORDER BY [id_good]", db_connection);
                        SqlDataAdapter tmp_a = new SqlDataAdapter(db_command);
                        DataTable tmp_t = new DataTable("orderbody");
                        tmp_a.Fill(tmp_t);

                        /*
                         *  1 [id_orderbody]
                         *  2 [id_order], 
                         *  3 [id_good], 
                         *  4 [name], 
                         *  5 [quantity],
                         *  6 =
                         *  7 [actual_quantity]
                         *  8 +
                         *  9 -
                         *  10 [datework]
                         */
                        gridEditOrder.DataSource = tmp_t;

                        gridEditOrder.Cols[1].Visible = false;
                        gridEditOrder.Cols[2].Visible = false;
                        gridEditOrder.Cols[3].Visible = false;
                        gridEditOrder.Cols[4].Visible = false;
                        gridEditOrder.Cols[5].Visible = false;
                        gridEditOrder.Cols[6].Visible = false;
                        gridEditOrder.Cols[7].Visible = false;
                        gridEditOrder.Cols[8].Visible = false;
                        gridEditOrder.Cols[9].Visible = false;
                        gridEditOrder.Cols[10].Visible = false;

                        gridEditOrder.Cols[4].Visible = true;
                        gridEditOrder.Cols[4].Width = 270;
                        gridEditOrder.Cols[4].AllowDragging = false;
                        gridEditOrder.Cols[4].AllowEditing = false;
                        gridEditOrder.Cols[4].AllowMerging = false;
                        gridEditOrder.Cols[4].AllowResizing = true;
                        gridEditOrder.Cols[4].AllowSorting = true;
                        gridEditOrder.Cols[4].Caption = "Наименование";

                        gridEditOrder.Cols[5].Visible = true;
                        gridEditOrder.Cols[5].Width = 100;
                        gridEditOrder.Cols[5].AllowDragging = false;
                        gridEditOrder.Cols[5].AllowEditing = false;
                        gridEditOrder.Cols[5].AllowMerging = false;
                        gridEditOrder.Cols[5].AllowResizing = true;
                        gridEditOrder.Cols[5].AllowSorting = true;
                        gridEditOrder.Cols[5].Caption = "Кол-во";
						if (prop.Round3)
							gridEditOrder.Cols[5].Format = "N3";
						else
							gridEditOrder.Cols[5].Format = "N2";

                        gridEditOrder.Cols[7].Visible = true;
                        gridEditOrder.Cols[7].Width = 100;
                        gridEditOrder.Cols[7].AllowDragging = false;
                        gridEditOrder.Cols[7].AllowEditing = true;
                        gridEditOrder.Cols[7].AllowMerging = false;
                        gridEditOrder.Cols[7].AllowResizing = true;
                        gridEditOrder.Cols[7].AllowSorting = true;
                        gridEditOrder.Cols[7].Caption = "Реально";
						if (prop.Round3)
							gridEditOrder.Cols[7].Format = "N3";
						else
							gridEditOrder.Cols[7].Format = "N2";

                        gridEditOrder.Cols[6].Visible = true;
                        gridEditOrder.Cols[6].Width = 20;
                        gridEditOrder.Cols[6].AllowDragging = false;
                        gridEditOrder.Cols[6].AllowEditing = false;
                        gridEditOrder.Cols[6].AllowMerging = false;
                        gridEditOrder.Cols[6].AllowResizing = true;
                        gridEditOrder.Cols[6].AllowSorting = false;
                        gridEditOrder.Cols[6].Caption = "=";
                        gridEditOrder.Cols[6].Style = gridEditOrder.Styles["Keys"];

                        gridEditOrder.Cols[8].Visible = true;
                        gridEditOrder.Cols[8].Width = 20;
                        gridEditOrder.Cols[8].AllowDragging = false;
                        gridEditOrder.Cols[8].AllowEditing = false;
                        gridEditOrder.Cols[8].AllowMerging = false;
                        gridEditOrder.Cols[8].AllowResizing = true;
                        gridEditOrder.Cols[8].AllowSorting = false;
                        gridEditOrder.Cols[8].Caption = "+";
                        gridEditOrder.Cols[8].Style = gridEditOrder.Styles["Keys"];

                        gridEditOrder.Cols[9].Visible = true;
                        gridEditOrder.Cols[9].Width = 20;
                        gridEditOrder.Cols[9].AllowDragging = false;
                        gridEditOrder.Cols[9].AllowEditing = false;
                        gridEditOrder.Cols[9].AllowMerging = false;
                        gridEditOrder.Cols[9].AllowResizing = true;
                        gridEditOrder.Cols[9].AllowSorting = false;
                        gridEditOrder.Cols[9].Caption = "-";
                        gridEditOrder.Cols[9].Style = gridEditOrder.Styles["Keys"];

                        DateTime tmp_dateend = DateTime.Now.AddYears(-20);
						db_command = new SqlCommand("SELECT MAX(event_date) AS event_date, event_status, id_order FROM orderevent WHERE (event_text LIKE 'Блокирование строчек%') GROUP BY event_status, id_order HAVING (id_order = " + lblFInfoOrderID.Text + ")", db_connection);
						SqlDataReader tmp_r = db_command.ExecuteReader();
                        if (tmp_r.Read())
                        {
                            try
                            {
                                tmp_dateend = tmp_r.GetDateTime(0);
                            }
                            catch (Exception)
                            {
                                //throw;
                            }
                        }
                        tmp_r.Close();


                        for (int i = 1; i < gridEditOrder.Rows.Count; i++)
                        {
                            if (gridEditOrder.Rows[i][3].ToString() == txtFilterGoods.SelectedValue.ToString())
                            {
                                gridEditOrder.Rows[i].Style = gridEditOrder.Styles["ActiveGood"];
                                if (i == 1)
                                {
                                    gridEditOrder.Styles["Highlight"].ForeColor = Color.MediumBlue;
                                    gridEditOrder.Styles["Focus"].ForeColor = Color.MediumBlue;
                                }
                                else
                                {
                                    gridEditOrder.Styles["Highlight"].ForeColor = Color.DarkGray;
                                    gridEditOrder.Styles["Focus"].ForeColor = Color.DarkGray;
                                }
                                break;
                            }
                            if (decimal.Parse(gridEditOrder.Rows[i][7].ToString()) < 0)
                            {
                                gridEditOrder.Rows[i].Style = gridEditOrder.Styles["Discarding"];
                                gridEditOrder.Rows[i].AllowEditing = false;
                            }
                            if ((gridEditOrder.GetData(i, 10) != null) && (gridEditOrder.GetData(i, 10).ToString() != ""))
                            {
								if (((DateTime.Parse(DateTime.Parse(gridEditOrder.GetData(i, 10).ToString()).ToShortDateString() + " " + DateTime.Parse(gridEditOrder.GetData(i, 10).ToString()).ToShortTimeString()) < DateTime.Parse(tmp_dateend.ToShortDateString() + " " + tmp_dateend.ToShortTimeString())) && (DateTime.Now.AddYears(-20).Year != tmp_dateend.Year)) || ((decimal.Parse(gridEditOrder.GetData(i, 5).ToString()) == 0) && (decimal.Parse(gridEditOrder.GetData(i, 7).ToString()) < 0)))
                                {
                                    gridEditOrder.Rows[i].AllowEditing = false;
                                    gridEditOrder.Rows[i][6] = "";
                                    gridEditOrder.Rows[i][8] = "";
                                    gridEditOrder.Rows[i][9] = "";
                                    //gridEditOrder.Rows[i][11] = "";
                                }
                            }
                            else
                            {
                                gridEditOrder.Rows[i].AllowEditing = true;
                            }

                        }
                        gridEditOrder.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    //ErrorNfo.WriteErrorInfo(ex);
                }
                finally
                {
                }
                tmr.Start();
            }
        }

		private void ClearOrders()
		{

			lblFInfoOrderAcceptance.Text = "";
			//lblFInfoOrderClient.Text = "";
			//lblFInfoOrderComment.Text = "";
			lblFInfoOrderCrop.Text = "";
			lblFInfoOrderDateOut.Text = "";
			lblFInfoOrderDateIn.Text = "";
			lblFInfoOrderID.Text = "";
			lblFInfoOrderNo.Text = "";
			lblFInfoOrderPaper.Text = "";
			checkPreview.Checked = false;
			txtFileInfo.Text = "";


			gridEditOrder.DataSource = "";
			int tmp = gridEditOrder.Rows.Count;
			for (int i = tmp; i > 1; i--)
				gridEditOrder.Rows.Remove(gridEditOrder.Rows.Count - 1);

			//btnAdvInfo.Enabled = false;
			checkPreview.Enabled = false;

		}

        private void FullLoadOrder()
        {
            if (CheckState(db_connection))
            {
                tmr.Stop();
                try
                {
                    if (int.Parse(lblFInfoOrderID.Text) > 0)
                    {
                        db_command = new SqlCommand("UPDATE [order] SET [id_user_designer] = " + usr.Id_user.ToString() + ", [name_designer] = '" + usr.Name + "', [status] = '000210', exported = 0 WHERE [id_order] = " + lblFInfoOrderID.Text, db_connection);
                        db_command.ExecuteNonQuery();

                        AddEvent("Дизайнер начал работу над заказом", int.Parse(lblFInfoOrderID.Text));

                        groupFilter.Enabled = false;
                        groupOrderList.Enabled = false;
                        gridEditOrder.Enabled = true;

                        btnSaveOrder.Enabled = true;
                        //btnCancelOrder.Enabled = true;
                        btnEndOrder.Enabled = true;
                        btnOpenOrder.Enabled = false;

                        gridEditOrder.Styles["Highlight"].ForeColor = Color.Green;
                        gridEditOrder.Styles["Focus"].ForeColor = Color.Green;

                        checkPreview.Enabled = true;
                        txtFileInfo.ReadOnly = false;
                        txtBarCode.Enabled = false;

                    }
                }
                catch (Exception ex)
                {
                    ErrorNfo.WriteErrorInfo(ex);
                }
                finally
                {
                }
                tmr.Start();
            }
        }

        private void SaveOrder()
        {
            if (CheckState(db_connection))
            {
                tmr.Stop();
                int order_id = 0;
                string query = "";
                try
                {
                    for (int i = 1; i < gridEditOrder.Rows.Count; i++)
                    {
                        if (gridEditOrder.Rows[i][7].ToString() == "")
                            gridEditOrder.Rows[i][7] = 0;
                        if ((decimal.Parse(gridEditOrder.Rows[i][7].ToString()) > 0) &&
                            (gridEditOrder.Rows[i][7].ToString().ToLower() != "null"))
                        {
                            string yin, min, din;
                            yin = DateTime.Now.Year.ToString();

                            if (DateTime.Now.Month < 10)
                                min = "0" + DateTime.Now.Month.ToString();
                            else
                                min = DateTime.Now.Month.ToString();

                            if (DateTime.Now.Day < 10)
                                din = "0" + DateTime.Now.Day.ToString();
                            else
                                din = DateTime.Now.Day.ToString();

                            SqlCommand t =
                                new SqlCommand(
                                    "SELECT [actual_quantity], [id_order] FROM [orderbody] WHERE [id_orderbody] = " +
                                    gridEditOrder.Rows[i][1].ToString(), db_connection);
                            SqlDataReader r = t.ExecuteReader();
                            if (r.Read())
                            {
                                query += "UPDATE [order] SET [exported] = 0 WHERE [id_order] = " + r.GetInt32(1) + ";";
                                order_id = r.GetInt32(1);
                                if (!r.IsDBNull(1))
                                {
                                    if (r.GetDecimal(0) != decimal.Parse(gridEditOrder.Rows[i][7].ToString()))
                                    {
                                        query += "UPDATE [orderbody] SET [actual_quantity] = " + gridEditOrder.Rows[i][7].ToString().Replace(",", ".") +
                                                 ", [datework] = '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() +
                                                 "', [id_user_work] = " + usr.Id_user + ", [name_work] = '" + usr.Name +
                                                 "', exported = 0 WHERE [id_orderbody] = " + gridEditOrder.Rows[i][1].ToString() + ";\n";
                                    }
                                }
                                else
                                {
                                    query += "UPDATE [orderbody] SET [actual_quantity] = " + gridEditOrder.Rows[i][7].ToString().Replace(",", ".") +
                                             ", [datework] = '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() +
                                             "', [id_user_work] = " + usr.Id_user + ", [name_work] = '" + usr.Name +
                                             "', exported = 0 WHERE [id_orderbody] = " + gridEditOrder.Rows[i][1].ToString() + ";\n";
                                }
                            }
                            r.Close();
                        }
                        else
                        {
                            query += "UPDATE [orderbody] SET [actual_quantity] = " + gridEditOrder.Rows[i][7].ToString().Replace(",", ".") +
                                     ", exported = 0 WHERE [id_orderbody] = " + gridEditOrder.Rows[i][1].ToString() + ";\n";
                        }
                    }
                    try
                    {
                        string y = DateTime.Parse(lblFInfoOrderDateIn.Text).Year.ToString();
                        string m = DateTime.Parse(lblFInfoOrderDateIn.Text).Month < 10
                                    ? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString()
                                    : DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString();
                        string d = DateTime.Parse(lblFInfoOrderDateIn.Text).Day < 10
                                    ? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString()
                                    : DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString();
                        string f = prop.Dir_edit + "\\" + y + "\\" + m + "\\" + d + "\\" + lblFInfoOrderNo.Text.Trim() + "\\" +
                                   lblFInfoOrderNo.Text.Trim() + ".txt";
                        if (File.Exists(f))
                        {
                            txtFileInfo.SaveFile(f, RichTextBoxStreamType.PlainText);
                        }
                    }
                    catch (Exception ex)
                    {
                        ErrorNfo.WriteErrorInfo(ex);
                        MessageBox.Show("Ошибка при сохранении файла информации", "Модуль оператора", MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    ErrorNfo.WriteErrorInfo(ex);
                }

                db_command = new SqlCommand(query, db_connection);
                if (db_command.CommandText != "")
                    db_command.ExecuteNonQuery();
                tmr.Start();
                AddEvent("Дизайнер сохранил изменения в заказе", order_id);
            }
        }

		private void FinishOrder()
		{
            if (CheckState(db_connection))
            {
                tmr.Stop();
                if (checkPreview.Checked)
                {
                    db_command =
                        new SqlCommand(
                            "UPDATE [order] SET [id_user_designer] = " + usr.Id_user.ToString() + ", [name_designer] = '" + usr.Name +
                            "', [status] = '000212', [preview] = 1, exported = 0 WHERE [id_order] = " + lblFInfoOrderID.Text, db_connection);
                }
                else
                {
                    SqlCommand tmp = new SqlCommand("SELECT COUNT(*) AS CNT " +
                                                    "FROM " +
                                                    "(SELECT dbo.orderbody.id_order, " +
                                                    "dbo.orderbody.id_good, " +
                                                    "dbo.good.type, " +
                                                    "dbo.orderbody.quantity, " +
                                                    "dbo.orderbody.actual_quantity " +
                                                    "FROM dbo.orderbody INNER JOIN " +
                                                    "dbo.good ON dbo.orderbody.id_good = dbo.good.id_good " +
                                                    "WHERE (dbo.good.type = 1) AND " +
                                                    "(dbo.orderbody.actual_quantity <> dbo.orderbody.quantity) AND " +
                                                    "(dbo.orderbody.id_order = " + lblFInfoOrderID.Text + ")) AS TBL", db_connection);
                    if ((int)tmp.ExecuteScalar() > 0)
                    {
                        db_command =
                            new SqlCommand(
                                "UPDATE [order] SET [id_user_designer] = " + usr.Id_user.ToString() + ", [name_designer] = '" + usr.Name +
                                "', [status] = '000211', [preview] = 0, exported = 0 WHERE [id_order] = " + lblFInfoOrderID.Text, db_connection);
                        AddEvent("Дизайнер завершил работу над заказом", int.Parse(lblFInfoOrderID.Text));
                    }
                    else
                    {
                        db_command =
                            new SqlCommand(
                                "UPDATE [order] SET [id_user_designer] = " + usr.Id_user.ToString() + ", [name_designer] = '" + usr.Name +
                                "', [status] = '000211', [preview] = 0, exported = 0 WHERE [id_order] = " + lblFInfoOrderID.Text, db_connection);
                        AddEvent("Дизайнер завершил работу над заказом", int.Parse(lblFInfoOrderID.Text));
                    }
                }
                db_command.ExecuteNonQuery();
                tmr.Start();
            }
		}

		private void HideWindow()
		{
			this.Top = (-1) * (this.Height - 10);
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

			checkPreview.Enabled = false;
			//btnAdvInfo.Enabled = false;

			gridEditOrder.Styles["Highlight"].ForeColor = Color.DarkGray;
			gridEditOrder.Styles["Focus"].ForeColor = Color.DarkGray;

			FillOrderList();
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

            txtFileInfo.ReadOnly = true;
            
            gridEditOrder.Enabled = false;

			gridEditOrder.Styles["Highlight"].ForeColor = Color.DarkGray;
			gridEditOrder.Styles["Focus"].ForeColor = Color.DarkGray;

			FillOrderList();
			txtBarCode.Enabled = true;

			tmr.Start();
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

		private void btnOpenOrder_Click(object sender, EventArgs e)
		{
            tmr.Stop();
            FullLoadOrder();
            tmr.Start();
		}

		private void gridEditOrder_Click(object sender, EventArgs e)
		{
			if (gridEditOrder.Row > 0)
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
				}
			}
		}

		private void gridEditOrder_DoubleClick(object sender, EventArgs e)
		{
			if (gridEditOrder.Row > 0)
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
				}
			}
		}

		private void btnAdvInfo_Click(object sender, EventArgs e)
		{
			try
			{
				string y = DateTime.Parse(lblFInfoOrderDateIn.Text).Year.ToString();
				string m = DateTime.Parse(lblFInfoOrderDateIn.Text).Month < 10
				           	? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString()
				           	: DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString();
				string d = DateTime.Parse(lblFInfoOrderDateIn.Text).Day < 10
				           	? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString()
				           	: DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString();
				string f = prop.Dir_edit + "\\" + y + "\\" + m + "\\" + d + "\\" + lblFInfoOrderNo.Text.Trim() + "\\" +
				           lblFInfoOrderNo.Text.Trim() + ".txt";
				if (File.Exists(f))
				{
					System.Diagnostics.Process.Start(f);
				}
			}
			catch(Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Ошибка при открытии файла информации", "Модуль дизайнера", MessageBoxButtons.OK,
				                MessageBoxIcon.Warning);
			}
		}

		private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
		{
			this.Top = 0;
			this.Focus();
			this.Activate();
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
						e.Cancel = false;
					else
						e.Cancel = true;
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

		private void btnHide_Click(object sender, EventArgs e)
		{
			HideWindow();
		}

		private void LogoffToolStripMenuItem_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			bool tmp_login_ok = false;
			bool tmp_exit = false;

			hidewin = false;

			this.TopMost = prop.Mod_designer_top_most;

			this.TopMost = false;

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
								if (fLogin.usr.prmCanLoginDesigner)
								{
									this.Show();
								}
								else
								{
									tmp_exit = true;
									MessageBox.Show("Доступ в модуль дизайнера заперщен!", "Вход в программу", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    this.Text = "Дизайнер - " + usr.Name + " - Photoland System Automation " + Application.ProductVersion;

					//запрашиваем машину и бумагу
					this.TopMost = false;
					this.TopMost = prop.Mod_designer_top_most;
					// Показываем окно
					this.Focus();

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
					FillDiscardTable();

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
			this.TopMost = prop.Mod_designer_top_most;
			tmr.Start();
		}

		private void mnuSetup_Click(object sender, EventArgs e)
		{
			OpenSettings();
		}

        private void btnAddBrak_Click(object sender, EventArgs e)
        {
            if (CheckState(db_connection))
            {
                this.TopMost = false;

                frmDiscard f = new frmDiscard();
                f.db_connection = db_connection;
                f.usr = usr;
                f.txtOrderNo.Text = lblFInfoOrderNo.Text;
                f.ShowDialog();
                if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                    FillDiscardTable();

                this.TopMost = prop.Mod_designer_top_most;
            }
        }

        private void btnEditBrak_Click(object sender, EventArgs e)
        {
            if (CheckState(db_connection))
            {
				try
				{
					if (gridTehAction.GetData(gridTehAction.Row, 1).ToString() != "")
					{
						this.TopMost = false;

						frmDiscard f = new frmDiscard();
						f.db_connection = db_connection;
						f.usr = usr;
						f.txtOrderNo.Text = lblFInfoOrderNo.Text;
						f.Id = int.Parse(gridTehAction.GetData(gridTehAction.Row, 1).ToString());
						f.ShowDialog();
						if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
							FillDiscardTable();

						this.TopMost = prop.Mod_designer_top_most;
					}
				}
				catch
				{
					
				}
            }
        }

        private void btnDelBrak_Click(object sender, EventArgs e)
        {
            if (CheckState(db_connection))
            {
                tmr.Stop();
                if (MessageBox.Show("Удалить запись о списании?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                {
                    try
                    {
                        if (int.Parse(gridTehAction.GetData(gridTehAction.Row, 1).ToString()) > 0)
                        {
                            SqlCommand t =
                                new SqlCommand(
                                    "UPDATE [discard] SET [del] = 1, exported = 0 WHERE [id_discard] = " +
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
                        FillDiscardTable();
                    }
                }
                tmr.Start();
            }
        }

        private void ShowReport(string rep_name, bool export, C1.Win.C1Report.FileFormatEnum format)
        {
            if (CheckState(db_connection))
            {
                tmr.Stop();
                this.TopMost = false;
                if (prop.PathReportsTemplates != "")
                {
                    frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
                    if (usr.prmCanLoginAdmin)
                        f.checkCurentUser.Enabled = true;
                    else
                        f.checkCurentUser.Enabled = false;
                    f.ShowDialog();
                    if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        bool ok = false;
                        DataTable r = new DataTable("Report");
                        SqlCommand c;
                        SqlDataAdapter a;
                        string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
                        switch (rep_name)
                        {
							case "Work by service 1":
								{
									/*
									 SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) < CONVERT(DATETIME, '2009-01-01 00:00:00', 102)) ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated WHERE (dbo.good.type = N'1') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd HAVING (SUM(orderbody_1.actual_quantity) > 0) AND (datecnt.dated > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (datecnt.dated < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
									 */
									//frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									//f.ShowDialog();
									if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
									{
										string filter = "";
										string filter2 = "";
										filter =
											" (datecnt.dated >= CONVERT(DATETIME, '" +
											f.Y1 + "-" +
											f.M1 + "-" +
											f.D1 +
											" 00:00:00', 102) AND datecnt.dated <= CONVERT(DATETIME, '" +
											f.Y2 + "-" +
											f.M2 + "-" +
											f.D2 + " 23:59:59', 102))";
										filter2 =
											" (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) >= CONVERT(DATETIME, '" +
											f.Y1 + "-" +
											f.M1 + "-" +
											f.D1 +
											" 00:00:00', 102) AND DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) <= CONVERT(DATETIME, '" +
											f.Y2 + "-" +
											f.M2 + "-" +
											f.D2 + " 23:59:59', 102))";
										if (f.checkCurentUser.Checked)
											filter += " AND (orderbody_1.id_user_work = " + usr.Id_user + ")";
										string query =
											"SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd, orderbody_1.id_user_work FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING " + filter2 + " ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated WHERE (dbo.good.type = N'2') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd, orderbody_1.id_user_work HAVING (SUM(orderbody_1.actual_quantity) > 0) AND " + filter;
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
							case "Work by service 1 adv":
								{
									/*
									 SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) < CONVERT(DATETIME, '2009-01-01 00:00:00', 102)) ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated WHERE (dbo.good.type = N'1') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd HAVING (SUM(orderbody_1.actual_quantity) > 0) AND (datecnt.dated > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (datecnt.dated < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
									 */
									//frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									//f.ShowDialog();
									if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
									{
										string filter = "";
										string filter2 = "";
										filter =
											" (datecnt.dated >= CONVERT(DATETIME, '" +
											f.Y1 + "-" +
											f.M1 + "-" +
											f.D1 +
											" 00:00:00', 102) AND datecnt.dated <= CONVERT(DATETIME, '" +
											f.Y2 + "-" +
											f.M2 + "-" +
											f.D2 + " 23:59:59', 102))";
										filter2 =
											" (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) >= CONVERT(DATETIME, '" +
											f.Y1 + "-" +
											f.M1 + "-" +
											f.D1 +
											" 00:00:00', 102) AND DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) <= CONVERT(DATETIME, '" +
											f.Y2 + "-" +
											f.M2 + "-" +
											f.D2 + " 23:59:59', 102))";
										if (f.checkCurentUser.Checked)
											filter += " AND (orderbody_1.id_user_work = " + usr.Id_user + ")";
										string query =
											"SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd, orderbody_1.id_user_work, dbo.[order].number FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING " + filter2 + " ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated INNER JOIN dbo.[order] ON orderbody_1.id_order = dbo.[order].id_order WHERE (dbo.good.type = N'2') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd, orderbody_1.id_user_work, dbo.[order].number HAVING (SUM(orderbody_1.actual_quantity) > 0) AND " + filter;
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
							case "Discarding designer 1":
                                {
                                    c =
                                        new SqlCommand(
                                            "SELECT id_discard, datediscard, id_material, material, quantity, comment, user_name, orderno, id_user, CONVERT(char, datediscard, 103) AS d FROM dbo.vwDiscardList WHERE (datediscard >= CONVERT(datetime, '" +
                                            f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120)) AND (datediscard <= CONVERT(datetime, '" + f.Y2 + "/" +
                                            f.M2 + "/" + f.D2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ")", db_connection);
									c.CommandTimeout = 9000;
									a = new SqlDataAdapter(c);
                                    a.Fill(r);
                                    rep.Load(prop.PathReportsTemplates, rep_name);
                                    rep.DataSource.Recordset = r;
                                    rep.Fields["lblDates"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
                                    if (f.Userfilter) rep.Fields["lblUser"].Text = "по пользователю " + usr.Name;
                                    ok = true;
                                    break;
                                }
                            case "Discarding designer 2":
                                {
                                    c =
                                        new SqlCommand(
                                            "SELECT material, SUM(quantity) AS quantity, user_name, CONVERT(char, datediscard, 103) AS d " +
                                            "FROM dbo.vwDiscardList " +
                                            "WHERE (datediscard >= CONVERT(datetime, '" +
                                            f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120)) AND (datediscard <= CONVERT(datetime, '" + f.Y2 + "/" +
                                            f.M2 + "/" + f.D2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ") " +
                                            "GROUP BY material, user_name, CONVERT(char, datediscard, 103) ", db_connection);
									c.CommandTimeout = 9000;
									a = new SqlDataAdapter(c);
                                    a.Fill(r);
                                    rep.Load(prop.PathReportsTemplates, rep_name);
                                    rep.DataSource.Recordset = r;
                                    rep.Fields["lblDates"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
                                    if (f.Userfilter) rep.Fields["lblUser"].Text = "по пользователю " + usr.Name;
                                    ok = true;
                                    break;
                                }
                            case "Discarding designer 3":
                                {
                                    c =
                                        new SqlCommand(
                                            "SELECT material, SUM(quantity) AS quantity, CONVERT(char, datediscard, 103) AS d " +
                                            "FROM dbo.vwDiscardList " +
                                            "WHERE (datediscard >= CONVERT(datetime, '" +
                                            f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120)) AND (datediscard <= CONVERT(datetime, '" + f.Y2 + "/" +
                                            f.M2 + "/" + f.D2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ") " +
                                            "GROUP BY material, CONVERT(char, datediscard, 103)", db_connection);
									c.CommandTimeout = 9000;
									a = new SqlDataAdapter(c);
                                    a.Fill(r);
                                    rep.Load(prop.PathReportsTemplates, rep_name);
                                    rep.DataSource.Recordset = r;
                                    rep.Fields["lblDates"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
                                    if (f.Userfilter) rep.Fields["lblUser"].Text = "по пользователю " + usr.Name;
                                    ok = true;
                                    break;
                                }
                            case "Discarding designer 4":
                                {
                                    c =
                                        new SqlCommand(
                                            "SELECT material, SUM(quantity) AS quantity " +
                                            "FROM dbo.vwDiscardList " +
                                            "WHERE (datediscard >= CONVERT(datetime, '" +
                                            f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120)) AND (datediscard <= CONVERT(datetime, '" + f.Y2 + "/" +
                                            f.M2 + "/" + f.D2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ") " +
                                            "GROUP BY material ", db_connection);
									c.CommandTimeout = 9000;
									a = new SqlDataAdapter(c);
                                    a.Fill(r);
                                    rep.Load(prop.PathReportsTemplates, rep_name);
                                    rep.DataSource.Recordset = r;
                                    rep.Fields["lblDates"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
                                    if (f.Userfilter) rep.Fields["lblUser"].Text = "по пользователю " + usr.Name;
                                    ok = true;
                                    break;
                                }
                            case "Discarding designer 5":
                                {
                                    c =
                                        new SqlCommand(
                                            "SELECT datediscard, material, quantity, comment, user_name " +
                                            "FROM dbo.vwDiscardList " +
                                            "WHERE (datediscard >= CONVERT(datetime, '" +
                                            f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120)) AND (datediscard <= CONVERT(datetime, '" + f.Y2 + "/" +
                                            f.M2 + "/" + f.D2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ")", db_connection);
									c.CommandTimeout = 9000;
									a = new SqlDataAdapter(c);
                                    a.Fill(r);
                                    rep.Load(prop.PathReportsTemplates, rep_name);
                                    rep.DataSource.Recordset = r;
                                    rep.Fields["lblDates"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
                                    if (f.Userfilter) rep.Fields["lblUser"].Text = "по пользователю " + usr.Name;
                                    ok = true;
                                    break;
                                }
                            case "Work 1":
                                {
                                    //frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
                                    usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
                                    if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                                    {
                                        /*
SELECT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS datework, dbo.orderbody.name_work, dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt, dbo.orderbody.id_user_work FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) GROUP BY DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)), dbo.orderbody.name_work, dbo.good.name, dbo.orderbody.id_user_work HAVING (dbo.orderbody.id_user_work <> 0) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2008/05/05 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '2008/05/08 23:59:59.999', 120)) ORDER BY datework, dbo.orderbody.name_work, dbo.good.name
                                         */
                                        string query =
                                            "SELECT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS datework, dbo.orderbody.name_work, dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt, dbo.orderbody.id_user_work FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) GROUP BY DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)), dbo.orderbody.name_work, dbo.good.name, dbo.orderbody.id_user_work ";
                                        query += f.Userfilter
                                                    ? "HAVING (dbo.orderbody.id_user_work = " + usr.Id_user + ")"
                                                    : "HAVING (dbo.orderbody.id_user_work <> 0) ";
                                        query += " AND ((DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '" + f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '" + f.Y2 + "/" + f.M2 + "/" + f.D2 + " 23:59:59.999', 120))) ORDER BY datework, dbo.orderbody.name_work, dbo.good.name";
                                        c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
                                        a.Fill(r);
                                        rep.Load(prop.PathReportsTemplates, rep_name);
                                        rep.DataSource.Recordset = r;
                                        rep.Fields["Filter"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
                                        ok = true;
                                    }
                                    break;
                                }
                            case "Work 2":
                                {
                                    //frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
                                    usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
                                    if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                                    {
                                        /*
SELECT dbo.orderbody.name_work, dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt, dbo.orderbody.id_user_work FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2008/05/05 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '2008/05/08 23:59:59.999', 120)) GROUP BY dbo.orderbody.name_work, dbo.good.name, dbo.orderbody.id_user_work HAVING (dbo.orderbody.id_user_work <> 0) ORDER BY dbo.orderbody.name_work, dbo.good.name                                         */
                                        string query =
                                            "SELECT dbo.orderbody.name_work, dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt, dbo.orderbody.id_user_work FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) ";
                                        query += " AND ((DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '" + f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '" + f.Y2 + "/" + f.M2 + "/" + f.D2 + " 23:59:59.999', 120))) ";
                                        query += "  GROUP BY dbo.orderbody.name_work, dbo.good.name, dbo.orderbody.id_user_work ";
                                        query += f.Userfilter
                                                    ? " HAVING (dbo.orderbody.id_user_work = " + usr.Id_user + ") ORDER BY dbo.orderbody.name_work, dbo.good.name"
                                                    : " HAVING (dbo.orderbody.id_user_work <> 0) ORDER BY dbo.orderbody.name_work, dbo.good.name";
                                        c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
                                        a.Fill(r);
                                        rep.Load(prop.PathReportsTemplates, rep_name);
                                        rep.DataSource.Recordset = r;
                                        rep.Fields["Filter"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
                                        ok = true;
                                    }
                                    break;
                                }
                            case "Work 3":
                                {
                                    //frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
                                    usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
                                    if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                                    {
                                        /*
SELECT dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2008/05/05 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '2008/05/08 23:59:59.999', 120)) AND (dbo.orderbody.id_user_work <> 0) GROUP BY dbo.good.name ORDER BY dbo.good.name     
                                         */
                                        string query =
                                            "SELECT dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) ";
                                        query += " AND ((DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '" + f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '" + f.Y2 + "/" + f.M2 + "/" + f.D2 + " 23:59:59.999', 120))) ";
                                        query += f.Userfilter
                                                    ? " AND (dbo.orderbody.id_user_work = " + usr.Id_user + ") "
                                                    : " AND (dbo.orderbody.id_user_work <> 0) ";
                                        query += "  GROUP BY dbo.good.name ORDER BY dbo.good.name";
                                        c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
                                        a.Fill(r);
                                        rep.Load(prop.PathReportsTemplates, rep_name);
                                        rep.DataSource.Recordset = r;
                                        rep.Fields["Filter"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
                                        ok = true;
                                    }
                                    break;
                                }
                            case "Work designer 1":
                                {
                                    if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
                                    {
                                        string query = "SELECT " +
                                                       "RTRIM(vwOrderBodyDesigner.name) AS name, " +
                                                       "SUM(vwOrderBodyDesigner.actual_quantity) AS quantity, " +
                                                       "vwOrderBodyDesigner.id_user_work, " +
                                                       "RTRIM(vwOrderBodyDesigner.name_work) AS name_work " +
                                                       "FROM vwOrderBodyDesigner INNER JOIN " +
                                                       "[order] ON vwOrderBodyDesigner.id_order = [order].id_order " +
                                                       "WHERE (vwOrderBodyDesigner.datework >= CONVERT(datetime, '" + f.Y1 + "/" + f.M1 + "/" + f.D1 +
                                                       " 00:00:00.000', 120)) " +
                                                       "AND " +
                                                       "(vwOrderBodyDesigner.datework <= CONVERT(datetime, '" + f.Y2 + "/" + f.M2 + "/" + f.D2 +
                                                       " 23:59:59.999', 120)) " +
                                                       "GROUP BY " +
                                                       "vwOrderBodyDesigner.name, " +
                                                       "vwOrderBodyDesigner.id_user_work, " +
                                                       "vwOrderBodyDesigner.name_work " +
                                                       "HAVING " +
                                                       "(SUM(vwOrderBodyDesigner.actual_quantity) > 0) " +
                                                       "AND ";
                                        query += f.Userfilter
                                                    ? "(vwOrderBodyDesigner.id_user_work = " + usr.Id_user + ") "
                                                    : "(vwOrderBodyDesigner.id_user_work <> 0) ";
                                        c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
                                        a.Fill(r);
                                        rep.Load(prop.PathReportsTemplates, rep_name);
                                        rep.DataSource.Recordset = r;
                                        rep.Fields["Header"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
                                        rep.Fields["Header"].Text += f.Userfilter
                                                    ? " Дизайнер: " + usr.Name
                                                    : "";
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
                this.TopMost = prop.Mod_designer_top_most;
                tmr.Start();
            }
        }

		private void toolStripMenuItem10_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 1", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem11_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 1", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem12_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 1", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem13_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 2", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem14_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 2", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem15_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 2", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem16_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 3", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem17_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 3", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem18_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 3", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem19_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 4", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem20_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 4", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem21_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 4", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem22_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 5", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem23_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 5", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem24_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 5", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void btnClearFilter_Click(object sender, EventArgs e)
		{
			txtFilterGoods.SelectedValue = "0";
			tmr.Stop();
			FillOrderList();
			tmr.Start();
		}

		private void btnApplyFilter_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			FillOrderList();
			tmr.Start();
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			FillOrderList();
		}

		private void PrintToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowReport("Work designer 1", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void pDFToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowReport("Work designer 1", false, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void excelToolStripMenuItem_Click(object sender, EventArgs e)
		{
			ShowReport("Work designer 1", false, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void btnHide_Click_1(object sender, EventArgs e)
		{
			HideWindow();
		}

        private void OstatokMaterialovToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckState(db_connection))
            {
                tmr.Stop();
                this.TopMost = false;
                try
                {
                    if (prop.PathReportsTemplates != "")
                    {
                        SqlCommand db_command =
                            new SqlCommand(
                            "SELECT * FROM [vwMaterialReportDesgner] ORDER BY [material]",
                                db_connection);
                        SqlDataAdapter db_adapter = new SqlDataAdapter(db_command);
                        DataTable tbl = new DataTable("ost");
                        db_adapter.Fill(tbl);
                        rep.Load(prop.PathReportsTemplates, "Material 2");
                        rep.DataSource.Recordset = tbl;
                        PrintPreviewDialog pd = new PrintPreviewDialog();
                        pd.Document = rep.Document;
                        pd.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Не выбран файл шаблонов отчетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    ErrorNfo.WriteErrorInfo(ex);
                    MessageBox.Show("Ошибка вывода отчета\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                tmr.Start();
                this.TopMost = prop.Mod_designer_top_most;
            }
        }

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.TopMost = false;
			Photoland.Forms.Interface.frmAbout f = new Photoland.Forms.Interface.frmAbout();
			f.ShowDialog();
			this.TopMost = prop.Mod_designer_top_most;
		}

		private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar == '0') || (e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == (char)Keys.Return))
				e.Handled = false;
			else
				e.Handled = true;

			if (e.KeyChar == (char)Keys.Return)
			{
				QuickLoadOrder(true);
				txtBarCode.Text = "";
			}
		}

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ShowReport("Work 1", false, C1.Win.C1Report.FileFormatEnum.Text);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ShowReport("Work 1", true, C1.Win.C1Report.FileFormatEnum.PDF);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            ShowReport("Work 1", true, C1.Win.C1Report.FileFormatEnum.Excel);
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
			ShowReport("Work by service 1 adv", false, C1.Win.C1Report.FileFormatEnum.Text);
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
			ShowReport("Work by service 1 adv", true, C1.Win.C1Report.FileFormatEnum.PDF);
        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {
			ShowReport("Work by service 1 adv", true, C1.Win.C1Report.FileFormatEnum.Excel);
        }

        private void toolStripMenuItem37_Click(object sender, EventArgs e)
        {
			ShowReport("Work by service 1", false, C1.Win.C1Report.FileFormatEnum.Text);
        }

        private void toolStripMenuItem38_Click(object sender, EventArgs e)
        {
			ShowReport("Work by service 1", true, C1.Win.C1Report.FileFormatEnum.PDF);
        }

        private void toolStripMenuItem39_Click(object sender, EventArgs e)
        {
			ShowReport("Work by service 1", true, C1.Win.C1Report.FileFormatEnum.Excel);
        }

		private void semaphoresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.TopMost = false;
			if (usr.prmCanSetup)
			{
				frmSemaphores fOptions = new frmSemaphores();
				fOptions.ShowDialog();
				prop = new PSA.Lib.Util.Settings();
			}
			else
			{
				MessageBox.Show("Нет доступа", "Доступ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			this.TopMost = prop.Mod_designer_top_most;
		}


        private void AddEvent(string Event, int id)
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
                            order.Id + ", '" + usr.Name.Trim() + "', '" + order.Distanation + "', '" + prop.Order_prefics.Trim() +
                            "', '" + Event + body + "')", db_connection);
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }

		private void btnClearBarCode_Click(object sender, EventArgs e)
		{
			txtBarCode.Text = "";
			txtBarCode.Focus();
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