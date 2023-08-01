using System.ComponentModel.DataAnnotations;

namespace DomainModel.Models.Mails;
public  class MailRequestDto
{
    public string ToEmail { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Body { get; set; } = null!;
    //public IList<Stream>? Attachments { get; set; }
}
