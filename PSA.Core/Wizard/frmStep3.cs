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

namespace Photoland.Acceptance.Wizard
{
	public partial class frmStep3 : Form
	{
		public SqlConnection db_connection;
		public UserInfo usr;
		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
		
		public frmStep3()
		{
			InitializeComponent();
			this.Text = "Мастер приемки заказов: шаг 4";
		}

		private void frmStep3_Load(object sender, EventArgs e)
		{
			lblUserInfo.Text = usr.Name + "\n" + usr.Post + "\n" + usr.Point;
			cldrDate.VisualStyle = C1.Win.C1Schedule.UI.VisualStyle.Custom;
			gridTime.Rows.DefaultSize = 29;

			DateTime[] d = new DateTime[1];
			if (DateTime.Now.AddHours(prop.Time_for_output).Hour > prop.Time_end_work)
				d[0] = DateTime.Now.AddDays(1);
			else
				d[0] = DateTime.Now.AddHours(prop.Time_for_output);
			cldrDate.SelectedDates = d;
			double seltime = d[0].Hour;

			if ((d[0].Minute > 15) && (d[0].Minute < 30))
				seltime += 0.5;
			if (d[0].Minute > 30)
				seltime += 1;
			if (seltime < prop.Time_begin_work)
				seltime = prop.Time_begin_work;
			if (seltime > prop.Time_end_work)
				seltime = prop.Time_begin_work;

			double t = prop.Time_begin_work;
			for (int i = 0; i < 6; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					if (t <= prop.Time_end_work)
					{
						if(t.ToString().LastIndexOf(",5")>0)
						{
							gridTime.SetData(j, i, t.ToString().Replace(",5", "") + "-30");
						}
						else
						{
							gridTime.SetData(j, i, t.ToString() + "-00");
						}
						if (seltime == t)
							gridTime.Select(j, i);
					}
					t+=0.5;
				}
			}

			gridTime.SetData(4, 5, "!!!");


			RebildDate();
		}

		private void RebildDate()
		{
			if (cldrDate.SelectedDates.Length != 0)
			{
				cldrDate.Text = cldrDate.SelectedDates[0].ToShortDateString();
				string tmp_dow = "";
				if (cldrDate.SelectedDates[0].DayOfWeek == DayOfWeek.Monday) tmp_dow = "Понедельник";
				if (cldrDate.SelectedDates[0].DayOfWeek == DayOfWeek.Tuesday) tmp_dow = "Вторник";
				if (cldrDate.SelectedDates[0].DayOfWeek == DayOfWeek.Wednesday) tmp_dow = "Среда";
				if (cldrDate.SelectedDates[0].DayOfWeek == DayOfWeek.Thursday) tmp_dow = "Четверг";
				if (cldrDate.SelectedDates[0].DayOfWeek == DayOfWeek.Friday) tmp_dow = "Пятница";
				if (cldrDate.SelectedDates[0].DayOfWeek == DayOfWeek.Saturday) tmp_dow = "Суббота";
				if (cldrDate.SelectedDates[0].DayOfWeek == DayOfWeek.Sunday) tmp_dow = "Воскресенье";

				lblOrderDate.Text = cldrDate.Text + " " + tmp_dow + " " + gridTime.GetData(gridTime.Row, gridTime.Selection.LeftCol).ToString();
			}
			else
			{
				lblOrderDate.Text = cldrDate.Text + " " + gridTime.GetData(gridTime.Row, gridTime.Selection.LeftCol).ToString();
			}
		}

		private void gridTime_Click(object sender, EventArgs e)
		{
			RebildDate();
		}

		private void cldrDate_Click(object sender, EventArgs e)
		{
			RebildDate();
		}

		private void gridTime_KeyPress(object sender, KeyPressEventArgs e)
		{
			RebildDate();
		}

		private void btnBack_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Retry;
		}

		private void btnFinish_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Отменить заказ?", "Внимание!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
				this.DialogResult = DialogResult.Cancel;
		}


	}
}