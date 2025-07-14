using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmailService
    {
        private readonly IConfiguration _config;

        public EmailService()
        {
            _config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

        public void SendMail(string to, string subject, string body)
        {
            string host = _config["Email:Host"];
            int port = int.Parse(_config["Email:Port"]);
            string user = _config["Email:User"];
            string pass = _config["Email:Pass"];
            bool enableSsl = bool.Parse(_config["Email:EnableSsl"]);

            var smtp = new SmtpClient(host, port)
            {
                Credentials = new NetworkCredential(user, pass),
                EnableSsl = enableSsl
            };

            var message = new MailMessage(user, to, subject, body)
            {
                IsBodyHtml = true 
            };

            smtp.Send(message);
        }
    }
}
