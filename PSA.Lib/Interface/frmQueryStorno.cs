using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface
{
    public partial class frmQueryStorno : PSA.Lib.Interface.Templates.frmTDialog
    {
        public decimal infoBonus = 0;
        public decimal infoMoney = 0;
        public decimal infoSum = 0;
        public bool goods = false;
        public bool discont = false;
        public int type = 0;

        public frmQueryStorno()
        {
            InitializeComponent();
        }

        private void frmQueryStorno_Load(object sender, EventArgs e)
        {
            lblStornoInfo.Text = "Сумма к возврату:" + infoSum.ToString("N2");
            lblBonus.Text = "Получено " + infoBonus.ToString("N2") + " б.";
            lblMoney.Text = "Получено " + infoMoney.ToString("N2") + " р.";
            string info = "";
            if (infoBonus > 0)
            {
                if (discont)
                    info += "Бонусы получены по дисконтной карте\n";
                else
                    info += "Бонусы получены не по дисконтной карте\n";
                if(goods)
                {
                    info += "Возвращаются услуги, за которые принимались бонусы.";
                }
                else
                {
                    info += "Возвращаются услуги, за которые не принимались бонусы";
                }
            }
            if((goods) && (infoBonus > 0))
            {
                radioBonus.Checked = true;
                radioMoney.Checked = false;
            }
            else
            {
                radioBonus.Checked = false;
                radioMoney.Checked = true;
            }
            lblInfo.Text = info;

            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (radioBonus.Checked)
                type = 1;
            if (radioMoney.Checked)
                type = 2;
            if((type != 1) && (type != 2))
            {
                radioBonus.ForeColor = Color.Red;
                radioMoney.ForeColor = Color.Red;
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

    }
}
