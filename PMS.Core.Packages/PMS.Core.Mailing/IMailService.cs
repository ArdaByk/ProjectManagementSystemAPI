using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Mailing;

public interface IMailService
{
    void SendMail(Mail mail);
    Task SendMailAsync(Mail mail);
}
