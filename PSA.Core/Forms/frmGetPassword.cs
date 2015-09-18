using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Interface
{
	public partial class frmGetPassword : Form
	{
		public frmGetPassword()
		{
			InitializeComponent();
		}

		public string Password = "";

		private void btnOK_Click(object sender, EventArgs e)
		{
			Password = txtPassword.Text;
		}
	}
}