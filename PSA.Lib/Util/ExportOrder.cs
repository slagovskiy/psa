using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace PSA.Lib.Util
{
    public class ExportOrder
    {

        public static void autoExport(int type, string order_id)
        {
            Settings prop = new Settings();
            SqlConnection db_connection = new SqlConnection();

            db_connection.ConnectionString = prop.Connection_string;
            db_connection.Open();

            bool finalStatus = false;
            if (type == 2)
            {
                finalStatus = true;
            }
            else
            {
                finalStatus = false;
            }

            string file = "";


            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT [id_order], id_user_accept, id_user_operator, id_user_designer, id_user_delivery, id_client, guid, del, name_accept, name_operator, name_designer, name_delivery, status, number, input_date, expected_date, output_date, advanced_payment, final_payment, discont_percent, discont_code, preview, comment, crop, type, exported, [id_order] FROM [order] WHERE (number IN (" + order_id + "))";
            cmd.Connection = db_connection;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable t = new DataTable("order");
            da.Fill(t);
            if (t.Rows.Count > 0)
            {
                for (int i = 0; i < t.Rows.Count; i++)
                {
                    string y = DateTime.Parse(t.Rows[i]["input_date"].ToString()).Year.ToString();
                    string m = DateTime.Parse(t.Rows[i]["input_date"].ToString()).Month < 10
                                   ? "0" +
                                     DateTime.Parse(t.Rows[i]["input_date"].ToString()).Month.
                                         ToString()
                                   : DateTime.Parse(t.Rows[i]["input_date"].ToString()).Month.
                                         ToString();
                    string d = DateTime.Parse(t.Rows[i]["input_date"].ToString()).Day < 10
                                   ? "0" +
                                     DateTime.Parse(t.Rows[i]["input_date"].ToString()).Day.
                                         ToString()
                                   : DateTime.Parse(t.Rows[i]["input_date"].ToString()).Day.
                                         ToString();

                    if (!Directory.Exists(prop.Dir_export + "\\auto_export\\"))
                        Directory.CreateDirectory(prop.Dir_export + "\\auto_export");
                    if (!Directory.Exists(prop.Dir_export + "\\auto_export\\" + t.Rows[i]["number"].ToString().Trim() + "\\"))
                        Directory.CreateDirectory(prop.Dir_export + "\\auto_export\\" + t.Rows[i]["number"].ToString().Trim());

                    file = prop.Dir_export + "\\auto_export\\" + t.Rows[i]["number"].ToString().Trim() + "\\" + t.Rows[i]["number"].ToString().Trim() + ".export";

                    FileInfo fli = new FileInfo(file);
                    StreamWriter fl = new StreamWriter(file, false, Encoding.GetEncoding(1251));
                    /*
                     *  0 id_user_accept, 
                     *  1 id_user_operator, 
                     *  2 id_user_designer, 
                     *  3 id_user_delivery, 
                     *  4 id_client, 
                     *  5 guid, 
                     *  6 del, 
                     *  7 name_accept, 
                     *  8 name_operator, 
                     *  9 name_designer, 
                     * 10 name_delivery, 
                     * 11 status, 
                     * 12 number, 
                     * 13 input_date, 
                     * 14 expected_date, 
                     * 15 output_date, 
                     * 16 advanced_payment, 
                     * 17 final_payment, 
                     * 18 discont_percent, 
                     * 19 discont_code, 
                     * 20 preview, 
                     * 21 comment, 
                     * 22 crop, 
                     * 23 type, 
                     * 24 exported
                     */
                    string _status = t.Rows[i][12].ToString().Trim();
                    if (_status == "100000")
                    {
                        //if(MessageBox.Show("Заказ № " + t.Rows[i][13].ToString().Trim() + " экспортируется в статусе \"Выдано\", экспортировать его в статусе \"На предпросмотре\"?", "Экспорт", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        _status = "000010";
                        //}
                    }

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
                                 _status + ";" +
                                 t.Rows[i][13].ToString().Trim().Replace(";", " ") + ";" +
                                 t.Rows[i][14].ToString().Trim().Replace(";", " ") + ";" +
                                 t.Rows[i][15].ToString().Trim().Replace(";", " ") + ";" +
                                 t.Rows[i][16].ToString().Trim().Replace(";", " ") + ";" +
                                 t.Rows[i][17].ToString().Trim().Replace(";", " ") + ";" +
                                 t.Rows[i][18].ToString().Trim().Replace(";", " ") + ";" +
                                 t.Rows[i][19].ToString().Trim().Replace(";", " ") + ";" +
                                 t.Rows[i][20].ToString().Trim().Replace(";", " ") + ";" +
                                 t.Rows[i][21].ToString().Trim().Replace(";", " ") + ";" +
								 t.Rows[i][22].ToString().Trim().Replace(";", " ").Replace("!Экспорт", "") + ";" +
                                 t.Rows[i][23].ToString().Trim().Replace(";", " ") + ";" +
                                 t.Rows[i][24].ToString().Trim().Replace(";", " ") + ";" +
                                 t.Rows[i][25].ToString().Trim().Replace(";", " "));

                    //GridOder.Rows[i + 2].Style = GridOder.Styles["Normal"];
                    if (finalStatus)
                    {
                        SqlCommand cmd_write =
                            new SqlCommand(
                                "UPDATE [order] SET [comment] = '" + t.Rows[i][22].ToString().Replace("!Экспорт", "").Trim() +
                                "', [status] = '100000' WHERE [id_order] = " + t.Rows[i][26].ToString().Trim(), db_connection);
                        cmd_write.ExecuteNonQuery();
                    }
                    else
                    {
                        SqlCommand cmd_write =
                            new SqlCommand(
                                "UPDATE [order] SET [comment] = '!Экспорт " + t.Rows[i][22].ToString().Replace("!Экспорт", "").Trim() +
                                "' WHERE [id_order] = " + t.Rows[i][26].ToString().Trim(), db_connection);
                        cmd_write.ExecuteNonQuery();
                    }

                    fl.WriteLine("[order body]");
                    SqlCommand rcmd = new SqlCommand();
                    rcmd.CommandText = "SELECT [id_order], [id_mashine], [id_material], [id_good], [guid], [del], [quantity], [actual_quantity], [sign], [price], [dateadd], [id_user_add], [name_add], [datework], [id_user_work], [name_work], [defect_quantity], [id_user_defect], [user_defect], [tech_defect], [exported] FROM [orderbody] WHERE ([id_order] in (" + t.Rows[i][0].ToString().Trim() + "))";
                    rcmd.Connection = db_connection;
                    SqlDataAdapter rda = new SqlDataAdapter(rcmd);
                    DataTable rt = new DataTable("order");
                    rda.Fill(rt);
                    if (rt.Rows.Count > 0)
                    {
                        for (int ii = 0; ii < rt.Rows.Count; ii++)
                        {
                            /*
                             *  0 [id_order], 
                             *  1 [id_mashine], 
                             *  2 [id_material], 
                             *  3 [id_good], 
                             *  4 [guid], 
                             *  5 [del], 
                             *  6 [quantity], 
                             *  7 [actual_quantity], 
                             *  8 [sign], 
                             *  9 [price], 
                             * 10 [dateadd], 
                             * 11 [id_user_add], 
                             * 12 [name_add], 
                             * 13 [datework], 
                             * 14 [id_user_work], 
                             * 15 [name_work], 
                             * 16 [defect_quantity], 
                             * 17 [id_user_defect], 
                             * 18 [user_defect], 
                             * 19 [tech_defect], 
                             * 20 [exported]
                             */
                            //Application.DoEvents();

                            fl.WriteLine(rt.Rows[ii][0].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][1].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][2].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][3].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][4].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][5].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][6].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][7].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][8].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][9].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][10].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][11].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][12].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][13].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][14].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][15].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][16].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][17].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][18].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][19].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][20].ToString().Trim().Replace(";", " "));

                        }
                    }

                    fl.WriteLine("[order events]");
                    //AddEvent("Заказ был экспортирован", int.Parse(t.Rows[i][0].ToString().Trim()));
                    rcmd = new SqlCommand();
                    rcmd.CommandText = "SELECT [id_orderevent], [del], [guid], [id_order], [event_date], [event_user], [event_status], [event_point], [event_text] FROM [dbo].[orderevent] WHERE ([id_order] in (" + t.Rows[i][0].ToString().Trim() + "))";
                    rcmd.Connection = db_connection;
                    rda = new SqlDataAdapter(rcmd);
                    rt = new DataTable("order");
                    rda.Fill(rt);
                    if (rt.Rows.Count > 0)
                    {
                        for (int ii = 0; ii < rt.Rows.Count; ii++)
                        {
                            /*
                             *  [id_orderevent], 
                             *  [del], 
                             *  [guid], 
                             *  [id_order], 
                             *  [event_date], 
                             *  [event_user], 
                             *  [event_status], 
                             *  [event_point], 
                             *  [event_text]
                             */
                            //Application.DoEvents();

                            fl.WriteLine(rt.Rows[ii][0].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][1].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][2].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][3].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][4].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][5].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][6].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][7].ToString().Trim().Replace(";", " ") + ";" +
                                         rt.Rows[ii][8].ToString().Trim().Replace(";", " "));

                        }
                    }


                    StreamWriter batw =
                        new StreamWriter(file + ".bat", false, Encoding.GetEncoding(866));
                    batw.WriteLine("@echo off\n");
                    batw.WriteLine("mkdir edit\n");
                    batw.WriteLine("mkdir print\n");
                    //////////////
                    batw.WriteLine("echo Копируются файлы для печати заказа № " +
                                   t.Rows[i]["number"].ToString().Trim());
                    batw.WriteLine("xcopy \"" + prop.Dir_print + "\\" + y + "\\" + m + "\\" + d +
                                   "\\" + t.Rows[i]["number"].ToString().Trim() + "\\*.*" +
                                   "\" print\\" + t.Rows[i]["number"].ToString().Trim() +
                                   "\\ /E /Y");
                    batw.WriteLine("echo Копируются файлы для обработки заказа № " +
                                   t.Rows[i]["number"].ToString().Trim());
                    batw.WriteLine("xcopy \"" + prop.Dir_edit + "\\" + y + "\\" + m + "\\" + d +
                                   "\\" + t.Rows[i]["number"].ToString().Trim() + "\\*.*" +
                                   "\" edit\\" + t.Rows[i]["number"].ToString().Trim() +
                                   "\\ /E /Y");
                    batw.Close();
                    if (File.Exists(file + ".bat"))
                    {
                        System.Diagnostics.Process pr = new System.Diagnostics.Process();
                        pr.StartInfo.WorkingDirectory = fli.DirectoryName;
                        pr.StartInfo.FileName = file + ".bat";
                        pr.Start();
                    }
                    fl.Close();
                }
                
            }


            //System.Diagnostics.Process pre = new System.Diagnostics.Process();
            //pre.StartInfo.WorkingDirectory = System.Environment.GetEnvironmentVariables()["SystemRoot"].ToString();
            //pre.StartInfo.Arguments = (new FileInfo(file).Directory.ToString());
            //pre.StartInfo.FileName = "explorer.exe";
            //pre.Start();
        }
    }
}
