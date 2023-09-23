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

  public bool CheckEmailAsync(string email)
  {
    return Regex.IsMatch(email, @"^[A-Za-z0-9._+\-\']+@[A-Za-z0-9.\-]+\.[A-Za-z]{2,}$");
  }

  public bool CheckPasswordAsync(string password, out string message)
  {
    var sb = new StringBuilder();
    
    if (password.Length < 8)
    {
      sb.Append($"Minimum password length should be 8{Environment.NewLine}");
    }

    if (!(Regex.IsMatch(password, "[a-z]") 
          && Regex.IsMatch(password, "[A-Z]") 
          && Regex.IsMatch(password, "[0-9]")))
    {
      sb.Append($"Password should be Alphanumeric{Environment.NewLine}");
    }

    if (!Regex.IsMatch(password, "[!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~]"))
    {
      sb.Append($"Password should contain special chars{Environment.NewLine}");
    }

    message = sb.ToString();

    return !string.IsNullOrEmpty(message);
  }
}