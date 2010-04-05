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
		private void Query_k(string kiosk, string order)
		{
			XmlDocument xml = new XmlDocument();
			string m = "";
			string root_dir = System.Environment.GetCommandLineArgs()[0].Substring(
				0,
				System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
				);
			DataTable tbl = new DataTable("body");
			tbl.Columns.Add("Раздел");
			tbl.Columns.Add("Услуга");
			tbl.Columns.Add("Количество");
			tbl.Columns.Add("Цена");
			tbl.Columns.Add("Доплата");
			tbl.Columns.Add("Материал");
			tbl.Columns.Add("Сумма");
			bool ok = false;
			int take = 3;
			while ((!ok) && (take > 0))
			{
				try
				{
					string Url = "http://print.fotoland.ru/photo/psa.get.korder.php?k=" + kiosk + "&o=" + order;
					XmlTextReader xmlTextReader = new XmlTextReader(Url);
					xml.Load(xmlTextReader);
					xmlTextReader.Close();
					XmlNode root = xml.GetElementsByTagName("KORDER")[0];
					foreach (XmlNode node in root.ChildNodes)
					{
						if (node.Name == "HEADER")
						{
							string t_kiosk = "", t_address = "", t_stamp = "", t_customer = "", t_phone = "", t_number = "", t_status = "", t_do = "", t_dp = "", t_dv = "";
							foreach (XmlNode hnode in node.ChildNodes)
							{
								if (hnode.Name == "NUMBER")
									t_number = hnode.InnerText;
								if (hnode.Name == "KIOSKID")
									t_kiosk = hnode.InnerText;
								if (hnode.Name == "ADDRESS")
									t_address = hnode.InnerText;
								if (hnode.Name == "STAMP")
									t_stamp = hnode.InnerText;
								if (hnode.Name == "CUSTOMER")
									t_customer = hnode.InnerText;
								if (hnode.Name == "PHONE")
									t_phone = hnode.InnerText;
								if (hnode.Name == "STATUS")
									t_status = hnode.InnerText;
								if (hnode.Name == "TOPRINT_AUTHDATE")
									t_do = hnode.InnerText;
								if (hnode.Name == "PRINTED_AUTHDATE")
									t_dp = hnode.InnerText;
								if (hnode.Name == "SHIP_AUTHDATE")
									t_dv = hnode.InnerText;
							}
							m += "Киоск №" + t_kiosk + ", " + t_address + "\nЗаказ: " + t_number + ", принят " + t_stamp + "\nКлиент: " + t_customer + ((t_phone != "") ? ", телефон: " + t_phone : "") + "\n" + ((t_do != "") ? "Отправлено в печать: " + t_do : "") + ((t_dp != "") ? " Напечатано: " + t_dp : "") + ((t_dv != "") ? " Отправлено в центр выдачи: " + t_dv : "") + "\nСТАТУС: " + t_status + " (данные получены: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ")\n";

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
										if (rnode.Name == "ACTION_NAME")
											r[1] = rnode.InnerText;
										if (rnode.Name == "ACTION_HEADER")
											r[0] = rnode.InnerText;
										if (rnode.Name == "SUBACTION_NAME")
											r[4] = rnode.InnerText;
										if (rnode.Name == "SUBACTION_PRICE")
											r[5] = rnode.InnerText;
										if (rnode.Name == "QTY")
											r[2] = rnode.InnerText;
										if (rnode.Name == "PRICE")
											r[3] = rnode.InnerText;
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
				if (File.Exists(prop.Dir_import + "//korder.xml"))
				{
					FileInfo _f = new FileInfo(prop.Dir_import + "//korder.xml");
					long _local_size = 0;
					DateTime _local_time = new DateTime();
					if(File.Exists(root_dir + "\\cache\\korder.xml"))
					{
						FileInfo __f = new FileInfo(root_dir + "\\cache\\korder.xml");
						_local_size = __f.Length;
						_local_time = __f.LastWriteTime;
					}
					if ((_f.Length != _local_size) || (_f.LastWriteTime != _local_time))
					{
						File.Copy(prop.Dir_import + "//korder.xml", root_dir + "\\cache\\korder.xml", true);
					}
				}
				if (File.Exists(root_dir + "\\cache\\korder.xml"))
				{
					XmlTextReader xmlTextReader = new XmlTextReader(root_dir + "\\cache\\korder.xml");
					xml.Load(xmlTextReader);
					xmlTextReader.Close();
					XmlNode root = xml.GetElementsByTagName("KORDER")[0];
					foreach (XmlNode onode in root.ChildNodes)
					{
						bool found = false;
						foreach (XmlNode node in onode.ChildNodes)
						{
							if (node.Name == "HEADER")
							{
								string t_kiosk = "", t_address = "", t_stamp = "", t_customer = "", t_phone = "", t_number = "", t_status = "", t_do = "", t_dp = "", t_dv = "", t_a = "";
								foreach (XmlNode hnode in node.ChildNodes)
								{
									if (hnode.Name == "NUMBER")
										t_number = hnode.InnerText;
									if (hnode.Name == "KIOSKID")
										t_kiosk = hnode.InnerText;
									if (hnode.Name == "ADDRESS")
										t_address = hnode.InnerText;
									if (hnode.Name == "STAMP")
										t_stamp = hnode.InnerText;
									if (hnode.Name == "CUSTOMER")
										t_customer = hnode.InnerText;
									if (hnode.Name == "PHONE")
										t_phone = hnode.InnerText;
									if (hnode.Name == "STATUS")
										t_status = hnode.InnerText;
									if (hnode.Name == "TOPRINT_AUTHDATE")
										t_do = hnode.InnerText;
									if (hnode.Name == "PRINTED_AUTHDATE")
										t_dp = hnode.InnerText;
									if (hnode.Name == "SHIP_AUTHDATE")
										t_dv = hnode.InnerText;
									if (hnode.Name == "ACTUAL")
										t_a = hnode.InnerText;
								}
								if ((t_number == order) && (t_kiosk == kiosk))
								{
									m += "Киоск №" + t_kiosk + ", " + t_address + "\nЗаказ: " + t_number + ", принят " + t_stamp + "\nКлиент: " + t_customer + ((t_phone != "") ? ", телефон: " + t_phone : "") + "\n" + ((t_do != "") ? "Отправлено в печать: " + t_do : "") + ((t_dp != "") ? " Напечатано: " + t_dp : "") + ((t_dv != "") ? " Отправлено в центр выдачи: " + t_dv : "") + "\nСТАТУС: " + t_status + " (данные получены: " + t_a + ")\n";
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
												if (rnode.Name == "ACTION_NAME")
													r[1] = rnode.InnerText;
												if (rnode.Name == "ACTION_HEADER")
													r[0] = rnode.InnerText;
												if (rnode.Name == "SUBACTION_NAME")
													r[4] = rnode.InnerText;
												if (rnode.Name == "SUBACTION_PRICE")
													r[5] = rnode.InnerText;
												if (rnode.Name == "QTY")
													r[2] = rnode.InnerText;
												if (rnode.Name == "PRICE")
													r[3] = rnode.InnerText;
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
				using (frmOrderRemoteInfo frm = new frmOrderRemoteInfo())
				{
					frm.t = tbl;
					frm.info = m;
					frm.UpdateData();
					frm.ShowDialog();
				}
			}
			else
			{
				if(take == 0)
					MessageBox.Show("Проблема со связью, информация о заказе не найдена.");
				else
					MessageBox.Show("Информация о заказе не найдена.");
			}
		}
	}
}
