using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using PSA.Lib.Util;
using System.Data.SqlClient;
using Xceed.Ftp;
using Xceed.FileSystem;
using System.IO;
using System.Threading;

namespace PSA.Lib.Interface
{
    public partial class frmCheckFtp : Form
    {
        Settings setting = new Settings();

        SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();

        public frmCheckFtp()
        {
            InitializeComponent();
        }

        private void frmCheckFtp_Load(object sender, EventArgs e)
        {
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            try
            {
                Xceed.Ftp.Licenser.LicenseKey = "FTN42-K40Z3-DXCGS-PYGA";

                data.Columns.Clear();
                data.Rows.Clear();
                data.Columns.Add("name", "Точка");
                data.Columns.Add("rezult", "Результат");
                data.Columns[0].Width = 200;
                data.Columns[1].Width = 350;

                cn = new SqlConnection(setting.Connection_string);
                cn.Open();

                cmd = new SqlCommand("SELECT * FROM [place];", cn);

                DataTable tbl = new DataTable();

                SqlDataAdapter da = new SqlDataAdapter(cmd);

                da.Fill(tbl);
                pb.Minimum = 0;
                pb.Maximum = tbl.Rows.Count;
                pb.Value = 0;
                foreach (DataRow r in tbl.Rows)
                {
                    try
                    {
                        using (FtpConnection connection = new FtpConnection(
                            r["server"].ToString().Trim(),
                            r["username"].ToString().Trim(),
                            r["password"].ToString().Trim()))
                        {
                            connection.Timeout = 10;
                            string tmp = Guid.NewGuid().ToString();
                            StreamWriter w = new StreamWriter(System.IO.Path.GetTempPath() + tmp);
                            w.Write(tmp);
                            w.Close();

                            connection.Encoding = Encoding.GetEncoding(1251);

                            DiskFile source = new DiskFile(System.IO.Path.GetTempPath() + tmp);
                            string ftp_to = r["path"].ToString().Trim();
                            if (ftp_to.Substring(0, 1) == "/") ftp_to = ftp_to.Substring(1);
                            FtpFolder destination = new FtpFolder(connection, ftp_to);

                            source.CopyTo(destination, true);

                            Thread.Sleep(2000);

                            FtpFile remote = new FtpFile(connection, ftp_to + tmp);

                            remote.Delete();
                        }
                        data.Rows.Add(new string[] { r["name"].ToString().Trim(), "ok" });
                    }
                    catch (Exception ex)
                    {
                        data.Rows.Add(new string[] { r["name"].ToString().Trim(), "ошибка: " + ex.Message });
                    }
                    finally
                    {
                        pb.Value++;
                        Application.DoEvents();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                btnStart.Enabled = true;
                MessageBox.Show("ok");
            }

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
