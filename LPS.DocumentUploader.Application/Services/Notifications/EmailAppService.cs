using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Services.Notifications
{
    public class EmailAppService : IEmailAppService, IDisposable
    {
        private readonly SmtpClient _smtpClient;

        public EmailAppService(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
        {
            _smtpClient = new SmtpClient(smtpServer, smtpPort)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(smtpUsername, smtpPassword),
                EnableSsl = true
            };
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (var mailMessage = new MailMessage
            {
                From = new MailAddress("LPS Server"),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                mailMessage.To.Add(toEmail);
                await _smtpClient.SendMailAsync(mailMessage);
            }
        }

        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}
