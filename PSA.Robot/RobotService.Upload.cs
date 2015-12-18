using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using PSA.Lib.Util;
using System.Data;
using System.Net;
using Xceed.Ftp;
using Xceed.FileSystem;

namespace PSA.Robot
{
    public partial class RobotService
    {

        public void UploadOrders()
        {
            try
            {
                Xceed.Ftp.Licenser.LicenseKey = "FTN42-K40Z3-DXCGS-PYGA";
                //Xceed.FileSystem.Licenser.LicenseKey = "";

                if (!UploadWork)
                {
                    try
                    {
                        UploadWork = true;
                        SqlConnection db_connection = new SqlConnection(prop.Connection_string);
                        db_connection.Open();
                        file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Выбираем заказы на экспорт");
                        file.Flush();
                        SqlCommand db_command = new SqlCommand("SELECT [number], [auto_export], [id_place] FROM [order] WHERE [auto_export] > 0;", db_connection);
                        SqlDataAdapter db_adapter = new SqlDataAdapter(db_command);
                        DataTable tbl = new DataTable();
                        db_adapter.Fill(tbl);

                        foreach (DataRow rw in tbl.Rows)
                        {
                            try
                            {
                                file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Подготавливаем к выгрузке заказ " + rw["number"].ToString().Trim());
                                file.Flush();
                                PSA.Lib.Util.ExportOrder.autoExport((int)rw["auto_export"], rw["number"].ToString().Trim());
                                db_command = new SqlCommand("SELECT [server], [path], [username], [password] FROM [place] WHERE [id_place] = " + rw["id_place"], db_connection);
                                db_adapter = new SqlDataAdapter(db_command);
                                DataTable ptbl = new DataTable();
                                db_adapter.Fill(ptbl);
                                if (ptbl.Rows.Count > 0)
                                {
                                    DataRow place = ptbl.Rows[0];
                                    file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Выгружаем заказ " + rw["number"].ToString().Trim() + " на " + place["server"].ToString().Trim() + " в " + place["path"].ToString().Trim());
                                    file.Flush();
                                    using (FtpConnection connection = new FtpConnection(
                                        place["server"].ToString().Trim(),
                                        place["username"].ToString().Trim(),
                                        place["password"].ToString().Trim()))
                                    {
                                        connection.Encoding = Encoding.GetEncoding(1251);

                                        file.WriteLine("from: " + prop.Dir_export + "\\auto_export\\" + rw["number"].ToString().Trim() + "\\");
                                        file.Flush();
                                        DiskFolder source = new DiskFolder(prop.Dir_export + "\\auto_export\\" + rw["number"].ToString().Trim() + "\\");

                                        string ftp_to = place["path"].ToString().Trim() + rw["number"].ToString().Trim() + "/";
                                        if (ftp_to.Substring(0, 1) == "/") ftp_to = ftp_to.Substring(1);
                                        file.WriteLine("to: " + ftp_to);
                                        file.Flush();
                                        try
                                        {
                                            FtpFolder _ftp_to = new FtpFolder(connection, ftp_to);
                                            _ftp_to.Delete();
                                        }
                                        catch { }

                                        FtpFolder destination = new FtpFolder(connection, ftp_to);

                                        StreamWriter _tmp = new StreamWriter(prop.Dir_export + "\\auto_export\\" + rw["number"].ToString().Trim() + "\\.lock");
                                        _tmp.Write("\n");
                                        _tmp.Close();
                                        db_command = new SqlCommand("UPDATE [order] SET [status_export] = 'Началась выгрузка', [status_export_date] = getdate() WHERE [number] = '" + rw["number"].ToString().Trim() + "'", db_connection);
                                        db_command.ExecuteNonQuery();
                                        source.CopyFilesTo(destination, true, true);
                                        FtpFile _lock = new FtpFile(connection, ftp_to + ".lock");
                                        _lock.Delete();
                                        db_command = new SqlCommand("UPDATE [order] SET [auto_export] = -1, [status_export] = 'Отправлен', [status] = '500000', [status_export_date] = getdate() WHERE [number] = '" + rw["number"].ToString().Trim() + "'", db_connection);
                                        db_command.ExecuteNonQuery();
                                        file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Выгружен заказ " + rw["number"].ToString().Trim());
                                        file.Flush();
                                        Directory.Delete(prop.Dir_export + "\\auto_export\\" + rw["number"].ToString().Trim() + "\\", true);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                db_command = new SqlCommand("UPDATE [order] SET [status_export] = '" + ex.Message + 
                                                            "', [status_export_date] = getdate() WHERE [number] = '" + rw["number"].ToString().Trim() + "'", 
                                                            db_connection);
                                db_command.ExecuteNonQuery();
                                file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выгрузки заказа " + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
                                file.Flush();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выгрузки заказов " + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
                        file.Flush();
                    }
                    finally
                    {
                        UploadWork = false;
                    }
                }
            }
            catch (Exception ex)
            {
                file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Глобальная ошибка по время отправления " + ex.Message);
                file.Flush();
            }
        }
    }
}
