using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Core.Mailing;

public class Mail
{
    public Mail(string subject, string textBody, string htmlBody,List<MailboxAddress> toList)
    {
        Subject = subject;
        TextBody = textBody;
        HtmlBody = htmlBody;
        ToList = toList;
    }
    public Mail()
    {
        Subject = string.Empty;
        TextBody = string.Empty;
        HtmlBody = string.Empty;
        ToList = [];
    }

    public string Subject { get; set; }
    public string TextBody { get; set; }
    public string HtmlBody { get; set; }
    public AttachmentCollection? Attachments { get; set; }
    public List<MailboxAddress> ToList { get; set; }
}
