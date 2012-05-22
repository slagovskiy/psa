using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Globalization;
using System.IO;
using PSA.Lib.Util;


namespace PSA.Robot
{
	public partial class RobotService : ServiceBase
	{
		private System.Timers.Timer t1 = new System.Timers.Timer();
		private System.Timers.Timer t2 = new System.Timers.Timer();
		private System.Timers.Timer t3 = new System.Timers.Timer();
		private System.Timers.Timer t4 = new System.Timers.Timer();
		private System.Timers.Timer t5 = new System.Timers.Timer();
        private System.Timers.Timer t6 = new System.Timers.Timer();
		private StreamWriter file;
		private CultureInfo ci = new CultureInfo("de-DE");
		Settings prop = new Settings();

        private bool UploadWork = false;


		public RobotService()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			file = new StreamWriter(System.Environment.GetCommandLineArgs()[0].Substring(
				0,
				System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
				) + "\\PSA.Robot.Service.log", true, System.Text.Encoding.UTF8);
			file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Служба стартовала.");
			file.Flush();

			t1.Enabled = true;
			t1.Interval = 60000;
			t1.AutoReset = true;
			t1.Elapsed += new System.Timers.ElapsedEventHandler(t1_Elapsed);
			t1.Start();
			file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Запущено задание импорта.");
			file.Flush();

			t2.Enabled = true;
			t2.Interval = 60000;
			t2.AutoReset = true;
			t2.Elapsed += new System.Timers.ElapsedEventHandler(t2_Elapsed);
			t2.Start();
			file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Запущено задание экспорта.");
			file.Flush();

			t3.Enabled = true;
			t3.Interval = 60000;
			t3.AutoReset = true;
			t3.Elapsed += new System.Timers.ElapsedEventHandler(t3_Elapsed);
			t3.Start();
			file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Запущено задание мониторинга.");
			file.Flush();

			t4.Enabled = true;
			t4.Interval = 60000;
			t4.AutoReset = true;
			t4.Elapsed += new System.Timers.ElapsedEventHandler(t4_Elapsed);
			t4.Start();
			file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Запущено задание получения КЭШа.");
			file.Flush();

			t5.Enabled = true;
			t5.Interval = 60000;
			t5.AutoReset = true;
			t5.Elapsed += new System.Timers.ElapsedEventHandler(t5_Elapsed);
			t5.Start();
			file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Запущено задание повторной выгрузки суточных данных.");
			file.Flush();

            t6.Enabled = true;
            t6.Interval = 60000;
            t6.AutoReset = true;
            t6.Elapsed += new System.Timers.ElapsedEventHandler(t6_Elapsed);
            t6.Start();
            file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Запущено задание автоматической выгрузки заказов.");
            file.Flush();
        }

		protected override void OnStop()
		{
			t1.Stop();
			file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Остановлено задание импорта");
			file.Flush();

			t2.Stop();
			file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Остановлено задание экспорта.");
			file.Flush();

			t3.Stop();
			file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Остановлено задание мониторинга.");
			file.Flush();

			t4.Stop();
			file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Остановлено задание получения КЭШа.");
			file.Flush();

            t5.Stop();
            file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Остановлено задание повторной выгрузки суточных данных.");
            file.Flush();

            t6.Stop();
            file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Остановлено задание автоматической выгрузки заказов.");
            file.Flush();

            file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Служба остановлена.");
			file.Flush();
			file.Close();

		}

		private void t1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if ((DateTime.Now.Minute == 0) || (DateTime.Now.Minute == 15) || (DateTime.Now.Minute == 30) || (DateTime.Now.Minute == 45))
			{
				t1.Stop();
				try
				{
					doImport();
				}
				catch { }
				t1.Start();
			}
		}

		private void t2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if ((DateTime.Now.Minute == 10) || (DateTime.Now.Minute == 40))
			{
				t2.Stop();
				try
				{
					doExport();
				}
				catch { }
				t2.Start();
			}
		}

		private void t3_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				if (File.Exists(prop.Dir_export + "\\robot.export"))
				{
					t2.Stop();
					t3.Stop();
					try
					{
						file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Принудительный экспорт данных.");
						file.Flush();
						doExport();
						new FileInfo(prop.Dir_export + "\\robot.export").Delete();
					}
					catch { }
					t2.Start();
					t3.Start();

				}
				if (File.Exists(prop.Dir_export + "\\robot.import"))
				{
					t1.Stop();
					t3.Stop();
					try
					{
						file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Принудительный импорт справочников.");
						file.Flush();
						doImport();
						new FileInfo(prop.Dir_export + "\\robot.import").Delete();
					}
					catch { }
					t1.Start();
					t3.Start();
				}
				if (File.Exists(prop.Dir_export + "\\robot.cache"))
				{
					t4.Stop();
					t3.Stop();
					try
					{
						file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Принудительное получение КЭШа.");
						file.Flush();
						doCache();
						new FileInfo(prop.Dir_export + "\\robot.cache").Delete();
					}
					catch { }
					t4.Start();
					t3.Start();
				}
			}
			catch
			{
			}
		}


		private void t4_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if ((DateTime.Now.Minute == 0) || (DateTime.Now.Minute == 30))
			{
				t4.Stop();
				try
				{
					doCache();
				}
				catch { }
				t4.Start();
			}
		}

        private void t5_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if ((DateTime.Now.Hour == 0) && (DateTime.Now.Minute == 15))
            {
                t5.Stop();
                try
                {
                    ReExport();
                }
                catch { }
                t5.Start();
            }
        }

        private void t6_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            t6.Stop();
            file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Пробуем выгрузить заказы");
            file.Flush();            
            try
            {
                UploadOrders();
            }
            catch { }
            t6.Start();
        }

    }
}
