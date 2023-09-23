using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using QMangaAPI.Data.Enums;
using QMangaAPI.Data.Interfaces.Repositories;
using QMangaAPI.Data.Interfaces.Services;
using QMangaAPI.Models;

namespace QMangaAPI.Controllers;

[Route("api/v1/user/")]
[ApiController]
public class UserController : ControllerBase
{
  private readonly IUserRepository userRepository;
  private readonly IRoleRepository roleRepository;

  private readonly IPasswordHasher passwordHasher;
  private readonly IJwtGenerator jwtGenerator;
  private readonly IUserValidator userValidator;

  public UserController(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IPasswordHasher passwordHasher,
    IJwtGenerator jwtGenerator,
    IUserValidator userValidator)
  {
    this.userRepository = userRepository;
    this.roleRepository = roleRepository;
    this.passwordHasher = passwordHasher;
    this.jwtGenerator = jwtGenerator;
    this.userValidator = userValidator;
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] User? user)
  {
    if (user == null)
      return BadRequest(new { Message = "user is null" });

    var byUsername = await userRepository.GetByUsernameAsync(user.Username);

    if (byUsername == null)
      return NotFound(new { Message = "user not found" });

    if (!passwordHasher.Verify(byUsername.Password, user.Password))
      return BadRequest(new { Message = "password is incorrect" });

    byUsername.Token = jwtGenerator.CreateJwt(byUsername.Username, byUsername.Role.Name);
    
    return Ok(new
    {
      Token = byUsername.Token,
      Message = "login success"
    });
  }

  [HttpPost("registration")]
  public async Task<IActionResult> Registration([FromBody] User? user)
  {
    if (user == null)
      return BadRequest(new { Message = "user is null" });

    if (await userValidator.CheckUsernameExistAsync(user.Username))
      return BadRequest(new { Message = "username already exist" });
    if (await userValidator.CheckEmailExistAsync(user.Email))
      return BadRequest(new { Message = "email already exist" });
    if (userValidator.CheckEmailAsync(user.Email))
      return BadRequest(new { Message = "invalid email" });
    if (userValidator.CheckPasswordAsync(user.Password, out var message))
      return BadRequest(new { Message = message });
    
    user.Password = passwordHasher.Hash(user.Password);
    user.Role = await roleRepository.GetByNameAsync(RoleEnums.User) ?? throw new InvalidOperationException();
    user.Token = "todo token";

    await userRepository.AddAsync(user);

    return Ok(new { Message = "user registered" });
  }
}