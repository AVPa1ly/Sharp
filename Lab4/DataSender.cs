using System;
using System.IO;
using System.Net;
using System.Text;

namespace Lab4
{
    public class DataSender
    {
        public string ServerName { get; set; }
        
        private NetworkCredential Cred { get; set; }
        
        public DataSender(string serverName, string login, string pass)
        {
            ServerName = serverName;
            ChangeCredentials(login, pass);
        }

        public void ChangeCredentials(string login, string pass)
        {
            Cred = new NetworkCredential(login, pass);
        }

        public void Send(string text1, string text2)
        {
            string address =  ServerName + "/Directory/";
            if(!DirectoryExists(address))
            {
                MakeDirectory(address);
            }
            address += DateTime.Now.ToString("yyyy") + "/";
            if (!DirectoryExists(address))
            {
                MakeDirectory(address);
            }
            address += DateTime.Now.ToString("MM") + "/";
            if (!DirectoryExists(address))
            {
                MakeDirectory(address);
            }
            address += DateTime.Now.ToString("dd") + "/";
            if (!DirectoryExists(address))
            {
                MakeDirectory(address);
            }
            string temp = address;
            address += Path.ChangeExtension("File_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"), ".xml");
            SendText(address, text1);
            temp += Path.ChangeExtension("File_" + DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss"), ".xsd");
            SendText(temp, text2);
        }

        private void SendText(string address, string text)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(address);
            request.Credentials = Cred;
            request.Method = WebRequestMethods.Ftp.UploadFile;
            using (Stream requestStream = request.GetRequestStream())
            {
                byte[] arr = Encoding.Unicode.GetBytes(text);
                requestStream.Write(arr, 0, arr.Length);
            }
        }
        private void MakeDirectory(string path)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
            request.Credentials = Cred;
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            using (request.GetResponse())
            {
            }            
        }

        private bool DirectoryExists(string path)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);
                request.Credentials = Cred;
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                using (request.GetResponse())
                {
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
