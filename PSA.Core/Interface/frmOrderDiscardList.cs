using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.Util;
using System.Data.SqlClient;
using Photoland.Security.User;
using Photoland.Components.FilterRow;

namespace PSA.Lib.Interface
{
	public partial class frmOrderDiscardList : PSA.Lib.Interface.Templates.frmTDialog
	{
		private Settings prop = new Settings();
		private SqlConnection db_connection;
		public UserInfo usr;
		private FilterRowLike fRow;

		public frmOrderDiscardList()
		{
			InitializeComponent();
			this.Title = "Списание заказов";
		}

		private void LoadData()
		{
			SqlCommand cmd = new SqlCommand("SELECT CAST(0 as bit) as [selected], dbo.[order].id_order, dbo.[order].number, dbo.order_status.status_desc, dbo.[order].name_accept, dbo.[order].input_date FROM dbo.[order] INNER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status", db_connection);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			DataTable tbl = new DataTable("tbl");
			da.Fill(tbl);

			data.DataSource = tbl;

			data.Cols[1].Visible = true;
			data.Cols[2].Visible = false;
			data.Cols[3].Visible = true;
			data.Cols[4].Visible = true;
			data.Cols[5].Visible = true;
			data.Cols[6].Visible = true;

			data.Cols[1].Width = 30;
			data.Cols[1].Caption = "#";

			data.Cols[3].Width = 120;
			data.Cols[3].Caption = "№ Заказа";

			data.Cols[4].Width = 150;
			data.Cols[4].Caption = "Статус";

			data.Cols[5].Width = 250;
			data.Cols[5].Caption = "Принял";

			data.Cols[6].Width = 150;
			data.Cols[6].Caption = "Принят";
		}

		private void frmOrderDiscardList_Load(object sender, EventArgs e)
		{
			db_connection = new SqlConnection(prop.Connection_string);
			LoadData();
			if (fRow == null)
				fRow = new FilterRowLike(data);
		}

		private void OpenOrder(string id)
		{
			using (SqlConnection con = new SqlConnection(prop.Connection_string))
			{
				using (SqlCommand cmd = new SqlCommand("SELECT dbo.[order].id_order FROM dbo.[order] WHERE (dbo.[order].id_order = " + id + ")", con))
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
						using (Photoland.Forms.Interface.frmOrderCloseRO f = new Photoland.Forms.Interface.frmOrderCloseRO(con, usr, id_order))
						{
							f.btnChangeStatusAndBay.Visible = false;
							f.btnChangeStatus.Text = "Изменить статус";
							f.RO = false;
							f.onlyStatus("400000");
							f.ShowDialog();
							LoadData();
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
			try
			{
				if (data.Row != -1)
				{
					if (data.GetData(data.Row, 2) != null)
					{
						OpenOrder(data.GetData(data.Row, 2).ToString());
					}
				}
			}
			catch
			{ }
		}

		private void btnOpenOrder_Click(object sender, EventArgs e)
		{
			try
			{
				if (data.Row != -1)
				{
					if (data.GetData(data.Row, 2) != null)
					{
						OpenOrder(data.GetData(data.Row, 2).ToString());
					}
				}

			}
			catch
			{
			}
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
