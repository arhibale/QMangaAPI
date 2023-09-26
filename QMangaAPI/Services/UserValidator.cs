using System.Text;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using QMangaAPI.Data.Context;
using QMangaAPI.Data.Interfaces.Services;

namespace QMangaAPI.Services;

public class UserValidator : IUserValidator
{
  private readonly AppDbContext context;

  public UserValidator(AppDbContext context)
  {
    this.context = context;
  }

  public async Task<bool> CheckUsernameExistAsync(string username)
  {
    return await context.Users.AnyAsync(x => string.Equals(x.Username, username));
  }

  public async Task<bool> CheckEmailExistAsync(string email)
  {
    return await context.Users.AnyAsync(x => string.Equals(x.Email, email));
  }

  public bool CheckEmail(string email)
  {
    return !Regex.IsMatch(email,@"^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
  }

  public bool CheckPassword(string password, out string message)
  {
    var sb = new StringBuilder();
    
    if (password.Length < 8)
    {
      sb.Append($"Minimum password length should be 8\n");
    }

    if (!(Regex.IsMatch(password, "[a-z]") 
          && Regex.IsMatch(password, "[A-Z]") 
          && Regex.IsMatch(password, "[0-9]")))
    {
      sb.Append($"Password should be alphanumeric\n");
    }

    if (!Regex.IsMatch(password, "[!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]"))
    {
      sb.Append($"Password should contain special chars\n");
    }

    message = sb.ToString();

    return !string.IsNullOrEmpty(message);
  }
}