using System.ComponentModel.DataAnnotations;
using QMangaAPI.Data.Enums;
using QMangaAPI.Data.Interfaces.Models;

namespace QMangaAPI.Models;

public class Role : IEntity
{
  [Key] public Guid Id { get; set; }

  public string Name { get; set; } = string.Empty;

  public IEnumerable<User> Users = new List<User>();

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedIn { get; set; }
}