using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface
{
	public partial class frmAbout : PSA.Lib.Interface.Templates.frmTClearDialog
	{
		public frmAbout()
		{
			InitializeComponent();
			this.Title = "О программе";
		}
	}
}
