using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Interface
{
	public partial class frmAddCommentApplyService : Form
	{
		public frmAddCommentApplyService()
		{
			InitializeComponent();
			this.Text = "Добавить комментарий";
		}

		public frmAddCommentApplyService(string comm)
		{
			InitializeComponent();
			this.Text = "Добавить комментарий";
			txtComment.Text = comm;
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			this.Hide();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Hide();
		}
	}
}