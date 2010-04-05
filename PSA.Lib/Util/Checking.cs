using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PSA.Lib.Util
{
	public class Checking
	{

		static public bool checkVersion(Modules Module, string version)
		{
			bool ret = false;
			try
			{
				Settings sett = new Settings();
				ftpClient ftp = new ftpClient(sett.FTP_Server, sett.FTP_User, sett.FTP_Password);
				foreach(String file in ftp.GetFileList(sett.FTP_Path))
				{
					if(file == "info.version")
					{
						ftp.Download(sett.Dir_import, "info.version", sett.FTP_Path, "info.version");
						ini ver = new ini(sett.Dir_import + "\\info.version");
						switch(Module)
						{
							case Modules.Acceptance:
								{
									if (ver.IniReadValue("version", "Acceptance", "0").Trim() != version)
										ret = false;
									else
										ret = true;
									break;
								}
							case Modules.Administrator:
								{
									if (ver.IniReadValue("version", "Administrator", "0").Trim() != version)
										ret = false;
									else
										ret = true;
									break;
								}
							case Modules.Designer:
								{
									if (ver.IniReadValue("version", "Designer", "0").Trim() != version)
										ret = false;
									else
										ret = true;
									break;
								}
							case Modules.Operator:
								{
									if (ver.IniReadValue("version", "Operator", "0").Trim() != version)
										ret = false;
									else
										ret = true;
									break;
								}
							case Modules.Robot:
								{
									if (ver.IniReadValue("version", "Robot", "0").Trim() != version)
										ret = false;
									else
										ret = true;
									break;
								}
							case Modules.Inventory:
								{
									if (ver.IniReadValue("version", "Inventory", "0").Trim() != version)
										ret = false;
									else
										ret = true;
									break;
								}
							case Modules.Kiosk:
								{
									if (ver.IniReadValue("version", "Kiosk", "0").Trim() != version)
										ret = false;
									else
										ret = true;
									break;
								}
							case Modules.Exchanger:
								{
									if (ver.IniReadValue("version", "Exchanger", "0").Trim() != version)
										ret = false;
									else
										ret = true;
									break;
								}
							default:
								{
									ret = false;
									break;
								}
						}
					}
				}
				//ret = true;
			}
			catch (Exception ex)
			{
				ret = true;
			}
			finally
			{
				
			}
			return ret;
		}
	}
}
