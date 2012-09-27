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
                System.Diagnostics.Process.Start("net.exe", "stop PSA.Robot");
                System.Diagnostics.Process.Start("net.exe", "start PSA.Robot");
            }
            catch (Exception ex)
            {
                file.WriteLine(DateTime.Now.ToString("g", ci) + " [!] Глобальная ошибка по время отправления " + ex.Message);
                file.Flush();
            }
        }
    }
}
