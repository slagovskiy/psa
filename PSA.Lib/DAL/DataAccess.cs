using System;
using System.Data;
using System.Configuration;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections.Generic;
using PSA.Lib.Util;

namespace PSA.Lib.DAL
{
    public class DataAccess
    {
		private string _connectionString = "";
		protected string ConnectionString
		{
			get { return _connectionString; }
			set { _connectionString = value; }
		}

		public DataAccess()
		{
            Settings settings = new Settings();
			if (settings.Connection_string == "")
			{
				throw new ApplicationException("Пропущено определение строки подключения к базе данных.");
			}
			else
			{
                _connectionString = settings.Connection_string;
			}
		}

		protected DataSet FillDataSet(SqlCommand cmd, string tableName)
		{
			SqlConnection db_connection = new SqlConnection();
			db_connection.ConnectionString = _connectionString;
			cmd.Connection = db_connection;
			SqlDataAdapter adapter = new SqlDataAdapter(cmd);

			DataSet dataSet = new DataSet();
			try
			{
				db_connection.Open();
				adapter.Fill(dataSet, tableName);
			}
			finally
			{
				db_connection.Close();
			}
			return dataSet;
		}

		protected DataSet FillDataSet(List<SqlCommand> cmd, List<string> tableName)
		{
			SqlConnection db_connection = new SqlConnection();
			db_connection.ConnectionString = _connectionString;
			DataSet dataSet = new DataSet();
			SqlDataAdapter adapter = new SqlDataAdapter();

			try
			{
				db_connection.Open();
				for (int i = 0; i < cmd.Count; i++)
				{
					cmd[i].Connection = db_connection;
					adapter.SelectCommand = cmd[i];
					DataSet ds = new DataSet();
					adapter.Fill(ds, tableName[i]);
					dataSet.Tables.Add(ds.Tables[0].Copy());
				}
			}
			finally
			{
				db_connection.Close();
			}
			return dataSet;
		}

		protected int ExecNonQuery(SqlCommand cmd)
		{
			SqlConnection db_connection = new SqlConnection();
			db_connection.ConnectionString = _connectionString;
			cmd.Connection = db_connection;

			int result = 0;
			try
			{
				db_connection.Open();
				result = cmd.ExecuteNonQuery();
			}
			finally
			{
				db_connection.Close();
			}
			return result;
		}

		protected int ExecNonQuery(List<SqlCommand> cmd)
		{
			SqlConnection db_connection = new SqlConnection();
			db_connection.ConnectionString = _connectionString;

			int result = 0;
			try
			{
				db_connection.Open();
				for (int i = 0; i < cmd.Count; i++)
				{
					cmd[i].Connection = db_connection;
					cmd[i].ExecuteNonQuery();
					result = 1;
				}
			}
			catch(Exception ex)
			{
				result = 0;
			}
			finally
			{
				db_connection.Close();
			}
			return result;
		}

		protected object ExecuteScalar(SqlCommand cmd)
		{
			SqlConnection db_connection = new SqlConnection();
			db_connection.ConnectionString = _connectionString;
			cmd.Connection = db_connection;

			object result = null;
			try
			{
				db_connection.Open();
				result = cmd.ExecuteScalar();
			}
			finally
			{
				db_connection.Close();
			}
			return result;
		}
    }
}
