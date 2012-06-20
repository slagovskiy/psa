using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using Photoland.Security.User;

using Photoland.Order;
using Photoland.Forms.Interface;

namespace PSA.Lib.Interface
{
    public partial class frmNewImport : PSA.Lib.Interface.Templates.frmTDialog
    {
        public SqlConnection db_connection = new SqlConnection();
        public UserInfo usr;
        public PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

        private DataTable tbl = new DataTable("Data");

        public frmNewImport()
        {
            InitializeComponent();

            this.Title = "Импорт заказов";

            tbl.Columns.Add("Checked", Type.GetType("System.Boolean"));
            tbl.Columns.Add("Number", Type.GetType("System.String"));

            tbl.Columns["Checked"].Caption = "Выбрано";
            tbl.Columns["Number"].Caption = "Номер";
            tbl.Columns["Number"].ReadOnly = true;

            db_connection.ConnectionString = prop.Connection_string;
            db_connection.Open();

        }

        private void frmNewImport_Load(object sender, EventArgs e)
        {
            loadOrders();
        }

        private void loadOrders()
        {
            if (Directory.Exists(prop.Dir_auto_import))
            {
                try
                {
                    tbl.Rows.Clear();
                    DirectoryInfo dir = new DirectoryInfo(prop.Dir_auto_import);
                    foreach (DirectoryInfo sub in dir.GetDirectories())
                    {
                        bool found = false;
                        foreach (FileInfo file in sub.GetFiles())
                        {
                            if ((file.Extension.ToLower() == ".import") || (file.Extension.ToLower() == ".export"))
                                found = true;

                            if (file.Extension.ToLower() == ".loaded")
                            {
                                found = false;
                                break;
                            }
                        }
                        if (found)
                        {
                            DataRow rw = tbl.NewRow();
                            rw["Checked"] = false;
                            rw["Number"] = sub.Name;
                            tbl.Rows.Add(rw);
                        }
                    }

                    data.DataSource = tbl;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка получения информации о каталоге импорта\n" + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Не обнаружена папка для импорта заказов, проверьте настройки программы.");
            }
        }

        private void btnSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                foreach (DataRow rw in tbl.Rows)
                {
                    rw["Checked"] = true;
                }
            }
            catch { }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                foreach (DataRow rw in tbl.Rows)
                {
                    rw["Checked"] = false;
                }
            }
            catch { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            loadOrders();
        }

        private void btnLoadSelected_Click(object sender, EventArgs e)
        {
            try
            {
                pb.Visible = true;
                btnClose.Enabled = false;
                btnLoadSelected.Enabled = false;
                btnUpdate.Enabled = false;
                checkPrintCheck.Enabled = false;

                Application.DoEvents();
                bool unknown = false;
                int clientid = 0;
                int cnt = 0;
                foreach (DataRow rw in tbl.Rows)
                {
                    if ((Boolean)rw["Checked"])
                    {
                        SqlCommand cmd = new SqlCommand("SELECT COUNT([number]) FROM [order] WHERE [number] = '" + rw["Number"].ToString() + "'", db_connection);
                        if ((int)cmd.ExecuteScalar() == 0)
                        {
                            unknown = true;
                        }
                        cnt++;
                    }
                }

                if (unknown)
                {
                    frmSelectClient fClient = new frmSelectClient(db_connection, usr, "Укажите клиента, который будет указан во всех новых заказах", 8);
                    fClient.ShowDialog();
                    if (fClient.DialogResult == DialogResult.OK)
                    {
                        clientid = fClient.client.Id;
                    }
                    else
                    {
                        return;
                    }
                }

                pb.Minimum = 0;
                pb.Maximum = cnt;
                pb.Value = 0;

                foreach (DataRow rw in tbl.Rows)
                {
                    if ((Boolean)rw["Checked"])
                    {
                        DirectoryInfo dir = new DirectoryInfo(prop.Dir_auto_import + "\\" + rw["Number"].ToString());
                        foreach (FileInfo file in dir.GetFiles())
                        {
                            if ((file.Extension.ToLower() == ".import") || (file.Extension.ToLower() == ".export"))
                            {
                                if (ImportFile(file.FullName, checkPrintCheck.Checked, clientid))
                                {
                                    try
                                    {
                                        File.Create(dir.FullName + "\\.loaded");
                                    }
                                    catch { }
                                }
                            }
                        }
                        if (pb.Value < pb.Maximum)
                        {
                            pb.Value++;
                            Application.DoEvents();
                        }
                    }
                }
                MessageBox.Show("Загрузка завершена!");

                btnClose.Enabled = true;
                btnLoadSelected.Enabled = true;
                btnUpdate.Enabled = true;
                checkPrintCheck.Enabled = true;

                pb.Visible = false;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Загрузка завершена с ошибкой\n" + ex.Message);
            }
            finally
            {
                loadOrders();
            }
        }

        /**************************************************
         * Вынести в отдельную процедуру!!
         * ************************************************/

        private bool ImportFile(string file, bool print, int clientid)
        {
            bool ok = false;
            try
            {
                string str = "";
                string flag = "";
                DataTable _order = new DataTable();
                _order.Columns.Add("c0", System.Type.GetType("System.Boolean"));
                _order.Columns.Add("c1", System.Type.GetType("System.String"));
                _order.Columns.Add("c2", System.Type.GetType("System.String"));
                _order.Columns.Add("c3", System.Type.GetType("System.String"));
                _order.Columns.Add("c4", System.Type.GetType("System.String"));
                _order.Columns.Add("c5", System.Type.GetType("System.String"));
                _order.Columns.Add("c6", System.Type.GetType("System.String"));
                _order.Columns.Add("c7", System.Type.GetType("System.String"));
                _order.Columns.Add("c8", System.Type.GetType("System.String"));
                _order.Columns.Add("c9", System.Type.GetType("System.String"));
                _order.Columns.Add("c10", System.Type.GetType("System.String"));
                _order.Columns.Add("c11", System.Type.GetType("System.String"));
                _order.Columns.Add("c12", System.Type.GetType("System.String"));
                _order.Columns.Add("c13", System.Type.GetType("System.String"));
                _order.Columns.Add("c14", System.Type.GetType("System.String"));
                _order.Columns.Add("c15", System.Type.GetType("System.String"));
                _order.Columns.Add("c16", System.Type.GetType("System.String"));
                _order.Columns.Add("c17", System.Type.GetType("System.String"));
                _order.Columns.Add("c18", System.Type.GetType("System.String"));
                _order.Columns.Add("c19", System.Type.GetType("System.String"));
                _order.Columns.Add("c20", System.Type.GetType("System.String"));
                _order.Columns.Add("c21", System.Type.GetType("System.String"));
                _order.Columns.Add("c22", System.Type.GetType("System.String"));
                _order.Columns.Add("c23", System.Type.GetType("System.String"));
                _order.Columns.Add("c24", System.Type.GetType("System.String"));
                _order.Columns.Add("c25", System.Type.GetType("System.String"));
                _order.Columns.Add("c26", System.Type.GetType("System.String"));
                StreamReader fs = new StreamReader(file, Encoding.GetEncoding(1251));
                List<string> orderToPrint = new List<string>();
                bool globalUpdate = false;
                bool globalInsert = false;
                bool globalNoUpdate = false;
                bool globalNoInsert = false;
                bool loadthis = false;
                bool loadbody = false;
                string orderid = "";
                while (fs.Peek() >= 0)
                {
                    str = fs.ReadLine();
                    if (str == "[order]")
                    {
                        flag = "order";
                        loadthis = false;
                        orderid = "";
                    }
                    else if (str == "[order body]")
                    {
                        if (loadbody)
                            flag = "body";
                        else
                            flag = "_body";
                    }
                    else if (str == "[order events]")
                    {
                        if (loadbody)
                            flag = "events";
                        else
                            flag = "_events";
                    }

                    switch (flag)
                    {
                        case "order":
                            {
                                string[] r = str.Split(';');
                                if (r.Length > 2)
                                {
                                    loadthis = true;
                                    if (loadthis)
                                    {
                                        string query = "SELECT * FROM [order] WHERE [guid] = '" + r[6] + "'";
                                        SqlCommand order_cmd = new SqlCommand(query, db_connection);
                                        SqlDataReader rdr = order_cmd.ExecuteReader();
                                        orderToPrint.Add(r[13].Trim());
                                        if (rdr.Read())
                                        {
                                            bool localUpdate = false;
                                            globalUpdate = true;
                                            localUpdate = true;
                                            if (localUpdate)
                                            {
                                                loadbody = true;
                                                try
                                                {
                                                    string prev = "";
                                                    if (r[21] == "True")
                                                        prev = "1";
                                                    else
                                                        prev = "0";

                                                    query = "UPDATE [order] " +
                                                            "SET [id_user_accept] = " + r[0] + " " +
                                                            ",[id_user_operator] = " + r[0] + " " +
                                                            ",[id_user_designer] = " + r[0] + " " +
                                                            ",[id_user_delivery] = " + r[0] + " " +
                                                            ",[name_accept] = '" + r[8] + "' " +
                                                            ",[name_operator] = '" + r[9] + "' " +
                                                            ",[name_designer] = '" + r[10] + "' " +
                                                            ",[name_delivery] = '" + r[11] + "' " +
                                                            ",[status] = '" + r[12] + "' " +
                                                            ",[preview] = " + prev + " " +
                                                            ",[auto_export] = 0 " +
                                                            ",[comment] = '" + r[22].Replace("!Экспорт", "").Trim() + "' " +
                                                            "WHERE [guid] = '" + r[6] + "'";
                                                    using (SqlConnection cn = new SqlConnection(prop.Connection_string))
                                                    {
                                                        cn.Open();
                                                        SqlCommand u = new SqlCommand(query, cn);
                                                        u.CommandTimeout = 9000;
                                                        u.ExecuteNonQuery();
                                                        u = new SqlCommand("SELECT [id_order] FROM [order] WHERE [guid] = '" + r[6] + "'", cn);
                                                        orderid = u.ExecuteScalar().ToString().Trim();
                                                        cn.Close();
                                                    }
                                                    if (orderid != "")
                                                        AddEvent("Заказ был импортирован (обновлен)", int.Parse(orderid));
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Ошибка во время обновления шапки заказа номер " + r[13] + "\n" + ex.Message + "\n" +
                                                                    ex.Source + "\n" + ex.StackTrace);
                                                }
                                            }
                                            else
                                            {
                                                loadbody = false;
                                            }
                                        }
                                        else
                                        {
                                            bool localInsert = false;
                                            globalInsert = true;
                                            localInsert = true;
                                            if (localInsert)
                                            {
                                                loadbody = true;
                                                try
                                                {
                                                    string prev = "";
                                                    string newguid = r[6];
                                                    if (r[21] == "True")
                                                        prev = "1";
                                                    else
                                                        prev = "0";
                                                    query =
                                                        "INSERT INTO [order] ([id_user_accept],[id_client],[guid],[del],[name_accept],[status],[number],[input_date],[expected_date],[advanced_payment],[final_payment],[preview],[comment],[crop],[type]) VALUES (" +
                                                        usr.Id_user.ToString() + "," + clientid + ",'" + newguid + "',0,'" + usr.Name +
                                                        "','" + r[12] + "','" + r[13] + "',CONVERT(DATETIME, '" + DateToSql(r[14]) +
                                                        "', 120),CONVERT(DATETIME, '" +
                                                        DateToSql(r[15]) + "', 120),0,0," + prev + ",'" + r[22].Replace("!Экспорт", "").Trim() + "'," + r[23] + "," + r[24] + ")";
                                                    using (SqlConnection cn = new SqlConnection(prop.Connection_string))
                                                    {
                                                        cn.Open();
                                                        SqlCommand u = new SqlCommand(query, cn);
                                                        u.CommandTimeout = 9000;
                                                        u.ExecuteNonQuery();
                                                        u = new SqlCommand("SELECT [id_order] FROM [order] WHERE [guid] = '" + newguid + "'", cn);
                                                        orderid = u.ExecuteScalar().ToString().Trim();
                                                        cn.Close();
                                                    }
                                                    if (orderid != "")
                                                        AddEvent("Заказ был импортирован (новый)", int.Parse(orderid));
                                                }
                                                catch (Exception ex)
                                                {
                                                    MessageBox.Show("Ошибка во время добавления нового заказа номер " + r[13] + "\n" + ex.Message + "\n" +
                                                                    ex.Source + "\n" + ex.StackTrace);
                                                }
                                            }
                                            else
                                            {
                                                loadbody = false;
                                            }
                                        }
                                        rdr.Close();
                                    }
                                }
                                break;
                            }
                        case "body":
                            {
                                string[] r = str.Split(';');
                                if (r.Length > 1)
                                {
                                    if (loadthis)
                                    {
                                        //MessageBox.Show(str + " " + r.Length.ToString());
                                        string query = "SELECT * FROM [orderbody] WHERE [guid] = '" + r[4] + "'";
                                        SqlCommand body_cmd = new SqlCommand(query, db_connection);
                                        body_cmd.CommandTimeout = 9000;
                                        SqlDataReader rdr = body_cmd.ExecuteReader();

                                        if (rdr.Read())
                                        {
                                            try
                                            {
                                                string tmp_datework = "";
                                                if (rdr.IsDBNull(14))
                                                    tmp_datework = DateTime.Now.AddYears(-20).ToString();
                                                else
                                                    tmp_datework = rdr.GetDateTime(14).ToString();
                                                string idwork = "0";
                                                string namework = "";
                                                string iddefect = "0";
                                                string namedefect = "";
                                                if (DateTime.Parse(tmp_datework) < DateTime.Parse(DateToSql(r[13])))
                                                {
                                                    if ((rdr.GetString(16).Trim() == r[15].Trim()) && (rdr.GetInt32(15).ToString() == r[14].Trim()))
                                                    {
                                                        idwork = r[14];
                                                        namework = r[15];
                                                        iddefect = r[14];
                                                        namedefect = r[15];
                                                    }
                                                    else
                                                    {
                                                        idwork = "0";
                                                        namework = r[15];
                                                        iddefect = "0";
                                                        namedefect = r[18];
                                                    }
                                                }
                                                else
                                                {
                                                    idwork = "-1";
                                                }

                                                query = "UPDATE [orderbody] " +
                                                        "SET [id_order] = " + orderid + " ";
                                                if (r[1] != "")
                                                    query += ",[id_mashine] = " + r[1] + " ";
                                                if (r[2] != "")
                                                    query += ",[id_material] = " + r[2] + " ";
                                                if (idwork != "-1")
                                                {
                                                    query += ",[id_user_work] = " + idwork + " " +
                                                             ",[name_work] = '" + namework + "' ";
                                                    if (r[13] != "")
                                                        query += ",[datework] = CONVERT(DATETIME, '" + DateToSql(r[13]) + "', 120) ";
                                                    if (r[7] != "")
                                                        query += ",[actual_quantity] = " + r[7].Replace(",", ".") + " ";
                                                }
                                                if (r[16] != "")
                                                    query += ",[defect_quantity] = " + r[16].Replace(",", ".") + " ";
                                                if (r[17] != "")
                                                    query += ",[id_user_defect] = " + iddefect + " ";
                                                if (r[18] != "")
                                                    query += ",[user_defect] = '" + namedefect + "' ";
                                                if (r[19] != "")
                                                    query += ",[tech_defect] = " + r[19] + " ";
                                                query += "WHERE [guid] = '" + r[4] + "'";
                                                using (SqlConnection cn = new SqlConnection(prop.Connection_string))
                                                {
                                                    cn.Open();
                                                    SqlCommand u = new SqlCommand(query, cn);
                                                    u.CommandTimeout = 9000;
                                                    u.ExecuteNonQuery();
                                                    cn.Close();
                                                }
                                                if (orderid != "")
                                                    AddEvent("Обновление строки в заказе при импорте", int.Parse(orderid));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Ошибка во время обновления табличной части заказа!" + "\n" + ex.Message + "\n" +
                                                                ex.Source + "\n" + ex.StackTrace);
                                            }
                                        }
                                        else
                                        {
                                            try
                                            {
                                                string idwork = r[14];
                                                //все новые это новые, значит id = 0
                                                /*
                                                if ((idwork == "0") || (idwork == ""))
                                                    idwork = "0";
                                                else
                                                    idwork = r[14];
                                                */
                                                idwork = "0";
                                                string namework = r[15];
                                                string iddefect = "0";
                                                string namedefect = r[18];

                                                query = "INSERT INTO [orderbody] " +
                                                        "([id_order]";
                                                if (r[1] != "")
                                                    query += ",[id_mashine]";
                                                if (r[2] != "")
                                                    query += ",[id_material]";
                                                if (r[3] != "")
                                                    query += ",[id_good]";
                                                query += ",[guid]";
                                                if (r[6] != "")
                                                    query += ",[quantity]";
                                                if (r[7] != "")
                                                    query += ",[actual_quantity]";
                                                query += ",[sign]";
                                                if (r[9] != "")
                                                    query += ",[price]";
                                                if (r[10] != "")
                                                    query += ",[dateadd]";
                                                query += ",[id_user_add]" +
                                                         ",[name_add]";
                                                if (r[13] != "")
                                                    query += ",[datework]";
                                                query += ",[id_user_work]" +
                                                         ",[name_work]" +
                                                         ",[defect_quantity]" +
                                                         ",[id_user_defect]" +
                                                         ",[user_defect]" +
                                                         ",[tech_defect])" +
                                                         "VALUES" +
                                                         "(" + orderid + " ";
                                                if (r[1] != "")
                                                    query += "," + r[1] + " ";
                                                if (r[2] != "")
                                                    query += "," + r[2] + " ";
                                                if (r[3] != "")
                                                    query += ",'" + r[3] + "' ";
                                                query += ",'" + r[4] + "' ";
                                                if (r[6] != "")
                                                    query += "," + r[6].Replace(",", ".") + " ";
                                                if (r[7] != "")
                                                    query += "," + r[7].Replace(",", ".") + " ";
                                                query += ",'" + r[8] + "' ";
                                                if (r[9] != "")
                                                    query += "," + r[9].Replace(",", ".") + " ";
                                                if (r[10] != "")
                                                    query += ",CONVERT(DATETIME, '" + DateToSql(r[10]) + "', 120) ";
                                                query += "," + usr.Id_user + " " +
                                                         ",'" + usr.Name + "' ";
                                                if (r[13] != "")
                                                    query += ",CONVERT(DATETIME, '" + DateToSql(r[13]) + "', 120) ";
                                                query += "," + idwork + " " +
                                                        ",'" + namework + "' " +
                                                        "," + r[16].Replace(",", ".") + " " +
                                                        "," + iddefect + " " +
                                                        ",'" + namedefect + "'" +
                                                        "," + r[19] + ")";
                                                using (SqlConnection cn = new SqlConnection(prop.Connection_string))
                                                {
                                                    cn.Open();
                                                    SqlCommand u = new SqlCommand(query, cn);
                                                    u.CommandTimeout = 9000;
                                                    u.ExecuteNonQuery();
                                                    cn.Close();
                                                }
                                                if (orderid != "")
                                                    AddEvent("Добавление строки в заказ при импорте", int.Parse(orderid));
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Ошибка во время обновления табличной части заказа!" + "\n" + ex.Message + "\n" +
                                                                ex.Source + "\n" + ex.StackTrace);
                                            }
                                        }
                                        rdr.Close();
                                    }
                                }
                                break;
                            }

                        case "events":
                            {
                                string[] r = str.Split(';');
                                if (r.Length > 1)
                                {
                                    if (loadthis)
                                    {
                                        //MessageBox.Show(str + " " + r.Length.ToString());
                                        string query = "SELECT * FROM [orderevent] WHERE [guid] = '" + r[2] + "'";
                                        SqlCommand body_cmd = new SqlCommand(query, db_connection);
                                        SqlDataReader rdr = body_cmd.ExecuteReader();

                                        if (rdr.Read())
                                        {
                                        }
                                        else
                                        {
                                            try
                                            {
                                                DateTime _d;
                                                try
                                                {
                                                    _d = DateTime.Parse(r[4]);
                                                }
                                                catch (Exception)
                                                {
                                                    _d = DateTime.Now;
                                                }

                                                query = "INSERT INTO [dbo].[orderevent]" +
                                                        "([id_order]" +
                                                        ",[event_user]" +
                                                        ",[event_status]" +
                                                        ",[event_point]" +
                                                        ",[event_date]" +
                                                        ",[guid]" +
                                                        ",[event_text])" +
                                                        "VALUES " +
                                                        "(" + orderid + "" +
                                                        ",'" + r[5].Trim() + "' " +
                                                        ",'" + r[6].Trim() + "' " +
                                                        ",'" + r[7] + "' " +
                                                        ",CONVERT(DATETIME, '" + _d.Year.ToString("D4") + "-" +
                                                        _d.Month.ToString("D2") + "-" + _d.Day.ToString("D2") +
                                                        " " + _d.ToShortTimeString() + "', 120) " +
                                                        ",'" + r[2] + "'" +
                                                        ",'" + r[8] + "')";
                                                using (SqlConnection cn = new SqlConnection(prop.Connection_string))
                                                {
                                                    cn.Open();
                                                    SqlCommand u = new SqlCommand(query, cn);
                                                    u.CommandTimeout = 9000;
                                                    u.ExecuteNonQuery();
                                                    cn.Close();

                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Ошибка во время переноса истории заказа!" + "\n" + ex.Message + "\n" +
                                                                ex.Source + "\n" + ex.StackTrace);
                                            }
                                        }
                                        rdr.Close();
                                    }
                                }
                                break;
                            }
                    }
                }
                fs.Close();
                if (print)
                {
                    foreach (string n in orderToPrint)
                    {
                        while (rep.IsBusy)
                        {
                            Application.DoEvents();
                        }

                        PrintCheck(n);
                    }
                }
                ok = true;
            }
            catch (Exception ex)
            {
                ok = false;
            }
            return ok;
        }



        private void PrintCheck(string num)
        {
            // Печатаем чек
            using (SqlConnection con = new SqlConnection(prop.Connection_string))
            {
                con.Open();
                OrderInfo prnOrder = new OrderInfo(con, num, true);
                try
                {
                    if (prop.PathReportsTemplates != "")
                    {
                        rep.Load(prop.PathReportsTemplates, "Check");
                        rep.DataSource.Recordset = prnOrder.OrderBody;
                        decimal itog = 0;
                        decimal iitog = 0;
                        for (int i = 0; i < prnOrder.OrderBody.Rows.Count; i++)
                        {
                            itog += decimal.Parse(prnOrder.OrderBody.Rows[i]["price"].ToString()) *
                                    decimal.Parse(prnOrder.OrderBody.Rows[i]["quantity"].ToString());
                        }
                        rep.Fields["Total"].Text = itog.ToString().Replace(",", ".");
                        rep.Fields["BarCode"].Text = prnOrder.Orderno.Trim();
                        rep.Fields["OrderNo"].Text = prnOrder.Orderno.Trim();
                        rep.Fields["DateOut"].Text = prnOrder.Dateout + " " + prnOrder.Timeout;
                        rep.Fields["Client"].Text = prnOrder.Client.Name.Trim();
                        rep.Fields["AddonInfo"].Text = prop.ReklamBlock1;
                        rep.Fields["Priemka"].Text = "Заказ принят: " + prnOrder.Datein + " " + prnOrder.Timein + "\nЗаказ принял: " + prnOrder.Name_accept;
                        string tp = "";
                        switch (prnOrder.Crop)
                        {
                            case 1:
                                {
                                    tp = "Обрезать под формат; ";
                                    break;
                                }
                            case 2:
                                {
                                    tp = "Сохранить пропорции; ";
                                    break;
                                }
                            case 3:
                                {
                                    tp = "Реальный размер; ";
                                    break;
                                }
                        }
                        if (prnOrder.Preview > 0)
                            rep.Fields["PreView"].Visible = true;
                        if (prnOrder.Type == 1) tp += "Глянцевая бумага;";
                        if (prnOrder.Type == 2) tp += "Матовая бумага;";
                        rep.Fields["TypePaper"].Text = tp;
                        if (prnOrder.Discont != null)
                        {
                            rep.Fields["Discont"].Text = prnOrder.Discont.Discserv.ToString().Replace(",", ".");
                            iitog = itog - ((itog * prnOrder.Discont.Discserv) / 100);
                        }
                        else
                        {
                            rep.Fields["Discont"].Text = "0";
                            iitog = itog;
                        }
                        switch (prop.ModelRound)
                        {
                            case 0:
                                {
                                    break;
                                }
                            case 1:
                                {
                                    if (((iitog - ((int)iitog)) <= (decimal)0.25) && ((iitog - ((int)iitog)) > 0))
                                        iitog = ((int)iitog);
                                    else if (((iitog - ((int)iitog)) > (decimal)0.25) && ((iitog - ((int)iitog)) <= (decimal)0.75))
                                        iitog = ((int)iitog) + (decimal)0.5;
                                    else if ((iitog - ((int)iitog)) > (decimal)0.75)
                                        iitog = ((int)iitog) + 1;
                                    break;
                                }
                            case 2:
                                {
                                    if (((iitog - ((int)iitog)) <= (decimal)0.45) && ((iitog - ((int)iitog)) > 0))
                                        iitog = ((int)iitog);
                                    else if (((iitog - ((int)iitog)) > (decimal)0.45) && ((iitog - ((int)iitog)) <= (decimal)0.95))
                                        iitog = ((int)iitog) + (decimal)0.5;
                                    else if ((iitog - ((int)iitog)) > (decimal)0.95)
                                        iitog = ((int)iitog) + 1;
                                    break;
                                }
                            case 3:
                                {
                                    if (((iitog - ((int)iitog)) <= (decimal)0.15) && ((iitog - ((int)iitog)) > 0))
                                        iitog = (int)iitog;
                                    else if (((iitog - ((int)iitog)) > (decimal)0.15) && ((iitog - ((int)iitog)) <= (decimal)0.65))
                                        iitog = ((int)iitog) + (decimal)0.5;
                                    else if ((iitog - ((int)iitog)) > (decimal)0.65)
                                        iitog = ((int)iitog) + 1;
                                    break;
                                }
                            case 4:
                                {
                                    if (((iitog - ((int)iitog)) <= (decimal)0.49) && ((iitog - ((int)iitog)) > 0))
                                        iitog = (int)iitog;
                                    else if ((iitog - ((int)iitog)) > (decimal)0.49)
                                        iitog = ((int)iitog) + 1;
                                    break;
                                }
                            case 5:
                                {
                                    if (((iitog - ((int)iitog)) <= (decimal)0.5) && ((iitog - ((int)iitog)) > 0))
                                        iitog = ((int)iitog) + (decimal)0.5;
                                    else if ((iitog - ((int)iitog)) > (decimal)0.5)
                                        iitog = ((int)iitog) + 1;
                                    break;
                                }
                        }
                        rep.Fields["Itogo"].Text = iitog.ToString().Replace(",", ".");
                        decimal p = prnOrder.FinalPayment + prnOrder.AdvancedPayment;
                        rep.Fields["Payment"].Text = p.ToString().Replace(",", ".");
                        rep.Fields["EndPayment"].Text = (iitog - p).ToString().Replace(",", ".");
                        if (prop.CheckPreview)
                        {
                            PrintPreviewDialog pd = new PrintPreviewDialog();
                            pd.ClientSize = new Size(465, 680);
                            pd.StartPosition = FormStartPosition.CenterScreen;
                            pd.PrintPreviewControl.Zoom = 1.5;
                            pd.Document = rep.Document;
                            pd.ShowDialog();
                        }
                        else
                        {
                            rep.Document.Print();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не выбран файл шаблонов отчетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка вывода чека\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private string DateToSql(string str)
        {
            string o = "";

            try
            {
                str = str.Replace(" AM", "").Replace(" PM", "");
                DateTime tmp = DateTime.Parse(str);

                o = tmp.Year.ToString();
                o += "/";
                o += tmp.Month < 10 ? "0" + tmp.Month.ToString() : tmp.Month.ToString();
                o += "/";
                o += tmp.Day < 10 ? "0" + tmp.Day.ToString() : tmp.Day.ToString();
                o += " ";
                o += tmp.ToShortTimeString();


            }
            catch
            {
                DateTime tmp = DateTime.Now;

                o = tmp.Year.ToString();
                o += "/";
                o += tmp.Month < 10 ? "0" + tmp.Month.ToString() : tmp.Month.ToString();
                o += "/";
                o += tmp.Day < 10 ? "0" + tmp.Day.ToString() : tmp.Day.ToString();
                o += " ";
                o += tmp.ToShortTimeString();


            }
            finally
            {

            }
            return o;
        }

        private void AddEvent(string Event, int id)
        {
            try
            {
                SqlConnection cn = new SqlConnection(prop.Connection_string);
                cn.Open();
                OrderInfo order = new OrderInfo(cn, id);
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

    }
}
