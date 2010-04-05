using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Photoland.Operator
{
    public partial class frmSelectMashineAndPaper : Form
    {
        public SqlConnection db_connection;

        private DataTable mashine = new DataTable("Mashine");
        private DataTable paper = new DataTable("Paper");

        public string _id_mashine = "", _id_paper = "";

        public frmSelectMashineAndPaper()
        {
            InitializeComponent();
			this.Text = "Выбор машины и типа бумаги";
			this.Text = "Выбор машины";
        }

        private void frmSelectMashineAndPaper_Load(object sender, EventArgs e)
        {
            SqlCommand tmp =
                new SqlCommand("SELECT [id_mashine], [mashine] FROM [vwMashine] ORDER BY [mashine]", db_connection);
            SqlDataAdapter ad = new SqlDataAdapter(tmp);
            ad.Fill(mashine);

            tmp =
                new SqlCommand("SELECT [id_material], [material] FROM [vwPaper] ORDER BY [material]", db_connection);
            ad = new SqlDataAdapter(tmp);
            ad.Fill(paper);

            //txtPaper.DataSource = paper;
            //txtPaper.DisplayMember = "material";
            //txtPaper.ValueMember = "id_material";
            //txtPaper.SelectedValue = "";

			DataRow r = mashine.NewRow();
			r[0] = "-1";
			r[1] = "Вход для приемки";
			mashine.Rows.Add(r);

            txtMashine.DataSource = mashine;
            txtMashine.DisplayMember = "mashine";
            txtMashine.ValueMember = "id_mashine";
            txtMashine.SelectedValue = "";

            if (_id_mashine != "")
            {
                txtMashine.SelectedValue = _id_mashine;
            }
			// if (_id_paper != "")
			// {
			//     txtPaper.SelectedValue = _id_paper;
			// }
			btnOK.Enabled = false;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
			//if ((txtPaper.Text != "") && (txtMashine.Text != ""))
			if (txtMashine.Text != "")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

		private void txtCounter_ValueChanged(object sender, EventArgs e)
		{
			if (txtCounter.Value > 0)
			{
				btnOK.Enabled = true;
			}
			else
			{
				btnOK.Enabled = false;
			}
		}

		private void txtCounter_KeyUp(object sender, KeyEventArgs e)
		{
			if (txtCounter.Value > 0)
			{
				btnOK.Enabled = true;
			}
			else
			{
				btnOK.Enabled = false;
			}
		}

		private void txtMashine_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (txtMashine.SelectedValue != null)
			{
				if (txtMashine.SelectedValue.ToString().Trim() == "-1")
				{
					btnOK.Enabled = true;
					txtCounter.Enabled = false;
				}
				else
				{
					btnOK.Enabled = false;
					txtCounter.Enabled = true;
				}
			}
		}


    }
}