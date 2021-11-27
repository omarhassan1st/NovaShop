using System;
using System.Management;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Elections.Classes
{
    class Licenses
    {
        public static string GetHwid()
        {
            try
            {
                string HWID = string.Empty;
                var Hwids = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
                ManagementObjectCollection mbsList = Hwids.Get();
                foreach (ManagementObject MyHwid in mbsList)
                {
                    HWID = MyHwid["ProcessorId"].ToString();
                }
                return HWID;
            }
            catch
            {
                return string.Empty;
            }

        }
        public static string Encrypt(string Txt)
        {
            try
            {
                return EasyEncryption.MD5.ComputeMD5Hash(Txt);
            }
            catch
            {
                return string.Empty;
            }
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }
        public static void SendNewMsg(string MSG)
        {
            if (CheckForInternetConnection())
            {
                try
                {
                    var host = Dns.GetHostEntry(Dns.GetHostName());
                    string EmailBody = MSG;
                    SmtpClient Client = new SmtpClient("smtp.gmail.com", 587);
                    MailMessage mailMessage = new MailMessage();
                    string Email = Querys.Reader_SingleValue("select E_Mail from _OWNER");
                    mailMessage.From = new MailAddress(Email);
                    mailMessage.To.Add(Email);
                    mailMessage.Subject = "Message From NovaTool (Electrics) ";
                    Client.UseDefaultCredentials = false;
                    Client.EnableSsl = true;
                    Client.Credentials = new NetworkCredential("Test", "Test");
                    mailMessage.Body = EmailBody;

                    Client.Send(mailMessage);
                }
                catch
                {

                }
            }
        }
    }
}
