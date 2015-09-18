using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface.Templates
{
	public partial class frmTMdiParent : Form
	{
	
		private string _title = "";
		//private PSA.Lib.Util.Settings settings = new PSA.Lib.Util.Settings();
		
		public frmTMdiParent()
		{
			InitializeComponent();
		}
		
		public string Title
		{
			get
			{
				return _title;
			}
			
			set
			{
				_title = value;
				this.Text = value + " - PSA v." + Application.ProductVersion.ToString();
			}
		}
		
		public string InfoString
		{
			get
			{
				return txtInfo.Text;
			}
			set
			{
				txtInfo.Text = value;
			}
		}

		private void tmrTime_Tick(object sender, EventArgs e)
		{
			txtTime.Text = DateTime.Now.ToLongTimeString();
		}

	}
}
