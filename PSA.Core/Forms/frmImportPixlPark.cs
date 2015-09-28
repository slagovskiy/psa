using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security.User;
using PSA.Lib.Util;
using Photoland.Forms.Interface;
using System.Data.SqlClient;
using PSA.Lib.Util;

namespace PSA.Core.Forms
{
    public partial class frmImportPixlPark : Form
    {
        public SqlConnection db_connection;
        public UserInfo usr;
        public Settings prop = new Settings();

        public frmImportPixlPark(SqlConnection db_connection, UserInfo usr)
        {
            InitializeComponent();
            this.db_connection = db_connection;
            this.usr = usr;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                RemoteQuery rm = new RemoteQuery();
                rm.usr = usr;
                if (rm.QueryData_p(txtOrder.Text))
                {
                    frmAcceptanceTable t = new frmAcceptanceTable(db_connection, usr);
                    t.OpenOrder(prop.OrderPixlPark + int.Parse(txtOrder.Text).ToString("D10"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
