using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Data;
using PSA.Lib.Interface;
using System.IO;
using System.Data.SqlClient;
using PSA.Lib.Util;



namespace PSA.Lib.Util
{
	public partial class RemoteQuery
	{
		private void QueryData_mfoto(string barcode)
		{
			XmlDocument xml = new XmlDocument();
			Dictionary<string, string> head = new Dictionary<string, string>();
			string root_dir = System.Environment.GetCommandLineArgs()[0].Substring(
				0,
				System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
				);
			string strORDER_ID = "";
			string strORDER_NUMBER = "";
			string strPRODUCT_NAME = "";
			string strDESCRIPTION = "";
			string strCOPIES = "";
			string strTOTAL_PRICE = "";
			string strSTATUS = "";
			string strCREATED_TIME = "";
			string strLAST_NAME = "";
			string strFIRST_NAME = "";
			string strCITY = "";
			string strSTREET = "";
			string strPHONE = "";
			string strEMAIL = "";
			string strKEY = "";
			string strPATH = "";

			bool ok = false;
			int take = 3;
			while ((!ok) && (take > 0))
			{
				try
				{
					string Url = "http://print.fotoland.ru/photo/psa.mfoto.get.php?o=" + barcode;
					XmlTextReader xmlTextReader = new XmlTextReader(Url);
					xml.Load(xmlTextReader);
					xmlTextReader.Close();
					XmlNode root = xml.GetElementsByTagName("DATA")[0];
					foreach (XmlNode node in root.ChildNodes)
					{
						if (node.Name == "ROW")
						{
							foreach (XmlNode hnode in node.ChildNodes)
							{
								if (hnode.Name == "ORDER_ID")
									strORDER_ID = hnode.InnerText;
								if (hnode.Name == "ORDER_NUMBER")
									strORDER_NUMBER = hnode.InnerText;
								if (hnode.Name == "PRODUCT_NAME")
									strPRODUCT_NAME = hnode.InnerText;
								if (hnode.Name == "COPIES")
									strCOPIES = hnode.InnerText;
								if (hnode.Name == "TOTAL_PRICE")
									strTOTAL_PRICE = hnode.InnerText;
								if (hnode.Name == "DESCRIPTION")
									strDESCRIPTION = hnode.InnerText;
								if (hnode.Name == "STATUS")
									strSTATUS = hnode.InnerText;
								if (hnode.Name == "CREATED_TIME")
									strCREATED_TIME = hnode.InnerText;
								if (hnode.Name == "LAST_NAME")
									strLAST_NAME = hnode.InnerText;
								if (hnode.Name == "FIRST_NAME")
									strFIRST_NAME = hnode.InnerText;
								if (hnode.Name == "CITY")
									strCITY = hnode.InnerText;
								if (hnode.Name == "STREET")
									strSTREET = hnode.InnerText;
								if (hnode.Name == "PHONE")
									strPHONE = hnode.InnerText;
								if (hnode.Name == "EMAIL")
									strEMAIL = hnode.InnerText;
							}
							ok = true;
						}
						if (node.Name == "ERROR")
						{
							if(node.InnerText != "") 
							{
								MessageBox.Show("Ошибка! Заказ " + barcode + " " + node.InnerText);
								take = 0;
							}

						}
					}
				}
				catch
				{
					take--;
				}
			}
			if (!ok)
			{
				if (!Directory.Exists(root_dir + "\\cache\\"))
					Directory.CreateDirectory(root_dir + "\\cache\\");
				if (File.Exists(prop.Dir_import + "//morder.xml"))
				{
					FileInfo _f = new FileInfo(prop.Dir_import + "//morder.xml");
					long _local_size = 0;
					DateTime _local_time = new DateTime();
					if (File.Exists(root_dir + "\\cache\\morder.xml"))
					{
						FileInfo __f = new FileInfo(root_dir + "\\cache\\morder.xml");
						_local_size = __f.Length;
						_local_time = __f.LastWriteTime;
					}
					if ((_f.Length != _local_size) || (_f.LastWriteTime != _local_time))
					{
						File.Copy(prop.Dir_import + "//morder.xml", root_dir + "\\cache\\morder.xml", true);
					}
				}
				if (File.Exists(root_dir + "\\cache\\morder.xml"))
				{
					XmlTextReader xmlTextReader = new XmlTextReader(root_dir + "\\cache\\morder.xml");
					xml.Load(xmlTextReader);
					xmlTextReader.Close();
					XmlNode root = xml.GetElementsByTagName("DATA")[0];
					foreach (XmlNode node in root.ChildNodes)
					{
						if (node.Name == "ROW")
						{
							foreach (XmlNode hnode in node.ChildNodes)
							{
								if (hnode.Name == "ORDER_ID")
									strORDER_ID = hnode.InnerText;
								if (hnode.Name == "ORDER_NUMBER")
									strORDER_NUMBER = hnode.InnerText;
								if (hnode.Name == "PRODUCT_NAME")
									strPRODUCT_NAME = hnode.InnerText;
								if (hnode.Name == "COPIES")
									strCOPIES = hnode.InnerText;
								if (hnode.Name == "TOTAL_PRICE")
									strTOTAL_PRICE = hnode.InnerText;
								if (hnode.Name == "DESCRIPTION")
									strDESCRIPTION = hnode.InnerText;
								if (hnode.Name == "STATUS")
									strSTATUS = hnode.InnerText;
								if (hnode.Name == "CREATED_TIME")
									strCREATED_TIME = hnode.InnerText;
								if (hnode.Name == "LAST_NAME")
									strLAST_NAME = hnode.InnerText;
								if (hnode.Name == "FIRST_NAME")
									strFIRST_NAME = hnode.InnerText;
								if (hnode.Name == "CITY")
									strCITY = hnode.InnerText;
								if (hnode.Name == "STREET")
									strSTREET = hnode.InnerText;
								if (hnode.Name == "PHONE")
									strPHONE = hnode.InnerText;
								if (hnode.Name == "EMAIL")
									strEMAIL = hnode.InnerText;
							}
						}
						ok = true;
					}

				}
			}
			if (ok)
			{
				try
				{
					using (SqlConnection cn = new SqlConnection(prop.Connection_string))
					{
						cn.Open();
						// проверяем на 9-й категорию прайса
						SqlCommand cmd = new SqlCommand("DECLARE @C int; SET @c = (SELECT COUNT(*) FROM [dbo].[category] WHERE [id_category] = 9); IF(@C = 0) BEGIN; INSERT INTO [dbo].[category]([id_category], [name], [input]) VALUES (9, 'Фототерминалы', 0); END;", cn);
						cmd.ExecuteNonQuery();

						//Проверяем на наличие клиента с имененм "Клиент МФОТО" и 9-ой категории прайса
						cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_client] FROM [dbo].[client] WHERE RTRIM([name]) = 'Клиент МФОТО'); IF(@ID > 0) BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[client] ([id_category], [id_dcard], [guid], [del], [name], [phone_1], [phone_2], [address], [email], [icq], [addon]) VALUES (9, 0, newid(), 0, 'Клиент МФОТО', '', '', '', '', '', ''); SET @ID = scope_identity(); SELECT @ID; END;", cn);
						int client_id = (int)cmd.ExecuteScalar();

						//Проверяем на наличие услуги с кодом -1
						cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_good] FROM [dbo].[good] WHERE RTRIM([id_good]) = '-1'); IF(@ID = '-1') BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[good] ([id_good], [guid], [del], [name], [description], [prefix], [folder], [type], [checked], [zero], [sign], [apply_form], [EI], [bonustype], [kiosk_name]) VALUES('-1', newid(), 0, 'УСЛУГА НЕ НАЙДЕНА', 'УСЛУГА НЕ НАЙДЕНА', '', '-1', '1', 0, 1, 'none-1', '', 0, '', ''); SET @ID = scope_identity(); SELECT @ID; END;", cn);
						cmd.ExecuteNonQuery();

						//Проверяем наличие пользователя "МФОТО"
						cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_user] FROM [dbo].[user] WHERE RTRIM([name]) = 'МФОТО'); IF(@ID > 0) BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[user]([name], [сontact], [login], [password], [permission]) VALUES ('МФОТО','','МФОТО','vajnj',0); SET @ID = scope_identity(); SELECT @ID; END;", cn);
						int user_id = (int)cmd.ExecuteScalar();

						string orderNumber = prop.Order_terminal_prefics.Substring(0, 1) + "4" + int.Parse(barcode).ToString("D10");
						
						cmd = new SqlCommand("SELECT * FROM [order] WHERE [number] = " + orderNumber, cn);
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						DataTable t_order = new DataTable();
						da.Fill(t_order);
						string query = "BEGIN TRANSACTION;\n\n";
						bool add = false;
						if (t_order.Rows.Count > 0)
						{
							if (
								(decimal.Parse(t_order.Rows[0]["advanced_payment"].ToString()) > 0) ||
								(decimal.Parse(t_order.Rows[0]["final_payment"].ToString()) > 0)
							)
								MessageBox.Show("Заказ с номером " + orderNumber + " уже существует в базе и по нему принималась оплата, изменение этого заказа невозможно!");
							else
							{


								if (MessageBox.Show("Заказ с номером " + orderNumber + " уже существует в базе, но оплата по нему не принималась.\nОтменить его и импортировать новый?", "Импорт", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
								{
									add = true;
									query += "UPDATE [order] SET \n\t[status] = '010000', \n\texported = 0, \n\t[number] = '" + prop.Order_terminal_prefics.Substring(0, 1) + "9" + int.Parse(barcode).ToString("D10") + "' \nWHERE \n\t[number] = " + orderNumber + ";\n";
									query += "INSERT INTO [dbo].[orderevent] \n" +
											   "([del] \n" +
											   ",[guid] \n" +
											   ",[id_order] \n" +
											   ",[event_date] \n" +
											   ",[event_user] \n" +
											   ",[event_status] \n" +
											   ",[event_point] \n" +
											   ",[event_text]) \n" +
										 "VALUES \n" +
											   "(0 \n" +
											   ",newid() \n" +
											   "," + t_order.Rows[0]["id_order"].ToString() + "\n" +
											   ",getdate() \n" +
											   "," + usr.Id_user + " \n" +
											   ",'" + t_order.Rows[0]["status"].ToString() + "' \n" +
											   ",'" + prop.Order_prefics + "' \n" +
											   ",'Повторное импортирование заказа! Заказ отменятся и получает новый номер.');\n";

								}
							}

						}
						else
						{
							add = true;
						}
						if (add)
						{
							// определяем ключи для услуг
							cmd = new SqlCommand("SELECT id_good, type FROM dbo.good WHERE (kiosk_name LIKE '%|" + strPRODUCT_NAME + "|%')", cn);
							SqlDataAdapter da_tmp = new SqlDataAdapter(cmd);
							DataSet ds = new DataSet();
							da_tmp.Fill(ds, "goods");
							if (ds.Tables[0].Rows.Count > 0)
							{

								strKEY = ds.Tables[0].Rows[0][0].ToString().Trim();
								strPATH = ds.Tables[0].Rows[0][1].ToString().Trim();
							}
							else
							{
								strKEY = "-1";
								strPATH = "1";
							}

							//клиент
							if (prop.Terminal_client_one)
								query += "DECLARE @CLIENTID int;\n" +
									"SET @CLIENTID = " + client_id + ";\n" +
									"DECLARE @CLIENTNAME nchar(255);\n" +
									"SET @CLIENTNAME = 'Клиент МФОТО'\n";
							else
								query += "DECLARE @name nchar(255)\n" +
										"DECLARE @phone nchar(255)\n" +
										"SET @name = '" + strLAST_NAME + " " + strFIRST_NAME + "%'\n" +
										"SET @phone = '" + strPHONE + "'\n" +
										"DECLARE @CNT int\n" +
										"DECLARE @CLIENTID int;\n" +
										"SET @CLIENTID = 0;\n" +
										"SET @CNT = (\n" +
										"SELECT COUNT(*)\n" +
										"FROM [dbo].[client]\n" +
										"WHERE [name] like @name\n" +
										"  AND [id_category] = 9\n" +
										"  AND [phone_1] = @phone\n" +
										")\n" +
										"IF (@CNT = 0)\n" +
										"BEGIN\n" +
										"	INSERT INTO [dbo].[client]\n" +
										"			   ([id_category]\n" +
										"			   ,[guid]\n" +
										"			   ,[del]\n" +
										"			   ,[name]\n" +
										"			   ,[phone_1]\n" +
										"			   )\n" +
										"		 VALUES\n" +
										"			   (9\n" +
										"			   ,newid()\n" +
										"			   ,0\n" +
										"			   ,'" + strLAST_NAME + " " + strFIRST_NAME + "'\n" +
										"			   ,'" + strPHONE + "')\n" +
										"	SET @CLIENTID = scope_identity()\n" +
										"END\n" +
										"IF (@CNT = 1)\n" +
										"BEGIN\n" +
										"	SET @CLIENTID = (\n" +
										"		SELECT [id_client]\n" +
										"		FROM [dbo].[client]\n" +
										"		WHERE [name] like @name\n" +
										"		  AND [id_category] = 9\n" +
										"		  AND [phone_1] = @phone\n" +
										"	)\n" +
										"END\n" +
										"IF (@CNT > 1)\n" +
										"BEGIN\n" +
										"	SET @CLIENTID = (\n" +
										"		SELECT MAX([id_client]) AS id_client\n" +
										"		FROM dbo.client\n" +
										"		WHERE [name] like @name\n" +
										"		  AND [id_category] = 9\n" +
										"		  AND [phone_1] = @phone\n" +
										"	)\n" +
										"END\n" +
										"SELECT @CLIENTID;\n" +
										"DECLARE @CLIENTNAME nchar(255);\n" +
										"SET @CLIENTNAME = '" + strLAST_NAME + " " + strFIRST_NAME + "'\n";

							// Шапка
							query += "INSERT INTO [dbo].[order]\n" +
								   "([id_user_accept] \n" +
								   ",[id_user_operator] \n" +
								   ",[id_user_designer] \n" +
								   ",[id_user_delivery] \n" +
								   ",[id_client] \n" +
								   ",[guid] \n" +
								   ",[del] \n" +
								   ",[name_accept] \n" +
								   ",[name_operator] \n" +
								   ",[name_designer] \n" +
								   ",[name_delivery] \n" +
								   ",[status] \n" +
								   ",[number] \n" +
								   ",[input_date] \n" +
								   ",[expected_date] \n" +
								   ",[advanced_payment] \n" +
								   ",[final_payment] \n" +
								   ",[discont_percent] \n" +
								   ",[discont_code] \n" +
								   ",[preview] \n" +
								   ",[comment] \n" +
								   ",[crop] \n" +
								   ",[type] \n" +
								   ",[exported] \n" +
								   ",[bonus]) \n" +
							 "VALUES \n" +
								   "(" + usr.Id_user + " \n" +
								   "," + user_id + " \n" +
								   ",0 \n" +
								   ",0 \n" +
								   ",@CLIENTID \n" +
								   ",NEWID() \n" +
								   ",0 \n" +
								   ",'" + usr.Name + "' \n" +
								   ",'" + "МФОТО" + "' \n" +
								   ",'' \n" +
								   ",'' \n" +
								   ",'000100' \n" +
								   ",'" + orderNumber + "' \n" +
								   ",GETDATE() \n" +
								   ",DATEADD(hour, 1, GETDATE()) \n" +
								   ",0 \n" +
								   ",0 \n" +
								   ",0 \n" +
								   ",'' \n" +
								   ",0 \n" +
								   ",'" + strDESCRIPTION + " Клиент:" + strLAST_NAME + " " + strFIRST_NAME + " Тел:" + strPHONE + "' \n" +
								   ",1 \n" +
								   ",1 \n" +
								   ",0 \n" +
								   ",0);\n";
							// Код нового заказа
							query += "DECLARE @ID int;\n";
							query += "SET @ID = SCOPE_IDENTITY();\n";

							//Табличная часть
							query += "INSERT INTO [dbo].[orderbody] \n" +
								   "([id_order] \n" +
								   ",[id_mashine] \n" +
								   ",[id_material] \n" +
								   ",[id_good] \n" +
								   ",[guid] \n" +
								   ",[del] \n" +
								   ",[quantity] \n" +
								   ",[actual_quantity] \n" +
								   ",[sign] \n" +
								   ",[price] \n" +
								   ",[dateadd] \n" +
								   ",[id_user_add] \n" +
								   ",[name_add] \n" +
								   ",[exported] \n" +
								   ",[comment]) \n" +
							 "VALUES \n" +
								   "(@ID \n" +
								   ",0 \n" +
								   ",0 \n" +
								   ",'" + strKEY + "' \n" +
								   ",newid() \n" +
								   ",0 \n" +
								   "," + strCOPIES + " \n" +
								   "," + strCOPIES + " \n" +
								   ",'+' \n" +
								   "," + (decimal.Parse(strTOTAL_PRICE) / decimal.Parse(strCOPIES)).ToString() + " \n" +
								   ",getdate() \n" +
								   "," + usr.Id_user + " \n" +
								   ",'" + usr.Name + "' \n" +
								   ",0\n" +
								   ",'" + strPRODUCT_NAME + " Copies:" + strCOPIES + " Total price:" + strTOTAL_PRICE + "р');\n";

							query += "INSERT INTO [dbo].[orderevent] \n" +
									   "([del] \n" +
									   ",[guid] \n" +
									   ",[id_order] \n" +
									   ",[event_date] \n" +
									   ",[event_user] \n" +
									   ",[event_status] \n" +
									   ",[event_point] \n" +
									   ",[event_text]) \n" +
								 "VALUES \n" +
									   "(0 \n" +
									   ",newid() \n" +
									   ",@ID \n" +
									   ",getdate() \n" +
									   "," + usr.Id_user + " \n" +
									   ",'000111' \n" +
									   ",'" + prop.Order_prefics + "' \n" +
									   ",'Заказ был импортирован с сервера МФОТО (" + usr.Name + ").');\n";
							//query += "INSERT INTO [dbo].[kiosk_orders_ok] ([number], [status], [exported]) VALUES ('" + prop.Order_terminal_prefics.Substring(0, 1) + "2" + int.Parse(order).ToString("D10") + "', 1, 0);\n";
							query += "COMMIT;\n\n";
							cmd.CommandText = query;
							cmd.Connection = cn;
							cmd.CommandTimeout = 15000;
							cmd.ExecuteNonQuery();
							try
							{
								if (Directory.Exists(prop.MfotoAlbumsPath + "\\" + barcode))
								{
									fs.CopyDirectory(prop.MfotoAlbumsPath + "\\" + barcode, prop.Dir_print + "\\" + DateTime.Now.Year.ToString("D4") + "\\" + DateTime.Now.Month.ToString("D2") + "\\" + DateTime.Now.Day.ToString("D2") + "\\" + orderNumber + "\\", true);
								}
							}
							catch (Exception ex)
							{
								MessageBox.Show("Ошибка копирования файлов заказа МФото\n" + ex.Message);
							}
						}
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message + "\n" + ex.Source);
				}
			}
			else
			{
				if (take == 0)
					MessageBox.Show("Проблема со связью, информация о заказе не найдена.");
				else
					MessageBox.Show("Информация о заказе не найдена.");
			}
		}
	}
}
