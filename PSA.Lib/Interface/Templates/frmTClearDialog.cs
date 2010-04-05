using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface.Templates
{
	public partial class frmTClearDialog : Form
	{
	
		private string _title = "";
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				this.Text = value + " PSA - v." + Application.ProductVersion.ToString();
				_title = value;
			}
		}
			
		public frmTClearDialog()
		{
			InitializeComponent();
		}
	}
}
