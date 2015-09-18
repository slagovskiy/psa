using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.BLL;

namespace PSA.Lib.Interface
{
	public partial class frmKioskList : PSA.Lib.Interface.Templates.frmTDialog
	{
		public frmKioskList()
		{
			InitializeComponent();
			this.Title = "Фотокиоски";
		}

		private void KioskList_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void LoadData()
		{
			List<Kiosk> lst = Kiosk.getListAll();
			DataTable t = new DataTable("l");
			t.Columns.Add("id");
			t.Columns.Add("code");
			t.Columns.Add("name");
			t.Columns.Add("path");

			for (int i = 0; i < lst.Count; i++)
			{
				t.Rows.Add(new object[4] 
				{
					lst[i].Id,
					lst[i].Code,
					lst[i].Name.Trim(),
					lst[i].Path.Trim()
				});
			}

			docs.DataSource = t;

			docs.Cols[1].Visible = false;

			docs.Cols[2].Caption = "Код";
			docs.Cols[2].Width = 50;

			docs.Cols[3].Caption = "Описание";
			docs.Cols[3].Width = 260;

			docs.Cols[4].Caption = "Путь";
			docs.Cols[4].Width = 260;


		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			OpenItem(-1);
		}

		private void btnEdit_Click(object sender, EventArgs e)
		{
			if (docs.GetData(docs.Row, 1) != null)
			{
				OpenItem(int.Parse(docs.GetData(docs.Row, 1).ToString()));
			}
		}

		private void OpenItem(int item)
		{
			using (frmKioskItem f = new frmKioskItem())
			{
				f.id = item;
				f.ShowDialog();
			}
			LoadData();

		}

		private void docs_DoubleClick(object sender, EventArgs e)
		{
			if (docs.GetData(docs.Row, 1) != null)
			{
				OpenItem(int.Parse(docs.GetData(docs.Row, 1).ToString()));
			}
		}

	}

}
