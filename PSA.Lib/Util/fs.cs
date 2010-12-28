﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PSA.Lib.Util
{
	class fs
	{
		public static bool CopyDirectory(string SourcePath, string DestinationPath, bool overwriteexisting)
		{
			bool ret = false;
			try
			{
				SourcePath = SourcePath.EndsWith(@"\") ? SourcePath : SourcePath + @"\";
				DestinationPath = DestinationPath.EndsWith(@"\") ? DestinationPath : DestinationPath + @"\";

				if (Directory.Exists(SourcePath))
				{
					if (Directory.Exists(DestinationPath) == false)
						Directory.CreateDirectory(DestinationPath);

					foreach (string fls in Directory.GetFiles(SourcePath))
					{
						FileInfo flinfo = new FileInfo(fls);
						flinfo.CopyTo(DestinationPath + flinfo.Name, overwriteexisting);
					}
					foreach (string drs in Directory.GetDirectories(SourcePath))
					{
						DirectoryInfo drinfo = new DirectoryInfo(drs);
						if (CopyDirectory(drs, DestinationPath + drinfo.Name, overwriteexisting) == false)
							ret = false;
					}
				}
				ret = true;
			}
			catch (Exception ex)
			{
				ret = false;
			}
			return ret;
		}
	}
}
