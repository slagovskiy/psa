using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security;
using Photoland.Security.User;
using PSA.Lib;
using PSA.Lib.Util;
using System.Data.SqlClient;
using Photoland.Forms.Interface;
using System.IO;

namespace PSA.Lib.Interface
{
    public partial class frmDeleteOld : PSA.Lib.Interface.Templates.frmTDialog
    {
        private UserInfo _usr;
        public UserInfo usr
        {
            get { return _usr; }
            set { _usr = value; }
        }

        private Settings prop = new Settings();

        public frmDeleteOld()
        {
            InitializeComponent();
			this.Title = "Удаление заказов";
        }

        private void frmDeleteOld_Load(object sender, EventArgs e)
        {
            txtDateFiler.Value = DateTime.Now.AddMonths(-2);

			using (SqlConnection db_connection = new SqlConnection(prop.Connection_string))
			{
				db_connection.Open();
				using (SqlCommand cmd = new SqlCommand("SELECT rtrim([order_status]) as [order_status], rtrim([status_desc]) as [status_desc] FROM [dbo].[order_status]  WHERE rtrim([order_status]) IN ('100000', '200000', '300000', '400000') ORDER BY [order_status]", db_connection))
				{
					DataTable d = new DataTable();
					SqlDataAdapter da = new SqlDataAdapter(cmd);
					da.Fill(d);

					DataRow r = d.NewRow();
					r["order_status"] = "-1";
					r["status_desc"] = "Не менять статус";

					d.Rows.InsertAt(r, 0);

					txtStatus.DataSource = d;
					txtStatus.DisplayMember = "status_desc";
					txtStatus.ValueMember = "order_status";

				}
			}

			txtStatus.SelectedValue = "100000";

			LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection db_connection = new SqlConnection(prop.Connection_string))
                {
                    db_connection.Open();
                    SqlCommand cmd = new SqlCommand("SELECT CAST('0' AS bit) AS sel, dbo.[order].id_order, dbo.[order].number, dbo.[order].input_date, dbo.order_status.status_desc, dbo.[order].name_accept FROM dbo.[order] LEFT OUTER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status WHERE (dbo.order_status.order_status = N'" + txtStatus.SelectedValue + "') AND (dbo.[order].input_date < CONVERT(DATETIME, '" + txtDateFiler.Value.Year.ToString("D4") + "-" + txtDateFiler.Value.Month.ToString("D2") + "-" + txtDateFiler.Value.Day.ToString("D2") + " 00:00:00', 102))", db_connection);
                    DataTable tbl = new DataTable("tbl");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(tbl);
                    data.DataSource = tbl;

                    foreach (C1.Win.C1FlexGrid.Column col in data.Cols)
                    {
                        col.Visible = false;
                        col.AllowEditing = false;
                        col.AllowSorting = true;
                        col.AllowDragging = false;
                        col.AllowResizing = true;
                    }

                    data.Cols[1].Width = 50;
                    data.Cols[1].Caption = "#";
                    data.Cols[1].AllowEditing = true;
                    data.Cols[1].Visible = true;

                    data.Cols[3].Width = 150;
                    data.Cols[3].Caption = "Номер";
                    data.Cols[3].Visible = true;

                    data.Cols[4].Width = 150;
                    data.Cols[4].Caption = "Прием";
                    data.Cols[4].Visible = true;

                    data.Cols[5].Width = 200;
                    data.Cols[5].Caption = "Статус";
                    data.Cols[5].Visible = true;

                    data.Cols[6].Width = 200;
                    data.Cols[6].Caption = "Принял";
                    data.Cols[6].Visible = true;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: \n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OpenOrder(string id)
        {
            using (SqlConnection con = new SqlConnection(prop.Connection_string))
            {
				using (SqlCommand cmd = new SqlCommand("SELECT dbo.[order].id_order FROM dbo.[order] WHERE (dbo.[order].id_order = '" + id + "')", con))
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
                        using (frmOrderClose f = new frmOrderClose(con, usr, id_order))
                        {
                            f.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Заказ не найден в базе!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
            }
        }

        private void btnOpenOrder_Click(object sender, EventArgs e)
        {
            if (data.GetData(data.Row, 2) != null)
            {
                OpenOrder(data.GetData(data.Row, 2).ToString());
            }
        }

		private void data_DoubleClick(object sender, EventArgs e)
		{
			if (data.GetData(data.Row, 2) != null)
			{
				OpenOrder(data.GetData(data.Row, 2).ToString());
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for(int i=1;i<data.Rows.Count;i++)
			{
				data.Rows[i][1] = true;
			}
		}

		private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			for (int i = 1; i < data.Rows.Count; i++)
			{
				data.Rows[i][1] = false;
			}
		}

		private void DoClear()
		{
			btnOpenOrder.Enabled = false;
			btnClose.Enabled = false;
			btnDeleteSelected.Enabled = false;
			string ids = "0";
			for (int i = 1; i < data.Rows.Count;i++ )
			{
				if(bool.Parse(data.Rows[i][1].ToString()))
				{
					ids += ", " + data.Rows[i][2].ToString();
				}
			}
			try
			{
				SqlConnection db_connection = new SqlConnection(prop.Connection_string);
				db_connection.Open();
				SqlCommand c = new SqlCommand("SELECT [id_order], [input_date], [number] FROM [dbo].[order] WHERE (id_order IN (" + ids + "))", db_connection);
				SqlDataAdapter da = new SqlDataAdapter(c);
				DataTable t = new DataTable();
				da.Fill(t);
				StreamWriter f = new StreamWriter(prop.Dir_export + "\\clear_" + DateTime.Now.Year.ToString("D4") + "-" + DateTime.Now.Month.ToString("D2") + "-" + DateTime.Now.Day.ToString("D2") + "-" + DateTime.Now.Hour.ToString("D2") + "-" + DateTime.Now.Minute.ToString("D2") + "-" + DateTime.Now.Second.ToString("D2") + ".info", true, Encoding.GetEncoding(1251));
				pb.Minimum = 0;
				pb.Maximum = t.Rows.Count;
				pb.Value = 0;
				for (int i = 0; i < t.Rows.Count; i++)
				{
					Application.DoEvents();
					bool doit = true;
					f.WriteLine("! Найден старый заказ #" + t.Rows[i][2].ToString().Trim() + ", принятый " + t.Rows[i][1].ToString().Trim());
					lblAction.Text = "Найден старый заказ #" + t.Rows[i][2].ToString().Trim() + ", принятый " +
									 t.Rows[i][1].ToString().Trim();
					Application.DoEvents();
					if (doit)
					{
						try
						{
							DateTime dd = DateTime.Parse(t.Rows[i][1].ToString().Trim());
							if (Directory.Exists(prop.Dir_print + "\\" + dd.Year.ToString("D4") + "\\" + dd.Month.ToString("D2") + "\\" + dd.Day.ToString("D2") + "\\" + t.Rows[i][2].ToString().Trim()))
							{
								Directory.Delete(prop.Dir_print + "\\" + dd.Year.ToString("D4") + "\\" + dd.Month.ToString("D2") + "\\" + dd.Day.ToString("D2") + "\\" + t.Rows[i][2].ToString().Trim(), true);
								f.WriteLine("  + Удалена папка с файлами на печать");
								lblAction.Text = "Удалена папка с файлами на печать";
								Application.DoEvents();
							}
							else
							{
								f.WriteLine("  ? Не найдена папка с файлами на печать");
								lblAction.Text = "Не найдена папка с файлами на печать";
								Application.DoEvents();
							}
							if (Directory.Exists(prop.Dir_edit + "\\" + dd.Year.ToString("D4") + "\\" + dd.Month.ToString("D2") + "\\" + dd.Day.ToString("D2") + "\\" + t.Rows[i][2].ToString().Trim()))
							{
								Directory.Delete(prop.Dir_edit + "\\" + dd.Year.ToString("D4") + "\\" + dd.Month.ToString("D2") + "\\" + dd.Day.ToString("D2") + "\\" + t.Rows[i][2].ToString().Trim(), true);
								f.WriteLine("  + Удалена папка с файлами на обработку");
								lblAction.Text = "Удалена папка с файлами на обработку";
								Application.DoEvents();
							}
							else
							{
								f.WriteLine("  ? Не найдена папка с файлами на обработку");
								lblAction.Text = "Не найдена папка с файлами на обработку";
								Application.DoEvents();
							}
						}
						catch(Exception ex)
						{
							f.WriteLine("  - Произошла ошибка при удалении файлов заказа " + ex.Message);
							lblAction.Text = "Произошла ошибка при удалении файлов заказа " + ex.Message;
							Application.DoEvents();
							doit = true;
						}
						if (doit)
						{
							try
							{
								c = new SqlCommand("DELETE FROM [dbo].[orderbody] WHERE [id_order] = " + t.Rows[i][0].ToString().Trim(), db_connection);
								c.ExecuteNonQuery();
								f.WriteLine("  + Удалены строчки этого заказа");
								lblAction.Text = "Удалены строчки этого заказа";
								Application.DoEvents();
							}
							catch
							{
								f.WriteLine("  - Невозможно удалить строчки этого заказа, заказ удаляться не будет");
								lblAction.Text = "Невозможно удалить строчки этого заказа, заказ удаляться не будет";
								Application.DoEvents();
								doit = false;
							}
							try
							{
								c = new SqlCommand("DELETE FROM [dbo].[orderevent] WHERE [id_order] = " + t.Rows[i][0].ToString().Trim(), db_connection);
								c.ExecuteNonQuery();
								f.WriteLine("  + Удалены события этого заказа");
								lblAction.Text = "Удалены события этого заказа";
								Application.DoEvents();
							}
							catch
							{
								f.WriteLine("  - Невозможно удалить события этого заказа, заказ удаляться не будет");
								lblAction.Text = "Невозможно удалить события этого заказа, заказ удаляться не будет";
								Application.DoEvents();
								doit = false;
							}
							if (doit)
							{
								try
								{
									c = new SqlCommand("DELETE FROM [dbo].[order] WHERE [id_order] = " + t.Rows[i][0].ToString().Trim(), db_connection);
									c.ExecuteNonQuery();
									f.WriteLine("  + Удален сам заказ");
									lblAction.Text = "Удален сам заказ";
									Application.DoEvents();

								}
								catch
								{
									f.WriteLine("  - Невозможно удалить заказ");
									lblAction.Text = "Невозможно удалить заказ";
									Application.DoEvents();
									doit = false;
								}
							}
						}
					}
					try
					{
						pb.Value = i;
					}
					catch
					{
					}
					Application.DoEvents();
				}
				pb.Value = 0;
				f.Close();
			}
			catch
			{ }
			finally
			{
				LoadData();
				lblAction.Text = "";
				btnOpenOrder.Enabled = true;
				btnClose.Enabled = true;
				btnDeleteSelected.Enabled = true;
			}
		}

		private void data_Click(object sender, EventArgs e)
		{
			if (data.Row > 0)
			{
				if (data.GetData(data.Row, data.Selection.LeftCol) != null)
				{
					if(data.Selection.LeftCol == 1)
					{
						data.Rows[data.Row][1] = !bool.Parse(data.Rows[data.Row][1].ToString());
					}
				}
			}
		}

		private void btnDeleteSelected_Click(object sender, EventArgs e)
		{
			if(MessageBox.Show("Удалить выбранные заказы?", "Удаление", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
			{
				DoClear();
			}
		}


    }
}
