using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface
{
	public partial class frmOrderRemoteInfo : PSA.Lib.Interface.Templates.frmTDialog
	{
		public DataTable t = new DataTable();
		public string info = "";

		public frmOrderRemoteInfo()
		{
			InitializeComponent();
			lblInfo.Text = info;
			data.DataSource = t;
			this.Title = "Информация о заказе";
		}

		private void btnCloase_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		internal void UpdateData()
		{
			lblInfo.Text = info;
			data.DataSource = t;
			//data.Columns[0].Width = 80;
			//data.Columns[1].Width = 120;
			//data.Columns[2].Width = 60;
			//data.Columns[3].Width = 60;
			//data.Columns[4].Width = 120;
			//data.Columns[5].Width = 80;
			//data.Columns[6].Width = 60;
			data.AllowUserToDeleteRows = false;
			data.AllowUserToAddRows = false;
			data.AllowUserToOrderColumns = false;
		}
	}
}
