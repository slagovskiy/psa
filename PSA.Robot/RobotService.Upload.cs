using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using PSA.Lib.Util;
using System.Data;
using System.Net;

namespace PSA.Robot
{
    public partial class RobotService
    {
        private void UploadOrders()
        {
            if (!UploadWork)
            {
                try
                {
                    UploadWork = true;
                    SqlConnection db_connection = new SqlConnection(prop.Connection_string);
                    db_connection.Open();
                    SqlCommand db_command = new SqlCommand("SELECT [number], [auto_export], [id_place] FROM [order] WHERE [auto_export] > 0;", db_connection);
                    SqlDataAdapter db_adapter = new SqlDataAdapter(db_command);
                    DataTable tbl = new DataTable();
                    db_adapter.Fill(tbl);

                    foreach (DataRow rw in tbl.Rows)
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
                            PSA.Lib.Util.ftpClient ftp = new PSA.Lib.Util.ftpClient(
                                place["server"].ToString().Trim(),
                                place["username"].ToString().Trim(),
                                place["password"].ToString().Trim()
                                );
                            if (ftp.Upload(
                                prop.Dir_export + "\\auto_export\\" + rw["number"].ToString().Trim() + "\\" + rw["number"].ToString().Trim() + ".export", 
                                place["path"].ToString() + "\\"
                                ))
                            {
                                file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Выгружен заказ " + rw["number"].ToString().Trim());
                                file.Flush();
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка выгрузки заказов " + ex.Message);
                    file.Flush();
                }
                finally
                {
                    UploadWork = false;
                }
            }
        }
    }
}
