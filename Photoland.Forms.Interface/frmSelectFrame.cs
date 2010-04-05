using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Forms.Interface
{
	public partial class frmSelectFrame : Form
	{
		private List<string> _content = new List<string>();
		public List<string> Content
		{
			get { return _content; }
		}

		private decimal _content_count = 0;
		public decimal Content_count
		{
			get { return _content_count; }
		}

		
		public frmSelectFrame()
		{
			InitializeComponent();
			this.Text = "Выбор кадров на кленке";
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			_content_count = 0;
			_content.Clear();
			foreach (Control c in this.Controls)
			{
				if (c.Name.IndexOf("unter") > 0)
				{
					Photoland.Components.Counter.Counter cn;
					cn = (Photoland.Components.Counter.Counter)c;
					if (cn.Count > 0)
					{
						//s += cn.Txt + " " + cn.Count + "\n";
						_content_count += (decimal)cn.Count;
						_content.Add("Кадр: " + cn.Txt + " - " + cn.Count.ToString() + "шт.");
					}
				}
			}
			_content.Reverse();

			if (_content_count > 0)
				this.DialogResult = DialogResult.OK;
			else
				this.DialogResult = DialogResult.Cancel;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}
	}
}