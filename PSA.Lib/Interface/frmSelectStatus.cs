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
    public partial class frmSelectStatus : PSA.Lib.Interface.Templates.frmTDialog
    {
        public string status_code = "";
        public string status_name = "";

        public frmSelectStatus()
        {
            InitializeComponent();
            this.Title = "Выбор статуса перед сканированием";
        }

        private void frmSelectStatus_Load(object sender, EventArgs e)
        {
            Settings prop = new Settings();
            SqlConnection db_connection = new SqlConnection(prop.Connection_string);
            SqlCommand cmd = new SqlCommand("SELECT order_status, status_desc FROM order_status ORDER BY status_desc", db_connection);
            DataTable tbl = new DataTable("t");
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(tbl);

            DataRow rw = tbl.NewRow();
            rw[0] = "-1";
            rw[1] = "< НЕ ВЫБРАНО >";
            tbl.Rows.InsertAt(rw, 0);

            txtStatus.DataSource = tbl;
            txtStatus.DisplayMember = "status_desc";
            txtStatus.ValueMember = "order_status";

            CheckAccess();


        }

        private void CheckAccess()
        {
            if (txtStatus.SelectedValue.ToString() == "-1")
                btnOk.Enabled = false;
            else
                btnOk.Enabled = true;
        }

        private void txtStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckAccess();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            status_code = txtStatus.SelectedValue.ToString();
            status_name = txtStatus.Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
