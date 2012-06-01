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
using PSA.Lib.Interface;
using PSA.Lib.Util;
using PSA.Lib.Interface;

namespace Photoland.Exchanger
{
	public partial class frmMain : Form
	{
		private SqlConnection db_connection = new SqlConnection();
		private SqlCommand db_command;
		private SqlDataAdapter db_adapter;
		private DataTable tblOrders = new DataTable("O");
		private FilterRowLike FilterR;
		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
		public UserInfo usr;

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
			this.Text = "Обмен - Photoland System Automation " + Application.ProductVersion;
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Photoland.Forms.Interface.frmAbout f = new Photoland.Forms.Interface.frmAbout();
			f.ShowDialog();
		}

		private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void mnuSetup_Click(object sender, EventArgs e)
		{
			OpenSettings();
		}

		private void OpenSettings()
		{
			frmSetup fOptions = new frmSetup();
			fOptions.ShowDialog();
			prop = new PSA.Lib.Util.Settings();
		}

		private void LoadGrid()
		{
            if (CheckState(db_connection))
            {
                GridOder.Enabled = true;
                try
                {

                    btnFilterApply.Enabled = false;
                    btnUpdate.Enabled = false;

                    tblOrders.Clear();
                    string yb, mb, db, ye, me, de;
                    yb = txtDateBegin.Value.Year.ToString();
                    if (txtDateBegin.Value.Month < 10)
                        mb = "0" + txtDateBegin.Value.Month.ToString();
                    else
                        mb = txtDateBegin.Value.Month.ToString();
                    if (txtDateBegin.Value.Day < 10)
                        db = "0" + txtDateBegin.Value.Day.ToString();
                    else
                        db = txtDateBegin.Value.Day.ToString();
                    ye = txtDateEnd.Value.Year.ToString();
                    if (txtDateEnd.Value.Month < 10)
                        me = "0" + txtDateEnd.Value.Month.ToString();
                    else
                        me = txtDateEnd.Value.Month.ToString();
                    if (txtDateEnd.Value.Day < 10)
                        de = "0" + txtDateEnd.Value.Day.ToString();
                    else
                        de = txtDateEnd.Value.Day.ToString();

                    string ybi, mbi, dbi, yei, mei, dei;
                    ybi = txtDateBeginPr.Value.Year.ToString();
                    if (txtDateBeginPr.Value.Month < 10)
                        mbi = "0" + txtDateBeginPr.Value.Month.ToString();
                    else
                        mbi = txtDateBeginPr.Value.Month.ToString();
                    if (txtDateBeginPr.Value.Day < 10)
                        dbi = "0" + txtDateBeginPr.Value.Day.ToString();
                    else
                        dbi = txtDateBeginPr.Value.Day.ToString();
                    yei = txtDateEndPr.Value.Year.ToString();
                    if (txtDateEndPr.Value.Month < 10)
                        mei = "0" + txtDateEndPr.Value.Month.ToString();
                    else
                        mei = txtDateEndPr.Value.Month.ToString();
                    if (txtDateEndPr.Value.Day < 10)
                        dei = "0" + txtDateEndPr.Value.Day.ToString();
                    else
                        dei = txtDateEndPr.Value.Day.ToString();

                    string txtFilter = "";
                    if (checkFilterOutput.Checked)
                    {
                        txtFilter += " AND (CONVERT(datetime, [expected_date], 120)>=CONVERT(DATETIME, '" + yb + "." + mb + "." + db + " 00:00:00.000" + "', 120) AND CONVERT(datetime, [expected_date], 120)<=CONVERT(DATETIME, '" + ye + "." + me + "." + de + " 23:59:59.999" + "', 120)) ";
                    }
                    if (checkFilterInput.Checked)
                    {
                        txtFilter += " AND (CONVERT(datetime, [input_date], 120)>=CONVERT(DATETIME, '" + ybi + "." + mbi + "." + dbi + " 00:00:00.000" + "', 120) AND CONVERT(datetime, [input_date], 120)<=CONVERT(DATETIME, '" + yei + "." + mei + "." + dei + " 23:59:59.999" + "', 120)) ";
                    }

                    string txtStatus = "''";
                    for (int j = 0; j < checkStatus.CheckedItems.Count; j++)
                    {
                        txtStatus += ", '" + ((System.Data.DataRowView)(checkStatus.CheckedItems[j])).Row.ItemArray[0].ToString().Trim() + "'";
                    }

                    db_command = new SqlCommand("SELECT CAST(0 AS Bit) AS Export, [id_order], [number], [name], [status], [status_desc], [input_date], [expected_date], [ordersum], [advanced_payment], [final_payment], [ordersumdiscont], [discont_percent], [comment] FROM (SELECT [order].id_order, [order].number, client.name, [order].status, order_status.status_desc, [order].input_date, [order].expected_date, vw_OrderBodyGroup.quantity * vw_OrderBodyGroup.price AS ordersum, CASE WHEN [order].advanced_payment IS NULL THEN 0 ELSE [order].advanced_payment END AS advanced_payment, CASE WHEN [order].final_payment IS NULL THEN 0 ELSE [order].final_payment END AS final_payment, CASE WHEN [order].discont_percent IS NULL THEN (vw_OrderBodyGroup.quantity * vw_OrderBodyGroup.price) WHEN [order].discont_percent > 0 THEN (vw_OrderBodyGroup.quantity * vw_OrderBodyGroup.price) - ((vw_OrderBodyGroup.quantity * vw_OrderBodyGroup.price) * ([order].discont_percent / 100)) ELSE (vw_OrderBodyGroup.quantity * vw_OrderBodyGroup.price) END AS ordersumdiscont, CASE WHEN [order].discont_percent IS NULL THEN 0 ELSE [order].discont_percent END AS discont_percent, [order].comment FROM vw_OrderBodyGroup INNER JOIN [order] ON vw_OrderBodyGroup.id_order = [order].id_order LEFT OUTER JOIN order_status ON [order].status = order_status.order_status LEFT OUTER JOIN client ON [order].id_client = client.id_client WHERE ([order].del IS NULL) OR ([order].del < 1)) as tbl WHERE [id_order] > 0 AND [status] IN (" + txtStatus + ") " + txtFilter + " ORDER BY [expected_date], [number]", db_connection);
					db_command.CommandTimeout = 9000;
                    /*
                     * 1  [id_order], 
                     * 2  [number], 
                     * 3  [name], 
                     * 4  [status], 
                     * 5  [status_desc], 
                     * 6  [input_date], 
                     * 7  [expected_date], 
                     * 8  [ordersum], 
                     * 9  [advanced_payment], 
                     * 10 [final_payment], 
                     * 11 [ordersumdiscont], 
                     * 12 [discont_percent]
                     */
                    db_adapter = new SqlDataAdapter(db_command);
                    db_adapter.Fill(tblOrders);
                    GridOder.DataSource = tblOrders;

                    GridOder.Rows.DefaultSize = 21;

                    GridOder.Cols[1].Visible = false;
                    GridOder.Cols[2].Visible = false;
                    GridOder.Cols[3].Visible = false;
                    GridOder.Cols[4].Visible = false;
                    GridOder.Cols[5].Visible = false;
                    GridOder.Cols[6].Visible = false;
                    GridOder.Cols[7].Visible = false;
                    GridOder.Cols[8].Visible = false;
                    GridOder.Cols[9].Visible = false;
                    GridOder.Cols[10].Visible = false;
                    GridOder.Cols[11].Visible = false;
                    GridOder.Cols[12].Visible = false;
                    GridOder.Cols[13].Visible = false;
                    GridOder.Cols[14].Visible = false;

                    GridOder.Cols[1].Visible = false;
                    GridOder.Cols[1].Width = 20;
                    GridOder.Cols[1].AllowDragging = false;
                    GridOder.Cols[1].AllowEditing = true;
                    GridOder.Cols[1].AllowMerging = false;
                    GridOder.Cols[1].AllowResizing = true;
                    GridOder.Cols[1].AllowSorting = true;
                    GridOder.Cols[1].Caption = "!";

                    GridOder.Cols[3].Visible = true;
                    GridOder.Cols[3].Width = 110;
                    GridOder.Cols[3].AllowDragging = false;
                    GridOder.Cols[3].AllowEditing = false;
                    GridOder.Cols[3].AllowMerging = false;
                    GridOder.Cols[3].AllowResizing = true;
                    GridOder.Cols[3].AllowSorting = true;
                    GridOder.Cols[3].Caption = "Заказ";

                    GridOder.Cols[4].Visible = true;
                    GridOder.Cols[4].Width = 430;
                    GridOder.Cols[4].AllowDragging = false;
                    GridOder.Cols[4].AllowEditing = false;
                    GridOder.Cols[4].AllowMerging = false;
                    GridOder.Cols[4].AllowResizing = true;
                    GridOder.Cols[4].AllowSorting = true;
                    GridOder.Cols[4].Caption = "Имя";

                    GridOder.Cols[6].Visible = true;
                    GridOder.Cols[6].Width = 160;
                    GridOder.Cols[6].AllowDragging = false;
                    GridOder.Cols[6].AllowEditing = false;
                    GridOder.Cols[6].AllowMerging = false;
                    GridOder.Cols[6].AllowResizing = true;
                    GridOder.Cols[6].AllowSorting = true;
                    GridOder.Cols[6].Caption = "Статус";

                    GridOder.Cols[7].Visible = true;
                    GridOder.Cols[7].Width = 110;
                    GridOder.Cols[7].AllowDragging = false;
                    GridOder.Cols[7].AllowEditing = false;
                    GridOder.Cols[7].AllowMerging = false;
                    GridOder.Cols[7].AllowResizing = true;
                    GridOder.Cols[7].AllowSorting = true;
                    GridOder.Cols[7].Caption = "Поступление";
                    GridOder.Cols[7].Format = "g";

                    GridOder.Cols[8].Visible = true;
                    GridOder.Cols[8].Width = 110;
                    GridOder.Cols[8].AllowDragging = false;
                    GridOder.Cols[8].AllowEditing = false;
                    GridOder.Cols[8].AllowMerging = false;
                    GridOder.Cols[8].AllowResizing = true;
                    GridOder.Cols[8].AllowSorting = true;
                    GridOder.Cols[8].Caption = "Выдача";
                    GridOder.Cols[8].Format = "g";

                    // Цветовая раскраска
                    for (int i = 2; i < GridOder.Rows.Count; i++)
                    {
                        if (GridOder.Rows[i][14].ToString().IndexOf("!Экспорт") == 0)
                        {
                            GridOder.Rows[i].Style = GridOder.Styles["MyGreen"];
                        }
                    }


                }
                catch (Exception ex)
                {
                    ErrorNfo.WriteErrorInfo(ex);
                }
                finally
                {
                    btnFilterApply.Enabled = true;
                    btnUpdate.Enabled = true;
                }
            }
		}

		private void checkFilter()
		{
			if (checkFilterInput.Checked)
			{
				txtDateBeginPr.Enabled = true;
				txtDateEndPr.Enabled = true;
			}
			else
			{
				txtDateBeginPr.Enabled = false;
				txtDateEndPr.Enabled = false;
			}

			if (checkFilterOutput.Checked)
			{
				txtDateBegin.Enabled = true;
				txtDateEnd.Enabled = true;
			}
			else
			{
				txtDateBegin.Enabled = false;
				txtDateEnd.Enabled = false;
			}

		}

		private void frmMain_Load(object sender, EventArgs e)
		{
            try
            {
                StreamWriter sw = new StreamWriter(prop.Dir_export + "\\Exchanger_" + Environment.MachineName + "_" + Environment.UserName + ".info", false, Encoding.GetEncoding(1251));
                sw.Write("Date:          " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                         "\nMashine:       " + Environment.MachineName +
                         "\nUser:          " + Environment.UserName +
                         "\nExchanger mod: " + Application.ProductVersion);
                sw.Close();
            }
            catch
            {
            }

			if (!Checking.checkVersion(Modules.Exchanger, Application.ProductVersion))
				if (MessageBox.Show("Внимание! Существует более новая версия модуля!\nУстановить обновление?", "Контроль обновлений", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
					System.Diagnostics.Process.Start(System.Environment.GetCommandLineArgs()[0].Substring(
													0,
													System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
													) + "\\PSA.Update.cmd");


			if (!PSA.Lib.Util.Semaphore.semInventory)
			{
				this.WindowState = FormWindowState.Maximized;

				txtDateBegin.Value = DateTime.Now.AddDays(-20);
				txtDateEnd.Value = DateTime.Now.AddDays(5);
				txtDateBeginPr.Value = DateTime.Now.AddDays(-20);
				txtDateEndPr.Value = DateTime.Now.AddDays(5);

				bool tmp_login_ok = false;
				bool tmp_exit = false;

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
						SqlCommand cmd_status = new SqlCommand("SELECT [order_status], [status_desc] FROM [order_status] ORDER BY [status_desc]", db_connection);
						SqlDataAdapter da_status = new SqlDataAdapter(cmd_status);
						DataTable dt_status = new DataTable("status");
						da_status.Fill(dt_status);
						checkStatus.DataSource = dt_status;
						checkStatus.DisplayMember = "status_desc";
						checkStatus.ValueMember = "order_status";
						for (int i = 0; i < checkStatus.Items.Count; i++)
						{
							checkStatus.SetSelected(i, true);
							//checkStatus.SelectedItem =  i;
							if (checkStatus.SelectedValue.ToString().Trim() == "000000")
								checkStatus.SetItemChecked(i, true);
						}

						checkFilterInput.Checked = true;

						checkFilter();

						FilterR = new FilterRowLike(GridOder);
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
			else
			{

				MessageBox.Show("В момент проведения инвентаризации вход в модуль запрещен!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				Application.Exit();
			}
		}

		private void checkFilterOutput_CheckedChanged(object sender, EventArgs e)
		{
			checkFilter();
		}

		private void checkFilterInput_CheckedChanged(object sender, EventArgs e)
		{
			checkFilter();
		}

		private void btnFilterApply_Click(object sender, EventArgs e)
		{
			LoadGrid();
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			LoadGrid();
		}

		private void GridOder_Click(object sender, EventArgs e)
		{
            if (GridOder.GetData(GridOder.Row, 1) != null)
            {
                if ((bool)GridOder.GetData(GridOder.Row, 1))
                {
                    GridOder.Rows[GridOder.Row].Style = GridOder.Styles["MyRed"];
                    if (GridOder.Rows[GridOder.Row][14].ToString().IndexOf("!Экспорт") == 0)
                    {
                        GridOder.Rows[GridOder.Row].Style = GridOder.Styles["MyBlue"];
                    }
                }
                else
                {
                    GridOder.Rows[GridOder.Row].Style = GridOder.Styles["Normal"];
                    if (GridOder.Rows[GridOder.Row][14].ToString().IndexOf("!Экспорт") == 0)
                    {
                        GridOder.Rows[GridOder.Row].Style = GridOder.Styles["MyGreen"];
                    }
                }
            }
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			for (int i = 2; i < GridOder.Rows.Count; i++)
			{
				GridOder.Rows[i][1] = 1;
				GridOder.Rows[i].Style = GridOder.Styles["MyRed"];
				if (GridOder.Rows[i][14].ToString().IndexOf("!Экспорт") == 0)
				{
					GridOder.Rows[i].Style = GridOder.Styles["MyBlue"];
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (GridOder.GetData(GridOder.Row, 3) != null)
			{
				OpenOrder(GridOder.GetData(GridOder.Row, 3).ToString());
			}
		}

		private void OpenOrder(string orderno)
		{
            if (CheckState(db_connection))
            {
                SqlCommand tmp_cmd = new SqlCommand("SELECT [id_order] FROM [vwOrderNoList] WHERE RTRIM([number]) = RTRIM('" + orderno + "')", db_connection);
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
                    frmOrderClose fOrder = new frmOrderClose(db_connection, usr, tmp_id);
                    fOrder.ShowDialog();
                }
            }
		}

        private void btnExport_Click(object sender, EventArgs e)
        {
            DoExport(1);
        }


		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private string DateToSql(string str)
		{
            string o = "";

            try
            {
                str = str.Replace(" AM", "").Replace(" PM", "");
                DateTime tmp = DateTime.Parse(str);

                o = tmp.Year.ToString();
                o += "/";
                o += tmp.Month < 10 ? "0" + tmp.Month.ToString() : tmp.Month.ToString();
                o += "/";
                o += tmp.Day < 10 ? "0" + tmp.Day.ToString() : tmp.Day.ToString();
                o += " ";
                o += tmp.ToShortTimeString();


            }
            catch
            {
                DateTime tmp = DateTime.Now;

                o = tmp.Year.ToString();
                o += "/";
                o += tmp.Month < 10 ? "0" + tmp.Month.ToString() : tmp.Month.ToString();
                o += "/";
                o += tmp.Day < 10 ? "0" + tmp.Day.ToString() : tmp.Day.ToString();
                o += " ";
                o += tmp.ToShortTimeString();


            }
            finally
            {
                
            }
            return o;
		}

		private void semaphoresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmSemaphores fOptions = new frmSemaphores();
			fOptions.ShowDialog();
			prop = new PSA.Lib.Util.Settings();
		}

        private void btnExport2_Click(object sender, EventArgs e)
        {
            DoExport(2);
        }

		private void btnImportTerminal_Click(object sender, EventArgs e)
		{
			DoImportTerminal();
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

		private void btnFilterAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (int i = 0; i < checkStatus.Items.Count; i++)
			{
				checkStatus.SetItemChecked(i, true);
			}
		}

		private void btnFilterClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (int i = 0; i < checkStatus.Items.Count; i++)
			{
				checkStatus.SetItemChecked(i, false);
			}
		}

		private void btnImportMFoto_Click(object sender, EventArgs e)
		{
			DoImportMFoto();
		}

        private void button2_Click(object sender, EventArgs e)
        {
            frmSelectExportPlace f = new frmSelectExportPlace();
            f.ShowDialog();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            PSA.Lib.Util.ExportOrder.autoExport(1, "150000045308");
        }


	}
}