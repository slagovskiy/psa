using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Photoland.Forms.Interface.Util
{
	/// <summary>
	/// Класс для работы с ini файлами
	/// </summary>
	public class ini
	{
		public string path;

		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		/// <summary>
		/// Конструктор
		/// </summary>
		/// <PARAM name="INIPath"></PARAM>
		public ini(string INIPath)
		{
			path = INIPath;
		}
		/// <summary>
		/// Запись данных в ini файл
		/// </summary>
		/// <PARAM name="Section"></PARAM>
		/// Имя секции
		/// <PARAM name="Key"></PARAM>
		/// Имя ключа
		/// <PARAM name="Value"></PARAM>
		/// Значение ключа
		public void IniWriteValue(string Section, string Key, string Value)
		{
			WritePrivateProfileString(Section, Key, Value, this.path);
		}

		/// <summary>
		/// Запись данных в ini файл (Статически)
		/// </summary>
		/// Имя секции
		/// <PARAM name="Section"></PARAM>
		/// Имя ключа
		/// <PARAM name="Key"></PARAM>
		/// Значение ключа
		/// <PARAM name="Value"></PARAM>
		/// Путь к ini файлу
		/// <PARAM name="INIPath"></PARAM>
		
		static public void IniWriteValue(string Section, string Key, string Value, string INIPath)
		{
			WritePrivateProfileString(Section, Key, Value, INIPath);
		}

		/// <summary>
		/// Чтение данных из ini файла
		/// </summary>
		/// Имя секции
		/// <PARAM name="Section"></PARAM>
		/// Имя ключа
		/// <PARAM name="Key"></PARAM>
		/// Возвращается строка
		/// <returns></returns>
		public string IniReadValue(string Section, string Key)
		{
			StringBuilder temp = new StringBuilder(255);
			int i = GetPrivateProfileString(Section, Key, "", temp, 255, this.path);
			return temp.ToString();

		}

		/// <summary>
		/// Чтение данных из ini файла
		/// </summary>
		/// Имя секции
		/// <PARAM name="Section"></PARAM>
		/// Имя ключа
		/// <PARAM name="Key"></PARAM>
		/// Значение по умолчанию
		/// <PARAM name="defaultValue"></PARAM>
		/// Возвращается строка
		/// <returns></returns>
		public string IniReadValue(string Section, string Key, string defaultValue)
		{
			StringBuilder temp = new StringBuilder(255);
			int i = GetPrivateProfileString(Section, Key, defaultValue, temp, 255, this.path);
			return temp.ToString();

		}

		/// <summary>
		/// Чтение данных из ini файла
		/// </summary>
		/// Имя секции
		/// <PARAM name="Section"></PARAM>
		/// Имя ключа
		/// <PARAM name="Key"></PARAM>
		/// Путь к файлу
		/// <PARAM name="INIPath"></PARAM>
		/// Значение по умолчанию
		/// <PARAM name="defaultValue"></PARAM>
		/// Возвращается строка
		/// <returns></returns>
		static public string IniReadValue(string Section, string Key, string INIPath, string defaultValue)
		{
			StringBuilder temp = new StringBuilder(255);
			int i = GetPrivateProfileString(Section, Key, defaultValue, temp, 255, INIPath);
			return temp.ToString();

		}
	}
}
