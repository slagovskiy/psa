using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace PSA.Lib.DAL.SqlProviders
{
	public class UserProvider : DataAccess
	{
		public UserProvider()
		{
		}

		public string getNameById(int id)
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spUser_GetNameById";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
			//SqlParameter p = new SqlParameter("@name", SqlDbType.NChar);
			//p.Direction = ParameterDirection.Output;
			//cmd.Parameters.Add(p);
			//ExecNonQuery(cmd);
			//return p.Value.ToString();
			return ExecuteScalar(cmd).ToString().Trim();
		}

	}
}
