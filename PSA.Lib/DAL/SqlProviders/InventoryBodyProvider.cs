﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace PSA.Lib.DAL.SqlProviders
{
    public class InventoryBodyProvider : DataAccess
    {
        public InventoryBodyProvider()
        {
        }

        public DataSet getByInventoryId(int id)
        {
            string query;
            SqlCommand cmd = new SqlCommand();
            query = "spInventoryBody_GetByInventoryId";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = query;
            cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
            return FillDataSet(cmd, "inventorybody");
        }

		public void UpdateAction(int id, string number, string action, string action_t, int user)
		{
			string query;
			SqlCommand cmd = new SqlCommand();
			query = "spInventoryBody_UpdateAction";
			cmd.CommandType = CommandType.StoredProcedure;
			cmd.CommandText = query;
			cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
			cmd.Parameters.Add("@number", SqlDbType.NChar).Value = number;
			cmd.Parameters.Add("@order_action", SqlDbType.NChar).Value = action;
			cmd.Parameters.Add("@order_action_t", SqlDbType.NChar).Value = action_t;
			cmd.Parameters.Add("@order_user", SqlDbType.Int).Value = user;
			ExecNonQuery(cmd);
		}
    }
}
