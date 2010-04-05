using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Photoland.Security;
using Photoland.Security.Discont;
using Photoland.Security.Client;
using Photoland.Security.User;
using Photoland.Forms.Interface;
using Photoland.Lib;
using Photoland.Components.FilterRow;

namespace Photoland.Acceptance.Wizard
{
	public partial class frmStep2a : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		public ClientInfo client;
		public DataTable tblOrder;
		public DiscontInfo discont;
		public decimal sum;
		public decimal AdvancedPayment;
		public decimal FinalPayment;

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();


        //////////////////////////////////////////////////////////////////////////
        private bool CheckState(SqlConnection c)
        {
            bool r = false;
            if (!TestCommand(c))
            {
                frmWaitConnection w = new frmWaitConnection();
                while (!TestCommand(c))
                {
                    Application.DoEvents();
                    w = new frmWaitConnection();
                    w.ShowDialog();
                    if (w.DialogResult == DialogResult.Cancel)
                    {
                        r = false;
                        break;
                    }
                }
                if (TestCommand(c))
                    r = true;
            }
            else
            {
                r = true;
            }
            return r;
        }

        private bool TestCommand(SqlConnection c)
        {
            bool r = false;
            try
            {
                if (c.State != ConnectionState.Open)
                    c.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = c;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select getdate()";
                DateTime _r = (DateTime)cmd.ExecuteScalar();
                r = true;
            }
            catch (Exception ex)
            {
                r = false;
            }
            return r;
        }
        //////////////////////////////////////////////////////////////////////////

        public frmStep2a()
		{
			InitializeComponent();
			this.Text = "Мастер приемки заказов: шаг 3";
		}

		private void frmStep2a_Load(object sender, EventArgs e)
		{
			lblUserInfo.Text = usr.Name + "\n" + usr.Post + "\n" + usr.Point;
			ReCalc();
		}

		private void ReCalc()
		{
            if (CheckState(db_connection))
            {
                decimal sum = 0;
                decimal end = 0;
                bool err = false;
                for (int i = 0; i < tblOrder.Rows.Count; i++)
                {
                    string tmp_id_good = tblOrder.Rows[i][4].ToString();
                    int tmp_count_good = int.Parse(tblOrder.Rows[i][2].ToString());
                    int tmp_id_client_category = int.Parse(client.Id_category.ToString());
                    //				SqlCommand db_command = new SqlCommand("SELECT [id_good], [id_category], [amount] FROM [vwPriceFull] WHERE [id_good] = " + tmp_id_good.ToString() + " AND [id_category] = " + tmp_id_client_category.ToString(), db_connection);
                    SqlCommand db_command = new SqlCommand("spAdvPrice", db_connection);
                    db_command.Parameters.Add(new SqlParameter("@id_good", tmp_id_good));
                    db_command.Parameters.Add(new SqlParameter("@id_category", tmp_id_client_category));
                    db_command.Parameters.Add(new SqlParameter("@threshold", tmp_count_good));
                    db_command.CommandType = CommandType.StoredProcedure;
                    //				SqlDataReader db_reader = db_command.ExecuteReader();
                    decimal price;
                    if (tmp_count_good > 0)
                    {
                        try
                        {
                            price = (decimal)db_command.ExecuteScalar();
                            {
                                if (price > 0)
                                {
                                    sum += tmp_count_good * price;
                                }
                                else
                                {
                                    MessageBox.Show("Внимание! Не найдена цена в прайсе!\nПроверьте заполение прайса!", "Оибка получаения цены",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    err = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            ErrorNfo.WriteErrorInfo(ex);
                            MessageBox.Show("Внимание! Не найдена цена в прайсе!\nПроверьте заполение прайса!", "Оибка получаения цены",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                            err = true;
                        }
                    }
                }
                this.sum = sum;
                lblOrderSum.Text = decimal.Round(sum, 2).ToString();
                if (this.discont != null)
                    if (this.discont.Discserv > 0)
                        sum -= sum * (this.discont.Discserv / 100);
                end = sum - this.AdvancedPayment;
                end = end - FinalPayment;
                if (end < 0)
                    end = 0;
                lblFinalSum.Text = decimal.Round(prop.DoRound(end), 2).ToString();
                decimal tm = AdvancedPayment + FinalPayment;
                lblTotalPayment.Text = tm.ToString();
                if (sum == 0)
                    err = true;
                if (err)
                {
                    btnNext.Enabled = false;
                }
                else
                {
                    btnNext.Enabled = true;
                }
            }
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Retry;
		}

		private void btnNext_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Отменить заказ?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				this.DialogResult = DialogResult.Cancel;
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			ReCalc();
		}

		private void btnFirstPay_Click(object sender, EventArgs e)
		{
			frmAdvancePayment fPayment = new frmAdvancePayment(double.Parse(lblOrderSum.Text));
			fPayment.ShowDialog();
			this.AdvancedPayment = (decimal)fPayment.Payment;
			lblFirstPay.Text = fPayment.Payment.ToString();
			ReCalc();
		}

		private void btnDiscont_Click(object sender, EventArgs e)
		{
            if (CheckState(db_connection))
            {
                frmGetDiscont fGetDiscont = new frmGetDiscont();
                fGetDiscont.db_connection = db_connection;
                fGetDiscont.ShowDialog();
                if (fGetDiscont.DialogResult == DialogResult.OK)
                {
                    if (fGetDiscont.discont.Code_dcard != "")
                    {
                        lblOrderDiscont.Text = fGetDiscont.discont.Discserv.ToString() + "%";
                        this.discont = fGetDiscont.discont;
                    }
                    else
                    {
                        MessageBox.Show("Дисконтная карта не найдена в базе!", "Скидка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        lblOrderDiscont.Text = "0%";
                        this.discont = null;
                    }
                }
                ReCalc();
            }
		}

		private void btnFirstPayClear_Click(object sender, EventArgs e)
		{
			this.AdvancedPayment = 0;
			lblFirstPay.Text = "0";
			ReCalc();
		}

		private void btnDiscontClear_Click(object sender, EventArgs e)
		{
			lblOrderDiscont.Text = "0%";
			this.discont = null;
			ReCalc();
		}

		private void btnFinalPayment_Click(object sender, EventArgs e)
		{
			this.FinalPayment = 0;
			ReCalc();

			frmFinalPayment fPayment = new frmFinalPayment(decimal.Parse(lblFinalSum.Text));
			fPayment.ShowDialog();
			this.FinalPayment = (decimal)fPayment.Payment;
			ReCalc();
		}

		private void btnFinalPaymentClear_Click(object sender, EventArgs e)
		{
			this.FinalPayment = 0;
			ReCalc();
		}
	}
}