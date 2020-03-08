using System.Collections.Generic;
using System.Threading.Tasks;

namespace FlightManager.Common.EmailSender.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string receiver, string templateId, object data);

        Task SendEmailToMultipleAsync(List<string> receivers, string templateId, object data);
    }
}