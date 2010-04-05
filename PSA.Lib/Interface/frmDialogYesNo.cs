using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PSA.Lib.Interface
{
	public partial class frmDialogYesNo : PSA.Lib.Interface.Templates.frmTDialog
	{
		
		private string _message = "";
		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
				txtMessage.Text = value;
			}
		}
	
		public frmDialogYesNo()
		{
			InitializeComponent();
			this.Title = "Выход из программы";
		}
	}
}
