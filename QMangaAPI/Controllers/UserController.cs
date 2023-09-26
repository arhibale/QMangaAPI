using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QMangaAPI.Data.Enums;
using QMangaAPI.Data.Interfaces.Repositories;
using QMangaAPI.Data.Interfaces.Services;
using QMangaAPI.Models;
using QMangaAPI.Models.Dto;

namespace QMangaAPI.Controllers;

[Route("api/v1/user/")]
[ApiController]
public class UserController : ControllerBase
{
  private readonly IUserRepository userRepository;
  private readonly IRoleRepository roleRepository;

  private readonly IPasswordHasher passwordHasher;
  private readonly IJwtService jwtService;
  private readonly IUserValidator userValidator;

  public UserController(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IPasswordHasher passwordHasher,
    IJwtService jwtService,
    IUserValidator userValidator)
  {
    this.userRepository = userRepository;
    this.roleRepository = roleRepository;
    this.passwordHasher = passwordHasher;
    this.jwtService = jwtService;
    this.userValidator = userValidator;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] User? user)
  {
    if (user == null)
      return BadRequest(new { Message = "User is null" });

    var byUsername = await userRepository.GetByUsernameAsync(user.Username);

    if (byUsername == null)
      return NotFound(new { Message = "User not found" });

    if (!passwordHasher.Verify(byUsername.Password, user.Password))
      return BadRequest(new { Message = "Password is incorrect" });

    byUsername.Token = jwtService.CreateJwt(byUsername.Username, byUsername.Role.Name);
    var newAccessToken = byUsername.Token;
    var newRefreshToken = await jwtService.CreateRefreshTokenAsync();
    byUsername.RefreshToken = newRefreshToken;
    byUsername.RefreshTokenExpiryTime = DateTime.Now.AddDays(32);
    await userRepository.UpdateAsync(byUsername);

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
    if (user == null)
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
    user.Role = await roleRepository.GetByNameAsync(RoleEnums.User) ?? throw new InvalidOperationException();
    user.Token = "todo token";

    await userRepository.AddAsync(user);

    return Ok(new { Message = "User registered" });
  }

  [HttpGet]
  [Authorize]
  public async Task<IActionResult> GetAllUsers()
  {
    return Ok(await userRepository.GetAllAsync());
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
    var user = await userRepository.GetByUsernameAsync(username);

    if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
      return BadRequest("Invalid request");

    var newAccessToken = jwtService.CreateJwt(user.Username, user.Role.Name);
    var newRefreshToken = await jwtService.CreateRefreshTokenAsync();
    user.RefreshToken = newRefreshToken;

    await userRepository.UpdateAsync(user);

    return Ok(new TokenApiDto
    {
      AccessToken = newAccessToken,
      RefreshToken = newRefreshToken,
      Message = "Successful refresh token"
    });
  }
}