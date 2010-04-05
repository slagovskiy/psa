using System;
using System.Collections.Generic;
using System.Text;
using PSA.Lib.DAL.SqlProviders;
using System.Data;

namespace PSA.Lib.BLL
{
    public class VerificationBody
    {
        private int _id;
        private bool _del;
        private string _guid;
		private int _verification_id;
        private string _number;
        private bool _found;
        private string _status_t;
        private string _status;
        private string _status_fact_t;
        private string _status_fact;
        private DateTime _date_in;
        private DateTime _date_out;
        private string _action;
        private string _action_t;
		private bool _exported;
        private Photoland.Security.User.UserInfo _usr;

        public int id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public bool del
        {
            get
            {
                return _del;
            }
            set
            {
                _del = value;
            }
        }

        public string guid
        {
            get
            {
                return _guid;
            }
            set
            {
                _guid = value;
            }
        }

       public int Verification_id
        {
            get
            {
                return _verification_id;
            }
            set
            {
                _verification_id = value;
            }
        }

        public string number
        {
            get
            {
                return _number;
            }
            set
            {
                _number = value;
            }
        }

        public bool found
        {
            get
            {
                return _found;
            }
            set
            {
                _found = value;
            }
        }

        public string status_t
        {
            get
            {
                return _status_t;
            }
            set
            {
                _status_t = value;
            }
        }

        public string status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public string status_fact_t
        {
            get
            {
                return _status_fact_t;
            }
            set
            {
                _status_fact_t = value;
            }
        }

        public string status_fact
        {
            get
            {
                return _status_fact;
            }
            set
            {
                _status_fact = value;
            }
        }

        public DateTime date_in
        {
            get
            {
                return _date_in;
            }
            set
            {
                _date_in = value;
            }
        }

        public DateTime date_out
        {
            get
            {
                return _date_out;
            }
            set
            {
                _date_out = value;
            }
        }

        public string action
        {
            get
            {
                return _action;
            }
            set
            {
                _action = value;
            }
        }

        public string action_t
        {
            get
            {
                return _action_t;
            }
            set
            {
                _action_t = value;
            }
        }

        public Photoland.Security.User.UserInfo usr
        {
            get
            {
                return _usr;
            }
            set
            {
                _usr = value;
            }
        }

		public bool exported
		{
			get
			{
				return _exported;
			}
			set
			{
				_exported = value;
			}
		}

		public VerificationBody()
        {
        }

		public static List<VerificationBody> getVerificationBodyByVerificationId(int Verification_id)
        {
			List<VerificationBody> lst = new List<VerificationBody>();

            try
            {
				VerificationBodyProvider pr = new VerificationBodyProvider();
				DataSet ds = pr.getByVerificationId(Verification_id);
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
					VerificationBody o = new VerificationBody();
					o.id = int.Parse(ds.Tables[0].Rows[i]["id_verificationbody"].ToString());
					o.del = bool.Parse(ds.Tables[0].Rows[i]["del"].ToString());
					o.guid = ds.Tables[0].Rows[i]["guid"].ToString();
					o.Verification_id = Verification_id;
					o.number = ds.Tables[0].Rows[i]["order_number"].ToString();
					o.found = bool.Parse(ds.Tables[0].Rows[i]["order_found"].ToString());
                    o.status = ds.Tables[0].Rows[i]["order_status"].ToString();
                    o.status_t = ds.Tables[0].Rows[i]["order_status_t"].ToString();
                    o.status_fact = ds.Tables[0].Rows[i]["order_status_fact"].ToString();
                    o.status_fact_t = ds.Tables[0].Rows[i]["order_status_fact_t"].ToString();
                    if (ds.Tables[0].Rows[i]["order_in"].ToString() != "")
						o.date_in = DateTime.Parse(ds.Tables[0].Rows[i]["order_in"].ToString());
					if (ds.Tables[0].Rows[i]["order_out"].ToString() != "")
						o.date_out = DateTime.Parse(ds.Tables[0].Rows[i]["order_out"].ToString());
					o.action = ds.Tables[0].Rows[i]["order_action"].ToString();
					o.action_t = ds.Tables[0].Rows[i]["order_action_t"].ToString();
					o.usr = new Photoland.Security.User.UserInfo();
					o.usr.Id_user = int.Parse(ds.Tables[0].Rows[i]["order_user"].ToString());
					o.exported = bool.Parse(ds.Tables[0].Rows[i]["exported"].ToString());

					lst.Add(o);

                    
                }
            }
            catch (Exception ex)
            {
            }


            return lst;
        }

    }
}
