using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using PSA.Lib.Util;
using System.Net;
using ICSharpCode.SharpZipLib.Zip;

namespace PSA.Robot
{
	public partial class RobotService
	{
		private void doCache()
		{
			ini iniRobot = new ini(prop.Dir_export + "\\robot.ini");
			try
			{
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Запрос кэша korder.xml");
				file.Flush();
				HttpWebRequest objWebRequest;
				HttpWebResponse objWebResponse;
				StreamReader streamReader;
				String strHTML;
				objWebRequest = (HttpWebRequest)WebRequest.Create("http://print.fotoland.ru/photo/psa.get.korder.cache" + ((prop.UseXmlCache)?"":".zip") + ".php");
				objWebRequest.Method = "GET";
				objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
				streamReader = new StreamReader(objWebResponse.GetResponseStream(), Encoding.GetEncoding(1251));
				strHTML = streamReader.ReadToEnd();
				StreamWriter wr = new StreamWriter(prop.Dir_import + "//korder.xml" + ((prop.UseXmlCache) ? "" : ".zip"), false, Encoding.GetEncoding(1251));
				wr.Write(strHTML);
				wr.Close();
				streamReader.Close();
				objWebResponse.Close();
				objWebRequest.Abort();
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Получен кэш korder.xml" + ((prop.UseXmlCache) ? "" : ".zip") + "");
				file.Flush();
				if(!prop.UseXmlCache)
				{
					if (UnZipFile(prop.Dir_import + "//korder.xml.zip", "korder.xml"))
					{
						file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Распакован кэш korder.xml" + ((prop.UseXmlCache) ? "" : ".zip") + "");
						file.Flush();
					}
					else
					{
						file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка распаковки korder.xml" + ((prop.UseXmlCache) ? "" : ".zip"));
						file.Flush();
					}
				}
				iniRobot.IniWriteValue("cache", "korder", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
			}
			catch (Exception ex)
			{
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка при получении кэша korder.xml" + ((prop.UseXmlCache) ? "" : ".zip") + " " + ex.Message + "\n" + ex.Source);
				file.Flush();
			}


			try
			{
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Запрос кэша ioorder" + ((prop.UseXmlCache) ? "" : ".zip") + ".xml");
				file.Flush();
				HttpWebRequest objWebRequest;
				HttpWebResponse objWebResponse;
				StreamReader streamReader;
				String strHTML;
				objWebRequest = (HttpWebRequest)WebRequest.Create("http://print.fotoland.ru/photo/psa.get.ioorder.cache" + ((prop.UseXmlCache) ? "" : ".zip") + ".php");
				objWebRequest.Method = "GET";
				objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
				streamReader = new StreamReader(objWebResponse.GetResponseStream(), Encoding.GetEncoding(1251));
				strHTML = streamReader.ReadToEnd();
				StreamWriter wr = new StreamWriter(prop.Dir_import + "//ioorder.xml" + ((prop.UseXmlCache) ? "" : ".zip"), false, Encoding.GetEncoding(1251));
				wr.Write(strHTML);
				wr.Close();
				streamReader.Close();
				objWebResponse.Close();
				objWebRequest.Abort();
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Получен кэш ioorder.xml" + ((prop.UseXmlCache) ? "" : ".zip") + "");
				file.Flush();
				if (!prop.UseXmlCache)
				{
					if (UnZipFile(prop.Dir_import + "//ioorder.xml.zip", "ioorder.xml"))
					{
						file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Распакован кэш ioorder.xml" + ((prop.UseXmlCache) ? "" : ".zip") + "");
						file.Flush();
					}
					else
					{
						file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка распаковки ioorder.xml" + ((prop.UseXmlCache) ? "" : ".zip"));
						file.Flush();
					}
				}
				iniRobot.IniWriteValue("cache", "ioorder", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
			}
			catch (Exception ex)
			{
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка при получении кэша ioorder.xml" + ((prop.UseXmlCache) ? "" : ".zip") + " " + ex.Message + "\n" + ex.Source);
				file.Flush();
			}


			try
			{
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Запрос кэша irorder" + ((prop.UseXmlCache) ? "" : ".zip") + ".xml");
				file.Flush();
				HttpWebRequest objWebRequest;
				HttpWebResponse objWebResponse;
				StreamReader streamReader;
				String strHTML;
				objWebRequest = (HttpWebRequest)WebRequest.Create("http://print.fotoland.ru/photo/psa.get.irorder.cache" + ((prop.UseXmlCache) ? "" : ".zip") + ".php");
				objWebRequest.Method = "GET";
				objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
				streamReader = new StreamReader(objWebResponse.GetResponseStream(), Encoding.GetEncoding(1251));
				strHTML = streamReader.ReadToEnd();
				StreamWriter wr = new StreamWriter(prop.Dir_import + "//irorder.xml" + ((prop.UseXmlCache) ? "" : ".zip"), false, Encoding.GetEncoding(1251));
				wr.Write(strHTML);
				wr.Close();
				streamReader.Close();
				objWebResponse.Close();
				objWebRequest.Abort();
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Получен кэш irorder.xml" + ((prop.UseXmlCache) ? "" : ".zip") + "");
				file.Flush();
				if (!prop.UseXmlCache)
				{
					if (UnZipFile(prop.Dir_import + "//irorder.xml.zip", "irorder.xml"))
					{
						file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Распакован кэш irorder.xml" + ((prop.UseXmlCache) ? "" : ".zip") + "");
						file.Flush();
					}
					else
					{
						file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка распаковки irorder.xml" + ((prop.UseXmlCache) ? "" : ".zip"));
						file.Flush();
					}
				}
				iniRobot.IniWriteValue("cache", "irorder", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
			}
			catch (Exception ex)
			{
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка при получении кэша irorder.xml" + ((prop.UseXmlCache) ? "" : ".zip") + " " + ex.Message + "\n" + ex.Source);
				file.Flush();
			}

		}

		public static bool UnZipFile(string InputPathOfZipFile, string NewName)
		{
			bool ret = true;
			try
			{
				if (File.Exists(InputPathOfZipFile))
				{
					string baseDirectory = Path.GetDirectoryName(InputPathOfZipFile);
					using (ZipInputStream ZipStream = new
					ZipInputStream(File.OpenRead(InputPathOfZipFile)))
					{
						ZipEntry theEntry;
						while ((theEntry = ZipStream.GetNextEntry()) != null)
						{
							if (theEntry.IsFile)
							{
								if (theEntry.Name != "")
								{
									string strNewFile = @"" + baseDirectory + @"\" +
														NewName;
									if (File.Exists(strNewFile))
									{
										//continue;
									}
									using (FileStream streamWriter = File.Create(strNewFile))
									{
										int size = 2048;
										byte[] data = new byte[2048];
										while (true)
										{
											size = ZipStream.Read(data, 0, data.Length);
											if (size > 0)
												streamWriter.Write(data, 0, size);
											else
												break;
										}
										streamWriter.Close();
									}
								}
							}
							else if (theEntry.IsDirectory)
							{
								string strNewDirectory = @"" + baseDirectory + @"\" +
														theEntry.Name;
								if (!Directory.Exists(strNewDirectory))
								{
									Directory.CreateDirectory(strNewDirectory);
								}
							}
						}
						ZipStream.Close();
					}
				}
			}
			catch (Exception ex)
			{
				ret = false;
			}
			return ret;
		}  

	}
}
