using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Globalization;
using PSA.Lib.BLL;

namespace PSA.Lib.Interface
{
	public partial class frmVerificationDoc : PSA.Lib.Interface.Templates.frmTDialog
	{
		private PSA.Lib.Util.Settings settings = new PSA.Lib.Util.Settings();
        private bool closed = false;
        private DataTable orders = new DataTable();
		public int id = -1;
		private Verification inv;
		private bool globalauto = false;

        public Photoland.Security.User.UserInfo usr = new Photoland.Security.User.UserInfo();

        public frmVerificationDoc(Photoland.Security.User.UserInfo usr)
		{
			InitializeComponent();
			this.Title = "Документ сверки";
            this.usr = usr;
		}

		private void btnDoScan_Click(object sender, EventArgs e)
		{
            frmSelectStatus frm = new frmSelectStatus();
		    frm.ShowDialog();
            if (frm.DialogResult == System.Windows.Forms.DialogResult.OK)
            {

                frmInventoryScan f = new frmInventoryScan();
                f.status_title = "Сканирование заказов в статусе: " + frm.status_name;
                f.ShowDialog();
                List<string> n = f.numbers;
                pb1.Minimum = 0;
                pb1.Maximum = n.Count;
                pb1.Value = pb1.Minimum;
                for (int i = 0; i < n.Count; i++)
                {
                    bool add = true;
                    for (int j = 0; j < lstScan.Items.Count; j++)
                    {
                        if (n[i] == lstScan.Items[j].ToString())
                        {
                            add = false;
                            break;
                        }
                    }
                    if (add)
                    {
                        lstScan.Items.Add(n[i] + ":" + frm.status_code.Trim() + ":" + frm.status_name.Trim());
                    }
                    Application.DoEvents();
                    pb1.Value = i;
                }
                pb1.Value = pb1.Minimum;

                CheckStatus();
            }
		}

		private void btnLoadData_Click(object sender, EventArgs e)
		{
			try
			{
				lblLoad.Visible = true;
				Application.DoEvents();
				using(SqlConnection con = new SqlConnection(settings.Connection_string))
				{
                    //SELECT TOP (100) PERCENT number, found, status_desc, '' as status_desc_fact, input_date, expected_date, action, status, '' as status_fact, show, taction, usr, exported FROM (SELECT TOP (100) PERCENT RTRIM(dbo.[order].number) AS number, CAST('0' AS bit) AS found, dbo.order_status.status_desc, dbo.[order].input_date, dbo.[order].expected_date, '' AS action, dbo.[order].status, CAST('1' AS bit) AS show, '' AS taction, CAST('0' AS int) AS usr, CAST('0' AS bit) AS exported FROM dbo.[order] INNER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status WHERE (dbo.[order].status <> N'100000') AND (dbo.[order].status <> N'200000') AND (dbo.[order].status <> N'010000') AND ((dbo.[order].comment NOT LIKE '%export%') OR (dbo.[order].comment NOT LIKE '%Экспорт%')) ORDER BY number UNION SELECT TOP (100) PERCENT RTRIM(order_1.number) AS number, CAST('0' AS bit) AS found, order_status_1.status_desc, order_1.input_date, order_1.expected_date, '' AS action, order_1.status, CAST('1' AS bit) AS show, '' AS taction, CAST('0' AS int) AS usr, CAST('1' AS bit) AS exported FROM dbo.[order] AS order_1 INNER JOIN dbo.order_status AS order_status_1 ON order_1.status = order_status_1.order_status WHERE (order_1.status <> N'100000') AND (order_1.status <> N'200000') AND (order_1.status <> N'010000') AND ((order_1.comment LIKE '%export%') OR (order_1.comment LIKE '%Экспорт%')) ORDER BY number) AS derivedtbl_1 ORDER BY number
					using (SqlCommand cmd = new SqlCommand("SELECT TOP (100) PERCENT number, found, status_desc, '' AS status_desc_fact, input_date, expected_date, action, status, '' AS status_fact, show, taction, usr, SUM(CAST(exported AS int)) AS exported FROM (SELECT TOP (100) PERCENT RTRIM(dbo.[order].number) AS number, CAST('0' AS bit) AS found, dbo.order_status.status_desc, dbo.[order].input_date, dbo.[order].expected_date, '' AS action, dbo.[order].status, CAST('1' AS bit) AS show, '' AS taction, CAST('0' AS int) AS usr, CAST('0' AS bit) AS exported FROM dbo.[order] INNER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status WHERE (dbo.[order].status <> N'100000') AND (dbo.[order].status <> N'200000') AND (dbo.[order].status <> N'010000') AND  (dbo.[order].comment NOT LIKE '%export%' OR dbo.[order].comment NOT LIKE '%Экспорт%') ORDER BY number UNION SELECT     TOP (100) PERCENT RTRIM(order_1.number) AS number, CAST('0' AS bit) AS found, order_status_1.status_desc, order_1.input_date,  order_1.expected_date, '' AS action, order_1.status, CAST('1' AS bit) AS show, '' AS taction, CAST('0' AS int) AS usr, CAST('1' AS bit)  AS exported FROM dbo.[order] AS order_1 INNER JOIN dbo.order_status AS order_status_1 ON order_1.status = order_status_1.order_status WHERE (order_1.status <> N'100000') AND (order_1.status <> N'200000') AND (order_1.status <> N'010000') AND (order_1.comment LIKE '%export%' OR order_1.comment LIKE '%Экспорт%') ORDER BY number) AS derivedtbl_1 GROUP BY number, status_desc, input_date, expected_date, action, status, taction, usr, found, show ORDER BY number", con))
					{
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						orders.Rows.Clear();
                        da.Fill(orders);
                        
                        pb1.Minimum = 0;
                        pb1.Maximum = lstScan.Items.Count;
                        pb1.Value = pb1.Minimum;
                        for (int i = 0; i < lstScan.Items.Count; i++)
                        {
                            bool found = false;
                            string l1 = lstScan.Items[i].ToString().Split(':')[0];
                            string l2 = "";
                            if (lstScan.Items[i].ToString().Split(':').Length > 1)
                                l2 = lstScan.Items[i].ToString().Split(':')[1];
                            string l3 = "";
                            if (lstScan.Items[i].ToString().Split(':').Length > 2)
                                l3 = lstScan.Items[i].ToString().Split(':')[2];

                            for (int j = 0; j < orders.Rows.Count; j++)
                            {
                                // найден
                                if (orders.Rows[j]["number"].ToString().Trim() == l1)
                                {
                                    orders.Rows[j]["found"] = true;
                                    orders.Rows[j]["status_desc_fact"] = l3;
                                    orders.Rows[j]["status_fact"] = l2;
                                    found = true;
                                    break;
                                }
                            }
                            if (!found)
                            {
                                //orders.Rows.Add(new object[7] { lstScan.Items[i].ToString(), true, "???", null, null, "", "-1" });
                                //SELECT TOP (100) PERCENT number, found, status_desc, '' as status_desc_fact, input_date, expected_date, action, status, '' as status_fact, show, taction, usr, exported FROM (SELECT TOP (100) PERCENT RTRIM(dbo.[order].number) AS number, CAST('0' AS bit) AS found, dbo.order_status.status_desc, dbo.[order].input_date, dbo.[order].expected_date, '' AS action, dbo.[order].status, CAST('1' AS bit) AS show, '' AS taction, CAST('0' AS int) AS usr, CAST('0' AS bit) AS exported FROM dbo.[order] INNER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status WHERE ((dbo.[order].comment NOT LIKE '%export%') OR (dbo.[order].comment NOT LIKE '%Экспорт%')) ORDER BY number UNION SELECT TOP (100) PERCENT RTRIM(order_1.number) AS number, CAST('0' AS bit) AS found, order_status_1.status_desc, order_1.input_date, order_1.expected_date, '' AS action, order_1.status, CAST('1' AS bit) AS show, '' AS taction, CAST('0' AS int) AS usr, CAST('1' AS bit) AS exported FROM dbo.[order] AS order_1 INNER JOIN dbo.order_status AS order_status_1 ON order_1.status = order_status_1.order_status WHERE ((order_1.comment LIKE '%export%') OR (order_1.comment LIKE '%Экспорт%')) ORDER BY number) AS derivedtbl_1 WHERE (number = '" + l1 + "') ORDER BY number
                                using (SqlCommand cmd1 = new SqlCommand("SELECT TOP (100) PERCENT number, found, status_desc, '' AS status_desc_fact, input_date, expected_date, action, status, '' AS status_fact, show, taction, usr, SUM(CAST(exported AS int)) AS exported FROM (SELECT TOP (100) PERCENT RTRIM(dbo.[order].number) AS number, CAST('0' AS bit) AS found, dbo.order_status.status_desc, dbo.[order].input_date, dbo.[order].expected_date, '' AS action, dbo.[order].status, CAST('1' AS bit) AS show, '' AS taction, CAST('0' AS int) AS usr, CAST('0' AS bit) AS exported FROM dbo.[order] INNER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status WHERE (dbo.[order].comment NOT LIKE '%export%' OR dbo.[order].comment NOT LIKE '%Экспорт%') ORDER BY number UNION SELECT TOP (100) PERCENT RTRIM(order_1.number) AS number, CAST('0' AS bit) AS found, order_status_1.status_desc, order_1.input_date,  order_1.expected_date, '' AS action, order_1.status, CAST('1' AS bit) AS show, '' AS taction, CAST('0' AS int) AS usr, CAST('1' AS bit)  AS exported FROM dbo.[order] AS order_1 INNER JOIN dbo.order_status AS order_status_1 ON order_1.status = order_status_1.order_status WHERE (order_1.comment LIKE '%export%' OR order_1.comment LIKE '%Экспорт%') ORDER BY number) AS derivedtbl_1 GROUP BY number, status_desc, input_date, expected_date, action, status, taction, usr, found, show HAVING (number = '" + l1 + "') ORDER BY number", con))
                                {
                                    DataTable d1 = new DataTable();
                                    SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                                    da1.Fill(d1);
                                    if (d1.Rows.Count > 0)
                                    {
										orders.Rows.Add(new object[13] { l1, true, d1.Rows[0]["status_desc"].ToString(), l3, DateTime.Parse(d1.Rows[0]["input_date"].ToString()), DateTime.Parse(d1.Rows[0]["expected_date"].ToString()), "", d1.Rows[0]["status"].ToString(), l2, true, "", 0, false });
                                    }
                                    else
                                    {
										orders.Rows.Add(new object[13] { l1, true, "Не найден в базе!", l3, null, null, "", "-1", l2, true, "", 0, false });
                                    }
                                }
                            }
                            pb1.Value = i;
                            Application.DoEvents();
                        }
                        pb1.Value = pb1.Minimum;

                        updateTable();
					}
				}
				
			}
			catch(Exception ex)
			{
			}
			finally
			{
				lblLoad.Visible = false;
			}

            CheckStatus();
		}


        private void LoadStatus()
        {
            try
            {
 				using(SqlConnection con = new SqlConnection(settings.Connection_string))
				{
                    using (SqlCommand cmd = new SqlCommand("SELECT rtrim([order_status]) as [order_status], rtrim([status_desc]) as [status_desc] FROM [dbo].[order_status] ORDER BY [order_status]", con))
					{
						DataTable d = new DataTable();
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						da.Fill(d);

                        DataRow r = d.NewRow();
                        r["order_status"] = "-1";
                        r["status_desc"] = "Не найден в базе";

                        d.Rows.InsertAt(r, 0);

                        checkStatus.DataSource = d;
                        checkStatus.DisplayMember = "status_desc";
                        checkStatus.ValueMember = "order_status";

                        for (int i = 0; i < checkStatus.Items.Count; i++)
                        {
                            checkStatus.SetItemChecked(i, true);
                        }
                        
                        
					}
				}
			}
			catch(Exception ex)
			{
			}
			finally
			{
				lblLoad.Visible = false;
			}
       }

        private void CheckStatus()
        {
            if (closed)
            {
                lstScan.Enabled = false;
                btnDoScan.Enabled = false;
                btnDeleteSelected.Enabled = false;
                btnLoadData.Enabled = false;
                btnCloseInvent.Enabled = false;
				btnClose.Enabled = true;

                radioFound.Enabled = true;
                radioNotFound.Enabled = true;
				checkExport.Enabled = true;
                checkSovp.Enabled = true;
                checkSovpOk.Enabled = true;
                radioBase.Enabled = true;
                radioScan.Enabled = true;

                btnFilterDeSelectAll.Enabled = true;
                btnFilterSelectAll.Enabled = true;

                btnOpenOrder.Enabled = true;

                checkStatus.Enabled = true;
                btnApply.Enabled = true;
            }
            else
            {
                lstScan.Enabled = true;
                btnDoScan.Enabled = true;
                btnDeleteSelected.Enabled = true;
                btnLoadData.Enabled = true;
                btnCloseInvent.Enabled = true;
				btnClose.Enabled = false;

                radioFound.Enabled = false;
                radioNotFound.Enabled = false;
				checkExport.Enabled = false;
                checkSovp.Enabled = false;
                checkSovpOk.Enabled = false;
                radioBase.Enabled = false;
                radioScan.Enabled = false;

                btnOpenOrder.Enabled = false;

                btnFilterDeSelectAll.Enabled = false;
                btnFilterSelectAll.Enabled = false;

                checkStatus.Enabled = false;
                btnApply.Enabled = false;
            }
        }

       private void frmVerificationDoc_Load(object sender, EventArgs e)
        {
			if (id > -1)
			{
				inv = new Verification(id);
				closed = true;
				DataTable t = new DataTable("i");
				t.Columns.Add("no", Type.GetType("System.String"));
				t.Columns.Add("found", Type.GetType("System.Boolean"));
                t.Columns.Add("status_t", Type.GetType("System.String"));
                t.Columns.Add("status_fact_t", Type.GetType("System.String"));
                t.Columns.Add("in", Type.GetType("System.DateTime"));
				t.Columns.Add("out", Type.GetType("System.DateTime"));
				t.Columns.Add("action_t", Type.GetType("System.String"));
                t.Columns.Add("status", Type.GetType("System.String"));
                t.Columns.Add("status_fact", Type.GetType("System.String"));
				t.Columns.Add("show", Type.GetType("System.Boolean"));
				t.Columns.Add("action", Type.GetType("System.String"));
				t.Columns.Add("user", Type.GetType("System.Int32"));
				t.Columns.Add("exported", Type.GetType("System.Boolean"));

				for (int i = 0; i < inv.body.Count; i++)
				{
					if (inv.body[i].date_in.Year > 1901)
					{
						t.Rows.Add(new object[13] 
							{
								inv.body[i].number,
								inv.body[i].found,
								inv.body[i].status_t,
								inv.body[i].status_fact_t,
								inv.body[i].date_in,
								inv.body[i].date_out,
								inv.body[i].action,
								inv.body[i].status,
								inv.body[i].status_fact,
								true,
								inv.body[i].action_t,
								inv.body[i].usr.Id_user,
								inv.body[i].exported
							}
						);
					}
					else
					{
						t.Rows.Add(new object[13] 
							{
								inv.body[i].number,
								inv.body[i].found,
								inv.body[i].status_t,
								inv.body[i].status_fact_t,
								null,
								null,
								inv.body[i].action,
								inv.body[i].status,
								inv.body[i].status_fact,
								true,
								inv.body[i].action_t,
								inv.body[i].usr.Id_user,
								inv.body[i].exported
							}
						);
					}
				
				}
				orders = t;
				updateTable();

				this.Title = "Документ сверки № " + inv.id.ToString("D6");

			}
           
           LoadStatus();
		   if (id > -1)
			btnApply_Click(this, new EventArgs());
           CheckStatus();
        }

       private void btnCloseInvent_Click(object sender, EventArgs e)
       {
           using (SqlConnection con = new SqlConnection(settings.Connection_string))
           {
               con.Open();
               SqlCommand cmd = con.CreateCommand();
               SqlTransaction transaction = con.BeginTransaction("save");
               cmd.Connection = con;
               cmd.Transaction = transaction;
               int i;
               try
               {
                   lblSave.Visible = true;
                   int verification_id = 0;
                   cmd.CommandText = "INSERT INTO [dbo].[verification]" +
                                    "([verification_date]" +
                                    ",[verification_user]" +
									",[exported])" +
                                    "VALUES" +
                                    "(getdate()" +
                                    "," + usr.Id_user.ToString() + ", 0); SELECT scope_identity();";
                   verification_id = int.Parse(cmd.ExecuteScalar().ToString());

                   pb1.Minimum = 1;
                   pb1.Maximum = data.Rows.Count;
                   pb1.Value = pb1.Minimum;
                   for (i = 1; i < data.Rows.Count; i++)
                   {
                       int show;
                       if (bool.Parse(data.Rows[i][2].ToString()))
                           show = 1;
                       else
                           show = 0;
                       if (data.Rows[i][5].ToString() == "")
                       {
                           cmd.CommandText = "INSERT INTO [dbo].[verificationbody]" +
                                           "([id_verification]" +
                                           ",[order_number]" +
                                           ",[order_found]" +
                                           ",[order_status_t]" +
                                           ",[order_status]" +
                                           ",[order_status_fact_t]" +
                                           ",[order_status_fact]" +
                                           ",[order_action_t]" +
                                           ",[order_action]" +
                                           ",[order_user]" +
                                           ",[exported])" +
                                           "VALUES" +
                                           "(" + verification_id +
                                           ",'" + data.Rows[i][1].ToString() + "'" +
                                           "," + show +
                                           ",'" + data.Rows[i][3].ToString() + "'" +
                                           ",'" + data.Rows[i][8].ToString() + "'" +
                                           ",'" + data.Rows[i][4].ToString() + "'" +
                                           ",'" + data.Rows[i][9].ToString() + "'" +
                                           ",'" + data.Rows[i][7].ToString() + "'" +
                                           ",'" + data.Rows[i][11].ToString() + "'" +
                                           "," + usr.Id_user.ToString() + "" +
                                           "," + data.Rows[i][13].ToString() + ")";
                       }
                       else
                       {
                           cmd.CommandText = "INSERT INTO [dbo].[verificationbody]" +
                                            "([id_verification]" +
                                            ",[order_number]" +
                                            ",[order_found]" +
                                            ",[order_status_t]" +
                                            ",[order_status]" +
                                            ",[order_status_fact_t]" +
                                            ",[order_status_fact]" +
                                            ",[order_in]" +
                                            ",[order_out]" +
                                            ",[order_action_t]" +
                                            ",[order_action]" +
                                            ",[order_user] " +
                                            ",[exported] " +
                                            ")" +
                                            "VALUES" +
                                            "(" + verification_id +
                                            ",'" + data.Rows[i][1].ToString() + "'" +
                                            "," + show +
                                            ",'" + data.Rows[i][3].ToString().Trim() + "'" +
                                            ",'" + data.Rows[i][8].ToString().Trim() + "'" +
                                            ",'" + data.Rows[i][4].ToString().Trim() + "'" +
                                            ",'" + data.Rows[i][9].ToString().Trim() + "'" +
                                            ",'" + ((DateTime)data.Rows[i][5]).Year.ToString("D4") + "/" + ((DateTime)data.Rows[i][5]).Month.ToString("D2") + "/" + ((DateTime)data.Rows[i][5]).Day.ToString("D2") + " " + ((DateTime)data.Rows[i][5]).ToShortTimeString() + "'" +
                                            ",'" + ((DateTime)data.Rows[i][6]).Year.ToString("D4") + "/" + ((DateTime)data.Rows[i][6]).Month.ToString("D2") + "/" + ((DateTime)data.Rows[i][6]).Day.ToString("D2") + " " + ((DateTime)data.Rows[i][6]).ToShortTimeString() + "'" +
                                            ",'" + data.Rows[i][7].ToString() + "'" +
                                            ",'" + data.Rows[i][11].ToString() + "'" +
                                            "," + usr.Id_user.ToString() +
                                            "," + data.Rows[i][13].ToString() +
                                            ")";
                       }
                       cmd.ExecuteNonQuery();
                       pb1.Value = i;
                       Application.DoEvents();
                   }

                   transaction.Commit();
				   inv = new Verification(verification_id);
                   closed = true;
                   CheckStatus();
               }
               catch (Exception ex)
               {
                   transaction.Rollback();
                   MessageBox.Show("Произошла ошибка во время сохранения инвентаризации.\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка сохранения", MessageBoxButtons.OK, MessageBoxIcon.Warning);
               }
               finally
               {
                   lblSave.Visible = false;
               }
           }

       }

       private void frmVerificationDoc_FormClosing(object sender, FormClosingEventArgs e)
       {
		   if (!globalauto)
		   {
			   frmDialogYesNo f = new frmDialogYesNo();
			   f.Message = "Закрыть документ сверки?\nВсе не сохраненные данные будут утеряны!";
			   f.Title = "Закрытие документа";
			   f.ShowDialog();
			   if (f.DialogResult == DialogResult.Yes)
			   {
				   e.Cancel = false;
				   //PSA.Lib.Util.Semaphore.semVerification = false;
			   }
			   else
			   {
				   e.Cancel = true;
			   }
		   }
		   else
		   {
			   //PSA.Lib.Util.Semaphore.semVerification = false;
			   e.Cancel = false;
		   }
       }

       private void btnCancel_Click(object sender, EventArgs e)
       {
           this.Close();
       }

       private void btnApply_Click(object sender, EventArgs e)
       {
           lblLoad.Visible = true;
           pb1.Minimum = 0;
           pb1.Maximum = orders.Rows.Count;
           pb1.Value = pb1.Minimum;
           data.DataSource = new DataView();
           for (int i = 0; i < orders.Rows.Count; i++)
           {
               bool show = false;

               for (int j = 0; j < checkStatus.Items.Count; j++)
               {
                   if (((radioBase.Checked) && (orders.Rows[i][7].ToString().Trim() == ((System.Data.DataRowView)(checkStatus.Items[j])).Row.ItemArray[0].ToString())) ||
                   ((radioScan.Checked) && (orders.Rows[i][8].ToString().Trim() == ((System.Data.DataRowView)(checkStatus.Items[j])).Row.ItemArray[0].ToString())))
                   {
                       bool fnd = false;
					   if (!((radioFound.Checked) && (radioNotFound.Checked)))
                       {
                           if((radioFound.Checked) && (bool.Parse(orders.Rows[i][1].ToString()) == true))
                               fnd = true;
                           else if ((radioNotFound.Checked) && (bool.Parse(orders.Rows[i][1].ToString()) == false))
                               fnd = true;
                           else
                               fnd = false;
                       }
                       else
                       {
                           fnd = true;
                       }
                       if (fnd)
                       {
                           if (checkStatus.GetItemChecked(j))
                           {
                               show = true;
                           }
                           else
                           {
                               show = false;
                           }
                       }
                       break;
                   }
               }
			   if (show)
			   {
				   if (checkExport.Checked == ((orders.Rows[i][12].ToString() == "1") || (orders.Rows[i][12].ToString() == "True")))
					   show = true;
				   else
					   show = false;
			   }
               if (show)
               {
                   if (((checkSovpOk.Checked) && (orders.Rows[i][7].ToString().Trim() == orders.Rows[i][8].ToString().Trim())) ||
                       ((checkSovp.Checked) && (orders.Rows[i][7].ToString().Trim() != orders.Rows[i][8].ToString().Trim())))
                       show = true;
                   else
                       show = false;
               }
               orders.Rows[i][9] = show;
               pb1.Value = i;
               Application.DoEvents();

           }
           lblLoad.Visible = false;
           updateTable();
       }

       private void btnFilterSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           for (int i = 0; i < checkStatus.Items.Count; i++)
           {
               checkStatus.SetItemChecked(i, true);
           }
       }

       private void btnFilterDeSelectAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
       {
           for (int i = 0; i < checkStatus.Items.Count; i++)
           {
               checkStatus.SetItemChecked(i, false);
           }
       }

       private void btnOpenOrder_Click(object sender, EventArgs e)
       {
           if (data.GetData(data.Row, 1) != null)
           {
               OpenOrder(data.GetData(data.Row, 1).ToString());
           }

       }

       private void OpenOrder(string id)
       {
           using (SqlConnection con = new SqlConnection(settings.Connection_string))
           {
               using (SqlCommand cmd = new SqlCommand("SELECT dbo.[order].id_order FROM dbo.[order] WHERE (dbo.[order].number = '" + id + "')", con))
               {
                   int id_order = -1;
                   con.Open();
                   SqlDataReader r = cmd.ExecuteReader();
                   if (r.Read())
                   {
                       id_order = r.GetInt32(0);
                   }
                   r.Close();

                   if (id_order > -1)
                   {
                       using (Photoland.Forms.Interface.frmOrderClose f = new Photoland.Forms.Interface.frmOrderClose(con, usr, id_order))
                       {
                           f.ShowDialog();
                           /*if (f.DialogResult == DialogResult.OK)
                           {
							   if(inv.UpdateBodyRow(data.GetData(data.Row, 1).ToString(), f.TAction, f.Action, usr.Id_user))
							   {
	                               data.SetData(data.Row, 11, f.Action);
		                           data.SetData(data.Row, 7, f.TAction);
							   }
                           }*/
                       }
                   }
                   else
                   {
                       MessageBox.Show("Заказ не найден в базе!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   }

               }
           }
       }

	   private void data_DoubleClick(object sender, EventArgs e)
	   {
		   if (closed)
		   {
			   if (data.GetData(data.Row, 1) != null)
			   {
				   OpenOrder(data.GetData(data.Row, 1).ToString());
			   }
		   }
	   }

	   private void btnClose_Click(object sender, EventArgs e)
	   {
		   globalauto = true;
		   this.Close();
	   }

	   private void data_GridChanged(object sender, C1.Win.C1FlexGrid.GridChangedEventArgs e)
	   {
		   
	   }

	   private void data_AfterSort(object sender, C1.Win.C1FlexGrid.SortColEventArgs e)
	   {
		   data.Visible = false;
		   DoColor();
		   data.Visible = true;
	   }

       private void pb1_Click(object sender, EventArgs e)
       {

       }

       private void btnDeleteSelected_Click(object sender, EventArgs e)
       {
           if (lstScan.SelectedItem != null)
           {
               if (
                   MessageBox.Show("Удалить выбранный заказ из списка отсканированных?", "Удаление",
                                   MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                   System.Windows.Forms.DialogResult.Yes)
               {
                   lstScan.Items.Remove(lstScan.SelectedItem);
               }
           }
       }



    }
}
