using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface.Templates
{
	public partial class frmTDialog : Form
	{
		public frmTDialog()
		{
			InitializeComponent();
		}
		
		public string Title
		{
			get
			{
				return lblTitle.Text;
			}
			
			set
			{
				lblTitle.Text = "   " + value;
				this.Text = value + " - PSA v." + Application.ProductVersion.ToString();
			}
		}
	}
}
