using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Forms.Interface;
using System.IO;
using Photoland.Lib;
using PSA.Lib.Interface;
using PSA.Lib.Util;

namespace Photoland.Robot
{
    public partial class frmMain : Form
    {
        private void DoExport()
        {
            if (Directory.Exists(prop.Dir_export))
            {
				ini iniRobot = new ini(System.Environment.GetCommandLineArgs()[0].Substring(
										0,
										System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
										) + "\\robot.ini");
                string exported_order = "0";
                string exported_orderbody = "0";
                string exported_payment = "0";
                string exported_discard = "0";
                string exported_inv = "0";
				string exported_ver = "0";
                string date = DateTime.Now.Year.ToString();
                date += "_";
                date += DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
                date += "_";
                date += DateTime.Now.Day < 10 ? "0" + DateTime.Now.Day.ToString() : DateTime.Now.Day.ToString();
                date += "_";
                date += DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
                date += "_";
                date += DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
                date += "_";
                date += DateTime.Now.Second < 10 ? "0" + DateTime.Now.Second.ToString() : DateTime.Now.Second.ToString();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandTimeout = 9000;
                //cmd.CommandText = "SELECT derivedtbl_1.id_order, derivedtbl_1.id_user_accept, derivedtbl_1.id_user_operator, derivedtbl_1.id_user_designer, derivedtbl_1.id_user_delivery, derivedtbl_1.id_client, derivedtbl_1.guid, derivedtbl_1.del, derivedtbl_1.name_accept, derivedtbl_1.name_operator, derivedtbl_1.name_designer,  derivedtbl_1.name_delivery, derivedtbl_1.status, derivedtbl_1.number, derivedtbl_1.input_date, derivedtbl_1.expected_date, derivedtbl_1.output_date,  derivedtbl_1.advanced_payment, derivedtbl_1.final_payment, derivedtbl_1.discont_percent, derivedtbl_1.discont_code, derivedtbl_1.preview,  derivedtbl_1.comment, derivedtbl_1.crop, derivedtbl_1.type, derivedtbl_1.exported, derivedtbl_1.client, derivedtbl_1.name AS category,  derivedtbl_1.id_category, derivedtbl_1.bonus, SUM(dbo.orderbody.price * dbo.orderbody.actual_quantity) AS sm FROM (SELECT dbo.[order].id_order, dbo.[order].id_user_accept, dbo.[order].id_user_operator, dbo.[order].id_user_designer, dbo.[order].id_user_delivery,  dbo.[order].id_client, dbo.[order].guid, dbo.[order].del, dbo.[order].name_accept, dbo.[order].name_operator, dbo.[order].name_designer,  dbo.[order].name_delivery, dbo.[order].status, CAST(dbo.[order].number AS nchar(15)) AS number, dbo.[order].input_date,  dbo.[order].expected_date, dbo.[order].output_date, dbo.[order].advanced_payment, dbo.[order].final_payment, dbo.[order].discont_percent,  CAST(dbo.[order].discont_code AS nchar(15)) AS discont_code, dbo.[order].preview, dbo.[order].comment, dbo.[order].crop, dbo.[order].type,  dbo.[order].exported, dbo.client.name AS client, dbo.category.name, dbo.category.id_category, dbo.[order].bonus FROM dbo.category LEFT OUTER JOIN dbo.client ON dbo.category.id_category = dbo.client.id_category RIGHT OUTER JOIN dbo.[order] ON dbo.client.id_client = dbo.[order].id_client WHERE (dbo.[order].exported = 0)) AS derivedtbl_1 INNER JOIN dbo.orderbody ON derivedtbl_1.id_order = dbo.orderbody.id_order GROUP BY derivedtbl_1.id_order, derivedtbl_1.id_user_accept, derivedtbl_1.id_user_operator, derivedtbl_1.id_user_designer, derivedtbl_1.id_user_delivery,  derivedtbl_1.id_client, derivedtbl_1.guid, derivedtbl_1.del, derivedtbl_1.name_accept, derivedtbl_1.name_operator, derivedtbl_1.name_designer, derivedtbl_1.name_delivery, derivedtbl_1.status, derivedtbl_1.number, derivedtbl_1.input_date, derivedtbl_1.expected_date, derivedtbl_1.output_date,  derivedtbl_1.advanced_payment, derivedtbl_1.final_payment, derivedtbl_1.discont_percent, derivedtbl_1.discont_code, derivedtbl_1.comment,  derivedtbl_1.crop, derivedtbl_1.type, derivedtbl_1.client, derivedtbl_1.name, derivedtbl_1.bonus, derivedtbl_1.exported, derivedtbl_1.preview, derivedtbl_1.id_category";
                cmd.CommandText =
                    "SELECT dbo.[order].id_order, dbo.[order].id_user_accept, dbo.[order].id_user_operator, dbo.[order].id_user_designer, dbo.[order].id_user_delivery," +
                    "dbo.[order].id_client, dbo.[order].guid, dbo.[order].del, dbo.[order].name_accept, dbo.[order].name_operator, dbo.[order].name_designer, " +
                    "dbo.[order].name_delivery, dbo.[order].status, CAST(dbo.[order].number AS nchar(15)) AS number, dbo.[order].input_date, dbo.[order].expected_date,  " +
                    "dbo.[order].output_date, dbo.[order].advanced_payment, dbo.[order].final_payment, dbo.[order].discont_percent, " +
                    "CAST(dbo.[order].discont_code AS nchar(15)) AS discont_code, dbo.[order].preview, dbo.[order].comment, dbo.[order].crop, dbo.[order].type,  " +
                    "dbo.[order].exported, dbo.client.name AS client, dbo.category.name AS category, dbo.category.id_category, dbo.[order].bonus,  " +
                    "dbo.[order].order_price AS sm " +
                    "FROM         dbo.category LEFT OUTER JOIN " +
                    "dbo.client ON dbo.category.id_category = dbo.client.id_category RIGHT OUTER JOIN " +
                    "dbo.[order] ON dbo.client.id_client = dbo.[order].id_client " +
                    "WHERE     (dbo.[order].exported = 0)";
                cmd.Connection = db_connection;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable t = new DataTable("order");
                da.Fill(t);
                if (t.Rows.Count > 0)
                {
                    StreamWriter fl = new StreamWriter(prop.Dir_export + "\\order3_" + date + ".csv", true, Encoding.GetEncoding(1251));
                    pb.Minimum = 0;
                    pb.Maximum = t.Rows.Count;
                    pb.Value = 0;
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        Application.DoEvents();
                        exported_order += ", ";
                        //fl.WriteLine("id_order;id_user_accept;id_user_operator;id_user_designer;id_user_delivery;id_client;guid;del;name_accept;name_operator;name_designer;name_delivery;status;number;input_date;expected_date;output_date;advanced_payment;final_payment;discont_percent;discont_code;preview;comment;crop;type;exported;client;category;category_id;bonus;suma");
                        fl.WriteLine("[order]");
                        fl.WriteLine(t.Rows[i][0].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][1].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][2].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][3].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][4].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][5].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][6].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][7].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][8].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][9].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][10].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][11].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][12].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][13].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][14].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][15].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][16].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][17].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][18].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][19].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][20].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][21].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][22].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][23].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][24].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][25].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][26].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][27].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][28].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][29].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][30].ToString().Trim().Replace(";", " "));
                        exported_order += t.Rows[i][0].ToString();

                        SqlCommand cmdo = new SqlCommand();
                        cmdo.CommandTimeout = 9000;
                        cmdo.CommandText =
                            "SELECT dbo.orderbody.id_orderbody, dbo.orderbody.id_order, dbo.orderbody.id_mashine, dbo.orderbody.id_material, dbo.orderbody.id_good, " +
                            "dbo.orderbody.guid, dbo.orderbody.del, dbo.orderbody.quantity, dbo.orderbody.actual_quantity, dbo.orderbody.sign, dbo.orderbody.price, " +
                            "dbo.orderbody.datework, ISNULL(user_1.id_user, 0) AS id_user_work, ISNULL(user_1.name, '') AS name_work, dbo.orderbody.defect_quantity, " +
                            "ISNULL(user_2.id_user, 0) AS id_user_defect, ISNULL(user_2.name, '') AS user_defect, dbo.orderbody.tech_defect, dbo.orderbody.exported, " +
                            "dbo.orderbody.dateadd, ISNULL(dbo.[user].id_user, 0) AS id_user_add, ISNULL(dbo.[user].name, '') AS name_add, dbo.orderbody.defect_ok,  " +
                            "dbo.orderbody.comment " +
                            "FROM         dbo.[user] AS user_2 RIGHT OUTER JOIN " +
                            "dbo.orderbody ON user_2.id_user = dbo.orderbody.id_user_defect LEFT OUTER JOIN " +
                            "dbo.[user] AS user_1 ON dbo.orderbody.id_user_work = user_1.id_user LEFT OUTER JOIN " +
                            "dbo.[user] ON dbo.orderbody.id_user_add = dbo.[user].id_user " +
                            "WHERE dbo.orderbody.id_order = " + t.Rows[i][0].ToString();
                        cmdo.Connection = db_connection;
                        SqlDataAdapter dao = new SqlDataAdapter(cmdo);
                        DataTable tt = new DataTable("orderbody");
                        dao.Fill(tt);
                        if (tt.Rows.Count > 0)
                        {
                            //StreamWriter flo =
                            //	new StreamWriter(prop.Dir_export + "\\orderbody_" + date + ".csv", true, Encoding.GetEncoding(1251));
                            //fl.WriteLine("id_orderbody;id_order;id_mashine;id_material;id_good;guid;del;quantity;actual_quantity;sign;price;datework;id_user_work;name_work;defect_quantity;id_user_defect;user_defect;tech_defect;exported;dateadd;id_user_add;name_add;defect_ok");
                            fl.WriteLine("[orderbody]");
                            for (int j = 0; j < tt.Rows.Count; j++)
                            {
                                //exported_orderbody += ", ";
                                fl.WriteLine(tt.Rows[j][0].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][1].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][2].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][3].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][4].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][5].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][6].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][7].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][8].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][9].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][10].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][11].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][12].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][13].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][14].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][15].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][16].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][17].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][18].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][19].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][20].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][21].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][22].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][23].ToString().Trim().Replace(";", " "));
                                //exported_orderbody += t.Rows[i][0].ToString();
                            }
                            //flo.Close();
                        }

                        cmdo = new SqlCommand();
                        cmdo.CommandTimeout = 9000;
                        cmdo.CommandText =
                            "SELECT [id_orderevent], [del], [guid], [id_order], [event_date], [event_user], [event_status], [event_point], [event_text] FROM [dbo].[orderevent] WHERE [id_order] = " + t.Rows[i][0].ToString();
                        cmdo.Connection = db_connection;
                        dao = new SqlDataAdapter(cmdo);
                        tt = new DataTable("orderbody");
                        dao.Fill(tt);
                        if (tt.Rows.Count > 0)
                        {
                            //StreamWriter flo =
                            //	new StreamWriter(prop.Dir_export + "\\orderbody_" + date + ".csv", true, Encoding.GetEncoding(1251));
                            //fl.WriteLine("id_orderbody;id_order;id_mashine;id_material;id_good;guid;del;quantity;actual_quantity;sign;price;datework;id_user_work;name_work;defect_quantity;id_user_defect;user_defect;tech_defect;exported;dateadd;id_user_add;name_add;defect_ok");
                            fl.WriteLine("[orderevents]");
                            for (int j = 0; j < tt.Rows.Count; j++)
                            {
                                //exported_orderbody += ", ";
                                fl.WriteLine(tt.Rows[j][0].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][1].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][2].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][3].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][4].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][5].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][6].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][7].ToString().Trim().Replace(";", " ") + ";" +
                                             tt.Rows[j][8].ToString().Trim().Replace(";", " "));
                                //exported_orderbody += t.Rows[i][0].ToString();
                            }
                            //flo.Close();
                        }
                        try
                        {
                            pb.Value = i;
                            Application.DoEvents();
                        }
                        catch
                        {
                        }

                    }
                    SqlCommand cmdd = new SqlCommand();
                    cmdd.CommandTimeout = 9000;
                    cmdd.CommandText = "SELECT id_orderbody, id_order, id_mashine, id_material, id_good, guid, del, quantity, actual_quantity, sign, price, datework, id_user_work, name_work, defect_quantity, id_user_defect, user_defect, tech_defect, exported, dateadd, id_user_add, name_add, defect_ok FROM dbo.orderbody WHERE (id_order IS NULL) AND (exported = 0) OR (id_order = 0) AND (exported = 0)";
                    cmdd.Connection = db_connection;
                    SqlDataAdapter dad = new SqlDataAdapter(cmdd);
                    DataTable td = new DataTable("orderbody");
                    dad.Fill(td);
                    if (td.Rows.Count > 0)
                    {
                        //StreamWriter flo =
                        //	new StreamWriter(prop.Dir_export + "\\orderbody_" + date + ".csv", true, Encoding.GetEncoding(1251));
                        //fl.WriteLine("id_orderbody;id_order;id_mashine;id_material;id_good;guid;del;quantity;actual_quantity;sign;price;datework;id_user_work;name_work;defect_quantity;id_user_defect;user_defect;tech_defect;exported;dateadd;id_user_add;name_add;defect_ok");
                        fl.WriteLine("[orderbody-defect]");
                        pb.Minimum = 0;
                        pb.Maximum = td.Rows.Count;
                        pb.Value = 0;
                        for (int j = 0; j < td.Rows.Count; j++)
                        {
                            exported_orderbody += ", ";
                            Application.DoEvents();
                            fl.WriteLine(td.Rows[j][0].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][1].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][2].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][3].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][4].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][5].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][6].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][7].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][8].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][9].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][10].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][11].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][12].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][13].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][14].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][15].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][16].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][17].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][18].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][19].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][20].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][21].ToString().Trim().Replace(";", " ") + ";" +
                                         td.Rows[j][22].ToString().Trim().Replace(";", " "));
                            try
                            {
                                exported_orderbody += td.Rows[j][0].ToString();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("");
                            }
                            try
                            {
                                pb.Value = j;
                                Application.DoEvents();
                            }
                            catch
                            {
                            }
                        }
                        //flo.Close();
                    }
                    fl.Close();
					iniRobot.IniWriteValue("export", "order", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                }
				
                /*
                cmd = new SqlCommand();
                cmd.CommandTimeout = 5000;
                cmd.CommandText = "SELECT [id_orderbody], [id_order], [id_mashine], [id_material], [id_good], [guid], [del], [quantity], [actual_quantity], [sign], [price], [datework], [id_user_work], [name_work], [defect_quantity], [id_user_defect], [user_defect], [tech_defect], [exported] FROM [vwExportOrderBody]";
                cmd.Connection = db_connection;
                da = new SqlDataAdapter(cmd);
                t = new DataTable("orderbody");
                da.Fill(t);
                if (t.Rows.Count > 0)
                {
                    StreamWriter fl =
                        new StreamWriter(prop.Dir_export + "\\orderbody_" + date + ".csv", true, Encoding.GetEncoding(1251));
                    fl.WriteLine("id_orderbody;id_order;id_mashine;id_material;id_good;guid;del;quantity;actual_quantity;sign;price;datework;id_user_work;name_work;defect_quantity;id_user_defect;user_defect;tech_defect;exported");
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        exported_orderbody += ", ";
                        fl.WriteLine(t.Rows[i][0].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][1].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][2].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][3].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][4].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][5].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][6].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][7].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][8].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][9].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][10].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][11].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][12].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][13].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][14].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][15].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][16].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][17].ToString().Trim().Replace(";", " ") + ";" +
                                     t.Rows[i][18].ToString().Trim().Replace(";", " "));
                        exported_orderbody += t.Rows[i][0].ToString();
                    }
                    fl.Close();
                }
                */

                cmd = new SqlCommand();
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SELECT [id_payment], [del], [guid], [date], [time], [id_user], [name_user], [number], [payment], [type], [comment], [payment_way], [exported] FROM [vwExportPayments]";
                cmd.Connection = db_connection;
                da = new SqlDataAdapter(cmd);
                t = new DataTable("payment");
                da.Fill(t);
                if (t.Rows.Count > 0)
                {
                    StreamWriter fl =
                        new StreamWriter(prop.Dir_export + "\\payments_" + date + ".csv", true, Encoding.GetEncoding(1251));
                    fl.WriteLine("id_payment;del;guid;date;time;id_user;name_user;number;payment;type;comment;payment_way;exported");
                    pb.Minimum = 0;
                    pb.Maximum = t.Rows.Count;
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        Application.DoEvents();
                        exported_payment += ", ";
                        fl.WriteLine(t.Rows[i][0].ToString().Trim() + ";" +
                                     t.Rows[i][1].ToString().Trim() + ";" +
                                     t.Rows[i][2].ToString().Trim() + ";" +
                                     t.Rows[i][3].ToString().Trim() + ";" +
                                     t.Rows[i][4].ToString().Trim() + ";" +
                                     t.Rows[i][5].ToString().Trim() + ";" +
                                     t.Rows[i][6].ToString().Trim() + ";" +
                                     t.Rows[i][7].ToString().Trim() + ";" +
                                     t.Rows[i][8].ToString().Trim() + ";" +
                                     t.Rows[i][9].ToString().Trim() + ";" +
                                     t.Rows[i][10].ToString().Trim() + ";" +
                                     t.Rows[i][11].ToString().Trim() + ";" +
                                     t.Rows[i][12].ToString().Trim());
                        exported_payment += t.Rows[i][0].ToString();
                        try
                        {
                            pb.Value = i;
                        }
                        catch
                        {
                        }
                    }
                    fl.Close();
					iniRobot.IniWriteValue("export", "payment", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                }

				

				cmd = new SqlCommand();
				cmd.CommandTimeout = 9000;
				cmd.CommandText = "SELECT [id_inventory], [del], [guid], [inventory_date], [inventory_user], [exported] FROM [dbo].[inventory] WHERE [exported] = 0";
				cmd.Connection = db_connection;
				da = new SqlDataAdapter(cmd);
				t = new DataTable("inv");
				da.Fill(t);
				if (t.Rows.Count > 0)
				{
					StreamWriter fl =
						new StreamWriter(prop.Dir_export + "\\inv_" + date + ".csv", true, Encoding.GetEncoding(1251));
					fl.WriteLine("id_inventory;del;guid;inventory_date;inventory_user");
					pb.Minimum = 0;
					pb.Maximum = t.Rows.Count;
					for (int i = 0; i < t.Rows.Count; i++)
					{
						Application.DoEvents();
						exported_inv += ", ";
						fl.WriteLine(t.Rows[i][0].ToString().Trim() + ";" +
									 t.Rows[i][1].ToString().Trim() + ";" +
									 t.Rows[i][2].ToString().Trim() + ";" +
									 t.Rows[i][3].ToString().Trim() + ";" +
									 t.Rows[i][4].ToString().Trim());
						exported_inv += t.Rows[i][0].ToString();

						SqlCommand cmdi = new SqlCommand();
						cmdi.CommandTimeout = 9000;
						cmdi.CommandText = "SELECT [id_inventorybody], [del], [guid], [id_inventory], [order_number], [order_found], [order_status], [order_status_t], [order_in], [order_out], [order_action], [order_action_t], [order_user], order_status_fact, order_status_fact_t, [exported] FROM [dbo].[inventorybody] WHERE [id_inventory] = " + t.Rows[i][0].ToString();
						cmdi.Connection = db_connection;
						SqlDataAdapter dai = new SqlDataAdapter(cmdi);
						DataTable ti = new DataTable("inv");
						dai.Fill(ti);
						if (ti.Rows.Count > 0)
						{
							pb.Minimum = 0;
							pb.Maximum = ti.Rows.Count;
							fl.WriteLine("id_inventorybody;del;guid;id_inventory;order_number;order_found;order_status;order_status_t;order_in;order_out;order_action;order_action_t;order_user;order_status_fact;order_status_fact_t;exported");
							for (int iij = 0; iij < ti.Rows.Count; iij++)
							{
								fl.WriteLine(ti.Rows[iij][0].ToString().Trim() + ";" +
								ti.Rows[iij][1].ToString().Trim() + ";" +
								ti.Rows[iij][2].ToString().Trim() + ";" +
								ti.Rows[iij][3].ToString().Trim() + ";" +
								ti.Rows[iij][4].ToString().Trim() + ";" +
								ti.Rows[iij][5].ToString().Trim() + ";" +
								ti.Rows[iij][6].ToString().Trim() + ";" +
								ti.Rows[iij][7].ToString().Trim() + ";" +
								ti.Rows[iij][8].ToString().Trim() + ";" +
								ti.Rows[iij][9].ToString().Trim() + ";" +
								ti.Rows[iij][10].ToString().Trim() + ";" +
								ti.Rows[iij][11].ToString().Trim() + ";" +
								ti.Rows[iij][12].ToString().Trim() + ";" +
								ti.Rows[iij][13].ToString().Trim() + ";" +
								ti.Rows[iij][14].ToString().Trim() + ";" +
								ti.Rows[iij][15].ToString().Trim());

								try
								{
									pb.Value = iij;
									Application.DoEvents();
								}
								catch
								{
								}

							}
						}

					}
					fl.Close();
					iniRobot.IniWriteValue("export", "inventory", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
				}

				

				cmd = new SqlCommand();
				cmd.CommandTimeout = 9000;
				cmd.CommandText = "SELECT [id_verification], [del], [guid], [verification_date], [verification_user], [exported] FROM [dbo].[verification] WHERE [exported] = 0";
				cmd.Connection = db_connection;
				da = new SqlDataAdapter(cmd);
				t = new DataTable("ver");
				da.Fill(t);
				if (t.Rows.Count > 0)
				{
					StreamWriter fl =
						new StreamWriter(prop.Dir_export + "\\ver_" + date + ".csv", true, Encoding.GetEncoding(1251));
					fl.WriteLine("id_verification;del;guid;verification_date;verification_user");
					pb.Minimum = 0;
					pb.Maximum = t.Rows.Count;
					for (int i = 0; i < t.Rows.Count; i++)
					{
						Application.DoEvents();
						exported_ver += ", ";
						fl.WriteLine(t.Rows[i][0].ToString().Trim() + ";" +
									 t.Rows[i][1].ToString().Trim() + ";" +
									 t.Rows[i][2].ToString().Trim() + ";" +
									 t.Rows[i][3].ToString().Trim() + ";" +
									 t.Rows[i][4].ToString().Trim());
						exported_ver += t.Rows[i][0].ToString();

						SqlCommand cmdi = new SqlCommand();
						cmdi.CommandTimeout = 5000;
						cmdi.CommandText = "SELECT [id_verificationbody], [del], [guid], [id_verification], [order_number], [order_found], [order_status], [order_status_t], [order_in], [order_out], [order_action], [order_action_t], [order_user], order_status_fact, order_status_fact_t, [exported] FROM [dbo].[verificationbody] WHERE [id_verification] = " + t.Rows[i][0].ToString();
						cmdi.Connection = db_connection;
						SqlDataAdapter dai = new SqlDataAdapter(cmdi);
						DataTable ti = new DataTable("ver");
						dai.Fill(ti);
						if (ti.Rows.Count > 0)
						{
							pb.Minimum = 0;
							pb.Maximum = ti.Rows.Count;
							fl.WriteLine("id_verificationbody;del;guid;id_verification;order_number;order_found;order_status;order_status_t;order_in;order_out;order_action;order_action_t;order_user;order_status_fact;order_status_fact_t;exported");
							for (int iij = 0; iij < ti.Rows.Count; iij++)
							{
								fl.WriteLine(ti.Rows[iij][0].ToString().Trim() + ";" +
								ti.Rows[iij][1].ToString().Trim() + ";" +
								ti.Rows[iij][2].ToString().Trim() + ";" +
								ti.Rows[iij][3].ToString().Trim() + ";" +
								ti.Rows[iij][4].ToString().Trim() + ";" +
								ti.Rows[iij][5].ToString().Trim() + ";" +
								ti.Rows[iij][6].ToString().Trim() + ";" +
								ti.Rows[iij][7].ToString().Trim() + ";" +
								ti.Rows[iij][8].ToString().Trim() + ";" +
								ti.Rows[iij][9].ToString().Trim() + ";" +
								ti.Rows[iij][10].ToString().Trim() + ";" +
								ti.Rows[iij][11].ToString().Trim() + ";" +
								ti.Rows[iij][12].ToString().Trim() + ";" +
								ti.Rows[iij][13].ToString().Trim() + ";" +
								ti.Rows[iij][14].ToString().Trim() + ";" +
								ti.Rows[iij][15].ToString().Trim());

								try
								{
									pb.Value = iij;
									Application.DoEvents();
								}
								catch
								{
								}

							}
						}

					}
					fl.Close();
					iniRobot.IniWriteValue("export", "verification", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
				}

				






                cmd = new SqlCommand();
                cmd.CommandTimeout = 9000;
                cmd.CommandText = "SELECT [id_discard], [del], [guid], [datediscard], [id_material], [quantity], [comment], [id_user], [user_name], [orderno], [id_mashine], [exported] FROM [vwExportDiscard]";
                cmd.Connection = db_connection;
                da = new SqlDataAdapter(cmd);
                t = new DataTable("discard");
                da.Fill(t);
                if (t.Rows.Count > 0)
                {
                    StreamWriter fl =
                        new StreamWriter(prop.Dir_export + "\\discarding_" + date + ".csv", true, Encoding.GetEncoding(1251));
                    fl.WriteLine("id_discard;del;guid;datediscard;id_material;quantity;comment;id_user;user_name;orderno;id_mashine;exported");
                    pb.Minimum = 0;
                    pb.Maximum = t.Rows.Count;
                    for (int i = 0; i < t.Rows.Count; i++)
                    {
                        exported_discard += ", ";
                        fl.WriteLine(t.Rows[i][0].ToString().Trim() + ";" +
                                     t.Rows[i][1].ToString().Trim() + ";" +
                                     t.Rows[i][2].ToString().Trim() + ";" +
                                     t.Rows[i][3].ToString().Trim() + ";" +
                                     t.Rows[i][4].ToString().Trim() + ";" +
                                     t.Rows[i][5].ToString().Trim() + ";" +
                                     t.Rows[i][6].ToString().Trim() + ";" +
                                     t.Rows[i][7].ToString().Trim() + ";" +
                                     t.Rows[i][8].ToString().Trim() + ";" +
									 t.Rows[i][9].ToString().Trim() + ";" +
									 t.Rows[i][10].ToString().Trim() + ";" +
                                     t.Rows[i][11].ToString().Trim());
                        exported_discard += t.Rows[i][0].ToString();
                        try
                        {
                            pb.Value = i;
                            Application.DoEvents();
                        }
                        catch
                        {
                        }
                    }
                    fl.Close();
					iniRobot.IniWriteValue("export", "discard", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
                }
                pb.Value = 0;

                cmd = new SqlCommand();
                cmd.CommandText = "UPDATE [order] SET [exported] = 1 WHERE id_order IN (" + exported_order + ")";
                cmd.Connection = db_connection;
                cmd.CommandTimeout = 9000;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand();
                cmd.CommandText = "UPDATE [orderbody] SET [exported] = 1 WHERE id_orderbody IN (" + exported_orderbody + ")";
                cmd.Connection = db_connection;
                cmd.CommandTimeout = 9000;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand();
                cmd.CommandText = "UPDATE [payments] SET [exported] = 1 WHERE id_payment IN (" + exported_payment + ")";
                cmd.Connection = db_connection;
                cmd.CommandTimeout = 9000;
                cmd.ExecuteNonQuery();

                cmd = new SqlCommand();
                cmd.CommandText = "UPDATE [discard] SET [exported] = 1 WHERE id_discard IN (" + exported_discard + ")";
                cmd.Connection = db_connection;
                cmd.CommandTimeout = 9000;
                cmd.ExecuteNonQuery();

				cmd = new SqlCommand();
				cmd.CommandText = "UPDATE [inventory] SET [exported] = 1 WHERE id_inventory IN (" + exported_inv + ")";
				cmd.Connection = db_connection;
                cmd.CommandTimeout = 9000;
                cmd.ExecuteNonQuery();

				cmd = new SqlCommand();
				cmd.CommandText = "UPDATE [verification] SET [exported] = 1 WHERE id_verification IN (" + exported_ver + ")";
				cmd.Connection = db_connection;
                cmd.CommandTimeout = 9000;
                cmd.ExecuteNonQuery();

				DirectoryInfo dExport = new DirectoryInfo(prop.Dir_export);
                pb.Minimum = 0;
                pb.Maximum = dExport.GetFiles("*.csv").Length;
                int ij = 0;
				bool doit = false;
				if (dExport.GetFiles("*.csv").Length > 0)
					doit = true;
                foreach (FileInfo f in dExport.GetFiles("*.csv"))
                {
                    Application.DoEvents();
                    try
                    {
                        pb.Value = ij;
                    }
                    catch
                    {
                    }
                    if (prop.ExportDoCopy)
                    {
                        try
                        {
                            string p = "";
                            p += "\\Copy\\";
                            if (!Directory.Exists(prop.Dir_export + p))
                                Directory.CreateDirectory(prop.Dir_export + p);
                            p += DateTime.Now.Year.ToString() + "\\";
                            if (!Directory.Exists(prop.Dir_export + p))
                                Directory.CreateDirectory(prop.Dir_export + p);
                            p += DateTime.Now.Month < 10
                                     ? "0" + DateTime.Now.Month.ToString() + "\\"
                                     : DateTime.Now.Month.ToString() + "\\";
                            if (!Directory.Exists(prop.Dir_export + p))
                                Directory.CreateDirectory(prop.Dir_export + p);
                            p += DateTime.Now.Day < 10
                                     ? "0" + DateTime.Now.Day.ToString() + "\\"
                                     : DateTime.Now.Day.ToString() + "\\";
                            if (!Directory.Exists(prop.Dir_export + p))
                                Directory.CreateDirectory(prop.Dir_export + p);

                            File.Copy(f.FullName, prop.Dir_export + p + f.Name, true);
                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                           "] - Файл " + f.Name + " успешно скопирован в резервный каталог.\n");
                        }
                        catch (Exception ex)
                        {
                            ErrorNfo.WriteErrorInfo(ex);
                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                           "] ! Ошибка копирования файла " + f.Name + " в резервный каталог." + ex.Message + "\n" + ex.Source + "\n");
                        }
                    }
                    if (prop.Export_from_ftp)
                    {
                        try
                        {
                            PSA.Lib.Util.ftpClient ftp = new PSA.Lib.Util.ftpClient(prop.FTP_Server_Export, prop.FTP_User, prop.FTP_Password);
                            if (ftp.Upload(prop.Dir_export + "\\" + f.Name, prop.FTP_Path_Export + "/" + f.Name))
                            {
                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                    "] - Файл " + f.Name + " успешно скопирован на ftp сервер.\n");
                                if (ftp.Download(prop.Dir_export, f.Name + ".tmp", prop.FTP_Path_Export, f.Name))
                                {
                                    if (checkFiles(prop.Dir_export + "\\" + f.Name, prop.Dir_export + "\\" + f.Name + ".tmp"))
                                    {

                                        if (prop.ExportClearDirAfterCopy)
                                        {
                                            try
                                            {
                                                f.Delete();
                                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                    "] - Файл " + f.Name + " успешно удален.\n");
                                                File.Delete(prop.Dir_export + "\\" + f.Name + ".tmp");
                                            }
                                            catch (Exception ex)
                                            {
                                                ErrorNfo.WriteErrorInfo(ex);
                                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                "] ! Ошибка удаления файла " + f.Name + "." + ex.Message + "\n" +
                                                ex.Source + "\n");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                "] ! Ошибка копирования файла " + f.Name + " на ftp сервер.\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorNfo.WriteErrorInfo(ex);
                            wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                "] ! Ошибка копирования файла " + f.Name + " на ftp сервер." + ex.Message + "\n" +
                                ex.Source + "\n");
                        }
                    }
                    ij++;
                }
				if (doit)
					iniRobot.IniWriteValue("export", "csv", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
				doit = false;

                pb.Minimum = 0;
                pb.Maximum = dExport.GetFiles("er*.info").Length;
                int ii = 0;
				if (dExport.GetFiles("er*.info").Length > 0)
					doit = true;
				foreach (FileInfo f in dExport.GetFiles("er*.info"))
                {
                    Application.DoEvents();
                    try
                    {
                        pb.Value = ii;
                    }
                    catch
                    {
                    }
                    if (f.Name != "er_" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + ".info")
                    {
                        if (prop.ExportDoCopy)
                        {
                            try
                            {
                                string p = "";
                                p += "\\Copy\\";
                                if (!Directory.Exists(prop.Dir_export + p))
                                    Directory.CreateDirectory(prop.Dir_export + p);
                                p += DateTime.Now.Year.ToString() + "\\";
                                if (!Directory.Exists(prop.Dir_export + p))
                                    Directory.CreateDirectory(prop.Dir_export + p);
                                p += DateTime.Now.Month < 10
                                         ? "0" + DateTime.Now.Month.ToString() + "\\"
                                         : DateTime.Now.Month.ToString() + "\\";
                                if (!Directory.Exists(prop.Dir_export + p))
                                    Directory.CreateDirectory(prop.Dir_export + p);
                                p += DateTime.Now.Day < 10
                                         ? "0" + DateTime.Now.Day.ToString() + "\\"
                                         : DateTime.Now.Day.ToString() + "\\";
                                if (!Directory.Exists(prop.Dir_export + p))
                                    Directory.CreateDirectory(prop.Dir_export + p);

                                File.Copy(f.FullName, prop.Dir_export + p + f.Name, true);
                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                               "] - Файл " + f.Name + " успешно скопирован в резервный каталог.\n");
                            }
                            catch (Exception ex)
                            {
                                ErrorNfo.WriteErrorInfo(ex);
                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                               "] ! Ошибка копирования файла " + f.Name + " в резервный каталог." + ex.Message + "\n" + ex.Source + "\n");
                            }
                        }
                        if (prop.Export_from_ftp)
                        {
                            try
                            {
                                PSA.Lib.Util.ftpClient ftp = new PSA.Lib.Util.ftpClient(prop.FTP_Server_Export, prop.FTP_User, prop.FTP_Password);
                                if (ftp.Upload(prop.Dir_export + "\\" + f.Name, prop.FTP_Path_Export + "/info/" + f.Name))
                                {
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                        "] - Файл " + f.Name + " успешно скопирован на ftp сервер.\n");
                                    if (ftp.Download(prop.Dir_export, f.Name + ".tmp", prop.FTP_Path_Export, "/info/" + f.Name))
                                    {
                                        if (checkFiles(prop.Dir_export + "\\" + f.Name, prop.Dir_export + "\\" + f.Name + ".tmp"))
                                        {

                                            if (prop.ExportClearDirAfterCopy)
                                            {
                                                try
                                                {
                                                    f.Delete();
                                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                        "] - Файл " + f.Name + " успешно удален.\n");
                                                    File.Delete(prop.Dir_export + "\\" + f.Name + ".tmp");
                                                }
                                                catch (Exception ex)
                                                {
                                                    ErrorNfo.WriteErrorInfo(ex);
                                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                    "] ! Ошибка удаления файла " + f.Name + "." + ex.Message + "\n" +
                                                    ex.Source + "\n");
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                    "] ! Ошибка копирования файла " + f.Name + " на ftp сервер.\n");
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorNfo.WriteErrorInfo(ex);
                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                    "] ! Ошибка копирования файла " + f.Name + " на ftp сервер." + ex.Message + "\n" +
                                    ex.Source + "\n");
                            }
                        }
                    }
                    ii++;
                }
				if (doit)
					iniRobot.IniWriteValue("export", "er", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
				doit = false;


                pb.Minimum = 0;
                pb.Maximum = dExport.GetFiles("*clear*.info").Length;
                ii = 0;
				if (dExport.GetFiles("*clear*.info").Length > 0)
					doit = true;
				foreach (FileInfo f in dExport.GetFiles("*clear*.info"))
                {
                    Application.DoEvents();
                    try
                    {
                        pb.Value = ii;
                    }
                    catch
                    {
                    }
                    if (f.Name != "er_" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + ".info")
                    {
                        if (prop.ExportDoCopy)
                        {
                            try
                            {
                                string p = "";
                                p += "\\Copy\\";
                                if (!Directory.Exists(prop.Dir_export + p))
                                    Directory.CreateDirectory(prop.Dir_export + p);
                                p += DateTime.Now.Year.ToString() + "\\";
                                if (!Directory.Exists(prop.Dir_export + p))
                                    Directory.CreateDirectory(prop.Dir_export + p);
                                p += DateTime.Now.Month < 10
                                         ? "0" + DateTime.Now.Month.ToString() + "\\"
                                         : DateTime.Now.Month.ToString() + "\\";
                                if (!Directory.Exists(prop.Dir_export + p))
                                    Directory.CreateDirectory(prop.Dir_export + p);
                                p += DateTime.Now.Day < 10
                                         ? "0" + DateTime.Now.Day.ToString() + "\\"
                                         : DateTime.Now.Day.ToString() + "\\";
                                if (!Directory.Exists(prop.Dir_export + p))
                                    Directory.CreateDirectory(prop.Dir_export + p);

                                File.Copy(f.FullName, prop.Dir_export + p + f.Name, true);
                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                               "] - Файл " + f.Name + " успешно скопирован в резервный каталог.\n");
                            }
                            catch (Exception ex)
                            {
                                ErrorNfo.WriteErrorInfo(ex);
                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                               "] ! Ошибка копирования файла " + f.Name + " в резервный каталог." + ex.Message + "\n" + ex.Source + "\n");
                            }
                        }
                        if (prop.Export_from_ftp)
                        {
                            try
                            {
                                PSA.Lib.Util.ftpClient ftp = new PSA.Lib.Util.ftpClient(prop.FTP_Server_Export, prop.FTP_User, prop.FTP_Password);
                                if (ftp.Upload(prop.Dir_export + "\\" + f.Name, prop.FTP_Path_Export + "/info/" + f.Name))
                                {
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                        "] - Файл " + f.Name + " успешно скопирован на ftp сервер.\n");
                                    if (ftp.Download(prop.Dir_export, f.Name + ".tmp", prop.FTP_Path_Export, "/info/" + f.Name))
                                    {
                                        if (checkFiles(prop.Dir_export + "\\" + f.Name, prop.Dir_export + "\\" + f.Name + ".tmp"))
                                        {

                                            if (prop.ExportClearDirAfterCopy)
                                            {
                                                try
                                                {
                                                    f.Delete();
                                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                        "] - Файл " + f.Name + " успешно удален.\n");
                                                    File.Delete(prop.Dir_export + "\\" + f.Name + ".tmp");
                                                }
                                                catch (Exception ex)
                                                {
                                                    ErrorNfo.WriteErrorInfo(ex);
                                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                                    "] ! Ошибка удаления файла " + f.Name + "." + ex.Message + "\n" +
                                                    ex.Source + "\n");
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                    "] ! Ошибка копирования файла " + f.Name + " на ftp сервер.\n");
                                }
                            }
                            catch (Exception ex)
                            {
                                ErrorNfo.WriteErrorInfo(ex);
                                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() +
                                    "] ! Ошибка копирования файла " + f.Name + " на ftp сервер." + ex.Message + "\n" +
                                    ex.Source + "\n");
                            }
                        }
                    }
                    ii++;
                }
				if (doit)
					iniRobot.IniWriteValue("export", "clear", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString());
				doit = false;

            
            }
            else
            {
                wtl("[" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "] ! Каталог экспорта не найден!\n");
            }
            pb.Value = pb.Minimum;
        }
    }
}
