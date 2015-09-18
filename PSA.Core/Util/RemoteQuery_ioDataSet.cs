using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Data;
using PSA.Lib.Interface;
using System.IO;


namespace PSA.Lib.Util
{
	public partial class RemoteQuery
	{
		private DataSet QueryDataSet_io(string kiosk, string order)
		{
			XmlDocument xml = new XmlDocument();
			string m = "";
			string root_dir = System.Environment.GetCommandLineArgs()[0].Substring(
				0,
				System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
				);
			DataSet ds = new DataSet();
			DataTable tblh = new DataTable("header");
			tblh.Columns.Add("NUMBER");
			tblh.Columns.Add("KIOSKID");
			tblh.Columns.Add("ADDRESS");
			tblh.Columns.Add("STAMP");
			tblh.Columns.Add("CUSTOMER");
			tblh.Columns.Add("PHONE");
			tblh.Columns.Add("STATUS");
			tblh.Columns.Add("TOPRINT_AUTHDATE");
			tblh.Columns.Add("PRINTED_AUTHDATE");
			tblh.Columns.Add("SHIP_AUTHDATE");
			tblh.Columns.Add("TOPRINT_AUTHID");
			tblh.Columns.Add("PRINTED_AUTHID");
			tblh.Columns.Add("SHIP_AUTHID");
			DataTable tbl = new DataTable("body");
			tbl.Columns.Add("ACTION_HEADER");
			tbl.Columns.Add("ACTION_NAME");
			tbl.Columns.Add("QTY");
			tbl.Columns.Add("PRICE");
			tbl.Columns.Add("SUBACTION_NAME");
			tbl.Columns.Add("SUBACTION_PRICE");
			tbl.Columns.Add("Сумма");
			bool ok = false;
			int take = 3;
			while ((!ok) && (take > 0))
			{
				try
				{
					string Url = "http://print.fotoland.ru/photo/psa.get.ioorder.php?k=" + kiosk + "&o=" + order;
					XmlTextReader xmlTextReader = new XmlTextReader(Url);
					xml.Load(xmlTextReader);
					xmlTextReader.Close();
					XmlNode root = xml.GetElementsByTagName("IOORDER")[0];
					foreach (XmlNode node in root.ChildNodes)
					{
						if (node.Name == "HEADER")
						{
							string t_kiosk = "", t_address = "", t_stamp = "", t_customer = "", t_phone = "", t_number = "", t_status = "", t_do = "", t_dp = "", t_dv = "";
							DataRow r = tblh.NewRow();
							foreach (XmlNode hnode in node.ChildNodes)
							{
								if (hnode.Name == "NUMBER")
									r["NUMBER"] = hnode.InnerText;
								if (hnode.Name == "KIOSKID")
									r["KIOSKID"] = hnode.InnerText;
								if (hnode.Name == "ADDRESS")
									r["ADDRESS"] = hnode.InnerText;
								if (hnode.Name == "STAMP")
									r["STAMP"] = hnode.InnerText;
								if (hnode.Name == "CUSTOMER")
									r["CUSTOMER"] = hnode.InnerText;
								if (hnode.Name == "PHONE")
									r["PHONE"] = hnode.InnerText;
								if (hnode.Name == "STATUS")
									r["STATUS"] = hnode.InnerText;
								if (hnode.Name == "TOPRINT_AUTHDATE")
									r["TOPRINT_AUTHDATE"] = hnode.InnerText;
								if (hnode.Name == "PRINTED_AUTHDATE")
									r["PRINTED_AUTHDATE"] = hnode.InnerText;
								if (hnode.Name == "SHIP_AUTHDATE")
									r["SHIP_AUTHDATE"] = hnode.InnerText;
								if (hnode.Name == "TOPRINT_AUTHID")
									r["TOPRINT_AUTHID"] = hnode.InnerText;
								if (hnode.Name == "PRINTED_AUTHID")
									r["PRINTED_AUTHID"] = hnode.InnerText;
								if (hnode.Name == "SHIP_AUTHID")
									r["SHIP_AUTHID"] = hnode.InnerText;
							}
							tblh.Rows.Add(r);
							m += "ok";

						}
						if (node.Name == "BODY")
						{
							foreach (XmlNode bnode in node.ChildNodes)
							{
								if (bnode.Name == "ROW")
								{
									DataRow r = tbl.NewRow();
									foreach (XmlNode rnode in bnode.ChildNodes)
									{
										if (rnode.Name == "ACTION_HEADER")
											r[0] = rnode.InnerText;
										if (rnode.Name == "ACTION_NAME")
											r[1] = rnode.InnerText;
										if (rnode.Name == "QTY")
											r[2] = rnode.InnerText;
										if (rnode.Name == "PRICE")
											r[3] = rnode.InnerText;
										if (rnode.Name == "SUBACTION_NAME")
											r[4] = rnode.InnerText;
										if (rnode.Name == "SUBACTION_PRICE")
											r[5] = rnode.InnerText;
									}
									tbl.Rows.Add(r);
								}
							}
						}

					}
					ok = true;
				}
				catch (Exception ex)
				{
					take--;
				}

			}
			if (!ok)
			{
				if (!Directory.Exists(root_dir + "\\cache\\"))
					Directory.CreateDirectory(root_dir + "\\cache\\");
				if (File.Exists(prop.Dir_import + "//ioorder.xml"))
				{
					FileInfo _f = new FileInfo(prop.Dir_import + "//ioorder.xml");
					long _local_size = 0;
					DateTime _local_time = new DateTime();
					if (File.Exists(root_dir + "\\cache\\ioorder.xml"))
					{
						FileInfo __f = new FileInfo(root_dir + "\\cache\\ioorder.xml");
						_local_size = __f.Length;
						_local_time = __f.LastWriteTime;
					}
					if ((_f.Length != _local_size) || (_f.LastWriteTime != _local_time))
					{
						File.Copy(prop.Dir_import + "//irorder.xml", root_dir + "\\cache\\ioorder.xml", true);
					}
				}
				if (File.Exists(root_dir + "\\cache\\ioorder.xml"))
				{
					XmlTextReader xmlTextReader = new XmlTextReader(root_dir + "\\cache\\ioorder.xml");
					xml.Load(xmlTextReader);
					xmlTextReader.Close();
					XmlNode root = xml.GetElementsByTagName("IOORDER")[0];
					foreach (XmlNode onode in root.ChildNodes)
					{
						bool found = false;
						foreach (XmlNode node in onode.ChildNodes)
						{
							if (node.Name == "HEADER")
							{
								string t_kiosk = "", t_address = "", t_stamp = "", t_customer = "", t_phone = "", t_number = "", t_status = "", t_do = "", t_dp = "", t_dv = "", t_a = "";
								DataRow r = tblh.NewRow();
								foreach (XmlNode hnode in node.ChildNodes)
								{
									if (hnode.Name == "NUMBER")
										r["NUMBER"] = hnode.InnerText;
									if (hnode.Name == "KIOSKID")
										r["KIOSKID"] = hnode.InnerText;
									if (hnode.Name == "ADDRESS")
										r["ADDRESS"] = hnode.InnerText;
									if (hnode.Name == "STAMP")
										r["STAMP"] = hnode.InnerText;
									if (hnode.Name == "CUSTOMER")
										r["CUSTOMER"] = hnode.InnerText;
									if (hnode.Name == "PHONE")
										r["PHONE"] = hnode.InnerText;
									if (hnode.Name == "STATUS")
										r["STATUS"] = hnode.InnerText;
									if (hnode.Name == "TOPRINT_AUTHDATE")
										r["TOPRINT_AUTHDATE"] = hnode.InnerText;
									if (hnode.Name == "PRINTED_AUTHDATE")
										r["PRINTED_AUTHDATE"] = hnode.InnerText;
									if (hnode.Name == "SHIP_AUTHDATE")
										r["SHIP_AUTHDATE"] = hnode.InnerText;
									if (hnode.Name == "TOPRINT_AUTHID")
										r["TOPRINT_AUTHID"] = hnode.InnerText;
									if (hnode.Name == "PRINTED_AUTHID")
										r["PRINTED_AUTHID"] = hnode.InnerText;
									if (hnode.Name == "SHIP_AUTHID")
										r["SHIP_AUTHID"] = hnode.InnerText;
								}
								if ((r["NUMBER"].ToString() == order) && (r["KIOSKID"].ToString() == kiosk))
								{
									tblh.Rows.Add(r);
									m += "ok";
									found = true;
								}

							}
							if (found)
							{
								if (node.Name == "BODY")
								{
									foreach (XmlNode bnode in node.ChildNodes)
									{
										if (bnode.Name == "ROW")
										{
											DataRow r = tbl.NewRow();
											foreach (XmlNode rnode in bnode.ChildNodes)
											{
												if (rnode.Name == "ACTION_HEADER")
													r[0] = rnode.InnerText;
												if (rnode.Name == "ACTION_NAME")
													r[1] = rnode.InnerText;
												if (rnode.Name == "QTY")
													r[2] = rnode.InnerText;
												if (rnode.Name == "PRICE")
													r[3] = rnode.InnerText;
												if (rnode.Name == "SUBACTION_NAME")
													r[4] = rnode.InnerText;
												if (rnode.Name == "SUBACTION_PRICE")
													r[5] = rnode.InnerText;
											}
											tbl.Rows.Add(r);
										}
									}
									ok = true;
								}
							}
						}
						if (found)
							break;
					}
				}
			}
			if ((ok) && (m != "") && (tbl.Rows.Count != 0))
			{
				ds.Tables.Add(tblh);
				ds.Tables.Add(tbl);
			}
			else
			{
				if(take == 0)
					MessageBox.Show("Проблема со связью, информация о заказе не найдена.");
				else
					MessageBox.Show("Информация о заказе не найдена.");
			}
			return ds;
		}
	}
}
