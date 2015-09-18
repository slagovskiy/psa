using System;
using System.Collections.Generic;
using System.Text;

namespace PSA.Lib.Util
{
	public class Version
	{

		//***********************************************
		//
		//          Подправить перед релизом
		//
		//***********************************************
		private string _Acceptance = "1.5.9.0814";
		private string _Administrator = "1.5.9.0804";
		private string _Designer = "1.5.9.0804";
		private string _Operator = "1.5.9.0804";
		private string _Exchanger = "1.5.9.0807";
		private string _Robot = "1.4.9.0512";
		private string _Inventory = "1.5.9.0804";
		private string _Kiosk = "1.5.9.0811";
		//***********************************************

		public string getAcceptanceVersion
		{
			get { return _Acceptance; }
		}

		public string getAdministratorVersion
		{
			get { return _Administrator; }
		}

		public string getDesignerVertion
		{
			get { return _Designer; }
		}

		public string getOperatorVersion
		{
			get { return _Operator; }
		}

		public string getExchangerVersion
		{
			get { return _Exchanger; }
		}

		public string getRobotVersion
		{
			get { return _Robot; }
		}

		public string getInventoryVersion
		{
			get { return _Inventory; }
		}

		public string getKioskVersion
		{
			get { return _Kiosk; }
		}


		public static string Acceptance
		{
			get { return new Version().getAcceptanceVersion; }
		}

		public static string Administrator
		{
			get { return new Version().getAdministratorVersion; }
		}

		public static string Designer
		{
			get { return new Version().getDesignerVertion; }
		}

		public static string Operator
		{
			get { return new Version().getOperatorVersion; }
		}

		public static string Exchanger
		{
			get { return new Version().getExchangerVersion; }
		}

		public static string Robot
		{
			get { return new Version().getRobotVersion; }
		}

		public static string Inventory
		{
			get { return new Version().getInventoryVersion; }
		}

		public static string Kiosk
		{
			get { return new Version().getKioskVersion; }
		}

	}
}
