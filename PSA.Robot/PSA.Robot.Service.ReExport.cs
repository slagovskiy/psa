using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using PSA.Lib.Util;

namespace PSA.Robot
{
	public partial class RobotService
	{
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
