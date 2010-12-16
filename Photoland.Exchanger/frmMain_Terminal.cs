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
using Photoland.Components.FilterRow;
using Photoland.Forms.Interface;
using Photoland.Lib;
using Photoland.Order;
using Photoland.Security;
using Photoland.Security.Client;
using Photoland.Security.User;
using PSA.Lib.Interface;
using PSA.Lib.Util;
using PSA.Lib.Interface;

namespace Photoland.Exchanger
{
	public partial class frmMain : Form
	{
		private void DoImportTerminal()
		{
			lblSaving.Visible = true;
			try
			{
				frmExportScan fscan = new frmExportScan();
				fscan.status = false;
				fscan.ShowDialog();
				if (fscan.DialogResult == DialogResult.OK)
				{
					pb.Minimum = 0;
					pb.Maximum = fscan.numbers.Count;
					pb.Value = 0;
					pb.Visible = true;
					foreach (string n in fscan.numbers)
					{
						Application.DoEvents();
						RemoteQuery rq = new RemoteQuery(usr);
						rq.GetData(n);
						if (pb.Value < pb.Maximum)
							pb.Value++;
					}
					pb.Visible = false;
					pb.Value = 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
			lblSaving.Visible = false;
		}

		private void DoImportMFoto()
		{
			lblSaving.Visible = true;
			try
			{
				frmExportScan fscan = new frmExportScan();
				fscan.status = false;
				fscan.ShowDialog();
				if (fscan.DialogResult == DialogResult.OK)
				{
					pb.Minimum = 0;
					pb.Maximum = fscan.numbers.Count;
					pb.Value = 0;
					pb.Visible = true;
					foreach (string n in fscan.numbers)
					{
						Application.DoEvents();
						RemoteQuery rq = new RemoteQuery(usr);
						rq.GetMFotoData(n);
						if (pb.Value < pb.Maximum)
							pb.Value++;
					}
					pb.Visible = false;
					pb.Value = 0;
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.Source);
			}
			lblSaving.Visible = false;
		}
	}
}
