using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;

namespace Photoland.Lib
{
	class ErrorNfo
	{
		internal static void WriteErrorInfo(Exception ex)
		{
			try
			{
				PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
				StringBuilder sb = new StringBuilder();

				sb.AppendFormat("{0}\r\n\r\n", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());

				sb.AppendFormat("- Информация об ошибке\r\n");

				sb.AppendFormat("Основная информация об ошибке: {0}\r\n", ex.Message);
				sb.AppendFormat("Произошла ошибка типа {0}\r\n", ex.GetType());
				sb.AppendFormat("Объект вызвавший ошибку: {0}\r\n", ex.Source);
				sb.AppendFormat("Ошибка произошла в методе {0}\r\n", ex.TargetSite);
				sb.AppendFormat("Стек вызова: \r\n{0}\r\n\r\n", ex.StackTrace);

				sb.AppendFormat("- Соединение с базой данных\r\n");
				sb.AppendFormat("Строка подключения: {0}\r\n", prop.Connection_string);

				try
				{
					sb.AppendFormat("Попытка подключения: {0:MM/dd/yyy hh:mm:ss.fff}\r\n", DateTime.Now);
					SqlConnection c = new SqlConnection(prop.Connection_string);
					c.Open();
					sb.AppendFormat("Соединение установлено: {0:MM/dd/yyy hh:mm:ss.fff}\r\n", DateTime.Now);
					c.Close();
				}
				catch (Exception exx)
				{
					ErrorNfo.WriteErrorInfo(exx);
					sb.AppendFormat("Произошла ошибка: {0:MM/dd/yyy hh:mm:ss.fff}\r\n", DateTime.Now);
					sb.AppendFormat("Описание ошибки: {0}", exx.Message);
				}
				finally
				{
					sb.AppendFormat("\r\n");
				}

				sb.AppendFormat("- Дополнительная информация\r\n");

				sb.AppendFormat("Machine name: {0}\r\n", Environment.MachineName);
				sb.AppendFormat("OS Version: {0}\r\n", Environment.OSVersion);
				sb.AppendFormat("Processor count: {0}\r\n", Environment.ProcessorCount);
				sb.AppendFormat("User domain name: {0}\r\n", Environment.UserDomainName);
				sb.AppendFormat("User name: {0}\r\n", Environment.UserName);
				sb.AppendFormat("User interactive: {0}\r\n", Environment.UserInteractive);
				sb.AppendFormat("dotNet version: {0}\r\n\r\n", Environment.Version);


				sb.AppendFormat("");

				StreamWriter sr = new StreamWriter(prop.Dir_export + "\\er_" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + ".info", true, Encoding.GetEncoding(1251));
				sr.Write(sb);
				sr.Close();
			}
			catch
			{
			}

		}
	}
}
