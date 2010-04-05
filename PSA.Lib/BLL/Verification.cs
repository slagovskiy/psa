using System;
using System.Collections.Generic;
using System.Text;
using PSA.Lib.DAL.SqlProviders;
using System.Data;

namespace PSA.Lib.BLL
{
    public class Verification
    {
        private int _id;
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

        private bool _del;
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

        private string _guid;
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

        private DateTime _date;
        public DateTime date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
            }
        }

        private Photoland.Security.User.UserInfo _usr;
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

        private List<VerificationBody> _body;
        public List<VerificationBody> body
        {
            get 
            { 
                return _body; 
            }
            set 
            { 
                _body = value; 
            }
        }

        public Verification()
        {

        }

        public Verification(int id)
        {
			try
			{
                VerificationProvider pr = new VerificationProvider();
				DataSet ds = pr.getById(id);
				if (ds.Tables[0].Rows.Count > 0)
				{
					this.id = int.Parse(ds.Tables[0].Rows[0]["id_verification"].ToString());
					this.del = bool.Parse(ds.Tables[0].Rows[0]["del"].ToString());
					this.guid = ds.Tables[0].Rows[0]["guid"].ToString();
					if(ds.Tables[0].Rows[0]["verification_date"].ToString() != "")
						this.date = DateTime.Parse(ds.Tables[0].Rows[0]["verification_date"].ToString());
					this.usr = new Photoland.Security.User.UserInfo();
					this.usr.Id_user = int.Parse(ds.Tables[0].Rows[0]["verification_user"].ToString());

					this.body = VerificationBody.getVerificationBodyByVerificationId(id);

				}
			}
			catch (Exception ex)
			{
			}
        }

		public bool UpdateBodyRow(string number, string action, string action_t, int user)
		{
			bool ok = false;
			try
			{
				VerificationBodyProvider pr = new VerificationBodyProvider();
				pr.UpdateAction(this.id, number, action, action_t, user);
				ok = true;
			}
			catch(Exception ex)
			{
			}
			return ok;
		}

		public static List<Verification> getList()
		{
			List<Verification> lst = new List<Verification>();
			try
			{
				VerificationProvider pr = new VerificationProvider();
				DataSet ds = pr.GetList();
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
				{
					Verification o = new Verification();
					o.id = int.Parse(ds.Tables[0].Rows[i]["id_verification"].ToString());
					o.del = bool.Parse(ds.Tables[0].Rows[i]["del"].ToString());
					o.guid = ds.Tables[0].Rows[i]["guid"].ToString();
					if (ds.Tables[0].Rows[i]["verification_date"].ToString() != "")
						o.date = DateTime.Parse(ds.Tables[0].Rows[i]["verification_date"].ToString());
					o.usr = new Photoland.Security.User.UserInfo();
					o.usr.Id_user = int.Parse(ds.Tables[0].Rows[i]["verification_user"].ToString());
					// не правильно, но старые объекты не стал переписывать :) пятница!
					UserProvider upr = new UserProvider();
					o.usr.Name = upr.getNameById(o.usr.Id_user);

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
