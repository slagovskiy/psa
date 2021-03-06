﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.Data;
using PSA.Lib.Interface;
using System.IO;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Security.Cryptography;


namespace PSA.Lib.Util
{
    public partial class RemoteQuery
    {
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA1.Create();  // SHA1.Create()
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private string Win1251ToUTF8(string source)
        {

            Encoding win1251 = Encoding.GetEncoding("windows-1251");

            byte[] win1251Bytes = win1251.GetBytes(source);
            source = Encoding.UTF8.GetString(win1251Bytes);
            return source;

        }

        public bool QueryData_p(string order, string status)
        {
            string token = "";
            string password = "";
            string publicKey = prop.PublicKey;
            string privateKey = prop.PrivateKey;
            string accessToken = "";
            bool accessOK = false;
            bool qq = false;

            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                string webData = wc.DownloadString(prop.ApiRequestToken);
                Dictionary<string, string> jToken = JsonConvert.DeserializeObject<Dictionary<string, string>>(webData);
                foreach (KeyValuePair<string, string> tmp in jToken)
                    if (tmp.Key == "RequestToken")
                    {
                        token = tmp.Value;
                        break;
                    }
                password = GetHashString(token + privateKey);

                try
                {
                    wc = new System.Net.WebClient();
                    webData = wc.DownloadString(prop.ApiAccessToken + "?oauth_token=" + token +
                        "&grant_type=api&username=" + publicKey + "&password=" + password);
                    jToken = JsonConvert.DeserializeObject<Dictionary<string, string>>(webData);
                    foreach (KeyValuePair<string, string> tmp in jToken)
                    {
                        if (tmp.Key == "AccessToken")
                            accessToken = tmp.Value;
                        if ((tmp.Key == "Success") && (tmp.Value == "True"))
                        {
                            accessOK = true;
                        }
                    }
                    if (accessOK)
                    {
                        Dictionary<string, string> head = new Dictionary<string, string>();
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
                        string user_fname = "", user_lname = "", user_email = "", user_phone = "", user_address = "";

                        try
                        {
                            wc = new System.Net.WebClient();
                            webData = wc.DownloadString(prop.ApiOrder + order + "?oauth_token=" + accessToken);
                            webData = Win1251ToUTF8(webData);
                            webData = "{\"DATA\":[" + webData + "]}";
                            XmlDocument x = (XmlDocument)JsonConvert.DeserializeXmlNode(webData);

                            foreach (XmlNode r in x.GetElementsByTagName("Result"))
                            {
                                foreach (XmlNode node in r.ChildNodes)
                                {
                                    if (node.Name == "UserId")
                                    {
                                        wc = new System.Net.WebClient();
                                        webData = wc.DownloadString(prop.ApiUser + node.InnerText + "?oauth_token=" + accessToken);
                                        webData = Win1251ToUTF8(webData);
                                        webData = "{\"DATA\":[" + webData + "]}";
                                        XmlDocument xx = (XmlDocument)JsonConvert.DeserializeXmlNode(webData);
                                        foreach (XmlNode rr in xx.GetElementsByTagName("Result"))
                                        {
                                            foreach (XmlNode nodee in rr.ChildNodes)
                                            {
                                                if (nodee.Name == "Email")
                                                    user_email = nodee.InnerText;
                                                if (nodee.Name == "FirstName")
                                                    user_fname = nodee.InnerText;
                                                if (nodee.Name == "LastName")
                                                    user_lname = nodee.InnerText;
                                                if (nodee.Name == "Phone")
                                                    user_phone = nodee.InnerText;
                                            }
                                        }
                                    }
                                    if (node.Name == "DeliveryAddress")
                                    {
                                        foreach (XmlNode rr in node.ChildNodes)
                                        {
                                            if (rr.Name == "AddressLine1")
                                                user_address = rr.InnerText.Replace('\n', ' ').Replace('\r', ' ');
                                            if (user_address.IndexOf("овосибирск, ") > 0)
                                                user_address = user_address.Replace("Новосибирск, ", "");
                                            if ((user_address.IndexOf('(') > 0) && (user_address.IndexOf(')') > 0))
                                                user_address = user_address.Replace(
                                                    user_address.Substring(
                                                    user_address.IndexOf('('), user_address.IndexOf(')') + 1 - user_address.IndexOf('(')
                                                    ), "").Trim();
                                        }
                                    }
                                }
                            }

                            string t_kiosk = "", t_address = "", t_stamp = "", t_customer = "", t_phone = "", t_number = "", t_status = "", t_do = "", t_dp = "", t_dv = "", m = "";
                            t_number = order;
                            //foreach (KeyValuePair<string, string> tmp in jData)
                            //{
                                //if (tmp.Key == "TotalPrice")
                            //}
                            head.Add("NUMBER", t_number);
                            head.Add("KIOSKID", "");
                            head.Add("ADDRESS", "");
                            head.Add("STAMP", "");
                            head.Add("CUSTOMER", "Клиент PixlPark");
                            head.Add("PHONE", "");
                            head.Add("STATUS", status);
                            head.Add("TOPRINT_AUTHDATE", "");
                            head.Add("PRINTED_AUTHDATE", "");
                            head.Add("SHIP_AUTHDATE", "");
                            m += "Киоск №" + t_kiosk + ", " + t_address + " Заказ: " + t_number + ", принят " + t_stamp + " Клиент: " + ((user_fname == "") ? "Клиент PixlPark" : user_fname + " " + user_lname) + ((t_phone != "") ? ", телефон: " + t_phone : "") + " " + ((t_do != "") ? " Отправлено в печать: " + t_do : "") + ((t_dp != "") ? " Напечатано: " + t_dp : "") + ((t_dv != "") ? " Отправлено в центр выдачи: " + t_dv : "") + " СТАТУС: " + t_status + " (данные получены: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "){" + user_fname + " " + user_lname + " [" + user_address + "]}" + "\n";


                            wc = new System.Net.WebClient();
                            webData = wc.DownloadString(prop.ApiOrderItems + order + "/items/?oauth_token=" + accessToken);
                            webData = Win1251ToUTF8(webData);
                            webData = "{\"DATA\":[" + webData + "]}";
                            x = (XmlDocument)JsonConvert.DeserializeXmlNode(webData);

                            foreach (XmlNode r in x.GetElementsByTagName("Result"))
                            {
                                DataRow rw = od.NewRow();
                                foreach (XmlNode node in r.ChildNodes)
                                {
                                    if (node.Name == "Id")
                                        rw["ACTION_ID"] = node.InnerText;
                                    if (node.Name == "Name")
                                        rw["ACTION_NAME"] = node.InnerText;
                                    if (node.Name == "Quantity")
                                        rw["QTY"] = node.InnerText;
                                    if (node.Name == "TotalPrice")
                                        rw["PRICE"] = node.InnerText;
                                    if (node.Name == "Options")
                                    {

                                        XmlDocument xx = new XmlDocument();
                                        xx.LoadXml("<DATA>" + node.InnerXml + "</DATA>");
                                        foreach (XmlNode rr in xx.GetElementsByTagName("DATA"))
                                        {
                                            foreach (XmlNode nodee in rr.ChildNodes)
                                            {
                                                if (nodee.Name == "Title")
                                                    rw["ACTION_NAME"] += " [" + nodee.InnerText + "]";
                                            }
                                        }
                                    }

                                }
                                rw["PRICE"] = (decimal.Parse(rw["PRICE"].ToString().Replace('.', ',')) / decimal.Parse(rw["QTY"].ToString().Replace('.', ','))).ToString();
                                od.Rows.Add(rw);
                            }
                            /*
                            webData = webData
                                .Replace("{\"ApiVersion\":\"1.0\",\"Result\":[", " ")
                                .Replace("],\"ResponseCode\":200}", "");
                            while (webData.IndexOf('{') > 0)
                            {
                                string _webData = webData.Substring(
                                    webData.IndexOf('{'), webData.IndexOf('}') - webData.IndexOf('{') + 1).Replace("[]", "\"\"");
                                while (_webData.IndexOf("[\"") > 0)
                                {
                                    _webData = _webData.Replace(
                                        _webData.Substring(
                                            _webData.IndexOf("[\""), _webData.IndexOf("\"]") - _webData.IndexOf("[\"") + 2
                                        ), "\"\"");
                                }
                                jData = JsonConvert.DeserializeObject<Dictionary<string, string>>(_webData);

                                DataRow r = od.NewRow();
                                foreach (KeyValuePair<string, string> tmp in jData)
                                {
                                    if (tmp.Key == "Id")
                                        r["ACTION_ID"] = tmp.Value;
                                    if (tmp.Key == "Name")
                                        r["ACTION_NAME"] = tmp.Value;
                                    if (tmp.Key == "--")
                                        r["ACTION_HEADER"] = tmp.Value;
                                    r["ACTION_PRICE"] = tmp.Value;
                                    r["SUBACTION_ID"] = tmp.Value;
                                    r["SUBACTION_NAME"] = tmp.Value;
                                    r["SUBACTION_PRICE"] = tmp.Value;
                                    if (tmp.Key == "Quantity")
                                        r["QTY"] = tmp.Value;
                                    if (tmp.Key == "ItemPrice")
                                        r["PRICE"] = tmp.Value;
                                }
                                od.Rows.Add(r);
                                webData = webData.Replace(
                                    webData.Substring(
                                        webData.IndexOf('{'), webData.IndexOf('}') - webData.IndexOf('{') + 1
                                    ), "");
                            }
                            */
                            try
                            {
                                using (SqlConnection cn = new SqlConnection(prop.Connection_string))
                                {
                                    cn.Open();
                                    // проверяем на 9-й категорию прайса
                                    SqlCommand cmd = new SqlCommand("DECLARE @C int; SET @c = (SELECT COUNT(*) FROM [dbo].[category] WHERE [id_category] = 9); IF(@C = 0) BEGIN; INSERT INTO [dbo].[category]([id_category], [name], [input]) VALUES (9, 'Фототерминалы', 0); END;", cn);
                                    cmd.CommandTimeout = 9000;
                                    cmd.ExecuteNonQuery();

                                    //Проверяем на наличие клиента с имененм "PixlPark" и 9-ой категории прайса
                                    cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_client] FROM [dbo].[client] WHERE RTRIM([name]) = 'PixlPark'); IF(@ID > 0) BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[client] ([id_category], [id_dcard], [guid], [del], [name], [phone_1], [phone_2], [address], [email], [icq], [addon]) VALUES (9, 0, newid(), 0, 'PixlPark', '', '', '', '', '', ''); SET @ID = scope_identity(); SELECT @ID; END;", cn);
                                    cmd.CommandTimeout = 9000;
                                    int client_id = (int)cmd.ExecuteScalar();

                                    //Проверяем на наличие услуги с кодом -1
                                    cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_good] FROM [dbo].[good] WHERE RTRIM([id_good]) = '-1'); IF(@ID = '-1') BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[good] ([id_good], [guid], [del], [name], [description], [prefix], [folder], [type], [checked], [zero], [sign], [apply_form], [EI], [bonustype], [kiosk_name]) VALUES('-1', newid(), 0, 'УСЛУГА НЕ НАЙДЕНА', 'УСЛУГА НЕ НАЙДЕНА', '', '-1', '1', 0, 1, 'none-1', '', 0, '', ''); SET @ID = scope_identity(); SELECT @ID; END;", cn);
                                    cmd.CommandTimeout = 9000;
                                    cmd.ExecuteNonQuery();

                                    //Проверяем наличие пользователя "PixlPark"
                                    cmd = new SqlCommand("DECLARE @ID int; SET @ID = (SELECT [id_user] FROM [dbo].[user] WHERE RTRIM([name]) = 'PixlPark'); IF(@ID > 0) BEGIN; SELECT @ID; END; ELSE BEGIN; INSERT INTO [dbo].[user]([name], [сontact], [login], [password], [permission]) VALUES ('PixlPark','','PixlPark','ajnjnthvbyfk',0); SET @ID = scope_identity(); SELECT @ID; END;", cn);
                                    cmd.CommandTimeout = 9000;
                                    int user_id = (int)cmd.ExecuteScalar();

                                    cmd = new SqlCommand("SELECT * FROM [order] WHERE [number] = " + prop.OrderPixlPark + int.Parse(order).ToString("D10"), cn);
                                    cmd.CommandTimeout = 9000;
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
                                            MessageBox.Show("Заказ с номером " + prop.OrderPixlPark + int.Parse(order).ToString("D10") + " уже существует в базе и по нему принималась оплата, изменение этого заказа невозможно!");
                                        else
                                        {


                                            if (MessageBox.Show("Заказ с номером " + prop.OrderPixlPark + int.Parse(order).ToString("D10") + " уже существует в базе, но оплата по нему не принималась.\nОтменить его и импортировать новый?", "Импорт", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                            {
                                                add = true;
                                                query += "UPDATE [order] SET \n\t[status] = '010000', \n\texported = 0, \n\t[number] = '" + prop.OrderPixlPark.Substring(0, 1) + "9" + int.Parse(order).ToString("D10") + "' \nWHERE \n\t[number] = " + prop.OrderPixlPark + int.Parse(order).ToString("D10") + ";\n";
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
                                            string sub_name = od.Rows[j]["ACTION_NAME"].ToString().Trim();
                                            if (sub_name.IndexOf('[') > 0)
                                                sub_name = sub_name.Substring(0, sub_name.IndexOf('['));
                                            if (sub_name.IndexOf('(') > 0)
                                                sub_name = sub_name.Substring(0, sub_name.IndexOf('('));

                                            // определяем ключи для услуг
                                            cmd = new SqlCommand("SELECT id_good, type FROM dbo.good WHERE (name = '" + sub_name.Trim() + "')", cn);
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
                                        if (prop.PixlParkClient)
                                            query += "DECLARE @CLIENTID int;\n" +
                                                "SET @CLIENTID = " + client_id + ";\n" +
                                                "DECLARE @CLIENTNAME nchar(255);\n" +
                                                "SET @CLIENTNAME = 'PixlPark'\n";
                                        else
								            query += "DECLARE @name nchar(255)\n" +
										            "DECLARE @phone nchar(255)\n" +
										            "SET @name = '" + user_lname + " " + user_fname + " [" + user_address + "]%'\n" +
										            "SET @phone = '" + user_phone + "'\n" +
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
                                                    "			   ,'" + user_lname + " " + user_fname + " [" + user_address + "]'\n" +
                                                    "			   ,'" + user_phone + "')\n" +
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
                                                    "SET @CLIENTNAME = '" + user_lname + " " + user_fname + " [" + user_address + "]'\n";


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
                                               ",'" + status + "' \n" +
                                               ",'" + prop.OrderPixlPark +
                                               int.Parse(order).ToString("D10") + "' \n" +
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
                                                   "," + ((status == "000000") ? od.Rows[j]["QTY"].ToString().Trim().Replace(',', '.') : "0") + " \n" +
                                                   ",'+' \n" +
                                                   "," + od.Rows[j]["PRICE"].ToString().Trim().Replace(',', '.') + " \n" +
                                                   ",getdate() \n" +
                                                   "," + usr.Id_user + " \n" +
                                                   ",'" + usr.Name + "' \n" +
                                                   ",0\n" +
                                                   ",'" + od.Rows[j]["ACTION_NAME"].ToString().Trim() + " " + od.Rows[j]["PRICE"].ToString().Trim() + "р');\n";
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
                                        query += "COMMIT;\n\n";
                                        cmd.CommandText = query;
                                        cmd.Connection = cn;
                                        cmd.CommandTimeout = 15000;
                                        cmd.ExecuteNonQuery();
                                        qq = true;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Ошибка сохранения заказа.\n" + ex.Message + "\n" + ex.Source);
                            }



                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка получения данных о заказе.\n" + ex.Message);
                        }
                    }
                    else
                        MessageBox.Show("Полученный токен доступа не действителен.");


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка получения токена доступа.\n" + ex.Message);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка получения токена.\n" + ex.Message);
            }
            return qq;
        }
    }
}
