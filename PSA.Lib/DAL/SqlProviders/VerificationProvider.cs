using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PSA.Lib.DAL;

namespace PSA.Lib.DAL.SqlProviders
{
	public class VerificationProvider : DataAccess
	{
		public VerificationProvider()
		{
		}

        public DataSet getById(int id)
        {
            string query;
            SqlCommand cmd = new SqlCommand();
            query = "spVerification_GetById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = query;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            return FillDataSet(cmd, "verification");
        }

        public DataSet GetList()
        {
            string query;
            SqlCommand cmd = new SqlCommand();
            query = "spVerification_GetList";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = query;
            return FillDataSet(cmd, "verification");
        }

	}
}
