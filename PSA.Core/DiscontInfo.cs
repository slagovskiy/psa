using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Photoland.Security;
using System.Net;
using System.IO;
using System.Xml;
using PSA.Lib.Util;



namespace Photoland.Security.Discont
{
	public class DiscontInfo
	{
		PSA.Lib.Util.Settings settings = new Settings();

		public DiscontInfo()
		{
			this._id_dcard = 0;
			this._code_dcard = "";
			this._name_dcard = "";
			this._disc = 0;
			this._discserv = 0;
			this._not = false;
			this._phone = "";
			this._email = "";
			this._bonus = 0;
			this._bonustype = "";
		}

		/// <summary>
		/// Поиск через web
		/// </summary>
		/// <param name="code">Номер карты</param>
		public DiscontInfo(string code, SqlConnection db_connection)
		{
			try
			{
				string key = DateTime.Now.Year.ToString("D4") + 
							DateTime.Now.Month.ToString("D2") + 
							DateTime.Now.Day.ToString("D2") + 
							DateTime.Now.Hour.ToString("D2") + 
							DateTime.Now.Minute.ToString("D2") + 
							DateTime.Now.Second.ToString("D2") + 
							DateTime.Now.Millisecond.ToString("D2");
                if (code.Trim() != "")
                {
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + settings.DiscontServerAddress + "/card.get.php?code=" + code + "&key=" + key + "&format=xml");
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream resStream = response.GetResponseStream();
                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        doc.Load(resStream);
                        foreach (XmlNode n in doc.ChildNodes)
                        {
                            if (n.Name.ToLower() == "data")
                            {
                                foreach (XmlNode nn in n.ChildNodes)
                                {
                                    if (nn.Name.ToLower() == "key")
                                    {
                                        if (key != nn.InnerText)
                                        {
                                            this._id_dcard = 0;
                                            this._code_dcard = "";
                                            this._name_dcard = "";
                                            this._disc = 0;
                                            this._discserv = 0;
                                            this._not = false;
                                            this._phone = "";
                                            this._email = "";
                                            this._bonus = 0;
                                            this._bonustype = "";
                                            return;
                                        }
                                    }
                                    if (nn.Name.ToLower() == "card")
                                    {
                                        foreach (XmlNode nnn in nn.ChildNodes)
                                        {
                                            switch (nnn.Name.ToLower())
                                            {
                                                case "code":
                                                    {
                                                        this._code_dcard = nnn.InnerText;
                                                        break;
                                                    }
                                                case "name":
                                                    {
                                                        this._name_dcard = nnn.InnerText;
                                                        break;
                                                    }
                                                case "disc":
                                                    {
                                                        this._disc = decimal.Parse(nnn.InnerText);
                                                        break;
                                                    }
                                                case "discserv":
                                                    {
                                                        this._discserv = decimal.Parse(nnn.InnerText);
                                                        break;
                                                    }
                                                case "phone":
                                                    {
                                                        this._phone = nnn.InnerText;
                                                        break;
                                                    }
                                                case "email":
                                                    {
                                                        this._email = nnn.InnerText;
                                                        break;
                                                    }
                                                case "bonus":
                                                    {
                                                        this._bonus = decimal.Parse(nnn.InnerText);
                                                        break;
                                                    }
                                                case "type":
                                                    {
                                                        this._bonustype = nnn.InnerText;
                                                        break;
                                                    }
                                            }
                                            this._not = false;
                                            this._id_dcard = 999999999;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    catch
                    {
                        // не нашлось среди в web-е
                        this._id_dcard = 0;
                        this._code_dcard = "";
                        this._name_dcard = "";
                        this._disc = 0;
                        this._discserv = 0;
                        this._not = false;
                        this._phone = "";
                        this._email = "";
                        this._bonus = 0;
                        this._bonustype = "";
                    }
                }
                else
                {
                    this._id_dcard = 0;
                    this._code_dcard = "";
                    this._name_dcard = "";
                    this._disc = 0;
                    this._discserv = 0;
                    this._not = false;
                    this._phone = "";
                    this._email = "";
                    this._bonus = 0;
                    this._bonustype = "";
                }

			}
			catch 
			{
				// ошибка веба
				db_connection = new SqlConnection(settings.Connection_string);
				SqlCommand db_command = new SqlCommand("SELECT [id_dcard], [guid], [code], [name], [disc], [discserv], [date], [not], [phone], [email], [bonus], [typebonus] FROM [vwDCardFull] WHERE RTRIM([code]) = '" + code + "'", db_connection);
				SqlDataAdapter da = new SqlDataAdapter(db_command);
				DataTable tbl = new DataTable("");
				da.Fill(tbl);
				if (tbl.Rows.Count > 0)
				{
					this._id_dcard = int.Parse(tbl.Rows[0][0].ToString() ?? "0");
					this._code_dcard = tbl.Rows[0][2].ToString() ?? "";
					this._name_dcard = tbl.Rows[0][3].ToString() ?? "";
					this._disc = decimal.Parse(tbl.Rows[0][4].ToString() ?? "0");
					this._discserv = decimal.Parse(tbl.Rows[0][5].ToString() ?? "0");
					this._not = false;
					this._phone = tbl.Rows[0][8].ToString() ?? "";
					this._email = tbl.Rows[0][9].ToString() ?? "";
					this._bonus = decimal.Parse(tbl.Rows[0][10].ToString() ?? "0");
					this._bonustype = tbl.Rows[0][11].ToString() ?? "";
				}
				else
				{
					this._id_dcard = 0;
					this._code_dcard = "";
					this._name_dcard = "";
					this._disc = 0;
					this._discserv = 0;
					this._not = false;
					this._phone = "";
					this._email = "";
					this._bonus = 0;
					this._bonustype = "";
				}
			}
		}

		public DiscontInfo(string code, decimal discont_serv)
		{
			this._code_dcard = code;
			this._name_dcard = "";
			this._disc = 0;
			this._discserv = discont_serv;
			this._not = false;
			this._phone = "";
			this._email = "";
			this._id_dcard = 0;
			this._bonus = 0;
			this._bonustype = "";
		}

		private int _id_dcard;
		public int Id_dcard
		{
			get { return _id_dcard; }
			set { _id_dcard = value; }
		}

		private string _code_dcard;
		public string Code_dcard
		{
			get { return _code_dcard; }
		}

		private string _bonustype;
		public string BonusType
		{
			get { return _bonustype; }
		}

		private string _name_dcard;
		public string Name_dcard
		{
			get { return _name_dcard; }
		}

		private decimal _disc;
		public decimal Disc
		{
			get { return _disc; }
		}

		private decimal _discserv;
		public decimal Discserv
		{
			get { return _discserv; }
		}

		private decimal _bonus;
		public decimal Bonus
		{
			get { return _bonus; }
		}

		private bool _not;
		public bool Not
		{
			get { return _not; }
		}

		private string _phone;
		public string Phone
		{
			get { return _phone; }
		}

		private string _email;
		public string Email
		{
			get { return _email; }
		}

	}
}
