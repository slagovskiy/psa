using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.Util;
using PSA.Lib.BLL;

namespace PSA.Lib.Interface
{
	public partial class frmInventoryList : PSA.Lib.Interface.Templates.frmTDialog
	{
		Settings settings = new Settings();
		public Photoland.Security.User.UserInfo usr = new Photoland.Security.User.UserInfo();

		public frmInventoryList()
		{
			InitializeComponent();
			this.Title = "Журнал инвентаризаций";
		}

		private void frmInventoryList_Load(object sender, EventArgs e)
		{
			LoadData();
		}

		private void LoadData()
		{
			List<Inventory> lst = Inventory.getList();
			DataTable t = new DataTable("l");
			t.Columns.Add("id");
			t.Columns.Add("number");
			t.Columns.Add("doc");
			t.Columns.Add("date");
			t.Columns.Add("author");

			for (int i = 0; i < lst.Count; i++)
			{
				t.Rows.Add(new object[5] 
				{
					lst[i].id, 
					lst[i].id.ToString("D6"), 
					"Документ инвентаризации", 
					lst[i].date, 
					lst[i].usr.Name 
				});
			}

			data.DataSource = t;

			data.Cols[1].Visible = false;

			data.Cols[2].Caption = "Номер";
			data.Cols[2].Width = 100;

			data.Cols[3].Caption = "Документ";
			data.Cols[3].Width = 200;

			data.Cols[4].Caption = "Создан";
			data.Cols[4].Width = 150;

			data.Cols[5].Caption = "Автор";
			data.Cols[5].Width = 250;

		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnUpdate_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void btnOpenInventory_Click(object sender, EventArgs e)
		{
			if (data.Row != -1)
			{
				if (data.GetData(data.Row, 1) != null)
				{
					OpenDoc(int.Parse(data.GetData(data.Row, 1).ToString()));
				}
			}
		}

		private void OpenDoc(int _id)
		{
			using (frmInventoryDoc f = new frmInventoryDoc(usr))
			{
				f.id = _id;
				f.ShowDialog();
				LoadData();
			}
		}

		private void data_DoubleClick(object sender, EventArgs e)
		{
			if (data.Row != -1)
			{
				if (data.GetData(data.Row, 1) != null)
				{
					OpenDoc(int.Parse(data.GetData(data.Row, 1).ToString()));
				}
			}
		}
	}
}
