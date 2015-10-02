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
using Photoland.Order;

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
                DisableInterface();
                RemoteQuery rm = new RemoteQuery();
                rm.usr = usr;
                if (rm.QueryData_p(txtOrder.Text, prop.AStatus))
                {
                    if (prop.PrintAfterImport)
                        PrintCheck(prop.OrderPixlPark + int.Parse(txtOrder.Text).ToString("D10"));
                    MessageBox.Show("Заказ импортрован!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void frmImportPixlPark_Load(object sender, EventArgs e)
        {
            if (prop.SelectImport)
            {
                this.Size = new Size(265, 278);
                btnImportO.Location = new Point(12, 89);
                btnImportO.Visible = true;
                btnImportD.Location = new Point(12, 125);
                btnImportD.Visible = true;
                btnImportA.Location = new Point(12, 161);
                btnImportA.Visible = true;
                btnCancel.Location = new Point(12, 198);
                btnCancel.Visible = true;

            }
            else
            {
                this.Size = new Size(265, 201);
                btnImportO.Location = new Point(12, 89);
                btnImportO.Visible = false;
                btnImportD.Location = new Point(12, 89);
                btnImportD.Visible = false;
                btnImportA.Location = new Point(12, 89);
                btnImportA.Visible = true;
                btnCancel.Location = new Point(12, 125);
                btnCancel.Visible = true;
            }
        }

        private void btnImportO_Click(object sender, EventArgs e)
        {
            try
            {
                DisableInterface();
                RemoteQuery rm = new RemoteQuery();
                rm.usr = usr;
                if (rm.QueryData_p(txtOrder.Text, prop.OStatus))
                {
                    if (prop.PrintAfterImport)
                        PrintCheck(prop.OrderPixlPark + int.Parse(txtOrder.Text).ToString("D10"));
                    MessageBox.Show("Заказ импортрован!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void btnImportD_Click(object sender, EventArgs e)
        {
            try
            {
                DisableInterface();
                RemoteQuery rm = new RemoteQuery();
                rm.usr = usr;
                if (rm.QueryData_p(txtOrder.Text, prop.DStatus))
                {
                    if (prop.PrintAfterImport)
                        PrintCheck(prop.OrderPixlPark + int.Parse(txtOrder.Text).ToString("D10"));
                    MessageBox.Show("Заказ импортрован!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }
        }

        private void PrintCheck(string order)
        {
            OrderInfo Order = new OrderInfo(db_connection, order, true);
            frmOrderClose frm = new frmOrderClose(db_connection, usr, Order.Id);
            frm.tmr.Stop();
            frm.PrintCheck();
        }

        private void DisableInterface()
        {
            txtOrder.Enabled = false;
            btnCancel.Enabled = false;
            btnImportA.Enabled = false;
            btnImportD.Enabled = false;
            btnImportO.Enabled = false;
            Application.DoEvents();
        }

        private void EnableInterface()
        {
            txtOrder.Enabled = false;
            btnCancel.Enabled = false;
            btnImportA.Enabled = false;
            btnImportD.Enabled = false;
            btnImportO.Enabled = false;
        }

    }
}
