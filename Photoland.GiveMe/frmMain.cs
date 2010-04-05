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

namespace Photoland.GiveMe
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
			this.Text = "Дай мне... - Photoland System Automation " + Application.ProductVersion;
		}

		private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
		{

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
					frmOptions fOptions = new frmOptions();
					fOptions.ShowDialog();
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

		private void propToolStripMenuItem_Click(object sender, EventArgs e)
		{
			frmOptions fOptions = new frmOptions();
			fOptions.ShowDialog();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();
		}

		private void btn1_Click(object sender, EventArgs e)
		{
			try
			{
				SqlCommand cmd = new SqlCommand("SELECT * FROM [dbo].[user]", db_connection);
				DataTable tbl = new DataTable("tbl");
				SqlDataAdapter da = new SqlDataAdapter(cmd);
				da.Fill(tbl);
				txtData.Text = "Справочник пользователей\n";
				for(int i = 0; i < tbl.Columns.Count; i++)
				{
					txtData.Text += tbl.Columns[i].Caption.Trim() + ";";
				}
				txtData.Text += "\n";
				for(int i = 0; i < tbl.Rows.Count; i++)
				{
					for(int j = 0; j < tbl.Columns.Count; j++)
					{
						txtData.Text += tbl.Rows[i][j].ToString().Trim() + ";";
					}
					txtData.Text += "\n";
				}
			}
			catch(Exception ex)
			{
			}
		}
		
		private void addTableUO()
		{
			string data = "" +
			"c00103;Цифровая 20х30;4.00\n" +
			"c00104;Аналог 10х15;1.00\n" +
			"c00105;Аналог 20х30;4.00\n" +
			"c00125;Цифровая 20х27;3.60\n" +
			"c00126;Цифровая 21х30;4.20\n" +
			"c00127;Цифровая 30х42;8.40\n" +
			"c00137;Аналог 9х13;0.78\n" +
			"c00138;Аналог 13х18;1.56\n" +
			"c00139;Аналог 15х20;2.00\n" +
			"c00149;Аналог 30х76;15.20\n" +
			"c00150;Аналог 30х91;18.20\n" +
			"c00151;Индекс-принт 10х15;1.00\n" +
			"c00166;Оцифровка пленки 36 кадров;36.00\n" +
			"c00191;Цифровая проявка 10*15;1.00\n" +
			"c00192;Цифровая проявка 20*30;4.00\n" +
			"c00223;Световая панель двусторонняя 1200*800;0.00\n" +
			"c00224;Световая панель двусторонняя 1200*1700;0.00\n" +
			"c00225;Световая панель двусторонняя 1000*700;0.00\n" +
			"c00263;Цифровая 20х30 металлик;4.00\n" +
			"c00264;Цифровая 21х30 металлик;4.20\n" +
			"c00265;Цифровая 30х40 металлик;8.00\n" +
			"c00281;Цифровая на DURST Lambda Clear;0.00\n" +
			"c00286;Сувенирная Печать 30*40 см;8.00\n" +
			"c00287;Сувенирная Кружка 1-сторонняя;0.00\n" +
			"c00299;Сувенирная Нанесение A3 на ткань;0.00\n" +
			"c00300;Сувенирная Нанесение A3 на цветную ткань;0.00\n" +
			"c00301;Сувенирная Нанесение A4 на цветную ткань;0.00\n" +
			"c00308;Сувенирная Календарь \"квартальный\" (на год);0.00\n" +
			"c00310;Аналог 10х15 все хорошие;1.00\n" +
			"c00311;Аналог 10х15 все подряд;1.00\n" +
			"c00326;Аналог 9х13 все хорошие;0.78\n" +
			"c00327;Аналог 15х20 все подряд;2.00\n" +
			"c00328;Аналог 15х20 все хорошие;2.00\n" +
			"c00345;Обложка DVD печать;0.00\n" +
			"c00346;Обложка VK печать;0.00\n" +
			"c00350;Цифровая 13х30;2.60\n" +
			"c00367;Аналог 10х15 по куп. 25 шт.;1.00\n" +
			"c00368;Сувенирная Кружка цвет. 2-сторонняя;0.00\n" +
			"c00369;Сувенирная Кружка цвет. 1-сторонняя;0.00\n" +
			"c00382;Сувенирная Портрет на памятник цветной СРОЧНО;0.00\n" +
			"c00383;Сувенирная Портрет на памятник ч/б СРОЧНО;0.00\n" +
			"c00389;Сувенирная Подушка;0.00\n" +
			"c00106;Цифровая 30х40;8.00\n" +
			"c00117;Цифровая 9,5х13;0.83\n" +
			"c00118;Цифровая 18х25;3.00\n" +
			"c00128;Цифровая 30х45;9.00\n" +
			"c00129;Цифровая 10х30;2.00\n" +
			"c00130;Цифровая 15х45;4.50\n" +
			"c00140;Аналог 14х20;1.86\n" +
			"c00141;Аналог 18х25;3.00\n" +
			"c00142;Аналог 20х36;4.80\n" +
			"c00152;Индекс-принт 15х20;2.00\n" +
			"c00153;Индекс-принт 20х30;4.00\n" +
			"c00154;Печать с белой рамкой;0.00\n" +
			"c00214;Световая панель односторонняя 400*600;0.00\n" +
			"c00215;Световая панель односторонняя 500*700;0.00\n" +
			"c00216;Световая панель односторонняя 600*900;0.00\n" +
			"c00226;Сканирование высокого разрешения 24*35 мм;0.00\n" +
			"c00227;Сканирование высокого разрешения 24*65 мм;0.00\n" +
			"c00228;Сканирование высокого разрешения 60*45 мм;0.00\n" +
			"c00266;Цифровая 30х42 металлик;8.40\n" +
			"c00267;Цифровая 30х45 металлик;9.00\n" +
			"c00268;Цифровая 10х30 металлик;2.00\n" +
			"c00288;Сувенирная Кружка 2-сторонняя;0.00\n" +
			"c00291;Сувенирная Магнит с изображением;0.00\n" +
			"c00292;Сувенирная Бейсболка цветная с нанесением изображения;0.00\n" +
			"c00119;Цифровая 25х34;5.66\n" +
			"c00120;Цифровая 25х38;6.33\n" +
			"c00121;Цифровая 9х13;0.78\n" +
			"c00131;Цифровая 30х61;12.20\n" +
			"c00132;Цифровая 30х76;15.20\n" +
			"c00133;Цифровая 30х91;18.20\n" +
			"c00143;Аналог 25х38;6.33\n" +
			"c00144;Аналог 30х42;8.40\n" +
			"c00145;Аналог 30х45;9.00\n" +
			"c00159;Оцифровка на ЦМФЛ 600*800 пикс.;1.00\n" +
			"c00160;Оцифровка на ЦМФЛ 1200*1600 пикс.;1.00\n" +
			"c00161;Оцифровка на ЦМФЛ 1400*2500 пикс.;1.00\n" +
			"c00217;Световая панель односторонняя 1000*700;0.00\n" +
			"c00218;Световая панель односторонняя 1200*800;0.00\n" +
			"c00219;Световая панель односторонняя 1200*1700;0.00\n" +
			"c00229;Сканирование высокого разрешения 60*60 мм;0.00\n" +
			"c00230;Сканирование высокого разрешения 60*70 мм;0.00\n" +
			"c00231;Сканирование высокого разрешения 60*80 мм;0.00\n" +
			"c00269;Цифровая 30х61 металлик;12.20\n" +
			"c00270;Цифровая 30х76 металлик;15.20\n" +
			"c00271;Цифровая 30х91 металлик;18.20\n" +
			"c00293;Сувенирная Бейсболка с нанесением изображения;0.00\n" +
			"c00294;Сувенирная Футболка белая х/б;0.00\n" +
			"c00295;Сувенирная Футболка белая х/б женская;0.00\n" +
			"c00302;Сувенирная Нанесение A5 на цветную ткань;0.00\n" +
			"c00303;Z удалено Сувенирная Puzzle 10*15 см;0.00\n" +
			"c00304;Сувенирная Puzzle 15*20 см;0.00\n" +
			"c00320;Сувенирная печать на самоклейке 30*40 см;0.00\n" +
			"c00321;Сувенирная Портрет на памятник цветной;0.00\n" +
			"c00322;Художественное фото 10*15 печать;1.00\n" +
			"c00329;Цифровая 10х15 по купону на 500 р.;1.00\n" +
			"c00331;Интернет 10х15;1.00\n" +
			"c00332;Интернет 15х20;2.00\n" +
			"c00351;Цифровая 15х21;2.10\n" +
			"c00352;Цифровая 15х22;2.20\n" +
			"c00353;Цифровая 13х38;3.30\n" +
			"c00370;Кристалл \"Прямоугольник\" 10х15 см;0.00\n" +
			"c00371;Кристалл \"Айсберг\" 11х11 см;0.00\n" +
			"c00372;Кристалл \"Яблоко\" 7х8 см;0.00\n" +
			"c00390;Цифровая 25х40;6.66\n" +
			"c00232;Сканирование высокого разрешения 90*120 мм;0.00\n" +
			"c00233;Сканирование высокого разрешения А4;0.00\n" +
			"c00262;Цифровая 15х20 металлик;2.00\n" +
			"c00278;Цифровая на DURST Lambda (матовая/глянцевая);0.00\n" +
			"c00279;Цифровая на DURST Lambda Metallic;0.00\n" +
			"c00280;Цифровая на DURST Lambda Transpasency;0.00\n" +
			"c00296;Сувенирная Футболка черная х/б;0.00\n" +
			"c00297;Сувенирная Нанесение A5 на ткань;0.00\n" +
			"c00298;Сувенирная Нанесение A4 на ткань;0.00\n" +
			"c00305;Сувенирная Puzzle 20*30 см;0.00\n" +
			"c00306;Z удалено Сувенирная Этикетка;0.00\n" +
			"c00307;Сувенирная Портрет на памятник ч/б;0.00\n" +
			"c00323;Художественное фото 15*20 печать;2.00\n" +
			"c00324;Художественное фото 20*30 печать;4.00\n" +
			"c00325;Аналог 9х13 все подряд;0.78\n" +
			"c00333;Интернет 20х30;4.00\n" +
			"c00343;Обложка CD печать;1.00\n" +
			"c00344;Обложка CD Slim печать;1.00\n" +
			"c00122;Цифровая 10х14;0.93\n" +
			"c00123;Цифровая 13х17;1.47\n" +
			"c00124;Цифровая 14х20;1.86\n" +
			"c00134;Кадрирование простое;0.00\n" +
			"c00135;Кадрирование с наклоном;0.00\n" +
			"c00136;Устранение эффекта красных глаз оператором;0.00\n" +
			"c00354;Цифровая 15х38;3.80\n" +
			"c00355;Цифровая 15х30;3.00\n" +
			"c00358;Художественное фото 30*40 печать;8.00\n" +
			"c00146;Аналог 10х30;2.00\n" +
			"c00147;Аналог 15х45;4.50\n" +
			"c00148;Аналог 30х61;12.20\n" +
			"c00162;Оцифровка на ЦМФЛ 2400*3600 пикс.;1.00\n" +
			"c00164;Оцифровка пленки 12 кадров;12.00\n" +
			"c00165;Оцифровка пленки 24 кадров;24.00\n" +
			"c00373;Кристалл \"Подсолнух на постаменте\" 7,5 см;0.00\n" +
			"c00374;Кристалл \"Брелок\" в ассортименте;0.00\n" +
			"c00375;Цифровая 20х25;3.33\n" +
			"c00100;Цифровая 10х15;1.00\n" +
			"c00101;Цифровая 13х18;1.56\n" +
			"c00102;Цифровая 15х20;2.00\n" +
			"c00220;Световая панель двусторонняя 600*900;0.00\n" +
			"c00221;Световая панель двусторонняя 500*700;0.00\n" +
			"c00222;Световая панель двусторонняя 400*600;0.00";
			
			string createtable = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[good_]') AND type in (N'U'))\n DROP TABLE [dbo].[good_];\nCREATE TABLE [dbo].[good_]([id] [nchar](30) COLLATE Cyrillic_General_CI_AS NULL, [name_good] [nchar](1024) COLLATE Cyrillic_General_CI_AS NULL, [s] [decimal](18, 2) NULL) ON [PRIMARY];";
			
			SqlCommand cmd = new SqlCommand(createtable, db_connection);
			cmd.ExecuteNonQuery();
			foreach(string s in data.Split('\n'))
			{
				cmd = new SqlCommand("INSERT INTO [dbo].[good_]([id],[name_good],[s]) VALUES ('" + s.Split(';')[0] + "','" + s.Split(';')[1] + "'," + s.Split(';')[2] + ")", db_connection);
				cmd.ExecuteNonQuery();
			}
		}

		private void ShowReport(string rep_name, bool export, C1.Win.C1Report.FileFormatEnum format)
		{
			if (CheckState(db_connection))
			{
				try
				{
					if (File.Exists(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\")) + "\\A.xml"))
					{
						{
							bool ok = false;
							DataTable r = new DataTable("Report");
							SqlCommand c;
							SqlDataAdapter a;
							switch (rep_name)
							{
								case "Rep1":
									{
										frmSelectMonth f = new frmSelectMonth();
										f.ShowDialog();
										if ((f.DialogResult == System.Windows.Forms.DialogResult.OK) && (f.txtMonth.Text != "") && (f.txtYear.Text != ""))
										{
											DateTime d1 = DateTime.Parse("01/" + f.txtMonth.Text + "/" + f.txtYear.Text);
											DateTime d2 = d1.AddMonths(1).AddDays(-1);
											string filter = "";
											string filter2 = "";
											filter =
												" (dbo.orderbody.datework >= CONVERT(DATETIME, '" +
												d1.Year.ToString("D4") + "-" +
												d1.Month.ToString("D2") + "-" +
												d1.Day.ToString("D2") +
												" 00:00:00', 102) AND dbo.orderbody.datework <= CONVERT(DATETIME, '" +
												d2.Year.ToString("D4") + "-" +
												d2.Month.ToString("D2") + "-" +
												d2.Day.ToString("D2") + " 23:59:59', 102))";
											filter2 =
												" (orderbody_1.datework >= CONVERT(DATETIME, '" +
												d1.Year.ToString("D4") + "-" +
												d1.Month.ToString("D2") + "-" +
												d1.Day.ToString("D2") +
												" 00:00:00', 102) AND orderbody_1.datework <= CONVERT(DATETIME, '" +
												d2.Year.ToString("D4") + "-" +
												d2.Month.ToString("D2") + "-" +
												d2.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT name_work, mashine, SUM(cnt) AS cnt, SUM(cntb) AS cntb, SUM(cntb) * 100 / SUM(cnt) AS prs FROM (SELECT dbo.orderbody.name_work, dbo.mashine.mashine, 0 AS cnt, SUM(dbo.orderbody.defect_quantity * dbo.good_.s) AS cntb FROM dbo.orderbody INNER JOIN dbo.defect ON dbo.orderbody.tech_defect = dbo.defect.defect_code INNER JOIN dbo.mashine ON dbo.orderbody.id_mashine = dbo.mashine.id_mashine INNER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good INNER JOIN dbo.good_ ON dbo.good.id_good = dbo.good_.id WHERE (dbo.orderbody.tech_defect = 1 OR dbo.orderbody.tech_defect = 2) AND " + filter + " AND (dbo.orderbody.defect_quantity > 0) GROUP BY dbo.orderbody.name_work, dbo.mashine.mashine UNION SELECT TOP (100) PERCENT orderbody_1.name_work, mashine_1.mashine, SUM(orderbody_1.actual_quantity * good__1.s) AS cnt, 0 AS cntb FROM dbo.[order] INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.[order].id_order = orderbody_1.id_order INNER JOIN dbo.mashine AS mashine_1 ON orderbody_1.id_mashine = mashine_1.id_mashine INNER JOIN dbo.good AS good_1 ON orderbody_1.id_good = good_1.id_good INNER JOIN dbo.good_ AS good__1 ON good_1.id_good = good__1.id WHERE (good_1.type = N'1') AND " + filter2 + " GROUP BY orderbody_1.name_work, mashine_1.mashine HAVING (SUM(orderbody_1.actual_quantity * good__1.s) <> 0)) AS t GROUP BY name_work, mashine";
											c = new SqlCommand(query, db_connection);
											a = new SqlDataAdapter(c);
											a.Fill(r);
											rep.Load(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\")) + "\\A.xml", rep_name);
											rep.DataSource.Recordset = r;
											ok = true;
										}
										break;
									}
								case "Rep2":
									{
										frmSelectMonth f = new frmSelectMonth();
										f.ShowDialog();
										if ((f.DialogResult == System.Windows.Forms.DialogResult.OK) && (f.txtMonth.Text != "") && (f.txtYear.Text != ""))
										{
											DateTime d1 = DateTime.Parse("01/" + f.txtMonth.Text + "/" + f.txtYear.Text);
											DateTime d2 = d1.AddMonths(1).AddDays(-1);
											string filter = "";
											filter =
												" (dbo.orderbody.datework >= CONVERT(DATETIME, '" +
												d1.Year.ToString("D4") + "-" +
												d1.Month.ToString("D2") + "-" +
												d1.Day.ToString("D2") +
												" 00:00:00', 102) AND dbo.orderbody.datework <= CONVERT(DATETIME, '" +
												d2.Year.ToString("D4") + "-" +
												d2.Month.ToString("D2") + "-" +
												d2.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT TOP (100) PERCENT name_work, SUM(sm) AS Expr1, SUM(pr) AS Expr2 FROM (SELECT     TOP (100) PERCENT SUM(dbo.orderbody.actual_quantity * dbo.orderbody.price) AS sm, dbo.orderbody.name_work,  SUM(dbo.orderbody.actual_quantity * dbo.orderbody.price - (dbo.orderbody.actual_quantity * dbo.orderbody.price) * (dbo.[order].discont_percent / 100)) AS pr FROM dbo.orderbody INNER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good INNER JOIN dbo.[order] ON dbo.orderbody.id_order = dbo.[order].id_order WHERE (dbo.good.type = N'2') AND (dbo.orderbody.actual_quantity <> 0) AND " + filter + " GROUP BY dbo.orderbody.name_work, dbo.[order].discont_percent, (dbo.orderbody.actual_quantity * dbo.orderbody.price)  * (dbo.[order].discont_percent / 100) ORDER BY dbo.orderbody.name_work) AS derivedtbl_1 GROUP BY name_work ORDER BY name_work";
											c = new SqlCommand(query, db_connection);
											a = new SqlDataAdapter(c);
											a.Fill(r);
											rep.Load(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\")) + "\\A.xml", rep_name);
											rep.DataSource.Recordset = r;
											ok = true;
										}
										break;
									}
								case "Rep3":
									{
										frmSelectMonth f = new frmSelectMonth();
										f.ShowDialog();
										if ((f.DialogResult == System.Windows.Forms.DialogResult.OK) && (f.txtMonth.Text != "") && (f.txtYear.Text != ""))
										{
											DateTime d1 = DateTime.Parse("01/" + f.txtMonth.Text + "/" + f.txtYear.Text);
											DateTime d2 = d1.AddMonths(1).AddDays(-1);
											string filter = "";
											string filter1 = "";
											filter =
												" (dbo.orderbody.datework >= CONVERT(DATETIME, '" +
												d1.Year.ToString("D4") + "-" +
												d1.Month.ToString("D2") + "-" +
												d1.Day.ToString("D2") +
												" 00:00:00', 102) AND dbo.orderbody.datework <= CONVERT(DATETIME, '" +
												d2.Year.ToString("D4") + "-" +
												d2.Month.ToString("D2") + "-" +
												d2.Day.ToString("D2") + " 23:59:59', 102))";
											filter1 =
												" (orderbody_1.datework >= CONVERT(DATETIME, '" +
												d1.Year.ToString("D4") + "-" +
												d1.Month.ToString("D2") + "-" +
												d1.Day.ToString("D2") +
												" 00:00:00', 102) AND orderbody_1.datework <= CONVERT(DATETIME, '" +
												d2.Year.ToString("D4") + "-" +
												d2.Month.ToString("D2") + "-" +
												d2.Day.ToString("D2") + " 23:59:59', 102))";
											string query =
												"SELECT TOP (100) PERCENT name_work, SUM(sm) AS sm, SUM(pr) AS pr, name, SUM(cnt) AS cnt, date AS d FROM         (SELECT     name_work, sm, pr, name, cnt, date FROM          (SELECT     TOP (100) PERCENT dbo.orderbody.name_work, SUM(dbo.orderbody.actual_quantity * dbo.orderbody.price) AS sm, SUM(dbo.orderbody.actual_quantity * dbo.orderbody.price - (dbo.orderbody.actual_quantity * dbo.orderbody.price)  * (dbo.[order].discont_percent / 100)) AS pr, dbo.good.name, COUNT(*) AS cnt, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS date FROM dbo.orderbody INNER JOIN dbo.[order] ON dbo.orderbody.id_order = dbo.[order].id_order INNER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE      " + filter + " AND (dbo.[order].name_designer <> N'') AND (dbo.good.type = N'2') GROUP BY dbo.orderbody.name_work, dbo.good.name, dbo.[order].name_designer, DATEADD(dd, 0, DATEDIFF(dd, 0,  dbo.orderbody.datework)) ORDER BY dbo.[order].name_designer) AS t1 UNION ALL SELECT     name_work, sm, pr, name, cnt, date FROM         (SELECT     TOP (100) PERCENT dbo.[user].name AS name_work, SUM(orderbody_1.actual_quantity * orderbody_1.price) AS sm, SUM(orderbody_1.actual_quantity * orderbody_1.price - (orderbody_1.actual_quantity * orderbody_1.price)  * (order_1.discont_percent / 100)) AS pr, good_1.name, COUNT(*) AS cnt, DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) AS date FROM          dbo.orderbody AS orderbody_1 INNER JOIN dbo.[order] AS order_1 ON orderbody_1.id_order = order_1.id_order INNER JOIN dbo.good AS good_1 ON orderbody_1.id_good = good_1.id_good INNER JOIN dbo.[user] ON orderbody_1.id_user_work = dbo.[user].id_user WHERE      " +  filter1+ " AND (order_1.name_designer = N'') AND  (good_1.type = N'2') GROUP BY good_1.type, good_1.name, order_1.name_designer, dbo.[user].name, orderbody_1.datework ORDER BY order_1.name_designer, good_1.type) AS t2) AS tt1 GROUP BY name_work, name, date ORDER BY name_work, d, name";
											c = new SqlCommand(query, db_connection);
											a = new SqlDataAdapter(c);
											a.Fill(r);
											rep.Load(Application.ExecutablePath.Substring(0, Application.ExecutablePath.LastIndexOf("\\")) + "\\A.xml", rep_name);
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
						MessageBox.Show("Не найден файл шаблонов дополнительных отчетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
				catch (Exception ex)
				{
					ErrorNfo.WriteErrorInfo(ex);
				}
			}
		}

		private void btnRep1_Click(object sender, EventArgs e)
		{
			addTableUO();
			ShowReport("Rep1", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void btnRep1PDF_Click(object sender, EventArgs e)
		{
			addTableUO();
			ShowReport("Rep1", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void btnRep2_Click(object sender, EventArgs e)
		{
			addTableUO();
			ShowReport("Rep2", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void btnRep2PDF_Click(object sender, EventArgs e)
		{
			addTableUO();
			ShowReport("Rep2", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void btnRep3_Click(object sender, EventArgs e)
		{
			addTableUO();
			ShowReport("Rep3", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void btnRep3PDF_Click(object sender, EventArgs e)
		{
			addTableUO();
			ShowReport("Rep3", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}


	}
}
