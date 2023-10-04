namespace QMangaAPI.Services;

public interface IUserValidatorService
{
  Task<bool> CheckUsernameExistAsync(string username);
  Task<bool> CheckEmailExistAsync(string email);
  bool CheckEmail(string email);
  bool CheckPassword(string password, out string message);
}