using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PSA.Lib.Util;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace PSA.Core.Admin
{
    public partial class frmRebuildIndex : Form
    {
        Settings prop = new Settings();
        public SqlConnection db_connection;

        public frmRebuildIndex()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            btnStart.Enabled = false;

            List<string> s = new List<string>();
            s.Add("ALTER INDEX [PK_order] ON [dbo].[order] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_order] ON [dbo].[order] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_order_1] ON [dbo].[order] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_order_2] ON [dbo].[order] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_order_3] ON [dbo].[order] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_order_4] ON [dbo].[order] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_order_5] ON [dbo].[order] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_order_6] ON [dbo].[order] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_order_7] ON [dbo].[order] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [PK_order] ON [dbo].[order] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_order] ON [dbo].[order] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_order_1] ON [dbo].[order] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_order_2] ON [dbo].[order] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_order_3] ON [dbo].[order] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_order_4] ON [dbo].[order] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_order_5] ON [dbo].[order] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_order_6] ON [dbo].[order] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_order_7] ON [dbo].[order] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [PK_orderbody] ON [dbo].[orderbody] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_orderbody] ON [dbo].[orderbody] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_orderbody_1] ON [dbo].[orderbody] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_orderbody_2] ON [dbo].[orderbody] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_orderbody_3] ON [dbo].[orderbody] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_orderbody_4] ON [dbo].[orderbody] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [PK_orderbody] ON [dbo].[orderbody] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_orderbody] ON [dbo].[orderbody] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_orderbody_1] ON [dbo].[orderbody] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_orderbody_2] ON [dbo].[orderbody] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_orderbody_3] ON [dbo].[orderbody] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_orderbody_4] ON [dbo].[orderbody] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [PK_orderevent] ON [dbo].[orderevent] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_orderevent] ON [dbo].[orderevent] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [PK_orderevent] ON [dbo].[orderevent] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_orderevent] ON [dbo].[orderevent] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [PK_payments] ON [dbo].[payments] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_payments] ON [dbo].[payments] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_payments_1] ON [dbo].[payments] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_payments_2] ON [dbo].[payments] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_payments_3] ON [dbo].[payments] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [PK_payments] ON [dbo].[payments] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_payments] ON [dbo].[payments] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_payments_1] ON [dbo].[payments] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_payments_2] ON [dbo].[payments] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_payments_3] ON [dbo].[payments] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [PK_price] ON [dbo].[price] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [IX_price] ON [dbo].[price] REBUILD WITH ( PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, SORT_IN_TEMPDB = OFF, ONLINE = OFF );");
            s.Add("ALTER INDEX [PK_price] ON [dbo].[price] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("ALTER INDEX [IX_price] ON [dbo].[price] REORGANIZE WITH ( LOB_COMPACTION = ON );");
            s.Add("DBCC CHECKDB;");
            s.Add("DBCC SHRINKDATABASE (" + prop.Db_base + ");");
            s.Add("DBCC SHRINKDATABASE(N'" + prop.Db_base + "');");

            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 9000;
            cmd.Connection = db_connection;

            pb.Minimum = 0;
            pb.Maximum = s.Count;
            pb.Value = 0;
            int er = 0;
            foreach (string q in s)
            {
                try
                {
                    cmd.CommandText = q;
                    cmd.ExecuteScalar();
                }
                catch 
                {
                    er++;
                }
                try
                {
                    pb.Value++;
                    Application.DoEvents();
                }
                catch { }
            }

            btnCancel.Enabled = true;
            btnStart.Enabled = true;

            MessageBox.Show("Все задачи выполнены.\n" + er.ToString() + " сообщений о проблемах.");
            this.Close();
        }
    }
}
