namespace QMangaAPI.Helpers;

public class EmailRequest
{
  public List<string> ToEmail { get; init; }
  public string Subject { get; init; } = string.Empty;
  public string Body { get; init; } = string.Empty;
}