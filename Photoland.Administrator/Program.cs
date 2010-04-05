using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Photoland.CrashInfo;

namespace Photoland.Administrator
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			// При возникновении исключений (ошибок)
			// вызываем обработчик
			Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
			Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
			// Дальше уже стандартный запуск
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new frmMain());
		}


		// Обработчик всех возникших исключений (ошибок)
		static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
		{
			// Создаем экземпляр формы
			frmApplicationCrash cf = new frmApplicationCrash();
			// Передаем в форму данные о возникшем исключении
			cf.FillInfo(e.Exception);
			cf.ShowDialog();
			// Определяем необходимость перезапуска программы
			if (cf.Restart)
				Application.Restart();
			else
				Application.Exit();
		}
	}
}