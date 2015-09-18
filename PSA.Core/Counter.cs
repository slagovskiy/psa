using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Components.Counter
{
	public partial class Counter : UserControl
	{
		private int _count = 0;
		public int Count
		{
			get
			{
				return _count;
			}
			set
			{
				_count = value;
			}
		}

		private string _text = "";
		public string Txt
		{
			get
			{
				return _text;
			}
			set
			{
				_text = value;
			}
		}


		public Counter()
		{
			InitializeComponent();
			this._count = 0;
			this._text = "";

			UpdateLabel();
		}

		public Counter(int count)
		{
			InitializeComponent();
			this._count = count;
			this._text = "";

			UpdateLabel();
		}

		public Counter(int count, string txt)
		{
			InitializeComponent();
			this._count = count;
			this._text = txt;

			UpdateLabel();
		}

		private void Counter_Load(object sender, EventArgs e)
		{
			lbl.Text = _count.ToString();

			UpdateLabel();
		}

		private void UpdateLabel()
		{
			if (this._count < 0)
				this._count = 0;
			lbl.Text = this._count.ToString();
			text.Text = this._text;
		}


		private void lbl_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this._count++;
				UpdateLabel();
			}

			if (e.Button == MouseButtons.Right)
			{
				this._count--;
				UpdateLabel();
			}
            setColor();
        }

		private void Counter_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this._count++;
				UpdateLabel();
			}

			if (e.Button == MouseButtons.Right)
			{
				this._count--;
				UpdateLabel();
			}
            setColor();
        }

		private void Counter_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this._count++;
				UpdateLabel();
			}

			if (e.Button == MouseButtons.Right)
			{
				this._count--;
				UpdateLabel();
			}
            setColor();
        }

		private void lbl_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this._count++;
				UpdateLabel();
			}

			if (e.Button == MouseButtons.Right)
			{
				this._count--;
				UpdateLabel();
			}
		    setColor();
		}

        private void setColor()
        {
            if (this._count == 1)
                lbl.BackColor = Color.Yellow;
            if (this._count == 2)
                lbl.BackColor = Color.LightPink;
            if (this._count == 3)
                lbl.BackColor = Color.LightGreen;
            if (this._count == 4)
                lbl.BackColor = Color.LightBlue;
            if (this._count == 5)
                lbl.BackColor = Color.LightSeaGreen;
            if (this._count == 6)
                lbl.BackColor = Color.LightCoral;
            if (this._count == 7)
                lbl.BackColor = Color.LightCyan;
            if (this._count == 0)
                lbl.BackColor = Color.White;
            if (this._count < 0)
                lbl.BackColor = Color.Red;
            
        }
	}
}