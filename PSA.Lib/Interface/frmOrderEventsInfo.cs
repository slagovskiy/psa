using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface
{
    public partial class frmOrderEventsInfo : PSA.Lib.Interface.Templates.frmTDialog
    {
        public string info = "";

        public frmOrderEventsInfo()
        {
            InitializeComponent();
            this.Title = "Дополнительная информация";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmOrderEventsInfo_Load(object sender, EventArgs e)
        {
            string[] seporator1 = new string[] { "$$" };
            if(info != "")
            {
                txtInfo.Text = info.Split(seporator1, StringSplitOptions.None)[0];
                DataTable t = new DataTable("t");
                t.Columns.Add("Новая", Type.GetType("System.Boolean"));
                t.Columns.Add("Услуга", Type.GetType("System.String"));
                t.Columns.Add("Заказано", Type.GetType("System.Decimal"));
                t.Columns.Add("Факт", Type.GetType("System.Decimal"));
                t.Columns.Add("Цена", Type.GetType("System.Decimal"));
                t.Columns.Add("Сумма", Type.GetType("System.Decimal"));
                t.Columns.Add("Сумма Факт", Type.GetType("System.Decimal"));
                t.Columns.Add("Добавил", Type.GetType("System.String"));
                t.Columns.Add("Выполнил", Type.GetType("System.String"));
                t.Columns.Add("Когда", Type.GetType("System.String"));
                t.Columns.Add("Списано", Type.GetType("System.String"));
                t.Columns.Add("Причина", Type.GetType("System.String"));
                if(info.Split(seporator1, StringSplitOptions.None).Length > 1)
                {
                    if (info.Split(seporator1, StringSplitOptions.None).Length > 2)
                        txtAdvPay.Text = "Предоплата: " + info.Split(seporator1, StringSplitOptions.None)[2];
                    if (info.Split(seporator1, StringSplitOptions.None).Length > 3)
                        txtFinalPay.Text = "Оплата: " + info.Split(seporator1, StringSplitOptions.None)[3];
                    if (info.Split(seporator1, StringSplitOptions.None).Length >= 4)
                        txtBonus.Text = "Бонусы: " + info.Split(seporator1, StringSplitOptions.None)[4];
                    string tbl = info.Split(seporator1, StringSplitOptions.None)[1];
                    for(int i=0; i < (tbl.Split('#')).Length; i++)
                    {
                        string row = tbl.Split('#')[i];
                        if(row.Split('|').Length == 12)
                        {
                            string[] _r = row.Split('|');
                            _r[0] = (!(bool.Parse(_r[0].ToString()))).ToString();
                            switch(_r[11].ToString())
                            {
                                case "1":
                                    {
                                        _r[11] = "Брак";
                                        break;
                                    }
                                case "2":
                                    {
                                        _r[11] = "Тех. Брак";
                                        break;
                                    }
                                case "3":
                                    {
                                        _r[11] = "Setup";
                                        break;
                                    }
                                case "4":
                                    {
                                        _r[11] = "Обрезка";
                                        break;
                                    }
                                case "5":
                                    {
                                        _r[11] = "Index print";
                                        break;
                                    }
                                case "6":
                                    {
                                        _r[11] = "Металик";
                                        break;
                                    }
                                case "7":
                                    {
                                        _r[11] = "Отмена (корректировка)";
                                        break;
                                    }
                                case "8":
                                    {
                                        _r[11] = "S-Print";
                                        break;
                                    }
                                case "9":
                                    {
                                        _r[11] = "Не стандартная обрезка";
                                        break;
                                    }
                                case "10":
                                    {
                                        _r[11] = "Возврат";
                                        break;
                                    }
                                default :
                                    {
                                        _r[11] = "";
                                        break;
                                    }
                            }

                            t.Rows.Add(_r);
                        }
                    }
                    data.DataSource = t;
                }

            }
            else
            {
                this.Close();
            }
        }
    }
}
