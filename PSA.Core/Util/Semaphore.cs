using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PSA.Lib.Util
{
	public class Semaphore
	{
		/// <summary>
		/// Проверяет наличие семафора для чтения настроек из удаленного каталога
		/// </summary>
		/// <returns></returns>
		static public string semRemoteSettings
		{
			get
			{
				string ret = "";
				try
				{
					if (File.Exists(System.Environment.GetCommandLineArgs()[0].Substring(
						0,
						System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
						) + "\\remote.sem"))
					{
						using (StreamReader sr = File.OpenText(System.Environment.GetCommandLineArgs()[0].Substring(
																0,
																System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
																) + "\\remote.sem"))
						{
							string s = "";
							if((s = sr.ReadLine()) != null)
							{
								ret = s;
							}
						}
					}
					else
					{
						ret = "";
					}
				}
				catch(Exception ex)
				{
					ret = "";
				}
				return ret;
			}
			set
			{
				if(value != "")
				{
					try
					{
						if (!File.Exists(System.Environment.GetCommandLineArgs()[0].Substring(
										0,
										System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
										) + "\\remote.sem"))
						{
							using (FileStream fs = File.Create(System.Environment.GetCommandLineArgs()[0].Substring(
																0,
																System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
																) + "\\remote.sem"))
							{
								Byte[] info =
									new UTF8Encoding(true).GetBytes(value);

								fs.Write(info, 0, info.Length);
							}
						}
						else
						{
							using (FileStream fs = File.OpenWrite(System.Environment.GetCommandLineArgs()[0].Substring(
																0,
																System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
																) + "\\remote.sem"))
							{
								Byte[] info =
									new UTF8Encoding(true).GetBytes(value);

								fs.Write(info, 0, info.Length);
							}
						}
					}
					catch(Exception ex)
					{
					}
				}
				else
				{
					if (File.Exists(System.Environment.GetCommandLineArgs()[0].Substring(
									0,
									System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
									) + "\\remote.sem"))
						File.Delete(System.Environment.GetCommandLineArgs()[0].Substring(
									0,
									System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
									) + "\\remote.sem");
				}
			}
		}

		static public bool semInventory
		{
			get
			{
				Settings prop = new Settings();
				return prop.SemaphoreInventory;
			}
			set
			{
				Settings prop = new Settings();
				prop.SemaphoreInventory = value;
				prop.Save();
			}
		}

	}
}
