using QMangaAPI.Models;

namespace QMangaAPI.Data.Interfaces.Services;

public interface IUserValidator
{
  Task<bool> CheckUsernameExistAsync(string username);
  Task<bool> CheckEmailExistAsync(string email);
  bool CheckEmail(string email);
  bool CheckPassword(string password, out string message);
  
}