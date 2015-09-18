using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using PSA.Lib.DAL;

namespace PSA.Lib.DAL.SqlProviders
{
	public class InventoryProvider : DataAccess
	{
		public InventoryProvider()
		{
		}

		public DataSet getById(int id)
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spInventory_GetById";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
			return FillDataSet(cmd, "inventory");
		}

		public DataSet GetList()
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spInventory_GetList";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			return FillDataSet(cmd, "inventory");
		}

	}
}
