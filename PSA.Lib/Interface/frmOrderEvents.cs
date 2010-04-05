using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.Util;
using System.Data.SqlClient;


namespace PSA.Lib.Interface
{
    public partial class frmOrderEvents : PSA.Lib.Interface.Templates.frmTDialog
    {
        private Settings prop = new Settings();
        private SqlConnection db_connection;
        public int id_order = 0;

        public frmOrderEvents(int id_order)
        {
            InitializeComponent();
            db_connection = new SqlConnection(prop.Connection_string);
            this.id_order = id_order;
            LoadData();
        }

        private void LoadData()
        {
            SqlCommand cmd = new SqlCommand("SELECT dbo.orderevent.event_date, dbo.orderevent.event_user, dbo.orderevent.event_point, dbo.order_status.status_desc,  dbo.orderevent.event_text FROM dbo.orderevent INNER JOIN dbo.order_status ON dbo.orderevent.event_status = dbo.order_status.order_status WHERE (dbo.orderevent.id_order = " + id_order + ") ORDER BY dbo.orderevent.event_date", db_connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tbl = new DataTable("tbl");
            da.Fill(tbl);

            data.DataSource = tbl;

            data.Cols[1].Caption = "Дата";
            data.Cols[2].Caption = "Пользователь";
            data.Cols[3].Caption = "Точка";
            data.Cols[4].Caption = "Статус";
            data.Cols[5].Caption = "Событие";

            data.Cols[1].Width = 120;
            data.Cols[2].Width = 200;
            data.Cols[3].Width = 100;
            data.Cols[4].Width = 150;
            data.Cols[5].Width = 400;

            data.Cols[1].Format = "dd/MM/yyyy HH:mm:ss";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void data_DoubleClick(object sender, EventArgs e)
        {
            OpenInfo();
        }

        private void OpenInfo()
        {
            if (data.Row != -1)
            {
                if (data.GetData(data.Row, 1) != null)
                {
                    frmOrderEventsInfo f = new frmOrderEventsInfo();
                    f.info = data.GetData(data.Row, 5).ToString();
                    f.ShowDialog();
                }
            }
        }
    }
}
