using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Photoland.Security;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;


namespace Photoland.Forms.Interface
{
	public partial class frmOtherDiscont : Form
	{
		private SqlConnection db_connection;
		private SqlCommand cmd;
		private DataTable tbl = new DataTable("odc");

		public decimal pers = 0;

		public frmOtherDiscont(SqlConnection db_connection)
		{
			InitializeComponent();
			this.Text = "Другие дисконтные карты";
			this.db_connection = db_connection;
		}

		private void frmOtherDiscont_Load(object sender, EventArgs e)
		{
			cmd = new SqlCommand("SELECT [percent], RTRIM([name]) + ' [' + RTRIM(CAST([percent] AS nchar(10))) + '%]' AS [name] FROM [vwOtherDCard] ORDER BY [name]", db_connection);
			SqlDataAdapter da = new SqlDataAdapter(cmd);
			da.Fill(tbl);
			txtOtherDiscont.DataSource = tbl;
			txtOtherDiscont.DisplayMember = "name";
			txtOtherDiscont.ValueMember = "percent";
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btnOK_Click(object sender, EventArgs e)
		{
			pers = decimal.Parse(txtOtherDiscont.SelectedValue.ToString());
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}
	}
}