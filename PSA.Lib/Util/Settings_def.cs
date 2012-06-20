using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PSA.Lib.Util
{
	public partial class Settings
	{

		private ini f;

		private string _Connection_type;
		public string Connection_type
		{
			get { return _Connection_type; }
			set { _Connection_type = value; }
		}

		private string _Db_dsn;
		public string Db_dsn
		{
			get
			{
				return _Db_dsn;
			}
			set
			{
				_Db_dsn = value;
			}
		}

		private string _Db_server;
		public string Db_server
		{
			get
			{
				return _Db_server;
			}
			set
			{
				_Db_server = value;
			}
		}

		private string _Db_user;
		public string Db_user
		{
			get
			{
				return _Db_user;
			}
			set
			{
				_Db_user = value;
			}
		}

		private string _Db_password;
		public string Db_password
		{
			get
			{
				return _Db_password;
			}
			set
			{
				_Db_password = value;
			}
		}

		private string _Db_base;
		public string Db_base
		{
			get
			{
				return _Db_base;
			}
			set
			{
				_Db_base = value;
			}
		}

		private string _Connection_string;
		public string Connection_string
		{
			get
			{
				return _Connection_string;
			}
			set
			{
				_Connection_string = value;
			}
		}

        private bool _Block_inventory;
        public bool Block_inventory
        {
            get
            {
                return _Block_inventory;
            }
            set
            {
                _Block_inventory = value;
            }
        }

        private bool _Run_one_copy_acceptance;
        public bool Run_one_copy_acceptance
        {
            get
            {
                return _Run_one_copy_acceptance;
            }
            set
            {
                _Run_one_copy_acceptance = value;
            }
        }

        private bool _Run_one_copy_inventory;
        public bool Run_one_copy_inventory
        {
            get
            {
                return _Run_one_copy_inventory;
            }
            set
            {
                _Run_one_copy_inventory = value;
            }
        }

        private bool _Run_one_copy_oprator;
		public bool Run_one_copy_oprator
		{
			get
			{
				return _Run_one_copy_oprator;
			}
			set
			{
				_Run_one_copy_oprator = value;
			}
		}

		private bool _Run_one_copy_designer;
		public bool Run_one_copy_designer
		{
			get
			{
				return _Run_one_copy_designer;
			}
			set
			{
				_Run_one_copy_designer = value;
			}
		}

		private bool _Run_one_copy_admin;
		public bool Run_one_copy_admin
		{
			get
			{
				return _Run_one_copy_admin;
			}
			set
			{
				_Run_one_copy_admin = value;
			}
		}

		private bool _Run_one_copy_robot;
		public bool Run_one_copy_robot
		{
			get
			{
				return _Run_one_copy_robot;
			}
			set
			{
				_Run_one_copy_robot = value;
			}
		}

        private string _Dir_print;
        public string Dir_print
        {
            get
            {
                return _Dir_print;
            }
            set
            {
                _Dir_print = value;
            }
        }

        private string _Dir_tmp_export;
        public string Dir_tmp_export
        {
            get
            {
                return _Dir_tmp_export;
            }
            set
            {
                _Dir_tmp_export = value;
            }
        }

        private string _Dir_edit;
		public string Dir_edit
		{
			get
			{
				return _Dir_edit;
			}
			set
			{
				_Dir_edit = value;
			}
		}

		private bool _Search_SubDir;
		public bool Search_SubDir
		{
			get
			{
				return _Search_SubDir;
			}
			set
			{
				_Search_SubDir = value;
			}
		}

		private string _List_of_files;
		public string List_of_files
		{
			get
			{
				return _List_of_files;
			}
			set
			{
				_List_of_files = value;
			}
		}

		private int _Dir_rescan;
		public int Dir_rescan
		{
			get
			{
				return _Dir_rescan;
			}
			set
			{
				_Dir_rescan = value;
			}
		}

		private int _Time_for_output;
		public int Time_for_output
		{
			get
			{
				return _Time_for_output;
			}
			set
			{
				_Time_for_output = value;
			}
		}

		private int _Time_begin_work;
		public int Time_begin_work
		{
			get
			{
				return _Time_begin_work;
			}
			set
			{
				_Time_begin_work = value;
			}
		}

		private int _Time_end_work;
		public int Time_end_work
		{
			get
			{
				return _Time_end_work;
			}
			set
			{
				_Time_end_work = value;
			}
		}

		private string _Order_prefics;
		public string Order_prefics
		{
			get
			{
				return _Order_prefics;
			}
			set
			{
				_Order_prefics = value;
			}
		}

		private string _Order_terminal_prefics;
		public string Order_terminal_prefics
		{
			get
			{
				return _Order_terminal_prefics;
			}
			set
			{
				_Order_terminal_prefics = value;
			}
		}

		private string _Qbtn01_id;
		public string Qbtn01_id
		{
			get
			{
				return _Qbtn01_id;
			}
			set
			{
				_Qbtn01_id = value;
			}
		}

		private string _Qbtn01_text;
		public string Qbtn01_text
		{
			get
			{
				return _Qbtn01_text;
			}
			set
			{
				_Qbtn01_text = value;
			}
		}

		private string _Qbtn02_id;
		public string Qbtn02_id
		{
			get
			{
				return _Qbtn02_id;
			}
			set
			{
				_Qbtn02_id = value;
			}
		}

		private string _Qbtn02_text;
		public string Qbtn02_text
		{
			get
			{
				return _Qbtn02_text;
			}
			set
			{
				_Qbtn02_text = value;
			}
		}

		private string _Qbtn03_id;
		public string Qbtn03_id
		{
			get
			{
				return _Qbtn03_id;
			}
			set
			{
				_Qbtn03_id = value;
			}
		}

		private string _Qbtn03_text;
		public string Qbtn03_text
		{
			get
			{
				return _Qbtn03_text;
			}
			set
			{
				_Qbtn03_text = value;
			}
		}

		private string _Qbtn04_id;
		public string Qbtn04_id
		{
			get
			{
				return _Qbtn04_id;
			}
			set
			{
				_Qbtn04_id = value;
			}
		}

		private string _Qbtn04_text;
		public string Qbtn04_text
		{
			get
			{
				return _Qbtn04_text;
			}
			set
			{
				_Qbtn04_text = value;
			}
		}

		private string _Qbtn05_id;
		public string Qbtn05_id
		{
			get
			{
				return _Qbtn05_id;
			}
			set
			{
				_Qbtn05_id = value;
			}
		}

		private string _Qbtn05_text;
		public string Qbtn05_text
		{
			get
			{
				return _Qbtn05_text;
			}
			set
			{
				_Qbtn05_text = value;
			}
		}

		private string _Qbtn06_id;
		public string Qbtn06_id
		{
			get
			{
				return _Qbtn06_id;
			}
			set
			{
				_Qbtn06_id = value;
			}
		}

		private string _Qbtn06_text;
		public string Qbtn06_text
		{
			get
			{
				return _Qbtn06_text;
			}
			set
			{
				_Qbtn06_text = value;
			}
		}

		private string _Qbtn07_id;
		public string Qbtn07_id
		{
			get
			{
				return _Qbtn07_id;
			}
			set
			{
				_Qbtn07_id = value;
			}
		}

		private string _Qbtn07_text;
		public string Qbtn07_text
		{
			get
			{
				return _Qbtn07_text;
			}
			set
			{
				_Qbtn07_text = value;
			}
		}

		private string _Qbtn08_id;
		public string Qbtn08_id
		{
			get
			{
				return _Qbtn08_id;
			}
			set
			{
				_Qbtn08_id = value;
			}
		}

		private string _Qbtn08_text;
		public string Qbtn08_text
		{
			get
			{
				return _Qbtn08_text;
			}
			set
			{
				_Qbtn08_text = value;
			}
		}

		private string _Qbtn09_id;
		public string Qbtn09_id
		{
			get
			{
				return _Qbtn09_id;
			}
			set
			{
				_Qbtn09_id = value;
			}
		}

		private string _Qbtn09_text;
		public string Qbtn09_text
		{
			get
			{
				return _Qbtn09_text;
			}
			set
			{
				_Qbtn09_text = value;
			}
		}

		private string _Qbtn10_id;
		public string Qbtn10_id
		{
			get
			{
				return _Qbtn10_id;
			}
			set
			{
				_Qbtn10_id = value;
			}
		}

		private string _Qbtn10_text;
		public string Qbtn10_text
		{
			get
			{
				return _Qbtn10_text;
			}
			set
			{
				_Qbtn10_text = value;
			}
		}

		private string _Qbtn01_stext;
		public string Qbtn01_stext
		{
			get
			{
				return _Qbtn01_stext;
			}
			set
			{
				_Qbtn01_stext = value;
			}
		}

		private string _Qbtn02_stext;
		public string Qbtn02_stext
		{
			get
			{
				return _Qbtn02_stext;
			}
			set
			{
				_Qbtn02_stext = value;
			}
		}

		private string _Qbtn03_stext;
		public string Qbtn03_stext
		{
			get
			{
				return _Qbtn03_stext;
			}
			set
			{
				_Qbtn03_stext = value;
			}
		}

		private string _Qbtn04_stext;
		public string Qbtn04_stext
		{
			get
			{
				return _Qbtn04_stext;
			}
			set
			{
				_Qbtn04_stext = value;
			}
		}

		private string _Qbtn05_stext;
		public string Qbtn05_stext
		{
			get
			{
				return _Qbtn05_stext;
			}
			set
			{
				_Qbtn05_stext = value;
			}
		}

		private string _Qbtn06_stext;
		public string Qbtn06_stext
		{
			get
			{
				return _Qbtn06_stext;
			}
			set
			{
				_Qbtn06_stext = value;
			}
		}

		private string _Qbtn07_stext;
		public string Qbtn07_stext
		{
			get
			{
				return _Qbtn07_stext;
			}
			set
			{
				_Qbtn07_stext = value;
			}
		}

		private string _Qbtn08_stext;
		public string Qbtn08_stext
		{
			get
			{
				return _Qbtn08_stext;
			}
			set
			{
				_Qbtn08_stext = value;
			}
		}

		private string _Qbtn09_stext;
		public string Qbtn09_stext
		{
			get
			{
				return _Qbtn09_stext;
			}
			set
			{
				_Qbtn09_stext = value;
			}
		}

		private string _Qbtn10_stext;
		public string Qbtn10_stext
		{
			get
			{
				return _Qbtn10_stext;
			}
			set
			{
				_Qbtn10_stext = value;
			}
		}

		private int _UpdatePaymentTable;
		public int UpdatePaymentTable
		{
			get
			{
				return _UpdatePaymentTable;
			}
			set
			{
				_UpdatePaymentTable = value;
			}
		}

		private int _UpdateOrderTableInAcceptance;
		public int UpdateOrderTableInAcceptance
		{
			get
			{
				return _UpdateOrderTableInAcceptance;
			}
			set
			{
				_UpdateOrderTableInAcceptance = value;
			}
		}

		private string _PathReportsTemplates;
		public string PathReportsTemplates
		{
			get
			{
				return _PathReportsTemplates;
			}
			set
			{
				_PathReportsTemplates = value;
			}
		}

		private bool _Color_rows_in_order;
		public bool Color_rows_in_order
		{
			get
			{
				return _Color_rows_in_order;
			}
			set
			{
				_Color_rows_in_order = value;
			}
		}

		private bool _Fly_window_operator;
		public bool Fly_window_operator
		{
			get
			{
				return _Fly_window_operator;
			}
			set
			{
				_Fly_window_operator = value;
			}
		}

		private bool _Mod_operator_top_most;
		public bool Mod_operator_top_most
		{
			get
			{
				return _Mod_operator_top_most;
			}
			set
			{
				_Mod_operator_top_most = value;
			}
		}

		private int _UpdateOrderTableInOperator;
		public int UpdateOrderTableInOperator
		{
			get
			{
				return _UpdateOrderTableInOperator;
			}
			set
			{
				_UpdateOrderTableInOperator = value;
			}
		}

		private bool _Mod_designer_top_most;
		public bool Mod_designer_top_most
		{
			get
			{
				return _Mod_designer_top_most;
			}
			set
			{
				_Mod_designer_top_most = value;
			}
		}

		private int _UpdateOrderTableInDesigner;
		public int UpdateOrderTableInDesigner
		{
			get
			{
				return _UpdateOrderTableInDesigner;
			}
			set
			{
				_UpdateOrderTableInDesigner = value;
			}
		}

		private bool _Fly_window_designer;
		public bool Fly_window_designer
		{
			get
			{
				return _Fly_window_designer;
			}
			set
			{
				_Fly_window_designer = value;
			}
		}

		private string _Dir_import;
		public string Dir_import
		{
			get
			{
				return _Dir_import;
			}
			set
			{
				_Dir_import = value;
			}
		}

		private string _Dir_export;
		public string Dir_export
		{
			get
			{
				return _Dir_export;
			}
			set
			{
				_Dir_export = value;
			}
		}

		private bool _Robot_animation_icon;
		public bool Robot_animation_icon
		{
			get
			{
				return _Robot_animation_icon;
			}
			set
			{
				_Robot_animation_icon = value;
			}
		}

		private string _SQL_Import_Template_Mashine;
		public string SQL_Import_Template_Mashine
		{
			get
			{
				return _SQL_Import_Template_Mashine;
			}
			set
			{
				_SQL_Import_Template_Mashine = value;
			}
		}

		private string _SQL_Import_Template_DCard;
		public string SQL_Import_Template_DCard
		{
			get
			{
				return _SQL_Import_Template_DCard;
			}
			set
			{
				_SQL_Import_Template_DCard = value;
			}
		}

		private string _SQL_Import_Template_Material;
		public string SQL_Import_Template_Material
		{
			get
			{
				return _SQL_Import_Template_Material;
			}
			set
			{
				_SQL_Import_Template_Material = value;
			}
		}

		private string _SQL_Import_Template_Good;
		public string SQL_Import_Template_Good
		{
			get
			{
				return _SQL_Import_Template_Good;
			}
			set
			{
				_SQL_Import_Template_Good = value;
			}
		}

		private string _ReklamBlock1;
		public string ReklamBlock1
		{
			get
			{
				return _ReklamBlock1;
			}
			set
			{
				_ReklamBlock1 = value;
			}
		}

		private bool _CheckPreview;
		public bool CheckPreview
		{
			get
			{
				return _CheckPreview;
			}
			set
			{
				_CheckPreview = value;
			}
		}

		private int _CheckCount;
		public int CheckCount
		{
			get
			{
				return _CheckCount;
			}
			set
			{
				_CheckCount = value;
			}
		}

		private string _PasswordClass1;
		public string PasswordClass1
		{
			get
			{
				return _PasswordClass1;
			}
			set
			{
				_PasswordClass1 = value;
			}
		}

		private string _SMTPServer;
		public string SMTPServer
		{
			get
			{
				return _SMTPServer;
			}
			set
			{
				_SMTPServer = value;
			}
		}

		private bool _SMTPAut;
		public bool SMTPAut
		{
			get
			{
				return _SMTPAut;
			}
			set
			{
				_SMTPAut = value;
			}
		}

		private string _SMTPUserFrom;
		public string SMTPUserFrom
		{
			get
			{
				return _SMTPUserFrom;
			}
			set
			{
				_SMTPUserFrom = value;
			}
		}

		private string _SMTPUserFromEmail;
		public string SMTPUserFromEmail
		{
			get
			{
				return _SMTPUserFromEmail;
			}
			set
			{
				_SMTPUserFromEmail = value;
			}
		}

		private string _SMTPPasswordFrom;
		public string SMTPPasswordFrom
		{
			get
			{
				return _SMTPPasswordFrom;
			}
			set
			{
				_SMTPPasswordFrom = value;
			}
		}

		private string _SMTPUserTo;
		public string SMTPUserTo
		{
			get
			{
				return _SMTPUserTo;
			}
			set
			{
				_SMTPUserTo = value;
			}
		}

		private bool _AutoSendCrashReport;
		public bool AutoSendCrashReport
		{
			get
			{
				return _AutoSendCrashReport;
			}
			set
			{
				_AutoSendCrashReport = value;
			}
		}

		private string _FTP_Server;
		public string FTP_Server
		{
			get
			{
				return _FTP_Server;
			}
			set
			{
				_FTP_Server = value;
			}
		}

		private string _FTP_Server_Export;
		public string FTP_Server_Export
		{
			get
			{
				return _FTP_Server_Export;
			}
			set
			{
				_FTP_Server_Export = value;
			}
		}

		private string _FTP_User;
		public string FTP_User
		{
			get
			{
				return _FTP_User;
			}
			set
			{
				_FTP_User = value;
			}
		}

		private string _FTP_Password;
		public string FTP_Password
		{
			get
			{
				return _FTP_Password;
			}
			set
			{
				_FTP_Password = value;
			}
		}

		private string _FTP_Path;
		public string FTP_Path
		{
			get
			{
				return _FTP_Path;
			}
			set
			{
				_FTP_Path = value;
			}
		}

		private string _FTP_Path_Export;
		public string FTP_Path_Export
		{
			get
			{
				return _FTP_Path_Export;
			}
			set
			{
				_FTP_Path_Export = value;
			}
		}

		private bool _Export_from_ftp;
		public bool Export_from_ftp
		{
			get
			{
				return _Export_from_ftp;
			}
			set
			{
				_Export_from_ftp = value;
			}
		}

		private bool _Import_from_ftp;
		public bool Import_from_ftp
		{
			get
			{
				return _Import_from_ftp;
			}
			set
			{
				_Import_from_ftp = value;
			}
		}

		private int _Import_time;
		public int Import_time
		{
			get
			{
				return _Import_time;
			}
			set
			{
				_Import_time = value;
			}
		}

		private int _Export_time;
		public int Export_time
		{
			get
			{
				return _Export_time;
			}
			set
			{
				_Export_time = value;
			}
		}

		private bool _ShortGoodsListInWizard;
		public bool ShortGoodsListInWizard
		{
			get
			{
				return _ShortGoodsListInWizard;
			}
			set
			{
				_ShortGoodsListInWizard = value;
			}
		}

		private bool _ExportDoCopy;
		public bool ExportDoCopy
		{
			get
			{
				return _ExportDoCopy;
			}
			set
			{
				_ExportDoCopy = value;
			}
		}

		private bool _ExportClearDirAfterCopy;
		public bool ExportClearDirAfterCopy
		{
			get
			{
				return _ExportClearDirAfterCopy;
			}
			set
			{
				_ExportClearDirAfterCopy = value;
			}
		}

		private int _ModelRound;
		public int ModelRound
		{
			get
			{
				return _ModelRound;
			}
			set
			{
				_ModelRound = value;
			}
		}

		private bool _QueryForDelete;
		public bool QueryForDelete
		{
			get
			{
				return _QueryForDelete;
			}
			set
			{
				_QueryForDelete = value;
			}
		}

		private bool _DenyDelete;
		public bool DenyDelete
		{
			get
			{
				return _DenyDelete;
			}
			set
			{
				_DenyDelete = value;
			}
		}

		private int _DCard_limit;
		public int DCard_limit
		{
			get
			{
				return _DCard_limit;
			}
			set
			{
				_DCard_limit = value;
			}
		}

		private string _PublicIni;
		public string PublicIni
		{
			get
			{
				return _PublicIni;
			}
			set
			{
				_PublicIni = value;
			}
		}

		private bool _SemaphoreInventory;
		public bool SemaphoreInventory
		{
			get
			{
				return _SemaphoreInventory;
			}
			set
			{
				_SemaphoreInventory = value;
			}
		}

		private bool _Round3;
		public bool Round3
		{
			get
			{
				return _Round3;
			}
			set
			{
				_Round3 = value;
			}
		}

		private bool _Terminal_client_one;
		public bool Terminal_client_one
		{
			get
			{
				return _Terminal_client_one;
			}
			set
			{
				_Terminal_client_one = value;
			}
		}

		private bool _Terminal_print_check;
		public bool Terminal_print_check
		{
			get
			{
				return _Terminal_print_check;
			}
			set
			{
				_Terminal_print_check = value;
			}
		}

		private bool _Terminal_control_worker;
		public bool Terminal_control_worker
		{
			get
			{
				return _Terminal_control_worker;
			}
			set
			{
				_Terminal_control_worker = value;
			}
		}

		private bool _UseXmlCache;
		public bool UseXmlCache
		{
			get
			{
				return _UseXmlCache;
			}
			set
			{
				_UseXmlCache = value;
			}
		}

		private bool _LoadNotApproved;
		public bool LoadNotApproved
		{
			get
			{
				return _LoadNotApproved;
			}
			set
			{
				_LoadNotApproved = value;
			}
		}

		private bool _Load1000;
		public bool Load1000
		{
			get
			{
				return _Load1000;
			}
			set
			{
				_Load1000 = value;
			}
		}

		private bool _ShowQuickOrder;
		public bool ShowQuickOrder
		{
			get
			{
				return _ShowQuickOrder;
			}
			set
			{
				_ShowQuickOrder = value;
			}
		}

		private bool _ShowMFoto;
		public bool ShowMFoto
		{
			get
			{
				return _ShowMFoto;
			}
			set
			{
				_ShowMFoto = value;
			}
		}

		private string _DiscontServerAddress;
		public string DiscontServerAddress
		{
			get
			{
				return _DiscontServerAddress;
			}
			set
			{
				_DiscontServerAddress = value;
			}
		}

		private string _checkString1;
		public string CheckString1
		{
			get
			{
				return _checkString1;
			}
			set
			{
				_checkString1 = value;
			}
		}

		private string _mfotoAlbumsPath;
		public string MfotoAlbumsPath
		{
			get
			{
				return _mfotoAlbumsPath;
			}
			set
			{
				_mfotoAlbumsPath = value;
			}
		}

		private string _checkString2;
		public string CheckString2
		{
			get
			{
				return _checkString2;
			}
			set
			{
				_checkString2 = value;
			}
		}

        private bool _ExportOld;
        public bool ExportOld
        {
            get
            {
                return _ExportOld;
            }
            set
            {
                _ExportOld = value;
            }
        }

        private string _Dir_auto_import;
        public string Dir_auto_import
		{
			get
			{
                return _Dir_auto_import;
			}
			set
			{
                _Dir_auto_import = value;
			}
		}

        private bool _DontLockExported;
        public bool DontLockExported
        {
            get
            {
                return _DontLockExported;
            }
            set
            {
                _DontLockExported = value;
            }
        }


	}
}
