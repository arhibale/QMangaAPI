namespace QMangaAPI.Data;

/// <summary>
/// Модель письма.
/// </summary>
public class EmailRequest
{
  public List<string> ToEmail { get; init; } = new();
  public string Subject { get; init; } = string.Empty;
  public string Body { get; init; } = string.Empty;
}