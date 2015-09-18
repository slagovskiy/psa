using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.BLL;
using FirebirdSql.Data.FirebirdClient;
using System.Data.SqlClient;
using PSA.Lib.Util;

namespace PSA.Lib.Interface
{
	public partial class frmKioskItem : PSA.Lib.Interface.Templates.frmTDialog
	{
		private Settings prop = new Settings();

		public int id = -1;
		private Kiosk k;

		public frmKioskItem()
		{
			InitializeComponent();
			this.Title = "Фотокиоск";
		}

		private void KioskItem_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void LoadData()
		{
			
			if (id == -1)
			{
				k = new Kiosk();
				k.Id = -1;
				k.Code = 0;
				k.Name = "Описание";
				k.Path = @"192.168.XXX.XXX@c:\projects\Avantime-kiosk\bin\base\df.base";
				this.Title = "Новый фотокиоск";
			}
			else
			{
				k = Kiosk.getById(id);
				this.Title = "Фотокиоск № " + k.Code.ToString();
			}

			if (k != null)
			{
				txtId.Text = k.Id.ToString();
				txtCode.Text = k.Code.ToString("D3");
				txtName.Text = k.Name.Trim();
				txtPath.Text = k.Path.Trim();
				checkDeleted.Checked = k.Del;
			}
			else
			{
				MessageBox.Show("Невозможно получить объект!", "Ошибка загрузки объекта", MessageBoxButtons.OK, MessageBoxIcon.Error);
				this.Close();
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			k.Code = int.Parse(txtCode.Text.Trim());
			k.Name = txtName.Text.Trim();
			k.Path = txtPath.Text.Trim();
			k.Del = checkDeleted.Checked;


			if (k.Id == -1)
			{
				if (!k.Add())
				{
					MessageBox.Show("Ошибка сохранения (добавления нового)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					this.Close();
				}
			}

			if (!k.Delete())
			{
				MessageBox.Show("Ошибка сохранения (признак удаленности)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				if (!k.Save())
				{
					MessageBox.Show("Ошибка сохранения (сохранения объекта)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
				{
					this.Close();
				}
			}

		}

		private void btnSetPath_Click(object sender, EventArgs e)
		{
			dlg.ShowDialog();
			if (dlg.SelectedPath != "")
			{
				txtPath.Text = dlg.SelectedPath;
			}
		}

		private void btnCheck_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtPath.Text.Split('@').Length == 2)
				{
					FbConnectionStringBuilder cnString = new FbConnectionStringBuilder();
					cnString.DataSource = txtPath.Text.Split('@')[0];
					cnString.Database = txtPath.Text.Split('@')[1];
					cnString.UserID = "SYSDBA";
					cnString.Password = "masterkey";
					cnString.Charset = "win1251";
					cnString.Dialect = 3;
					using (FbConnection cn = new FbConnection(cnString.ToString()))
					{
						cn.Open();
						FbCommand cmd = new FbCommand("SELECT PRICE.* FROM PRICE", cn);
						cmd.ExecuteNonQuery();
					}
					MessageBox.Show("ok");
				}
				else
				{
					MessageBox.Show("Не верный формат строки!");
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
		}

		private void btnSync_Click(object sender, EventArgs e)
		{
			try
			{
				if (txtPath.Text.Split('@').Length == 2)
				{
					FbConnectionStringBuilder cnString = new FbConnectionStringBuilder();
					cnString.DataSource = txtPath.Text.Split('@')[0];
					cnString.Database = txtPath.Text.Split('@')[1];
					cnString.UserID = "SYSDBA";
					cnString.Password = "masterkey";
					cnString.Charset = "win1251";
					cnString.Dialect = 3;
					using (FbConnection cn = new FbConnection(cnString.ToString()))
					{
						cn.Open();
						FbCommand cmd = new FbCommand("SELECT SESSIONS.ID FROM SESSIONS", cn);
						FbDataAdapter da = new FbDataAdapter(cmd);
						DataTable tbl = new DataTable();
						da.Fill(tbl);
						string query = "";
						using (FbCommand cmd_o = new FbCommand())
						{
							cmd_o.Connection = cn;
							cmd_o.CommandTimeout = 9000;
							query = "CREATE TABLE SESSIONS_OK (ID INTEGER);";
							cmd_o.CommandText = query;
							try
							{
								cmd_o.ExecuteNonQuery();
							}
							catch { }
							query = "DELETE FROM SESSIONS_OK;\n";
							cmd_o.CommandText = query;
							cmd_o.ExecuteNonQuery();
							for (int i = 0; i < tbl.Rows.Count; i++)
							{
								query = "INSERT INTO SESSIONS_OK\n" +
												"\t(SESSIONS_OK.ID)\n" +
												"\tVALUES \n" +
												"\t(" + tbl.Rows[i]["ID"].ToString() + ");\n";
								cmd_o.CommandText = query;
								cmd_o.ExecuteNonQuery();
							}
						}
						MessageBox.Show("ok");
					}
				}
				else
				{
					MessageBox.Show("Не верный формат строки!");
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
		}

	}
}
