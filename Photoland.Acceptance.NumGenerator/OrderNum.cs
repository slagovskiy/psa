using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Photoland.Security;
using Photoland.Security.User;


namespace Photoland.Acceptance.NumGenerator
{
	public class OrderNum
	{
		public string NewOrderNum(SqlConnection db_connection, UserInfo usr)
		{
			PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
			/*
			SqlCommand db_command = new SqlCommand("SELECT [order_num] FROM [vwLastOrderNum]", db_connection);
			SqlDataReader db_reader = db_command.ExecuteReader();
			long lastno = 0;
			if (db_reader.Read())
			{
				if (!db_reader.IsDBNull(0))
					lastno = db_reader.GetInt64(0);
				else
					lastno = 0;
			}
			else
			{
				lastno = 0;
			}
			db_reader.Close();
			lastno++;
			*/
			SqlCommand db_command = new SqlCommand();
			db_command.CommandText = "DECLARE @last int; INSERT INTO [dbo].[ordernum] ([del], [guid], [date], [usr], [used]) VALUES (0, newid(), getdate(), '', 1); SET @last = scope_identity(); SELECT @last;";
			db_command.Connection = db_connection;
			long lastno = int.Parse(prop.Order_prefics) * 10000000000 + (int)db_command.ExecuteScalar();
			if (lastno.ToString().Length < 12)
			{
				string t = lastno.ToString();
				string o = "";
				for (int i = 0; i < (12 - t.Length); i++)
				{
					o += "0";
				}
				return o + t;
			}
			else
			{
				return lastno.ToString();
			}
		}
	}
}
