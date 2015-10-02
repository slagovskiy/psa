using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.Util;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace PSA.Core.Admin
{
    public partial class frmDeleteFromDate : Form
    {
        Settings prop = new Settings();
        public SqlConnection db_connection;

        public frmDeleteFromDate()
        {
            InitializeComponent();
        }

        private void ok()
        {
            if ((chekOk.Checked) && ((DateTime.Now - cal.SelectionStart).Days > 60))
                btnDelete.Enabled = true;
            else
                btnDelete.Enabled = false;
        }

        private void frmDeleteFromDate_Load(object sender, EventArgs e)
        {
            ok();
        }

        private void chekOk_CheckedChanged(object sender, EventArgs e)
        {
            ok();
        }

        private void cal_DateChanged(object sender, DateRangeEventArgs e)
        {
            ok();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить заказы до " + cal.SelectionStart.ToLongDateString(), "", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    btnCancel.Enabled = false;
                    btnDelete.Enabled = false;
                    cal.Enabled = false;
                    chekOk.Enabled = false;

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandTimeout = 9000;
                    cmd.Connection = db_connection;
                    cmd.CommandText = "DECLARE @DATEFILTER AS datetime;\n";
                    cmd.CommandText += "SET @DATEFILTER = '" + cal.SelectionStart.Year.ToString("D4") + "-" + cal.SelectionStart.Month.ToString("D2") + "-" + cal.SelectionStart.Day.ToString("D2") + " 00:00:00';\n";
                    cmd.CommandText += "DECLARE @ORDERS AS table (id_order int);\n";
                    cmd.CommandText += "DECLARE @ORDERBODY AS table (id_orderbody int);\n";
                    cmd.CommandText += "DECLARE @ORDEREVENT AS table (id_orderevent int);\n";
                    cmd.CommandText += "DECLARE @PAYMENT AS table (id_payment int);\n";

                    cmd.CommandText += "INSERT INTO @ORDERS (id_order) (SELECT id_order FROM [order] WHERE (input_date < CONVERT(DATETIME, @DATEFILTER, 102)));\n";
                    cmd.CommandText += "INSERT INTO @ORDERS (id_order) VALUES (0);\n";
                    cmd.CommandText += "INSERT INTO @ORDERBODY (id_orderbody) (SELECT id_orderbody FROM [orderbody] WHERE (id_order IN (SELECT id_order FROM @ORDERS)));\n";
                    cmd.CommandText += "INSERT INTO @ORDERBODY (id_orderbody) VALUES (0);\n";
                    cmd.CommandText += "INSERT INTO @ORDEREVENT (id_orderevent) (SELECT id_orderevent FROM [orderevent] WHERE (id_order IN (SELECT id_order FROM @ORDERS)));\n";
                    cmd.CommandText += "INSERT INTO @ORDEREVENT (id_orderevent) VALUES (0);\n";
                    cmd.CommandText += "INSERT INTO @PAYMENT (id_payment) (SELECT id_payment FROM [payments] WHERE ([number] IN (SELECT [number] FROM [order] WHERE (id_order IN (SELECT id_order FROM @ORDERS)))));\n";
                    cmd.CommandText += "INSERT INTO @PAYMENT (id_payment) VALUES (0);\n";

                    cmd.CommandText += "DELETE FROM [order] WHERE (id_order IN (SELECT id_order FROM @ORDERS));\n";
                    cmd.CommandText += "DELETE FROM [orderbody] WHERE (id_orderbody IN (SELECT id_orderbody FROM @ORDERBODY));\n";
                    cmd.CommandText += "DELETE FROM [orderevent] WHERE (id_orderevent IN (SELECT id_orderevent FROM @ORDEREVENT));\n";
                    cmd.CommandText += "DELETE FROM [payments] WHERE (id_payment IN (SELECT id_payment FROM @PAYMENT));\n";
                    cmd.CommandText += "DELETE FROM client WHERE (id_client IN (SELECT id_client FROM client WHERE (id_client NOT IN (SELECT id_client FROM dbo.[order]))));\n";

                    cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                btnCancel.Enabled = true;
                btnDelete.Enabled = true;
                cal.Enabled = true;
                chekOk.Enabled = true;

                MessageBox.Show("Удаление завершено, рекомендуется перестроить индексы.");

            }
        }
    }
}
