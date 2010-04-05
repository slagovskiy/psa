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


namespace PSA.Tools
{
    public partial class frmMain : Form
    {
        Settings settings = new Settings();
        bool stop = false;

        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
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
	

    }
}
