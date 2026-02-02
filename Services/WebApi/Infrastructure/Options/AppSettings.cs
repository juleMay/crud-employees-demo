namespace WebApi.Infrastructure.Options;

public class AppSettings
{
    public const string SectionName = "ConectionString";
    public string ConectionString { get; set; } = string.Empty;
}
