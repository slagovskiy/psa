using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace PSA.Update.Util
{
	public class ftpClient
	{
		public string ftpServerIP;
		public string ftpUserID;
		public string ftpPassword;
		public List<string> Errors = new List<string>();

		public ftpClient(string _ftpServerIP, string _ftpUserID, string _ftpPassword)
		{
			ftpServerIP = _ftpServerIP;
			ftpUserID = _ftpUserID;
			ftpPassword = _ftpPassword;
		}

		public bool Upload(string filename, string tofilename)
		{
			FileInfo fileInf = new FileInfo(filename);
			string uri = "ftp://" + ftpServerIP + "/" + tofilename;
			FtpWebRequest reqFTP;

			reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + tofilename));
			reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
			reqFTP.KeepAlive = false;
			reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
			reqFTP.UseBinary = true;

			reqFTP.ContentLength = fileInf.Length;

			int buffLength = 2048;
			byte[] buff = new byte[buffLength];
			int contentLen;

			FileStream fs = fileInf.OpenRead();

			try
			{
				Stream strm = reqFTP.GetRequestStream();
				contentLen = fs.Read(buff, 0, buffLength);
				while (contentLen != 0)
				{
					strm.Write(buff, 0, contentLen);
					contentLen = fs.Read(buff, 0, buffLength);
				}

				strm.Close();
				fs.Close();
				return true;
			}
			catch(Exception ex)
			{
				Errors.Add(ex.Message);
				return false;
			}
		}


		public bool Delete(string fileName)
		{
			try
			{
				string uri = "ftp://" + ftpServerIP + "/" + fileName;
				FtpWebRequest reqFTP;
				reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));

				reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
				reqFTP.KeepAlive = false;
				reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

				string result = String.Empty;
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				long size = response.ContentLength;
				Stream datastream = response.GetResponseStream();
				StreamReader sr = new StreamReader(datastream);
				result = sr.ReadToEnd();
				sr.Close();
				datastream.Close();
				response.Close();
				return true;
			}
			catch (Exception ex)
			{
				Errors.Add(ex.Message);
				return false;
			}
		}

		private string[] GetFilesDetailList()
		{
			string[] downloadFiles;
			try
			{
				StringBuilder result = new StringBuilder();
				FtpWebRequest ftp;
				ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
				ftp.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
				ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
				WebResponse response = ftp.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				string line = reader.ReadLine();
				while (line != null)
				{
					result.Append(line);
					result.Append("\n");
					line = reader.ReadLine();
				}

				result.Remove(result.ToString().LastIndexOf("\n"), 1);
				reader.Close();
				response.Close();
				return result.ToString().Split('\n');
			}
			catch (Exception ex)
			{
				Errors.Add(ex.Message);
				downloadFiles = null;
				return downloadFiles;
			}
		}

		public string[] GetFileList()
		{
			string[] downloadFiles;
			StringBuilder result = new StringBuilder();
			FtpWebRequest reqFTP;
			try
			{
				reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/"));
				reqFTP.UseBinary = true;
				reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
				reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
				WebResponse response = reqFTP.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				string line = reader.ReadLine();
				while (line != null)
				{
					result.Append(line);
					result.Append("\n");
					line = reader.ReadLine();
				}
				result.Remove(result.ToString().LastIndexOf('\n'), 1);
				reader.Close();
				response.Close();
				return result.ToString().Split('\n');
			}
			catch (Exception ex)
			{
				Errors.Add(ex.Message);
				downloadFiles = null;
				return downloadFiles;
			}
		}

		public string[] GetFileList(string filePath)
		{
			string[] downloadFiles;
			StringBuilder result = new StringBuilder();
			FtpWebRequest reqFTP;
			try
			{
				reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + filePath + "/"));
				reqFTP.UseBinary = true;
				reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
				reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
				WebResponse response = reqFTP.GetResponse();
				StreamReader reader = new StreamReader(response.GetResponseStream());
				string line = reader.ReadLine();
				while (line != null)
				{
					result.Append(line);
					result.Append("\n");
					line = reader.ReadLine();
				}
				result.Remove(result.ToString().LastIndexOf('\n'), 1);
				reader.Close();
				response.Close();
				return result.ToString().Split('\n');
			}
			catch (Exception ex)
			{
				Errors.Add(ex.Message);
				downloadFiles = null;
				return downloadFiles;
			}
		}

		public bool Download(string filePath, string fileName)
		{
			FtpWebRequest reqFTP;
			try
			{
				FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

				reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileName));
				reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
				reqFTP.UseBinary = true;
				reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				Stream ftpStream = response.GetResponseStream();
				long cl = response.ContentLength;
				int bufferSize = 2048;
				int readCount;
				byte[] buffer = new byte[bufferSize];

				readCount = ftpStream.Read(buffer, 0, bufferSize);
				while (readCount > 0)
				{
					outputStream.Write(buffer, 0, readCount);
					readCount = ftpStream.Read(buffer, 0, bufferSize);
				}

				ftpStream.Close();
				outputStream.Close();
				response.Close();
				return true;
			}
			catch (Exception ex)
			{
				Errors.Add(ex.Message);
				return false;
			}
		}

		public bool Download(string filePath, string fileName, string fileServerPath)
		{
			FtpWebRequest reqFTP;
			try
			{
				FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

				reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileServerPath + "/" + fileName));
				reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
				reqFTP.UseBinary = true;
				reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				Stream ftpStream = response.GetResponseStream();
				long cl = response.ContentLength;
				int bufferSize = 2048;
				int readCount;
				byte[] buffer = new byte[bufferSize];

				readCount = ftpStream.Read(buffer, 0, bufferSize);
				while (readCount > 0)
				{
					outputStream.Write(buffer, 0, readCount);
					readCount = ftpStream.Read(buffer, 0, bufferSize);
				}

				ftpStream.Close();
				outputStream.Close();
				response.Close();
				return true;
			}
			catch (Exception ex)
			{
				Errors.Add(ex.Message);
				return false;
			}
		}

		public bool Download(string filePath, string fileName, string fileServerPath, string fileNameRemote)
		{
			FtpWebRequest reqFTP;
			try
			{
				using (FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create))
				{

					reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + fileServerPath + "/" + fileNameRemote));
					reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
					reqFTP.UseBinary = true;
					reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
					FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
					Stream ftpStream = response.GetResponseStream();
					long cl = response.ContentLength;
					int bufferSize = 2048;
					int readCount;
					byte[] buffer = new byte[bufferSize];

					readCount = ftpStream.Read(buffer, 0, bufferSize);
					while (readCount > 0)
					{
						outputStream.Write(buffer, 0, readCount);
						readCount = ftpStream.Read(buffer, 0, bufferSize);
					}

					ftpStream.Close();
					outputStream.Close();
					response.Close();
				}
				return true;
			}
			catch (Exception ex)
			{
				Errors.Add(ex.Message);
				return false;
			}
		}

		public long GetFileSize(string filename)
		{
			FtpWebRequest reqFTP;
			long fileSize = 0;
			try
			{
				reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + filename));
				reqFTP.Method = WebRequestMethods.Ftp.GetFileSize;
				reqFTP.UseBinary = true;
				reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				Stream ftpStream = response.GetResponseStream();
				fileSize = response.ContentLength;

				ftpStream.Close();
				response.Close();
			}
			catch (Exception ex)
			{
				Errors.Add(ex.Message);
			}
			return fileSize;
		}

		public bool Rename(string currentFilename, string newFilename)
		{
			FtpWebRequest reqFTP;
			try
			{
				reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + currentFilename));
				reqFTP.Method = WebRequestMethods.Ftp.Rename;
				reqFTP.RenameTo = newFilename;
				reqFTP.UseBinary = true;
				reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				Stream ftpStream = response.GetResponseStream();

				ftpStream.Close();
				response.Close();
				return true;
			}
			catch (Exception ex)
			{
				Errors.Add(ex.Message);
				return false;
			}
		}

		public bool MakeDir(string dirName)
		{
			FtpWebRequest reqFTP;
			try
			{
				reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + ftpServerIP + "/" + dirName));
				reqFTP.Method = WebRequestMethods.Ftp.MakeDirectory;
				reqFTP.UseBinary = true;
				reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
				FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
				Stream ftpStream = response.GetResponseStream();

				ftpStream.Close();
				response.Close();
				return true;
			}
			catch (Exception ex)
			{
				Errors.Add(ex.Message);
				return false;
			}
		}




	}
}
