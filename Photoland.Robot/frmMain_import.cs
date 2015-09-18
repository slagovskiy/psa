using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Forms.Interface;
using System.IO;
using Photoland.Lib;
using Photoland.Forms.Interface.Util;

namespace Photoland.Robot
{
    public partial class frmMain : Form
    {
        private void tmrRobot_Tick(object sender, EventArgs e)
        {
            try
            {
				ini iniRobot = new ini(System.Environment.GetCommandLineArgs()[0].Substring(
										0,
										System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
										) + "\\robot.ini");
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
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                        "] * Получен файл " + s + "\n");
                                    if (ftp.Delete(prop.FTP_Path + "/" + s + "\n"))
                                    {
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                            "] * Файл " + s + " удален с сервера\n");
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
                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                               "] Опрос каталога\n");
                if (Directory.Exists(prop.Dir_import))
                {
                    DirectoryInfo dr = new DirectoryInfo(prop.Dir_import);
                    // before
                    pb.Minimum = 0;
                    pb.Maximum = dr.GetFiles("before*.sql").Length;
                    pb.Value = 0;
                    int ibef = 0;
                    foreach (FileInfo f in dr.GetFiles("before*.sql"))
                    {
                        Application.DoEvents();
                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                       "] * Найден файл предварительной SQL команды " + f.Name + "\n");
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
                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] -> " + query +
                                       "\n");
                        try
                        {
                            SqlCommand cmd = new SqlCommand(query, db_connection);
                            cmd.CommandTimeout = 9000;
                            cmd.ExecuteNonQuery();
                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                           "] * Запрос успешно выполнен\n");
                        }
                        catch (Exception ex)
                        {
                            ErrorNfo.WriteErrorInfo(ex);
                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                           "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                        }
                        finally
                        {
                            f.Delete();
                        }
                        try
                        {
                            pb.Value = ibef;
                        }
                        catch
                        {
                        }
                        ibef++;
                    }


                    // import
                    foreach (FileInfo f in dr.GetFiles("*.csv"))
                    {
                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                       "] * Найден файл импорта " + f.Name + "\n");
                        switch (f.Name)
                        {
                            case "mashine.csv":
                                {
                                    // Загружаем таблицу машин
                                    // Первый тестовый проход на пригодность файла
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                   "] * Начинаем иморт данных из файла " + f.Name + "\n");
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
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Найдена ошибка в структуре файла\n" + s + "\n");
                                            not_good = true;
                                            break;
                                        }
                                        pbi++;
                                    }
                                    fl.Close();

                                    if (!not_good)
                                    {
                                        pb.Minimum = 0;
                                        pb.Maximum = pbi;
                                        pb.Value = 0;
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] * Очищаем таблицу машин операторов.\n");
                                        try
                                        {
                                            string query = "DELETE FROM [mashine]";
                                            SqlCommand cmd = new SqlCommand(query, db_connection);
                                            cmd.CommandTimeout = 9000;
                                            cmd.ExecuteNonQuery();
                                            query = "DBCC CHECKIDENT ([mashine], RESEED, 0)";
                                            cmd = new SqlCommand(query, db_connection);
                                            cmd.CommandTimeout = 9000;
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorNfo.WriteErrorInfo(ex);
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                        }
                                        s = "";
                                        fl = f.OpenText();
                                        while ((s = fl.ReadLine()) != null)
                                        {
                                            Application.DoEvents();
                                            try
                                            {
                                                string[] col;
                                                col = s.Split(';');
												string query = "INSERT INTO [mashine] ([id_mashine], [del], [mashine], [sklad]) VALUES ('{<ID>}', {<DEL>}, '{<MASHINE>}', '{<SKLAD>}')";
                                                query = query.Replace("{<ID>}", col[0]);
                                                query = query.Replace("{<DEL>}", col[1]);
                                                query = query.Replace("{<MASHINE>}", col[2]);
                                                SqlCommand cmd = new SqlCommand(query, db_connection);
                                                cmd.CommandTimeout = 9000;
                                                cmd.ExecuteNonQuery();
                                                try
                                                {
                                                    pb.Value++;
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorNfo.WriteErrorInfo(ex);
                                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                            }
                                        }
                                        fl.Close();
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] * Импорт завершен\n");
                                    }
                                    else
                                    {
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] ! При проверке структуры файла " + f.Name +
                                                       "были найдены ошибки, импорт этого справочника не будет производится.\n");
                                    }
									iniRobot.IniWriteValue("import", "mashine", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                                    break;
                                }
                            case "material.csv":
                                {
                                    // Загружаем таблицу материалов
                                    // Первый тестовый проход на пригодность файла
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                   "] * Начинаем иморт данных из файла " + f.Name + "\n");
                                    bool not_good = false;
                                    string s = "";
                                    StreamReader fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
                                    int pbi = 0;
                                    while ((s = fl.ReadLine()) != null)
                                    {
                                        string[] col;
                                        col = s.Split(';');
                                        if (col.Length != 4)
                                        {
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Найдена ошибка в структуре файла\n" + s + "\n");
                                            not_good = true;
                                            break;
                                        }
                                        pbi++;
                                    }
                                    fl.Close();

                                    if (!not_good)
                                    {
                                        pb.Minimum = 0;
                                        pb.Maximum = pbi;
                                        pb.Value = 0;
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] * Очищаем таблицу материалов.\n");
                                        try
                                        {
                                            string query = "DELETE FROM [material]";
                                            SqlCommand cmd = new SqlCommand(query, db_connection);
                                            cmd.CommandTimeout = 9000;
                                            cmd.ExecuteNonQuery();
                                            query = "DBCC CHECKIDENT ([material], RESEED, 0)";
                                            cmd = new SqlCommand(query, db_connection);
                                            cmd.CommandTimeout = 9000;
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorNfo.WriteErrorInfo(ex);
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                        }
                                        s = "";
                                        fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
                                        while ((s = fl.ReadLine()) != null)
                                        {
                                            Application.DoEvents();
                                            try
                                            {
                                                string[] col;
                                                col = s.Split(';');
                                                string query = "INSERT INTO [material] ([id_material], [del], [material], [remainder]) VALUES ('{<ID>}', {<DEL>}, '{<MATERIAL>}', {<REMAINDER>})";
                                                query = query.Replace("{<ID>}", col[0]);
                                                query = query.Replace("{<DEL>}", col[1]);
                                                query = query.Replace("{<MATERIAL>}", col[2]);
                                                query = query.Replace("{<REMAINDER>}", col[3].Replace(",", "."));
                                                SqlCommand cmd = new SqlCommand(query, db_connection);
                                                cmd.CommandTimeout = 9000;
                                                cmd.ExecuteNonQuery();
                                                try
                                                {
                                                    pb.Value++;
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorNfo.WriteErrorInfo(ex);
                                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                            }
                                        }
                                        fl.Close();
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] * Импорт завершен\n");
                                    }
                                    else
                                    {
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] ! При проверке структуры файла " + f.Name +
                                                       "были найдены ошибки, импорт этого справочника не будет производится.\n");
                                    }
									iniRobot.IniWriteValue("import", "material", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                                    break;
                                }
                            case "good.csv":
                                {
                                    // Загружаем таблицу товаров
                                    // Первый тестовый проход на пригодность файла
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                   "] * Начинаем иморт данных из файла " + f.Name + "\n");
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
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Найдена ошибка в структуре файла\n" + s + "\n");
                                            not_good = true;
                                            break;
                                        }
                                        pbi++;
                                    }
                                    fl.Close();

                                    if (!not_good)
                                    {
                                        pb.Minimum = 0;
                                        pb.Maximum = pbi;
                                        pb.Value = 0;
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] * Очищаем таблицу товаров и услуг.\n");
                                        try
                                        {
                                            string query = "DELETE FROM [good]";
                                            SqlCommand cmd = new SqlCommand(query, db_connection);
                                            cmd.CommandTimeout = 9000;
                                            cmd.ExecuteNonQuery();
                                            query = "DBCC CHECKIDENT ([good], RESEED, 0)";
                                            cmd = new SqlCommand(query, db_connection);
                                            cmd.CommandTimeout = 9000;
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorNfo.WriteErrorInfo(ex);
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                        }
                                        s = "";
                                        fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
                                        while ((s = fl.ReadLine()) != null)
                                        {
                                            Application.DoEvents();
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
                                                try
                                                {
                                                    pb.Value++;
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorNfo.WriteErrorInfo(ex);
                                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                            }
                                        }
                                        fl.Close();
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] * Импорт завершен\n");
                                    }
                                    else
                                    {
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] ! При проверке структуры файла " + f.Name +
                                                       "были найдены ошибки, импорт этого справочника не будет производится.\n");
                                    }
									iniRobot.IniWriteValue("import", "good", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                                    break;
                                }
                            case "price.csv":
                                {
                                    // Загружаем таблицу товаров
                                    // Первый тестовый проход на пригодность файла
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                   "] * Начинаем иморт данных из файла " + f.Name + "\n");
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
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Найдена ошибка в структуре файла\n" + s + "\n");
                                            not_good = true;
                                            break;
                                        }
                                        pbi++;
                                    }
                                    fl.Close();

                                    if (!not_good)
                                    {
                                        pb.Minimum = 0;
                                        pb.Maximum = pbi;
                                        pb.Value = 0;
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] * Очищаем таблицу прайса.\n");
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
                                            ErrorNfo.WriteErrorInfo(ex);
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                        }
                                        s = "";
                                        fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
                                        while ((s = fl.ReadLine()) != null)
                                        {
                                            Application.DoEvents();
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
                                                try
                                                {
                                                    pb.Value++;
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorNfo.WriteErrorInfo(ex);
                                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                            }
                                        }
                                        fl.Close();
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] * Импорт завершен\n");
                                    }
                                    else
                                    {
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] ! При проверке структуры файла " + f.Name +
                                                       "были найдены ошибки, импорт этого справочника не будет производится.\n");
                                    }
									iniRobot.IniWriteValue("import", "price", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                                    break;
                                }
                            case "category.csv":
                                {
                                    // Загружаем таблицу категорий
                                    // Первый тестовый проход на пригодность файла
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                   "] * Начинаем иморт данных из файла " + f.Name + "\n");
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
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Найдена ошибка в структуре файла\n" + s + "\n");
                                            not_good = true;
                                            break;
                                        }
                                        pbi++;
                                    }
                                    fl.Close();

                                    if (!not_good)
                                    {
                                        pb.Minimum = 0;
                                        pb.Maximum = pbi;
                                        pb.Value = 0;
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] * Очищаем таблицу категорий и услуг.\n");
                                        try
                                        {
                                            string query = "DELETE FROM [category]";
                                            SqlCommand cmd = new SqlCommand(query, db_connection);
                                            cmd.CommandTimeout = 9000;
                                            cmd.ExecuteNonQuery();
                                            query = "DBCC CHECKIDENT ([category], RESEED, 0)";
                                            cmd = new SqlCommand(query, db_connection);
                                            cmd.CommandTimeout = 9000;
                                            cmd.ExecuteNonQuery();
                                        }
                                        catch (Exception ex)
                                        {
                                            ErrorNfo.WriteErrorInfo(ex);
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                        }
                                        s = "";
                                        fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
                                        while ((s = fl.ReadLine()) != null)
                                        {
                                            Application.DoEvents();
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
                                                try
                                                {
                                                    pb.Value++;
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorNfo.WriteErrorInfo(ex);
                                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                            }
                                        }
                                        fl.Close();
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] * Импорт завершен\n");
                                    }
                                    else
                                    {
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] ! При проверке структуры файла " + f.Name +
                                                       "были найдены ошибки, импорт этого справочника не будет производится.\n");
                                    }
									iniRobot.IniWriteValue("import", "category", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                                    break;
                                }
                            case "dcard.csv":
                                {
                                    // Загружаем таблицу дисконтных карт
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                   "] * Начинаем иморт данных из файла " + f.Name + "\n");
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
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Найдена ошибка в структуре файла\n" + s + "\n");
                                            not_good = true;
                                            break;
                                        }
                                        pbi++;
                                    }
                                    fl.Close();

                                    if (!not_good)
                                    {
                                        pb.Minimum = 0;
                                        pb.Maximum = pbi;
                                        pb.Value = 0;
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] * Очищаем таблицу дисконтных карт.\n");
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
                                            ErrorNfo.WriteErrorInfo(ex);
                                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                           "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                        }
                                        s = "";
                                        fl = new StreamReader(f.FullName, System.Text.Encoding.GetEncoding(1251));
                                        while ((s = fl.ReadLine()) != null)
                                        {
                                            Application.DoEvents();
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
                                                try
                                                {
                                                    pb.Value++;
                                                }
                                                catch
                                                {
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorNfo.WriteErrorInfo(ex);
                                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                                            }
                                        }
                                        fl.Close();
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                               "] * Импорт завершен\n");
                                    }
                                    else
                                    {
                                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                       "] ! При проверке структуры файла " + f.Name +
                                                       "были найдены ошибки, импорт этого справочника не будет производится.\n");
                                    }
									iniRobot.IniWriteValue("import", "dcard", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                                    break;
                                }
                        }
                        f.Delete();
                    }

                    // after
                    pb.Minimum = 0;
                    pb.Maximum = dr.GetFiles("after*.sql").Length;
                    pb.Value = 0;
                    int iaft = 0;
                    foreach (FileInfo f in dr.GetFiles("after*.sql"))
                    {
                        Application.DoEvents();
                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                       "] * Найден файл заключительной SQL команды " + f.Name + "\n");
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
                        wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] -> " + query +
                                       "\n");
                        try
                        {
                            SqlCommand cmd = new SqlCommand(query, db_connection);
                            cmd.CommandTimeout = 9000;
                            cmd.ExecuteNonQuery();
                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                           "] * Запрос успешно выполнен\n");
                        }
                        catch (Exception ex)
                        {
                            ErrorNfo.WriteErrorInfo(ex);
                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                           "] ! Ошибка выполнения запроса! " + ex.Message + "\n" + ex.Source + "\n");
                        }
                        finally
                        {
                            f.Delete();
                        }
                        try
                        {
                            pb.Value = iaft;
                        }
                        catch
                        {
                        }
                        iaft++;
                    }


                }
                else
                {
                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                   "] ! Каталог импорта не найден!\n");
                }
            }
            catch
            {
            }
        }
    }
}
