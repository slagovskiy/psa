using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Photoland.Security;
using Photoland.Forms.Admin;

namespace Photoland.Lib
{
	public class fso
	{
		private static int _count_of_files;
		private static long _directory_size;
		private static PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();

		public static int Count_of_files(string directory, string filter, bool subdir, bool clear_last)
		{
			DirectoryInfo dir = new DirectoryInfo(directory);
			if (clear_last)
			{
				_count_of_files = 0;
				foreach (FileInfo fl in dir.GetFiles())
				{
					foreach (string ex in prop.List_of_files.ToLower().Replace(" ", "").Split(new Char[] { ',', ';', '.' }))
						if ("." + ex == fl.Extension.ToLower())
						{
							string subname = "";
							if (fl.Name.Replace(fl.Extension, "").Length > 6)
								subname = fl.Name.Replace(fl.Extension, "").Substring(fl.Name.Replace(fl.Extension, "").Length - 6);
							else
								subname = fl.Name.Replace(fl.Extension, "");
							if (subname.LastIndexOf("_") > 0)
							{
								if (subname.Substring(subname.LastIndexOf("_") + 1).Length < 5)
								{
									try
									{
										int t = int.Parse(subname.Substring(subname.LastIndexOf("_") + 1));
										if (t > 0)
											fso._count_of_files += t;
										else
											fso._count_of_files++;
									}
									catch(Exception exx)
									{
										//ErrorNfo.WriteErrorInfo(exx);
										fso._count_of_files++;
									}
								}
								else
								{
									fso._count_of_files++;
								}
							}
							else
							{
								fso._count_of_files++;
							}
							break;

						}
				}
			}
			if (subdir)
			{
				foreach (DirectoryInfo d in dir.GetDirectories())
				{
					Count_of_files(d.FullName, filter, true, false);
					foreach (FileInfo fl in d.GetFiles())
					{
						foreach (string ex in prop.List_of_files.ToLower().Replace(" ", "").Split(new Char[] { ',', ';', '.' }))
							if ("." + ex == fl.Extension.ToLower())
							{
								string subname = "";
								if (fl.Name.Replace(fl.Extension, "").Length > 6)
									subname = fl.Name.Replace(fl.Extension, "").Substring(fl.Name.Replace(fl.Extension, "").Length - 4);
								else
									subname = fl.Name.Replace(fl.Extension, "");
								if (subname.LastIndexOf("_") > 0)
								{
									if (subname.Substring(subname.LastIndexOf("_") + 1).Length < 5)
									{
										try
										{
											int t = int.Parse(subname.Substring(subname.LastIndexOf("_") + 1));
											if (t > 0)
												fso._count_of_files += t;
											else
												fso._count_of_files++;
										}
										catch(Exception exx)
										{
											ErrorNfo.WriteErrorInfo(exx);
											fso._count_of_files++;
										}
									}
									else
									{
										fso._count_of_files++;
									}
								}
								else
								{
									fso._count_of_files++;
								}
								break;
							}
					}
				}
			}
			return _count_of_files;
		}

		public static long Directory_Size(string directory, string filter, bool subdir, bool clear_last)
		{
			DirectoryInfo dir = new DirectoryInfo(directory);
			if (clear_last)
			{
				_directory_size = 0;
				foreach (FileInfo fl in dir.GetFiles())
				{
					foreach (string ex in prop.List_of_files.ToLower().Replace(" ", "").Split(new Char[] { ',', ';', '.' }))
						if ("." + ex == fl.Extension.ToLower())
						{
							fso._directory_size += fl.Length;
							break;
						}
				}
			}
			if (subdir)
			{
				foreach (DirectoryInfo d in dir.GetDirectories())
				{

					Directory_Size(d.FullName, filter, true, false);
					foreach (FileInfo fl in d.GetFiles())
					{
						foreach (string ex in prop.List_of_files.ToLower().Replace(" ", "").Split(new Char[] { ',', ';', '.' }))
							if ("." + ex == fl.Extension.ToLower())
							{
								fso._directory_size += fl.Length;
								break;
							}
					}
				}
			}
			return _directory_size;
		}

		public static List<string> GetListFilesForProc(string directory, string filter, bool subdir, bool clear_last)
		{
			List<string> files = new List<string>();

			DirectoryInfo dir = new DirectoryInfo(directory);
			if (clear_last)
			{
				foreach (FileInfo fl in dir.GetFiles())
				{
					foreach (string ex in prop.List_of_files.ToLower().Replace(" ", "").Split(new Char[] { ',', ';', '.' }))
						if ("." + ex == fl.Extension.ToLower())
						{
							files.Add(fl.FullName.Replace(directory, ""));
							break;
						}
				}
			}
			if (subdir)
			{
				foreach (DirectoryInfo d in dir.GetDirectories())
				{

					GetListFilesForProc(d.FullName, filter, true, false);
					foreach (FileInfo fl in d.GetFiles())
					{
						foreach (string ex in prop.List_of_files.ToLower().Replace(" ", "").Split(new Char[] { ',', ';', '.' }))
							if ("." + ex == fl.Extension.ToLower())
							{
								files.Add(fl.FullName.Replace(directory, ""));
								break;
							}
					}
				}
			}
			return files;
		}

		public static string GetDateSubFolders()
		{
			// Подпапки Год-месяц-день
			string tmp_subdirs = DateTime.Now.Year.ToString() + "\\";
			if (DateTime.Now.Month.ToString().Length == 1)
				tmp_subdirs += "0";
			tmp_subdirs += DateTime.Now.Month.ToString() + "\\";
			if (DateTime.Now.Day.ToString().Length == 1)
				tmp_subdirs += "0";
			tmp_subdirs += DateTime.Now.Day.ToString();
			return tmp_subdirs;
		}

		public static string GetDateSubFolders(string Date)
		{
			// Подпапки Год-месяц-день
			if (Date != "")
			{
				string tmp_subdirs = DateTime.Parse(Date).Year.ToString() + "\\";
				if (DateTime.Parse(Date).Month.ToString().Length == 1)
					tmp_subdirs += "0";
				tmp_subdirs += DateTime.Parse(Date).Month.ToString() + "\\";
				if (DateTime.Parse(Date).Day.ToString().Length == 1)
					tmp_subdirs += "0";
				tmp_subdirs += DateTime.Parse(Date).Day.ToString();
				return tmp_subdirs;
			}
			else
			{
				// Подпапки Год-месяц-день
				string tmp_subdirs = DateTime.Now.Year.ToString() + "\\";
				if (DateTime.Now.Month.ToString().Length == 1)
					tmp_subdirs += "0";
				tmp_subdirs += DateTime.Now.Month.ToString() + "\\";
				if (DateTime.Now.Day.ToString().Length == 1)
					tmp_subdirs += "0";
				tmp_subdirs += DateTime.Now.Day.ToString();
				return tmp_subdirs;
			}
		}

	}
}
