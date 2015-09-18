using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PSA.Lib.Interface
{
    public partial class frmInventoryDoc
    {
        private void updateTable()
        {
            data.Visible = false;
            FillData();
            DoColor();
            data.Visible = true;

        }

        private void FillData()
        {
            DataView dv = new DataView(orders);
            dv.RowFilter = "show = 1";


            data.DataSource = dv;

            data.Cols[1].Caption = "№ заказа";
            data.Cols[1].Width = 100;

            data.Cols[2].Caption = "Найден";
            data.Cols[2].Width = 80;

            data.Cols[3].Caption = "Статус (база)";
            data.Cols[3].Width = 140;

            data.Cols[4].Caption = "Статус (скан)";
            data.Cols[4].Width = 140;

            data.Cols[5].Caption = "Принят";
            data.Cols[5].Width = 100;

            data.Cols[6].Caption = "Выдача";
            data.Cols[6].Width = 100;

            data.Cols[11].Caption = "Действие";
            data.Cols[11].Width = 200;

            //status
            data.Cols[7].Visible = false;
            //show
            data.Cols[8].Visible = false;
            //taction
            data.Cols[9].Visible = false;
			//user
			data.Cols[10].Visible = false;
			//exported
            //data.Cols[11].Visible = false;
            data.Cols[12].Visible = false;
            data.Cols[13].Visible = false;


        }

        private void DoColor()
        {/*
            pb1.Minimum = 1;
            pb1.Maximum = orders.Rows.Count;
            pb1.Value = pb1.Minimum;
            for (int i = 1; i < data.Rows.Count; i++)
            {
                if (bool.Parse(data.Rows[i][10].ToString()))
                {
                    // найден, не выдан
                    if ((bool.Parse(data.Rows[i][2].ToString())) && (data.Rows[i][9].ToString() == "-1"))
                    {
                        data.Rows[i].Style = data.Styles["red1"];
                    }
                    // найден, на выдаче
                    else if ((bool.Parse(data.Rows[i][2].ToString())) && (data.Rows[i][9].ToString().Trim() == "000000"))
                    {
                        data.Rows[i].Style = data.Styles["green"];
                    }
                    // не найден, на выдаче
                    else if ((!bool.Parse(data.Rows[i][2].ToString())) && (data.Rows[i][9].ToString().Trim() == "000000"))
                    {
                        data.Rows[i].Style = data.Styles["red3"];
                    }
                    // найден, выдан
                    else if ((bool.Parse(data.Rows[i][2].ToString())) && (data.Rows[i][9].ToString().Trim() == "100000"))
                    {
                        data.Rows[i].Style = data.Styles["red2"];
                    }
                    // найден, выдан
                    else if ((bool.Parse(data.Rows[i][2].ToString())) && (data.Rows[i][9].ToString().Trim() == "200000"))
                    {
                        data.Rows[i].Style = data.Styles["red2"];
                    }
                    // найден, утерян
                    else if ((bool.Parse(data.Rows[i][2].ToString())) && (data.Rows[i][9].ToString().Trim() == "300000"))
                    {
                        data.Rows[i].Style = data.Styles["red2"];
                    }
                    // не найден, на предпросмотре
                    else if ((!bool.Parse(data.Rows[i][2].ToString())) && (data.Rows[i][9].ToString().Trim() == "000010"))
                    {
                        data.Rows[i].Style = data.Styles["red3"];
                    }
                    // не найден, в очереди
                    else if ((!bool.Parse(data.Rows[i][2].ToString())) && (
                        (data.Rows[i][9].ToString().Trim() == "000100") ||
                        (data.Rows[i][9].ToString().Trim() == "000200")
                        ))
                    {
                        data.Rows[i].Style = data.Styles["blue"];
                    }
                    // не найден, в процессе
                    else if ((!bool.Parse(data.Rows[i][2].ToString())) && (
                        (data.Rows[i][9].ToString().Trim() == "000110") ||
                        (data.Rows[i][9].ToString().Trim() == "000210")
                        ))
                    {
                        data.Rows[i].Style = data.Styles["blue"];
                    }
                    // не найден
                    else if (!bool.Parse(data.Rows[i][2].ToString()))
                    {
                        data.Rows[i].Style = data.Styles["red1"];
                    }
                }
                else
                {
                    data.Rows[i].Visible = false;
                }
                pb1.Value = i;
                Application.DoEvents();
            }
            pb1.Value = pb1.Minimum;
          */
        }
    }
}
