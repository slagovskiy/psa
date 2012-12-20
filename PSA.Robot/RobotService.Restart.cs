using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using PSA.Lib.Util;
using System.Data;
using System.Net;
using Xceed.Ftp;
using Xceed.FileSystem;
using System.IO;

namespace PSA.Robot
{
    public partial class RobotService
    {

        public void RestartService()
        {
            try
            {
                /*
                 * net stop PSA.Robot
                 * net start PSA.Robot
                 */
                //System.Diagnostics.Process.Start("net.exe", "stop PSA.Robot");
                //System.Diagnostics.Process.Start("net.exe", "start PSA.Robot");
                string file = Guid.NewGuid().ToString() + ".cmd";
                using (System.IO.StreamWriter bat = new StreamWriter(System.IO.Path.GetTempPath() + "\\" + file))
                {
                    bat.WriteLine("net stop PSA.Robot");
                    bat.WriteLine("net start PSA.Robot");
                }
                System.Diagnostics.Process.Start(System.IO.Path.GetTempPath() + "\\" + file);
                File.Delete(file);
            }
            catch (Exception ex)
            {
                file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Глобальная ошибка по время отправления " + ex.Message);
                file.Flush();
            }
        }
    }
}
