using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Net.Mail;
using Photoland.Security;
using System.Data.SqlClient;
using System.IO;

namespace Photoland.CrashInfo
{
	public partial class frmApplicationCrash : Form
	{
		public frmApplicationCrash()
		{
			InitializeComponent();
		}

		private PSA.Lib.Util.Settings prop = new PSA.Lib.Util.Settings();
		private string Body = "";


		// Внешний метод для заполнения информации
		public void FillInfo(Exception ex)
		{
			StringBuilder sb = new StringBuilder();
			FillExceptionInfo(ex, sb);
			txtInfo.Text = sb.ToString();
			txtInfo.SelectionLength = 0;
			ErrorNfo.WriteErrorInfo(ex);
			
		}

		// Внешнее свойство, определяет необходимость перезапуска программы
		public bool Restart
		{
			get
			{
				if (checkRestart.Checked == true)
					return true;
				else
					return false;
			}
		}

		// Закрытый метод составления информации об ошибках
		// если возникло несколько ошибок, то
		// метод рекурсивно будет вызывать сам себя
		private void FillExceptionInfo(Exception ex, StringBuilder sb)
		{
			sb.AppendFormat("------------------------\r\n");

			sb.AppendFormat("Произошла ошибка типа {0}\r\n", ex.GetType());
			sb.AppendFormat("Объект вызвавший ошибку: {0}\r\n", ex.Source);
			sb.AppendFormat("Ошибка произошла в методе {0}\r\n", ex.TargetSite);
			sb.AppendFormat("Основная информация об ошибке: {0}\r\n", ex.Message);
			sb.AppendFormat("Стек вызова: {0}\r\n\r\n", ex.StackTrace);

			sb.AppendFormat("------------------------\r\n");
			
			sb.AppendFormat("Machine name: {0}\r\n", Environment.MachineName);
			sb.AppendFormat("OS Version: {0}\r\n", Environment.OSVersion);
			sb.AppendFormat("Processor count: {0}\r\n", Environment.ProcessorCount);
			sb.AppendFormat("User domain name: {0}\r\n", Environment.UserDomainName);
			sb.AppendFormat("User name: {0}\r\n", Environment.UserName);
			sb.AppendFormat("User interactive: {0}\r\n", Environment.UserInteractive);
			sb.AppendFormat("dotNet version: {0}\r\n", Environment.Version);
			
			sb.AppendFormat("------------------------\r\n");

			Body = "<style>td {border-bottom: 1 sold black;}</style>";
			Body += "<div style=\"font-family:Verdana, Arial, Helvetica, sans-serif; font-size:12px; font-weight: bold;\">";
			Body += "<div style=\"background-color:#FFFFCC; text-align: right; font-size: 14px;\">Crash report<br />";
			Body += DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + "<br />";
			Body += "</div>";
			Body += "<div style=\"background-color: #C6E2FF; font-size: 11px; line-height: 16px;\">";
			Body += "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>Произошла ошибка типа:</b></td>";
			Body += "<td valign=\"top\"><span>" + ex.GetType().ToString().Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;") + "</span></td>";
			Body += "</tr>";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>Объект вызвавший ошибку:</b></td>";
			Body += "<td valign=\"top\"><span>" + ex.Source.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
			Body += "</span></td>";
			Body += "</tr>";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>Ошибка произошла в методе:</b></td>";
			Body += "<td valign=\"top\"><span>" + ex.TargetSite.ToString().Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;") + "</span></td>";
			Body += "</tr>";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>Основная информация об ошибке:</b></td>";
			Body += "<td valign=\"top\"><span>" + ex.Message.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
			Body += "</span></td>";
			Body += "</tr>";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>Стек вызова:</b></td>";
			Body += "<td valign=\"top\"><span>" +
					  ex.StackTrace.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
			Body += "</span></td>";
			Body += "</tr>";
			Body += "</table>";
			Body += "</div>";
			Body += "<div style=\"background-color:#D7FFEB; font-size: 11px; line-height: 16px;\">";
			Body += "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\">";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>Machine name:</b></td>";
			Body += "<td valign=\"top\"><span>" + Environment.MachineName.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;") + "</span></td>";
			Body += "</tr>";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>OS Version:</b></td>";
			Body += "<td valign=\"top\"><span>" + Environment.OSVersion.ToString().Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;") + "</span></td>";
			Body += "</tr>";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>Processor count:</b></td>";
			Body += "<td valign=\"top\"><span>" +
					  Environment.ProcessorCount.ToString().Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
			Body += "</span></td>";
			Body += "</tr>";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>User domain name:</b></td>";
			Body += "<td valign=\"top\"><span>" + Environment.UserDomainName.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;") + "</span></td>";
			Body += "</tr>";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>User name:</b></td>";
			Body += "<td valign=\"top\"><span>" +
					  Environment.UserName.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
			Body += "</span></td>";
			Body += "</tr>";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>User interactive:</b></td>";
			Body += "<td valign=\"top\"><span>" +
					  Environment.UserInteractive.ToString().Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;");
			Body += "</span></td>";
			Body += "</tr>";
			Body += "<tr>";
			Body += "<td width=\"250\" valign=\"top\"><b>dotNet version:</b></td>";
			Body += "<td valign=\"top\"><span>" + Environment.Version.ToString().Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;") + "</span></td>";
			Body += "</tr>";
			Body += "</table></div></div>";

			if (ex.InnerException != null) 
				FillExceptionInfo(ex.InnerException, sb);

			if(prop.AutoSendCrashReport)
				btnSendMail_Click(this, new EventArgs());
		}

		// Закрываем окно
		private void btnClose_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		// Отправляем отчет об ошибке
		private void btnSendMail_Click(object sender, EventArgs e)
		{
			try
			{
				MailAddress mFrom = new MailAddress(prop.SMTPUserFromEmail, "Система автоматизации");
				MailAddress mTo = new MailAddress(prop.SMTPUserTo);
				MailMessage m = new MailMessage(mFrom, mTo);
				m.Subject = "Crash report";
				m.IsBodyHtml = true;
				m.Body = Body;
				SmtpClient s = new SmtpClient(prop.SMTPServer);
				s.UseDefaultCredentials = prop.SMTPAut;
				if (prop.SMTPAut)
				{
					NetworkCredential c = new NetworkCredential(prop.SMTPUserFrom, prop.SMTPPasswordFrom);
					s.Credentials = c;
				}
				s.Send(m);
			}
			catch (Exception ex)
			{
				MessageBox.Show(
					"Неудалось отправть сообщение об ошибке!\n" + ex.Message + "\n" + ex.Source + "\n" + ex.StackTrace + "\n", "Ошибка",
					MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			finally
			{
				btnSendMail.Enabled = false;
			}
		}

		private void frmApplicationCrash_Load(object sender, EventArgs e)
		{
			//if (prop.AutoSendCrashReport)
			//{
			//	btnSendMail.Enabled = false;
			//}
		}
	}
}