using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PSA.Lib.Util
{
	public partial class Settings
	{
		public Settings()
		{
			string iniFile = Semaphore.semRemoteSettings + "\\settings.ini";
			if(iniFile == "\\settings.ini")
			{
				iniFile = System.Environment.GetCommandLineArgs()[0].Substring(
				0,
				System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
				) + "\\settings.ini";
				if (!File.Exists(System.Environment.GetCommandLineArgs()[0].Substring(
				0,
				System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
				) + "\\settings.ini"))
					File.Create(System.Environment.GetCommandLineArgs()[0].Substring(
				0,
				System.Environment.GetCommandLineArgs()[0].LastIndexOf('\\')
				) + "\\settings.ini");
			}
			
			f = new ini(iniFile); 
			
			_Connection_type = f.IniReadValue("DB", "Connection_type", "ConnectionString"); //string 
			_Db_dsn = f.IniReadValue("DB", "Db_dsn", ""); //string 
			_Db_server = f.IniReadValue("DB", "Db_server", "svr\\sqlexpress"); //string 
			_Db_user = f.IniReadValue("DB", "Db_user", "sa"); //string 
			_Db_password = f.IniReadValue("DB", "Db_password", "solaris"); //string 
			_Db_base = f.IniReadValue("DB", "Db_base", "PSAv1"); //string 
			_Connection_string = f.IniReadValue("DB", "Connection_string", ""); //string 
            _Block_inventory = bool.Parse(f.IniReadValue("Inventory", "Block_inventory", "true")); //bool 
            _Run_one_copy_acceptance = bool.Parse(f.IniReadValue("Run", "Run_one_copy_acceptance", "true")); //bool 
            _Run_one_copy_inventory = bool.Parse(f.IniReadValue("Run", "Run_one_copy_inventory", "true")); //bool 
            _Run_one_copy_oprator = bool.Parse(f.IniReadValue("Run", "Run_one_copy_oprator", "true")); //bool 
			_Run_one_copy_designer = bool.Parse(f.IniReadValue("Run", "Run_one_copy_designer", "true")); //bool 
			_Run_one_copy_admin = bool.Parse(f.IniReadValue("Run", "Run_one_copy_admin", "true")); //bool 
			_Run_one_copy_robot = bool.Parse(f.IniReadValue("Run", "Run_one_copy_robot", "true")); //bool 
			_Dir_print = f.IniReadValue("Main", "Dir_print", ""); //string 
            _Dir_edit = f.IniReadValue("Main", "Dir_edit", ""); //string 
            _Dir_tmp_export = f.IniReadValue("Main", "Dir_tmp_export", ""); //string 
			_Search_SubDir = bool.Parse(f.IniReadValue("Main", "Search_SubDir", "true")); //bool 
			_List_of_files = f.IniReadValue("Main", "List_of_files", "jpg, jpeg, bmp, gif, tif"); //string 
			_Dir_rescan = int.Parse(f.IniReadValue("Main", "Dir_rescan", "5")); //int 
			_Time_for_output = int.Parse(f.IniReadValue("Main", "Time_for_output", "1")); //int 
			_Time_begin_work = int.Parse(f.IniReadValue("Main", "Time_begin_work", "8")); //int 
			_Time_end_work = int.Parse(f.IniReadValue("Main", "Time_end_work", "22")); //int 
			_Order_terminal_prefics = f.IniReadValue("Main", "Order_terminal_prefics", "40"); //string 
			_Order_prefics = f.IniReadValue("Main", "Order_prefics", "00"); //string 
			_Qbtn01_id = f.IniReadValue("Form", "Qbtn01_id", ""); //string 
			_Qbtn01_text = f.IniReadValue("Form", "Qbtn01_text", ""); //string 
			_Qbtn02_id = f.IniReadValue("Form", "Qbtn02_id", ""); //string 
			_Qbtn02_text = f.IniReadValue("Form", "Qbtn02_text", ""); //string 
			_Qbtn03_id = f.IniReadValue("Form", "Qbtn03_id", ""); //string 
			_Qbtn03_text = f.IniReadValue("Form", "Qbtn03_text", ""); //string 
			_Qbtn04_id = f.IniReadValue("Form", "Qbtn04_id", ""); //string 
			_Qbtn04_text = f.IniReadValue("Form", "Qbtn04_text", ""); //string 
			_Qbtn05_id = f.IniReadValue("Form", "Qbtn05_id", ""); //string 
			_Qbtn05_text = f.IniReadValue("Form", "Qbtn05_text", ""); //string 
			_Qbtn06_id = f.IniReadValue("Form", "Qbtn06_id", ""); //string 
			_Qbtn06_text = f.IniReadValue("Form", "Qbtn06_text", ""); //string 
			_Qbtn07_id = f.IniReadValue("Form", "Qbtn07_id", ""); //string 
			_Qbtn07_text = f.IniReadValue("Form", "Qbtn07_text", ""); //string 
			_Qbtn08_id = f.IniReadValue("Form", "Qbtn08_id", ""); //string 
			_Qbtn08_text = f.IniReadValue("Form", "Qbtn08_text", ""); //string 
			_Qbtn09_id = f.IniReadValue("Form", "Qbtn09_id", ""); //string 
			_Qbtn09_text = f.IniReadValue("Form", "Qbtn09_text", ""); //string 
			_Qbtn10_id = f.IniReadValue("Form", "Qbtn10_id", ""); //string 
			_Qbtn10_text = f.IniReadValue("Form", "Qbtn10_text", ""); //string 
			_Qbtn01_stext = f.IniReadValue("Form", "Qbtn01_stext", ""); //string 
			_Qbtn02_stext = f.IniReadValue("Form", "Qbtn02_stext", ""); //string 
			_Qbtn03_stext = f.IniReadValue("Form", "Qbtn03_stext", ""); //string 
			_Qbtn04_stext = f.IniReadValue("Form", "Qbtn04_stext", ""); //string 
			_Qbtn05_stext = f.IniReadValue("Form", "Qbtn05_stext", ""); //string 
			_Qbtn06_stext = f.IniReadValue("Form", "Qbtn06_stext", ""); //string 
			_Qbtn07_stext = f.IniReadValue("Form", "Qbtn07_stext", ""); //string 
			_Qbtn08_stext = f.IniReadValue("Form", "Qbtn08_stext", ""); //string 
			_Qbtn09_stext = f.IniReadValue("Form", "Qbtn09_stext", ""); //string 
			_Qbtn10_stext = f.IniReadValue("Form", "Qbtn10_stext", ""); //string 
			_UpdatePaymentTable = int.Parse(f.IniReadValue("Main", "UpdatePaymentTable", "30")); //int 
			_UpdateOrderTableInAcceptance = int.Parse(f.IniReadValue("Main", "UpdateOrderTableInAcceptance", "30")); //int 
			_PathReportsTemplates = f.IniReadValue("Main", "PathReportsTemplates", ""); //string 
			_Color_rows_in_order = bool.Parse(f.IniReadValue("Main", "Color_rows_in_order", "false")); //bool 
			_Fly_window_operator = bool.Parse(f.IniReadValue("Main", "Fly_window_operator", "true")); //bool 
			_Mod_operator_top_most = bool.Parse(f.IniReadValue("Main", "Mod_operator_top_most", "true")); //bool 
			_UpdateOrderTableInOperator = int.Parse(f.IniReadValue("Main", "UpdateOrderTableInOperator", "60")); //int 
			_Mod_designer_top_most = bool.Parse(f.IniReadValue("Main", "Mod_designer_top_most", "true")); //bool 
			_UpdateOrderTableInDesigner = int.Parse(f.IniReadValue("Main", "UpdateOrderTableInDesigner", "60")); //int 
			_Fly_window_designer = bool.Parse(f.IniReadValue("Main", "Fly_window_designer", "true")); //bool 
			_Dir_import = f.IniReadValue("Main", "Dir_import", ""); //string 
            _Dir_export = f.IniReadValue("Main", "Dir_export", ""); //string 
            _Dir_net_export = f.IniReadValue("Main", "Dir_net_export", ""); //string 
            _Robot_animation_icon = bool.Parse(f.IniReadValue("Robot", "Robot_animation_icon", "true")); //bool 
			_SQL_Import_Template_Mashine = f.IniReadValue("Robot", "SQL_Import_Template_Mashine", ""); //string 
			_SQL_Import_Template_DCard = f.IniReadValue("Robot", "SQL_Import_Template_DCard", ""); //string 
			_SQL_Import_Template_Material = f.IniReadValue("Robot", "SQL_Import_Template_Material", ""); //string 
			_SQL_Import_Template_Good = f.IniReadValue("Robot", "SQL_Import_Template_Good", ""); //string 
			_ReklamBlock1 = f.IniReadValue("Main", "ReklamBlock1", "").Replace("<br>", "\n"); //string 
			_CheckPreview = bool.Parse(f.IniReadValue("Main", "CheckPreview", "false")); //bool 
			_CheckCount = int.Parse(f.IniReadValue("Main", "CheckCount", "2")); //int 
			_PasswordClass1 = f.IniReadValue("Main", "PasswordClass1", ""); //string 
			_SMTPServer = f.IniReadValue("Debug", "SMTPServer", "int.fotoland.ru"); //string 
			_SMTPAut = bool.Parse(f.IniReadValue("Debug", "SMTPAut", "true")); //bool 
			_SMTPUserFrom = f.IniReadValue("Debug", "SMTPUserFrom", "PSA Error reporter"); //string 
			_SMTPUserFromEmail = f.IniReadValue("Debug", "SMTPUserFromEmail", "psa@fotoland.ru"); //string 
			_SMTPPasswordFrom = f.IniReadValue("Debug", "SMTPPasswordFrom", "psa1029"); //string 
			_SMTPUserTo = f.IniReadValue("Debug", "SMTPUserTo", "psa@fotoland.ru"); //string 
			_AutoSendCrashReport = bool.Parse(f.IniReadValue("Debug", "AutoSendCrashReport", "false")); //bool 
			_FTP_Server = f.IniReadValue("Robot", "FTP_Server", "int.fotoland.ru"); //string 
			_FTP_Server_Export = f.IniReadValue("Robot", "FTP_Server_Export", "int.fotoland.ru"); //string 
			_FTP_User = f.IniReadValue("Robot", "FTP_User", "anonynous"); //string 
			_FTP_Password = f.IniReadValue("Robot", "FTP_Password", "name@server.ru"); //string 
			_FTP_Path = f.IniReadValue("Robot", "FTP_Path", "PSA/XXXXX/Import"); //string 
			_FTP_Path_Export = f.IniReadValue("Robot", "FTP_Path_Export", "PSA/XXXXX/Export"); //string 
			_Export_from_ftp = bool.Parse(f.IniReadValue("Robot", "Export_from_ftp", "true")); //bool 
			_Import_from_ftp = bool.Parse(f.IniReadValue("Robot", "Import_from_ftp", "true")); //bool 
			_Import_time = int.Parse(f.IniReadValue("Robot", "Import_time", "60")); //int 
			_Export_time = int.Parse(f.IniReadValue("Robot", "Export_time", "60")); //int 
			_ShortGoodsListInWizard = bool.Parse(f.IniReadValue("Main", "ShortGoodsListInWizard", "true")); //bool 
			_ExportDoCopy = bool.Parse(f.IniReadValue("Robot", "ExportDoCopy", "true")); //bool 
			_ExportClearDirAfterCopy = bool.Parse(f.IniReadValue("Robot", "ExportClearDirAfterCopy", "true")); //bool 
			_ModelRound = int.Parse(f.IniReadValue("Main", "ModelRound", "5")); //int 
			_QueryForDelete = bool.Parse(f.IniReadValue("Debug", "QueryForDelete", "true")); //bool 
			_DenyDelete = bool.Parse(f.IniReadValue("Debug", "DenyDelete", "true")); //bool 
			_DCard_limit = int.Parse(f.IniReadValue("Main", "DCard_limit", "2")); //int 
			_PublicIni = f.IniReadValue("Main", "PublicIni", ""); //string 
			_SemaphoreInventory = bool.Parse(f.IniReadValue("Semaphore", "SemaphoreInventory", "false")); //bool
			_Round3 = bool.Parse(f.IniReadValue("Main", "Round3", "false")); //bool
			_Terminal_client_one = bool.Parse(f.IniReadValue("Terminal", "TerminalClient", "false")); //bool
			_Terminal_print_check = bool.Parse(f.IniReadValue("Terminal", "TerminalPrintCheck", "true")); //bool
			_Terminal_control_worker = bool.Parse(f.IniReadValue("Terminal", "TerminalControlWorker", "true")); //bool
			_UseXmlCache = bool.Parse(f.IniReadValue("Terminal", "UseXmlCache", "false")); //bool
			_LoadNotApproved = bool.Parse(f.IniReadValue("Terminal", "LoadNotApproved", "false")); //bool
			_Load1000 = bool.Parse(f.IniReadValue("Terminal", "Load1000", "true")); //bool
			_ShowQuickOrder = bool.Parse(f.IniReadValue("Main", "ShowQuickOrder", "false")); //bool
			_ShowMFoto = bool.Parse(f.IniReadValue("Main", "ShowMFoto", "false")); //bool
			_DiscontServerAddress = f.IniReadValue("Main", "DiscontServerAddress", "k.fotoland.ru"); //staring
			_checkString1 = f.IniReadValue("Main", "checkString1", "").Replace("<br>", "\n"); //staring
			_checkString2 = f.IniReadValue("Main", "checkString2", "").Replace("<br>", "\n"); //staring
			_mfotoAlbumsPath = f.IniReadValue("Main", "MfotoAlbumsPath", "d:\\MPR500_Albums"); //staring

            _ExportOld = bool.Parse(f.IniReadValue("Main", "ExportOld", "true")); //bool
            _DontLockExported = bool.Parse(f.IniReadValue("Main", "DontLockExported", "true")); //bool
            _Dir_auto_import = f.IniReadValue("Main", "DirAutoImport", "d:\\Exchange"); //staring

            _PublicKey = f.IniReadValue("API", "PublicKey", "3c209e8c86464be69d9fbf36b1bde8a9"); //staring
            _PrivateKey = f.IniReadValue("API", "PrivateKey", "b41e9b4a20c647038bc590bac05ecf30"); //staring
            _ApiRequestToken = f.IniReadValue("API", "RequestToken", "http://api.pixlpark.com/oauth/requesttoken"); //staring
            _ApiAccessToken = f.IniReadValue("API", "AccessToken", "http://api.pixlpark.com/oauth/accesstoken"); //staring
            _ApiOrder = f.IniReadValue("API", "Order", "http://api.pixlpark.com/orders/"); //staring
            _ApiOrderItems = f.IniReadValue("API", "OrderItems", "http://api.pixlpark.com/orders/"); //staring
            _ApiProducts = f.IniReadValue("API", "Products", "http://api.pixlpark.com/products"); //staring
            _ApiUser = f.IniReadValue("API", "User", "http://api.pixlpark.com/users/"); //staring
            _OrderPixlPark = f.IniReadValue("API", "OrderPrefix", "50"); //staring
            _AStatus = f.IniReadValue("API", "A Status", "000000"); //staring
            _OStatus = f.IniReadValue("API", "O Status", "000100"); //staring
            _DStatus = f.IniReadValue("API", "D Status", "000200"); //staring
            _SelectImport = bool.Parse(f.IniReadValue("API", "Select import", "true")); //bool
            _PrintAfterImport = bool.Parse(f.IniReadValue("API", "Print after import", "true")); //bool
            _PixlParkClient = bool.Parse(f.IniReadValue("API", "PixlPark Client", "false")); //bool
            _EmailDayReport = f.IniReadValue("Main", "Email day report", "sereda@fotoland.ru"); //staring
        }
		
		public bool Save()
		{
			bool r = false;
			try
			{
				f.IniWriteValue("DB", "Connection_type", _Connection_type); //string 
				f.IniWriteValue("DB", "Db_dsn", _Db_dsn); //string 
				f.IniWriteValue("DB", "Db_server", _Db_server); //string 
				f.IniWriteValue("DB", "Db_user", _Db_user); //string 
				f.IniWriteValue("DB", "Db_password", _Db_password); //string 
				f.IniWriteValue("DB", "Db_base", _Db_base); //string 
				f.IniWriteValue("DB", "Connection_string", _Connection_string); //string 
                f.IniWriteValue("Inventory", "Block_inventory", _Block_inventory.ToString()); //bool 
                f.IniWriteValue("Run", "Run_one_copy_acceptance", _Run_one_copy_acceptance.ToString()); //bool 
                f.IniWriteValue("Run", "Run_one_copy_inventory", _Run_one_copy_inventory.ToString()); //bool 
                f.IniWriteValue("Run", "Run_one_copy_oprator", _Run_one_copy_oprator.ToString()); //bool 
				f.IniWriteValue("Run", "Run_one_copy_designer", _Run_one_copy_designer.ToString()); //bool 
				f.IniWriteValue("Run", "Run_one_copy_admin", _Run_one_copy_admin.ToString()); //bool 
				f.IniWriteValue("Run", "Run_one_copy_robot", _Run_one_copy_robot.ToString()); //bool 
				f.IniWriteValue("Main", "Dir_print", _Dir_print); //string 
                f.IniWriteValue("Main", "Dir_edit", _Dir_edit); //string 
                f.IniWriteValue("Main", "Dir_tmp_export", _Dir_tmp_export); //string 
				f.IniWriteValue("Main", "Search_SubDir", _Search_SubDir.ToString()); //bool 
				f.IniWriteValue("Main", "List_of_files", _List_of_files); //string 
				f.IniWriteValue("Main", "Dir_rescan", _Dir_rescan.ToString()); //int 
				f.IniWriteValue("Main", "Time_for_output", _Time_for_output.ToString()); //int 
				f.IniWriteValue("Main", "Time_begin_work", _Time_begin_work.ToString()); //int 
				f.IniWriteValue("Main", "Time_end_work", _Time_end_work.ToString()); //int 
				f.IniWriteValue("Main", "Order_prefics", _Order_prefics); //string 
				f.IniWriteValue("Main", "Order_terminal_prefics", _Order_terminal_prefics); //string 
				f.IniWriteValue("Form", "Qbtn01_id", _Qbtn01_id); //string 
				f.IniWriteValue("Form", "Qbtn01_text", _Qbtn01_text); //string 
				f.IniWriteValue("Form", "Qbtn02_id", _Qbtn02_id); //string 
				f.IniWriteValue("Form", "Qbtn02_text", _Qbtn02_text); //string 
				f.IniWriteValue("Form", "Qbtn03_id", _Qbtn03_id); //string 
				f.IniWriteValue("Form", "Qbtn03_text", _Qbtn03_text); //string 
				f.IniWriteValue("Form", "Qbtn04_id", _Qbtn04_id); //string 
				f.IniWriteValue("Form", "Qbtn04_text", _Qbtn04_text); //string 
				f.IniWriteValue("Form", "Qbtn05_id", _Qbtn05_id); //string 
				f.IniWriteValue("Form", "Qbtn05_text", _Qbtn05_text); //string 
				f.IniWriteValue("Form", "Qbtn06_id", _Qbtn06_id); //string 
				f.IniWriteValue("Form", "Qbtn06_text", _Qbtn06_text); //string 
				f.IniWriteValue("Form", "Qbtn07_id", _Qbtn07_id); //string 
				f.IniWriteValue("Form", "Qbtn07_text", _Qbtn07_text); //string 
				f.IniWriteValue("Form", "Qbtn08_id", _Qbtn08_id); //string 
				f.IniWriteValue("Form", "Qbtn08_text", _Qbtn08_text); //string 
				f.IniWriteValue("Form", "Qbtn09_id", _Qbtn09_id); //string 
				f.IniWriteValue("Form", "Qbtn09_text", _Qbtn09_text); //string 
				f.IniWriteValue("Form", "Qbtn10_id", _Qbtn10_id); //string 
				f.IniWriteValue("Form", "Qbtn10_text", _Qbtn10_text); //string 
				f.IniWriteValue("Form", "Qbtn01_stext", _Qbtn01_stext); //string 
				f.IniWriteValue("Form", "Qbtn02_stext", _Qbtn02_stext); //string 
				f.IniWriteValue("Form", "Qbtn03_stext", _Qbtn03_stext); //string 
				f.IniWriteValue("Form", "Qbtn04_stext", _Qbtn04_stext); //string 
				f.IniWriteValue("Form", "Qbtn05_stext", _Qbtn05_stext); //string 
				f.IniWriteValue("Form", "Qbtn06_stext", _Qbtn06_stext); //string 
				f.IniWriteValue("Form", "Qbtn07_stext", _Qbtn07_stext); //string 
				f.IniWriteValue("Form", "Qbtn08_stext", _Qbtn08_stext); //string 
				f.IniWriteValue("Form", "Qbtn09_stext", _Qbtn09_stext); //string 
				f.IniWriteValue("Form", "Qbtn10_stext", _Qbtn10_stext); //string 
				f.IniWriteValue("Main", "UpdatePaymentTable", _UpdatePaymentTable.ToString()); //int 
				f.IniWriteValue("Main", "UpdateOrderTableInAcceptance", _UpdateOrderTableInAcceptance.ToString()); //int 
				f.IniWriteValue("Main", "PathReportsTemplates", _PathReportsTemplates); //string 
				f.IniWriteValue("Main", "Color_rows_in_order", _Color_rows_in_order.ToString()); //bool 
				f.IniWriteValue("Main", "Fly_window_operator", _Fly_window_operator.ToString()); //bool 
				f.IniWriteValue("Main", "Mod_operator_top_most", _Mod_operator_top_most.ToString()); //bool 
				f.IniWriteValue("Main", "UpdateOrderTableInOperator", _UpdateOrderTableInOperator.ToString()); //int 
				f.IniWriteValue("Main", "Mod_designer_top_most", _Mod_designer_top_most.ToString()); //bool 
				f.IniWriteValue("Main", "UpdateOrderTableInDesigner", _UpdateOrderTableInDesigner.ToString()); //int 
				f.IniWriteValue("Main", "Fly_window_designer", _Fly_window_designer.ToString()); //bool 
				f.IniWriteValue("Main", "Dir_import", _Dir_import); //string 
                f.IniWriteValue("Main", "Dir_export", _Dir_export); //string 
                f.IniWriteValue("Main", "Dir_net_export", _Dir_net_export); //string 
                f.IniWriteValue("Robot", "Robot_animation_icon", _Robot_animation_icon.ToString()); //bool 
				f.IniWriteValue("Robot", "SQL_Import_Template_Mashine", _SQL_Import_Template_Mashine); //string 
				f.IniWriteValue("Robot", "SQL_Import_Template_DCard", _SQL_Import_Template_DCard); //string 
				f.IniWriteValue("Robot", "SQL_Import_Template_Material", _SQL_Import_Template_Material); //string 
				f.IniWriteValue("Robot", "SQL_Import_Template_Good", _SQL_Import_Template_Good); //string 
				f.IniWriteValue("Main", "ReklamBlock1", _ReklamBlock1.Replace("\n", "<br>").Replace("\r", "")); //string 
				f.IniWriteValue("Main", "CheckPreview", _CheckPreview.ToString()); //bool 
				f.IniWriteValue("Main", "CheckCount", _CheckCount.ToString()); //int 
				f.IniWriteValue("Main", "PasswordClass1", _PasswordClass1); //string 
				f.IniWriteValue("Debug", "SMTPServer", _SMTPServer); //string 
				f.IniWriteValue("Debug", "SMTPAut", _SMTPAut.ToString()); //bool 
				f.IniWriteValue("Debug", "SMTPUserFrom", _SMTPUserFrom); //string 
				f.IniWriteValue("Debug", "SMTPUserFromEmail", _SMTPUserFromEmail); //string 
				f.IniWriteValue("Debug", "SMTPPasswordFrom", _SMTPPasswordFrom); //string 
				f.IniWriteValue("Debug", "SMTPUserTo", _SMTPUserTo); //string 
				f.IniWriteValue("Debug", "AutoSendCrashReport", _AutoSendCrashReport.ToString()); //bool 
				f.IniWriteValue("Robot", "FTP_Server", _FTP_Server); //string 
				f.IniWriteValue("Robot", "FTP_Server_Export", _FTP_Server_Export); //string 
				f.IniWriteValue("Robot", "FTP_User", _FTP_User); //string 
				f.IniWriteValue("Robot", "FTP_Password", _FTP_Password); //string 
				f.IniWriteValue("Robot", "FTP_Path", _FTP_Path); //string 
				f.IniWriteValue("Robot", "FTP_Path_Export", _FTP_Path_Export); //string 
				f.IniWriteValue("Robot", "Export_from_ftp", _Export_from_ftp.ToString()); //bool 
				f.IniWriteValue("Robot", "Import_from_ftp", _Import_from_ftp.ToString()); //bool 
				f.IniWriteValue("Robot", "Import_time", _Import_time.ToString()); //int 
				f.IniWriteValue("Robot", "Export_time", _Export_time.ToString()); //int 
				f.IniWriteValue("Main", "ShortGoodsListInWizard", _ShortGoodsListInWizard.ToString()); //bool 
				f.IniWriteValue("Robot", "ExportDoCopy", _ExportDoCopy.ToString()); //bool 
				f.IniWriteValue("Robot", "ExportClearDirAfterCopy", _ExportClearDirAfterCopy.ToString()); //bool 
				f.IniWriteValue("Main", "ModelRound", _ModelRound.ToString()); //int 
				f.IniWriteValue("Debug", "QueryForDelete", _QueryForDelete.ToString()); //bool 
				f.IniWriteValue("Debug", "DenyDelete", _DenyDelete.ToString()); //bool 
				f.IniWriteValue("Main", "DCard_limit", _DCard_limit.ToString()); //int 
				f.IniWriteValue("Main", "PublicIni", _PublicIni); //string 
				f.IniWriteValue("Semaphore", "SemaphoreInventory", _SemaphoreInventory.ToString()); //bool
				f.IniWriteValue("Main", "Round3", _Round3.ToString()); //bool
				f.IniWriteValue("Terminal", "TerminalClient", _Terminal_client_one.ToString()); //bool
				f.IniWriteValue("Terminal", "TerminalPrintCheck", _Terminal_print_check.ToString()); //bool
				f.IniWriteValue("Terminal", "TerminalControlWorker", _Terminal_control_worker.ToString()); //bool
				f.IniWriteValue("Terminal", "UseXmlCache", _UseXmlCache.ToString()); //bool
				f.IniWriteValue("Terminal", "LoadNotApproved", _LoadNotApproved.ToString()); //bool
				f.IniWriteValue("Terminal", "Load1000", _Load1000.ToString()); //bool
				f.IniWriteValue("Main", "ShowQuickOrder", _ShowQuickOrder.ToString()); //bool
				f.IniWriteValue("Main", "ShowMFoto", _ShowMFoto.ToString()); //bool
				f.IniWriteValue("Main", "DiscontServerAddress", _DiscontServerAddress); //staring
				f.IniWriteValue("Main", "checkString1", _checkString1.Replace("\n", "<br>").Replace("\r", "")); //staring
				f.IniWriteValue("Main", "checkString2", _checkString2.Replace("\n", "<br>").Replace("\r", "")); //staring
                f.IniWriteValue("Main", "MfotoAlbumsPath", _mfotoAlbumsPath); //staring
                f.IniWriteValue("Main", "ExportOld", _ExportOld.ToString()); //bool
                f.IniWriteValue("Main", "DontLockExported", _DontLockExported.ToString()); //bool
                f.IniWriteValue("Main", "DirAutoImport", _Dir_auto_import); //staring
                f.IniWriteValue("API", "PublicKey", _PublicKey); //staring
                f.IniWriteValue("API", "PrivateKey", _PrivateKey); //staring
                f.IniWriteValue("API", "RequestToken", _ApiRequestToken); //staring
                f.IniWriteValue("API", "AccessToken", _ApiAccessToken); //staring
                f.IniWriteValue("API", "Order", _ApiOrder); //staring
                f.IniWriteValue("API", "OrderItems", _ApiOrderItems); //staring
                f.IniWriteValue("API", "Products", _ApiProducts); //staring
                f.IniWriteValue("API", "User", _ApiUser); //staring
                f.IniWriteValue("API", "OrderPrefix", _OrderPixlPark); //staring
                f.IniWriteValue("API", "A Status", _AStatus); //staring
                f.IniWriteValue("API", "D Status", _DStatus); //staring
                f.IniWriteValue("API", "O Status", _OStatus); //staring
                f.IniWriteValue("API", "Select import", _SelectImport.ToString()); //bool
                f.IniWriteValue("API", "Print after import", _PrintAfterImport.ToString()); //bool
                f.IniWriteValue("API", "PixlPark Client", _PixlParkClient.ToString()); //bool
                f.IniWriteValue("Main", "Email day report", _EmailDayReport); //string
                r = true;
			}
			catch(Exception ex)
			{
				r = false;
			}
			return r;
		}

		public decimal DoRound(decimal sum)
		{
			switch (ModelRound)
			{
				/*
				0 нет округления
				1 1,00-1,25=1,00; 1,26-1,75=1,50; 1,76=2,00
				2 1,00-1,45=1,00; 1,46-1,95=1,50; 1,96=2,00
				3 1,00-1,15=1,00; 1,16-1,65=1,50; 1,66=2,00
				4 1,00-1,49=1,00; 1,50-1,99=2,00
				 */
				case 0:
					{
						return sum;
						break;
					}
				case 1:
					{
						int s = (int)sum;
						decimal o = sum - s;
						if (o < (decimal)0.26)
							return s;
						else if ((o > (decimal)0.25) && (o < (decimal)0.76))
							return s + (decimal)0.5;
						else if (o > (decimal)0.75)
							return s + 1;
						else
							return s;
						break;
					}
				case 2:
					{
						int s = (int)sum;
						decimal o = sum - s;
						if (o < (decimal)0.46)
							return s;
						else if ((o > (decimal)0.45) && (o < (decimal)0.96))
							return s + (decimal)0.5;
						else if (o > (decimal)0.95)
							return s + 1;
						else
							return s;
						break;
					}
				case 3:
					{
						int s = (int)sum;
						decimal o = sum - s;
						if (o < (decimal)0.16)
							return s;
						else if ((o > (decimal)0.15) && (o < (decimal)0.66))
							return s + (decimal)0.5;
						else if (o > (decimal)0.65)
							return s + 1;
						else
							return s;
						break;
					}
				case 4:
					{
						int s = (int)sum;
						decimal o = sum - s;
						if (o < (decimal)0.5)
							return s;
						else
							return s + 1;
						break;
					}
				case 5:
					{
						int s = (int)sum;
						decimal o = sum - s;
						if (o < (decimal)0.49)
							return s;
						else if ((o > (decimal)0.49) && (o < (decimal)0.51))
							return s + (decimal)0.5;
						else if (o > (decimal)0.50)
							return s + 1;
						else
							return s;
						break;
					}
				default:
					{
						return sum;
					}
			}
		}

	
	
	}
}
