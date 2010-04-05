using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Photoland.Administrator
{
    public partial class frmReportSelectDateDefect : Form
    {
        public SqlConnection db_connection;

        public frmReportSelectDateDefect()
        {
            InitializeComponent();
        }

        private void frmReportSelectDateDefect_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT defect_code, defect_name FROM defect ORDER BY defect_code", db_connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable t = new DataTable("t");
            da.Fill(t);
            DataRow rw = t.NewRow();
            rw[0] = "-1";
            rw[1] = "< ВСЕ >";
            t.Rows.InsertAt(rw, 0);

            txtDefect.DataSource = t;
            txtDefect.DisplayMember = "defect_name";
            txtDefect.ValueMember = "defect_code";


        }
    }
}
