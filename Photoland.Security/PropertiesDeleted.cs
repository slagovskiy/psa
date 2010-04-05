using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.IO;

namespace Photoland.Security
{
	public class PropertiesDeleted
	{
		// Ключ работы с реестром
		private RegistryKey Key;
		
		// тип подключения
		public string Connection_type
		{
			get 
			{
				if (Key.GetValue("Connection type").ToString() != "")
					return Key.GetValue("Connection type").ToString();
				else
					return "";
			}
			set 
			{
				if (value != "")
					Key.SetValue("Connection type", value);
				else
					Key.SetValue("Connection type", "");
			}
		}

		// имя дсн
		public string Db_dsn
		{
			get
			{
				if (Key.GetValue("Database DSN").ToString() != "")
					return Key.GetValue("Database DSN").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Database DSN", value);
				else
					Key.SetValue("Database DSN", "");
			}
		}

		// имя сервера
		public string Db_server
		{
			get
			{
				if (Key.GetValue("Database server").ToString() != "")
					return Key.GetValue("Database server").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Database server", value);
				else
					Key.SetValue("Database server", "");
			}
		}

		// имя пользователя
		public string Db_user
		{
			get
			{
				if (Key.GetValue("Database user").ToString() != "")
					return Key.GetValue("Database user").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Database user", value);
				else
					Key.SetValue("Database user", "");
			}
		}

		// пароль
		public string Db_password
		{
			get
			{
				if (Key.GetValue("Database password").ToString() != "")
					return Key.GetValue("Database password").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Database password", value);
				else
					Key.SetValue("Database password", "");
			}
		}

		// имя базы
		public string Db_base
		{
			get
			{
				if (Key.GetValue("Database base").ToString() != "")
					return Key.GetValue("Database base").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Database base", value);
				else
					Key.SetValue("Database base", "");
			}
		}

		// строка подключения
		public string Connection_string
		{
			get
			{
				if (Key.GetValue("Connection string").ToString() != "")
					return Key.GetValue("Connection string").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Connection string", value);
				else
					Key.SetValue("Connection string", "");
			}
		}

		// Запускать не более одной копии модуля приемщик
		public bool Run_one_copy_acceptance
		{
			get
			{
				if (Key.GetValue("Run one copy mod acceptance").ToString() != "")
					return bool.Parse(Key.GetValue("Run one copy mod acceptance").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Run one copy mod acceptance", value.ToString());
				else
					Key.SetValue("Run one copy mod acceptance", false.ToString());
			}
		}

		// Запускать не более одной копии модуля оператор
		public bool Run_one_copy_oprator
		{
			get
			{
				if (Key.GetValue("Run one copy mod operator").ToString() != "")
					return bool.Parse(Key.GetValue("Run one copy mod operator").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Run one copy mod operator", value.ToString());
				else
					Key.SetValue("Run one copy mod operator", false.ToString());
			}
		}

		// Запускать не более одной копии модуля дизайнер
		public bool Run_one_copy_designer
		{
			get
			{
				if (Key.GetValue("Run one copy mod designer").ToString() != "")
					return bool.Parse(Key.GetValue("Run one copy mod designer").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Run one copy mod designer", value.ToString());
				else
					Key.SetValue("Run one copy mod designer", false.ToString());
			}
		}

		// Запускать не более одной копии модуля администратор
		public bool Run_one_copy_admin
		{
			get
			{
				if (Key.GetValue("Run one copy mod admin").ToString() != "")
					return bool.Parse(Key.GetValue("Run one copy mod admin").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Run one copy mod admin", value.ToString());
				else
					Key.SetValue("Run one copy mod admin", false.ToString());
			}
		}

		// Запускать не более одной копии модуля робот
		public bool Run_one_copy_robot
		{
			get
			{
				if (Key.GetValue("Run one copy mod robot").ToString() != "")
					return bool.Parse(Key.GetValue("Run one copy mod robot").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Run one copy mod robot", value.ToString());
				else
					Key.SetValue("Run one copy mod robot", false.ToString());
			}
		}

		// Каталог заказов на печать
		public string Dir_print
		{
			get
			{
				if (Key.GetValue("Public ini").ToString() != "")
				{
					if (File.Exists(Key.GetValue("Public ini").ToString()))
					{
						if (IniFile.IniReadValue("PSA", "Dir print", Key.GetValue("Public ini").ToString(), "") != "")
						{
							return IniFile.IniReadValue("PSA", "Dir print", Key.GetValue("Public ini").ToString(), "");
						}
						else
						{
							if (Key.GetValue("Dir print").ToString() != "")
								return Key.GetValue("Dir print").ToString();
							else
								return "";
						}
					}
					else
					{
						if (Key.GetValue("Dir print").ToString() != "")
							return Key.GetValue("Dir print").ToString();
						else
							return "";
					}
				}
				else
				{
					if (Key.GetValue("Dir print").ToString() != "")
						return Key.GetValue("Dir print").ToString();
					else
						return "";
				}
			}
			set
			{
				if (value != "")
					Key.SetValue("Dir print", value);
				else
					Key.SetValue("Dir print", "");
			}
		}

		// Каталог заказов на обработку
		public string Dir_edit
		{
			get
			{
				if (Key.GetValue("Public ini").ToString() != "")
				{
					if (File.Exists(Key.GetValue("Public ini").ToString()))
					{
						if (IniFile.IniReadValue("PSA", "Dir edit", Key.GetValue("Public ini").ToString(), "") != "")
						{
							return IniFile.IniReadValue("PSA", "Dir edit", Key.GetValue("Public ini").ToString(), "");
						}
						else
						{
							if (Key.GetValue("Dir edit").ToString() != "")
								return Key.GetValue("Dir edit").ToString();
							else
								return "";
						}
					}
					else
					{
						if (Key.GetValue("Dir edit").ToString() != "")
							return Key.GetValue("Dir edit").ToString();
						else
							return "";
					}
				}
				else
				{
					if (Key.GetValue("Dir edit").ToString() != "")
						return Key.GetValue("Dir edit").ToString();
					else
						return "";
				}
			}
			set
			{
				if (value != "")
					Key.SetValue("Dir edit", value);
				else
					Key.SetValue("Dir edit", "");
			}
		}

		// Поиск в подпапках
		public bool Search_SubDir
		{
			get
			{
				if (Key.GetValue("Search SubDir").ToString() != "")
					return bool.Parse(Key.GetValue("Search SubDir").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Search SubDir", value.ToString());
				else
					Key.SetValue("Search SubDir", false.ToString());
			}
		}

		// Список типов файлов, подлежащих обработке при поиске
		public string List_of_files
		{
			get
			{
				if (Key.GetValue("List of files").ToString() != "")
					return Key.GetValue("List of files").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("List of files", value);
				else
					Key.SetValue("List of files", "");
			}
		}

		// время пересканирования папки заказов
		public int Dir_rescan
		{
			get
			{
				if (Key.GetValue("Dir rescan").ToString() != "")
					return int.Parse(Key.GetValue("Dir rescan").ToString());
				else
					return 0;
			}
			set
			{
				if (value != 0)
					Key.SetValue("Dir rescan", value.ToString());
				else
					Key.SetValue("Dir rescan", "");
			}
		}

		// Время готовности заказа
		public int Time_for_output
		{
			get
			{
				if (Key.GetValue("Time for output").ToString() != "")
					return int.Parse(Key.GetValue("Time for output").ToString());
				else
					return 0;
			}
			set
			{
				if (value != 0)
					Key.SetValue("Time for output", value.ToString());
				else
					Key.SetValue("Time for output", "");
			}
		}

		// Начало работы магазина
		public int Time_begin_work
		{
			get
			{
				if (Key.GetValue("Time begin work").ToString() != "")
					return int.Parse(Key.GetValue("Time begin work").ToString());
				else
					return 0;
			}
			set
			{
				if (value != 0)
					Key.SetValue("Time begin work", value.ToString());
				else
					Key.SetValue("Time begin work", "");
			}
		}

		// Конец работы магазина
		public int Time_end_work
		{
			get
			{
				if (Key.GetValue("Time end work").ToString() != "")
					return int.Parse(Key.GetValue("Time end work").ToString());
				else
					return 0;
			}
			set
			{
				if (value != 0)
					Key.SetValue("Time end work", value.ToString());
				else
					Key.SetValue("Time end work", "");
			}
		}

		// Префикс штрихкода для этого магазина
		public string Order_prefics
		{
			get
			{
				if (Key.GetValue("Public ini").ToString() != "")
				{
					if (File.Exists(Key.GetValue("Public ini").ToString()))
					{
						if (IniFile.IniReadValue("PSA", "Order prefics", Key.GetValue("Public ini").ToString(), "") != "")
						{
							return IniFile.IniReadValue("PSA", "Order prefics", Key.GetValue("Public ini").ToString(), "");
						}
						else
						{
							if (Key.GetValue("Order prefics").ToString() != "")
								return Key.GetValue("Order prefics").ToString();
							else
								return "";
						}
					}
					else
					{
						if (Key.GetValue("Order prefics").ToString() != "")
							return Key.GetValue("Order prefics").ToString();
						else
							return "";
					}
				}
				else
				{
					if (Key.GetValue("Order prefics").ToString() != "")
						return Key.GetValue("Order prefics").ToString();
					else
						return "";
				}
			}
			set
			{
				if (value != "")
					Key.SetValue("Order prefics", value);
				else
					Key.SetValue("Order prefics", "00");
			}
		}

		// Быстрая кнопка 1, код
        public string Qbtn01_id
		{
			get
			{
				if (Key.GetValue("Quick btn id 01").ToString() != "")
					return Key.GetValue("Quick btn id 01").ToString();
				else
					return "0";
			}
			set
			{
				if (value != "0")
					Key.SetValue("Quick btn id 01", value);
				else
					Key.SetValue("Quick btn id 01", "");
			}
		}

		// Быстрая кнопка 1, текст
		public string Qbtn01_text
		{
			get
			{
				if (Key.GetValue("Quick btn string 01").ToString() != "")
					return Key.GetValue("Quick btn string 01").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn string 01", value);
				else
					Key.SetValue("Quick btn string 01", "");
			}
		}

		// Быстрая кнопка 2, код
        public string Qbtn02_id
		{
			get
			{
				if (Key.GetValue("Quick btn id 02").ToString() != "")
					return Key.GetValue("Quick btn id 02").ToString();
				else
					return "0";
			}
			set
			{
				if (value != "0")
					Key.SetValue("Quick btn id 02", value.ToString());
				else
					Key.SetValue("Quick btn id 02", "");
			}
		}

		// Быстрая кнопка 2, текст
		public string Qbtn02_text
		{
			get
			{
				if (Key.GetValue("Quick btn string 02").ToString() != "")
					return Key.GetValue("Quick btn string 02").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn string 02", value);
				else
					Key.SetValue("Quick btn string 02", "");
			}
		}

		// Быстрая кнопка 3, код
        public string Qbtn03_id
		{
			get
			{
				if (Key.GetValue("Quick btn id 03").ToString() != "")
					return Key.GetValue("Quick btn id 03").ToString();
				else
					return "0";
			}
			set
			{
				if (value != "0")
					Key.SetValue("Quick btn id 03", value.ToString());
				else
					Key.SetValue("Quick btn id 03", "");
			}
		}

		// Быстрая кнопка 3, текст
		public string Qbtn03_text
		{
			get
			{
				if (Key.GetValue("Quick btn string 03").ToString() != "")
					return Key.GetValue("Quick btn string 03").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn string 03", value);
				else
					Key.SetValue("Quick btn string 03", "");
			}
		}

		// Быстрая кнопка 4, код
        public string Qbtn04_id
		{
			get
			{
				if (Key.GetValue("Quick btn id 04").ToString() != "")
					return Key.GetValue("Quick btn id 04").ToString();
				else
					return "0";
			}
			set
			{
				if (value != "0")
					Key.SetValue("Quick btn id 04", value.ToString());
				else
					Key.SetValue("Quick btn id 04", "");
			}
		}

		// Быстрая кнопка 4, текст
		public string Qbtn04_text
		{
			get
			{
				if (Key.GetValue("Quick btn string 04").ToString() != "")
					return Key.GetValue("Quick btn string 04").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn string 04", value);
				else
					Key.SetValue("Quick btn string 04", "");
			}
		}

		// Быстрая кнопка 5, код
        public string Qbtn05_id
		{
			get
			{
				if (Key.GetValue("Quick btn id 05").ToString() != "")
					return Key.GetValue("Quick btn id 05").ToString();
				else
					return "0";
			}
			set
			{
				if (value != "0")
					Key.SetValue("Quick btn id 05", value.ToString());
				else
					Key.SetValue("Quick btn id 05", "");
			}
		}

		// Быстрая кнопка 5, текст
		public string Qbtn05_text
		{
			get
			{
				if (Key.GetValue("Quick btn string 05").ToString() != "")
					return Key.GetValue("Quick btn string 05").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn string 05", value);
				else
					Key.SetValue("Quick btn string 05", "");
			}
		}

		// Быстрая кнопка 6, код
        public string Qbtn06_id
		{
			get
			{
				if (Key.GetValue("Quick btn id 06").ToString() != "")
					return Key.GetValue("Quick btn id 06").ToString();
				else
					return "0";
			}
			set
			{
				if (value != "0")
					Key.SetValue("Quick btn id 06", value.ToString());
				else
					Key.SetValue("Quick btn id 6", "");
			}
		}

		// Быстрая кнопка 6, текст
		public string Qbtn06_text
		{
			get
			{
				if (Key.GetValue("Quick btn string 06").ToString() != "")
					return Key.GetValue("Quick btn string 06").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn string 06", value);
				else
					Key.SetValue("Quick btn string 06", "");
			}
		}

		// Быстрая кнопка 7, код
        public string Qbtn07_id
		{
			get
			{
				if (Key.GetValue("Quick btn id 07").ToString() != "")
					return Key.GetValue("Quick btn id 07").ToString();
				else
					return "0";
			}
			set
			{
				if (value != "0")
					Key.SetValue("Quick btn id 07", value.ToString());
				else
					Key.SetValue("Quick btn id 07", "");
			}
		}

		// Быстрая кнопка 7, текст
		public string Qbtn07_text
		{
			get
			{
				if (Key.GetValue("Quick btn string 07").ToString() != "")
					return Key.GetValue("Quick btn string 07").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn string 07", value);
				else
					Key.SetValue("Quick btn string 07", "");
			}
		}

		// Быстрая кнопка 8, код
        public string Qbtn08_id
		{
			get
			{
				if (Key.GetValue("Quick btn id 08").ToString() != "")
					return Key.GetValue("Quick btn id 08").ToString();
				else
					return "0";
			}
			set
			{
				if (value != "0")
					Key.SetValue("Quick btn id 08", value.ToString());
				else
					Key.SetValue("Quick btn id 08", "");
			}
		}

		// Быстрая кнопка 8, текст
		public string Qbtn08_text
		{
			get
			{
				if (Key.GetValue("Quick btn string 08").ToString() != "")
					return Key.GetValue("Quick btn string 08").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn string 08", value);
				else
					Key.SetValue("Quick btn string 08", "");
			}
		}

		// Быстрая кнопка 9, код
        public string Qbtn09_id
		{
			get
			{
				if (Key.GetValue("Quick btn id 09").ToString() != "")
					return Key.GetValue("Quick btn id 09").ToString();
				else
					return "0";
			}
			set
			{
				if (value != "0")
					Key.SetValue("Quick btn id 09", value.ToString());
				else
					Key.SetValue("Quick btn id 09", "");
			}
		}

		// Быстрая кнопка 9, текст
		public string Qbtn09_text
		{
			get
			{
				if (Key.GetValue("Quick btn string 09").ToString() != "")
					return Key.GetValue("Quick btn string 09").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn string 09", value);
				else
					Key.SetValue("Quick btn string 09", "");
			}
		}

		// Быстрая кнопка 10, код
        public string Qbtn10_id
		{
			get
			{
				if (Key.GetValue("Quick btn id 10").ToString() != "")
					return Key.GetValue("Quick btn id 10").ToString();
				else
					return "0";
			}
			set
			{
				if (value != "0")
					Key.SetValue("Quick btn id 10", value.ToString());
				else
					Key.SetValue("Quick btn id 10", "");
			}
		}

		// Быстрая кнопка 10, текст
		public string Qbtn10_text
		{
			get
			{
				if (Key.GetValue("Quick btn string 10").ToString() != "")
					return Key.GetValue("Quick btn string 10").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn string 10", value);
				else
					Key.SetValue("Quick btn string 10", "");
			}
		}

		// Быстрая кнопка 1, сокр текст
		public string Qbtn01_stext
		{
			get
			{
				if (Key.GetValue("Quick btn s string 01").ToString() != "")
					return Key.GetValue("Quick btn s string 01").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn s string 01", value);
				else
					Key.SetValue("Quick btn s string 01", "");
			}
		}

		// Быстрая кнопка 2, сокр текст
		public string Qbtn02_stext
		{
			get
			{
				if (Key.GetValue("Quick btn s string 02").ToString() != "")
					return Key.GetValue("Quick btn s string 02").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn s string 02", value);
				else
					Key.SetValue("Quick btn s string 02", "");
			}
		}

		// Быстрая кнопка 3, сокр текст
		public string Qbtn03_stext
		{
			get
			{
				if (Key.GetValue("Quick btn s string 03").ToString() != "")
					return Key.GetValue("Quick btn s string 03").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn s string 03", value);
				else
					Key.SetValue("Quick btn s string 03", "");
			}
		}

		// Быстрая кнопка 4, сокр текст
		public string Qbtn04_stext
		{
			get
			{
				if (Key.GetValue("Quick btn s string 04").ToString() != "")
					return Key.GetValue("Quick btn s string 04").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn s string 04", value);
				else
					Key.SetValue("Quick btn s string 04", "");
			}
		}

		// Быстрая кнопка 5, сокр текст
		public string Qbtn05_stext
		{
			get
			{
				if (Key.GetValue("Quick btn s string 05").ToString() != "")
					return Key.GetValue("Quick btn s string 05").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn s string 05", value);
				else
					Key.SetValue("Quick btn s string 05", "");
			}
		}

		// Быстрая кнопка 6, сокр текст
		public string Qbtn06_stext
		{
			get
			{
				if (Key.GetValue("Quick btn s string 06").ToString() != "")
					return Key.GetValue("Quick btn s string 06").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn s string 06", value);
				else
					Key.SetValue("Quick btn s string 06", "");
			}
		}

		// Быстрая кнопка 7, сокр текст
		public string Qbtn07_stext
		{
			get
			{
				if (Key.GetValue("Quick btn s string 07").ToString() != "")
					return Key.GetValue("Quick btn s string 07").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn s string 07", value);
				else
					Key.SetValue("Quick btn s string 07", "");
			}
		}

		// Быстрая кнопка 8, сокр текст
		public string Qbtn08_stext
		{
			get
			{
				if (Key.GetValue("Quick btn s string 08").ToString() != "")
					return Key.GetValue("Quick btn s string 08").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn s string 08", value);
				else
					Key.SetValue("Quick btn s string 08", "");
			}
		}

		// Быстрая кнопка 9, сокр текст
		public string Qbtn09_stext
		{
			get
			{
				if (Key.GetValue("Quick btn s string 09").ToString() != "")
					return Key.GetValue("Quick btn s string 09").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn s string 09", value);
				else
					Key.SetValue("Quick btn s string 09", "");
			}
		}

		// Быстрая кнопка 10, сокр текст
		public string Qbtn10_stext
		{
			get
			{
				if (Key.GetValue("Quick btn s string 10").ToString() != "")
					return Key.GetValue("Quick btn s string 10").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Quick btn s string 10", value);
				else
					Key.SetValue("Quick btn s string 10", "");
			}
		}

		// время обновления таблицы платежей
		public int UpdatePaymentTable
		{
			get
			{
				if (Key.GetValue("Update payment table").ToString() != "")
					return int.Parse(Key.GetValue("Update payment table").ToString());
				else
					return 0;
			}
			set
			{
				if (value != 0)
					Key.SetValue("Update payment table", value.ToString());
				else
					Key.SetValue("Update payment table", "0");
			}
		}

		// время обновления таблицы заказов на приемке
		public int UpdateOrderTableInAcceptance
		{
			get
			{
				if (Key.GetValue("Update order table in acceptance").ToString() != "")
					return int.Parse(Key.GetValue("Update order table in acceptance").ToString());
				else
					return 0;
			}
			set
			{
				if (value != 0)
					Key.SetValue("Update order table in acceptance", value.ToString());
				else
					Key.SetValue("Update order table in acceptance", "0");
			}
		}

		// Путь к файлу шаблонов отчетов
		public string PathReportsTemplates
		{
			get
			{
				if (Key.GetValue("Public ini").ToString() != "")
				{
					if (File.Exists(Key.GetValue("Public ini").ToString()))
					{
						if (IniFile.IniReadValue("PSA", "Path Reports Templates", Key.GetValue("Public ini").ToString(), "") != "")
						{
							return IniFile.IniReadValue("PSA", "Path Reports Templates", Key.GetValue("Public ini").ToString(), "");
						}
						else
						{
							if (Key.GetValue("Path Reports Templates").ToString() != "")
								return Key.GetValue("Path Reports Templates").ToString();
							else
								return "";
						}
					}
					else
					{
						if (Key.GetValue("Path Reports Templates").ToString() != "")
							return Key.GetValue("Path Reports Templates").ToString();
						else
							return "";
					}
				}
				else
				{
					if (Key.GetValue("Path Reports Templates").ToString() != "")
						return Key.GetValue("Path Reports Templates").ToString();
					else
						return "";
				}
			}
			set
			{
				if (value != "")
					Key.SetValue("Path Reports Templates", value);
				else
					Key.SetValue("Path Reports Templates", "");
			}
		}


		// Цветовая раскраска таблицы заказов на приемке
		public bool Color_rows_in_order
		{
			get
			{
				if (Key.GetValue("Color rows in order").ToString() != "")
					return bool.Parse(Key.GetValue("Color rows in order").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Color rows in order", value.ToString());
				else
					Key.SetValue("Color rows in order", false.ToString());
			}
		}

		// делать окно модуля оператор выплывающим
		public bool Fly_window_operator
		{
			get
			{
				if (Key.GetValue("Fly window operator").ToString() != "")
					return bool.Parse(Key.GetValue("Fly window operator").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Fly window operator", value.ToString());
				else
					Key.SetValue("Fly window operator", false.ToString());
			}
		}

		// Модуль оператора поверх всех окон
		public bool Mod_operator_top_most
		{
			get
			{
				if (Key.GetValue("Mod operator top most").ToString() != "")
					return bool.Parse(Key.GetValue("Mod operator top most").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Mod operator top most", value.ToString());
				else
					Key.SetValue("Mod operator top most", false.ToString());
			}
		}

        // время обновления таблицы заказов у оператора
        public int UpdateOrderTableInOperator
        {
            get
            {
                if (Key.GetValue("Update order operator").ToString() != "")
                    return int.Parse(Key.GetValue("Update order operator").ToString());
                else
                    return 10;
            }
            set
            {
                if (value != 0)
                    Key.SetValue("Update order operator", value.ToString());
                else
                    Key.SetValue("Update order operator", "10");
            }
        }

		// Модуль дизайнера поверх всех окон
		public bool Mod_designer_top_most
		{
			get
			{
				if (Key.GetValue("Mod designer top most").ToString() != "")
					return bool.Parse(Key.GetValue("Mod designer top most").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Mod designer top most", value.ToString());
				else
					Key.SetValue("Mod designer top most", false.ToString());
			}
		}

		// время обновления таблицы заказов у дизайнера
		public int UpdateOrderTableInDesigner
		{
			get
			{
				if (Key.GetValue("Update order designer").ToString() != "")
					return int.Parse(Key.GetValue("Update order designer").ToString());
				else
					return 10;
			}
			set
			{
				if (value != 0)
					Key.SetValue("Update order designer", value.ToString());
				else
					Key.SetValue("Update order designer", "10");
			}
		}

		// делать окно модуля оператор выплывающим
		public bool Fly_window_designer
		{
			get
			{
				if (Key.GetValue("Fly window designer").ToString() != "")
					return bool.Parse(Key.GetValue("Fly window designer").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Fly window designer", value.ToString());
				else
					Key.SetValue("Fly window designer", false.ToString());
			}
		}

		// Каталог импорта
		public string Dir_import
		{
			get
			{
				if (Key.GetValue("Dir import").ToString() != "")
					return Key.GetValue("Dir import").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Dir import", value);
				else
					Key.SetValue("Dir import", "");
			}
		}

		// Каталог экспорта
		public string Dir_export
		{
			get
			{
				if (Key.GetValue("Dir export").ToString() != "")
					return Key.GetValue("Dir export").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Dir export", value);
				else
					Key.SetValue("Dir export", "");
			}
		}

		// Анимация иконки в модуле робота
		public bool Robot_animation_icon
		{
			get
			{
				if (Key.GetValue("Robot animation icon").ToString() != "")
					return bool.Parse(Key.GetValue("Robot animation icon").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Robot animation icon", value.ToString());
				else
					Key.SetValue("Robot animation icon", false.ToString());
			}
		}

		// Шаблон загрузки машин 
		public string SQL_Import_Template_Mashine
		{
			get
			{
				if (Key.GetValue("SQL Import Template Mashine").ToString() != "")
					return Key.GetValue("SQL Import Template Mashine").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("SQL Import Template Mashine", value);
				else
					Key.SetValue("SQL Import Template Mashine", "");
			}
		}

		// Шаблон загрузки дисконтных карт
		public string SQL_Import_Template_DCard
		{
			get
			{
				if (Key.GetValue("SQL Import Template DCard").ToString() != "")
					return Key.GetValue("SQL Import Template DCard").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("SQL Import Template DCard", value);
				else
					Key.SetValue("SQL Import Template DCard", "");
			}
		}

		// Шаблон загрузки материалов
		public string SQL_Import_Template_Material
		{
			get
			{
				if (Key.GetValue("SQL Import Template Material").ToString() != "")
					return Key.GetValue("SQL Import Template Material").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("SQL Import Template Material", value);
				else
					Key.SetValue("SQL Import Template Material", "");
			}
		}

		// Шаблон загрузки товаров и услуг
		public string SQL_Import_Template_Good
		{
			get
			{
				if (Key.GetValue("SQL Import Template Good").ToString() != "")
					return Key.GetValue("SQL Import Template Good").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("SQL Import Template Good", value);
				else
					Key.SetValue("SQL Import Template Good", "");
			}
		}

		// Первый рекламный блок на чеке
		public string ReklamBlock1
		{
			get
			{
				if (Key.GetValue("Public ini").ToString() != "")
				{
					if (File.Exists(Key.GetValue("Public ini").ToString()))
					{
						if (IniFile.IniReadValue("PSA", "Reklam Block 1", Key.GetValue("Public ini").ToString(), "") != "")
						{
							return IniFile.IniReadValue("PSA", "Reklam Block 1", Key.GetValue("Public ini").ToString(), "").Replace("<br>", "\r\n");
						}
						else
						{
							if (Key.GetValue("Reklam Block 1").ToString() != "")
								return Key.GetValue("Reklam Block 1").ToString();
							else
								return "";
						}
					}
					else
					{
						if (Key.GetValue("Reklam Block 1").ToString() != "")
							return Key.GetValue("Reklam Block 1").ToString();
						else
							return "";
					}
				}
				else
				{
					if (Key.GetValue("Reklam Block 1").ToString() != "")
						return Key.GetValue("Reklam Block 1").ToString();
					else
						return "";
				}
			}
			set
			{
				if (value != "")
					Key.SetValue("Reklam Block 1", value);
				else
					Key.SetValue("Reklam Block 1", "");
			}
		}

		// Показывать предпросмотр чека
		public bool CheckPreview
		{
			get
			{
				if (Key.GetValue("Debug: Check preview").ToString() != "")
					return bool.Parse(Key.GetValue("Debug: Check preview").ToString());
				else
					return false;
			}
			set
			{
				if (value != false)
					Key.SetValue("Debug: Check preview", value.ToString());
				else
					Key.SetValue("Debug: Check preview", false.ToString());
			}
		}

		// Количество печатаемых чеков
		public int CheckCount
		{
			get
			{
				if (Key.GetValue("Check count").ToString() != "")
					return int.Parse(Key.GetValue("Check count").ToString());
				else
					return 2;
			}
			set
			{
				if (value != 0)
					Key.SetValue("Check count", value.ToString());
				else
					Key.SetValue("Check count", "2");
			}
		}

		// Мастер пароль 1
		public string PasswordClass1
		{
			get
			{
				if (Key.GetValue("Public ini").ToString() != "")
				{
					if (File.Exists(Key.GetValue("Public ini").ToString()))
					{
						if (IniFile.IniReadValue("PSA", "Password class 1", Key.GetValue("Public ini").ToString(), "") != "")
						{
							return IniFile.IniReadValue("PSA", "Password class 1", Key.GetValue("Public ini").ToString(), "");
						}
						else
						{
							if (Key.GetValue("Password class 1").ToString() != "")
								return Key.GetValue("Password class 1").ToString();
							else
								return "pas12";
						}
					}
					else
					{
						if (Key.GetValue("Password class 1").ToString() != "")
							return Key.GetValue("Password class 1").ToString();
						else
							return "pas12";
					}
				}
				else
				{
					if (Key.GetValue("Password class 1").ToString() != "")
						return Key.GetValue("Password class 1").ToString();
					else
						return "pas12";
				}
			}
			set
			{
				if (value != "")
					Key.SetValue("Password class 1", value);
				else
					Key.SetValue("Password class 1", "pas12");
			}
		}

		// 
		public string SMTPServer
		{
			get
			{
				if (Key.GetValue("SMTP Server").ToString() != "")
					return Key.GetValue("SMTP Server").ToString();
				else
					return "int.fotoland.ru";
			}
			set
			{
				if (value != "")
					Key.SetValue("SMTP Server", value);
				else
					Key.SetValue("SMTP Server", "int.fotoland.ru");
			}
		}

		public bool SMTPAut
		{
			get
			{
				if (Key.GetValue("SMTP Aut").ToString() != "")
					return bool.Parse(Key.GetValue("SMTP Aut").ToString());
				else
					return true;
			}
			set
			{
				if (value.ToString() != "")
					Key.SetValue("SMTP Aut", value);
				else
					Key.SetValue("SMTP Aut", true.ToString());
			}
		}

		public string SMTPUserFrom
		{
			get
			{
				if (Key.GetValue("SMTP User From").ToString() != "")
					return Key.GetValue("SMTP User From").ToString();
				else
					return "psa";
			}
			set
			{
				if (value != "")
					Key.SetValue("SMTP User From", value);
				else
					Key.SetValue("SMTP User From", "psa");
			}
		}

		public string SMTPUserFromEmail
		{
			get
			{
				if (Key.GetValue("SMTP User From Email").ToString() != "")
					return Key.GetValue("SMTP User From Email").ToString();
				else
					return "psa@fotoland.ru";
			}
			set
			{
				if (value != "")
					Key.SetValue("SMTP User From Email", value);
				else
					Key.SetValue("SMTP User From Email", "psa@fotoland.ru");
			}
		}

		public string SMTPPasswordFrom
		{
			get
			{
				if (Key.GetValue("SMTP Password From").ToString() != "")
					return Key.GetValue("SMTP Password From").ToString();
				else
					return "psa1029";
			}
			set
			{
				if (value != "")
					Key.SetValue("SMTP Password From", value);
				else
					Key.SetValue("SMTP Password From", "psa1029");
			}
		}


		public string SMTPUserTo
		{
			get
			{
				if (Key.GetValue("SMTP User To").ToString() != "")
					return Key.GetValue("SMTP User To").ToString();
				else
					return "psa@fotoland.ru";
			}
			set
			{
				if (value != "")
					Key.SetValue("SMTP User To", value);
				else
					Key.SetValue("SMTP User To", "psa@fotoland.ru");
			}
		}

		public bool AutoSendCrashReport
		{
			get
			{
				if (Key.GetValue("Auto send crash report").ToString() != "")
					return bool.Parse(Key.GetValue("Auto send crash report").ToString());
				else
					return true;
			}
			set
			{
				if (value.ToString() != "")
					Key.SetValue("Auto send crash report", value);
				else
					Key.SetValue("Auto send crash report", true.ToString());
			}
		}

		public string FTP_Server
		{
			get
			{
				if (Key.GetValue("FTP Server").ToString() != "")
					return Key.GetValue("FTP Server").ToString();
				else
					return "int.fotoland.ru";
			}
			set
			{
				if (value != "")
					Key.SetValue("FTP Server", value);
				else
					Key.SetValue("FTP Server", "int.fotoland.ru");
			}
		}

        public string FTP_Server_Export
        {
            get
            {
                if (Key.GetValue("FTP Server Export").ToString() != "")
                    return Key.GetValue("FTP Server Export").ToString();
                else
                    return "int.fotoland.ru";
            }
            set
            {
                if (value != "")
                    Key.SetValue("FTP Server Export", value);
                else
                    Key.SetValue("FTP Server Export", "int.fotoland.ru");
            }
        }
        
        public string FTP_User
		{
			get
			{
				if (Key.GetValue("FTP User").ToString() != "")
					return Key.GetValue("FTP User").ToString();
				else
					return "ftp";
			}
			set
			{
				if (value != "")
					Key.SetValue("FTP User", value);
				else
					Key.SetValue("FTP User", "ftp");
			}
		}
		public string FTP_Password
		{
			get
			{
				if (Key.GetValue("FTP Password").ToString() != "")
					return Key.GetValue("FTP Password").ToString();
				else
					return "fotoftp";
			}
			set
			{
				if (value != "")
					Key.SetValue("FTP Password", value);
				else
					Key.SetValue("FTP Password", "fotoftp");
			}
		}

        public string FTP_Path
        {
            get
            {
                if (Key.GetValue("FTP Path").ToString() != "")
                    return Key.GetValue("FTP Path").ToString();
                else
                    return "PSA/XXX";
            }
            set
            {
                if (value != "")
                    Key.SetValue("FTP Path", value);
                else
                    Key.SetValue("FTP Path", "PSA/XXX");
            }
        }

        public string FTP_Path_Export
        {
            get
            {
                if (Key.GetValue("FTP Path Export").ToString() != "")
                    return Key.GetValue("FTP Path Export").ToString();
                else
                    return "PSA/XXX";
            }
            set
            {
                if (value != "")
                    Key.SetValue("FTP Path Export", value);
                else
                    Key.SetValue("FTP Path Export", "PSA/XXX");
            }
        }

        public bool Export_from_ftp
        {
            get
            {
                if (Key.GetValue("Export from ftp").ToString() != "")
                    return bool.Parse(Key.GetValue("Export from ftp").ToString());
                else
                    return true;
            }
            set
            {
                if (value.ToString() != "")
                    Key.SetValue("Export from ftp", value);
                else
                    Key.SetValue("Export from ftp", true.ToString());
            }
        }

        public bool Import_from_ftp
        {
            get
            {
                if (Key.GetValue("Import from ftp").ToString() != "")
                    return bool.Parse(Key.GetValue("Import from ftp").ToString());
                else
                    return true;
            }
            set
            {
                if (value.ToString() != "")
                    Key.SetValue("Import from ftp", value);
                else
                    Key.SetValue("Import from ftp", true.ToString());
            }
        }

        public int Import_time
        {
            get
            {
                if (Key.GetValue("Import time").ToString() != "")
                    return int.Parse(Key.GetValue("Import time").ToString());
                else
                    return 2;
            }
            set
            {
                if (value != 0)
                    Key.SetValue("Import time", value.ToString());
                else
                    Key.SetValue("Import time", "60");
            }
        }

        public int Export_time
        {
            get
            {
                if (Key.GetValue("Export time").ToString() != "")
                    return int.Parse(Key.GetValue("Export time").ToString());
                else
                    return 2;
            }
            set
            {
                if (value != 0)
                    Key.SetValue("Export time", value.ToString());
                else
                    Key.SetValue("Export time", "60");
            }
        }

        public bool ShortGoodsListInWizard
        {
            get
            {
                if (Key.GetValue("Short Goods List In Wizard").ToString() != "")
                    return bool.Parse(Key.GetValue("Short Goods List In Wizard").ToString());
                else
                    return true;
            }
            set
            {
                if (value.ToString() != "")
                    Key.SetValue("Short Goods List In Wizard", value);
                else
                    Key.SetValue("Short Goods List In Wizard", true.ToString());
            }
        }

        public bool ExportDoCopy
        {
            get
            {
                if (Key.GetValue("Export do copy").ToString() != "")
                    return bool.Parse(Key.GetValue("Export do copy").ToString());
                else
                    return true;
            }
            set
            {
                if (value.ToString() != "")
                    Key.SetValue("Export do copy", value);
                else
                    Key.SetValue("Export do copy", true.ToString());
            }
        }

		public bool ExportClearDirAfterCopy
		{
			get
			{
				if (Key.GetValue("Export Clear Dir After Copy").ToString() != "")
					return bool.Parse(Key.GetValue("Export Clear Dir After Copy").ToString());
				else
					return true;
			}
			set
			{
				if (value.ToString() != "")
					Key.SetValue("Export Clear Dir After Copy", value);
				else
					Key.SetValue("Export Clear Dir After Copy", true.ToString());
			}
		}

		public int ModelRound
		{
			get
			{
				if (Key.GetValue("Model round").ToString() != "")
					return int.Parse(Key.GetValue("Model round").ToString());
				else
					return 5;
			}
			set
			{
				if (value.ToString() != "")
					Key.SetValue("Model round", value);
				else
					Key.SetValue("Model round", 5);
			}
		}

		public bool QueryForDelete
		{
			get
			{
				if (Key.GetValue("Query for delete").ToString() != "")
					return bool.Parse(Key.GetValue("Query for delete").ToString());
				else
					return true;
			}
			set
			{
				if (value.ToString() != "")
					Key.SetValue("Query for delete", value);
				else
					Key.SetValue("Query for delete", true);
			}
		}

		public bool DenyDelete
		{
			get
			{
				if (Key.GetValue("Deny delete").ToString() != "")
					return bool.Parse(Key.GetValue("Deny delete").ToString());
				else
					return false;
			}
			set
			{
				if (value.ToString() != "")
					Key.SetValue("Deny delete", value);
				else
					Key.SetValue("Deny delete", false);
			}
		}

		public int DCard_limit
		{
			get
			{
				if (Key.GetValue("Public ini").ToString() != "")
				{
					if (File.Exists(Key.GetValue("Public ini").ToString()))
					{
						if (int.Parse(IniFile.IniReadValue("PSA", "DCard limit", Key.GetValue("Public ini").ToString(), "-1")) != -1)
						{
							return int.Parse(IniFile.IniReadValue("PSA", "DCard limit", Key.GetValue("Public ini").ToString(), "-1"));
						}
						else
						{
							if (Key.GetValue("DCard limit").ToString() != "")
								return int.Parse(Key.GetValue("DCard limit").ToString());
							else
								return 2;
						}
					}
					else
					{
						if (Key.GetValue("DCard limit").ToString() != "")
							return int.Parse(Key.GetValue("DCard limit").ToString());
						else
							return 2;
					}
				}
				else
				{
					if (Key.GetValue("DCard limit").ToString() != "")
						return int.Parse(Key.GetValue("DCard limit").ToString());
					else
						return 2;
				}
			}
			set
			{
				if (value != 0)
					Key.SetValue("DCard limit", value.ToString());
				else
					Key.SetValue("DCard limit", "3");
			}
		}

		public string PublicIni
		{
			get
			{
				if (Key.GetValue("Public ini") != "")
					return Key.GetValue("Public ini").ToString();
				else
					return "";
			}
			set
			{
				if (value != "")
					Key.SetValue("Public ini", value);
				else
					Key.SetValue("Public ini", "");
			}
		}

		// конструктор
		public PropertiesDeleted()
		{
			Key = Registry.CurrentUser.OpenSubKey("Software\\Photoland\\System Automation\\", true);
			if (Key == null)
				Key = Registry.CurrentUser.CreateSubKey("Software\\Photoland\\System Automation\\");

			if (Key.GetValue("Connection type") == null)
				Key.SetValue("Connection type", "");

			if (Key.GetValue("Database DSN") == null)
				Key.SetValue("Database DSN", "");

			if (Key.GetValue("Database server") == null)
				Key.SetValue("Database server", "");

			if (Key.GetValue("Database user") == null)
				Key.SetValue("Database user", "");

			if (Key.GetValue("Database password") == null)
				Key.SetValue("Database password", "");

			if (Key.GetValue("Database base") == null)
				Key.SetValue("Database base", "");

			if (Key.GetValue("Connection string") == null)
				Key.SetValue("Connection string", "");

			if (Key.GetValue("Run one copy mod acceptance") == null)
				Key.SetValue("Run one copy mod acceptance", false.ToString());

			if (Key.GetValue("Run one copy mod operator") == null)
				Key.SetValue("Run one copy mod operator", false.ToString());

			if (Key.GetValue("Run one copy mod designer") == null)
				Key.SetValue("Run one copy mod designer", false.ToString());

			if (Key.GetValue("Run one copy mod admin") == null)
				Key.SetValue("Run one copy mod admin", false.ToString());

			if (Key.GetValue("Run one copy mod robot") == null)
				Key.SetValue("Run one copy mod robot", false.ToString());

			if (Key.GetValue("Dir print") == null)
				Key.SetValue("Dir print", "");

			if (Key.GetValue("Dir edit") == null)
				Key.SetValue("Dir edit", "");

			if (Key.GetValue("Search SubDir") == null)
				Key.SetValue("Search SubDir", false.ToString());

			if (Key.GetValue("List of files") == null)
				Key.SetValue("List of files", "jpg, jpeg, bmp, gif, tif");

			if (Key.GetValue("Dir rescan") == null)
				Key.SetValue("Dir rescan", "5");

			if (Key.GetValue("Time for output") == null)
				Key.SetValue("Time for output", "1");

			if (Key.GetValue("Time begin work") == null)
				Key.SetValue("Time begin work", "8");

			if (Key.GetValue("Time end work") == null)
				Key.SetValue("Time end work", "22");

			if (Key.GetValue("Order prefics") == null)
				Key.SetValue("Order prefics", "00");

			if (Key.GetValue("Quick btn id 01") == null)
				Key.SetValue("Quick btn id 01", "");

			if (Key.GetValue("Quick btn string 01") == null)
				Key.SetValue("Quick btn string 01", "");

			if (Key.GetValue("Quick btn id 02") == null)
				Key.SetValue("Quick btn id 02", "");

			if (Key.GetValue("Quick btn string 02") == null)
				Key.SetValue("Quick btn string 02", "");

			if (Key.GetValue("Quick btn id 03") == null)
				Key.SetValue("Quick btn id 03", "");

			if (Key.GetValue("Quick btn string 03") == null)
				Key.SetValue("Quick btn string 03", "");

			if (Key.GetValue("Quick btn id 04") == null)
				Key.SetValue("Quick btn id 04", "");

			if (Key.GetValue("Quick btn string 04") == null)
				Key.SetValue("Quick btn string 04", "");

			if (Key.GetValue("Quick btn id 05") == null)
				Key.SetValue("Quick btn id 05", "");

			if (Key.GetValue("Quick btn string 05") == null)
				Key.SetValue("Quick btn string 05", "");

			if (Key.GetValue("Quick btn id 06") == null)
				Key.SetValue("Quick btn id 06", "");

			if (Key.GetValue("Quick btn string 06") == null)
				Key.SetValue("Quick btn string 06", "");

			if (Key.GetValue("Quick btn id 07") == null)
				Key.SetValue("Quick btn id 07", "");

			if (Key.GetValue("Quick btn string 07") == null)
				Key.SetValue("Quick btn string 07", "");

			if (Key.GetValue("Quick btn id 08") == null)
				Key.SetValue("Quick btn id 08", "");

			if (Key.GetValue("Quick btn string 08") == null)
				Key.SetValue("Quick btn string 08", "");

			if (Key.GetValue("Quick btn id 09") == null)
				Key.SetValue("Quick btn id 09", "");

			if (Key.GetValue("Quick btn string 09") == null)
				Key.SetValue("Quick btn string 09", "");

			if (Key.GetValue("Quick btn id 10") == null)
				Key.SetValue("Quick btn id 10", "");

			if (Key.GetValue("Quick btn string 10") == null)
				Key.SetValue("Quick btn string 10", "");

			if (Key.GetValue("Quick btn s string 01") == null)
				Key.SetValue("Quick btn s string 01", "");

			if (Key.GetValue("Quick btn s string 02") == null)
				Key.SetValue("Quick btn s string 02", "");

			if (Key.GetValue("Quick btn s string 03") == null)
				Key.SetValue("Quick btn s string 03", "");

			if (Key.GetValue("Quick btn s string 04") == null)
				Key.SetValue("Quick btn s string 04", "");

			if (Key.GetValue("Quick btn s string 05") == null)
				Key.SetValue("Quick btn s string 05", "");

			if (Key.GetValue("Quick btn s string 06") == null)
				Key.SetValue("Quick btn s string 06", "");

			if (Key.GetValue("Quick btn s string 07") == null)
				Key.SetValue("Quick btn s string 07", "");

			if (Key.GetValue("Quick btn s string 08") == null)
				Key.SetValue("Quick btn s string 08", "");

			if (Key.GetValue("Quick btn s string 09") == null)
				Key.SetValue("Quick btn s string 09", "");

			if (Key.GetValue("Quick btn s string 10") == null)
				Key.SetValue("Quick btn s string 10", "");

			if (Key.GetValue("Update payment table") == null)
				Key.SetValue("Update payment table", "10");

			if (Key.GetValue("Update order table in acceptance") == null)
				Key.SetValue("Update order table in acceptance", "10");

			if (Key.GetValue("Path Reports Templates") == null)
				Key.SetValue("Path Reports Templates", "");

			if (Key.GetValue("Color rows in order") == null)
				Key.SetValue("Color rows in order", false.ToString());

			if (Key.GetValue("Fly window operator") == null)
				Key.SetValue("Fly window operator", false.ToString());

			if (Key.GetValue("Mod operator top most") == null)
				Key.SetValue("Mod operator top most", true.ToString());

			if (Key.GetValue("Update order operator") == null)
				Key.SetValue("Update order operator", "10");

			if (Key.GetValue("Mod designer top most") == null)
				Key.SetValue("Mod designer top most", true.ToString());

			if (Key.GetValue("Update order designer") == null)
				Key.SetValue("Update order designer", "10");

			if (Key.GetValue("Fly window designer") == null)
				Key.SetValue("Fly window designer", false.ToString());

			if (Key.GetValue("Dir import") == null)
				Key.SetValue("Dir import", "");

			if (Key.GetValue("Dir export") == null)
				Key.SetValue("Dir export", "");

			if (Key.GetValue("Robot animation icon") == null)
				Key.SetValue("Robot animation icon", true.ToString());

			if (Key.GetValue("SQL Import Template Mashine") == null)
				Key.SetValue("SQL Import Template Mashine", "INSERT INTO [mashine] ([id_mashine], [del], [mashine]) VALUES ('{<ID>}', {<DEL>}, '{<MASHINE>}')");

			if (Key.GetValue("SQL Import Template DCard") == null)
				Key.SetValue("SQL Import Template DCard", "INSERT INTO [dcard] ([code], [name], [disc], [discserv], [phone], [email]) VALUES ('{<CODE>}', '{<NAME>}', {<DISC>}, {<DISCSERV>}, '{<PHONE>}', '{<EMAIL>}')");

			if (Key.GetValue("SQL Import Template Material") == null)
				Key.SetValue("SQL Import Template Material", "INSERT INTO [material] ([id_material], [del], [material], [remainder]) VALUES ('{<ID>}', {<DEL>}, '{<MATERIAL>}', {<REMAINDER>})");

			if (Key.GetValue("SQL Import Template Good") == null)
                Key.SetValue("SQL Import Template Good", "INSERT INTO [good] ([id_good], [guid], [del], [name], [description], [prefix], [folder], [type], [checked], [sign], [apply_form], [EI], [zero]) VALUES ('{<ID>}', '{<GUID>}', {<DEL>}, '{<NAME>}', '{<DESCRIPTION>}', '{<PREFIX>}', '{<FOLDER>}', '{<TYPE>}', {<CHECKED>}, '{<SIGN>}', '{<APPLYFORM>}', {<EI>}, {<ZERO_PRICE>})");

			if (Key.GetValue("Reklam Block 1") == null)
				Key.SetValue("Reklam Block 1", "");

			if (Key.GetValue("Debug: Check preview") == null)
				Key.SetValue("Debug: Check preview", false.ToString());

			if (Key.GetValue("Check count") == null)
				Key.SetValue("Check count", "2");

			if (Key.GetValue("Password class 1") == null)
				Key.SetValue("Password class 1", "pas12");

			if (Key.GetValue("SMTP Server") == null)
				Key.SetValue("SMTP Server", "int.fotoland.ru");

			if (Key.GetValue("SMTP User From") == null)
				Key.SetValue("SMTP User From", "psa");

			if (Key.GetValue("SMTP User From Email") == null)
				Key.SetValue("SMTP User From Email", "psa@fotoland.ru");

			if (Key.GetValue("SMTP Password From") == null)
				Key.SetValue("SMTP Password From", "psa1029");

			if (Key.GetValue("SMTP User To") == null)
				Key.SetValue("SMTP User To", "psa@fotoland.ru");

			if (Key.GetValue("SMTP Aut") == null)
				Key.SetValue("SMTP Aut", true.ToString());

			if (Key.GetValue("Auto send crash report") == null)
				Key.SetValue("Auto send crash report", true.ToString());

            if (Key.GetValue("FTP Server") == null)
                Key.SetValue("FTP Server", "int.fotoland.ru");

            if (Key.GetValue("FTP Server Export") == null)
                Key.SetValue("FTP Server Export", "int.fotoland.ru");

            if (Key.GetValue("FTP User") == null)
                Key.SetValue("FTP User", "ftp");

            if (Key.GetValue("FTP User Export") == null)
                Key.SetValue("FTP User Export", "ftp");

            if (Key.GetValue("FTP Password") == null)
				Key.SetValue("FTP Password", "fotoftp");

            if (Key.GetValue("FTP Path") == null)
                Key.SetValue("FTP Path", "PSA/XXX");

            if (Key.GetValue("FTP Path Export") == null)
                Key.SetValue("FTP Path Export", "PSA/XXX");

            if (Key.GetValue("Export from ftp") == null)
                Key.SetValue("Export from ftp", true.ToString());

            if (Key.GetValue("Import from ftp") == null)
                Key.SetValue("Import from ftp", true.ToString());

            if (Key.GetValue("Export time") == null)
                Key.SetValue("Export time", "60");

            if (Key.GetValue("Import time") == null)
                Key.SetValue("Import time", "60");

            if (Key.GetValue("Short Goods List In Wizard") == null)
                Key.SetValue("Short Goods List In Wizard", true.ToString());

            if (Key.GetValue("Export do copy") == null)
                Key.SetValue("Export do copy", true.ToString());

			if (Key.GetValue("Export Clear Dir After Copy") == null)
				Key.SetValue("Export Clear Dir After Copy", true.ToString());

			if (Key.GetValue("Model round") == null)
				Key.SetValue("Model round", "5");

			if (Key.GetValue("Query for delete") == null)
				Key.SetValue("Query for delete", true.ToString());

			if (Key.GetValue("Deny delete") == null)
				Key.SetValue("Deny delete", false.ToString());

			if (Key.GetValue("DCard limit") == null)
				Key.SetValue("DCard limit", "3");

			if (Key.GetValue("Public ini") == null)
				Key.SetValue("Public ini", "");

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
