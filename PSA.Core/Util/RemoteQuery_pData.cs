using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Data;
using PSA.Lib.Interface;
using System.IO;
using System.Data.SqlClient;


namespace PSA.Lib.Util
{
    public partial class RemoteQuery
    {
        private void QueryData_p(string order)
        {
            /*
            XmlDocument xml = new XmlDocument();
            Dictionary<string, string> head = new Dictionary<string, string>();
            string m = "";
            string root_dir = System.Environment.GetCommandLineArgs()[0].Substring(
                0,
                System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
                );
            DataTable od = new DataTable("orderdetail");
            od.Columns.Add("ACTION_ID");
            od.Columns.Add("SUBACTION_ID");
            od.Columns.Add("QTY");
            od.Columns.Add("PRICE");
            od.Columns.Add("ACTION_NAME");
            od.Columns.Add("ACTION_HEADER");
            od.Columns.Add("ACTION_PRICE");
            od.Columns.Add("SUBACTION_NAME");
            od.Columns.Add("SUBACTION_HEADER");
            od.Columns.Add("SUBACTION_PRICE");
            od.Columns.Add("KEY");
            od.Columns.Add("COMMENT");
            od.Columns.Add("PATH");
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
                            head.Add("NUMBER", t_number);
                            head.Add("KIOSKID", t_kiosk);
                            head.Add("ADDRESS", t_address);
                            head.Add("STAMP", t_stamp);
                            head.Add("CUSTOMER", ((t_customer == "") ? "Клиент фототерминала" : t_customer));
                            head.Add("PHONE", t_phone);
                            head.Add("STATUS", t_status);
                            head.Add("TOPRINT_AUTHDATE", t_do);
                            head.Add("PRINTED_AUTHDATE", t_dp);
                            head.Add("SHIP_AUTHDATE", t_dv);
                            m += "Киоск №" + t_kiosk + ", " + t_address + " Заказ: " + t_number + ", принят " + t_stamp + " Клиент: " + ((t_customer == "") ? "Клиент фототерминала" : t_customer) + ((t_phone != "") ? ", телефон: " + t_phone : "") + " " + ((t_do != "") ? " Отправлено в печать: " + t_do : "") + ((t_dp != "") ? " Напечатано: " + t_dp : "") + ((t_dv != "") ? " Отправлено в центр выдачи: " + t_dv : "") + " СТАТУС: " + t_status + " (данные получены: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ")\n";

                        }
                        if (node.Name == "BODY")
                        {
                            foreach (XmlNode bnode in node.ChildNodes)
                            {
                                if (bnode.Name == "ROW")
                                {
                                    DataRow r = od.NewRow();
                                    foreach (XmlNode rnode in bnode.ChildNodes)
                                    {
                                        if (rnode.Name == "ACTION_ID")
                                            r["ACTION_ID"] = rnode.InnerText;
                                        if (rnode.Name == "ACTION_NAME")
                                            r["ACTION_NAME"] = rnode.InnerText;
                                        if (rnode.Name == "ACTION_HEADER")
                                            r["ACTION_HEADER"] = rnode.InnerText;
                                        if (rnode.Name == "ACTION_PRICE")
                                            r["ACTION_PRICE"] = rnode.InnerText;
                                        if (rnode.Name == "SUBACTION_ID")
                                            r["SUBACTION_ID"] = rnode.InnerText;
                                        if (rnode.Name == "SUBACTION_NAME")
                                            r["SUBACTION_NAME"] = rnode.InnerText;
                                        if (rnode.Name == "SUBACTION_PRICE")
                                            r["SUBACTION_PRICE"] = rnode.InnerText;
                                        if (rnode.Name == "QTY")
                                            r["QTY"] = rnode.InnerText;
                                        if (rnode.Name == "PRICE")
                                            r["PRICE"] = rnode.InnerText;
                                    }
                                    od.Rows.Add(r);
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
                    if (File.Exists(root_dir + "\\cache\\korder.xml"))
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
                                    head.Add("NUMBER", t_number);
                                    head.Add("KIOSKID", t_kiosk);
                                    head.Add("ADDRESS", t_address);
                                    head.Add("STAMP", t_stamp);
                                    head.Add("CUSTOMER", ((t_customer == "") ? "Клиент фототерминала" : t_customer));
                                    head.Add("PHONE", t_phone);
                                    head.Add("STATUS", t_status);
                                    head.Add("TOPRINT_AUTHDATE", t_do);
                                    head.Add("PRINTED_AUTHDATE", t_dp);
                                    head.Add("SHIP_AUTHDATE", t_dv);
                                    m += "Киоск №" + t_kiosk + ", " + t_address + " Заказ: " + t_number + ", принят " + t_stamp + " Клиент: " + ((t_customer == "") ? "Клиент фототерминала" : t_customer) + ((t_phone != "") ? ", телефон: " + t_phone : "") + " " + ((t_do != "") ? " Отправлено в печать: " + t_do : "") + ((t_dp != "") ? " Напечатано: " + t_dp : "") + ((t_dv != "") ? " Отправлено в центр выдачи: " + t_dv : "") + " СТАТУС: " + t_status + " (данные получены: " + t_a + ")\n";
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
                                            DataRow r = od.NewRow();
                                            foreach (XmlNode rnode in bnode.ChildNodes)
                                            {
                                                if (rnode.Name == "ACTION_ID")
                                                    r["ACTION_ID"] = rnode.InnerText;
                                                if (rnode.Name == "ACTION_NAME")
                                                    r["ACTION_NAME"] = rnode.InnerText;
                                                if (rnode.Name == "ACTION_HEADER")
                                                    r["ACTION_HEADER"] = rnode.InnerText;
                                                if (rnode.Name == "ACTION_PRICE")
                                                    r["ACTION_PRICE"] = rnode.InnerText;
                                                if (rnode.Name == "SUBACTION_ID")
                                                    r["SUBACTION_ID"] = rnode.InnerText;
                                                if (rnode.Name == "SUBACTION_NAME")
                                                    r["SUBACTION_NAME"] = rnode.InnerText;
                                                if (rnode.Name == "SUBACTION_PRICE")
                                                    r["SUBACTION_PRICE"] = rnode.InnerText;
                                                if (rnode.Name == "QTY")
                                                    r["QTY"] = rnode.InnerText;
                                                if (rnode.Name == "PRICE")
                                                    r["PRICE"] = rnode.InnerText;
                                            }
                                            od.Rows.Add(r);
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
            if ((ok) && (m != "") && (od.Rows.Count != 0))
            {
                try
                {
                    using (SqlConnection cn = new SqlConnection(prop.Connection_string))
                    {
                        cn.Open();
                        // проверяем на 9-й категорию прайса
                        SqlCommand cmd = new SqlCommand("DECLARE @C int; SET @c = (SELECT COUNT(*) FROM [dbo].[category] WHERE [id_category] = 9); IF(@C = 0) BEGIN; INSERT INTO [dbo].[category]([id_category], [name], [input]) VALUES (9, 'Фототерминалы', 0); END;", cn);
                        cmd.ExecuteNonQuery();

                        //Проверяем на наличие клиента с имененм "Фототерминал" и 9-ой категории прайса
                        cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_client] FROM [dbo].[client] WHERE RTRIM([name]) = 'Фототерминал'); IF(@ID > 0) BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[client] ([id_category], [id_dcard], [guid], [del], [name], [phone_1], [phone_2], [address], [email], [icq], [addon]) VALUES (9, 0, newid(), 0, 'Фототерминал', '', '', '', '', '', ''); SET @ID = scope_identity(); SELECT @ID; END;", cn);
                        int client_id = (int)cmd.ExecuteScalar();

                        //Проверяем на наличие услуги с кодом -1
                        cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_good] FROM [dbo].[good] WHERE RTRIM([id_good]) = '-1'); IF(@ID = '-1') BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[good] ([id_good], [guid], [del], [name], [description], [prefix], [folder], [type], [checked], [zero], [sign], [apply_form], [EI], [bonustype], [kiosk_name]) VALUES('-1', newid(), 0, 'УСЛУГА НЕ НАЙДЕНА', 'УСЛУГА НЕ НАЙДЕНА', '', '-1', '1', 0, 1, 'none-1', '', 0, '', ''); SET @ID = scope_identity(); SELECT @ID; END;", cn);
                        cmd.ExecuteNonQuery();

                        //Проверяем наличие пользователя "Фототерминал"
                        cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_user] FROM [dbo].[user] WHERE RTRIM([name]) = 'Фототерминал'); IF(@ID > 0) BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[user]([name], [сontact], [login], [password], [permission]) VALUES ('Фототерминал','','Фототерминал','ajnjnthvbyfk',0); SET @ID = scope_identity(); SELECT @ID; END;", cn);
                        int user_id = (int)cmd.ExecuteScalar();

                        cmd = new SqlCommand("SELECT * FROM [order] WHERE [number] = " + prop.Order_terminal_prefics + int.Parse(kiosk).ToString("D3") + int.Parse(order).ToString("D7"), cn);
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
                                MessageBox.Show("Заказ с номером " + prop.Order_terminal_prefics + int.Parse(kiosk).ToString("D3") + int.Parse(order).ToString("D7") + " уже существует в базе и по нему принималась оплата, изменение этого заказа невозможно!");
                            else
                            {


                                if (MessageBox.Show("Заказ с номером " + prop.Order_terminal_prefics + int.Parse(kiosk).ToString("D3") + int.Parse(order).ToString("D7") + " уже существует в базе, но оплата по нему не принималась.\nОтменить его и импортировать новый?", "Импорт", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    add = true;
                                    query += "UPDATE [order] SET \n\t[status] = '010000', \n\texported = 0, \n\t[number] = '" + prop.Order_terminal_prefics.Substring(0, 1) + "9" + int.Parse(kiosk).ToString("D3") + int.Parse(order).ToString("D7") + "' \nWHERE \n\t[number] = " + prop.Order_terminal_prefics + int.Parse(kiosk).ToString("D3") + int.Parse(order).ToString("D7") + ";\n";
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
                            for (int j = 0; j < od.Rows.Count; j++)
                            {
                                // определяем ключи для услуг
                                cmd = new SqlCommand("SELECT id_good, type FROM dbo.good WHERE (kiosk_name LIKE '%|" + od.Rows[j]["ACTION_HEADER"].ToString().Trim() + " " + od.Rows[j]["ACTION_NAME"].ToString().Trim() + "|%')", cn);
                                SqlDataAdapter da_tmp = new SqlDataAdapter(cmd);
                                DataSet ds = new DataSet();
                                da_tmp.Fill(ds, "goods");
                                if (ds.Tables[0].Rows.Count > 0)
                                {

                                    od.Rows[j]["KEY"] = ds.Tables[0].Rows[0][0].ToString().Trim();
                                    od.Rows[j]["PATH"] = ds.Tables[0].Rows[0][1].ToString().Trim();
                                }
                                else
                                {
                                    od.Rows[j]["KEY"] = "-1";
                                    od.Rows[j]["PATH"] = "1";
                                }
                            }

                            //клиент
                            if (prop.Terminal_client_one)
                                query += "DECLARE @CLIENTID int;\n" +
                                    "SET @CLIENTID = " + client_id + ";\n" +
                                    "DECLARE @CLIENTNAME nchar(255);\n" +
                                    "SET @CLIENTNAME = 'фототерминал'\n";
                            else
                                query += "DECLARE @name nchar(255)\n" +
                                        "DECLARE @phone nchar(255)\n" +
                                        "SET @name = '" + head["CUSTOMER"] + "%'\n" +
                                        "SET @phone = '" + head["PHONE"] + "'\n" +
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
                                        "			   ,'" + head["CUSTOMER"] + "'\n" +
                                        "			   ,'" + head["PHONE"] + "')\n" +
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
                                        "SET @CLIENTNAME = '" + head["CUSTOMER"] + "'\n";

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
                                   ",0 \n" +
                                   ",0 \n" +
                                   ",0 \n" +
                                   ",@CLIENTID \n" +
                                   ",NEWID() \n" +
                                   ",0 \n" +
                                   ",'" + usr.Name + "' \n" +
                                   ",'' \n" +
                                   ",'' \n" +
                                   ",'' \n" +
                                   ",'000010' \n" +
                                   ",'" + prop.Order_terminal_prefics +
                                   int.Parse(kiosk).ToString("D3") +
                                   int.Parse(order).ToString("D7") + "' \n" +
                                   ",GETDATE() \n" +
                                   ",DATEADD(hour, 1, GETDATE()) \n" +
                                   ",0 \n" +
                                   ",0 \n" +
                                   ",0 \n" +
                                   ",'' \n" +
                                   ",0 \n" +
                                   ",'" + m + "' \n" +
                                   ",1 \n" +
                                   ",1 \n" +
                                   ",0 \n" +
                                   ",0);\n";
                            // Код нового заказа
                            query += "DECLARE @ID int;\n";
                            query += "SET @ID = SCOPE_IDENTITY();\n";

                            for (int j = 0; j < od.Rows.Count; j++)
                            {
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
                                       ",'" + od.Rows[j]["KEY"].ToString().Trim() + "' \n" +
                                       ",newid() \n" +
                                       ",0 \n" +
                                       "," + od.Rows[j]["QTY"].ToString().Trim().Replace(',', '.') + " \n" +
                                       "," + od.Rows[j]["QTY"].ToString().Trim().Replace(',', '.') + " \n" +
                                       ",'+' \n" +
                                       "," + od.Rows[j]["PRICE"].ToString().Trim().Replace(',', '.') + " \n" +
                                       ",getdate() \n" +
                                       "," + usr.Id_user + " \n" +
                                       ",'" + usr.Name + "' \n" +
                                       ",0\n" +
                                       ",'" + od.Rows[j]["ACTION_HEADER"].ToString().Trim() + " " + od.Rows[j]["ACTION_NAME"].ToString().Trim() + " " + od.Rows[j]["QTY"].ToString().Trim() + "*" + od.Rows[j]["ACTION_PRICE"].ToString().Trim() + "р; " + od.Rows[j]["SUBACTION_NAME"].ToString().Trim() + " " + od.Rows[j]["SUBACTION_PRICE"].ToString().Trim() + "р');\n";
                            }
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
                                       ",'000010' \n" +
                                       ",'" + prop.Order_prefics + "' \n" +
                                       ",'Заказ был импортирован с сервера.');\n";
                            query += "INSERT INTO [dbo].[kiosk_orders_ok] ([number], [status], [exported]) VALUES ('" + prop.Order_terminal_prefics +
                                   int.Parse(kiosk).ToString("D3") +
                                   int.Parse(order).ToString("D7") + "', 1, 0);\n";
                            query += "COMMIT;\n\n";
                            cmd.CommandText = query;
                            cmd.Connection = cn;
                            cmd.CommandTimeout = 15000;
                            cmd.ExecuteNonQuery();
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
        */
        }
    }
}
