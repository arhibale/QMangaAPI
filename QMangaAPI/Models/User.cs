using System.ComponentModel.DataAnnotations;
using QMangaAPI.Data.Interfaces.Models;

namespace QMangaAPI.Models;

public class User : IEntity
{
  [Key] public Guid Id { get; set; }
  
  public string Username { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
  public string Password { get; set; } = string.Empty;
  public string Token { get; set; } = string.Empty;
  public string RefreshToken { get; set; } = string.Empty;
  public DateTime RefreshTokenExpiryTime { get; set; }

  public string ResetPasswordToken { get; set; } = string.Empty;
  public DateTime ResetPasswordExpiry { get; set; }
  
  public Translator? Translator { get; set; }
  
  public Guid RoleId { get; set; }
  public Role Role { get; set; } = null!;

  public IEnumerable<Comment> Comments { get; } = new List<Comment>();
  public IEnumerable<Book> Books { get; } = new List<Book>();

  public DateTime CreatedAt { get; set; } = DateTime.Now;
  public DateTime UpdatedIn { get; set; } = DateTime.Now;
}