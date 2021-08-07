using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace PcBuilder.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _config;

        public EmailSender(IConfiguration config)
        {
            _config = config;
        }

        public async Task SendEmailAsync(string subject, string htmlMessage, string email)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(subject);
            mail.From = new MailAddress(_config.GetValue<string>("SmtpServers:login"));
            mail.Subject = htmlMessage;
            mail.Body = email;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = _config.GetValue<string>("SmtpServers:host");
            smtp.Port = _config.GetValue<int>("SmtpServers:port");
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(_config.GetValue<string>("SmtpServers:login"), _config.GetValue<string>("SmtpServers:password"));
            smtp.EnableSsl = true;
            await smtp.SendMailAsync(mail);
        }
    }
}