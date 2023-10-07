using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QMangaAPI.Data;
using QMangaAPI.Data.Dto;
using QMangaAPI.Data.Enum;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories;
using QMangaAPI.Services;

namespace QMangaAPI.Controllers;

[Route("api/v1/user")]
[ApiController]
public class UserController : ControllerBase
{
  private readonly IRepositoryManager repositoryManager;
  private readonly IPasswordHasherService passwordHasher;
  private readonly IJwtService jwtService;
  private readonly IUserValidatorService userValidator;
  private readonly IEmailService emailService;

  public UserController
  (
    IRepositoryManager repositoryManager,
    IPasswordHasherService passwordHasher,
    IJwtService jwtService,
    IUserValidatorService userValidator,
    IEmailService emailService
  )
  {
    this.repositoryManager = repositoryManager;
    this.passwordHasher = passwordHasher;
    this.jwtService = jwtService;
    this.userValidator = userValidator;
    this.emailService = emailService;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] User? user)
  {
    if (user is null)
      return BadRequest(new { Message = "User is null" });

    var byUsername =
      await repositoryManager.Users.FirstOrDefaultUserIncludeModelAsync(e => e.Role,
        e => string.Equals(e.Username, user.Username), true);

    if (byUsername is null)
      return NotFound(new { Message = "User not found" });
    if (!passwordHasher.Verify(byUsername.Password, user.Password))
      return BadRequest(new { Message = "Password is incorrect" });

    byUsername.Token = jwtService.CreateJwt(byUsername.Username, byUsername.Role.Name);
    var newAccessToken = byUsername.Token;
    var newRefreshToken = await jwtService.CreateRefreshTokenAsync();
    byUsername.RefreshToken = newRefreshToken;
    byUsername.RefreshTokenExpiryTime = DateTime.Now.AddDays(32);

    repositoryManager.Users.UpdateUser(byUsername);
    repositoryManager.Save();

    return Ok(new TokenApiDto
    {
      AccessToken = newAccessToken,
      RefreshToken = newRefreshToken,
      Message = "Login access"
    });
  }

  [HttpPost("registration")]
  public async Task<IActionResult> Registration([FromBody] User? user)
  {
    if (user is null)
      return BadRequest(new { Message = "User is null" });

    if (await userValidator.CheckUsernameExistAsync(user.Username))
      return BadRequest(new { Message = "Username already exist" });
    if (await userValidator.CheckEmailExistAsync(user.Email))
      return BadRequest(new { Message = "Email already exist" });
    if (userValidator.CheckEmail(user.Email))
      return BadRequest(new { Message = "Invalid email" });
    if (userValidator.CheckPassword(user.Password, out var message))
      return BadRequest(new { Message = message });

    user.Password = passwordHasher.Hash(user.Password);
    user.Role = await repositoryManager.Roles.FirstOrDefaultRolesAsync(
                  e => string.Equals(e.Name, RoleEnum.User.ToString()), true) ??
                throw new InvalidOperationException();

    repositoryManager.Users.CreateUser(user);
    repositoryManager.Save();

    return Ok(new { Message = "User registered" });
  }

  [HttpPost("refresh")]
  public async Task<IActionResult> Refresh(TokenApiDto? tokenApiDto)
  {
    if (tokenApiDto is null)
      return BadRequest("Invalid client request");

    var accessToken = tokenApiDto.AccessToken;
    var refreshToken = tokenApiDto.RefreshToken;

    var principal = jwtService.GetPrincipleFromExpiredToken(accessToken);
    var username = principal.Identity?.Name;
    var user = await repositoryManager.Users.FirstOrDefaultUserIncludeModelAsync(e => e.Role ,e => string.Equals(e.Username, username), true);

    if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
      return BadRequest("Invalid request");

    var newAccessToken = jwtService.CreateJwt(user.Username, user.Role.Name);
    var newRefreshToken = await jwtService.CreateRefreshTokenAsync();
    user.RefreshToken = newRefreshToken;

    repositoryManager.Users.UpdateUser(user);
    repositoryManager.Save();

    return Ok(new TokenApiDto
    {
      AccessToken = newAccessToken,
      RefreshToken = newRefreshToken,
      Message = "Successful refresh token"
    });
  }

  [HttpPost("send-email-reset-password/{email}")]
  public async Task<IActionResult> SendEmail(string email)
  {
    var user = await repositoryManager.Users.FirstOrDefaultUserAsync(e => string.Equals(e.Email, email), true);

    if (user is null)
      return NotFound(new { Message = "Email doesn't exist" });

    var tokenBytes = RandomNumberGenerator.GetBytes(64);
    var emailToken = Convert.ToBase64String(tokenBytes);

    user.ResetPasswordToken = emailToken;
    user.ResetPasswordTokenExpiryTime = DateTime.Now.AddMinutes(15);

    var emailRequest = new EmailRequest
    {
      ToEmail = new List<string> { email },
      Subject = "Reset Password | QManga",
      Body = EmailPasswordResetBody.EmailStringBody(email, emailToken)
    };

    await emailService.SendEmailAsync(emailRequest);
    repositoryManager.Users.UpdateUser(user);
    repositoryManager.Save();

    return Ok(new { Message = "Email sent" });
  }

  [HttpPost("reset-password")]
  public async Task<IActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
  {
    var newToken = resetPasswordDto.EmailToken.Replace(" ", "+");
    var user = await repositoryManager.Users.FirstOrDefaultUserAsync(
      e => string.Equals(e.Email, resetPasswordDto.Email), true);

    if (user is null)
      return NotFound(new { Message = "User doesn't exist" });

    var tokenCode = user.ResetPasswordToken;
    var emailTokenExpiry = user.ResetPasswordTokenExpiryTime;

    if (tokenCode != resetPasswordDto.EmailToken || emailTokenExpiry < DateTime.Now)
      return BadRequest(new { Message = "Invalid reset link" });

    user.Password = passwordHasher.Hash(resetPasswordDto.NewPassword);
    repositoryManager.Users.UpdateUser(user);
    repositoryManager.Save();

    return Ok(new { Message = "Password reset successfully" });
  }

  [HttpGet("profile/{token}")]
  [Authorize]
  public async Task<IActionResult> Profile(string token)
  {
    if (string.IsNullOrEmpty(token))
      return BadRequest("Invalid request");

    var principal = jwtService.GetPrincipleFromExpiredToken(token);
    var username = principal.Identity?.Name;
    var user = await repositoryManager.Users.FirstOrDefaultUserIncludeModelAsync(e => e.Role, e => string.Equals(e.Username, username), true);

    if (user is null || user.RefreshTokenExpiryTime <= DateTime.Now)
      return BadRequest("Invalid request");

    var bookPage = await repositoryManager.Books
      .FindBooksByCondition(e => e.UploadedByUserId == user.Id, false)
      .Select(e => new BookPageDto { Name = e.Name, BookType = e.BookType.Name, Tags = e.Tags.Select(tag => tag.Name) })
      .ToListAsync();

    return Ok(new UserProfileDto
    {
      Username = user.Username,
      Email = user.Email,
      Role = user.Role.Name,
      BookUploads = bookPage
    });
  }
}