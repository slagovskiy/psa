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
    public partial class frmSelectExportPlace : PSA.Lib.Interface.Templates.frmTDialogNoBaner
    {
        private Settings prop = new Settings();
        private SqlConnection db_connection = new SqlConnection();
        private SqlCommand db_command;
        private SqlDataAdapter db_adapter;
        private DataTable tbl = new DataTable("");

        public int place;

        public frmSelectExportPlace()
        {
            InitializeComponent();
        }

        private void frmSelectExportPlace_Load(object sender, EventArgs e)
        {
            try
            {
                db_connection.ConnectionString = prop.Connection_string;
                db_connection.Open();

                db_command = new SqlCommand("SELECT * FROM [place] WHERE [del] = 0;", db_connection);
                db_adapter = new SqlDataAdapter(db_command);
                db_adapter.Fill(tbl);
                txtSelectPlace.DataSource = tbl;
                txtSelectPlace.ValueMember = "id_place";
                txtSelectPlace.DisplayMember = "name";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Ошибка\n" + ex.Message);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            place = (int)txtSelectPlace.SelectedValue;
            DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
