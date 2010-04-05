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
    public partial class frmExportScan : PSA.Lib.Interface.Templates.frmTDialog
    {
        public List<string> numbers = new List<string>();
		public bool status = true;

		private Settings prop = new Settings();

        public frmExportScan()
        {
            InitializeComponent();
            this.Title = "Сканирование штрих-кодов";
            txtScan.Text = "";
            lstScan.Items.Clear();
        }

        private void frmExportScan_Load(object sender, EventArgs e)
        {
            CheckAccess();
        }

        private void CheckAccess()
        {
            if (lstScan.Items.Count == 0)
            {
                btnClear.Enabled = false;
                btnOk.Enabled = false;
                btnDeleteList.Enabled = false;
            }
            else
            {
                btnClear.Enabled = true;
                btnOk.Enabled = true;
                btnDeleteList.Enabled = true;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            frmDialogYesNo f = new frmDialogYesNo();
            f.Message = "Закрыть окно для добавления заказов?\nДанные сканирования будут утеряны.";
            f.Title = "Отмена сканирования";
            f.ShowDialog();
            if (f.DialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void frmInventoryScan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((status) && ((e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == '0') || (e.KeyChar == (char)Keys.Delete) || (e.KeyChar == (char)Keys.Back) || (e.KeyChar == (char)Keys.Return) || (e.KeyChar == (char)Keys.Escape))) || (!status))
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    try
                    {
                        if (((txtScan.Text.Length == 12) && (status)) || ((!status) && (txtScan.Text.Length > 0)))
                        {
                            bool add = true;
                            for (int i = 0; i < lstScan.Items.Count; i++)
                            {
                                if (lstScan.Items[i].ToString() == txtScan.Text)
                                {
                                    add = false;
                                    break;
                                }
                            }
                            if (add)
                            {
								if (status)
								{
									string status_name = "не удалось получить статус";
									try
									{
										using (SqlConnection con = new SqlConnection(prop.Connection_string))
										{
											con.Open();
											using (SqlCommand cmd = new SqlCommand())
											{
												cmd.Connection = con;
												cmd.CommandTimeout = 9000;
												cmd.CommandType = CommandType.StoredProcedure;
												cmd.CommandText = "spOrderGetStatusNameByNumber";
												SqlParameter p = new SqlParameter("@n", txtScan.Text);
												cmd.Parameters.Add(p);
												status_name = cmd.ExecuteScalar().ToString().Trim();
											}
										}
									}
									catch { }
									lstScan.Items.Insert(0, txtScan.Text + "; - " + status_name);
									CheckAccess();
								}
								else
								{
									lstScan.Items.Insert(0, txtScan.Text);
									CheckAccess();
								}
                            }
                            txtScan.Text = "";
                        }
                        e.Handled = true;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else if (e.KeyChar == (char)Keys.Escape)
                {
                    txtScan.Text = "";
                    e.Handled = true;
                }
                else
                {
                    if (((status) && (txtScan.Text.Length < 12)) || (!status))
                    {
                        txtScan.Text += e.KeyChar.ToString();
                        e.Handled = false;
                    }
                }
            }
            else
            {
                e.Handled = true;
            }
            label2.Focus();

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            frmDialogYesNo f = new frmDialogYesNo();
            f.Message = "Вы действительно хотите очистить список заказов?";
            f.Title = "Очистка списка";
            f.ShowDialog();
            if (f.DialogResult == DialogResult.Yes)
            {
                lstScan.Items.Clear();
            }
        }

        private void btnDeleteList_Click(object sender, EventArgs e)
        {
            if (lstScan.SelectedIndex >= 0)
            {
                frmDialogYesNo f = new frmDialogYesNo();
                f.Message = "Вы действительно хотите удалить выбранный номер заказа?";
                f.Title = "Удаление из списка";
                f.ShowDialog();
                if (f.DialogResult == DialogResult.Yes)
                {
                    lstScan.Items.RemoveAt(lstScan.SelectedIndex);
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            frmDialogYesNo f = new frmDialogYesNo();
            f.Message = "Завершить сканирование?";
            f.Title = "Завершение сканирования";
            f.ShowDialog();
            if (f.DialogResult == DialogResult.Yes)
            {
                for (int i = 0; i < lstScan.Items.Count; i++)
                {
                    numbers.Add(lstScan.Items[i].ToString().Split(';')[0]);
                }
                DialogResult = DialogResult.OK;
                this.Close();
            }

        }
    }
}
