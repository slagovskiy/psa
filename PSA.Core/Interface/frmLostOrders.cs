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
    public partial class frmLostOrders : PSA.Lib.Interface.Templates.frmTDialog
    {
        private Settings prop = new Settings();
        private SqlConnection db_connection;
        public UserInfo usr;
		private FilterRowLike fRow;

        public frmLostOrders()
        {
            InitializeComponent();
            this.Title = "Утерянные заказы";
        }

        private void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT dbo.[order].id_order, dbo.[order].number, dbo.order_status.status_desc, dbo.[order].name_accept, dbo.[order].input_date FROM dbo.[order] INNER JOIN dbo.order_status ON dbo.[order].status = dbo.order_status.order_status WHERE (dbo.order_status.order_status = N'300000')", db_connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable("tbl");
            da.Fill(tbl);

            data.DataSource = tbl;

            data.Cols[1].Visible = false;
            data.Cols[2].Visible = true;
            data.Cols[3].Visible = true;
            data.Cols[4].Visible = true;
            data.Cols[5].Visible = true;

            data.Cols[2].Width = 150;
            data.Cols[2].Caption = "№ Заказа";

            data.Cols[3].Width = 150;
            data.Cols[3].Caption = "Статус";

            data.Cols[4].Width = 250;
            data.Cols[4].Caption = "Принял";

            data.Cols[5].Width = 150;
            data.Cols[5].Caption = "Принят";
        }

        private void frmLostOrders_Load(object sender, EventArgs e)
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
                    if (data.GetData(data.Row, 1) != null)
                    {
                        OpenOrder(data.GetData(data.Row, 1).ToString());
                    }
                }
            }
            catch
            {}
        }

        private void btnOpenOrder_Click(object sender, EventArgs e)
        {
            try
            {
            if (data.Row != -1)
            {
                if (data.GetData(data.Row, 1) != null)
                {
                    OpenOrder(data.GetData(data.Row, 1).ToString());
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
