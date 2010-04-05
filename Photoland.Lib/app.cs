using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.IO;

namespace Photoland.Lib
{
	public class app
	{
		public static string ModuleName = Process.GetCurrentProcess().MainModule.ModuleName;
		public static string ProcName = Path.GetFileNameWithoutExtension(ModuleName);
		public static bool Search_twin()
		{
			Process[] pr;

			pr = Process.GetProcessesByName(ProcName);
			if (pr.Length > 1)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
