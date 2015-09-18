using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Photoland.Security;


namespace Photoland.Security.Client
{
	public class ClientInfo
	{
		public ClientInfo(int id_client, SqlConnection db_con)
		{
			SqlCommand db_command = new SqlCommand();
			db_command.CommandText = "SELECT [id_client], [id_category], [guid], [name], [phone_1], [phone_2], [address], [email], [icq], [addon], [categoryname] FROM [vwClientFull] WHERE [id_client] = " + id_client.ToString();
			db_command.Connection = db_con;
			SqlDataReader db_reader = db_command.ExecuteReader();
			if (db_reader.Read())
			{
				if (!db_reader.IsDBNull(0))
					this._id = db_reader.GetInt32(0);
				if (!db_reader.IsDBNull(1))
					this._id_category = db_reader.GetInt32(1);
				if (!db_reader.IsDBNull(2))
					this._guid = db_reader.GetString(2);
				if (!db_reader.IsDBNull(3))
					this._name = db_reader.GetString(3);
				if (!db_reader.IsDBNull(4))
					this._phone1 = db_reader.GetString(4);
				if (!db_reader.IsDBNull(5))
					this._phone2 = db_reader.GetString(5);
				if (!db_reader.IsDBNull(6))
					this._address = db_reader.GetString(6);
				if (!db_reader.IsDBNull(7))
					this._email = db_reader.GetString(7);
				if (!db_reader.IsDBNull(8))
					this._icq = db_reader.GetString(8);
				if (!db_reader.IsDBNull(9))
					this._addon = db_reader.GetString(9);
				if (!db_reader.IsDBNull(10))
					this._category_name = db_reader.GetString(10);
			}
			db_reader.Close();
		}

		public ClientInfo(string client, int id_category, SqlConnection db_con)
		{
			SqlCommand db_command = new SqlCommand();
			db_command.CommandText = "SELECT [id_client], [id_category], [guid], [name], [phone_1], [phone_2], [address], [email], [icq], [addon], [categoryname] FROM [vwClientFull] WHERE RTRIM([name]) = '" + client.ToString().Trim() + "' AND [id_category] = " + id_category;
			db_command.Connection = db_con;
			SqlDataReader db_reader = db_command.ExecuteReader();
			if (db_reader.Read())
			{
				if (!db_reader.IsDBNull(0))
					this._id = db_reader.GetInt32(0);
				if (!db_reader.IsDBNull(1))
					this._id_category = db_reader.GetInt32(1);
				if (!db_reader.IsDBNull(2))
					this._guid = db_reader.GetString(2);
				if (!db_reader.IsDBNull(3))
					this._name = db_reader.GetString(3);
				if (!db_reader.IsDBNull(4))
					this._phone1 = db_reader.GetString(4);
				if (!db_reader.IsDBNull(5))
					this._phone2 = db_reader.GetString(5);
				if (!db_reader.IsDBNull(6))
					this._address = db_reader.GetString(6);
				if (!db_reader.IsDBNull(7))
					this._email = db_reader.GetString(7);
				if (!db_reader.IsDBNull(8))
					this._icq = db_reader.GetString(8);
				if (!db_reader.IsDBNull(9))
					this._addon = db_reader.GetString(9);
				if (!db_reader.IsDBNull(10))
					this._category_name = db_reader.GetString(10);
			}
			db_reader.Close();
		}

		public ClientInfo(int id_client, string guid, SqlConnection db_con)
        {
            SqlCommand db_command = new SqlCommand();
            db_command.CommandText = "SELECT [id_client], [id_category], [guid], [name], [phone_1], [phone_2], [address], [email], [icq], [addon], [categoryname] FROM [vwClientFull] WHERE [guid] = '" + guid + "'";
            db_command.Connection = db_con;
            SqlDataReader db_reader = db_command.ExecuteReader();
            if (db_reader.Read())
            {
                if (!db_reader.IsDBNull(0))
                    this._id = db_reader.GetInt32(0);
                if (!db_reader.IsDBNull(1))
                    this._id_category = db_reader.GetInt32(1);
                if (!db_reader.IsDBNull(2))
                    this._guid = db_reader.GetString(2);
                if (!db_reader.IsDBNull(3))
                    this._name = db_reader.GetString(3);
                if (!db_reader.IsDBNull(4))
                    this._phone1 = db_reader.GetString(4);
                if (!db_reader.IsDBNull(5))
                    this._phone2 = db_reader.GetString(5);
                if (!db_reader.IsDBNull(6))
                    this._address = db_reader.GetString(6);
                if (!db_reader.IsDBNull(7))
                    this._email = db_reader.GetString(7);
                if (!db_reader.IsDBNull(8))
                    this._icq = db_reader.GetString(8);
                if (!db_reader.IsDBNull(9))
                    this._addon = db_reader.GetString(9);
                if (!db_reader.IsDBNull(10))
                    this._category_name = db_reader.GetString(10);
            }
            db_reader.Close();
        }

        private int _id;
		public int Id
		{
			get { return _id; }
		}

		private string _guid;
		public string Guid
		{
			get { return _guid; }
		}
		
		private int _id_category;
		public int Id_category
		{
			get { return _id_category; }
		}

		private string _name;
		public string Name
		{
			get { return _name; }
		}
		
		private string _category_name;
		public string Category_name
		{
			get { return _category_name; }
		}

		private string _phone1;
		public string Phone1
		{
			get { return _phone1; }
		}
		private string _phone2;
		public string Phone2
		{
			get { return _phone2; }
		}

		private string _address;
		public string Address
		{
			get { return _address; }
		}

		private string _email;
		public string Email
		{
			get { return _email; }
		}

		private string _icq;
		public string Icq
		{
			get { return _icq; }
		}

		private string _addon;
		public string Addon
		{
			get { return _addon; }
		}

	}
}
