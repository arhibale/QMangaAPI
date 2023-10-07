using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using QMangaAPI.Data.Dto;
using QMangaAPI.Models.Impl;
using QMangaAPI.Repositories;
using QMangaAPI.Services;

namespace QMangaAPI.Controllers;

[Route("api/v1/book")]
[ApiController]
public class BookRepository : ControllerBase
{
  private readonly IRepositoryManager repositoryManager;
  private readonly IImageService imageService;
  private readonly IJwtService jwtService;

  public BookRepository(IRepositoryManager repositoryManager, IImageService imageService, IJwtService jwtService)
  {
    this.repositoryManager = repositoryManager;
    this.imageService = imageService;
    this.jwtService = jwtService;
  }

  [HttpGet("page={page:int}&size={pageSize:int}")]
  public async Task<IActionResult> GetAllPage(int? page, int pageSize = 12)
  {
    var countDetails = await repositoryManager.Books.GetCountAsync();
    var book = await repositoryManager.Books.GetPageAsync(page, pageSize);

    return Ok(new PageResultDto<BookPageDto>
    {
      Count = countDetails,
      PageIndex = page ?? 1,
      PageSize = pageSize,
      Items = book.Select(e => new BookPageDto
      {
        Name = e.Name,
        BookType = e.BookType.Name,
        Tags = e.Tags.Select(tag => tag.Name).ToList()
      }).ToList()
    });
  }
  
  [HttpPost("upload/token={token}")]
  [Authorize]
  public async Task<IActionResult> Upload(string token, [FromBody] BookDto? bookDto)
  {
    if (string.IsNullOrEmpty(token))
      return BadRequest("Invalid request");

    var principal = jwtService.GetPrincipleFromExpiredToken(token);
    var username = principal.Identity?.Name;
    var user = await repositoryManager.Users.FirstOrDefaultUserIncludeModelAsync(e => e.Role, e => string.Equals(e.Username, username), true);

    if (user is null || user.RefreshTokenExpiryTime <= DateTime.Now)
      return BadRequest("Invalid request");
    if (bookDto is null)
      return BadRequest(new { Message = "Book is null" });
    if (bookDto.CoverImage is null)
      return BadRequest(new { Message = "Cover image is null" });

    var coverImage = await imageService.SaveImageAsync(bookDto.CoverImage, bookDto.Name);

    return Ok(new { Message = "Successful added" });
  }
}