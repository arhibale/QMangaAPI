using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
  
  // 10 гб.
  private const long MaxFileSize = 10L * 1024L * 1024L * 1024L;

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
        CoverImagePath = e.CoverImage?.Name ?? string.Empty,
        Tags = e.Tags.Select(tag => tag.Name).ToList()
      }).ToList()
    });
  }
  
  [HttpPost("upload/token={token}")]
  [RequestSizeLimit(MaxFileSize)]
  [RequestFormLimits(MultipartBodyLengthLimit = MaxFileSize)]
  [Authorize]
  public async Task<IActionResult> Upload(string token, [FromForm] BookDto? bookDto)
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
      return BadRequest(new { Message = $"Cover image is null!" });
    if (!bookDto.Images.Any())
      return BadRequest(new { Message = $"Images is null: {bookDto.Images.Count}" });

    var type = await repositoryManager.BookTypes.FirstOrDefaultUserAsync(e => string.Equals(e.Name, "Manga"), true);

    if (type is null)
      return BadRequest(new { Message = "BookType is wrong" });
    
    var images = await imageService.SaveImagesAsync(bookDto.Images, bookDto.Name);
    var coverImage = await imageService.SaveImageAsync(bookDto.CoverImage, bookDto.Name);

    var book = new Book
    {
      Name = bookDto.Name,
      Description = bookDto.Description,
      BookType = type,
      UploadedByUser = user,
      CoverImage = coverImage,
      Images = images
    };

    var authors = new List<Author>();
    foreach (var au in bookDto.Authors)
    {
      var author = await repositoryManager.Authors.FirstOrDefaultUserAsync(e => string.Equals(e.Name, au), true);
      if (author is null)
        continue;
      
      authors.Add(author);
    }
    
    var artists = new List<Artist>();
    foreach (var ar in bookDto.Artists)
    {
      var artist = await repositoryManager.Artists.FirstOrDefaultUserAsync(e => string.Equals(e.Name, ar), true);
      if (artist is null)
        continue;
      
      artists.Add(artist);
    }
    
    var tags = new List<Tag>();
    foreach (var t in bookDto.Tags)
    {
      var tag = await repositoryManager.Tags.FirstOrDefaultUserAsync(e => string.Equals(e.Name, t), true);
      if (tag is null)
        continue;
      
      tags.Add(tag);
    }

    book.Authors.ToList().AddRange(authors);
    book.Artists.ToList().AddRange(artists);
    book.Tags.ToList().AddRange(tags);
    
    repositoryManager.Books.CreateBook(book);
    repositoryManager.Save();

    return Ok(new { Message = "Successful added" });
  }

  [HttpGet("tags")]
  public async Task<IActionResult> Tags()
  {
    var tags = await repositoryManager.Tags.GetAll(false).ToListAsync();

    if (!tags.Any())
      return BadRequest(new {Message = "tags is null"});

    return Ok(new List<string>(tags.Select(e => e.Name)));
  }
  
  [HttpGet("authors")]
  public async Task<IActionResult> Authors()
  {
    var authors = await repositoryManager.Authors.GetAll(false).ToListAsync();

    if (!authors.Any())
      return BadRequest(new {Message = "authors is null"});

    return Ok(new List<string>(authors.Select(e => e.Name)));
  }
  
  [HttpGet("artists")]
  public async Task<IActionResult> Artists()
  {
    var artists = await repositoryManager.Artists.GetAll(false).ToListAsync();

    if (!artists.Any())
      return BadRequest(new {Message = "artists is null"});

    return Ok(new List<string>(artists.Select(e => e.Name)));
  }
}