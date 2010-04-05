using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Photoland.Forms.Interface.Util
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
					if(File.Exists(Application.StartupPath + "\\remote.sem" ))
					{
						using (StreamReader sr = File.OpenText(Application.StartupPath + "\\remote.sem"))
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
						if(!File.Exists(Application.StartupPath + "\\remote.sem"))
						{
							using (FileStream fs = File.Create(Application.StartupPath + "\\remote.sem"))
							{
								Byte[] info =
									new UTF8Encoding(true).GetBytes(value);

								fs.Write(info, 0, info.Length);
							}
						}
						else
						{
							using (FileStream fs = File.OpenWrite(Application.StartupPath + "\\remote.sem"))
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
					if (File.Exists(Application.StartupPath + "\\remote.sem"))
						File.Delete(Application.StartupPath + "\\remote.sem");
				}
			}
		}

	}
}
