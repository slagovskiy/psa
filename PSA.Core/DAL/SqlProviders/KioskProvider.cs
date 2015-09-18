using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace PSA.Lib.DAL.SqlProviders
{
	public class KioskProvider : DataAccess
	{
		public DataSet Get()
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spKiosk_Get";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			return FillDataSet(cmd, "kiosk");
		}

		public DataSet GetAll()
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spKiosk_GetAll";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			return FillDataSet(cmd, "kiosk");
		}

		public DataSet getById(int id)
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spKiosk_GetById";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
			return FillDataSet(cmd, "kiosk");
		}

		public DataSet getByCode(int code)
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spKiosk_GetByCode";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			cmd.Parameters.Add("@code", SqlDbType.Int).Value = code;
			return FillDataSet(cmd, "kiosk");
		}

		public int add(string name, string path, int code)
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spKiosk_Add";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			cmd.Parameters.Add("@name", SqlDbType.NChar).Value = name;
			cmd.Parameters.Add("@path", SqlDbType.NChar).Value = path;
			cmd.Parameters.Add("@code", SqlDbType.Int).Value = code;
			SqlParameter p = new SqlParameter("@id", SqlDbType.Int);
			p.Direction = ParameterDirection.Output;
			cmd.Parameters.Add(p);
			ExecNonQuery(cmd);
			return (int)p.Value;
		}

		public int update(string name, string path, int code, int id)
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spKiosk_Update";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			cmd.Parameters.Add("@name", SqlDbType.NChar).Value = name;
			cmd.Parameters.Add("@path", SqlDbType.NChar).Value = path;
			cmd.Parameters.Add("@code", SqlDbType.Int).Value = code;
			cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
			return ExecNonQuery(cmd);
		}

		public int delete(int id, bool del)
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spKiosk_Delete";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
			cmd.Parameters.Add("@del", SqlDbType.Bit).Value = del;
			return ExecNonQuery(cmd);
		}
	}
}
