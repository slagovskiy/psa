using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.Util;
using System.IO;

namespace PSA.Lib.Interface
{
	public partial class frmRobotManager : PSA.Lib.Interface.Templates.frmTDialog
	{
		Settings prop = new Settings();

		public frmRobotManager()
		{
			InitializeComponent();
			this.Title = "Управление роботом";
		}

		private void frmRobotManager_Load(object sender, EventArgs e)
		{
			LoadData();
			tmr.Start();
		}

		private void LoadData()
		{
			btnUpdate.Enabled = false;
			tmr.Stop();
			try
			{
				DataTable tbl = new DataTable();
				tbl.Columns.Add("a");
				tbl.Columns.Add("d");

				ini iniRobot = new ini(prop.Dir_export + "\\robot.ini");
				DataRow r;

				r = tbl.NewRow();
				r["a"] = "Импорт спр. машин";
				r["d"] = iniRobot.IniReadValue("import", "mashine", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Импорт материалов";
				r["d"] = iniRobot.IniReadValue("import", "material", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Импорт услуг";
				r["d"] = iniRobot.IniReadValue("import", "good", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Импорт прайса";
				r["d"] = iniRobot.IniReadValue("import", "price", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Импорт категорий клиентов";
				r["d"] = iniRobot.IniReadValue("import", "category", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Импорт дисконтных карт";
				r["d"] = iniRobot.IniReadValue("import", "dcard", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Импорт типов оплаты";
				r["d"] = iniRobot.IniReadValue("import", "ptype", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Экспорт заказов";
				r["d"] = iniRobot.IniReadValue("export", "order", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Экспорт платежей";
				r["d"] = iniRobot.IniReadValue("export", "payment", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Экспорт инвентаризации";
				r["d"] = iniRobot.IniReadValue("export", "inventory", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Экспорт сверки";
				r["d"] = iniRobot.IniReadValue("export", "verification", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Экспорт по терминалам";
				r["d"] = iniRobot.IniReadValue("export", "taction", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Экспорт списаний";
				r["d"] = iniRobot.IniReadValue("export", "discard", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Экспорт счетчиков операторов";
				r["d"] = iniRobot.IniReadValue("export", "counter1", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Экспорт данных в офис";
				r["d"] = iniRobot.IniReadValue("export", "csv", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Экспорт ошибок в офис";
				r["d"] = iniRobot.IniReadValue("export", "er", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Экспорт логов чистки базы в офис";
				r["d"] = iniRobot.IniReadValue("export", "clear", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Обновление кеша терминальных заказов";
				r["d"] = iniRobot.IniReadValue("cache", "korder", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Обновление кеша интернет заказов";
				r["d"] = iniRobot.IniReadValue("cache", "irorder", "нет данных");
				tbl.Rows.Add(r);

				r = tbl.NewRow();
				r["a"] = "Обновление кеша оптовых заказов";
				r["d"] = iniRobot.IniReadValue("cache", "ioorder", "нет данных");
				tbl.Rows.Add(r);

				data.DataSource = tbl;
				data.Cols[1].Caption = "Действие";
				data.Cols[2].Caption = "Время";
				data.Cols[1].AllowEditing = false;
				data.Cols[2].AllowEditing = false;
				data.Cols[1].AllowSorting = true;
				data.Cols[2].AllowSorting = true;
				data.Cols[1].Width = 245;
				data.Cols[2].Width = 150;

				if (File.Exists(prop.Dir_export + "\\robot.export"))
				{
					btnExport.Enabled = false;
				}
				else
				{
					btnExport.Enabled = true;
				}

				if (File.Exists(prop.Dir_export + "\\robot.cache"))
				{
					button1.Enabled = false;
				}
				else
				{
					button1.Enabled = true;
				}

				if (File.Exists(prop.Dir_export + "\\robot.import"))
				{
					btnImport.Enabled = false;
				}
				else
				{
					btnImport.Enabled = true;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
			btnUpdate.Enabled = true;
			tmr.Start();

		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnImport_Click(object sender, EventArgs e)
		{
			try
			{
				StreamWriter f = new StreamWriter(prop.Dir_export + "\\robot.import");
				f.WriteLine("");
				f.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
			LoadData();
		}

		private void btnExport_Click(object sender, EventArgs e)
		{
			try
			{
				StreamWriter f = new StreamWriter(prop.Dir_export + "\\robot.export");
				f.WriteLine("");
				f.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
			LoadData();
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void tmr_Tick(object sender, EventArgs e)
		{
			LoadData();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				StreamWriter f = new StreamWriter(prop.Dir_export + "\\robot.cache");
				f.WriteLine("");
				f.Close();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
			LoadData();
		}
	}
}
