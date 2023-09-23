using QMangaAPI.Models;

namespace QMangaAPI.Data.Interfaces.Services;

public interface IUserValidator
{
  Task<bool> CheckUsernameExistAsync(string username);
  Task<bool> CheckEmailExistAsync(string email);
  bool CheckEmailAsync(string email);
  bool CheckPasswordAsync(string password, out string message);
  
}