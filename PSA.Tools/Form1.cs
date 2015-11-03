using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.Util;
using PSA.Lib.BLL;
using System.Data.SqlClient;
using System.IO;
using PSA.Lib.Interface;
using FirebirdSql.Data.FirebirdClient;
using System.Net;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using PSA.Robot;
using Xceed.Ftp;
using Xceed.FileSystem;
using Photoland.Security.User;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Xml;

namespace PSA.Tools
{
    public partial class frmMain : Form
    {
        Settings settings = new Settings();
        bool stop = false;

        UserInfo usr;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            frmLogin fl = new frmLogin();
            fl.ShowDialog();
            if (fl.DialogResult == DialogResult.OK)
            {
                usr = fl.usr;
            }
        }

        private void btnAction1a_Click(object sender, EventArgs e)
        {
            List<Inventory> lst = Inventory.getList();
            lstInventory.DataSource = lst;
            lstInventory.DisplayMember = "id";
            lstInventory.ValueMember = "id";
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(PSA.Lib.Interface.frmSetup frm = new PSA.Lib.Interface.frmSetup())
            {
                frm.ShowDialog();
                settings = new Settings();
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            stop = true;
            Application.Exit();
        }

        private void btnAction1_Click(object sender, EventArgs e)
        {
            if (lstInventory.SelectedValue != null)
            {
                chlstOrderInventory.Items.Clear();
                Inventory inv = new Inventory(int.Parse(lstInventory.SelectedValue.ToString()));
                lblInfo.Text = "Загружена инвентаризация № " + inv.id + "; Табличная часть из " + inv.body.Count.ToString() + " элементов";
                pb1.Minimum = 0;
                pb1.Maximum = inv.body.Count;
                pb1.Value = pb1.Minimum;
                foreach (InventoryBody item in inv.body)
                {
                    using (SqlConnection c = new SqlConnection(settings.Connection_string))
                    {
                        string info = "";
                        c.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "SELECT * FROM [order] WHERE [number] = '" + item.number + "'";
                        cmd.CommandTimeout = 9000;
                        cmd.Connection = c;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable t = new DataTable();
                        da.Fill(t);
                        if (t.Rows.Count > 0)
                        {
                            info += "Найден заказ №" + t.Rows[0]["number"].ToString().Trim() + " статус заказа " + t.Rows[0]["status"].ToString().Trim();
                            cmd.CommandText = "SELECT MAX(event_date) AS d FROM orderevent WHERE (id_order =" + t.Rows[0]["id_order"].ToString() + ")";
                            cmd.CommandTimeout = 9000;

                            if (!item.found)
                            {
                                info += " Заказ не был найден в момент инвентаризации.";
                                DateTime maxevent = new DateTime();
                                try
                                {
                                    maxevent = DateTime.Parse(cmd.ExecuteScalar().ToString());
                                    info += " Последнее событие в заказе произошло " + maxevent.ToString().Trim() + ".";
                                }
                                catch
                                {
                                    info += " Дату последнего события определить неудалось.";
                                }
                                if (inv.date > maxevent)
                                {
                                    info += " После инвентаризации с заказом не работали, назначаем статус УТЕРЯН.";
                                    chlstOrderInventory.Items.Add(
                                        t.Rows[0]["number"].ToString().Trim() + 
                                        ";" + 
                                        t.Rows[0]["status"].ToString().Trim()
                                        , false);
                                }
                                else
                                {
                                    info += " После инвентаризации с заказом работали, статус не меняется";
                                }
                            }
                            else
                            {
                                info += " Заказ был найден в момнет инветаризации. Ни каких действий не будет проводиться.";
                            }
                        }
                        else
                        {
                            info += "Не найден заказ №" + item.number.Trim();
                        }

                        lstinfo.Items.Add(info);
                        lstinfo.SelectedIndex = lstinfo.Items.Count-1;
                        info = "";
                        Application.DoEvents();
                        pb1.Value++;
                        if (stop)
                            return;
                    }
                }
                pb1.Value = pb1.Minimum;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            stop = true;
            Application.Exit();
        }

        private void lstinfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblInfo.Text = lstinfo.SelectedItem.ToString();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            stop = true;
        }

        private void btnAction1b_Click(object sender, EventArgs e)
        {
            saveFile.Filter = "Backup files|*.backup|All files|*.*";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                using (StreamWriter w = new StreamWriter(saveFile.FileName, true))
                {
                    try
                    {
                        pb1.Minimum = 0;
                        pb1.Maximum = chlstOrderInventory.Items.Count;
                        pb1.Value = pb1.Minimum;
                        for (int i = 0; i < chlstOrderInventory.Items.Count; i++)
                        {
                            try
                            {
                                string[] order = (chlstOrderInventory.Items[i].ToString()).Split(';');
                                using (SqlConnection con = new SqlConnection(settings.Connection_string))
                                {
                                    SqlCommand cmd = new SqlCommand();
									cmd.CommandText = "UPDATE [order] SET [status] = '300000', [exported] = 0 WHERE [number] = '" + order[0].Trim() + "';\nDECLARE @A nchar(255);\nDECLARE @B nchar(255);\nSET @A = (SELECT RTRIM(order_status_t) FROM dbo.inventorybody WHERE id_inventory = " + lstInventory.SelectedValue.ToString() + " AND order_number = '" + order[0].Trim() + "') + ' > Утеряно';\nSET @B = (SELECT RTRIM([order_status]) FROM dbo.inventorybody WHERE id_inventory = " + lstInventory.SelectedValue.ToString() + " AND order_number = '" + order[0].Trim() + "') + ' > 300000';\nUPDATE [dbo].[inventorybody] SET [order_action_t] = @A, [order_action] = @B WHERE id_inventory = " + lstInventory.SelectedValue.ToString() + " AND order_number = '" + order[0].Trim() + "';\nUPDATE [dbo].[inventory] SET [exported] = 0 WHERE id_inventory = " + lstInventory.SelectedValue.ToString() + ";";
                                    cmd.CommandTimeout = 9000;
                                    con.Open();
                                    cmd.Connection = con;
                                    cmd.ExecuteNonQuery();
                                }
                                w.WriteLine(chlstOrderInventory.Items[i].ToString() + ";300000");
                                chlstOrderInventory.SetItemChecked(i, true);
                                chlstOrderInventory.SelectedItem = chlstOrderInventory.Items[i];
                            }
                            catch
                            {
                            }
                            pb1.Value++;
                            Application.DoEvents();
                            if (stop)
                                return;
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        w.Close();
                    }
                }
            }
            pb1.Value = pb1.Minimum;
        }

        private void btnAction1с_Click(object sender, EventArgs e)
        {
            openFile.Filter = "Backup file|*.backup|All files|*.*";
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                chlstOrderInventory.Items.Clear();
                using (StreamReader r = new StreamReader(openFile.FileName))
                {
                    while (r.Peek() >= 0)
                    {
                        string l = r.ReadLine();
                        string[] item = l.Split(';');
                        if (item.Length == 3)
                            chlstOrderInventory.Items.Add(item[0] + ";" + item[1], true);
                        else
                            chlstOrderInventory.Items.Add("ERROR!;", false);
                        chlstOrderInventory.SelectedItem = chlstOrderInventory.Items[chlstOrderInventory.Items.Count - 1];
                        Application.DoEvents();
                        if (stop)
                            return;
                    }
                }

                pb1.Minimum = 0;
                pb1.Maximum = chlstOrderInventory.Items.Count;
                pb1.Value = pb1.Minimum;
                for (int i = 0; i < chlstOrderInventory.Items.Count; i++)
                {
                    try
                    {
                        string[] order = (chlstOrderInventory.Items[i].ToString()).Split(';');
                        using (SqlConnection con = new SqlConnection(settings.Connection_string))
                        {
                            SqlCommand cmd = new SqlCommand();
							cmd.CommandText = "UPDATE [order] SET [status] = '" + order[1].Trim() + "', [exported] = 0 WHERE [number] = '" + order[0].Trim() + "'";
                            cmd.CommandTimeout = 9000;
                            con.Open();
                            cmd.Connection = con;
                            cmd.ExecuteNonQuery();
                        }
                        chlstOrderInventory.SetItemChecked(i, false);
                        chlstOrderInventory.SelectedItem = chlstOrderInventory.Items[i];
                        pb1.Value++;
                        Application.DoEvents();
                        if (stop)
                            return;
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void btnAction2a_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(settings.Connection_string))
            {
                try
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("SELECT * FROM [order_status]", con);
                    cmd.CommandTimeout = 9000;
                    DataTable t = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(t);
                    foreach (DataRow r in t.Rows)
                    {
                        chlststatus.Items.Add(r["status_desc"].ToString() + "                                                 ;" + r["order_status"].ToString(), true);
                    }
                }
                catch
                {
                }
            }
        }

        private void linkall1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < chlststatus.Items.Count; i++)
            {
                chlststatus.SetItemChecked(i, true);
            }
        }

        private void linkdeall1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            for (int i = 0; i < chlststatus.Items.Count; i++)
            {
                chlststatus.SetItemChecked(i, false);
            }
        }

        private void btnAction2b_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(settings.Connection_string))
            {
                SqlCommand cmd = new SqlCommand("SELECT dbo.[order].id_order, dbo.[order].number, dbo.[order].input_date, dbo.order_status.status_desc FROM dbo.[order] INNER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status WHERE (dbo.[order].input_date < CONVERT(DATETIME, '2009-01-01 00:00:00', 102)) ORDER BY dbo.[order].input_date", con);

            }

        }

		private void button1_Click(object sender, EventArgs e)
		{
			using (frmGetBarCode f = new frmGetBarCode())
			{
				if (f.ShowDialog() == DialogResult.OK)
				{
					MessageBox.Show(f.BarCode);
					RemoteQuery rq = new RemoteQuery();
					rq.GetInfo(f.BarCode);
				}
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			try
			{
				FbConnectionStringBuilder cnString = new FbConnectionStringBuilder();
				cnString.DataSource = cstr.Text.Split('@')[0];
				cnString.Database = cstr.Text.Split('@')[1];
				cnString.UserID = "SYSDBA";
				cnString.Password = "masterkey";
				cnString.Charset = "win1251";
				cnString.Dialect = 3;
				using (FbConnection cn = new FbConnection(cnString.ToString()))
				{
					cn.Open();
					FbCommand cmd = new FbCommand(qy.Text, cn);
					FbDataAdapter da = new FbDataAdapter(cmd);
					DataTable tbl = new DataTable();
					da.Fill(tbl);
					datav.DataSource = tbl;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void btnGo_Click(object sender, EventArgs e)
		{
			try
			{
				String strURL = httpStr.Text;
				HttpWebRequest objWebRequest;
				HttpWebResponse objWebResponse;
				StreamReader streamReader;
				String strHTML;
				objWebRequest = (HttpWebRequest)WebRequest.Create(strURL);
				objWebRequest.Method = "GET";
				objWebResponse = (HttpWebResponse)objWebRequest.GetResponse();
				streamReader = new StreamReader(objWebResponse.GetResponseStream());
				strHTML = streamReader.ReadToEnd();
				httpRezult.Text = strHTML;
				streamReader.Close();
				objWebResponse.Close();
				objWebRequest.Abort();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
		}

		private void btnUnZip_Click(object sender, EventArgs e)
		{
			UnZipFile(txtZ.Text, "korder.xml");

		}

		public static bool UnZipFile(string InputPathOfZipFile, string NewName)
		{
			bool ret = true;
			try
			{
				if (File.Exists(InputPathOfZipFile))
				{
					string baseDirectory = Path.GetDirectoryName(InputPathOfZipFile);
					using (ZipInputStream ZipStream = new
					ZipInputStream(File.OpenRead(InputPathOfZipFile)))
					{
						ZipEntry theEntry;
						while ((theEntry = ZipStream.GetNextEntry()) != null)
						{
							if (theEntry.IsFile)
							{
								if (theEntry.Name != "")
								{
									string strNewFile = @"" + baseDirectory + @"\" +
														NewName;
									if (File.Exists(strNewFile))
									{
										continue;
									}
									using (FileStream streamWriter = File.Create(strNewFile))
									{
										int size = 2048;
										byte[] data = new byte[2048];
										while (true)
										{
											size = ZipStream.Read(data, 0, data.Length);
											if (size > 0)
												streamWriter.Write(data, 0, size);
											else
												break;
										}
										streamWriter.Close();
									}
								}
							}
							else if (theEntry.IsDirectory)
							{
								string strNewDirectory = @"" + baseDirectory + @"\" +
														theEntry.Name;
								if (!Directory.Exists(strNewDirectory))
								{
									Directory.CreateDirectory(strNewDirectory);
								}
							}
						}
						ZipStream.Close();
					}
				}
			}
			catch (Exception ex)
			{
				ret = false;
			}
			return ret;
		}

        private void btnSQLtoCSV_Click(object sender, EventArgs e)
        {
            txtSQLtoCSV.Text = SQL2CSV(txtSQL.Text);
        }

        private string SQL2CSV(string sql)
        {
            string ret = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(settings.Connection_string))
                {
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = sql;
                    cmd.CommandTimeout = 90000;
                    cmd.Connection = cn;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable tbl = new DataTable("sql2cmd");
                    da.Fill(tbl);
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < tbl.Rows.Count; i++)
                    {
                        if (i == 0)
                        {
                            for (int j = 0; j < tbl.Columns.Count; j++)
                            {
                                sb.Append(tbl.Columns[j].ColumnName);
                                if (j < tbl.Columns.Count - 1)
                                    sb.Append(";");
                            }
                            sb.Append("\n");
                        }
                        for (int j = 0; j < tbl.Columns.Count; j++)
                        {
                            sb.Append(tbl.Rows[i][j].ToString().Trim());
                            if (j < tbl.Columns.Count - 1)
                                sb.Append(";");
                        }
                        sb.Append("\n");
                    }
                    ret = sb.ToString();
                }
            }
            catch (Exception ex)
            {
                ret = ex.Message;
            }
            return ret;
        }

		private void button3_Click(object sender, EventArgs e)
		{
			backupDataBase(
				@"c:",
				"backup.bak",
				"backup" +
					DateTime.Now.Year.ToString("D4") +
					DateTime.Now.Month.ToString("D2") +
					DateTime.Now.Day.ToString("D2") +
					DateTime.Now.Hour.ToString("D2") +
					DateTime.Now.Minute.ToString("D2") +
					DateTime.Now.Second.ToString("D2"),
				true
				);
		}

		private void backupDataBase(string path, string backup, string zip, bool upload)
		{
			using (SqlConnection cn = new SqlConnection())
			{
				try
				{
					cn.ConnectionString = settings.Connection_string;
					cn.Open();
					SqlCommand cmd = new SqlCommand();
					cmd.Connection = cn;
					cmd.CommandTimeout = 99999;
					cmd.CommandText = "DBCC CHECKDB";
					cmd.ExecuteNonQuery();
					cmd.CommandText = "DBCC SHRINKDATABASE ([psa])";
					cmd.ExecuteNonQuery();
					cmd.CommandText = "EXEC master.dbo.sp_dropdevice " +
									  "@logicalname = N'backup'";
					cmd.ExecuteNonQuery();
					cmd.CommandText = "EXEC master.dbo.sp_addumpdevice  " +
									  "@devtype = N'disk', @logicalname = N'backup', " +
									  "@physicalname = N'" + path + "\\" + backup + "'";
					cmd.ExecuteNonQuery();
					cmd.CommandText = "BACKUP DATABASE [psa] " +
									  "TO  [backup] " +
									  "WITH NOFORMAT, INIT,  " +
									  "NAME = N'psa-Full Database Backup', " +
									  "SKIP, NOREWIND, NOUNLOAD,  STATS = 10";
					cmd.ExecuteNonQuery();
				}
				catch { }
			}
			try
			{
				string[] filenames = new string[1] { path + "\\" + backup };
				using (ZipOutputStream s = new ZipOutputStream(File.Create(path + "\\" + zip)))
				{
					s.SetLevel(9);
					byte[] buffer = new byte[4096];
					foreach (string file in filenames)
					{
						ZipEntry entry = new
							   ZipEntry(Path.GetFileName(file));
						entry.DateTime = DateTime.Now;
						s.PutNextEntry(entry);
						using (FileStream fs = File.OpenRead(file))
						{
							int sourceBytes;
							do
							{
								sourceBytes = fs.Read(buffer, 0, buffer.Length);
								s.Write(buffer, 0, sourceBytes);
							}
							while (sourceBytes > 0);
						}
					}
					s.Finish();
					s.Close();
				}
			}
			catch { }
			PSA.Lib.Util.ftpClient ftp = new PSA.Lib.Util.ftpClient(settings.FTP_Server_Export, settings.FTP_User, settings.FTP_Password);
			ftp.Upload(path + "\\" + zip, settings.FTP_Path_Export + "/" + zip);
		}


		private void button4_Click(object sender, EventArgs e)
		{
			try
			{
				string[] filenames = new string[1] { @"c:\backup.bak" };// Directory.GetFiles(@"c:\Python25\Tools\Scripts");
				using (ZipOutputStream s = new ZipOutputStream(File.Create(txtZo.Text + "\\backup.zip")))
				{
					s.SetLevel(9);
					byte[] buffer = new byte[4096];
					foreach (string file in filenames)
					{
						ZipEntry entry = new
							   ZipEntry(Path.GetFileName(file));
						entry.DateTime = DateTime.Now;
						s.PutNextEntry(entry);
						using (FileStream fs = File.OpenRead(file))
						{
							int sourceBytes;
							do
							{
								sourceBytes = fs.Read(buffer, 0, buffer.Length);
								s.Write(buffer, 0, sourceBytes);
							}
							while (sourceBytes > 0);
						}
					}
					s.Finish();
					s.Close();
				}
				MessageBox.Show("ok");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message.ToString(), "Zip Operation Error");
			}
		}

        private void button5_Click(object sender, EventArgs e)
        {
            RobotService robot = new RobotService();
            robot.doExport();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                Xceed.Ftp.Licenser.LicenseKey = "FTN42-K40Z3-DXCGS-PYGA";
                
                using (FtpConnection connection = new FtpConnection(
                    ftpAddress.Text,
                    ftpUser.Text,
                    ftpPassword.Text))
                {
                    connection.Encoding = Encoding.GetEncoding(1251);

                    DiskFolder source = new DiskFolder(ftpSource.Text);

                    FtpFolder destination = new FtpFolder(connection, ftpDestanation.Text);

                    source.CopyFilesTo(destination, true, true);
                }
                MessageBox.Show("ok");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {

            frmNewImport import = new frmNewImport();
            import.usr = usr;
            import.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RobotService robot = new RobotService();
            robot.UploadOrders();
        }

        private void btnGetToken_Click(object sender, EventArgs e)
        {
            string token = "";
            string password = "";
            string publicKey = "3c209e8c86464be69d9fbf36b1bde8a9";
            string privateKey = "b41e9b4a20c647038bc590bac05ecf30";
            string accessToken = "";
            bool accessOK = false;

            txtPublicKey.Text = publicKey;
            txtPrivateKey.Text = privateKey;
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                string webData = wc.DownloadString("http://api.pixlpark.com/oauth/requesttoken");
                Dictionary<string, string> jToken = JsonConvert.DeserializeObject<Dictionary<string, string>>(webData);
                foreach (KeyValuePair<string, string> tmp in jToken)
                    if (tmp.Key == "RequestToken")
                    {
                        token = tmp.Value;
                        break;
                    }
                txtToken.Text = token.ToString();
                password = GetHashString(token + privateKey);
                txtApiPassword.Text = password;

                try
                {
                    wc = new System.Net.WebClient();
                    webData = wc.DownloadString("http://api.pixlpark.com/oauth/accesstoken?oauth_token=" + token +
                        "&grant_type=api&username=" + publicKey + "&password=" + password);
                    jToken = JsonConvert.DeserializeObject<Dictionary<string, string>>(webData);
                    foreach (KeyValuePair<string, string> tmp in jToken)
                    {
                        if (tmp.Key == "AccessToken")
                            accessToken = tmp.Value;
                        if ((tmp.Key == "Success") && (tmp.Value == "True"))
                        {
                            lblAccess.Text = "Access: SUCCESS";
                            accessOK = true;
                        }
                    }
                    if (accessOK)
                        txtAccessToken.Text = accessToken;
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
        }

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

        private void btnGetOrderInfo_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                string webData = wc.DownloadString("http://api.pixlpark.com/orders/" + txtOrderNo.Text + "?oauth_token=" + txtAccessToken.Text);
                webData = Win1251ToUTF8(webData);
                webData = webData
                    .Replace("{\"ApiVersion\":\"1.0\",\"Result\":[{", "")
                    .Replace("}],\"ResponseCode\":200}", "");
                while (webData.IndexOf('{') > 0)
                {
                    webData = webData.Replace(
                        webData.Substring(
                            webData.IndexOf('{'), webData.IndexOf('}') - webData.IndexOf('{') + 1
                        ), "\"\"");
                }
                webData = "{" + webData + "}";
                MessageBox.Show(webData);
                Dictionary<string, string> jData = JsonConvert.DeserializeObject<Dictionary<string, string>>(webData);

                wc = new System.Net.WebClient();
                webData = wc.DownloadString("http://api.pixlpark.com/orders/" + txtOrderNo.Text + "/items/?oauth_token=" + txtAccessToken.Text);
                webData = Win1251ToUTF8(webData);
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
                    MessageBox.Show(_webData);
                    jData = JsonConvert.DeserializeObject<Dictionary<string, string>>(_webData);
                    webData = webData.Replace(
                        webData.Substring(
                            webData.IndexOf('{'), webData.IndexOf('}') - webData.IndexOf('{') + 1
                        ), "");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnGetProducts_Click(object sender, EventArgs e)
        {
            try
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                string webData = wc.DownloadString("http://api.pixlpark.com/products?oauth_token=" + txtAccessToken.Text);
                webData = Win1251ToUTF8(webData);

                foreach (string tmp in webData.Substring(webData.IndexOf('[') + 2, webData.IndexOf(']') - webData.IndexOf('[') - 3).Replace(",{", "").Split('}'))
                {
                    foreach(string tmp_ in tmp.Split(','))
                    {
                        string[] tmp__ = tmp_.Replace("\"", "").Split(':');
                        if (tmp__[0] == "Id")
                            txtData.Text += tmp__[1] + "\t";
                        if (tmp__[0] == "Title")
                            txtData.Text += tmp__[1] + "\n";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                System.Net.WebClient wc = new System.Net.WebClient();
                string webData = wc.DownloadString("http://api.pixlpark.com/orders/" + txtOrderNo.Text + "?oauth_token=" + txtAccessToken.Text);
                webData = Win1251ToUTF8(webData);
                MessageBox.Show(webData);

                webData = "{\"DATA\":[" + webData + "]}";

                XmlDocument x = (XmlDocument)JsonConvert.DeserializeXmlNode(webData);

                foreach (XmlNode r in x.GetElementsByTagName("Result"))
                {
                    foreach (XmlNode node in r.ChildNodes)
                    {
                        if (node.Name == "UserId")
                        {
                            wc = new System.Net.WebClient();
                            webData = wc.DownloadString("http://api.pixlpark.com/users/" + node.InnerText + "?oauth_token=" + txtAccessToken.Text);
                            webData = Win1251ToUTF8(webData);
                            MessageBox.Show(webData);
                        }
                    }
                }


                wc = new System.Net.WebClient();
                webData = wc.DownloadString("http://api.pixlpark.com/orders/" + txtOrderNo.Text + "/items/?oauth_token=" + txtAccessToken.Text);
                webData = Win1251ToUTF8(webData);
                MessageBox.Show(webData);
                //webData = webData
                //    .Replace("{\"ApiVersion\":\"1.0\",", "{")
                //    .Replace(",\"ResponseCode\":200}", "}");
                webData = "{\"DATA\":[" + webData + "]}";

                x = (XmlDocument)JsonConvert.DeserializeXmlNode(webData);

                foreach (XmlNode r in x.GetElementsByTagName("Result"))
                {
                    foreach (XmlNode node in r.ChildNodes)
                    {
                        MessageBox.Show(node.InnerXml);
                    }
                }
                //Dictionary<string, string> jData = JsonConvert.DeserializeObject<Dictionary<string, string>>(webData);

                /*
                wc = new System.Net.WebClient();
                webData = wc.DownloadString("http://api.pixlpark.com/orders/" + txtOrderNo.Text + "/items/?oauth_token=" + txtAccessToken.Text);
                webData = Win1251ToUTF8(webData);
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
                    MessageBox.Show(_webData);
                    jData = JsonConvert.DeserializeObject<Dictionary<string, string>>(_webData);
                    webData = webData.Replace(
                        webData.Substring(
                            webData.IndexOf('{'), webData.IndexOf('}') - webData.IndexOf('{') + 1
                        ), "");
                }
                */

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReport1_Click(object sender, EventArgs e)
        {
            string r = "\"Отчет по выданным заказам\",\"\"\n";
            string rf1 = System.IO.Path.GetTempPath() +
                        dateReportFilter.Value.Year.ToString("D4") + "-" +
                        dateReportFilter.Value.Month.ToString("D2") + "-" +
                        dateReportFilter.Value.Day.ToString("D2") + "-" +
                        settings.Order_prefics + "-R1.csv";
            string rf2 = System.IO.Path.GetTempPath() +
                        dateReportFilter.Value.Year.ToString("D4") + "-" +
                        dateReportFilter.Value.Month.ToString("D2") + "-" +
                        dateReportFilter.Value.Day.ToString("D2") + "-" +
                        settings.Order_prefics + "-R2.csv";
            double s = 0;
            r += "\"за " + dateReportFilter.Value.Day.ToString("D2") + "/" + dateReportFilter.Value.Month.ToString("D2") + "/" + dateReportFilter.Value.Year.ToString("D4") + "\",\"\"\n";
            r += ",\n,\n";
            string q = "SELECT name, SUM(sum) AS sum " +
                        "FROM (SELECT name, SUM(payment) AS sum, Expr1 " +
                        "   FROM(" +
                        "       SELECT dbo.client.name, dbo.ptype.name_ptype, dbo.[order].advanced_payment + dbo.[order].final_payment AS payment, dbo.orderevent.event_status,  " +
                        "       dbo.[order].number, MAX(dbo.orderevent.event_date) AS Expr1 " +
                        "       FROM dbo.[order] INNER JOIN " +
                        "           dbo.client ON dbo.[order].id_client = dbo.client.id_client INNER JOIN " +
                        "           dbo.ptype ON dbo.[order].ptype = dbo.ptype.id_ptype INNER JOIN " +
                        "           dbo.orderevent ON dbo.[order].id_order = dbo.orderevent.id_order " +
                        "           GROUP BY dbo.client.name, dbo.ptype.name_ptype, dbo.[order].advanced_payment + dbo.[order].final_payment, dbo.orderevent.event_status,  " +
                        "           dbo.[order].number " +
                        "           HAVING (dbo.orderevent.event_status = N'100000') OR " +
                        "           (dbo.orderevent.event_status = N'200000')) AS tbl " +
                        "   GROUP BY name, name_ptype, event_status, number, Expr1) AS t " +
                        "WHERE     (Expr1 > CONVERT(DATETIME, '" + dateReportFilter.Value.Year.ToString("D4") + "-" + dateReportFilter.Value.Month.ToString("D2") + "-" + dateReportFilter.Value.Day.ToString("D2") + " 00:00:00', 102)) AND (Expr1 < CONVERT(DATETIME, '" + dateReportFilter.Value.Year.ToString("D4") + "-" + dateReportFilter.Value.Month.ToString("D2") + "-" + dateReportFilter.Value.Day.ToString("D2") + " 23:59:59', 102)) " +
                        "GROUP BY name " +
                        "ORDER BY name";
            using (SqlConnection cn = new SqlConnection())
            {
                try
                {
                    cn.ConnectionString = settings.Connection_string;
                    cn.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = cn;
                    cmd.CommandTimeout = 99999;
                    cmd.CommandText = q;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable t = new DataTable();
                    da.Fill(t);
                    foreach (DataRow rw in t.Rows)
                    {
                        s += double.Parse(rw[1].ToString());
                        r += "\"" + rw[0].ToString().Trim() + "\",\"" + rw[1].ToString().Replace(',', '.').Trim() + "\"\n";
                    }
                    r += "\"\",\"\"\n\"Итого\",\"" + s.ToString().Replace(",", ".") + "\"";
                    txtReportBody.Text = r;
                    System.IO.File.WriteAllText(rf1, r, Encoding.UTF8);
                    cn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

        }

    }
}
