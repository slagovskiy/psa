using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security;
using Photoland.Lib;
using System.IO;

namespace Photoland.Forms.Interface
{
	public partial class frmApplyService : Form
	{
		private string OrderNo;
		private string Date = "";
		private Util.Settings prop = new Util.Settings();

		private List<string> _content = new List<string>();
		public List<string> Content
		{
			get { return _content; }
			set { _content = value; }
		}

		public frmApplyService(string OrderNo)
		{
			InitializeComponent();
			this.Text = "Применить услугу к ...";
			this.OrderNo = OrderNo;

			LoadFilesList();
		}

		public frmApplyService(string OrderNo, string Date)
		{
			InitializeComponent();
			this.Text = "Применить услугу к ...";
			this.OrderNo = OrderNo;
			this.Date = Date;

			LoadFilesList();
		}

		private void LoadFilesList()
		{
			if (Directory.Exists(prop.Dir_edit + "\\" + fso.GetDateSubFolders(Date) + "\\" + this.OrderNo.Trim()))
			{
				List<string> tmp;
				tmp = fso.GetListFilesForProc(prop.Dir_edit + "\\" + fso.GetDateSubFolders(Date) + "\\" + this.OrderNo.Trim(), prop.List_of_files, true, true);
				if (tmp.Count > 0)
				{
					for (int i = 0; i < tmp.Count; i++)
					{
						txtFiles.Items.Add(tmp[i]);
					}
				}
				else
				{
					MessageBox.Show("Не найдено файлов в папке для обработки", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
					btnApply.Enabled = false;
				}
			}
			else
			{
				MessageBox.Show("Не найдено файлов в папке для обработки", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Error);
				btnApply.Enabled = false;
			}
		}

		private void LoadImage(string file)
		{
			
			if (File.Exists(file))
				preview.Image = System.Drawing.Image.FromFile(file);
		}

		private void txtFiles_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Directory.Exists(prop.Dir_edit + "\\" + fso.GetDateSubFolders(Date) + "\\" + this.OrderNo.Trim()))
			{
				if (txtFiles.Text.IndexOf(" : ") > 0)
				{
					string[] s = txtFiles.Text.Split(new string[] { " : " }, StringSplitOptions.None);
					LoadImage(prop.Dir_edit + "\\" + fso.GetDateSubFolders(Date) + "\\" + this.OrderNo.Trim() + s[0]);
				}
				else
				{
					LoadImage(prop.Dir_edit + "\\" + fso.GetDateSubFolders(Date) + "\\" + this.OrderNo.Trim() + txtFiles.Text);
				}

			}
			
			if (txtFiles.Text.IndexOf(" : ") > 0)
			{
				string[] s = txtFiles.Text.Split(new string[] { " : " }, StringSplitOptions.None);
				txtComment.Text = s[1];
			}
			else
			{
				txtComment.Text = "";
			}
			
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			button1_Click(this, new EventArgs());

			foreach (object item in txtFiles.CheckedItems)
			{
				_content.Add(item.ToString());
			}

			if (_content.Count > 0)
				this.DialogResult = DialogResult.OK;
			else
				this.DialogResult = DialogResult.Cancel;
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (txtFiles.Items.Count > 0)
			{
				if (txtFiles.Text.IndexOf(" : ") > 0)
				{
					string[] s = txtFiles.Text.Split(new string[] {" : "}, StringSplitOptions.None);
					txtFiles.Items[txtFiles.SelectedIndex] = s[0] + " : " + txtComment.Text;
				}
				else
				{
					txtFiles.Items[txtFiles.SelectedIndex] += " : " + txtComment.Text;
				}
			}
		}

		private void frmApplyService_Load(object sender, EventArgs e)
		{

		}


	}
}