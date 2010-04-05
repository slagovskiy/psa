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
using PSA.Lib.Interface;
using PSA.Lib.Util;


namespace Photoland.Operator
{
	partial class frmMain : Form
	{
		private void btnAvdInfo_Click(object sender, EventArgs e)
		{
			/*
			try
			{
				string y = DateTime.Parse(lblFInfoOrderDateIn.Text).Year.ToString();
				string m = DateTime.Parse(lblFInfoOrderDateIn.Text).Month < 10
							? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString()
							: DateTime.Parse(lblFInfoOrderDateIn.Text).Month.ToString();
				string d = DateTime.Parse(lblFInfoOrderDateIn.Text).Day < 10
							? "0" + DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString()
							: DateTime.Parse(lblFInfoOrderDateIn.Text).Day.ToString();
				string f = prop.Dir_print + "\\" + y + "\\" + m + "\\" + d + "\\" + lblFInfoOrderNo.Text.Trim() + "\\" +
						   lblFInfoOrderNo.Text.Trim() + ".txt";
				if (File.Exists(f))
				{
					System.Diagnostics.Process.Start(f);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("Ошибка при открытии файла информации", "Модуль оператора", MessageBoxButtons.OK,
								MessageBoxIcon.Warning);
			}
			 */
		}

		private void button2_Click(object sender, EventArgs e)
		{
			tmr.Stop();
			this.TopMost = false;

			try
			{
				ShowReport("Material 1", false, C1.Win.C1Report.FileFormatEnum.Text);
			}
			catch (Exception ex)
			{
				ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Ошибка вывода отчета\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			tmr.Start();
			this.TopMost = prop.Mod_operator_top_most;

		}

		private void btnClearFilter_Click(object sender, EventArgs e)
		{
			txtFilterCrop.SelectedValue = 0;
			txtFilterGoods.SelectedValue = 0;
			txtFilterPaper.SelectedValue = 0;
			tmr.Stop();
			FillOrderList();
			tmr.Start();
		}

		private void ShowReport(string rep_name, bool export, C1.Win.C1Report.FileFormatEnum format)
		{
			if (CheckState(db_connection))
			{
				tmr.Stop();
				this.TopMost = false;
				if (prop.PathReportsTemplates != "")
				{
					{
						bool ok = false;
						DataTable r = new DataTable("Report");
						SqlCommand c;
						SqlDataAdapter a;
						switch (rep_name)
						{
							case "Work by service":
								{
									/*
									 SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) < CONVERT(DATETIME, '2009-01-01 00:00:00', 102)) ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated WHERE (dbo.good.type = N'1') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd HAVING (SUM(orderbody_1.actual_quantity) > 0) AND (datecnt.dated > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (datecnt.dated < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
									 */
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									f.ShowDialog();
									if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
									{
										string filter = "";
										string filter2 = "";
										filter =
											" (datecnt.dated >= CONVERT(DATETIME, '" +
											f.Y1 + "-" +
											f.M1 + "-" +
											f.D1 +
											" 00:00:00', 102) AND datecnt.dated <= CONVERT(DATETIME, '" +
											f.Y2 + "-" +
											f.M2 + "-" +
											f.D2 + " 23:59:59', 102))";
										filter2 =
											" (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) >= CONVERT(DATETIME, '" +
											f.Y1 + "-" +
											f.M1 + "-" +
											f.D1 +
											" 00:00:00', 102) AND DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) <= CONVERT(DATETIME, '" +
											f.Y2 + "-" +
											f.M2 + "-" +
											f.D2 + " 23:59:59', 102))";
										if (f.checkCurentUser.Checked)
											filter += " AND (orderbody_1.id_user_work = " + usr.Id_user + ")";
										string query =
											"SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd, orderbody_1.id_user_work FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING " + filter2 + " ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated WHERE (dbo.good.type <> N'2') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd, orderbody_1.id_user_work HAVING (SUM(orderbody_1.actual_quantity) > 0) AND " + filter;
										c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										ok = true;
									}
									break;
								}
							case "Work by service adv":
								{
									/*
									 SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) < CONVERT(DATETIME, '2009-01-01 00:00:00', 102)) ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated WHERE (dbo.good.type = N'1') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd HAVING (SUM(orderbody_1.actual_quantity) > 0) AND (datecnt.dated > CONVERT(DATETIME, '2000-01-01 00:00:00', 102)) AND (datecnt.dated < CONVERT(DATETIME, '2009-01-01 00:00:00', 102))
									 */
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									f.ShowDialog();
									if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
									{
										string filter = "";
										string filter2 = "";
										filter =
											" (datecnt.dated >= CONVERT(DATETIME, '" +
											f.Y1 + "-" +
											f.M1 + "-" +
											f.D1 +
											" 00:00:00', 102) AND datecnt.dated <= CONVERT(DATETIME, '" +
											f.Y2 + "-" +
											f.M2 + "-" +
											f.D2 + " 23:59:59', 102))";
										filter2 =
											" (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) >= CONVERT(DATETIME, '" +
											f.Y1 + "-" +
											f.M1 + "-" +
											f.D1 +
											" 00:00:00', 102) AND DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) <= CONVERT(DATETIME, '" +
											f.Y2 + "-" +
											f.M2 + "-" +
											f.D2 + " 23:59:59', 102))";
										if (f.checkCurentUser.Checked)
											filter += " AND (orderbody_1.id_user_work = " + usr.Id_user + ")";
										string query =
											"SELECT TOP (100) PERCENT datecnt.dated, orderbody_1.name_work, dbo.good.name, SUM(orderbody_1.actual_quantity) AS cnt, datecnt.cntd, orderbody_1.id_user_work, dbo.[order].number FROM dbo.good INNER JOIN dbo.orderbody AS orderbody_1 ON dbo.good.id_good = orderbody_1.id_good INNER JOIN (SELECT TOP (100) PERCENT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS dated, good_1.id_good, SUM(dbo.orderbody.actual_quantity) AS cntd FROM dbo.orderbody INNER JOIN dbo.good AS good_1 ON dbo.orderbody.id_good = good_1.id_good GROUP BY good_1.id_good, DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) HAVING " + filter2 + " ORDER BY dated) AS datecnt ON orderbody_1.id_good = datecnt.id_good AND DATEADD(dd, 0, DATEDIFF(dd, 0, orderbody_1.datework)) = datecnt.dated INNER JOIN dbo.[order] ON orderbody_1.id_order = dbo.[order].id_order WHERE (dbo.good.type <> N'2') GROUP BY datecnt.dated, orderbody_1.name_work, dbo.good.name, datecnt.cntd, orderbody_1.id_user_work, dbo.[order].number HAVING (SUM(orderbody_1.actual_quantity) > 0) AND " + filter;
										c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										ok = true;
									}
									break;
								}
							case "Discarding designer 1":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									f.ShowDialog();
									if (f.DialogResult == DialogResult.OK)
									{
										string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
										c =
											new SqlCommand(
												"SELECT id_discard, datediscard, id_material, material, quantity, comment, user_name, orderno, id_user, CONVERT(char, datediscard, 103) AS d FROM dbo.vwDiscardList WHERE (datediscard >= CONVERT(datetime, '" +
												f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120)) AND (datediscard <= CONVERT(datetime, '" + f.Y2 + "/" +
												f.M2 + "/" + f.D2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ")", db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										rep.Fields["lblDates"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
										if (f.Userfilter) rep.Fields["lblUser"].Text = "по пользователю " + usr.Name;
										ok = true;
									}
									break;
								}
							case "Discarding designer 2":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									f.ShowDialog();
									if (f.DialogResult == DialogResult.OK)
									{
										string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
										c =
											new SqlCommand(
												"SELECT material, SUM(quantity) AS quantity, user_name, CONVERT(char, datediscard, 103) AS d " +
												"FROM dbo.vwDiscardList " +
												"WHERE (datediscard >= CONVERT(datetime, '" +
												f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120)) AND (datediscard <= CONVERT(datetime, '" + f.Y2 + "/" +
												f.M2 + "/" + f.D2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ") " +
												"GROUP BY material, user_name, CONVERT(char, datediscard, 103) ", db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										rep.Fields["lblDates"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
										if (f.Userfilter) rep.Fields["lblUser"].Text = "по пользователю " + usr.Name;
										ok = true;
									}
									break;
								}
							case "Discarding designer 3":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									f.ShowDialog();
									if (f.DialogResult == DialogResult.OK)
									{
										string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
										c =
											new SqlCommand(
												"SELECT material, SUM(quantity) AS quantity, CONVERT(char, datediscard, 103) AS d " +
												"FROM dbo.vwDiscardList " +
												"WHERE (datediscard >= CONVERT(datetime, '" +
												f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120)) AND (datediscard <= CONVERT(datetime, '" + f.Y2 + "/" +
												f.M2 + "/" + f.D2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ") " +
												"GROUP BY material, CONVERT(char, datediscard, 103)", db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										rep.Fields["lblDates"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
										if (f.Userfilter) rep.Fields["lblUser"].Text = "по пользователю " + usr.Name;
										ok = true;
									}
									break;
								}
							case "Discarding designer 4":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									f.ShowDialog();
									if (f.DialogResult == DialogResult.OK)
									{
										string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
										c =
											new SqlCommand(
												"SELECT material, SUM(quantity) AS quantity " +
												"FROM dbo.vwDiscardList " +
												"WHERE (datediscard >= CONVERT(datetime, '" +
												f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120)) AND (datediscard <= CONVERT(datetime, '" + f.Y2 + "/" +
												f.M2 + "/" + f.D2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ") " +
												"GROUP BY material ", db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										rep.Fields["lblDates"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
										if (f.Userfilter) rep.Fields["lblUser"].Text = "по пользователю " + usr.Name;
										ok = true;
									}
									break;
								}
							case "Discarding designer 5":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									f.ShowDialog();
									if (f.DialogResult == DialogResult.OK)
									{
										string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
										c =
											new SqlCommand(
												"SELECT datediscard, material, quantity, comment, user_name " +
												"FROM dbo.vwDiscardList " +
												"WHERE (datediscard >= CONVERT(datetime, '" +
												f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120)) AND (datediscard <= CONVERT(datetime, '" + f.Y2 + "/" +
												f.M2 + "/" + f.D2 + " 23:59:59.999', 120)) AND (id_user " + usrfilter + ")", db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										rep.Fields["lblDates"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
										if (f.Userfilter) rep.Fields["lblUser"].Text = "по пользователю " + usr.Name;
										ok = true;
									}
									break;
								}
							case "Material 1":
								{
									string query = "SELECT * FROM [vwMaterialReportOperator] ORDER BY [material]";
									c = new SqlCommand(query, db_connection);
									c.CommandTimeout = 9000;
									a = new SqlDataAdapter(c);
									a.Fill(r);
									rep.Load(prop.PathReportsTemplates, rep_name);
									rep.DataSource.Recordset = r;
									ok = true;
									break;
								}
							case "Defect Operator 1":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									f.ShowDialog();
									string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
									if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
									{
										string query =
											"SELECT [id_orderbody], [id_order], [id_good], [name], [guid], [defect_quantity], [actual_quantity], [type], [datework], [id_user_work], [name_work], [tech_defect], [number], [material], [mashine] FROM [vwOrderBodyOperatorDefect] WHERE ";
										query += f.Userfilter
													? "(id_user_work = " + usr.Id_user + ") "
													: "(id_user_work <> 0) ";
										query += " AND ((datework >= CONVERT(datetime, '" + f.Y1 + "/" + f.M1 + "/" + f.D1 +
												 " 00:00:00.000', 120)) AND (datework <= CONVERT(datetime, '" + f.Y2 + "/" + f.M2 + "/" + f.D2 +
												 " 23:59:59.999', 120)))";
										c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										ok = true;
									}
									break;
								}
							case "Defect Operator 2":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									f.ShowDialog();
									string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
									if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
									{
										string query =
											"SELECT [id_orderbody], [id_order], [id_good], [name], [guid], [defect_quantity], [actual_quantity], [type], [datework], [id_user_work], [name_work], [tech_defect], [number], [material], [mashine] FROM [vwOrderBodyOperatorDefectO] WHERE ";
										query += f.Userfilter
													? "(id_user_work = " + usr.Id_user + ") "
													: "(id_user_work <> 0) ";
										query += " AND ((datework >= CONVERT(datetime, '" + f.Y1 + "/" + f.M1 + "/" + f.D1 +
												 " 00:00:00.000', 120)) AND (datework <= CONVERT(datetime, '" + f.Y2 + "/" + f.M2 + "/" + f.D2 +
												 " 23:59:59.999', 120)))";
										c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										ok = true;
									}
									break;
								}
							case "Work 1":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									if (usr.prmCanLoginAdmin)
										f.checkCurentUser.Enabled = true;
									else
										f.checkCurentUser.Enabled = false;
									f.ShowDialog();
									string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
									if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
									{
										/*
SELECT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS datework, dbo.orderbody.name_work, dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt, dbo.orderbody.id_user_work FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) GROUP BY DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)), dbo.orderbody.name_work, dbo.good.name, dbo.orderbody.id_user_work HAVING (dbo.orderbody.id_user_work <> 0) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2008/05/05 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '2008/05/08 23:59:59.999', 120)) ORDER BY datework, dbo.orderbody.name_work, dbo.good.name
										 */
										string query =
											"SELECT DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) AS datework, dbo.orderbody.name_work, dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt, dbo.orderbody.id_user_work FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) GROUP BY DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)), dbo.orderbody.name_work, dbo.good.name, dbo.orderbody.id_user_work ";
										query += f.Userfilter
													? "HAVING (dbo.orderbody.id_user_work = " + usr.Id_user + ")"
													: "HAVING (dbo.orderbody.id_user_work <> 0) ";
										query += " AND ((DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '" + f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '" + f.Y2 + "/" + f.M2 + "/" + f.D2 + " 23:59:59.999', 120))) ORDER BY datework, dbo.orderbody.name_work, dbo.good.name";
										c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										rep.Fields["Filter"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
										ok = true;
									}
									break;
								}
							case "Work 2":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									if (usr.prmCanLoginAdmin)
										f.checkCurentUser.Enabled = true;
									else
										f.checkCurentUser.Enabled = false;
									f.ShowDialog();
									string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
									if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
									{
										/*
SELECT dbo.orderbody.name_work, dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt, dbo.orderbody.id_user_work FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2008/05/05 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '2008/05/08 23:59:59.999', 120)) GROUP BY dbo.orderbody.name_work, dbo.good.name, dbo.orderbody.id_user_work HAVING (dbo.orderbody.id_user_work <> 0) ORDER BY dbo.orderbody.name_work, dbo.good.name                                         */
										string query =
											"SELECT dbo.orderbody.name_work, dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt, dbo.orderbody.id_user_work FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) ";
										query += " AND ((DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '" + f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '" + f.Y2 + "/" + f.M2 + "/" + f.D2 + " 23:59:59.999', 120))) ";
										query += "  GROUP BY dbo.orderbody.name_work, dbo.good.name, dbo.orderbody.id_user_work ";
										query += f.Userfilter
													? " HAVING (dbo.orderbody.id_user_work = " + usr.Id_user + ") ORDER BY dbo.orderbody.name_work, dbo.good.name"
													: " HAVING (dbo.orderbody.id_user_work <> 0) ORDER BY dbo.orderbody.name_work, dbo.good.name";
										c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										rep.Fields["Filter"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
										ok = true;
									}
									break;
								}
							case "Work 3":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									if (usr.prmCanLoginAdmin)
										f.checkCurentUser.Enabled = true;
									else
										f.checkCurentUser.Enabled = false;
									f.ShowDialog();
									string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
									if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
									{
										/*
SELECT dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) AND (DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '2008/05/05 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '2008/05/08 23:59:59.999', 120)) AND (dbo.orderbody.id_user_work <> 0) GROUP BY dbo.good.name ORDER BY dbo.good.name     
										 */
										string query =
											"SELECT dbo.good.name, SUM(dbo.orderbody.actual_quantity) AS cnt FROM dbo.orderbody LEFT OUTER JOIN dbo.good ON dbo.orderbody.id_good = dbo.good.id_good WHERE (dbo.orderbody.tech_defect = 0) ";
										query += " AND ((DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) > CONVERT(DATETIME, '" + f.Y1 + "/" + f.M1 + "/" + f.D1 + " 00:00:00.000', 120) OR DATEADD(dd, 0, DATEDIFF(dd, 0, dbo.orderbody.datework)) = CONVERT(DATETIME, '" + f.Y2 + "/" + f.M2 + "/" + f.D2 + " 23:59:59.999', 120))) ";
										query += f.Userfilter
													? " AND (dbo.orderbody.id_user_work = " + usr.Id_user + ") "
													: " AND (dbo.orderbody.id_user_work <> 0) ";
										query += "  GROUP BY dbo.good.name ORDER BY dbo.good.name";
										c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										rep.Fields["Filter"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
										ok = true;
									}
									break;
								}
							case "1":
								{
									frmGetDateIntervalUserCheck f = new frmGetDateIntervalUserCheck();
									f.ShowDialog();
									string usrfilter = f.Userfilter ? " = " + usr.Id_user.ToString() : " <> 0";
									if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
									{
										string query = "SELECT " +
													   "RTRIM(vwOrderBodyDesigner.name) AS name, " +
													   "SUM(vwOrderBodyDesigner.actual_quantity) AS quantity, " +
													   "vwOrderBodyDesigner.id_user_work, " +
													   "RTRIM(vwOrderBodyDesigner.name_work) AS name_work " +
													   "FROM vwOrderBodyDesigner INNER JOIN " +
													   "[order] ON vwOrderBodyDesigner.id_order = [order].id_order " +
													   "WHERE (vwOrderBodyDesigner.datework >= CONVERT(datetime, '" + f.Y1 + "/" + f.M1 + "/" + f.D1 +
													   " 00:00:00.000', 120)) " +
													   "AND " +
													   "(vwOrderBodyDesigner.datework <= CONVERT(datetime, '" + f.Y2 + "/" + f.M2 + "/" + f.D2 +
													   " 23:59:59.999', 120)) " +
													   "GROUP BY " +
													   "vwOrderBodyDesigner.name, " +
													   "vwOrderBodyDesigner.id_user_work, " +
													   "vwOrderBodyDesigner.name_work " +
													   "HAVING " +
													   "(SUM(vwOrderBodyDesigner.actual_quantity) > 0) " +
													   "AND ";
										query += f.Userfilter
													? "(vwOrderBodyDesigner.id_user_work = " + usr.Id_user + ") "
													: "(vwOrderBodyDesigner.id_user_work <> 0) ";
										c = new SqlCommand(query, db_connection);
										c.CommandTimeout = 9000;
										a = new SqlDataAdapter(c);
										a.Fill(r);
										rep.Load(prop.PathReportsTemplates, rep_name);
										rep.DataSource.Recordset = r;
										rep.Fields["Header"].Text = "c " + f.Y1 + "/" + f.M1 + "/" + f.D1 + " по " + f.Y2 + "/" + f.M2 + "/" + f.D2;
										rep.Fields["Header"].Text += f.Userfilter
																		? " Дизайнер: " + usr.Name
																		: "";
										ok = true;
									}
									break;
								}
							default:
								{
									ok = false;
									break;
								}
						}

						if (ok)
						{
							if (export)
							{
								switch (format)
								{
									case C1.Win.C1Report.FileFormatEnum.PDF:
										{
											sdlg.Filter = "Adobe PDF (*.pdf)|*.pdf";
											sdlg.ShowDialog();
											if (sdlg.FileName != "")
											{
												rep.RenderToFile(sdlg.FileName, C1.Win.C1Report.FileFormatEnum.PDF);
											}
											break;
										}
									case C1.Win.C1Report.FileFormatEnum.Excel:
										{
											sdlg.Filter = "Microsoft Excel (*.xls)|*.xls";
											sdlg.ShowDialog();
											if (sdlg.FileName != "")
											{
												rep.RenderToFile(sdlg.FileName, C1.Win.C1Report.FileFormatEnum.Excel);
											}
											break;
										}
								}
							}
							else
							{
								PrintPreviewDialog pd = new PrintPreviewDialog();
								pd.Document = rep.Document;
								pd.ShowDialog();
							}
						}
					}
				}
				else
				{
					MessageBox.Show("Не выбран файл шаблонов отчетов", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				this.TopMost = prop.Mod_operator_top_most;
				tmr.Start();
			}
		}

		private void toolStripMenuItem2_Click(object sender, EventArgs e)
		{
			ShowReport("Material 1", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem3_Click(object sender, EventArgs e)
		{
			ShowReport("Material 1", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem4_Click(object sender, EventArgs e)
		{
			ShowReport("Material 1", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem7_Click(object sender, EventArgs e)
		{
			ShowReport("Defect Operator 1", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem10_Click(object sender, EventArgs e)
		{
			ShowReport("Defect Operator 2", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem8_Click(object sender, EventArgs e)
		{
			ShowReport("Defect Operator 1", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem9_Click(object sender, EventArgs e)
		{
			ShowReport("Defect Operator 1", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem11_Click(object sender, EventArgs e)
		{
			ShowReport("Defect Operator 2", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem12_Click(object sender, EventArgs e)
		{
			ShowReport("Defect Operator 2", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem14_Click(object sender, EventArgs e)
		{
			ShowReport("Work 1", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem15_Click(object sender, EventArgs e)
		{
			ShowReport("Work 1", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem16_Click(object sender, EventArgs e)
		{
			ShowReport("Work 1", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.TopMost = false;
			Photoland.Forms.Interface.frmAbout f = new Photoland.Forms.Interface.frmAbout();
			f.ShowDialog();
			this.TopMost = prop.Mod_operator_top_most;
		}



		private void FillDiscardTable()
		{
			if (CheckState(db_connection))
			{
				DateTime tm = new DateTime();
				tm = DateTime.Now.AddDays(-10);
				string y = tm.Year.ToString();
				string m = tm.Month < 10
							   ? "0" + tm.Month.ToString()
							   : tm.Month.ToString();
				string d = tm.Day < 10
							   ? "0" + tm.Day.ToString()
							   : tm.Day.ToString();

				db_discard.Rows.Clear();
				db_command_discard =
					new SqlCommand(
						"SELECT [id_discard], [datediscard], [material], [quantity], [comment], [user_name], [orderno], [mashine] FROM [vwDiscardList] WHERE [datediscard] > 0 AND CONVERT(datetime, [datediscard]) >= CONVERT(datetime, '" +
						y + "/" + m + "/" + d + " 00:00:00.000') AND [id_user] = " + usr.Id_user.ToString() + " ORDER BY [datediscard]",
						db_connection);
				db_adapter_discard = new SqlDataAdapter(db_command_discard);
				db_adapter_discard.Fill(db_discard);

				gridTehAction1.DataSource = db_discard;

				/*
				 * 1 [id_discard], 
				 * 2 [datediscard], 
				 * 3 [material], 
				 * 4 [quantity], 
				 * 5 [comment], 
				 * 6 [user_name], 
				 * 7 [orderno]
				 */

				gridTehAction1.Cols[1].Visible = false;
				gridTehAction1.Cols[2].Visible = false;
				gridTehAction1.Cols[3].Visible = false;
				gridTehAction1.Cols[4].Visible = false;
				gridTehAction1.Cols[5].Visible = false;
				gridTehAction1.Cols[6].Visible = false;
				gridTehAction1.Cols[7].Visible = false;



				gridTehAction1.Cols[2].Visible = true;
				gridTehAction1.Cols[2].Width = 105;
				gridTehAction1.Cols[2].AllowDragging = false;
				gridTehAction1.Cols[2].AllowEditing = false;
				gridTehAction1.Cols[2].AllowMerging = false;
				gridTehAction1.Cols[2].AllowResizing = true;
				gridTehAction1.Cols[2].AllowSorting = true;
				gridTehAction1.Cols[2].Caption = "Дата";
				gridTehAction1.Cols[2].Format = "g";

				gridTehAction1.Cols[3].Visible = true;
				gridTehAction1.Cols[3].Width = 205;
				gridTehAction1.Cols[3].AllowDragging = false;
				gridTehAction1.Cols[3].AllowEditing = false;
				gridTehAction1.Cols[3].AllowMerging = false;
				gridTehAction1.Cols[3].AllowResizing = true;
				gridTehAction1.Cols[3].AllowSorting = true;
				gridTehAction1.Cols[3].Caption = "Материал";

				gridTehAction1.Cols[4].Visible = true;
				gridTehAction1.Cols[4].Width = 60;
				gridTehAction1.Cols[4].AllowDragging = false;
				gridTehAction1.Cols[4].AllowEditing = false;
				gridTehAction1.Cols[4].AllowMerging = false;
				gridTehAction1.Cols[4].AllowResizing = true;
				gridTehAction1.Cols[4].AllowSorting = true;
				gridTehAction1.Cols[4].Caption = "Кол-во";
				gridTehAction1.Cols[4].Format = "N2";

				gridTehAction1.Cols[5].Visible = true;
				gridTehAction1.Cols[5].Width = 205;
				gridTehAction1.Cols[5].AllowDragging = false;
				gridTehAction1.Cols[5].AllowEditing = false;
				gridTehAction1.Cols[5].AllowMerging = false;
				gridTehAction1.Cols[5].AllowResizing = true;
				gridTehAction1.Cols[5].AllowSorting = true;
				gridTehAction1.Cols[5].Caption = "Причина";

				gridTehAction1.Cols[6].Visible = true;
				gridTehAction1.Cols[6].Width = 125;
				gridTehAction1.Cols[6].AllowDragging = false;
				gridTehAction1.Cols[6].AllowEditing = false;
				gridTehAction1.Cols[6].AllowMerging = false;
				gridTehAction1.Cols[6].AllowResizing = true;
				gridTehAction1.Cols[6].AllowSorting = true;
				gridTehAction1.Cols[6].Caption = "Пользователь";

				gridTehAction1.Cols[7].Visible = true;
				gridTehAction1.Cols[7].Width = 100;
				gridTehAction1.Cols[7].AllowDragging = false;
				gridTehAction1.Cols[7].AllowEditing = false;
				gridTehAction1.Cols[7].AllowMerging = false;
				gridTehAction1.Cols[7].AllowResizing = true;
				gridTehAction1.Cols[7].AllowSorting = true;
				gridTehAction1.Cols[7].Caption = "Заказ №";

				gridTehAction1.Cols[8].Visible = true;
				gridTehAction1.Cols[8].Width = 100;
				gridTehAction1.Cols[8].AllowDragging = false;
				gridTehAction1.Cols[8].AllowEditing = false;
				gridTehAction1.Cols[8].AllowMerging = false;
				gridTehAction1.Cols[8].AllowResizing = true;
				gridTehAction1.Cols[8].AllowSorting = true;
				gridTehAction1.Cols[8].Caption = "Машина";

			}
		}

		private void btnAddBrak1_Click(object sender, EventArgs e)
		{
			if (CheckState(db_connection))
			{
				this.TopMost = false;

				frmDiscard f = new frmDiscard();
				f.db_connection = db_connection;
				f.usr = usr;
				f.mashine = usr.Id_Mashine.Trim();
				f.txtOrderNo.Text = lblFInfoOrderNo.Text;
				f.ShowDialog();
				if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
					FillDiscardTable();

				this.TopMost = prop.Mod_operator_top_most;
			}
		}

		private void btnEditBrak1_Click(object sender, EventArgs e)
		{
			try
			{
				if (CheckState(db_connection))
				{
					if (int.Parse(gridTehAction1.GetData(gridTehAction1.Row, 1).ToString()) > 0)
					{
						this.TopMost = false;

						frmDiscard f = new frmDiscard();
						f.db_connection = db_connection;
						f.usr = usr;
						f.txtOrderNo.Text = lblFInfoOrderNo.Text;
						f.Id = int.Parse(gridTehAction1.GetData(gridTehAction1.Row, 1).ToString());
						f.ShowDialog();
						if (f.DialogResult == System.Windows.Forms.DialogResult.OK)
							FillDiscardTable();

						this.TopMost = prop.Mod_operator_top_most;
					}
				}
			}
			catch
			{

			}
		}

		private void btnDelBrak1_Click(object sender, EventArgs e)
		{
			try
			{
				if (CheckState(db_connection))
				{
					tmr.Stop();
					if (
						MessageBox.Show("Удалить запись о списании?", "Удаление", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) ==
						DialogResult.OK)
					{
						try
						{
							if (int.Parse(gridTehAction1.GetData(gridTehAction1.Row, 1).ToString()) > 0)
							{
								SqlCommand t =
									new SqlCommand(
										"UPDATE [discard] SET [del] = 1, exported = 0 WHERE [id_discard] = " +
										int.Parse(gridTehAction1.GetData(gridTehAction1.Row, 1).ToString()), db_connection);
								t.ExecuteNonQuery();
							}
						}
						catch (Exception ex)
						{
							ErrorNfo.WriteErrorInfo(ex);
						}
						finally
						{
							FillDiscardTable();
						}
					}
					tmr.Start();
				}
			}
			catch
			{ }
		}

		private void toolStripMenuItem17_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 1", false, C1.Win.C1Report.FileFormatEnum.Text);

		}

		private void toolStripMenuItem18_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 1", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem19_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 1", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem20_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 2", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem21_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 2", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem22_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 2", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem23_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 3", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem24_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 3", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem25_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 3", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem26_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 4", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem27_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 4", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem28_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 4", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem29_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 5", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem30_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 5", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem31_Click(object sender, EventArgs e)
		{
			ShowReport("Discarding designer 5", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void txtBarCode_KeyPress(object sender, KeyPressEventArgs e)
		{
			if ((e.KeyChar == '0') || (e.KeyChar == '1') || (e.KeyChar == '2') || (e.KeyChar == '3') || (e.KeyChar == '4') || (e.KeyChar == '5') || (e.KeyChar == '6') || (e.KeyChar == '7') || (e.KeyChar == '8') || (e.KeyChar == '9') || (e.KeyChar == (char)Keys.Return))
				e.Handled = false;
			else
				e.Handled = true;

			if (e.KeyChar == (char)Keys.Return)
			{
				QuickLoadOrder(true);
				txtBarCode.Text = "";
			}
		}

		private void toolStripMenuItem33_Click(object sender, EventArgs e)
		{
			ShowReport("Work 2", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem34_Click(object sender, EventArgs e)
		{
			ShowReport("Work 2", true, C1.Win.C1Report.FileFormatEnum.PDF);
		}

		private void toolStripMenuItem35_Click(object sender, EventArgs e)
		{
			ShowReport("Work 2", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem37_Click(object sender, EventArgs e)
		{
			ShowReport("Work 3", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem38_Click(object sender, EventArgs e)
		{
			ShowReport("Work 3", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem39_Click(object sender, EventArgs e)
		{
			ShowReport("Work 3", true, C1.Win.C1Report.FileFormatEnum.Excel);
		}

		private void toolStripMenuItem33_Click_1(object sender, EventArgs e)
		{

		}

		private void toolStripMenuItem34_Click_1(object sender, EventArgs e)
		{
		}

		private void toolStripMenuItem35_Click_1(object sender, EventArgs e)
		{
		}

		private void toolStripMenuItem13_Click(object sender, EventArgs e)
		{
			ShowReport("Work by service", false, C1.Win.C1Report.FileFormatEnum.Text);
		}

		private void toolStripMenuItem15_Click_1(object sender, EventArgs e)
		{
			ShowReport("Work by service", false, C1.Win.C1Report.FileFormatEnum.Text);

		}

		private void toolStripMenuItem16_Click_1(object sender, EventArgs e)
		{
			ShowReport("Work by service", true, C1.Win.C1Report.FileFormatEnum.Excel);

		}

		private void toolStripMenuItem14_Click_1(object sender, EventArgs e)
		{
			ShowReport("Work by service adv", false, C1.Win.C1Report.FileFormatEnum.Text);

		}

		private void toolStripMenuItem37_Click_1(object sender, EventArgs e)
		{
			ShowReport("Work by service adv", true, C1.Win.C1Report.FileFormatEnum.PDF);

		}

		private void toolStripMenuItem38_Click_1(object sender, EventArgs e)
		{
			ShowReport("Work by service adv", true, C1.Win.C1Report.FileFormatEnum.Excel);

		}

		private void semaphoresToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.TopMost = false;
			if (usr.prmCanSetup)
			{
				frmSemaphores fOptions = new frmSemaphores();
				fOptions.ShowDialog();
				prop = new PSA.Lib.Util.Settings();
			}
			else
			{
				MessageBox.Show("Нет доступа", "Доступ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			this.TopMost = prop.Mod_operator_top_most;
		}

		private void AddEvent(string Event, int id)
		{
			try
			{
				OrderInfo order = new OrderInfo(db_connection, id);
				string body = "";
				for (int i = 0; i < order.OrderBody.Rows.Count; i++)
				{
					body += order.OrderBody.Rows[i][9].ToString() + "|" +
							order.OrderBody.Rows[i][1].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][3].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][4].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][5].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][6].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][7].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][18].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][13].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][11].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][16].ToString().Trim() + "|" +
							order.OrderBody.Rows[i][17].ToString().Trim();
					body += "#";
				}
				body = body.Substring(0, body.Length - 1);
				body = "$$" + body + "$$" + order.AdvancedPayment + "$$" + order.FinalPayment + "$$" + order.Bonus;
				SqlCommand _cmd = new SqlCommand("INSERT INTO [dbo].[orderevent] ([id_order], [event_user], [event_status], [event_point], [event_text]) VALUES (" +
							order.Id + ", '" + usr.Name.Trim() + "', '" + order.Distanation + "', '" + prop.Order_prefics.Trim() +
							"', '" + Event + body + "')", db_connection);
				_cmd.ExecuteNonQuery();
			}
			catch (Exception ex)
			{
			}
		}

		private void btnClearBarCode_Click(object sender, EventArgs e)
		{
			txtBarCode.Text = "";
			txtBarCode.Focus();
		}

	}
}
