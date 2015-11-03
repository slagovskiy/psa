using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using PSA.Lib.Util;
using System.Net.Mail;
using System.Net;
using System.Data;

namespace PSA.Robot
{
	public partial class RobotService
	{
        private void FinExport()
        {
            string rf1 = System.IO.Path.GetTempPath() +
                                    DateTime.Now.Year.ToString("D4") + "-" +
                                    DateTime.Now.Month.ToString("D2") + "-" +
                                    DateTime.Now.Day.ToString("D2") + "-" +
                                    prop.Order_prefics + "-R1.csv";
            string rf2 = System.IO.Path.GetTempPath() +
                        DateTime.Now.Year.ToString("D4") + "-" +
                        DateTime.Now.Month.ToString("D2") + "-" +
                        DateTime.Now.Day.ToString("D2") + "-" +
                        prop.Order_prefics + "-R2.csv";
            using (SqlConnection cn = new SqlConnection())
            {
                try
                {
                    string r = "\"Отчет по выданным заказам\",\"\"\n";
                    double s = 0;
                    r += "\"за " + DateTime.Now.Day.ToString("D2") + "/" + DateTime.Now.Month.ToString("D2") + "/" + DateTime.Now.Year.ToString("D4") + "\",\"\"\n";
                    r += ",\n,\n";
                    r += "КЛИЕНТ,СУММА\n";
                    string q = "SELECT name, SUM(sum) AS sum " +
                                "FROM (SELECT name, SUM(payment) AS sum, Expr1 " +
                                "   FROM(" +
                                "       SELECT dbo.client.name, dbo.ptype.name_ptype, dbo.[order].advanced_payment + dbo.[order].final_payment AS payment, dbo.orderevent.event_status,  " +
                                "       dbo.[order].number, MAX(dbo.orderevent.event_date) AS Expr1 " +
                                "       FROM dbo.[order] INNER JOIN " +
                                "           dbo.client ON dbo.[order].id_client = dbo.client.id_client INNER JOIN " +
                                "           dbo.ptype ON dbo.[order].ptype = dbo.ptype.id_ptype INNER JOIN " +
                                "           dbo.orderevent ON dbo.[order].id_order = dbo.orderevent.id_order " +
                                "           GROUP BY dbo.client.name, dbo.ptype.name_ptype, dbo.[order].advanced_payment + dbo.[order].final_payment, dbo.orderevent.event_status,  " +
                                "           dbo.[order].number " +
                                "           HAVING (dbo.orderevent.event_status = N'100000') OR " +
                                "           (dbo.orderevent.event_status = N'200000')) AS tbl " +
                                "   GROUP BY name, name_ptype, event_status, number, Expr1) AS t " +
                                "WHERE     (Expr1 > CONVERT(DATETIME, '" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + " 00:00:00', 102)) AND (Expr1 < CONVERT(DATETIME, '" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + " 23:59:59', 102)) " +
                                "GROUP BY name " +
                                "ORDER BY name";
                    cn.ConnectionString = prop.Connection_string;
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandTimeout = 99999;
                    cmd.CommandText = q;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable t = new DataTable();
                    da.Fill(t);
                    foreach (DataRow rw in t.Rows)
                    {
                        s += double.Parse(rw[1].ToString());
                        r += "\"" + rw[0].ToString().Trim() + "\",\"" + rw[1].ToString().Replace(',', '.').Trim() + "\"\n";
                    }
                    r += "\"\",\"\"\n\"Итого\",\"" + s.ToString().Replace(",", ".") + "\"";
                    System.IO.File.WriteAllText(rf1, r, Encoding.UTF8);


                    cmd.CommandText = "SELECT [name_ptype] FROM [ptype] WHERE [del]=0";
                    da = new SqlDataAdapter(cmd);
                    DataTable tp = new DataTable();
                    da.Fill(tp);

                    r = "\"Отчет по выданным заказам\",\"\"\n";
                    r += "\"за " + DateTime.Now.Day.ToString("D2") + "/" + DateTime.Now.Month.ToString("D2") + "/" + DateTime.Now.Year.ToString("D4") + "\",\"\"\n";
                    r += ",\n,\n";
                    r += "\"КЛИЕНТ\",";
                    foreach (DataRow rwp in tp.Rows)
                    {
                        r += "\"" + rwp[0].ToString().Trim().ToUpper() + "\",";
                    }
                    r = r.Substring(0, r.Length - 1) + "\n";

                    cmd.CommandText = "SELECT     TOP (100) PERCENT name, SUM(sum) AS sum, name_ptype " +
                                "FROM         (SELECT     name, name_ptype, SUM(payment) AS sum, Expr1 " +
                                "                       FROM          (SELECT     dbo.client.name, dbo.ptype.name_ptype, dbo.[order].advanced_payment + dbo.[order].final_payment AS payment, dbo.orderevent.event_status,  " +
                                "                                                                      dbo.[order].number, MAX(dbo.orderevent.event_date) AS Expr1 " +
                                "                                               FROM          dbo.[order] INNER JOIN " +
                                "                                                                      dbo.client ON dbo.[order].id_client = dbo.client.id_client INNER JOIN " +
                                "                                                                      dbo.ptype ON dbo.[order].ptype = dbo.ptype.id_ptype INNER JOIN " +
                                "                                                                      dbo.orderevent ON dbo.[order].id_order = dbo.orderevent.id_order " +
                                "                                               GROUP BY dbo.client.name, dbo.ptype.name_ptype, dbo.[order].advanced_payment + dbo.[order].final_payment, dbo.orderevent.event_status,  " +
                                "                                                                      dbo.[order].number " +
                                "                                               HAVING      (dbo.orderevent.event_status = N'100000') OR " +
                                "                                                                      (dbo.orderevent.event_status = N'200000')) AS tbl " +
                                "                       GROUP BY name, name_ptype, event_status, number, Expr1) AS t " +
                                "WHERE     (Expr1 > CONVERT(DATETIME, '" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + " 00:00:00', 102)) AND (Expr1 < CONVERT(DATETIME, '" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + " 23:59:59', 102)) " +
                                "GROUP BY name, name_ptype " +
                                "ORDER BY name";
                    da = new SqlDataAdapter(cmd);
                    t = new DataTable();
                    da.Fill(t);
                    foreach (DataRow rw in t.Rows)
                    {
                        int rr = 0;
                        foreach (DataRow rwp in tp.Rows)
                        {
                            if (rwp[0].ToString().Trim() == rw[2].ToString().Trim())
                                break;
                            else
                                rr++;
                        }
                        string rp = "";
                        for (int i = 0; i < rr; i++)
                            rp += "\"\",";
                        r += "\"" + rw[0].ToString().Trim() + "\"," + rp + "\"" + rw[1].ToString().Replace(',', '.').Trim() + "\"\n";

                    }
                    System.IO.File.WriteAllText(rf2, r, Encoding.UTF8);

                    cn.Close();
                }
                catch (Exception ex)
                {
                }
            }

            try
            {
                foreach (string ss in prop.EmailDayReport.Split(';'))
                {
                    MailMessage m = new MailMessage(prop.EmailDayFrom, ss);
                    m.Subject = prop.Order_prefics + " отчет по выданным заказам";
                    m.Attachments.Add(new Attachment(rf1));
                    m.Attachments.Add(new Attachment(rf2));
                    m.Body = prop.Order_prefics + " отчет по выданным заказам";
                    SmtpClient s = new SmtpClient();
                    s.Host = prop.EmailDayHost;
                    s.Port = prop.EmailDayPort;
                    s.DeliveryMethod = SmtpDeliveryMethod.Network;
                    s.UseDefaultCredentials = true;
                    s.Credentials = new NetworkCredential(prop.EmailDayAuth, prop.EmailDayPas);
                    s.Send(m);
                }
            }
            catch (Exception ex)
            {
            }
        }

		private void ReExport()
		{
			try
			{
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Начинаем подготовку к повторной выгрузке данных за двое последних суток");
				file.Flush();
				using (SqlConnection cn = new SqlConnection(prop.Connection_string))
				{
					cn.Open();
					SqlCommand cmd = new SqlCommand();
					cmd.CommandTimeout = 90000;
					cmd.Connection = cn;

					string query = "";

					string date = DateTime.Now.AddDays(-2).Year.ToString("D4") + "-" +
									DateTime.Now.AddDays(-2).Month.ToString("D2") + "-" +
									DateTime.Now.AddDays(-2).Day.ToString("D2") + " " +
									"00:00:00";

					query += "UPDATE [order]" +
							 " SET [exported] = 0" +
							 " WHERE ([id_order] IN" +
							 " (SELECT [id_order]" +
							 " FROM [orderevent]" +
							 " WHERE (([event_date] >= CONVERT(DATETIME, '" + date + "', 102)) " +
							 " AND ([event_date] <= getdate()))))\n";

					query += "UPDATE [orderbody] SET [exported] = 0" +
							 " WHERE (id_order = 0) AND (([dateadd] >= CONVERT(DATETIME, '" + date + "', 102))" +
							 " AND ([dateadd] <= getdate()))\n";

					query += "UPDATE [payments] SET [exported] = 0" +
							 " WHERE (([date] >= CONVERT(DATETIME, '" + date + "', 102)) AND ([date] <= getdate()))\n";

					query += "UPDATE [discard] SET [exported] = 0" +
							 " WHERE (([datediscard] >= CONVERT(DATETIME, '" + date + "', 102))"+
							 "AND ([datediscard] <= getdate()))\n";

					cmd.CommandText = query;
					cmd.ExecuteNonQuery();
					file.WriteLine(DateTime.Now.ToString("g", ci) + " [+] Подготовка данных к повторной выгрузке завершена");
					file.Flush();
				}
			}
			catch(Exception ex)
			{
				file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Ошибка подготовки повторной выгрузки " + ex.Message);
				file.Flush();
			}
		}
	}
}
