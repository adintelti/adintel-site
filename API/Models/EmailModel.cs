namespace adintelSiteAPI.Models
{
    public record EmailModel(string From, string[] To, string Subject, string Body);
}
