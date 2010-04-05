using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace PSA.Lib.Interface
{
	public partial class frmWasteList : PSA.Lib.Interface.Templates.frmTDialog
	{
		public Photoland.Security.User.UserInfo usr;
		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
		private Photoland.Components.FilterRow.FilterRowLike rf;

		public frmWasteList()
		{
			InitializeComponent();
			this.Title = "Расход";
		}

		private void frmWasteList_Load(object sender, EventArgs e)
		{
			date1.Value = DateTime.Now.AddDays(-20);
			date2.Value = DateTime.Now;
			LoadData();
		}

		private void LoadData()
		{
			try
			{
				DataTable tbl = new DataTable();
				using (SqlConnection con = new SqlConnection(prop.Connection_string))
				{
					con.Open();
					using (SqlCommand cmd = new SqlCommand("SELECT discard.id_discard, discard.datediscard, discard.id_material, material.material, discard.quantity, discard.id_user, discard.user_name, discard.comment, discard.id_mashine, mashine.mashine FROM discard LEFT OUTER JOIN mashine ON discard.id_mashine = mashine.id_mashine LEFT OUTER JOIN material ON discard.id_material = material.id_material WHERE(discard.datediscard >= CONVERT(DATETIME, '" + date1.Value.Year.ToString("D4") + "-" + date1.Value.Month.ToString("D2") + "-" + date1.Value.Day.ToString("D2") + " 00:00:00', 102) AND discard.datediscard <= CONVERT(DATETIME, '" + date2.Value.Year.ToString("D4") + "-" + date2.Value.Month.ToString("D2") + "-" + date2.Value.Day.ToString("D2") + " 23:59:59', 102))", con))
					{
						cmd.CommandTimeout = 9000;
						SqlDataAdapter da = new SqlDataAdapter(cmd);
						da.Fill(tbl);
					}
				}
				data.DataSource = tbl;

				data.Cols[1].Visible = false;
				data.Cols[2].Visible = true;
				data.Cols[3].Visible = false;
				data.Cols[4].Visible = true;
				data.Cols[5].Visible = true;
				data.Cols[6].Visible = false;
				data.Cols[7].Visible = true;
				data.Cols[8].Visible = true;
				data.Cols[9].Visible = false;
				data.Cols[10].Visible = true;

				data.Cols[2].Caption = "Дата";
				data.Cols[4].Caption = "Материал";
				data.Cols[5].Caption = "Кол-во";
				data.Cols[7].Caption = "Списал";
				data.Cols[8].Caption = "Комментарий";
				data.Cols[10].Caption = "Машина";

				data.Cols[2].Width = 80;
				data.Cols[4].Width = 250;
				data.Cols[5].Width = 120;
				data.Cols[7].Width = 180;
				data.Cols[8].Width = 120;
				data.Cols[10].Width = 120;

				data.Cols[2].AllowDragging = false;
				data.Cols[4].AllowDragging = false;
				data.Cols[5].AllowDragging = false;
				data.Cols[7].AllowDragging = false;
				data.Cols[8].AllowDragging = false;
				data.Cols[10].AllowDragging = false;

				data.Cols[2].AllowEditing = false;
				data.Cols[4].AllowEditing = false;
				data.Cols[5].AllowEditing = false;
				data.Cols[7].AllowEditing = false;
				data.Cols[8].AllowEditing = false;
				data.Cols[10].AllowEditing = false;

				data.Cols[2].AllowSorting = true;
				data.Cols[4].AllowSorting = true;
				data.Cols[5].AllowSorting = true;
				data.Cols[7].AllowSorting = true;
				data.Cols[8].AllowSorting = true;
				data.Cols[10].AllowSorting = true;

				if (rf == null)
					rf = new Photoland.Components.FilterRow.FilterRowLike(data);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace + "\n", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnApply_Click(object sender, EventArgs e)
		{
			LoadData();
		}

		private void btnAdd_Click(object sender, EventArgs e)
		{
			frmWasteEdit f = new frmWasteEdit();
			f.usr = usr;
			f.ShowDialog();
			LoadData();
		}
	}
}
