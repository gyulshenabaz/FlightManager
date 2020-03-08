using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightManager.Common.EmailSender.Interfaces;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FlightManager.Common.EmailSender.Implementation
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridConfiguration options;

        public EmailSender(IOptions<SendGridConfiguration> options)
        {
            this.options = options.Value;
        }

        public async Task SendEmailAsync(string receiver, string templateId, object data)
            => await this.SendEmailAsync(GlobalConstants.FlightManagerEmail, receiver, templateId, data);
        
        public async Task SendEmailToMultipleAsync(List<string> receivers, string templateId, object data)
            => await this.SendEmailToMultipleAsync(GlobalConstants.FlightManagerEmail, receivers, templateId, data);
        
        private async Task SendEmailAsync(string sender, string receiver, string templateId, object data)
        {
            var client = new SendGridClient(this.options.ApiKey);
            var from = new EmailAddress(sender, "Flight Manager");
            var to = new EmailAddress(receiver, receiver);
            var msg = MailHelper.CreateSingleTemplateEmail(from, to,templateId, data);

            await client.SendEmailAsync(msg);
        }
        
        private async Task SendEmailToMultipleAsync(string sender, ICollection<string> recipients, string templateId, object data)
        {
            var client = new SendGridClient(this.options.ApiKey);
            var from = new EmailAddress(sender, "Flight Manager");
            var tos = recipients.Select(MailHelper.StringToEmailAddress).ToList();
            var msg = MailHelper.CreateSingleTemplateEmailToMultipleRecipients(from, tos, templateId, data);
            
            await client.SendEmailAsync(msg);
        }
        
    }
}