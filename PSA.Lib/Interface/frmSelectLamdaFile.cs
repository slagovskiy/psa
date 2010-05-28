using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.Util;
using System.IO;

namespace PSA.Lib.Interface
{
	public partial class frmSelectLamdaFile : PSA.Lib.Interface.Templates.frmTDialog
	{
		public string path = "";
		public string SelectedFile = "";
		private Settings settings = new Settings();

		public frmSelectLamdaFile()
		{
			InitializeComponent();
		}

		private void frmSelectLamdaFile_Load(object sender, EventArgs e)
		{
			this.Title = "Выбрать файл";
			LoadData();
		}

		private void LoadData()
		{
			try
			{
				if(Directory.Exists(path))
				{
					DirectoryInfo dr = new DirectoryInfo(path);
					DataTable tbl = new DataTable();
					tbl.Columns.Add("Path");
					tbl.Columns.Add("File");

					foreach (FileInfo f in dr.GetFiles())
					{
						bool get = false;
						foreach(string ext in settings.List_of_files.Split(','))
						{
							if("." + ext.ToLower().Trim() == f.Extension.ToLower())
							{
								get = true;
								break;
							}
						}
						if(get)
						{
							DataRow r = tbl.NewRow();
							r["Path"] = f.FullName;
							r["File"] = f.Name;
							tbl.Rows.Add(r);
						}
					}
					files.DataSource = tbl;
					files.DisplayMember = "File";
					files.ValueMember = "Path";
					if (checkAutoPreview.Checked)
					{
						if (File.Exists(files.SelectedValue.ToString()))
							pic.Image = System.Drawing.Image.FromFile(files.SelectedValue.ToString());
					}
				}
			}
			catch { }
		}

		private void files_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				if (checkAutoPreview.Checked)
				{
					if(File.Exists(files.SelectedValue.ToString()))
						pic.Image = System.Drawing.Image.FromFile(files.SelectedValue.ToString());
				}
			}
			catch { }
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			SelectedFile = files.Text;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

	}
}
