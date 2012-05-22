using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Photoland.Components.FilterRow;
using Photoland.Forms.Interface;
using Photoland.Lib;
using Photoland.Order;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.User;
using PSA.Lib.Interface;
using PSA.Lib.Util;
using PSA.Lib.Interface;

namespace Photoland.Exchanger
{
	public partial class frmMain : Form
{
        private void DoExport(int type)
        {
            if (CheckState(db_connection))
            {
                try
                {
                    frmExportScan fscan = new frmExportScan();
                    fscan.ShowDialog();
                    if (fscan.DialogResult == DialogResult.OK)
                    {
                        /*
                        pb.Visible = true;
                        pb.Minimum = 0;
                        pb.Value = 0;
                        pb.Maximum = 1;

                        string file = "";
                        string folderToExport = DateTime.Now.Year.ToString("D4") + "-" +
                                                DateTime.Now.Month.ToString("D2") + "-" +
                                                DateTime.Now.Day.ToString("D2") + " " + DateTime.Now.Hour.ToString("D2") +
                                                "-" + DateTime.Now.Minute.ToString("D2") + "-" +
                                                DateTime.Now.Second.ToString("D2");
                        if (prop.Dir_tmp_export != "")
                        {
                            if (Directory.Exists(prop.Dir_tmp_export))
                            {
                                Directory.CreateDirectory(prop.Dir_tmp_export + "\\" + folderToExport);
                                file = prop.Dir_tmp_export + "\\" + folderToExport + "\\" + folderToExport + ".export";
                            }
                            else
                            {
                                file = "!!!";
                            }
                        }
                        else
                        {
                            file = "!!!";
                        }
                        if (file == "!!!")
                        {
                            dlgSave.ShowDialog();
                            file = dlgSave.FileName;
                        }

                        if (file != "")
                        {
                            //export!!
                        }*/
                        frmSelectExportPlace fExport = new frmSelectExportPlace();
                        fExport.ShowDialog();
                        if (fExport.DialogResult == DialogResult.OK)
                        {
                            for (int i = 0; i < fscan.numbers.Count; i++)
                            {
                                db_command = new SqlCommand(
                                    "UPDATE [dbo].[order] SET [auto_export] = " + type.ToString() +
                                    ", [id_place] = " + fExport.place.ToString() +
                                    " WHERE [number] = '" + fscan.numbers[i] + "'",
                                    db_connection);
                                db_command.ExecuteNonQuery();
                            }
                            MessageBox.Show("Заказы будут экспорированы автоматически в ближайшее время.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ErrorNfo.WriteErrorInfo(ex);
                    MessageBox.Show("Произошла ошибка экспорта!\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    pb.Visible = false;
                }
            }
        }


        private void AddEvent(string Event, int id)
        {
            try
            {
				SqlConnection cn = new SqlConnection(prop.Connection_string);
				cn.Open();
                OrderInfo order = new OrderInfo(cn , id);
                string body = "";
                for (int i = 0; i < order.OrderBody.Rows.Count; i++)
                {
                    body += order.OrderBody.Rows[i][9].ToString() + "|" +
                            order.OrderBody.Rows[i][1].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][3].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][4].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][5].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][6].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][7].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][18].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][13].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][11].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][16].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][17].ToString().Trim();
                    body += "#";
                }
				if (body.Length > 0)
					body = body.Substring(0, body.Length - 1);
				body = "$$" + body + "$$" + order.AdvancedPayment + "$$" + order.FinalPayment + "$$" + order.Bonus;
                SqlCommand _cmd = new SqlCommand("INSERT INTO [dbo].[orderevent] ([id_order], [event_user], [event_status], [event_point], [event_text]) VALUES (" +
                            order.Id + ", '" + usr.Name.Trim() + "', '" + order.Distanation + "', '" + prop.Order_prefics.Trim() +
							"', '" + Event + body + "')", cn);
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
				MessageBox.Show("Ошибка записи в историю.\n" + ex.Message + "\n" + ex.Source);
            }
        }


        private void AddEvent(string Event, int id, SqlConnection cn)
        {
            try
            {
				SqlConnection cnn = new SqlConnection(prop.Connection_string);
				cnn.Open();
				OrderInfo order = new OrderInfo(cnn, id);
                string body = "";
                for (int i = 0; i < order.OrderBody.Rows.Count; i++)
                {
                    body += order.OrderBody.Rows[i][9].ToString() + "|" +
                            order.OrderBody.Rows[i][1].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][3].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][4].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][5].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][6].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][7].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][18].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][13].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][11].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][16].ToString().Trim() + "|" +
                            order.OrderBody.Rows[i][17].ToString().Trim();
                    body += "#";
                }
				if (body.Length > 0)
					body = body.Substring(0, body.Length - 1);
                body = "$$" + body + "$$" + order.AdvancedPayment + "$$" + order.FinalPayment + "$$" + order.Bonus;
                SqlCommand _cmd = new SqlCommand("INSERT INTO [dbo].[orderevent] ([id_order], [event_user], [event_status], [event_point], [event_text]) VALUES (" +
                            order.Id + ", '" + usr.Name.Trim() + "', '" + order.Distanation + "', '" + prop.Order_prefics.Trim() +
                            "', '" + Event + body + "')", cnn);
                _cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
        }


}
}
