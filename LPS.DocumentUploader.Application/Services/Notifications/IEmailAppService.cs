using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LPS.DocumentUploader.Application.Services.Notifications
{
    public interface IEmailAppService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }
}
