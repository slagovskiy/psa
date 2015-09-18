using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Data;
using PSA.Lib.Interface;
using System.IO;
using Photoland.Security.User;


namespace PSA.Lib.Util
{
	public partial class RemoteQuery
	{
		Settings prop = new Settings();
		public UserInfo usr;

		public RemoteQuery()
		{
		}

		public RemoteQuery(UserInfo User)
		{
			usr = User;
		}

		public void GetInfo(string barcode)
		{
			string k = "0";
			string o = "0";
			try
			{
				if (barcode.IndexOf('-') > 0)
				{
					if (barcode.Split('-').Length == 3)
					{
						if (barcode.Split('-')[0].ToLower() == "k")
						{
							k = int.Parse(barcode.Split('-')[1]).ToString();
							o = int.Parse(barcode.Split('-')[2]).ToString();
							Query_k(k, o);
						}
						else if (barcode.Split('-')[0].ToLower() == "ir")
						{
							k = int.Parse(barcode.Split('-')[1]).ToString();
							o = int.Parse(barcode.Split('-')[2]).ToString();
							Query_ir(k, o);
						}
						else if (barcode.Split('-')[0].ToLower() == "io")
						{
							k = int.Parse(barcode.Split('-')[1]).ToString();
							o = int.Parse(barcode.Split('-')[2]).ToString();
							Query_io(k, o);
						}
					}
					else if (barcode.Split('-').Length == 2)
					{
						if (barcode.Split('-')[0].ToLower() == "k")
						{
							MessageBox.Show("Не верный номер " + barcode + "!");
						}
						else if (barcode.Split('-')[0].ToLower() == "ir")
						{
							k = "0";
							o = int.Parse(barcode.Split('-')[1]).ToString();
							Query_ir(k, o);
						}
						else if (barcode.Split('-')[0].ToLower() == "io")
						{
							k = "0";
							o = int.Parse(barcode.Split('-')[1]).ToString();
							Query_io(k, o);
						}
					}
					else
					{
						MessageBox.Show("Не верный номер " + barcode + "!");
					}
				}
				else
				{
					if ((barcode.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1)) && (barcode.Length > 11))
					{
						if (barcode.Substring(1, 1) == "0")
						{
							k = int.Parse(barcode.Substring(2, 3)).ToString();
							o = int.Parse(barcode.Substring(5, 7)).ToString();
							Query_k(k, o);
						}
						else if (barcode.Substring(1, 1) == "1")
						{
							k = "";
							o = int.Parse(barcode.Substring(2, 10)).ToString();
							Query_ir(k, o);
						}
						else if (barcode.Substring(1, 1) == "2")
						{
							k = "";
							o = int.Parse(barcode.Substring(2, 10)).ToString();
							Query_io(k, o);
						}
					}
				}
			}
			catch
			{
				MessageBox.Show("Не верный номер " + barcode + "!");
			}
		}

		public DataSet GetDataSet(string barcode)
		{
			string k = "0";
			string o = "0";
			DataSet ds = new DataSet();
			try
			{
				if (barcode.IndexOf('-') > 0)
				{
					if (barcode.Split('-').Length == 3)
					{
						if (barcode.Split('-')[0].ToLower() == "k")
						{
							k = int.Parse(barcode.Split('-')[1]).ToString();
							o = int.Parse(barcode.Split('-')[2]).ToString();
							ds = QueryDataSet_k(k, o);
						}
						else if (barcode.Split('-')[0].ToLower() == "ir")
						{
							k = int.Parse(barcode.Split('-')[1]).ToString();
							o = int.Parse(barcode.Split('-')[2]).ToString();
							ds = QueryDataSet_ir(k, o);
						}
						else if (barcode.Split('-')[0].ToLower() == "io")
						{
							k = int.Parse(barcode.Split('-')[1]).ToString();
							o = int.Parse(barcode.Split('-')[2]).ToString();
							ds = QueryDataSet_io(k, o);
						}
					}
					else if (barcode.Split('-').Length == 2)
					{
						if (barcode.Split('-')[0].ToLower() == "k")
						{
							MessageBox.Show("Не верный номер " + barcode + "!");
						}
						else if (barcode.Split('-')[0].ToLower() == "ir")
						{
							k = "0";
							o = int.Parse(barcode.Split('-')[1]).ToString();
							ds = QueryDataSet_ir(k, o);
						}
						else if (barcode.Split('-')[0].ToLower() == "io")
						{
							k = "0";
							o = int.Parse(barcode.Split('-')[1]).ToString();
							ds = QueryDataSet_io(k, o);
						}
					}
					else
					{
						MessageBox.Show("Не верный номер " + barcode + "!");
					}
				}
				else
				{
					if ((barcode.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1)) && (barcode.Length > 11))
					{
						if (barcode.Substring(1, 1) == "0")
						{
							k = int.Parse(barcode.Substring(2, 3)).ToString();
							o = int.Parse(barcode.Substring(5, 7)).ToString();
							ds = QueryDataSet_k(k, o);
						}
						else if (barcode.Substring(1, 1) == "1")
						{
							k = "";
							o = int.Parse(barcode.Substring(2, 10)).ToString();
							ds = QueryDataSet_ir(k, o);
						}
						else if (barcode.Substring(1, 1) == "2")
						{
							k = "";
							o = int.Parse(barcode.Substring(2, 10)).ToString();
							ds = QueryDataSet_io(k, o);
						}
					}
				}
			}
			catch
			{
				MessageBox.Show("Не верный номер " + barcode + "!");
			}
			return ds;
		}


		public void GetData(string barcode)
		{
			string k = "0";
			string o = "0";
			try
			{
				if (barcode.IndexOf('-') > 0)
				{
					if (barcode.Split('-').Length == 3)
					{
						if (barcode.Split('-')[0].ToLower() == "k")
						{
							k = int.Parse(barcode.Split('-')[1]).ToString();
							o = int.Parse(barcode.Split('-')[2]).ToString();
							QueryData_k(k, o);
						} else if (barcode.Split('-')[0].ToLower() == "ir")
						{
							k = int.Parse(barcode.Split('-')[1]).ToString();
							o = int.Parse(barcode.Split('-')[2]).ToString();
							QueryData_ir(k, o);
						} else if (barcode.Split('-')[0].ToLower() == "io")
						{
							k = int.Parse(barcode.Split('-')[1]).ToString();
							o = int.Parse(barcode.Split('-')[2]).ToString();
							QueryData_io(k, o);
						}
					}
					else if (barcode.Split('-').Length == 2)
					{
						if (barcode.Split('-')[0].ToLower() == "k")
						{
							MessageBox.Show("Не верный номер " + barcode + "!");
						}
						else if (barcode.Split('-')[0].ToLower() == "ir")
						{
							k = "0";
							o = int.Parse(barcode.Split('-')[1]).ToString();
							QueryData_ir(k, o);
						}
						else if (barcode.Split('-')[0].ToLower() == "io")
						{
							k = "0";
							o = int.Parse(barcode.Split('-')[1]).ToString();
							QueryData_io(k, o);
						}
					}
					else 
					{
						MessageBox.Show("Не верный номер " + barcode + "!");
					}
				}
				else
				{
					if ((barcode.Substring(0, 1) == prop.Order_terminal_prefics.Substring(0, 1)) && (barcode.Length > 11))
					{
						if (barcode.Substring(1, 1) == "0")
						{
							k = int.Parse(barcode.Substring(2, 3)).ToString();
							o = int.Parse(barcode.Substring(5, 7)).ToString();
							QueryData_k(k, o);
						} else if (barcode.Substring(1, 1) == "1")
						{
							k = "";
							o = int.Parse(barcode.Substring(2, 10)).ToString();
							QueryData_ir(k, o);
						} else if (barcode.Substring(1, 1) == "2")
						{
							k = "";
							o = int.Parse(barcode.Substring(2, 10)).ToString();
							QueryData_io(k, o);
						}
					}
				}
			}
			catch
			{
				MessageBox.Show("Не верный номер " + barcode + "!");
			}
		}

		public void GetMFotoData(string barcode)
		{
			QueryData_mfoto(barcode);
		}
	}
}
