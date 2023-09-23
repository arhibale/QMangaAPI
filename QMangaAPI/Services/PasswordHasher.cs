using System.Security.Cryptography;
using QMangaAPI.Data.Interfaces.Services;

namespace QMangaAPI.Services;

public class PasswordHasher : IPasswordHasher
{
  private static readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;
  
  private const int saltSize = 128 / 8;
  private const int keySize = 256 / 8;
  private const int iterations = 10000;

  private const char delimiter = ';';

  public string Hash(string password)
  {
    var salt = RandomNumberGenerator.GetBytes(saltSize);
    var hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, hashAlgorithmName, keySize);

    return string.Join(delimiter, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
  }

  public bool Verify(string passwordHash, string inputPassword)
  {
    var elements = passwordHash.Split(delimiter);
    var salt = Convert.FromBase64String(elements[0]);
    var hash = Convert.FromBase64String(elements[1]);

    var hashInput = Rfc2898DeriveBytes.Pbkdf2(inputPassword, salt, iterations, hashAlgorithmName, keySize);

    return CryptographicOperations.FixedTimeEquals(hash, hashInput);
  }
}