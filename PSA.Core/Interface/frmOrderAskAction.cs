using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security.User;

namespace PSA.Lib.Interface
{
    public partial class frmOrderAskAction : PSA.Lib.Interface.Templates.frmTDialog
    {
        public PSA.Lib.Util.OrderActions DoAction;
		public UserInfo usr;

		public frmOrderAskAction(UserInfo user)
		{
			InitializeComponent();
			usr = user;
		}

		private void btnPrintCheck_Click(object sender, EventArgs e)
        {
            this.DoAction = PSA.Lib.Util.OrderActions.PrintCheck;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DoAction = PSA.Lib.Util.OrderActions.MoveEdit;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DoAction = PSA.Lib.Util.OrderActions.MovePrint;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DoAction = PSA.Lib.Util.OrderActions.MovePreview;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.DoAction = PSA.Lib.Util.OrderActions.MoveWaitPay;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DoAction = PSA.Lib.Util.OrderActions.MoveToAward;
            this.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.DoAction = PSA.Lib.Util.OrderActions.none;
            this.Close();
        }

		private void frmOrderAskAction_Load(object sender, EventArgs e)
		{
			if (usr.prmCanMovePayment)
			{
				btnMovePayment.Enabled = true;
				btnReLogin.Enabled = false;
			}
			else
			{
				btnMovePayment.Enabled = false;
				btnReLogin.Enabled = true;
			}
		}

		private void btnReLogin_Click(object sender, EventArgs e)
		{
			PSA.Lib.Interface.frmLogin fLogin = new PSA.Lib.Interface.frmLogin();
			bool tmp_login_ok = false;
			bool tmp_exit = false;
			while (!tmp_login_ok)
			{
				switch (fLogin.ShowDialog())
				{
					case DialogResult.Cancel:
						{
							tmp_login_ok = true;
							tmp_exit = true;
							break;
						}
					case DialogResult.OK:
						{
							tmp_login_ok = true;
							if (fLogin.usr.prmCanMovePayment)
							{
								usr = fLogin.usr;
								if (usr.prmCanMovePayment)
								{
									btnMovePayment.Enabled = true;
									btnReLogin.Enabled = false;
								}
								else
								{
									btnMovePayment.Enabled = false;
									btnReLogin.Enabled = true;
								}
							}
							else
							{
								tmp_exit = true;
								MessageBox.Show("У Вас нет прав на перемещение платежей.", "Контроль ", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
							break;
						}
				}
			}
		}

		private void button6_Click_1(object sender, EventArgs e)
		{
			this.DoAction = PSA.Lib.Util.OrderActions.none;
			this.Close();
		}

		private void btnMovePayment_Click(object sender, EventArgs e)
		{
			this.DoAction = PSA.Lib.Util.OrderActions.MovePayment;
			this.Close();
		}
    }
}
