using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using PSA.Lib.Util;
using System.Data;

namespace PSA.Robot
{
	public partial class RobotService
	{
		private void doImport()
		{
			try
			{
				ini iniRobot = new ini(prop.Dir_export + "\\robot.ini");
				using (SqlConnection db_connection = new SqlConnection(prop.Connection_string))
				{
					db_connection.Open();
					if (prop.Import_from_ftp)
					{
						PSA.Lib.Util.ftpClient ftp = new PSA.Lib.Util.ftpClient(prop.FTP_Server, prop.FTP_User, prop.FTP_Password);
						string[] fileList = ftp.GetFileList(prop.FTP_Path);
						foreach (string s in fileList)
						{
							try
							{
								if (s != "info.version")
								{
									if (ftp.Download(prop.Dir_import, s, prop.FTP_Path))
									{
										file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Получен файл " + s);
										file.Flush();
										if (ftp.Delete(prop.FTP_Path + "/" + s + "\n"))
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Файл " + s + " удален с сервера");
											file.Flush();
										}
									}
								}
							}
							catch
							{
							}
						}
					}


					bool err = false;
					file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Опрос каталога");
					file.Flush();
					if (Directory.Exists(prop.Dir_import))
					{
						DirectoryInfo dr = new DirectoryInfo(prop.Dir_import);
						// before
						int ibef = 0;
						foreach (FileInfo f in dr.GetFiles("before*.sql"))
						{
							file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Найден файл предварительной SQL команды " + f.Name);
							file.Flush();
							string query = "";
							using (StreamReader fs = new StreamReader(f.FullName, Encoding.GetEncoding(1251)))
							{
								string s = "";
								while ((s = fs.ReadLine()) != null)
								{
									query += s + "\n";
								}
								fs.Close();
							}
							file.WriteLine(DateTime.Now.ToString("g", ci) + " [>] " + query);
							file.Flush();
							try
							{
								SqlCommand cmd = new SqlCommand(query, db_connection);
								cmd.CommandTimeout = 9000;
								cmd.ExecuteNonQuery();
								file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Запрос успешно выполнен");
								file.Flush();
							}
							catch (Exception ex)
							{
								file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
								file.Flush();
							}
							finally
							{
								f.Delete();
							}
							ibef++;
						}


						// import
						foreach (FileInfo f in dr.GetFiles("*.csv"))
						{
							file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Найден файл импорта " + f.Name);
							file.Flush();
							switch (f.Name)
							{
								case "mashine.csv":
									{
										// Загружаем таблицу машин
										// Первый тестовый проход на пригодность файла
										file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Начинаем иморт данных из файла " + f.Name);
										file.Flush();
										bool not_good = false;
										string s = "";
										StreamReader fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
										//StreamReader fl = f.OpenText();
										int pbi = 0;
										while ((s = fl.ReadLine()) != null)
										{
											string[] col;
											col = s.Split(';');
											if (col.Length != 4)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Найдена ошибка в структуре файла " + s);
												file.Flush();
												not_good = true;
												break;
											}
											pbi++;
										}
										fl.Close();

										if (!not_good)
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Очищаем таблицу машин операторов");
											file.Flush();
											try
											{
												string query = "DELETE FROM [mashine]";
												SqlCommand cmd = new SqlCommand(query, db_connection);
												cmd.CommandTimeout = 9000;
												cmd.ExecuteNonQuery();
											}
											catch (Exception ex)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
												file.Flush();
											}
											s = "";
											fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
											while ((s = fl.ReadLine()) != null)
											{
												try
												{
													string[] col;
													col = s.Split(';');
													string query = "INSERT INTO [mashine] ([id_mashine], [del], [mashine], [sklad]) VALUES ('{<ID>}', {<DEL>}, '{<MASHINE>}', '{<SKLAD>}')";
													query = query.Replace("{<ID>}", col[0]);
													query = query.Replace("{<DEL>}", col[1]);
													query = query.Replace("{<MASHINE>}", col[2]);
													query = query.Replace("{<SKLAD>}", col[3]);
													SqlCommand cmd = new SqlCommand(query, db_connection);
													cmd.CommandTimeout = 9000;
													cmd.ExecuteNonQuery();
												}
												catch (Exception ex)
												{
													file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
													file.Flush();
												}
											}
											fl.Close();
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Импорт завершен");
											file.Flush();
										}
										else
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] При проверке структуры файла " + f.Name +
														   "были найдены ошибки, импорт этого справочника не будет производится");
											file.Flush();
										}
										iniRobot.IniWriteValue("import", "mashine", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
										break;
									}
								case "material.csv":
									{
										// Загружаем таблицу материалов
										// Первый тестовый проход на пригодность файла
										file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Начинаем иморт данных из файла " + f.Name);
										file.Flush();
										bool not_good = false;
										string s = "";
										StreamReader fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
										int pbi = 0;
										while ((s = fl.ReadLine()) != null)
										{
											string[] col;
											col = s.Split(';');
											if (col.Length != 5)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Найдена ошибка в структуре файла " + s);
												file.Flush();
												not_good = true;
												break;
											}
											pbi++;
										}
										fl.Close();

										if (!not_good)
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Очищаем таблицу материалов");
											file.Flush();
											try
											{
												string query = "DELETE FROM [material]";
												SqlCommand cmd = new SqlCommand(query, db_connection);
												cmd.CommandTimeout = 9000;
												cmd.ExecuteNonQuery();
											}
											catch (Exception ex)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
												file.Flush();
											}
											s = "";
											fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
											while ((s = fl.ReadLine()) != null)
											{
												try
												{
													string[] col;
													col = s.Split(';');
													string query = "INSERT INTO [material] ([id_material], [del], [material], [remainder], [sklad]) VALUES ('{<ID>}', {<DEL>}, '{<MATERIAL>}', {<REMAINDER>}, '{<SKLAD>}')";
													query = query.Replace("{<ID>}", col[0]);
													query = query.Replace("{<DEL>}", col[1]);
													query = query.Replace("{<MATERIAL>}", col[2]);
													query = query.Replace("{<REMAINDER>}", col[3].Replace(",", "."));
													query = query.Replace("{<SKLAD>}", col[4]);
													SqlCommand cmd = new SqlCommand(query, db_connection);
													cmd.CommandTimeout = 9000;
													cmd.ExecuteNonQuery();
												}
												catch (Exception ex)
												{
													file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
													file.Flush();
												}
											}
											fl.Close();
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Импорт завершен");
										}
										else
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] При проверке структуры файла " + f.Name +
														   "были найдены ошибки, импорт этого справочника не будет производится");
											file.Flush();
										}
										iniRobot.IniWriteValue("import", "material", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
										break;
									}
								case "good.csv":
									{
										// Загружаем таблицу товаров
										// Первый тестовый проход на пригодность файла
										file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Начинаем иморт данных из файла " + f.Name);
										file.Flush();
										bool not_good = false;
										string s = "";
										StreamReader fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
										int pbi = 0;
										while ((s = fl.ReadLine()) != null)
										{
											string[] col;
											col = s.Split(';');
											if (col.Length != 15)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Найдена ошибка в структуре файла" + s);
												file.Flush();
												not_good = true;
												break;
											}
											pbi++;
										}
										fl.Close();

										if (!not_good)
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Очищаем таблицу товаров и услуг");
											file.Flush();
											try
											{
												string query = "DELETE FROM [good]";
												SqlCommand cmd = new SqlCommand(query, db_connection);
												cmd.CommandTimeout = 9000;
												cmd.ExecuteNonQuery();
											}
											catch (Exception ex)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
												file.Flush();
											}
											s = "";
											fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
											while ((s = fl.ReadLine()) != null)
											{
												try
												{
													string[] col;
													col = s.Split(';');
													string query = "INSERT INTO [good] ([id_good], [guid], [del], [name], [description], [prefix], [folder], [type], [checked], [sign], [apply_form], [EI], [zero], [bonustype], [kiosk_name]) VALUES ('{<ID>}', '{<GUID>}', {<DEL>}, '{<NAME>}', '{<DESCRIPTION>}', '{<PREFIX>}', '{<FOLDER>}', '{<TYPE>}', {<CHECKED>}, '{<SIGN>}', '{<APPLYFORM>}', {<EI>}, {<ZERO_PRICE>}, '{<BONUSTYPE>}', '{<KIOSK_NAME>}')";

													query = query.Replace("{<ID>}", col[0]);
													query = query.Replace("{<GUID>}", col[1]);
													query = query.Replace("{<DEL>}", col[2]);
													query = query.Replace("{<NAME>}", col[3]);
													query = query.Replace("{<DESCRIPTION>}", col[4]);
													query = query.Replace("{<PREFIX>}", col[5]);
													query = query.Replace("{<FOLDER>}", col[6]);
													query = query.Replace("{<TYPE>}", col[7]);
													if (col[8] == "True")
														query = query.Replace("{<CHECKED>}", "1");
													else
														query = query.Replace("{<CHECKED>}", "0");
													query = query.Replace("{<SIGN>}", col[9]);
													query = query.Replace("{<APPLYFORM>}", col[10]);
													query = query.Replace("{<EI>}", col[11].Replace(",", "."));
													if (col[12] == "True")
														query = query.Replace("{<ZERO_PRICE>}", "1");
													else
														query = query.Replace("{<ZERO_PRICE>}", "0");
													query = query.Replace("{<BONUSTYPE>}", col[13]);
													query = query.Replace("{<KIOSK_NAME>}", col[14]);
													SqlCommand cmd = new SqlCommand(query, db_connection);
													cmd.CommandTimeout = 9000;
													cmd.ExecuteNonQuery();
												}
												catch (Exception ex)
												{
													file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
													file.Flush();
												}
											}
											fl.Close();
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Импорт завершен");
											file.Flush();
										}
										else
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + "[!] При проверке структуры файла " + f.Name +
														   "были найдены ошибки, импорт этого справочника не будет производится");
											file.Flush();
										}
										iniRobot.IniWriteValue("import", "good", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
										break;
									}
								case "price.csv":
									{
										// Загружаем таблицу товаров
										// Первый тестовый проход на пригодность файла
										file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Начинаем иморт данных из файла " + f.Name);
										file.Flush();
										bool not_good = false;
										string s = "";
										StreamReader fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
										int pbi = 0;
										while ((s = fl.ReadLine()) != null)
										{
											string[] col;
											col = s.Split(';');
											if (col.Length != 11)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Найдена ошибка в структуре файла" + s);
												file.Flush();
												not_good = true;
												break;
											}
											pbi++;
										}
										fl.Close();

										if (!not_good)
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Очищаем таблицу прайса");
											file.Flush();
											try
											{
												string query = "DELETE FROM [price]";
												SqlCommand cmd = new SqlCommand(query, db_connection);
												cmd.ExecuteNonQuery();
												query = "DBCC CHECKIDENT (price, RESEED, 0)";
												cmd = new SqlCommand(query, db_connection);
												cmd.CommandTimeout = 9000;
												cmd.ExecuteNonQuery();
											}
											catch (Exception ex)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
												file.Flush();
											}
											s = "";
											fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
											while ((s = fl.ReadLine()) != null)
											{
												try
												{
													string[] col;
													col = s.Split(';');
													string query = "INSERT INTO [dbo].[price] ([id_good], [id_category], [amount], [amount2], [amount3], [threshold], [threshold2], [ondate]) VALUES ('{<IDGOOD>}', {<IDCATEGORY>}, {<AMOUNT>}, {<AMOUNT2>}, {<AMOUNT3>}, {<THRESHOLD>}, {<THRESHOLD2>}, CONVERT(DATETIME, '{<DATE>}', 102))";

													query = query.Replace("{<IDGOOD>}", col[1]);
													query = query.Replace("{<IDCATEGORY>}", col[2]);
													query = query.Replace("{<AMOUNT>}", col[5].Replace(",", "."));
													query = query.Replace("{<AMOUNT2>}", col[6].Replace(",", "."));
													query = query.Replace("{<AMOUNT3>}", col[7].Replace(",", "."));
													query = query.Replace("{<THRESHOLD>}", col[8].Replace(",", "."));
													query = query.Replace("{<THRESHOLD2>}", col[9].Replace(",", "."));
													query = query.Replace("{<DATE>}", col[10]);
													SqlCommand cmd = new SqlCommand(query, db_connection);
													cmd.CommandTimeout = 9000;
													cmd.ExecuteNonQuery();
												}
												catch (Exception ex)
												{
													file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
													file.Flush();
												}
											}
											fl.Close();
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Импорт завершен");
											file.Flush();
										}
										else
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] При проверке структуры файла " + f.Name +
														   "были найдены ошибки, импорт этого справочника не будет производится");
											file.Flush();
										}
										iniRobot.IniWriteValue("import", "price", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
										break;
									}
								case "category.csv":
									{
										// Загружаем таблицу категорий
										// Первый тестовый проход на пригодность файла
										file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Начинаем иморт данных из файла " + f.Name);
										file.Flush();
										bool not_good = false;
										string s = "";
										StreamReader fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
										int pbi = 0;
										while ((s = fl.ReadLine()) != null)
										{
											string[] col;
											col = s.Split(';');
											if (col.Length != 5)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Найдена ошибка в структуре файла" + s);
												file.Flush();
												not_good = true;
												break;
											}
											pbi++;
										}
										fl.Close();

										if (!not_good)
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Очищаем таблицу категорий и услуг");
											file.Flush();
											try
											{
												string query = "DELETE FROM [category]";
												SqlCommand cmd = new SqlCommand(query, db_connection);
												cmd.CommandTimeout = 9000;
												cmd.ExecuteNonQuery();
											}
											catch (Exception ex)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
												file.Flush();
											}
											s = "";
											fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
											while ((s = fl.ReadLine()) != null)
											{
												try
												{
													string[] col;
													col = s.Split(';');
													string query = "INSERT INTO [dbo].[category] ([id_category], [del], [name], [input]) VALUES ({<ID>}, {<DEL>}, '{<NAME>}', {<INPUT>})";

													query = query.Replace("{<ID>}", col[0]);
													query = query.Replace("{<DEL>}", col[2]);
													query = query.Replace("{<NAME>}", col[3]);
													query = query.Replace("{<INPUT>}", col[4]);
													SqlCommand cmd = new SqlCommand(query, db_connection);
													cmd.CommandTimeout = 9000;
													cmd.ExecuteNonQuery();
												}
												catch (Exception ex)
												{
													file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
													file.Flush();
												}
											}
											fl.Close();
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Импорт завершен");
											file.Flush();
										}
										else
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] При проверке структуры файла " + f.Name +
														   "были найдены ошибки, импорт этого справочника не будет производится");
											file.Flush();
										}
										iniRobot.IniWriteValue("import", "category", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
										break;
									}
								case "ptype.csv":
									{
										// Загружаем таблицу карт
										// Первый тестовый проход на пригодность файла
										file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Начинаем иморт данных из файла " + f.Name);
										file.Flush();
										bool not_good = false;
										string s = "";
										StreamReader fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
										int pbi = 0;
										while ((s = fl.ReadLine()) != null)
										{
											string[] col;
											col = s.Split(';');
											if (col.Length != 3)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Найдена ошибка в структуре файла" + s);
												file.Flush();
												not_good = true;
												break;
											}
											pbi++;
										}
										fl.Close();

										if (!not_good)
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Очищаем таблицу типов оплаты");
											file.Flush();
											try
											{
												string query = "DELETE FROM [ptype]";
												SqlCommand cmd = new SqlCommand(query, db_connection);
												cmd.CommandTimeout = 9000;
												cmd.ExecuteNonQuery();
											}
											catch (Exception ex)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
												file.Flush();
											}
											s = "";
											fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
											while ((s = fl.ReadLine()) != null)
											{
												try
												{
													string[] col;
													col = s.Split(';');
													string query = "INSERT INTO [dbo].[ptype] ([id_ptype], [name_ptype], [del]) VALUES ({<ID>}, '{<NAME>}', {<DEL>})";
													query = query.Replace("{<ID>}", col[0]);
													query = query.Replace("{<NAME>}", col[1]);
													query = query.Replace("{<DEL>}", col[2]);
													SqlCommand cmd = new SqlCommand(query, db_connection);
													cmd.CommandTimeout = 9000;
													cmd.ExecuteNonQuery();
												}
												catch (Exception ex)
												{
													file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
													file.Flush();
												}
											}
											fl.Close();
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Импорт завершен");
											file.Flush();
										}
										else
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] При проверке структуры файла " + f.Name +
														   "были найдены ошибки, импорт этого справочника не будет производится");
											file.Flush();
										}
										iniRobot.IniWriteValue("import", "ptype", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
										break;
									}
								case "dcard.csv":
									{
										// Загружаем таблицу дисконтных карт
										file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Начинаем иморт данных из файла " + f.Name);
										file.Flush();
										// Первый тестовый проход на пригодность файла
										bool not_good = false;
										string s = "";
										StreamReader fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
										int pbi = 0;
										while ((s = fl.ReadLine()) != null)
										{
											string[] col;
											col = s.Split(';');
											if (col.Length != 8)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Найдена ошибка в структуре файла" + s);
												file.Flush();
												not_good = true;
												break;
											}
											pbi++;
										}
										fl.Close();

										if (!not_good)
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Очищаем таблицу дисконтных карт");
											file.Flush();
											try
											{
												string query = "DELETE FROM [dcard]";
												SqlCommand cmd = new SqlCommand(query, db_connection);
												cmd.CommandTimeout = 9000;
												cmd.ExecuteNonQuery();
												query = "DBCC CHECKIDENT ([dcard], RESEED, 0)";
												cmd = new SqlCommand(query, db_connection);
												cmd.CommandTimeout = 9000;
												cmd.ExecuteNonQuery();
											}
											catch (Exception ex)
											{
												file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
												file.Flush();
											}
											s = "";
											fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
											while ((s = fl.ReadLine()) != null)
											{
												try
												{
													string[] col;
													col = s.Split(';');
													string query = "INSERT INTO [dcard] ([code], [name], [disc], [discserv], [phone], [email], [bonus], [typebonus]) VALUES ('{<CODE>}', '{<NAME>}', {<DISC>}, {<DISCSERV>}, '{<PHONE>}', '{<EMAIL>}', {<BONUS>}, '{<TYPEBONUS>}')";
													query = query.Replace("{<CODE>}", col[0]);
													query = query.Replace("{<NAME>}", col[1]);
													query = query.Replace("{<DISC>}", col[2].Replace(",", "."));
													query = query.Replace("{<DISCSERV>}", col[3].Replace(",", "."));
													query = query.Replace("{<PHONE>}", col[4]);
													query = query.Replace("{<EMAIL>}", col[5]);
													query = query.Replace("{<BONUS>}", col[6]);
													query = query.Replace("{<TYPEBONUS>}", col[7]);
													SqlCommand cmd = new SqlCommand(query, db_connection);
													cmd.CommandTimeout = 9000;
													cmd.ExecuteNonQuery();
												}
												catch (Exception ex)
												{
													file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
													file.Flush();
												}
											}
											fl.Close();
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Импорт завершен");
											file.Flush();
										}
										else
										{
											file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] При проверке структуры файла " + f.Name +
														   "были найдены ошибки, импорт этого справочника не будет производится");
											file.Flush();
										}
										iniRobot.IniWriteValue("import", "dcard", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
										break;
									}
							}
							f.Delete();
						}

						// after
						int iaft = 0;
						foreach (FileInfo f in dr.GetFiles("after*.sql"))
						{
							file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Найден файл заключительной SQL команды " + f.Name);
							file.Flush();
							string query = "";
							using (StreamReader fs = new StreamReader(f.FullName, Encoding.GetEncoding(1251)))
							{
								//byte[] buf = new byte[1];
								//ASCIIEncoding t = new ASCIIEncoding();
								//UTF8Encoding t = new UTF8Encoding(true);
								string s = "";
								while ((s = fs.ReadLine()) != null)
								{
									query += s + "\n";
								}
								fs.Close();
							}
							file.WriteLine(DateTime.Now.ToString("g", ci) + " [>] " + query);
							file.Flush();
							try
							{
								SqlCommand cmd = new SqlCommand(query, db_connection);
								cmd.CommandTimeout = 9000;
								cmd.ExecuteNonQuery();
								file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Запрос успешно выполнен");
								file.Flush();
							}
							catch (Exception ex)
							{
								file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
								file.Flush();
							}
							finally
							{
								f.Delete();
							}
							iaft++;
						}

                        foreach (FileInfo f in dr.GetFiles("getdata*.sql"))
                        {
                            file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Найден файл экспорта из SQL команды " + f.Name);
                            file.Flush();
                            string query = "";
                            using (StreamReader fs = new StreamReader(f.FullName, Encoding.GetEncoding(1251)))
                            {
                                string s = "";
                                while ((s = fs.ReadLine()) != null)
                                {
                                    query += s + "\n";
                                }
                                fs.Close();
                            }
                            file.WriteLine(DateTime.Now.ToString("g", ci) + " [>] " + query);
                            file.Flush();
                            try
                            {
                                string csv = SQL2CSV(query);
                                if (Directory.Exists(prop.Dir_export))
                                {
                                    StreamWriter fl = new StreamWriter(prop.Dir_export + "\\" + f.Name + "." + 
                                        DateTime.Now.Year.ToString("D4") + "_" + DateTime.Now.Month.ToString("D2") + "_" +
                                        DateTime.Now.Day.ToString("D2") + "_" + DateTime.Now.Hour.ToString("D2") + "_" +
                                        DateTime.Now.Minute.ToString("D2") + "_" + DateTime.Now.Second.ToString("D2") +
                                        ".csv", true, Encoding.GetEncoding(1251));
                                    fl.Write(csv);
                                    fl.Close();
                                }
                            }
                            catch (Exception ex)
                            {
                                file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выполнения запроса " + ex.Message + "\n" + ex.Source);
                                file.Flush();
                            }
                            finally
                            {
                                f.Delete();
                            }
                            iaft++;
                        }

					}
					else
					{
						file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Каталог импорта не найден");
						file.Flush();
					}
				}
			}
			catch
			{
			}
		}


        private string SQL2CSV(string sql)
        {
            string ret = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(prop.Connection_string))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 90000;
                    cmd.Connection = cn;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tbl = new DataTable("sql2cmd");
                    da.Fill(tbl);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            for (int j = 0; j < tbl.Columns.Count; j++)
                            {
                                sb.Append(tbl.Columns[j].ColumnName);
                                if (j < tbl.Columns.Count - 1)
                                    sb.Append(";");
                            }
                            sb.Append("\n");
                        }
                        for (int j = 0; j < tbl.Columns.Count; j++)
                        {
                            sb.Append(tbl.Rows[i][j].ToString().Trim());
                            if (j < tbl.Columns.Count - 1)
                                sb.Append(";");
                        }
                        sb.Append("\n");
                    }
                    ret = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }
    }


}
