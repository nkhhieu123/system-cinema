using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace cinema_system
{
    // Gửi email qua SMTP, cấu hình trong App.config (appSettings: Smtp*)
    internal static class EmailSender
    {
        private static string Setting(string key)
        {
            return (ConfigurationManager.AppSettings[key] ?? "").Trim();
        }

        public static bool IsConfigured
        {
            get { return Setting("SmtpHost") != "" && Setting("SmtpUser") != "" && Setting("SmtpPassword") != ""; }
        }

        public static async Task SendAsync(string to, string subject, string body)
        {
            if (!IsConfigured)
                throw new InvalidOperationException("Chưa cấu hình máy chủ gửi email (SmtpHost, SmtpUser, SmtpPassword) trong App.config.");

            int port = int.TryParse(Setting("SmtpPort"), out int p) ? p : 587;
            bool ssl = !bool.TryParse(Setting("SmtpEnableSsl"), out bool b) || b; // mặc định bật SSL
            string from = Setting("SmtpFrom") != "" ? Setting("SmtpFrom") : Setting("SmtpUser");
            string displayName = Setting("SmtpDisplayName") != "" ? Setting("SmtpDisplayName") : "Rạp chiếu phim";

            using (MailMessage message = new MailMessage())
            using (SmtpClient client = new SmtpClient(Setting("SmtpHost"), port))
            {
                message.From = new MailAddress(from, displayName, Encoding.UTF8);
                message.To.Add(to);
                message.Subject = subject;
                message.SubjectEncoding = Encoding.UTF8;
                message.Body = body;
                message.BodyEncoding = Encoding.UTF8;

                client.EnableSsl = ssl;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Setting("SmtpUser"), Setting("SmtpPassword"));
                client.Timeout = 30000;

                await client.SendMailAsync(message);
            }
        }

        // Che bớt địa chỉ email khi hiển thị: nguyenvana@gmail.com -> ng*******@gmail.com
        public static string Mask(string email)
        {
            int at = email.IndexOf('@');
            if (at <= 0)
                return email;
            int keep = Math.Min(2, at);
            return email.Substring(0, keep) + new string('*', Math.Max(3, at - keep)) + email.Substring(at);
        }
    }
}
