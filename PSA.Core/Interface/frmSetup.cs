using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Data.SqlClient;
using Photoland.Forms.Interface;

namespace PSA.Lib.Interface
{
	public partial class frmSetup : PSA.Lib.Interface.Templates.frmTDialog
	{
		private PSA.Lib.Util.Settings p = new PSA.Lib.Util.Settings();
		private DataTable tblRount = new DataTable("round");
		private bool checkRemote = false;
	
		public frmSetup()
		{
			InitializeComponent();
			
			tblRount.Columns.Add(new DataColumn("code"));
			tblRount.Columns.Add(new DataColumn("name"));
			object[] r = new object[2];
			r[0] = "0";
			r[1] = "Нет округления";
			tblRount.Rows.Add(r);
			r[0] = "5";
			r[1] = "1,01~1,50=1,50; 1,51~1,99=2,00";
			tblRount.Rows.Add(r);
			r[0] = "1";
			r[1] = "1,00~1,25=1,00; 1,26~1,75=1,50; 1,76~1,99=2,00";
			tblRount.Rows.Add(r);
			r[0] = "2";
			r[1] = "1,00~1,45=1,00; 1,46~1,95=1,50; 1,96~1,99=2,00";
			tblRount.Rows.Add(r);
			r[0] = "3";
			r[1] = "1,00~1,15=1,00; 1,16~1,65=1,50; 1,66~1,99=2,00";
			tblRount.Rows.Add(r);
			r[0] = "4";
			r[1] = "1,00~1,49=1,00; 1,50~1,99=2,00";
			tblRount.Rows.Add(r);
			txtSumRound.DataSource = tblRount;
			txtSumRound.DisplayMember = "name";
			txtSumRound.ValueMember = "code";
			
			LoadProperties();
		}

		private void LoadProperties()
		{
			//Photoland.Security.Properties p = new Photoland.Security.Properties();
			

			if (p.Connection_type == "DSN")
				radioDSN.Checked = true;
			if (p.Connection_type == "ConnectionString")
				radioConnectionString.Checked = true;

			txtDSN.Text = p.Db_dsn;
			txtSQLServer.Text = p.Db_server;
			txtSQLUser.Text = p.Db_user;
			txtSQLPassword.Text = p.Db_password;
			txtSQLBase.Text = p.Db_base;

			if (p.Run_one_copy_acceptance)
				checkRunOneCopyAcceptance.Checked = true;
			else
				checkRunOneCopyAcceptance.Checked = false;

			if (p.Run_one_copy_oprator)
				checkRunOneCopyOperator.Checked = true;
			else
				checkRunOneCopyOperator.Checked = false;

			if (p.Run_one_copy_designer)
				checkRunOneCopyDesigner.Checked = true;
			else
				checkRunOneCopyDesigner.Checked = false;

			if (p.Run_one_copy_admin)
				checkRunOneCopyAdmin.Checked = true;
			else
				checkRunOneCopyAdmin.Checked = false;

			txtDirPrint.Text = p.Dir_print;
			txtDirEdit.Text = p.Dir_edit;
			txtListOfFiles.Text = p.List_of_files;

			if (p.Search_SubDir)
				checkSearchSubDir.Checked = true;
			else
				checkSearchSubDir.Checked = false;

			txtDirRescan.Text = p.Dir_rescan.ToString();
			txtTimeForOutput.Text = p.Time_for_output.ToString();
			txtBeginWork.Text = p.Time_begin_work.ToString();
			txtEndWork.Text = p.Time_end_work.ToString();
			txtOrderPrifics.Text = p.Order_prefics.ToString();

			lblQbtn1_id.Text = p.Qbtn01_id.ToString();
			lblQbtn2_id.Text = p.Qbtn02_id.ToString();
			lblQbtn3_id.Text = p.Qbtn03_id.ToString();
			lblQbtn4_id.Text = p.Qbtn04_id.ToString();
			lblQbtn5_id.Text = p.Qbtn05_id.ToString();
			lblQbtn6_id.Text = p.Qbtn06_id.ToString();
			lblQbtn7_id.Text = p.Qbtn07_id.ToString();
			lblQbtn8_id.Text = p.Qbtn08_id.ToString();
			lblQbtn9_id.Text = p.Qbtn09_id.ToString();
			lblQbtn10_id.Text = p.Qbtn10_id.ToString();

			txtQbtn1_string.Text = p.Qbtn01_text;
			txtQbtn2_string.Text = p.Qbtn02_text;
			txtQbtn3_string.Text = p.Qbtn03_text;
			txtQbtn4_string.Text = p.Qbtn04_text;
			txtQbtn5_string.Text = p.Qbtn05_text;
			txtQbtn6_string.Text = p.Qbtn06_text;
			txtQbtn7_string.Text = p.Qbtn07_text;
			txtQbtn8_string.Text = p.Qbtn08_text;
			txtQbtn9_string.Text = p.Qbtn09_text;
			txtQbtn10_string.Text = p.Qbtn10_text;

			lblqbnts1.Text = p.Qbtn01_stext;
			lblqbnts2.Text = p.Qbtn02_stext;
			lblqbnts3.Text = p.Qbtn03_stext;
			lblqbnts4.Text = p.Qbtn04_stext;
			lblqbnts5.Text = p.Qbtn05_stext;
			lblqbnts6.Text = p.Qbtn06_stext;
			lblqbnts7.Text = p.Qbtn07_stext;
			lblqbnts8.Text = p.Qbtn08_stext;
			lblqbnts9.Text = p.Qbtn09_stext;
			lblqbnts10.Text = p.Qbtn10_stext;

			txtUpdateTablePayment.Text = p.UpdatePaymentTable.ToString();
			txtUpdateOrderTableInAcceptance.Text = p.UpdateOrderTableInAcceptance.ToString();

			txtReportFile.Text = p.PathReportsTemplates;

			if (p.Color_rows_in_order)
				checkColorRowsInOrderTable.Checked = true;
			else
				checkColorRowsInOrderTable.Checked = false;

			if (p.Fly_window_operator)
				checkFlyWindowOperator.Checked = true;
			else
				checkFlyWindowOperator.Checked = false;

			if (p.Mod_operator_top_most)
				checkModOperatorTopMost.Checked = true;
			else
				checkModOperatorTopMost.Checked = false;

			txtOrderListOperatorUpdate.Text = p.UpdateOrderTableInOperator.ToString();

			if (p.Fly_window_designer)
				checkFlyWindowDesigner.Checked = true;
			else
				checkFlyWindowDesigner.Checked = false;

			if (p.Mod_designer_top_most)
				checkModDesignerTopMost.Checked = true;
			else
				checkModDesignerTopMost.Checked = false;

			txtOrderListDesignerUpdate.Text = p.UpdateOrderTableInDesigner.ToString();

            txtExport.Text = p.Dir_export;
            txtExportNet.Text = p.Dir_net_export;
            txtImport.Text = p.Dir_import;

			if (p.Robot_animation_icon)
				checkRobotIconAnimation.Checked = true;
			else
				checkRobotIconAnimation.Checked = false;

			if (p.Run_one_copy_robot)
				checkRunOneCopyRobot.Checked = true;
			else
				checkRunOneCopyRobot.Checked = false;

			txtSQLTemplateDCard.Text = p.SQL_Import_Template_DCard;
			txtSQLTemplateGood.Text = p.SQL_Import_Template_Good;
			txtSQLTemplateMashine.Text = p.SQL_Import_Template_Mashine;
			txtSQLTemplateMaterial.Text = p.SQL_Import_Template_Material;

			txtReklamBlock1.Text = p.ReklamBlock1;

			if (p.CheckPreview)
				checkCheckPreview.Checked = true;
			else
				checkCheckPreview.Checked = false;

			txtCheckCount.Text = p.CheckCount.ToString();

			txtPasswordClass1.Text = p.PasswordClass1;

			if (p.AutoSendCrashReport)
				checkAutoSendCrashReport.Checked = true;
			else
				checkAutoSendCrashReport.Checked = false;

			if (p.AutoSendCrashReport)
				checkAutoSendCrashReport.Checked = true;
			else
				checkAutoSendCrashReport.Checked = false;

			if (p.SMTPAut)
				checkSMTPAut.Checked = true;
			else
				checkSMTPAut.Checked = false;

			txtSMTPFromEmail.Text = p.SMTPUserFromEmail;
			txtSMTPName.Text = p.SMTPUserFrom;
			txtSMTPPassword.Text = p.SMTPPasswordFrom;
			txtSMTPServer.Text = p.SMTPServer;
			txtSMTPToEmail.Text = p.SMTPUserTo;

			txtFTPServer.Text = p.FTP_Server;
			txtFTPPath.Text = p.FTP_Path;
			txtFTPUser.Text = p.FTP_User;
			txtFTPPassword.Text = p.FTP_Password;
			txtFTPServerExport.Text = p.FTP_Server_Export;
			txtFTPPathExport.Text = p.FTP_Path_Export;
			txtTimeExport.Text = p.Export_time.ToString();
			txtTimeImport.Text = p.Import_time.ToString();

			if (p.Import_from_ftp)
				checkFtpImport.Checked = true;
			else
				checkFtpImport.Checked = false;

			if (p.Export_from_ftp)
				checkExportFP.Checked = true;
			else
				checkExportFP.Checked = false;

			if (p.ShortGoodsListInWizard)
				checkUseShortListGoods.Checked = true;
			else
				checkUseShortListGoods.Checked = false;

			if (p.ExportDoCopy)
				checkExportDoCopy.Checked = true;
			else
				checkExportDoCopy.Checked = false;

			if (p.ExportClearDirAfterCopy)
				checkClearAfterCopy.Checked = true;
			else
				checkClearAfterCopy.Checked = false;

			txtSumRound.SelectedValue = p.ModelRound.ToString();

			if (p.QueryForDelete)
				checkQueryDelete.Checked = true;
			else
				checkQueryDelete.Checked = false;

			if (p.DenyDelete)
				checkDenyDelete.Checked = true;
			else
				checkDenyDelete.Checked = false;

            if (p.ExportOld)
                checkExportOld.Checked = true;
            else
                checkExportOld.Checked = false;
            
            txtDCardLimit.Text = p.DCard_limit.ToString();
			txtPublicIni.Text = p.PublicIni;

			Rebild();
			
			if(PSA.Lib.Util.Semaphore.semRemoteSettings != "")
			{
				checkRemoteSetup.Checked = true;
				txtRemoteSetupPath.Text = PSA.Lib.Util.Semaphore.semRemoteSettings;
			}
			else
			{
				checkRemoteSetup.Checked = false;
			}
			CheckRemoteSetupAccess();
			checkRemote = true;

            if (p.Run_one_copy_inventory)
                checkRunOneInventory.Checked = true;
            else
                checkRunOneInventory.Checked = false;

            txtDirTmpExport.Text = p.Dir_tmp_export;


			txtPrefixTerminal.Text = p.Order_terminal_prefics;

			checkInventory.Checked = p.SemaphoreInventory;
			checkRound3.Checked = p.Round3;
			checkTerminalClientOne.Checked = p.Terminal_client_one;
			checkTerminalPrintCheck.Checked = p.Terminal_print_check;
			checkTerminalControlWorker.Checked = p.Terminal_control_worker;
			checkUseXmlCache.Checked = p.UseXmlCache;
			checkLoadNotApproved.Checked = p.LoadNotApproved;
			checkLoad1000.Checked = p.Load1000;
			checkShowQuickOrder.Checked = p.ShowQuickOrder;
			txtDiscontServerAddress.Text = p.DiscontServerAddress;
			txtCheckText1.Text = p.CheckString1;
			txtCheckText2.Text = p.CheckString2;
			checkMFoto.Checked = p.ShowMFoto;
			txtDirMfoto.Text = p.MfotoAlbumsPath;
            txtDirAutoImport.Text = p.Dir_auto_import;
            checkExportOld.Checked = p.ExportOld;
            checkDontLockExport.Checked = p.DontLockExported;
            txtPublicKey.Text = p.PublicKey;
            txtPrivateKey.Text = p.PrivateKey;
            txtApiRequestToken.Text = p.ApiRequestToken;
            txtApiAccessToken.Text = p.ApiAccessToken;
            txtApiOrder.Text = p.ApiOrder;
            txtApiOrderItems.Text = p.ApiOrderItems;
            txtApiProducts.Text = p.ApiProducts;

			
		}

		private void Rebild()
		{
			try
			{
				// Если выбран DSN
				if (radioDSN.Checked)
				{
					txtDSN.Enabled = true;
					txtSQLBase.Enabled = false;
					txtSQLPassword.Enabled = false;
					txtSQLServer.Enabled = false;
					txtSQLUser.Enabled = false;
				}

				if (radioConnectionString.Checked)
				{
					txtDSN.Enabled = false;
					txtSQLBase.Enabled = true;
					txtSQLPassword.Enabled = true;
					txtSQLServer.Enabled = true;
					txtSQLUser.Enabled = true;
				}
			}
			catch
			{
				radioDSN.Enabled = false;
				txtDSN.Enabled = false;
				txtSQLBase.Enabled = true;
				txtSQLPassword.Enabled = true;
				txtSQLServer.Enabled = true;
				txtSQLUser.Enabled = true;
			}
		}

		private void SaveProperties()
		{
			//Photoland.Security.Properties p = new Photoland.Security.Properties();
			
			if((checkRemoteSetup.Checked) && (txtRemoteSetupPath.Text != ""))
			{
				PSA.Lib.Util.Semaphore.semRemoteSettings = txtRemoteSetupPath.Text;
			}
			else
			{
				PSA.Lib.Util.Semaphore.semRemoteSettings = "";
			}
			
			
			p = new PSA.Lib.Util.Settings();

			if (radioDSN.Checked == true)
			{
				p.Connection_type = "DSN";
				p.Connection_string = "Dsn=" + txtDSN.Text + ";";
			}
			if (radioConnectionString.Checked == true)
			{
				p.Connection_type = "ConnectionString";
				p.Connection_string = "Data Source=" + txtSQLServer.Text + ";Initial Catalog=" + txtSQLBase.Text + ";Persist Security Info=True;User ID=" + txtSQLUser.Text + ";Password=" + txtSQLPassword.Text + ";";
			}

			p.Db_dsn = txtDSN.Text;
			p.Db_server = txtSQLServer.Text;
			p.Db_user = txtSQLUser.Text;
			p.Db_password = txtSQLPassword.Text;
			p.Db_base = txtSQLBase.Text;

			if (checkRunOneCopyAcceptance.Checked)
				p.Run_one_copy_acceptance = true;
			else
				p.Run_one_copy_acceptance = false;

			if (checkRunOneCopyOperator.Checked)
				p.Run_one_copy_oprator = true;
			else
				p.Run_one_copy_oprator = false;

			if (checkRunOneCopyDesigner.Checked)
				p.Run_one_copy_designer = true;
			else
				p.Run_one_copy_designer = false;

			if (checkRunOneCopyAdmin.Checked)
				p.Run_one_copy_admin = true;
			else
				p.Run_one_copy_admin = false;

			p.Dir_print = txtDirPrint.Text;
			p.Dir_edit = txtDirEdit.Text;
			p.List_of_files = txtListOfFiles.Text;

			if (checkSearchSubDir.Checked)
				p.Search_SubDir = true;
			else
				p.Search_SubDir = false;

			p.Dir_rescan = int.Parse(txtDirRescan.Text);

			p.Time_for_output = int.Parse(txtTimeForOutput.Text);
			p.Time_begin_work = int.Parse(txtBeginWork.Text);
			p.Time_end_work = int.Parse(txtEndWork.Text);
			p.Order_prefics = txtOrderPrifics.Text;

			p.Qbtn01_id = lblQbtn1_id.Text;
			p.Qbtn02_id = lblQbtn2_id.Text;
			p.Qbtn03_id = lblQbtn3_id.Text;
			p.Qbtn04_id = lblQbtn4_id.Text;
			p.Qbtn05_id = lblQbtn5_id.Text;
			p.Qbtn06_id = lblQbtn6_id.Text;
			p.Qbtn07_id = lblQbtn7_id.Text;
			p.Qbtn08_id = lblQbtn8_id.Text;
			p.Qbtn09_id = lblQbtn9_id.Text;
			p.Qbtn10_id = lblQbtn10_id.Text;

			p.Qbtn01_text = txtQbtn1_string.Text;
			p.Qbtn02_text = txtQbtn2_string.Text;
			p.Qbtn03_text = txtQbtn3_string.Text;
			p.Qbtn04_text = txtQbtn4_string.Text;
			p.Qbtn05_text = txtQbtn5_string.Text;
			p.Qbtn06_text = txtQbtn6_string.Text;
			p.Qbtn07_text = txtQbtn7_string.Text;
			p.Qbtn08_text = txtQbtn8_string.Text;
			p.Qbtn09_text = txtQbtn9_string.Text;
			p.Qbtn10_text = txtQbtn10_string.Text;

			p.Qbtn01_stext = lblqbnts1.Text;
			p.Qbtn02_stext = lblqbnts2.Text;
			p.Qbtn03_stext = lblqbnts3.Text;
			p.Qbtn04_stext = lblqbnts4.Text;
			p.Qbtn05_stext = lblqbnts5.Text;
			p.Qbtn06_stext = lblqbnts6.Text;
			p.Qbtn07_stext = lblqbnts7.Text;
			p.Qbtn08_stext = lblqbnts8.Text;
			p.Qbtn09_stext = lblqbnts9.Text;
			p.Qbtn10_stext = lblqbnts10.Text;

			p.UpdatePaymentTable = int.Parse(txtUpdateTablePayment.Text);
			p.UpdateOrderTableInAcceptance = int.Parse(txtUpdateOrderTableInAcceptance.Text);

			p.PathReportsTemplates = txtReportFile.Text;

			if (checkColorRowsInOrderTable.Checked)
				p.Color_rows_in_order = true;
			else
				p.Color_rows_in_order = false;

			if (checkFlyWindowOperator.Checked)
				p.Fly_window_operator = true;
			else
				p.Fly_window_operator = false;

			if (checkModOperatorTopMost.Checked)
				p.Mod_operator_top_most = true;
			else
				p.Mod_operator_top_most = false;

			p.UpdateOrderTableInOperator = int.Parse(txtOrderListOperatorUpdate.Text);

			if (checkFlyWindowDesigner.Checked)
				p.Fly_window_designer = true;
			else
				p.Fly_window_designer = false;

			if (checkModDesignerTopMost.Checked)
				p.Mod_designer_top_most = true;
			else
				p.Mod_designer_top_most = false;

			p.UpdateOrderTableInDesigner = int.Parse(txtOrderListDesignerUpdate.Text);

			p.Dir_import = txtImport.Text;
            p.Dir_export = txtExport.Text;
            p.Dir_net_export = txtExportNet.Text;

			if (checkRobotIconAnimation.Checked)
				p.Robot_animation_icon = true;
			else
				p.Robot_animation_icon = false;

			if (checkRunOneCopyRobot.Checked)
				p.Run_one_copy_robot = true;
			else
				p.Run_one_copy_robot = false;

			p.SQL_Import_Template_DCard = txtSQLTemplateDCard.Text;
			p.SQL_Import_Template_Good = txtSQLTemplateGood.Text;
			p.SQL_Import_Template_Mashine = txtSQLTemplateMashine.Text;
			p.SQL_Import_Template_Material = txtSQLTemplateMaterial.Text;
			p.ReklamBlock1 = txtReklamBlock1.Text;

			if (checkCheckPreview.Checked)
				p.CheckPreview = true;
			else
				p.CheckPreview = false;

			p.CheckCount = int.Parse(txtCheckCount.Text);

			p.PasswordClass1 = txtPasswordClass1.Text;

			if (checkAutoSendCrashReport.Checked)
				p.AutoSendCrashReport = true;
			else
				p.AutoSendCrashReport = false;

			if (checkSMTPAut.Checked)
				p.SMTPAut = true;
			else
				p.SMTPAut = false;

			p.SMTPPasswordFrom = txtSMTPPassword.Text;
			p.SMTPServer = txtSMTPServer.Text;
			p.SMTPUserFrom = txtSMTPName.Text;
			p.SMTPUserFromEmail = txtSMTPFromEmail.Text;
			p.SMTPUserTo = txtSMTPToEmail.Text;

			p.FTP_Server = txtFTPServer.Text;
			p.FTP_Path = txtFTPPath.Text;
			p.FTP_User = txtFTPUser.Text;
			p.FTP_Password = txtFTPPassword.Text;
			p.FTP_Path_Export = txtFTPPathExport.Text;
			p.FTP_Server_Export = txtFTPServerExport.Text;
			p.Import_time = int.Parse(txtTimeImport.Text);
			p.Export_time = int.Parse(txtTimeExport.Text);

			if (checkFtpImport.Checked)
				p.Import_from_ftp = true;
			else
				p.Import_from_ftp = false;

			if (checkExportFP.Checked)
				p.Export_from_ftp = true;
			else
				p.Export_from_ftp = false;

			if (checkUseShortListGoods.Checked)
				p.ShortGoodsListInWizard = true;
			else
				p.ShortGoodsListInWizard = false;

			if (checkExportDoCopy.Checked)
				p.ExportDoCopy = true;
			else
				p.ExportDoCopy = false;

			if (checkClearAfterCopy.Checked)
				p.ExportClearDirAfterCopy = true;
			else
				p.ExportClearDirAfterCopy = false;

			p.ModelRound = int.Parse(txtSumRound.SelectedValue.ToString());

			if (checkQueryDelete.Checked)
				p.QueryForDelete = true;
			else
				p.QueryForDelete = false;

			if (checkDenyDelete.Checked)
				p.DenyDelete = true;
			else
				p.DenyDelete = false;

            if (checkExportOld.Checked)
                p.ExportOld = true;
            else
                p.ExportOld = false;


			p.DCard_limit = int.Parse(txtDCardLimit.Text);
			p.PublicIni = txtPublicIni.Text;



            if (checkRunOneInventory.Checked)
                p.Run_one_copy_inventory = true;
            else
                p.Run_one_copy_inventory = false;

			p.Order_terminal_prefics = txtPrefixTerminal.Text;

			p.SemaphoreInventory = checkInventory.Checked;

            p.Dir_tmp_export = txtDirTmpExport.Text;
			p.Round3 = checkRound3.Checked;
			p.Terminal_client_one = checkTerminalClientOne.Checked;
			p.Terminal_print_check = checkTerminalPrintCheck.Checked;
			p.Terminal_control_worker = checkTerminalControlWorker.Checked;
			p.UseXmlCache = checkUseXmlCache.Checked;
			p.LoadNotApproved = checkLoadNotApproved.Checked;
			p.Load1000 = checkLoad1000.Checked;
			p.ShowQuickOrder = checkShowQuickOrder.Checked;
			p.DiscontServerAddress = txtDiscontServerAddress.Text;
			p.CheckString1 = txtCheckText1.Text;
			p.CheckString2 = txtCheckText2.Text;
			p.ShowMFoto = checkMFoto.Checked;
			p.MfotoAlbumsPath = txtDirMfoto.Text;

            p.ExportOld = checkExportOld.Checked;
            p.DontLockExported = checkDontLockExport.Checked;

            p.PublicKey = txtPublicKey.Text;
            p.PrivateKey = txtPrivateKey.Text;
            p.ApiRequestToken = txtApiRequestToken.Text;
            p.ApiAccessToken = txtApiAccessToken.Text;
            p.ApiOrder = txtApiOrder.Text;
            p.ApiOrderItems = txtApiOrderItems.Text;
            p.ApiProducts = txtApiProducts.Text;

            p.Dir_auto_import = txtDirAutoImport.Text;
            if(!p.Save())
				MessageBox.Show("Ошибка при сохранении настроек!", "Настройки программы", MessageBoxButtons.OK, MessageBoxIcon.Warning);

		}

		private void BildDSNList()
		{
			try
			{
				txtDSN.Items.Clear();
				RegistryKey key;
				key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\ODBC\\ODBC.INI\\ODBC Data Sources");
				foreach (string keyName in key.GetValueNames())
				{
					txtDSN.Items.Add(keyName);
				}
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				radioDSN.Enabled = false;
				txtDSN.Enabled = false;
				txtSQLBase.Enabled = true;
				txtSQLPassword.Enabled = true;
				txtSQLServer.Enabled = true;
				txtSQLUser.Enabled = true;
			}
		}

		private void frmSetup_Load(object sender, EventArgs e)
		{
			this.Title = "Настройки программы";
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			SaveProperties();
		}

		private void radioDSN_CheckedChanged(object sender, EventArgs e)
		{
			Rebild();
		}

		private void radioConnectionString_CheckedChanged(object sender, EventArgs e)
		{
			Rebild();
		}

		private void btnDirPrint_Click(object sender, EventArgs e)
		{
			dlg.ShowDialog();
			if (dlg.SelectedPath != "")
				txtDirPrint.Text = dlg.SelectedPath;
		}

		private void btnDirEdit_Click(object sender, EventArgs e)
		{
			dlg.ShowDialog();
			if (dlg.SelectedPath != "")
				txtDirEdit.Text = dlg.SelectedPath;
		}

		private void btnCheckDBConnection_Click(object sender, EventArgs e)
		{
			SqlConnection t_con = new SqlConnection();
			if (radioDSN.Checked == true)
			{
				t_con.ConnectionString = "Data Source=" + txtDSN.Text + ";";
			}
			if (radioConnectionString.Checked == true)
			{
				t_con.ConnectionString = "Data Source=" + txtSQLServer.Text + ";Initial Catalog=" + txtSQLBase.Text + ";Persist Security Info=True;User ID=" + txtSQLUser.Text + ";Password=" + txtSQLPassword.Text + ";";
			}
			try
			{
				t_con.Open();
				MessageBox.Show("Соединение установлено!", "Тестовое соединение", MessageBoxButtons.OK, MessageBoxIcon.Information);
				t_con.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Ошибка соединения с базой данных!\n" + ex.Message + "\n" + ex.Source, "Тестовое соединение", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

		}

		private void btnQbtn1_clear_Click(object sender, EventArgs e)
		{
			txtQbtn1_string.Text = "";
			lblQbtn1_id.Text = "0";
			lblqbnts1.Text = "";
		}

		private void btnQbtn2_clear_Click(object sender, EventArgs e)
		{
			txtQbtn2_string.Text = "";
			lblQbtn2_id.Text = "0";
			lblqbnts2.Text = "";
		}

		private void btnQbtn3_clear_Click(object sender, EventArgs e)
		{
			txtQbtn3_string.Text = "";
			lblQbtn3_id.Text = "0";
			lblqbnts3.Text = "";
		}

		private void btnQbtn4_clear_Click(object sender, EventArgs e)
		{
			txtQbtn4_string.Text = "";
			lblQbtn4_id.Text = "0";
			lblqbnts4.Text = "";
		}

		private void btnQbtn5_clear_Click(object sender, EventArgs e)
		{
			txtQbtn5_string.Text = "";
			lblQbtn5_id.Text = "0";
			lblqbnts5.Text = "";
		}

		private void btnQbtn6_clear_Click(object sender, EventArgs e)
		{
			txtQbtn6_string.Text = "";
			lblQbtn6_id.Text = "0";
			lblqbnts6.Text = "";
		}

		private void btnQbtn7_clear_Click(object sender, EventArgs e)
		{
			txtQbtn7_string.Text = "";
			lblQbtn7_id.Text = "0";
			lblqbnts7.Text = "";
		}

		private void btnQbtn8_clear_Click(object sender, EventArgs e)
		{
			txtQbtn8_string.Text = "";
			lblQbtn8_id.Text = "0";
			lblqbnts8.Text = "";
		}

		private void btnQbtn9_clear_Click(object sender, EventArgs e)
		{
			txtQbtn9_string.Text = "";
			lblQbtn9_id.Text = "0";
			lblqbnts9.Text = "";
		}

		private void btnQbtn10_clear_Click(object sender, EventArgs e)
		{
			txtQbtn10_string.Text = "";
			lblQbtn10_id.Text = "0";
			lblqbnts10.Text = "";
		}

		private void btnSelectQBtn1_Click(object sender, EventArgs e)
		{
			try
			{
				//Photoland.Security.Properties p = new Photoland.Security.Properties();
				frmSelectService fsel = new frmSelectService(new SqlConnection(p.Connection_string), false, "");
				fsel.ShowDialog();
				if (fsel.DialogResult == DialogResult.OK)
				{
					lblQbtn1_id.Text = fsel.id_serv.ToString();
					txtQbtn1_string.Text = fsel.text_serv;
					lblqbnts1.Text = fsel.stext_serv;
				}
				fsel.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Необходимо установить соединение с бызой данных!\nПроверьте настройки, сохраните их попробуйте снова.");
			}
		}

		private void btnSelectQBtn2_Click(object sender, EventArgs e)
		{
			try
			{
				//Photoland.Security.Properties p = new Photoland.Security.Properties();
				frmSelectService fsel = new frmSelectService(new SqlConnection(p.Connection_string), false, "");
				fsel.ShowDialog();
				if (fsel.DialogResult == DialogResult.OK)
				{
					lblQbtn2_id.Text = fsel.id_serv.ToString();
					txtQbtn2_string.Text = fsel.text_serv;
					lblqbnts2.Text = fsel.stext_serv;
				}
				fsel.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Необходимо установить соединение с бызой данных!\nПроверьте настройки, сохраните их попробуйте снова.");
			}
		}

		private void btnSelectQBtn3_Click(object sender, EventArgs e)
		{
			try
			{
				//Photoland.Security.Properties p = new Photoland.Security.Properties();
				frmSelectService fsel = new frmSelectService(new SqlConnection(p.Connection_string), false, "");
				fsel.ShowDialog();
				if (fsel.DialogResult == DialogResult.OK)
				{
					lblQbtn3_id.Text = fsel.id_serv.ToString();
					txtQbtn3_string.Text = fsel.text_serv;
					lblqbnts3.Text = fsel.stext_serv;
				}
				fsel.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Необходимо установить соединение с бызой данных!\nПроверьте настройки, сохраните их попробуйте снова.");
			}
		}

		private void btnSelectQBtn4_Click(object sender, EventArgs e)
		{
			try
			{
				//Photoland.Security.Properties p = new Photoland.Security.Properties();
				frmSelectService fsel = new frmSelectService(new SqlConnection(p.Connection_string), false, "");
				fsel.ShowDialog();
				if (fsel.DialogResult == DialogResult.OK)
				{
					lblQbtn4_id.Text = fsel.id_serv.ToString();
					txtQbtn4_string.Text = fsel.text_serv;
					lblqbnts4.Text = fsel.stext_serv;
				}
				fsel.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Необходимо установить соединение с бызой данных!\nПроверьте настройки, сохраните их попробуйте снова.");
			}
		}

		private void btnSelectQBtn5_Click(object sender, EventArgs e)
		{
			try
			{
				//Photoland.Security.Properties p = new Photoland.Security.Properties();
				frmSelectService fsel = new frmSelectService(new SqlConnection(p.Connection_string), false, "");
				fsel.ShowDialog();
				if (fsel.DialogResult == DialogResult.OK)
				{
					lblQbtn5_id.Text = fsel.id_serv.ToString();
					txtQbtn5_string.Text = fsel.text_serv;
					lblqbnts5.Text = fsel.stext_serv;
				}
				fsel.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Необходимо установить соединение с бызой данных!\nПроверьте настройки, сохраните их попробуйте снова.");
			}
		}

		private void btnSelectQBtn6_Click(object sender, EventArgs e)
		{
			try
			{
				//Photoland.Security.Properties p = new Photoland.Security.Properties();
				frmSelectService fsel = new frmSelectService(new SqlConnection(p.Connection_string), false, "");
				fsel.ShowDialog();
				if (fsel.DialogResult == DialogResult.OK)
				{
					lblQbtn6_id.Text = fsel.id_serv.ToString();
					txtQbtn6_string.Text = fsel.text_serv;
					lblqbnts6.Text = fsel.stext_serv;
				}
				fsel.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Необходимо установить соединение с бызой данных!\nПроверьте настройки, сохраните их попробуйте снова.");
			}
		}

		private void btnSelectQBtn7_Click(object sender, EventArgs e)
		{
			try
			{
				//Photoland.Security.Properties p = new Photoland.Security.Properties();
				frmSelectService fsel = new frmSelectService(new SqlConnection(p.Connection_string), false, "");
				fsel.ShowDialog();
				if (fsel.DialogResult == DialogResult.OK)
				{
					lblQbtn7_id.Text = fsel.id_serv.ToString();
					txtQbtn7_string.Text = fsel.text_serv;
					lblqbnts7.Text = fsel.stext_serv;
				}
				fsel.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Необходимо установить соединение с бызой данных!\nПроверьте настройки, сохраните их попробуйте снова.");
			}
		}

		private void btnSelectQBtn8_Click(object sender, EventArgs e)
		{
			try
			{
				//Photoland.Security.Properties p = new Photoland.Security.Properties();
				frmSelectService fsel = new frmSelectService(new SqlConnection(p.Connection_string), false, "");
				fsel.ShowDialog();
				if (fsel.DialogResult == DialogResult.OK)
				{
					lblQbtn8_id.Text = fsel.id_serv.ToString();
					txtQbtn8_string.Text = fsel.text_serv;
					lblqbnts8.Text = fsel.stext_serv;
				}
				fsel.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Необходимо установить соединение с бызой данных!\nПроверьте настройки, сохраните их попробуйте снова.");
			}
		}

		private void btnSelectQBtn9_Click(object sender, EventArgs e)
		{
			try
			{
				//Photoland.Security.Properties p = new Photoland.Security.Properties();
				frmSelectService fsel = new frmSelectService(new SqlConnection(p.Connection_string), false, "");
				fsel.ShowDialog();
				if (fsel.DialogResult == DialogResult.OK)
				{
					lblQbtn9_id.Text = fsel.id_serv.ToString();
					txtQbtn9_string.Text = fsel.text_serv;
					lblqbnts9.Text = fsel.stext_serv;
				}
				fsel.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Необходимо установить соединение с бызой данных!\nПроверьте настройки, сохраните их попробуйте снова.");
			}
		}

		private void btnSelectQBtn10_Click(object sender, EventArgs e)
		{
			try
			{
				//Photoland.Security.Properties p = new Photoland.Security.Properties();
				frmSelectService fsel = new frmSelectService(new SqlConnection(p.Connection_string), false, "");
				fsel.ShowDialog();
				if (fsel.DialogResult == DialogResult.OK)
				{
					lblQbtn10_id.Text = fsel.id_serv.ToString();
					txtQbtn10_string.Text = fsel.text_serv;
					lblqbnts10.Text = fsel.stext_serv;
				}
				fsel.Close();
			}
			catch (Exception ex)
			{
				//ErrorNfo.WriteErrorInfo(ex);
				MessageBox.Show("Необходимо установить соединение с бызой данных!\nПроверьте настройки, сохраните их попробуйте снова.");
			}
		}

		private void checkDebugIdForQBtn_CheckedChanged(object sender, EventArgs e)
		{
			if (checkDebugIdForQBtn.Checked)
			{
				lblQbtn1_id.Visible = true;
				lblQbtn2_id.Visible = true;
				lblQbtn3_id.Visible = true;
				lblQbtn4_id.Visible = true;
				lblQbtn5_id.Visible = true;
				lblQbtn6_id.Visible = true;
				lblQbtn7_id.Visible = true;
				lblQbtn8_id.Visible = true;
				lblQbtn9_id.Visible = true;
				lblQbtn10_id.Visible = true;

				lblqbnts1.Visible = true;
				lblqbnts2.Visible = true;
				lblqbnts3.Visible = true;
				lblqbnts4.Visible = true;
				lblqbnts5.Visible = true;
				lblqbnts6.Visible = true;
				lblqbnts7.Visible = true;
				lblqbnts8.Visible = true;
				lblqbnts9.Visible = true;
				lblqbnts10.Visible = true;
			}
			else
			{
				lblQbtn1_id.Visible = false;
				lblQbtn2_id.Visible = false;
				lblQbtn3_id.Visible = false;
				lblQbtn4_id.Visible = false;
				lblQbtn5_id.Visible = false;
				lblQbtn6_id.Visible = false;
				lblQbtn7_id.Visible = false;
				lblQbtn8_id.Visible = false;
				lblQbtn9_id.Visible = false;
				lblQbtn10_id.Visible = false;

				lblqbnts1.Visible = false;
				lblqbnts2.Visible = false;
				lblqbnts3.Visible = false;
				lblqbnts4.Visible = false;
				lblqbnts5.Visible = false;
				lblqbnts6.Visible = false;
				lblqbnts7.Visible = false;
				lblqbnts8.Visible = false;
				lblqbnts9.Visible = false;
				lblqbnts10.Visible = false;

			}
		}

		private void btnSelectReportFile_Click(object sender, EventArgs e)
		{
			odlg.Title = "Шаблон отчетов";
			odlg.ShowDialog();
			if (odlg.FileName != "")
			{
				txtReportFile.Text = odlg.FileName;
			}
		}

		private void btnSelectImportDir_Click(object sender, EventArgs e)
		{
			dlg.ShowDialog();
			if (dlg.SelectedPath != "")
				txtImport.Text = dlg.SelectedPath;
		}

		private void btnSelectExportDir_Click(object sender, EventArgs e)
		{
			dlg.ShowDialog();
			if (dlg.SelectedPath != "")
				txtExport.Text = dlg.SelectedPath;
		}

		private void btnDoCrash_Click(object sender, EventArgs e)
		{
			if (MessageBox.Show("Вы уверены?", "Сбой!", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
			{
				int i = 0;
				int j = 100 / i;
			}
		}
		
		private void CheckRemoteSetupAccess()
		{
			if(checkRemoteSetup.Checked)
			{
				txtRemoteSetupPath.Enabled = true;
				btnRemotePath.Enabled = true;
				if (checkRemote)
				{
					if (checkRemoteSetup.Checked == true)
					{
						frmDialogYesNo f = new frmDialogYesNo();
						f.Title = "Включение семафора";
						f.Message = "Внимание! В приключении этого семафора все настройки будут сохранены в указанной папке, если они были сохранены там ранне, то файл настроек будут перезаписан и даные будут утеряны! Для ключения семафора выполните \"Сервис\" -> \"Семафоры\"\nВклчить семафор и перезаписать настройки?";
						f.ShowDialog();
						if (f.DialogResult == DialogResult.No)
						{
							checkRemoteSetup.Checked = false;
						}
					}
				}
			
			}
			else
			{
				txtRemoteSetupPath.Enabled = false;
				btnRemotePath.Enabled = false;
			}
		}

		private void checkRemoteSetup_CheckedChanged(object sender, EventArgs e)
		{
			CheckRemoteSetupAccess();
		}

		private void btnRemotePath_Click(object sender, EventArgs e)
		{
			dlg.ShowDialog();
			if (dlg.SelectedPath != "")
				txtRemoteSetupPath.Text = dlg.SelectedPath;
		}

		private void btnKiosk_Click(object sender, EventArgs e)
		{
			using (frmKioskList f = new frmKioskList())
			{
				f.ShowDialog();
			}
		}

        private void btnDirTmpExport_Click(object sender, EventArgs e)
        {
            dlg.ShowDialog();
            if (dlg.SelectedPath != "")
                txtDirTmpExport.Text = dlg.SelectedPath;
        }

		private void btnDirMFoto_Click(object sender, EventArgs e)
		{
			dlg.ShowDialog();
			if (dlg.SelectedPath != "")
				txtDirMfoto.Text = dlg.SelectedPath;
		}

        private void btnAutoImport_Click(object sender, EventArgs e)
        {
            dlg.ShowDialog();
            if (dlg.SelectedPath != "")
                txtDirAutoImport.Text = dlg.SelectedPath;
        }

        private void btnSelectExportDirNet_Click(object sender, EventArgs e)
        {
            dlg.ShowDialog();
            if (dlg.SelectedPath != "")
                txtExportNet.Text = dlg.SelectedPath;
        }
		
	}
}
