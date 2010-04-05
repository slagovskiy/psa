using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Photoland.Robot
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			//Application.EnableVisualStyles();
			//Application.SetCompatibleTextRenderingDefault(false);
			//Application.Run(new frmMain());
			MessageBox.Show("Используйте службу PSA.Robot!"); 
			Application.Exit();
		}
	}
}