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


namespace Photoland.Security.Discont
{
	public class DiscontInfo
	{

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
				HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://k.fotoland.ru/card.get.php?code=" + code + "&key=" + key + "&format=xml");
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
			catch 
			{
				// ошибка веба
				if (db_connection.State == ConnectionState.Closed)
					db_connection.Open();
				SqlCommand db_command = new SqlCommand("SELECT [id_dcard], [guid], [code], [name], [disc], [discserv], [date], [not], [phone], [email], [bonus], [typebonus] FROM [vwDCardFull] WHERE RTRIM([code]) = '" + code + "'", db_connection);
				SqlDataReader db_reader = db_command.ExecuteReader();
				if (db_reader.Read())
				{
					if (!db_reader.IsDBNull(0))
						this._id_dcard = db_reader.GetInt32(0);
					else
						this._id_dcard = 0;

					if (!db_reader.IsDBNull(2))
						this._code_dcard = db_reader.GetString(2);
					else
						this._code_dcard = "";

					if (!db_reader.IsDBNull(3))
						this._name_dcard = db_reader.GetString(3);
					else
						this._name_dcard = "";

					if (!db_reader.IsDBNull(4))
						this._disc = db_reader.GetDecimal(4);
					else
						this._disc = 0;

					if (!db_reader.IsDBNull(5))
						this._discserv = db_reader.GetDecimal(5);
					else
						this._discserv = 0;

					if (!db_reader.IsDBNull(7))
						if (db_reader.GetString(7) == "1")
							this._not = true;
						else
							this._not = false;
					else
						this._not = false;

					if (!db_reader.IsDBNull(8))
						this._phone = db_reader.GetString(8);
					else
						this._phone = "";

					if (!db_reader.IsDBNull(9))
						this._email = db_reader.GetString(9);
					else
						this._email = "";

					if (!db_reader.IsDBNull(10))
						this._bonus = db_reader.GetDecimal(10);
					else
						this._bonus = 0;

					if (!db_reader.IsDBNull(11))
						this._bonustype = db_reader.GetString(11);
					else
						this._bonustype = "";
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
				db_reader.Close();
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
