using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.Discont;
using Photoland.Security.User;
using Photoland.Forms.Admin;
using Photoland.Forms.Interface;
using Photoland.Lib;
using Photoland.Order;
using Photoland.Components.FilterRow;
using System.IO;
using C1.Win.C1FlexGrid;
using System.Globalization;

namespace Photoland.Operator
{
    public partial class SelectPaper : Form
    {
        public SqlConnection db_connection;

        private DataTable paper = new DataTable("Paper");

        public string _id_paper = "";

        public SelectPaper(SqlConnection _db_connection)
        {
            InitializeComponent();
            this.Text = "Выбор бумаги";
            db_connection = _db_connection;

        }

        private void SelectPaper_Load(object sender, EventArgs e)
        {
            SqlCommand tmp =
                new SqlCommand("SELECT [id_material], [material] FROM [vwPaper] ORDER BY [material]", db_connection);
            SqlDataAdapter ad = new SqlDataAdapter(tmp);
            ad.Fill(paper);
            DataRow rw = paper.NewRow();
            rw[0] = "0";
            rw[1] = "не выбрано (используется автоопределение в офисе)";
            paper.Rows.InsertAt(rw, 0);

            txtPaper.DataSource = paper;
            txtPaper.DisplayMember = "material";
            txtPaper.ValueMember = "id_material";
            txtPaper.SelectedValue = "";

            if (_id_paper == "") 
                _id_paper = "0";

             if (_id_paper != "")
             {
                 txtPaper.SelectedValue = _id_paper;
             }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPaper.Text != "")
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
