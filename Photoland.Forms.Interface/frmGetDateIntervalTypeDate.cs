using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Interface
{
	public partial class frmGetDateIntervalTypeDate : Form
	{
		private DataTable t = new DataTable("type");
		private string y1, m1, d1, y2, m2, d2;

		public string Y1
		{
			get { return y1; }
		}

		public string M1
		{
			get { return m1; }
		}

		public string D1
		{
			get { return d1; }
		}

		public string Y2
		{
			get { return y2; }
		}

		public string M2
		{
			get { return m2; }
		}

		public string D2
		{
			get { return d2; }
		}

		public frmGetDateIntervalTypeDate()
		{
			InitializeComponent();
			this.Text = "Параметры";
		}

		private void frmGetDateIntervalTypeDate_Load(object sender, EventArgs e)
		{
			txtDateBegin.Value = DateTime.Now.AddDays(-1);
			txtDateEnd.Value = DateTime.Now;

			t.Columns.Add("id");
			t.Columns.Add("name");

			object [] r = new object[2];
			r[0] = 1;
			r[1] = "Дата приема заказа";
			t.Rows.Add(r);
			r[0] = 2;
			r[1] = "Дата выдачи заказа";
			t.Rows.Add(r);
			r[0] = 3;
			r[1] = "Дата ожидаемой выдачи заказа";
			t.Rows.Add(r);
			txtDateType.DataSource = t;
			txtDateType.DisplayMember = "name";
			txtDateType.ValueMember = "id";
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			if (txtDateBegin.Value < txtDateEnd.Value)
			{
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

				this.DialogResult = System.Windows.Forms.DialogResult.OK;
			}
			else
			{
				MessageBox.Show("Проверьте введеные даты!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		}


	}
}