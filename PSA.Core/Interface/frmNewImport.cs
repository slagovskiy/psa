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
using Xceed.Ftp;
using Xceed.FileSystem;
using System.Threading;

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

            this.Title = "Автоматический обмен заказами";

            tbl.Columns.Add("Выбрано", Type.GetType("System.Boolean"));
            tbl.Columns.Add("Номер", Type.GetType("System.String"));
            tbl.Columns.Add("Статус", Type.GetType("System.String"));
            tbl.Columns.Add("Дата", Type.GetType("System.String"));
            tbl.Columns.Add(" ", Type.GetType("System.String"));

            tbl.Columns[0].Caption = "Выбрано";
            tbl.Columns[1].Caption = "Номер";
            tbl.Columns[2].Caption = "Статус";
            tbl.Columns[3].Caption = "Дата";
            tbl.Columns[3].Caption = " ";
            tbl.Columns[1].ReadOnly = true;
            tbl.Columns[2].ReadOnly = true;
            tbl.Columns[3].ReadOnly = true; 
            tbl.Columns[4].ReadOnly = true;

            db_connection.ConnectionString = prop.Connection_string;
            db_connection.Open();

        }

        private void frmNewImport_Load(object sender, EventArgs e)
        {
            loadOrders();

            dateFilterStart1.Value = DateTime.Now.AddDays(-3);
            dateFilterStop1.Value = DateTime.Now.AddDays(+1);
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
                        bool found_k = false;
                        foreach (FileInfo file in sub.GetFiles())
                        {
                            if ((file.Extension.ToLower() == ".import") || (file.Extension.ToLower() == ".export"))
                            {
                                found = true;
                            }

                            if (file.Name == ".k")
                            {
                                found_k = true;
                            }

                            if (file.Name == ".lock")
                            {
                                found_k = false;
                                break;
                            }
                            //if (file.Extension.ToLower() == ".loaded")
                            //{
                            //    found = false;
                            //    break;
                            //}
                            //SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM [order_import] WHERE [number] = '" + sub.Name + "'", db_connection);
                            //if ((int)cmd.ExecuteScalar() > 0)
                            //{
                                //found = false;
                                //break;
                            //}
                        }
                        if (found)
                        {
                            string _status = "";
                            string _date = "";
                            if (File.Exists(sub.FullName + "\\" + sub.Name + ".export"))
                            {
                                using (StreamReader r = new StreamReader(sub.FullName + "\\" + sub.Name + ".export"))
                                {
                                    string line = r.ReadLine();
                                    if (line == "[order]")
                                    {
                                        line = r.ReadLine();
                                        line = line.Replace(";000100;", ";В очереди на печать;");
                                        line = line.Replace(";000200;", ";В очереди на обработку;");
                                        line = line.Replace(";000110;", ";В процессе печати;");
                                        line = line.Replace(";000210;", ";В процессе обработки;");
                                        line = line.Replace(";000000;", ";На выдаче;");
                                        line = line.Replace(";000010;", ";На предпросмотре;");
                                        line = line.Replace(";000001;", ";В ожидании оплаты;");
                                        line = line.Replace(";100000;", ";Выдано;");
                                        line = line.Replace(";010000;", ";Отменено;");
                                        line = line.Replace(";000111;", ";Готово после печати;");
                                        line = line.Replace(";000211;", ";Готово после обработки;");
                                        line = line.Replace(";000212;", ";Готово после обработки, предпросмотр;");
                                        line = line.Replace(";200000;", ";выдано без оплаты;");
                                        line = line.Replace(";300000;", ";Утерян;");
                                        string[] _data = line.Split(';');
                                        _status = _data[12];
                                        _date = _data[14];
                                    }
                                }
                                
                            }
                            DataRow rw = tbl.NewRow();
                            rw[0] = false;
                            rw[1] = sub.Name;
                            if (found_k)
                                rw[4] = "конверт";
                            rw[2] = _status;
                            rw[3] = _date;
                            tbl.Rows.Add(rw);
                        }
                    }

                    data.DataSource = tbl;
                    data.Columns[0].Width = 60;
                    data.Columns[1].Width = 85;
                    data.Columns[2].Width = 120;
                    data.Columns[3].Width = 80;
                    data.Columns[4].Width = 60;
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
                    rw[0] = true;
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
                    rw[0] = false;
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
                log.Items.Add("Загрузка заказов началась");
                log.SelectedIndex = log.Items.Count - 1;
                Application.DoEvents();

                pb.Visible = true;
                btnLoadSelected.Enabled = false;
                btnUpdate.Enabled = false;
                checkPrintCheck.Enabled = false;

                Application.DoEvents();
                bool unknown = false;
                int clientid = 0;
                int cnt = 0;
                foreach (DataRow rw in tbl.Rows)
                {
                    if ((Boolean)rw[0])
                    {
                        SqlCommand cmd = new SqlCommand("SELECT COUNT([number]) FROM [order] WHERE [number] = '" + rw[1].ToString() + "'", db_connection);
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
                    if ((Boolean)rw[0])
                    {
                        DirectoryInfo dir = new DirectoryInfo(prop.Dir_auto_import + "\\" + rw[1].ToString());
                        foreach (FileInfo file in dir.GetFiles())
                        {
                            if ((file.Extension.ToLower() == ".import") || (file.Extension.ToLower() == ".export"))
                            {
                                string number = file.Name.Replace(file.Extension, "");
                                log.Items.Add("Загружаем " + number);
                                log.SelectedIndex = log.Items.Count - 1;
                                Application.DoEvents();
                                log.Items.Add("Читаем файл и загружаем");
                                if (ImportFile(file.FullName, checkPrintCheck.Checked, clientid))
                                {
                                    try
                                    {
                                        //File.Create(dir.FullName + "\\.loaded");
                                        //SqlCommand cmd = new SqlCommand("INSERT INTO [order_import] ([number]) VALUES ('" + rw[1].ToString() + "')", db_connection);
                                        //cmd.ExecuteNonQuery();

                                        log.Items.Add("Проверяется загруженный заказ");
                                        log.SelectedIndex = log.Items.Count - 1;
                                        Application.DoEvents();

                                        OrderInfo order = new OrderInfo(db_connection, number);

                                        log.Items.Add("Копируются файлы для печати");
                                        log.SelectedIndex = log.Items.Count - 1;
                                        Application.DoEvents();

                                        string dy = DateTime.Parse(order.Datein).Year.ToString("D4");
                                        string dm = DateTime.Parse(order.Datein).Month.ToString("D2");
                                        string dd = DateTime.Parse(order.Datein).Day.ToString("D2");

                                        Directory.CreateDirectory(prop.Dir_print + "\\" + dy + "\\" + dm + "\\" + dd + "\\" + number);

                                        string SourcePath = prop.Dir_auto_import + "\\" + rw[1].ToString() + "\\print\\" + rw[1].ToString() + "\\";
                                        string DestinationPath = prop.Dir_print + "\\" + dy + "\\" + dm + "\\" + dd + "\\" + number + "\\";

                                        if (Directory.Exists(SourcePath))
                                        {
                                            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                                                SearchOption.AllDirectories))
                                                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));


                                            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                                                SearchOption.AllDirectories))
                                                if (!File.Exists(newPath.Replace(SourcePath, DestinationPath)))
                                                    File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath));
                                        }
                                        else
                                        {
                                            log.Items.Add("!!! Не удалось скопировать файлы для печати !!!");
                                            log.SelectedIndex = log.Items.Count - 1;
                                            MessageBox.Show("Не удалось скопировать файлы для печати");
                                        }

                                        log.Items.Add("Копируются файлы для обработки");
                                        log.SelectedIndex = log.Items.Count - 1;
                                        Application.DoEvents();

                                        Directory.CreateDirectory(prop.Dir_edit + "\\" + dy + "\\" + dm + "\\" + dd + "\\" + number);

                                        SourcePath = prop.Dir_auto_import + "\\" + rw[1].ToString() + "\\edit\\" + rw[1].ToString() + "\\";
                                        DestinationPath = prop.Dir_edit + "\\" + dy + "\\" + dm + "\\" + dd + "\\" + number + "\\";

                                        if (Directory.Exists(SourcePath))
                                        {
                                            foreach (string dirPath in Directory.GetDirectories(SourcePath, "*",
                                                SearchOption.AllDirectories))
                                                Directory.CreateDirectory(dirPath.Replace(SourcePath, DestinationPath));


                                            foreach (string newPath in Directory.GetFiles(SourcePath, "*.*",
                                                SearchOption.AllDirectories))
                                                if (!File.Exists(newPath.Replace(SourcePath, DestinationPath)))
                                                    File.Copy(newPath, newPath.Replace(SourcePath, DestinationPath));
                                        }
                                        else
                                        {
                                            log.Items.Add("!!! Не удалось скопировать файлы для обработки !!!");
                                            log.SelectedIndex = log.Items.Count - 1;
                                            MessageBox.Show("Не удалось скопировать файлы для обработки");
                                        }

                                        log.Items.Add("Заказ " + number + " загружен");
                                        log.SelectedIndex = log.Items.Count - 1;
                                        Application.DoEvents();

                                        log.Items.Add("Заказ " + number + " перемещается в архив");
                                        log.SelectedIndex = log.Items.Count - 1;
                                        Application.DoEvents();
                                        try
                                        {
                                            if (!Directory.Exists(prop.Dir_auto_import + "\\ok\\"))
                                                Directory.CreateDirectory(prop.Dir_auto_import + "\\ok\\");
                                            Directory.Move(prop.Dir_auto_import + "\\" + rw[1].ToString(),
                                                prop.Dir_auto_import + "\\ok\\" + rw[1].ToString());
                                        }
                                        catch
                                        {
                                            log.Items.Add("Заказ " + number + " не перемещен в архив");
                                            log.SelectedIndex = log.Items.Count - 1;
                                            Application.DoEvents();
                                            MessageBox.Show("Не удалось переместить заказ в архив");
                                        }



                                    }
                                    catch { }
                                }
                                else
                                {
                                    log.Items.Add("Не удалось загрузить заказ из файла");
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
                log.Items.Add("Загрузка завершена");
                log.SelectedIndex = log.Items.Count - 1;
                Application.DoEvents();

                MessageBox.Show("Загрузка завершена!");

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
                                log.Items.Add("Читается секция order");
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
                                                    log.Items.Add("Обновляется информация в шапке заказа");
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
                                                    log.Items.Add("Добавляется новая шапка для заказа");
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
                                log.Items.Add("Читается секция body");
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
                                                log.Items.Add("Обновляется строка в табличной части");
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
                                                log.Items.Add("Добавляется строка в табличкую часть");
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
                                log.Items.Add("Читается секция events");
                                try
                                {
                                    string[] r = str.Split(';');
                                    if (r.Length > 1)
                                    {
                                        if (loadthis)
                                        {
                                            //MessageBox.Show(str + " " + r.Length.ToString());
                                            string query = "SELECT * FROM [orderevent] WHERE [guid] = '" + r[2] + "'";
                                            SqlCommand body_cmd = new SqlCommand(query, db_connection);
                                            body_cmd.CommandTimeout = 9000;
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
                                                    log.Items.Add("Добавляется строка в историю");
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
                                                            ",CONVERT(DATETIME, '" + DateToSql(r[4]) + "', 120) " +
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
                                }
                                catch (Exception ex)
                                {
                                    log.Items.Add("Произошла ошибка во время загрузки истории " + ex.Message);
                                }
                                break;
                            }
                    }
                }
                fs.Close();
                log.Items.Add("Чтение завершено");
                if (print)
                {
                    foreach (string n in orderToPrint)
                    {
                        while (rep.IsBusy)
                        {
                            Application.DoEvents();
                        }
                        log.Items.Add("Пробуем напечатать чек");
                        PrintCheck(n);
                    }
                }
                log.Items.Add("Загрузка прошла успешно, возвращаем true");
                ok = true;
            }
            catch (Exception ex)
            {
                log.Items.Add("Произошла ошибка во время загрузки");
                //MessageBox.Show(ex.Message + "\n" + ex.Source);
                ok = false;
            }
            return ok;
        }



        private void PrintCheck(string num)
        {
            // Печатаем чек
            log.Items.Add("Начинаем печать чека");
            using (SqlConnection con = new SqlConnection(prop.Connection_string))
            {
                con.Open();
                log.Items.Add("Получаем объект заказа");
                OrderInfo prnOrder = new OrderInfo(con, num, true);
                try
                {
                    if (prop.PathReportsTemplates != "")
                    {
                        log.Items.Add("Загружаем шаблон для печати");
                        rep.Load(prop.PathReportsTemplates, "Check");
                        log.Items.Add("Заполняем поля в шаблоне");
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
                            log.Items.Add("Показываем превью чека");
                            PrintPreviewDialog pd = new PrintPreviewDialog();
                            pd.ClientSize = new Size(465, 680);
                            pd.StartPosition = FormStartPosition.CenterScreen;
                            pd.PrintPreviewControl.Zoom = 1.5;
                            pd.Document = rep.Document;
                            pd.ShowDialog();
                        }
                        else
                        {
                            log.Items.Add("Отправляем шаблон в принтер");
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

        private void btnCheckPionts_Click(object sender, EventArgs e)
        {
            using (frmCheckFtp frm = new frmCheckFtp())
            {
                frm.ShowDialog();
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            try
            {
                Xceed.Ftp.Licenser.LicenseKey = "FTN42-K40Z3-DXCGS-PYGA";

                SqlConnection cn = new SqlConnection(prop.Connection_string);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                data2.Columns.Clear();
                data2.Rows.Clear();
                data2.Columns.Add("name", "Точка");
                data2.Columns.Add("rezult", "Результат");
                data2.Columns[0].Width = 200;
                data2.Columns[1].Width = 350;

                cn = new SqlConnection(prop.Connection_string);
                cn.Open();

                cmd = new SqlCommand("SELECT * FROM [place];", cn);

                DataTable tbl = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(tbl);
                pb2.Minimum = 0;
                pb2.Maximum = tbl.Rows.Count;
                pb2.Value = 0;
                foreach (DataRow r in tbl.Rows)
                {
                    try
                    {
                        using (FtpConnection connection = new FtpConnection(
                            r["server"].ToString().Trim(),
                            r["username"].ToString().Trim(),
                            r["password"].ToString().Trim()))
                        {
                            connection.Timeout = 10;
                            string tmp = Guid.NewGuid().ToString();
                            StreamWriter w = new StreamWriter(System.IO.Path.GetTempPath() + tmp);
                            w.Write(tmp);
                            w.Close();

                            connection.Encoding = Encoding.GetEncoding(1251);

                            DiskFile source = new DiskFile(System.IO.Path.GetTempPath() + tmp);
                            string ftp_to = r["path"].ToString().Trim();
                            if (ftp_to.Substring(0, 1) == "/") ftp_to = ftp_to.Substring(1);
                            FtpFolder destination = new FtpFolder(connection, ftp_to);

                            source.CopyTo(destination, true);

                            Thread.Sleep(2000);

                            FtpFile remote = new FtpFile(connection, ftp_to + tmp);

                            remote.Delete();
                        }
                        data2.Rows.Add(new string[] { r["name"].ToString().Trim(), "ok" });
                    }
                    catch (Exception ex)
                    {
                        data2.Rows.Add(new string[] { r["name"].ToString().Trim(), "ошибка: " + ex.Message });
                    }
                    finally
                    {
                        pb2.Value++;
                        Application.DoEvents();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                btnStart.Enabled = true;
                MessageBox.Show("ok");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdateSendOrder_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(prop.Connection_string))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandText = @"SELECT dbo.[order].number, dbo.order_status.status_desc, dbo.[order].input_date, dbo.place.name, dbo.[order].status_export, 
                                        dbo.[order].status_export_date
                                        FROM dbo.[order] INNER JOIN
                                        dbo.place ON dbo.[order].id_place = dbo.place.id_place INNER JOIN
                                        dbo.order_status ON dbo.[order].status = dbo.order_status.order_status
                                        WHERE (dbo.[order].auto_export > 0)";
                    cmd.CommandTimeout = 9000;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable tbl = new DataTable();

                    da.Fill(tbl);

                    DataTable _tbl = new DataTable();
                    _tbl.Columns.Add("Номер");
                    _tbl.Columns.Add("Статус");
                    _tbl.Columns.Add("Принят");
                    _tbl.Columns.Add("Отправляется в");
                    _tbl.Columns.Add("Ошибки отправки");
                    _tbl.Columns.Add("Время последней попытки");

                    foreach (DataRow rw in tbl.Rows)
                    {
                        DataRow _rw = _tbl.NewRow();
                        _rw[0] = rw[0].ToString().Trim();
                        _rw[1] = rw[1].ToString().Trim();
                        _rw[2] = DateTime.Parse(rw[2].ToString()).ToString("u");
                        _rw[3] = rw[3].ToString().Trim();
                        _rw[4] = rw[4].ToString().Trim();
                        _rw[5] = (rw[5].ToString() != "" ? DateTime.Parse(rw[5].ToString()).ToString("u") : "");

                        _tbl.Rows.Add(_rw);

                    }
                    data3.DataSource = _tbl;

                }
            }
            catch { }
            finally { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(prop.Connection_string))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandText = @"SELECT     dbo.[order].number, dbo.order_status.status_desc, dbo.[order].input_date, dbo.place.name, dbo.[order].status_export, 
                      dbo.[order].status_export_date
FROM         dbo.[order] INNER JOIN
                      dbo.place ON dbo.[order].id_place = dbo.place.id_place INNER JOIN
                      dbo.order_status ON dbo.[order].status = dbo.order_status.order_status
WHERE     (dbo.[order].auto_export = - 1) AND (dbo.place.id_place > 0) AND (dbo.[order].input_date > CONVERT(DATETIME, '" + dateFilterStart1.Value.ToString("yyyy-MM-dd") + @" 00:00:00', 102) AND 
                      dbo.[order].input_date < CONVERT(DATETIME, '" + dateFilterStop1.Value.ToString("yyyy-MM-dd") + " 23:59:59', 102))";
                    cmd.CommandTimeout = 9000;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable tbl = new DataTable();

                    da.Fill(tbl);

                    DataTable _tbl = new DataTable();
                    _tbl.Columns.Add("Номер");
                    _tbl.Columns.Add("Статус");
                    _tbl.Columns.Add("Принят");
                    _tbl.Columns.Add("Отправлено в");
                    _tbl.Columns.Add("Статус отправки");
                    _tbl.Columns.Add("Время отправки");

                    foreach (DataRow rw in tbl.Rows)
                    {
                        DataRow _rw = _tbl.NewRow();
                        _rw[0] = rw[0].ToString().Trim();
                        _rw[1] = rw[1].ToString().Trim();
                        _rw[2] = DateTime.Parse(rw[2].ToString()).ToString("u");
                        _rw[3] = rw[3].ToString().Trim();
                        _rw[4] = rw[4].ToString().Trim();
                        _rw[5] = (rw[5].ToString() != "" ? DateTime.Parse(rw[5].ToString()).ToString("u") : "");

                        _tbl.Rows.Add(_rw);

                    }
                    data4.DataSource = _tbl;

                }
            }
            catch { }
            finally { }
        }

    }
}
