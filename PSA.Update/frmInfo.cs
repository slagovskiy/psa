using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Update
{
	public partial class frmInfo : Form
	{
		private string _info = "";

		public frmInfo()
		{
			InitializeComponent();
		}

		public frmInfo(string info)
		{
			InitializeComponent();
			_info = info;
		}

		private void frmInfo_Load(object sender, EventArgs e)
		{
			info.Text = _info;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Close();
		}

	}
}
