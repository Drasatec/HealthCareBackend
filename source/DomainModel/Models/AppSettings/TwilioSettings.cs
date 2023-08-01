namespace DomainModel.Models.AppSettings;

public class TwilioSettings
{
    public string AccountSID { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
    public string TwilioPhoneNumber { get; set; } = string.Empty;
}
