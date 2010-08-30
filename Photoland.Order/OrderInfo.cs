using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Photoland.Lib;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.User;
using Photoland.Security.Discont;


namespace Photoland.Order
{
	public class OrderInfo
	{
		public SqlConnection db_connection;

		public UserInfo Usr;
		public ClientInfo Client;
		public DiscontInfo Discont;
		public DataTable OrderBody;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		private int _crop = 0;
		public int Crop
		{
			get { return _crop; }
			set { _crop = value; }
		}

		private int _type = 0;
		public int Type
		{
			get { return _type; }
			set { _type = value; }
		}


		private string _datein;
		public string Datein
		{
			get { return _datein; }
			set { _datein = value; }
		}

		private string _timein;
		public string Timein
		{
			get { return _timein; }
			set { _timein = value; }
		}

		private string _dateout;
		public string Dateout
		{
			get { return _dateout; }
			set { _dateout = value; }
		}

		private string _timeout;
		public string Timeout
		{
			get { return _timeout; }
			set { _timeout = value.Replace("-", ":"); }
		}

		private string _orderno = "";
		public string Orderno
		{
			get { return _orderno; }
			set { _orderno = value; }
		}

		private string _err = "";
		public string Err
		{
			get { return _err; }
			set { _err = value; }
		}

		private decimal _advancedPayment = 0;
		public decimal AdvancedPayment
		{
			get { return _advancedPayment; }
			set { _advancedPayment = decimal.Round(value, 2); }
		}

		private decimal _finalPayment = 0;
		public decimal FinalPayment
		{
			get { return _finalPayment; }
			set { _finalPayment = decimal.Round(value, 2); }
		}

		private decimal _bonus = 0;
		public decimal Bonus
		{
			get { return _bonus; }
			set { _bonus = decimal.Round(value, 2); }
		}

		private string _distanation = "000100";
		public string Distanation
		{
			get { return _distanation; }
			set { _distanation = value; }
		}

		private int _preview = 0;
		public int Preview
		{
			get { return _preview; }
			set { _preview = value; }
		}

		private int _id = 0;
		public int Id
		{
			get { return _id; }
			set { _id = value; }
		}

		private int _client_id = 0;
		public int ClientId
		{
			get { return _client_id; }
			set { _client_id = value; }
		}

		private string _comment = "";
		public string Comment
		{
			get { return _comment; }
			set { _comment = value; }
		}

        private string _name_accept = "";
        public string Name_accept
        {
            get { return _name_accept; }
            set { _name_accept = value; }
        }

        private decimal _order_price = 0;
        public decimal Order_price
        {
            get { return _order_price; }
            set { _order_price = value; }
        }

		private int _ptype = 0;
		public int PType
		{
			get { return _ptype; }
			set { _ptype = value; }
		}


		public OrderInfo(SqlConnection db_connection)
		{
			this.db_connection = db_connection;
		}

		public OrderInfo(SqlConnection db_connection, int id_order)
		{
			this.db_connection = db_connection;
			this._id = id_order;

			SqlCommand tmp_cmd = new SqlCommand("SELECT [id_order] as [id], [id_order], [id_user_accept], [id_user_operator], [id_user_designer], [id_user_delivery], [id_client], [guid], [del], [name_accept], [name_operator], [name_delivery], [status], [number], [input_date], [expected_date], [output_date], [advanced_payment], [final_payment], [discont_percent], [discont_code], [preview], [comment], [crop], [type], [bonus], [order_price], [ptype] FROM [order] WHERE [id_order] = " + id_order, db_connection);
			SqlDataReader tmp_rdr = tmp_cmd.ExecuteReader();
			if (tmp_rdr.Read())
			{
				/*
				 * 1  [id_order], 
				 * 2  [id_user_accept], 
				 * 3  [id_user_operator], 
				 * 4  [id_user_designer], 
				 * 5  [id_user_delivery], 
				 * 6!  [id_client], 
				 * 7  [guid], 
				 * 8  [del], 
				 * 9  [name_accept], 
				 * 10 [name_operator], 
				 * 11 [name_delivery], 
				 *! 12 [status], 
				 *! 13 [number], 
				 *! 14 [input_date], 
				 *! 15 [expected_date], 
				 * 16 [output_date], 
				 *! 17 [advanced_payment], 
				 *! 18 [final_payment], 
				 *! 19 [discont_percent], 
				 *! 20 [discont_code], 
				 *! 21 [preview], 
				 *! 22 [comment]
				 *! 23 [crop]
				 *! 24 [type]
				 *  25 [bonus]
                 *  26 order_price
				 *  27 ptype
				 */
				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(9))
					_name_accept = tmp_rdr.GetString(9);
				else
					_name_accept = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(23))
					_crop = tmp_rdr.GetInt32(23);
				else
					_crop = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(24))
					_type = tmp_rdr.GetInt32(24);
				else
					_type = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(22))
					_comment = tmp_rdr.GetString(22);
				else
					_comment = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(21))
					if (tmp_rdr.GetBoolean(21))
						_preview = 1;
					else
						_preview = 0;
				else
					_preview = 0;

				///////////////////////////////////
				if ((!tmp_rdr.IsDBNull(20)) && (!tmp_rdr.IsDBNull(20)))
				{
					Discont = new DiscontInfo(tmp_rdr.GetString(20), db_connection);
					Discont.Id_dcard = 777777777;
				}
				else
				{
					Discont = new DiscontInfo("", 0);
				}

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(18))
					_finalPayment = tmp_rdr.GetDecimal(18);
				else
					_finalPayment = 0;

                ///////////////////////////////////
                if (!tmp_rdr.IsDBNull(25))
                    _bonus = tmp_rdr.GetDecimal(25);
                else
                    _bonus = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(26))
					_order_price = tmp_rdr.GetDecimal(26);
				else
					_order_price = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(27))
					_ptype = tmp_rdr.GetInt32(27);
				else
					_ptype = -1;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(17))
					_advancedPayment = tmp_rdr.GetDecimal(17);
				else
					_advancedPayment = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(14))
					_datein = tmp_rdr.GetDateTime(14).ToShortDateString();
				else
					_datein = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(14))
					_timein = tmp_rdr.GetDateTime(14).ToShortTimeString();
				else
					_timein = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(15))
					_dateout = tmp_rdr.GetDateTime(15).ToShortDateString();
				else
					_dateout = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(15))
					_timeout = tmp_rdr.GetDateTime(15).ToShortTimeString();
				else
					_timeout = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(13))
					_orderno = tmp_rdr.GetString(13);
				else
					_orderno = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(12))
					_distanation = tmp_rdr.GetString(12);
				else
					_distanation = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(6))
					_client_id = tmp_rdr.GetInt32(6);
				else
					_client_id = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(1))
					id_order = tmp_rdr.GetInt32(1);
				else
					id_order = 0;

			}
			tmp_rdr.Close();
			if (_client_id > 0)
				Client = new ClientInfo(_client_id, db_connection);
			else
				Client = null;
            tmp_cmd.CommandText = "SELECT dbo.orderbody.id_orderbody, dbo.orderbody.id_order, dbo.orderbody.id_good, dbo.orderbody.guid, dbo.orderbody.del, dbo.orderbody.quantity, dbo.orderbody.actual_quantity, dbo.orderbody.sign, dbo.orderbody.price, dbo.good.name, dbo.orderbody.datework, dbo.orderbody.id_user_work, dbo.orderbody.name_work, dbo.orderbody.defect_quantity, dbo.orderbody.id_user_defect, dbo.orderbody.user_defect, dbo.orderbody.tech_defect, dbo.orderbody.name_add, dbo.orderbody.comment FROM dbo.orderbody INNER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (((dbo.orderbody.del <> 1) OR (ISNULL(dbo.orderbody.del, 0) = 0)) AND ([id_order] = " + this.Id + ") AND ((dbo.orderbody.actual_quantity <> 0) OR (dbo.orderbody.quantity <> 0))) ORDER BY dbo.orderbody.id_order, dbo.orderbody.id_orderbody";
			tmp_cmd.CommandTimeout = 9000;
			tmp_rdr = tmp_cmd.ExecuteReader();

			OrderBody = new DataTable("Order");

			OrderBody.Columns.Add("check", System.Type.GetType("System.Boolean"));			// 0
			OrderBody.Columns.Add("Name", System.Type.GetType("System.String"));			// 1
			OrderBody.Columns.Add("GoodId", System.Type.GetType("System.String"));			// 2
			OrderBody.Columns.Add("Count", System.Type.GetType("System.Decimal"));			// 3
			OrderBody.Columns.Add("ActualCount", System.Type.GetType("System.Decimal"));	// 4
			OrderBody.Columns.Add("Price", System.Type.GetType("System.Decimal"));			// 5
			OrderBody.Columns.Add("Sum", System.Type.GetType("System.Decimal"));			// 6
			OrderBody.Columns.Add("ActualSum", System.Type.GetType("System.Decimal"));		// 7
			OrderBody.Columns.Add("Sign", System.Type.GetType("System.String"));			// 8
			OrderBody.Columns.Add("old", System.Type.GetType("System.Boolean"));			// 9
			OrderBody.Columns.Add("guid", System.Type.GetType("System.String"));			// 10
			OrderBody.Columns.Add("datework", System.Type.GetType("System.String"));		// 11 10
			OrderBody.Columns.Add("id_user_work", System.Type.GetType("System.Int32"));		// 12 11
			OrderBody.Columns.Add("name_work", System.Type.GetType("System.String"));		// 13 12
			OrderBody.Columns.Add("defect_quantity", System.Type.GetType("System.Decimal"));// 14 13
			OrderBody.Columns.Add("id_user_defect", System.Type.GetType("System.Int32"));	// 15 14
			OrderBody.Columns.Add("user_defect", System.Type.GetType("System.String"));		// 16 15
            OrderBody.Columns.Add("tech_defect", System.Type.GetType("System.Int32"));		// 17 16
            OrderBody.Columns.Add("name_add", System.Type.GetType("System.String"));		// 18 17
            OrderBody.Columns.Add("comment", System.Type.GetType("System.String"));		    // 19 18

			/*
			 * 0  [id_orderbody], 
			 * 1  [id_order], 
			 * 2  [id_good], 
			 * 3  [guid], 
			 * 4  [del], 
			 * 5  [quantity], 
			 * 6  [actual_quantity], 
			 * 7  [sign], 
			 * 8  [price]
			 * 9  [name]
			 * 10 datework, 
			 * 11 id_user_work, 
			 * 12 name_work, 
			 * 13 defect_quantity, 
			 * 14 id_user_defect, 
			 * 15 user_defect, 
			 * 16 tech_defect
			 */

			while (tmp_rdr.Read())
			{
				object[] r = new object[20];
				r[0] = false;
				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(9))
					r[1] = tmp_rdr.GetString(9);
				else
					r[1] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(2))
					r[2] = tmp_rdr.GetString(2);
				else
					r[2] = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(5))
					r[3] = tmp_rdr.GetDecimal(5);
				else
					r[3] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(6))
					r[4] = tmp_rdr.GetDecimal(6);
				else
					r[4] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(8))
					r[5] = tmp_rdr.GetDecimal(8);
				else
					r[5] = 0;

				///////////////////////////////////
				r[6] = (decimal)r[3] * (decimal)r[5];

				///////////////////////////////////
				r[7] = (decimal)r[4] * (decimal)r[5];

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(7))
					r[8] = tmp_rdr.GetString(7);
				else
					r[8] = 0;

				///////////////////////////////////
				r[9] = true;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(3))
					r[10] = tmp_rdr.GetString(3);
				else
					r[10] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(10))
					r[11] = tmp_rdr.GetDateTime(10);
				else
					r[11] = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(11))
					r[12] = tmp_rdr.GetInt32(11);
				else
					r[12] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(12))
					r[13] = tmp_rdr.GetString(12);
				else
					r[13] = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(13))
					r[14] = tmp_rdr.GetDecimal(13);
				else
					r[14] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(14))
					r[15] = tmp_rdr.GetInt32(14);
				else
					r[15] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(15))
					r[16] = tmp_rdr.GetString(15);
				else
					r[16] = "";

                ///////////////////////////////////
                if (!tmp_rdr.IsDBNull(16))
                    r[17] = tmp_rdr.GetInt32(16);
                else
                    r[17] = 0;

                ///////////////////////////////////
                if (!tmp_rdr.IsDBNull(17))
                    r[18] = tmp_rdr.GetString(17).Trim();
                else
                    r[18] = "";

                ///////////////////////////////////
                if (!tmp_rdr.IsDBNull(18))
                    r[19] = tmp_rdr.GetString(18).Trim();
                else
                    r[19] = "";

                OrderBody.Rows.Add(r);

			}
			tmp_rdr.Close();



		}

        public bool IsSame(OrderInfo order)
        {
            bool rezult = true;
            try
            {
                if (this.Distanation != order.Distanation)
                    rezult = false;
                else if (this.Orderno != order.Orderno)
                    rezult = false;
                else if (this.Timeout.ToString() != order.Timeout.ToString())
                    rezult = false;
                else if (this.OrderBody.Rows.Count != order.OrderBody.Rows.Count)
                    rezult = false;
                else if (this.OrderBody.Columns.Count != order.OrderBody.Columns.Count)
                    rezult = false;
                else if (this.FinalPayment != order.FinalPayment)
                    rezult = false;
                else if (this.Bonus != order.Bonus)
                    rezult = false;

                if(rezult)
                {
                    for(int i = 0; (i < this.OrderBody.Columns.Count || !rezult); i++)
                    {
                        for (int j = 0; (j < this.OrderBody.Rows.Count || !rezult); j++)
                        {
                            if(this.OrderBody.Rows[j][i].ToString() != order.OrderBody.Rows[j][i].ToString())
                                rezult = false;
                        }
                    }
                }

                if(rezult)
                {
                    if ((this.Discont != null) && (order.Discont != null))
                    {
                        if (this.Discont.Bonus != order.Discont.Bonus)
                            rezult = false;
                        else if (this.Discont.Discserv != order.Discont.Discserv)
                            rezult = false;
                        else if (this.Discont.Name_dcard != order.Discont.Name_dcard)
                            rezult = false;
                        else if (this.Discont.Id_dcard != order.Discont.Id_dcard)
                            rezult = false;
                    }
                    else if ((this.Discont != null) && (order.Discont == null))
                        rezult = false;
                    else if ((this.Discont == null) && (order.Discont != null))
                        rezult = false;
                }
            }
            catch (Exception)
            {
                rezult = false;
            }
            return rezult;
        }

		public OrderInfo(SqlConnection db_connection, string orderno)
		{
			this.db_connection = db_connection;

			SqlCommand tmp_cmd = new SqlCommand("SELECT [id_order] as [id], [id_order], [id_user_accept], [id_user_operator], [id_user_designer], [id_user_delivery], [id_client], [guid], [del], [name_accept], [name_operator], [name_delivery], [status], [number], [input_date], [expected_date], [output_date], [advanced_payment], [final_payment], [discont_percent], [discont_code], [preview], [comment], [crop], [type], [bonus], [order_price], [ptype] FROM [order] WHERE [number] = '" + orderno + "'", db_connection);
			SqlDataReader tmp_rdr = tmp_cmd.ExecuteReader();
			if (tmp_rdr.Read())
			{
				/*
				 * 1  [id_order], 
				 * 2  [id_user_accept], 
				 * 3  [id_user_operator], 
				 * 4  [id_user_designer], 
				 * 5  [id_user_delivery], 
				 * 6!  [id_client], 
				 * 7  [guid], 
				 * 8  [del], 
				 * 9  [name_accept], 
				 * 10 [name_operator], 
				 * 11 [name_delivery], 
				 *! 12 [status], 
				 *! 13 [number], 
				 *! 14 [input_date], 
				 *! 15 [expected_date], 
				 * 16 [output_date], 
				 *! 17 [advanced_payment], 
				 *! 18 [final_payment], 
				 *! 19 [discont_percent], 
				 *! 20 [discont_code], 
				 *! 21 [preview], 
				 *! 22 [comment]
				 *! 23 [crop]
				 *! 24 [type]
				 *  25 [bonus]
                 *  26 order_price
				 *  27 ptype
				 */
				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(9))
					_name_accept = tmp_rdr.GetString(9);
				else
					_name_accept = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(23))
					_crop = tmp_rdr.GetInt32(23);
				else
					_crop = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(24))
					_type = tmp_rdr.GetInt32(24);
				else
					_type = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(22))
					_comment = tmp_rdr.GetString(22);
				else
					_comment = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(21))
					if (tmp_rdr.GetBoolean(21))
						_preview = 1;
					else
						_preview = 0;
				else
					_preview = 0;

				///////////////////////////////////
				if ((!tmp_rdr.IsDBNull(20)) && (!tmp_rdr.IsDBNull(20)))
				{
					Discont = new DiscontInfo(tmp_rdr.GetString(20), db_connection);
					Discont.Id_dcard = 777777777;
				}
					//Discont = new DiscontInfo(tmp_rdr.GetString(20), tmp_rdr.GetDecimal(19));
				else
					Discont = new DiscontInfo("", 0);

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(18))
					_finalPayment = tmp_rdr.GetDecimal(18);
				else
					_finalPayment = 0;

                ///////////////////////////////////
                if (!tmp_rdr.IsDBNull(25))
                    _bonus = tmp_rdr.GetDecimal(25);
                else
                    _bonus = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(26))
					_order_price = tmp_rdr.GetDecimal(26);
				else
					_order_price = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(27))
					_ptype = tmp_rdr.GetInt32(27);
				else
					_ptype = -1;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(17))
					_advancedPayment = tmp_rdr.GetDecimal(17);
				else
					_advancedPayment = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(14))
					_datein = tmp_rdr.GetDateTime(14).ToShortDateString();
				else
					_datein = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(14))
					_timein = tmp_rdr.GetDateTime(14).ToShortTimeString();
				else
					_timein = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(15))
					_dateout = tmp_rdr.GetDateTime(15).ToShortDateString();
				else
					_dateout = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(15))
					_timeout = tmp_rdr.GetDateTime(15).ToShortTimeString();
				else
					_timeout = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(13))
					_orderno = tmp_rdr.GetString(13);
				else
					_orderno = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(12))
					_distanation = tmp_rdr.GetString(12);
				else
					_distanation = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(6))
					_client_id = tmp_rdr.GetInt32(6);
				else
					_client_id = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(1))
					this._id = tmp_rdr.GetInt32(1);
				else
					this._id = 0;

			}
			tmp_rdr.Close();
			if (_client_id > 0)
				Client = new ClientInfo(_client_id, db_connection);
			else
				Client = null;
            tmp_cmd.CommandText = "SELECT dbo.orderbody.id_orderbody, dbo.orderbody.id_order, dbo.orderbody.id_good, dbo.orderbody.guid, dbo.orderbody.del, dbo.orderbody.quantity, dbo.orderbody.actual_quantity, dbo.orderbody.sign, dbo.orderbody.price, dbo.good.name, dbo.orderbody.datework, dbo.orderbody.id_user_work, dbo.orderbody.name_work, dbo.orderbody.defect_quantity, dbo.orderbody.id_user_defect, dbo.orderbody.user_defect, dbo.orderbody.tech_defect, dbo.orderbody.name_add, dbo.orderbody.comment FROM dbo.orderbody INNER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (((dbo.orderbody.del <> 1) OR (ISNULL(dbo.orderbody.del, 0) = 0)) AND ([id_order] = " + this.Id + ") AND ((dbo.orderbody.actual_quantity <> 0) OR (dbo.orderbody.quantity <> 0))) ORDER BY dbo.orderbody.id_order, dbo.orderbody.id_orderbody";
			tmp_rdr = tmp_cmd.ExecuteReader();

			OrderBody = new DataTable("Order");

			OrderBody.Columns.Add("check", System.Type.GetType("System.Boolean"));			// 0
			OrderBody.Columns.Add("Name", System.Type.GetType("System.String"));			// 1
			OrderBody.Columns.Add("GoodId", System.Type.GetType("System.String"));			// 2
			OrderBody.Columns.Add("Count", System.Type.GetType("System.Decimal"));			// 3
			OrderBody.Columns.Add("ActualCount", System.Type.GetType("System.Decimal"));	// 4
			OrderBody.Columns.Add("Price", System.Type.GetType("System.Decimal"));			// 5
			OrderBody.Columns.Add("Sum", System.Type.GetType("System.Decimal"));			// 6
			OrderBody.Columns.Add("ActualSum", System.Type.GetType("System.Decimal"));		// 7
			OrderBody.Columns.Add("Sign", System.Type.GetType("System.String"));			// 8
			OrderBody.Columns.Add("old", System.Type.GetType("System.Boolean"));			// 9
			OrderBody.Columns.Add("guid", System.Type.GetType("System.String"));			// 10
			OrderBody.Columns.Add("datework", System.Type.GetType("System.String"));		// 11 10
			OrderBody.Columns.Add("id_user_work", System.Type.GetType("System.Int32"));		// 12 11
			OrderBody.Columns.Add("name_work", System.Type.GetType("System.String"));		// 13 12
			OrderBody.Columns.Add("defect_quantity", System.Type.GetType("System.Decimal"));// 14 13
			OrderBody.Columns.Add("id_user_defect", System.Type.GetType("System.Int32"));	// 15 14
			OrderBody.Columns.Add("user_defect", System.Type.GetType("System.String"));		// 16 15
			OrderBody.Columns.Add("tech_defect", System.Type.GetType("System.Int32"));		// 17 16
            OrderBody.Columns.Add("name_add", System.Type.GetType("System.String"));		// 18 17
            OrderBody.Columns.Add("comment", System.Type.GetType("System.String"));		    // 19 18

			/*
			 * 0  [id_orderbody], 
			 * 1  [id_order], 
			 * 2  [id_good], 
			 * 3  [guid], 
			 * 4  [del], 
			 * 5  [quantity], 
			 * 6  [actual_quantity], 
			 * 7  [sign], 
			 * 8  [price]
			 * 9  [name]
			 * 10 datework, 
			 * 11 id_user_work, 
			 * 12 name_work, 
			 * 13 defect_quantity, 
			 * 14 id_user_defect, 
			 * 15 user_defect, 
			 * 16 tech_defect
			 */

			while (tmp_rdr.Read())
			{
				object[] r = new object[20];
				r[0] = false;
				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(9))
					r[1] = tmp_rdr.GetString(9);
				else
					r[1] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(2))
					r[2] = tmp_rdr.GetString(2);
				else
					r[2] = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(5))
					r[3] = tmp_rdr.GetDecimal(5);
				else
					r[3] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(6))
					r[4] = tmp_rdr.GetDecimal(6);
				else
					r[4] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(8))
					r[5] = tmp_rdr.GetDecimal(8);
				else
					r[5] = 0;

				///////////////////////////////////
				r[6] = (decimal)r[3] * (decimal)r[5];

				///////////////////////////////////
				r[7] = (decimal)r[4] * (decimal)r[5];

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(7))
					r[8] = tmp_rdr.GetString(7);
				else
					r[8] = 0;

				///////////////////////////////////
				r[9] = true;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(3))
					r[10] = tmp_rdr.GetString(3);
				else
					r[10] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(10))
					r[11] = tmp_rdr.GetDateTime(10);
				else
					r[11] = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(11))
					r[12] = tmp_rdr.GetInt32(11);
				else
					r[12] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(12))
					r[13] = tmp_rdr.GetString(12);
				else
					r[13] = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(13))
					r[14] = tmp_rdr.GetDecimal(13);
				else
					r[14] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(14))
					r[15] = tmp_rdr.GetInt32(14);
				else
					r[15] = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(15))
					r[16] = tmp_rdr.GetString(15);
				else
					r[16] = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(16))
					r[17] = tmp_rdr.GetInt32(16);
				else
					r[17] = 0;

                ///////////////////////////////////
                if (!tmp_rdr.IsDBNull(17))
                    r[18] = tmp_rdr.GetString(17).Trim();
                else
                    r[18] = "";

                ///////////////////////////////////
                if (!tmp_rdr.IsDBNull(18))
                    r[19] = tmp_rdr.GetString(18).Trim();
                else
                    r[19] = "";

				OrderBody.Rows.Add(r);

			}
			tmp_rdr.Close();



		}

		public OrderInfo(SqlConnection db_connection, string orderno, bool forPrint)
		{
			this.db_connection = db_connection;

			SqlCommand tmp_cmd = new SqlCommand("SELECT [id_order] as [id], [id_order], [id_user_accept], [id_user_operator], [id_user_designer], [id_user_delivery], [id_client], [guid], [del], [name_accept], [name_operator], [name_delivery], [status], [number], [input_date], [expected_date], [output_date], [advanced_payment], [final_payment], [discont_percent], [discont_code], [preview], [comment], [crop], [type], [bonus], [order_price], [ptype] FROM [order] WHERE [number] = '" + orderno + "'", db_connection);
			SqlDataReader tmp_rdr = tmp_cmd.ExecuteReader();
			if (tmp_rdr.Read())
			{
				/*
				 * 1  [id_order], 
				 * 2  [id_user_accept], 
				 * 3  [id_user_operator], 
				 * 4  [id_user_designer], 
				 * 5  [id_user_delivery], 
				 * 6!  [id_client], 
				 * 7  [guid], 
				 * 8  [del], 
				 * 9  [name_accept], 
				 * 10 [name_operator], 
				 * 11 [name_delivery], 
				 *! 12 [status], 
				 *! 13 [number], 
				 *! 14 [input_date], 
				 *! 15 [expected_date], 
				 * 16 [output_date], 
				 *! 17 [advanced_payment], 
				 *! 18 [final_payment], 
				 *! 19 [discont_percent], 
				 *! 20 [discont_code], 
				 *! 21 [preview], 
				 *! 22 [comment]
				 *! 23 [crop]
				 *! 24 [type]
				 *  25 [bonus]
                 *  26 order_price
				 *  27 ptype
				 */
				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(9))
					_name_accept = tmp_rdr.GetString(9);
				else
					_name_accept = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(23))
					_crop = tmp_rdr.GetInt32(23);
				else
					_crop = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(24))
					_type = tmp_rdr.GetInt32(24);
				else
					_type = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(22))
					_comment = tmp_rdr.GetString(22);
				else
					_comment = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(21))
					if (tmp_rdr.GetBoolean(21))
						_preview = 1;
					else
						_preview = 0;
				else
					_preview = 0;

				///////////////////////////////////
				if ((!tmp_rdr.IsDBNull(20)) && (!tmp_rdr.IsDBNull(20)))
				{
					Discont = new DiscontInfo(tmp_rdr.GetString(20), db_connection);
					Discont.Id_dcard = 777777777;
				}
				//Discont = new DiscontInfo(tmp_rdr.GetString(20), tmp_rdr.GetDecimal(19));
				else
					Discont = new DiscontInfo("", 0);

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(18))
					_finalPayment = tmp_rdr.GetDecimal(18);
				else
					_finalPayment = 0;

                ///////////////////////////////////
                if (!tmp_rdr.IsDBNull(25))
                    _bonus = tmp_rdr.GetDecimal(25);
                else
                    _bonus = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(26))
					_order_price = tmp_rdr.GetDecimal(26);
				else
					_order_price = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(27))
					_ptype = tmp_rdr.GetInt32(27);
				else
					_ptype = -1;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(17))
					_advancedPayment = tmp_rdr.GetDecimal(17);
				else
					_advancedPayment = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(14))
					_datein = tmp_rdr.GetDateTime(14).ToShortDateString();
				else
					_datein = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(14))
					_timein = tmp_rdr.GetDateTime(14).ToShortTimeString();
				else
					_timein = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(15))
					_dateout = tmp_rdr.GetDateTime(15).ToShortDateString();
				else
					_dateout = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(15))
					_timeout = tmp_rdr.GetDateTime(15).ToShortTimeString();
				else
					_timeout = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(13))
					_orderno = tmp_rdr.GetString(13);
				else
					_orderno = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(12))
					_distanation = tmp_rdr.GetString(12);
				else
					_distanation = "";

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(6))
					_client_id = tmp_rdr.GetInt32(6);
				else
					_client_id = 0;

				///////////////////////////////////
				if (!tmp_rdr.IsDBNull(1))
					this._id = tmp_rdr.GetInt32(1);
				else
					this._id = 0;

			}
			tmp_rdr.Close();
			if (_client_id > 0)
				Client = new ClientInfo(_client_id, db_connection);
			else
				Client = null;
            tmp_cmd.CommandText = "SELECT dbo.orderbody.id_orderbody, dbo.orderbody.id_order, dbo.orderbody.id_good, dbo.orderbody.guid, dbo.orderbody.del, dbo.orderbody.quantity, dbo.orderbody.actual_quantity, dbo.orderbody.sign, dbo.orderbody.price, dbo.good.name, dbo.orderbody.datework, dbo.orderbody.id_user_work, dbo.orderbody.name_work, dbo.orderbody.defect_quantity, dbo.orderbody.id_user_defect, dbo.orderbody.user_defect, dbo.orderbody.tech_defect, dbo.orderbody.name_add, dbo.orderbody.comment FROM dbo.orderbody INNER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (((dbo.orderbody.del <> 1) OR (ISNULL(dbo.orderbody.del, 0) = 0)) AND ([id_order] = " + this.Id + ") AND ((dbo.orderbody.actual_quantity <> 0) OR (dbo.orderbody.quantity <> 0))) ORDER BY dbo.orderbody.id_order, dbo.orderbody.id_orderbody";
			//tmp_rdr = tmp_cmd.ExecuteReader();
			tmp_cmd.CommandTimeout = 9000;
			SqlDataAdapter da = new SqlDataAdapter(tmp_cmd);
			OrderBody = new DataTable("Order");
			da.Fill(OrderBody);

		}

		// Сохраняем из визарда
		public bool SaveEasyOrder()
		{
			SqlCommand db_command = new SqlCommand();
			//SqlTransaction db_transaction;
			//db_transaction = db_connection.BeginTransaction("SaveEasyOrder");
			db_command.Connection = db_connection;
			//db_command.Transaction = db_transaction;

			try
			{

				string Newguid = System.Guid.NewGuid().ToString();

				string yin, min, din, yout, mout, dout;

				yin = DateTime.Now.Year.ToString();

				if (DateTime.Now.Month < 10)
					min = "0" + DateTime.Now.Month.ToString();
				else
					min = DateTime.Now.Month.ToString();

				if (DateTime.Now.Day < 10)
					din = "0" + DateTime.Now.Day.ToString();
				else
					din = DateTime.Now.Day.ToString();

				yout = DateTime.Parse(Dateout).Year.ToString();

				if (DateTime.Parse(Dateout).Month < 10)
					mout = "0" + DateTime.Parse(Dateout).Month.ToString();
				else
					mout = DateTime.Parse(Dateout).Month.ToString();

				if (DateTime.Parse(Dateout).Day < 10)
					dout = "0" + DateTime.Parse(Dateout).Day.ToString();
				else
					dout = DateTime.Parse(Dateout).Day.ToString();

				string Discont_discserv = "";
				string Discont_Code_dcard = "";
				if (Discont == null)
				{
					Discont_discserv = "0";
					Discont_Code_dcard = "";
				}
				else
				{
					Discont_discserv = Discont.Discserv.ToString();
					Discont_Code_dcard = Discont.Code_dcard;
				}


                string query = "INSERT INTO [order] ([id_user_accept], [id_client], [guid], [name_accept], [status], [number], [input_date], [expected_date], [advanced_payment], [final_payment], [discont_percent], [discont_code], [preview], [crop], [type], [bonus], [order_price], [ptype]) VALUES (" + Usr.Id_user + ", " + Client.Id + ", '" + Newguid + "', '" + Usr.Name + "', '" + Distanation + "', '" + Orderno + "', CONVERT(DATETIME, '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() + "', 120), CONVERT(DATETIME, '" + yout + "." + mout + "." + dout + " " + Timeout + "', 120), " + AdvancedPayment.ToString().Replace(",", ".") + ", " + FinalPayment.ToString().Replace(",", ".") + ", " + Discont_discserv.Replace(",", ".") + ", '" + Discont_Code_dcard + "', " + Preview.ToString() + ", " + Crop + ", " + Type + ", " + Bonus.ToString().Replace(",", ".") + ", " + Order_price.ToString().Replace(",", ".") + ", 0); \n";
				db_command.CommandText = query;
				db_command.ExecuteNonQuery();

				if (AdvancedPayment > 0)
				{
					query = "INSERT INTO [payments] ([guid], [date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way]) VALUES('" + System.Guid.NewGuid().ToString() + "', CONVERT(DATETIME, '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() + "', 120), '" + DateTime.Now.ToShortTimeString() + "', " + Usr.Id_user + ", '" + Usr.Name + "', '" + Orderno + "', " + AdvancedPayment.ToString().Replace(",", ".") + ", 1, 'Автоматически зачисленная предоплата', 1); \n";
					db_command.CommandText = query;
					db_command.ExecuteNonQuery();
				}

				if (FinalPayment > 0)
				{
					query = "INSERT INTO [payments] ([guid], [date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way]) VALUES('" + System.Guid.NewGuid().ToString() + "', CONVERT(DATETIME, '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() + "', 120), '" + DateTime.Now.ToShortTimeString() + "', " + Usr.Id_user + ", '" + Usr.Name + "', '" + Orderno + "', " + FinalPayment.ToString().Replace(",", ".") + ", 2, 'Автоматически зачисленная сумма', 1); ";
					db_command.CommandText = query;
					db_command.ExecuteNonQuery();
				}

				query = "SELECT [id_order] FROM [order] WHERE RTRIM([guid]) = '" + Newguid + "'";
				db_command.CommandText = query;
				SqlDataReader db_reader = db_command.ExecuteReader();
				if (db_reader.Read())
				{
					int id_order = db_reader.GetInt32(0);
					db_reader.Close();
					for (int i = 0; i < OrderBody.Rows.Count; i++)
					{
						string tmp_id_good = OrderBody.Rows[i][4].ToString();
						decimal tmp_count_good = decimal.Parse(OrderBody.Rows[i][2].ToString());
						if (tmp_count_good > 0)
						{
							/*
							SqlCommand db_command_p = new SqlCommand("SELECT [id_good], [id_category], [amount] FROM [vwPriceFull] WHERE [id_good] = '" + tmp_id_good + "' AND [id_category] = " + Client.Id_category, db_connection);
							db_reader = db_command_p.ExecuteReader();
							db_reader.Read();
							decimal price = db_reader.GetDecimal(2);
							db_reader.Close();
							 */
							SqlCommand db_command_p = new SqlCommand("spAdvPrice", db_connection);
							db_command_p.Parameters.Add(new SqlParameter("@id_good", tmp_id_good));
							db_command_p.Parameters.Add(new SqlParameter("@id_category", Client.Id_category));
							db_command_p.Parameters.Add(new SqlParameter("@threshold", tmp_count_good));
							db_command_p.CommandType = CommandType.StoredProcedure;
							decimal price = 0;
							if (tmp_count_good > 0)
							{
								try
								{
									price = (decimal)db_command_p.ExecuteScalar();
								}
								catch
								{
									price = 0;
								}
							}


							query = "INSERT INTO [orderbody] ([id_order], [id_good], [guid], [quantity], [actual_quantity], [sign], [price], [id_user_add], [name_add]) VALUES ('" + id_order + "', '" + tmp_id_good + "', '" + System.Guid.NewGuid().ToString() + "', " + tmp_count_good.ToString().Replace(",", ".") + ", 0, '+', " + price.ToString().Replace(",", ".") + ", " + Usr.Id_user.ToString() + ", '" + Usr.Name + "')";
							db_command.CommandText = query;
							db_command.ExecuteNonQuery();
						}
					}
					//db_transaction.Commit();
					return true;
				}
				else
				{
					//db_transaction.Rollback("SaveEasyOrder");
					return false;
				}
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				_err = ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace;
				return false;
				//db_transaction.Rollback("SaveEasyOrder");
			}

		}


		// Сохраняем из основной формы
		public bool SaveAdvOrder()
		{
			return this.SaveAdvOrder(false);
		}

		// Сохраняем из основной формы
		public bool SaveAdvOrder(bool quick)
		{
			SqlCommand db_command = new SqlCommand();
			db_command.CommandTimeout = 9000;
			db_command.Connection = db_connection;

			try
			{

				string Newguid = System.Guid.NewGuid().ToString();

				string yin, min, din, yout, mout, dout;

				yin = DateTime.Now.Year.ToString();

				if (DateTime.Now.Month < 10)
					min = "0" + DateTime.Now.Month.ToString();
				else
					min = DateTime.Now.Month.ToString();

				if (DateTime.Now.Day < 10)
					din = "0" + DateTime.Now.Day.ToString();
				else
					din = DateTime.Now.Day.ToString();

				yout = DateTime.Parse(Dateout).Year.ToString();

				if (DateTime.Parse(Dateout).Month < 10)
					mout = "0" + DateTime.Parse(Dateout).Month.ToString();
				else
					mout = DateTime.Parse(Dateout).Month.ToString();

				if (DateTime.Parse(Dateout).Day < 10)
					dout = "0" + DateTime.Parse(Dateout).Day.ToString();
				else
					dout = DateTime.Parse(Dateout).Day.ToString();

				string Discont_discserv = "";
				string Discont_Code_dcard = "";
				if (Discont == null)
				{
					Discont_discserv = "0";
					Discont_Code_dcard = "";
				}
				else
				{
					Discont_discserv = Discont.Discserv.ToString();
					Discont_Code_dcard = Discont.Code_dcard;
				}


                string query = "INSERT INTO [order] ([id_user_accept], [id_client], [guid], [name_accept], [status], [number], [input_date], [expected_date], [advanced_payment], [final_payment], [discont_percent], [discont_code], [preview], [comment], [crop], [type], [bonus], [order_price], [ptype]) VALUES (" + Usr.Id_user + ", " + Client.Id + ", '" + Newguid + "', '" + Usr.Name + "', '" + Distanation + "', '" + Orderno + "', CONVERT(DATETIME, '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() + "', 120), CONVERT(DATETIME, '" + yout + "." + mout + "." + dout + " " + Timeout + "', 120), " + AdvancedPayment.ToString().Replace(",", ".") + ", " + FinalPayment.ToString().Replace(",", ".") + ", " + Discont_discserv.Replace(",", ".") + ", '" + Discont_Code_dcard + "', " + Preview.ToString() + ", '" + Comment.ToString() + "', " + Crop + ", " + Type + ", " + Bonus.ToString().Replace(",", ".") + ", " + Order_price.ToString().Replace(",", ".") + ", " + PType + ")";
				db_command.CommandText = query;
				db_command.ExecuteNonQuery();

				if (AdvancedPayment > 0)
				{
					query = "INSERT INTO [payments] ([guid], [date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way]) VALUES('" + System.Guid.NewGuid().ToString() + "', CONVERT(DATETIME, '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() + "', 120), '" + DateTime.Now.ToShortTimeString() + "', " + Usr.Id_user + ", '" + Usr.Name + "', '" + Orderno + "', " + AdvancedPayment.ToString().Replace(",", ".") + ", 1, 'Автоматически зачисленная предоплата', 1)";
					db_command.CommandText = query;
					db_command.ExecuteNonQuery();
				}

				if (FinalPayment > 0)
				{
					query = "INSERT INTO [payments] ([guid], [date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way]) VALUES('" + System.Guid.NewGuid().ToString() + "', CONVERT(DATETIME, '" + yin + "." + min + "." + din + " " + DateTime.Now.ToShortTimeString() + "', 120), '" + DateTime.Now.ToShortTimeString() + "', " + Usr.Id_user + ", '" + Usr.Name + "', '" + Orderno + "', " + FinalPayment.ToString().Replace(",", ".") + ", 2, 'Автоматически зачисленная сумма', 1)";
					db_command.CommandText = query;
					db_command.ExecuteNonQuery();
				}

				query = "SELECT [id_order] FROM [order] WHERE RTRIM([guid]) = '" + Newguid + "'";
				db_command.CommandText = query;
				SqlDataReader db_reader = db_command.ExecuteReader();
			    string _mes_info = "";
				if (db_reader.Read())
				{
					int id_order = db_reader.GetInt32(0);
					db_reader.Close();
					for (int i = 0; i < OrderBody.Rows.Count; i++)
					{
						string tmp_id_good = OrderBody.Rows[i][5].ToString();
						decimal tmp_count_good = decimal.Parse(OrderBody.Rows[i][2].ToString());
					    string tmp_comment = OrderBody.Rows[i]["comment"].ToString();
						if (tmp_count_good > 0)
						{
							decimal price = decimal.Parse(OrderBody.Rows[i][3].ToString());
                            string guid1 = System.Guid.NewGuid().ToString();
							query =
								"INSERT INTO [orderbody] ([id_order], [id_good], [guid], [quantity], [actual_quantity], [sign], [price], [id_user_add], [name_add], [comment]" + (quick ? ",[datework], [id_user_work], [name_work]" : "") + ") VALUES ('" +
								id_order + "', '" + tmp_id_good + "', '" + guid1 + "', " +
								tmp_count_good.ToString().Replace(",", ".") +
								", " + (quick ? tmp_count_good.ToString().Replace(",", ".") : "0") + ", '+', " + price.ToString().Replace(",", ".") + ", " + Usr.Id_user.ToString() + ", '" + Usr.Name + "', '" + tmp_comment + "'" + (quick ? ", getdate(), " + Usr.Id_user.ToString() + ", '" + Usr.Name + "' " : "") + ")";
                            try
                            {
                                List<string> s = (List<string>)OrderBody.Rows[i][9];
                                if (s.Count > 0)
                                {
                                    s[0] = "\r\nGUID: " + guid1 + "" + s[0];
                                    OrderBody.Rows[i][9] = s;
                                }
                            }
                            catch
                            {
                            }
							db_command.CommandText = query;
							db_command.ExecuteNonQuery();
						}
					}
				    string body = "$$";
                    for (int i = 0; i < OrderBody.Rows.Count; i++)
                    {
                        body += "false" + "|" +
                                OrderBody.Rows[i][1].ToString().Trim() + "|" +
                                OrderBody.Rows[i][2].ToString().Trim() + "|" +
                                "0" + "|" +
                                OrderBody.Rows[i][3].ToString().Trim() + "|" +
                                OrderBody.Rows[i][4].ToString().Trim() + "|" +
                                "0" + "|" +
                                OrderBody.Rows[i][11].ToString().Trim() + "|" +
                                "" + "|" +
                                "" + "|" +
                                "" + "|" +
                                "";
                        body += "#";
                    }
                    body = body.Substring(0, body.Length - 1);
				    body += "$$" + AdvancedPayment + "$$" + FinalPayment + "$$" + Bonus;
					db_command.CommandText =
						"INSERT INTO [dbo].[orderevent] ([id_order], [event_user], [event_status], [event_point], [event_text]) VALUES (" +
						id_order + ", '" + Usr.Name.Trim() + "', '" + Distanation.Trim() + "', '" + prop.Order_prefics.Trim() +
						"', '" + (quick ? "Сохранен новый моментальный заказ" : "Сохранен новый заказ") + body + "')";
                    db_command.ExecuteNonQuery();
                    if (AdvancedPayment > 0)
                    {
                        db_command.CommandText =
                            "INSERT INTO [dbo].[orderevent] ([id_order], [event_user], [event_status], [event_point], [event_text]) VALUES (" +
                            id_order + ", '" + Usr.Name.Trim() + "', '" + Distanation.Trim() + "', '" + prop.Order_prefics.Trim() +
                            "', 'Принята предоплата " + AdvancedPayment.ToString().Replace(",", ".") + "" + body + "')";
                        db_command.ExecuteNonQuery();

                    }

					return true;
				}
				else
				{
					return false;
				}
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				_err = ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace;
				return false;
				//db_transaction.Rollback("SaveEasyOrder");
			}

		}

		// Обновляем из основной формы
		public bool UpdateOrder(UserInfo Usr, bool updateFinalPayment, bool getAdvPayment, bool updateWorkDate)
		{
			SqlCommand db_command = new SqlCommand();
			db_command.CommandTimeout = 9000;
			db_command.Connection = db_connection;

			try
			{
                
				string yin, min, din, yout, mout, dout, yn, mn, dn;

				yin = DateTime.Parse(Datein).Year.ToString();

				if (DateTime.Now.Month < 10)
					min = "0" + DateTime.Now.Month.ToString();
				else
					min = DateTime.Now.Month.ToString();

				if (DateTime.Now.Day < 10)
					din = "0" + DateTime.Now.Day.ToString();
				else
					din = DateTime.Now.Day.ToString();

				yout = DateTime.Parse(Dateout).Year.ToString();

				if (DateTime.Parse(Dateout).Month < 10)
					mout = "0" + DateTime.Parse(Dateout).Month.ToString();
				else
					mout = DateTime.Parse(Dateout).Month.ToString();

				if (DateTime.Parse(Dateout).Day < 10)
					dout = "0" + DateTime.Parse(Dateout).Day.ToString();
				else
					dout = DateTime.Parse(Dateout).Day.ToString();

				yn = DateTime.Now.Year.ToString();

				if (DateTime.Now.Month < 10)
					mn = "0" + DateTime.Now.Month.ToString();
				else
					mn = DateTime.Now.Month.ToString();

				if (DateTime.Now.Day < 10)
					dn = "0" + DateTime.Now.Day.ToString();
				else
					dn = DateTime.Now.Day.ToString();

				string Discont_discserv = "";
				string Discont_Code_dcard = "";
				if (Discont == null)
				{
					Discont_discserv = "0";
					Discont_Code_dcard = "";
				}
				else
				{
					Discont_discserv = Discont.Discserv.ToString();
					Discont_Code_dcard = Discont.Code_dcard;
				}

				string fullquery = "";
				string finish = "";
				if (Distanation.Trim() == "100000")
				{
					finish += ", [id_user_delivery] = " + Usr.Id_user + ", [name_delivery] = '" + Usr.Name.Trim() +
							  "', [output_date] = CONVERT(DATETIME, '" + yn + "." + mn + "." + dn + " " +
							  DateTime.Now.ToLongTimeString() + "', 120)";
				}
				string query = "UPDATE [order]" +
								" SET [id_client] = " + Client.Id.ToString() + "" +
								" ,[status] = '" + Distanation + "'" +
								" ,[number] = '" + Orderno + "'" +
								" ,[expected_date] = CONVERT(DATETIME, '" + yout + "." + mout + "." + dout + " " + Timeout + "', 120)" +
								" ,[advanced_payment] = " + AdvancedPayment.ToString().Replace(",", ".") +
								" ,[final_payment] = " + FinalPayment.ToString().Replace(",", ".") +
                                " ,[bonus] = " + Bonus.ToString().Replace(",", ".") +
                                " ,[order_price] = " + Order_price.ToString().Replace(",", ".") +
								" ,[discont_percent] = " + Discont_discserv.Replace(",", ".") +
								" ,[discont_code] = '" + Discont_Code_dcard + "'" +
								" ,[preview] = " + Preview +
								" ,[comment] = '" + Comment + "'" +
								" ,[crop] = " + Crop.ToString() +
								" ,[type] = " + this.Type.ToString() +
								" ,[ptype] = " + this.PType.ToString() +
								" , exported = 0" +
								finish +
								" WHERE [id_order] = " + this.Id.ToString() + ";\n";
				fullquery += query;
				//db_command.CommandText = query;
				//db_command.ExecuteNonQuery();

				if (updateFinalPayment)
				{
					query = "INSERT INTO [payments] ([guid], [date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way]) VALUES('" + System.Guid.NewGuid().ToString() + "', CONVERT(DATETIME, '" + yn + "." + mn + "." + dn + " " + DateTime.Now.ToShortTimeString() + "', 120), '" + DateTime.Now.ToShortTimeString() + "', " + Usr.Id_user + ", '" + Usr.Name + "', '" + Orderno + "', " + ((getAdvPayment) ? AdvancedPayment.ToString().Replace(",", ".") : FinalPayment.ToString().Replace(",", ".")) + ", 2, 'Автоматически зачисленная сумма', 1);\n";
					fullquery += query;
					//db_command.CommandText = query;
					//db_command.ExecuteNonQuery();
				}
				for (int i = 0; i < OrderBody.Rows.Count; i++)
				{
					if (!bool.Parse(OrderBody.Rows[i][10].ToString()))
					{
						query = "INSERT INTO [orderbody]" +
								   " ([id_order]" +
								   " ,[id_mashine]" +
								   " ,[id_material]" +
								   " ,[id_good]" +
								   " ,[guid]" +
								   " ,[quantity]" +
								   " ,[actual_quantity]" +
								   " ,[sign]" +
								   " ,[price]" +
								   " ,[datework]" +
								   " ,[id_user_work]" +
								   " ,[name_work]" +
								   " ,[defect_quantity]" +
								   " ,[id_user_defect]" +
								   " ,[user_defect]" +
								   " ,[tech_defect], [id_user_add], [name_add], [comment])" +
								" VALUES" +
								   " (" + this.Id +
								   " ,'" + OrderBody.Rows[i][20].ToString() + "'" +
								   " ,'" + OrderBody.Rows[i][21].ToString() + "'" +
								   " ,'" + OrderBody.Rows[i][2].ToString() + "'" +
								   " ,'" + System.Guid.NewGuid().ToString() + "'" +
								   " ," + OrderBody.Rows[i][3].ToString().Replace(",", ".") +
								   " ," + OrderBody.Rows[i][4].ToString().Replace(",", ".") +
								   " ,'" + OrderBody.Rows[i][9].ToString() + "'" +
								   " ," + OrderBody.Rows[i][5].ToString().Replace(",", ".") +
								   " ,CONVERT(DATETIME, '" + yn + "." + mn + "." + dn + " " + DateTime.Now.ToShortTimeString() + "', 120)" +
								   " ," + OrderBody.Rows[i][14].ToString() + "" +
								   " ,'" + OrderBody.Rows[i][15].ToString() + "'" +
								   " ," + OrderBody.Rows[i][16].ToString().Replace(",", ".") +
								   " ," + OrderBody.Rows[i][17].ToString() + "" +
								   " ,'" + OrderBody.Rows[i][18].ToString() + "'" +
                                   " ," + OrderBody.Rows[i][19].ToString() + ", " + Usr.Id_user.ToString() + ", '" + Usr.Name + "', '" + OrderBody.Rows[i]["comment"].ToString() + "');\n";
						//db_command.CommandText = query;
						//db_command.ExecuteNonQuery();
						fullquery += query;
					}
					else
					{
						query = "DECLARE @LN" + i.ToString() + " nchar(255);\n";
						query += "SET @LN" + i.ToString() + " = (SELECT RTRIM([name_work]) FROM [dbo].[orderbody] WHERE [guid] = '" + OrderBody.Rows[i][12].ToString() + "');\n";
						query += "IF (@LN" + i.ToString() + " = '')\n";
						query += "BEGIN\n";
						query += "\tUPDATE [dbo].[orderbody] SET " +
                                "[price] = " + OrderBody.Rows[i][5].ToString().Replace(",", ".") +
                                " ,[comment] = '" + OrderBody.Rows[i]["comment"].ToString() +  "'" +
								" ,[name_work] = '" + OrderBody.Rows[i]["name_work"].ToString().Trim() + "'" +
								((updateWorkDate) ? " ,[datework] = CONVERT(DATETIME, '" + DateTime.Parse(OrderBody.Rows[i]["datework"].ToString()).Year.ToString("D4") + "." + DateTime.Parse(OrderBody.Rows[i]["datework"].ToString()).Month.ToString("D2") + "." + DateTime.Parse(OrderBody.Rows[i]["datework"].ToString()).Day.ToString("D2") + " " + DateTime.Parse(OrderBody.Rows[i]["datework"].ToString()).ToLongTimeString() + "')" : "") +
								" WHERE [guid] = '" + OrderBody.Rows[i][12].ToString() + "'\n";
						query += "END\n";
						fullquery += query;
					}
				}
				db_command.CommandText = fullquery;
				db_command.ExecuteNonQuery();
				return true;
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				_err = ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace;
				return false;
			}

		}

		// Сохраняем из визарда текстовое описание
		public bool SaveEasyTextFile()
		{
			try
			{
				PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
				string path = prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + Orderno + "\\" + Orderno + ".txt";

				if (!File.Exists(path))
				{
					using (FileStream fs = File.Create(path))
					{
					}

					using (StreamWriter fs = new StreamWriter(path, true, System.Text.Encoding.GetEncoding(1251)))
					{
						fs.WriteLine("! Заказ № " + Orderno);
						fs.WriteLine("# Печать");
						List<string> Files = fso.GetListFilesForProc(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + Orderno + "\\", prop.List_of_files, true, true);
						if (Files.Count > 0)
						{
							foreach (string f in Files)
							{
								fs.WriteLine("+ Файл -> " + f);
							}
						}
						else
						{
							fs.WriteLine("; Файлов не обнаружено!");
						}
					}
				}



				path = prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + Orderno + "\\" + Orderno + ".txt";

				if (!File.Exists(path))
				{
					using (FileStream fs = File.Create(path))
					{
					}

					using (StreamWriter fs = new StreamWriter(path, true, System.Text.Encoding.GetEncoding(1251)))
					{
						fs.WriteLine("! " + Orderno);
						fs.WriteLine("# Обработка");
						List<string> Files = fso.GetListFilesForProc(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + Orderno + "\\", prop.List_of_files, true, true);
						if (Files.Count > 0)
						{
							foreach (string f in Files)
							{
								fs.WriteLine("+ Файл -> " + f);
							}
						}
						else
						{
							fs.WriteLine("; Файлов не обнаружено!");
						}
					}
				}


				return true;
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				_err = ex.Message + " " + ex.Source + " " + ex.StackTrace;
				return false;
			}


		}
		// Сохраняем из визарда текстовое описание
		public bool SaveAdvTextFile()
		{
			try
			{
				PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
				string path = prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + Orderno + "\\" + Orderno + ".txt";

				if (!File.Exists(path))
				{
					using (FileStream fs = File.Create(path))
					{
					}

					using (StreamWriter fs = new StreamWriter(path, true, System.Text.Encoding.GetEncoding(1251)))
					{
						fs.WriteLine("! Заказ № " + Orderno);
						fs.WriteLine("# Печать");
						fs.WriteLine("% " + Comment);
						// гуляем по строкам таблицы
						for (int i = 0; i < OrderBody.Rows.Count; i++)
						{
							// Если в 8-ой колонке на выставлена истина, значит эта 
							// строка добавлена автоматически в результате сканирования папок
							if (!(bool)OrderBody.Rows[i][8])
							{
								fs.WriteLine(": Услуга: " + OrderBody.Rows[i][6].ToString().Trim() + " [Количество " + OrderBody.Rows[i][2].ToString() + "] " + OrderBody.Rows[i]["comment"].ToString());
								List<string> Files = fso.GetListFilesForProc(prop.Dir_print + "\\" + fso.GetDateSubFolders() + "\\" + Orderno + "\\" + OrderBody.Rows[i][6].ToString().Trim() + "\\", prop.List_of_files, true, true);
								if (Files.Count > 0)
								{
									foreach (string f in Files)
									{
										fs.WriteLine("+ Файл -> " + f);
									}
								}
								else
								{
									fs.WriteLine("; Файлов не обнаружено!");
								}
							}
							else
							{
								// Если код услуги есть (а он по любому должен быть)
								if (OrderBody.Rows[i][5].ToString() != "")
								{
									// Запрашиваем тип услуги
									SqlCommand tmp_cmd = new SqlCommand("SELECT RTRIM([type]) as [type] FROM [vwGoodFull] WHERE [id_good] = '" + OrderBody.Rows[i][5].ToString() + "'", db_connection);
									SqlDataReader tmp_rdr = tmp_cmd.ExecuteReader();
									if (tmp_rdr.Read())
									{
										// Если это для печати (1)
										if (tmp_rdr.GetString(0) == "1")
										{
											fs.WriteLine("= Услуга: " + OrderBody.Rows[i][1].ToString().Trim() + " [Количество " + OrderBody.Rows[i][2].ToString() + "] " + OrderBody.Rows[i]["comment"].ToString());
											List<string> Files = (List<string>)OrderBody.Rows[i][9];
											if (Files.Count > 0)
											{
												foreach (string f in Files)
												{
													fs.WriteLine("+ Файл -> " + f);
												}
											}
											else
											{
												fs.WriteLine("; Файлов не обнаружено!");
											}
										}
									}
									tmp_rdr.Close();
								}
							}
						}
					}
				}


				path = prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + Orderno + "\\" + Orderno + ".txt";

				if (!File.Exists(path))
				{
					using (FileStream fs = File.Create(path))
					{
					}

					using (StreamWriter fs = new StreamWriter(path, true, System.Text.Encoding.GetEncoding(1251)))
					{
						fs.WriteLine("! Заказ № " + Orderno);
						fs.WriteLine("# Обработка");
						fs.WriteLine("% " + Comment);
						// гуляем по строкам таблицы
						for (int i = 0; i < OrderBody.Rows.Count; i++)
						{
							// Если в 8-ой колонке на выставлена истина, значит эта 
							// строка добавлена автоматически в результате сканирования папок
							if (!(bool)OrderBody.Rows[i][8])
							{
								fs.WriteLine(": Услуга: " + OrderBody.Rows[i][6].ToString().Trim() + " [Количество " + OrderBody.Rows[i][2].ToString() + "] " + OrderBody.Rows[i]["comment"].ToString());
								List<string> Files = fso.GetListFilesForProc(prop.Dir_edit + "\\" + fso.GetDateSubFolders() + "\\" + Orderno + "\\" + OrderBody.Rows[i][6].ToString().Trim() + "\\", prop.List_of_files, true, true);
								if (Files.Count > 0)
								{
									foreach (string f in Files)
									{
										fs.WriteLine("+ Файл -> " + f);
									}
								}
								else
								{
									fs.WriteLine("; Файлов не обнаружено!");
								}
							}
							else
							{
								// Если код услуги есть (а он по любому должен быть)
								if (OrderBody.Rows[i][5].ToString() != "")
								{
									// Запрашиваем тип услуги
									SqlCommand tmp_cmd = new SqlCommand("SELECT RTRIM([type]) as [type] FROM [vwGoodFull] WHERE [id_good] = '" + OrderBody.Rows[i][5].ToString() + "'", db_connection);
									SqlDataReader tmp_rdr = tmp_cmd.ExecuteReader();
									if (tmp_rdr.Read())
									{
										// Если это не для печати (2)
										if (tmp_rdr.GetString(0) == "2")
										{
											fs.WriteLine("= Услуга: " + OrderBody.Rows[i][1].ToString().Trim() + " [Количество " + OrderBody.Rows[i][2].ToString() + "] " + OrderBody.Rows[i]["comment"].ToString());
											List<string> Files = (List<string>)OrderBody.Rows[i][9];
											if (Files.Count > 0)
											{
												foreach (string f in Files)
												{
													fs.WriteLine("+ Файл -> " + f);
												}
											}
											else
											{
												fs.WriteLine("; Файлов не обнаружено!");
											}
										}
									}
									tmp_rdr.Close();
								}
							}
						}
					}
				}


				return true;
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				_err = ex.Message + " " + ex.Source + " " + ex.StackTrace;
				return false;
			}

		}

	}
}
