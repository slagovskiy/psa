using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security;
using Photoland.Security.User;

namespace Photoland.Operator
{
    public partial class frmDiscardingReportPrep : Form
    {
        private UserInfo usr;
		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
        private SqlConnection db_connection;

        public frmDiscardingReportPrep(UserInfo usr, SqlConnection db_connection)
        {
            InitializeComponent();
            this.Text = "Параметры отчета по списаниям";
            this.usr = usr;
            this.db_connection = db_connection;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (prop.PathReportsTemplates != "")
                {
                    string y1, m1, d1, y2, m2, d2;
                    string usrfilter;
                    usrfilter = checkCurentUser.Checked ? "=" + usr.Id_user.ToString() : ">0";
                    y1 = txtDateBegin.Value.Year.ToString();
                    m1 = txtDateBegin.Value.Month < 10
                             ? "0" + txtDateBegin.Value.Month.ToString()
                             : txtDateBegin.Value.Month.ToString();
                    d1 = txtDateBegin.Value.Day < 10
                             ? "0" + txtDateBegin.Value.Day.ToString()
                             : txtDateBegin.Value.Day.ToString();
                    y2 = txtDateEnd.Value.Year.ToString();
                    m2 = txtDateEnd.Value.Month < 10
                             ? "0" + txtDateEnd.Value.Month.ToString()
                             : txtDateEnd.Value.Month.ToString();
                    d2 = txtDateEnd.Value.Day < 10
                             ? "0" + txtDateEnd.Value.Day.ToString()
                             : txtDateEnd.Value.Day.ToString();
                    SqlCommand db_command =
                        new SqlCommand(
                        "SELECT name, CASE WHEN paper = 1 THEN 'Глянцевая бумага' ELSE 'Матовая бумага' END AS paper, SUM(t1) AS t1, SUM(t2) AS t2, SUM(t3) AS t3, SUM(t4) AS t4, SUM(t5) AS t5 FROM (SELECT name, paper, CASE WHEN (typeaction = 1 AND quantity > 0) THEN quantity ELSE 0 END AS t1, CASE WHEN (typeaction = 2 AND quantity > 0) THEN quantity ELSE 0 END AS t2, CASE WHEN (typeaction = 3 AND quantity > 0) THEN quantity ELSE 0 END AS t3, CASE WHEN (typeaction = 4 AND quantity > 0) THEN quantity ELSE 0 END AS t4, CASE WHEN (typeaction = 5 AND quantity > 0) THEN quantity ELSE 0 END AS t5 FROM (SELECT name, SUM(quantity) AS quantity, typeaction, paper FROM vwTehActionFull WHERE (datea >= CONVERT(datetime, '" + y1 + "/" + m1 + "/" + d1 + " 00:00:00.000', 120)) AND (datea <= CONVERT(datetime, '" + y2 + "/" + m2 + "/" + d2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ") GROUP BY name, typeaction, paper) AS TMP) AS TMP2 GROUP BY name, paper",
                            db_connection);
                    SqlDataAdapter db_adapter = new SqlDataAdapter(db_command);
                    DataTable tbl = new DataTable("discarding");
                    db_adapter.Fill(tbl);
                    rep.Load(prop.PathReportsTemplates, "Discarding");
                    rep.DataSource.Recordset = tbl;
                    rep.Fields["titleDates"].Text = "с " + txtDateBegin.Value.ToShortDateString() + " по " +
                                                    txtDateEnd.Value.ToShortDateString();
                    if (checkCurentUser.Checked)
                        rep.Fields["titleDates"].Text += " для " + usr.Name;
                    else
                        rep.Fields["titleDates"].Text += " для всех пользователей";
                    PrintPreviewDialog pd = new PrintPreviewDialog();
                    pd.Document = rep.Document;
                    pd.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Не выбран файл шаблонов отчетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
				ErrorNfo.WriteErrorInfo(ex);
                MessageBox.Show("Ошибка вывода отчета\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}