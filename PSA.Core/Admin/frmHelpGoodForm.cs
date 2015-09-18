using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Admin
{
	public partial class frmHelpGoodForm : Form
	{
		private string f = "";

		public frmHelpGoodForm(string f)
		{
			InitializeComponent();

			this.f = f.Trim();

			this.Text = "Предпросмотр формы";
		}

		private void frmHelpGoodForm_Load(object sender, EventArgs e)
		{
			pictureBox1.Visible = false;
			pictureBox2.Visible = false;
			pictureBox3.Visible = false;

			switch (f)
			{
				case "":
					{
						pictureBox1.Visible = true;
						break;
					}
				case "00002":
					{
						pictureBox2.Visible = true;
						break;
					}
				case "00001":
					{
						pictureBox3.Visible = true;
						break;
					}
				case "00003":
					{
						pictureBox4.Visible = true;
						break;
					}
			}
		}
	}
}