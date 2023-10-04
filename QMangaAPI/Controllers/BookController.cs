using Microsoft.AspNetCore.Mvc;
using QMangaAPI.Data.Dto;
using QMangaAPI.Repositories;
using QMangaAPI.Services;

namespace QMangaAPI.Controllers;

[Route("api/v1/book")]
[ApiController]
public class BookRepository : ControllerBase
{
  private readonly IRepositoryManager repositoryManager;
  private readonly IImageService imageService;

  public BookRepository(IRepositoryManager repositoryManager, IImageService imageService)
  {
    this.repositoryManager = repositoryManager;
    this.imageService = imageService;
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

  // [HttpPost]
  // public async Task<IActionResult> SaveBook([FromBody] BookDto? book)
  // {
  //   if (book is null)
  //     return BadRequest(new { Message = "Book is null." });
  //
  //   var user = await userRepository.GetByUsernameAsync(book.UserName);
  //   var bookType = await bookTypeRepository.GetByNameAsync(book.BookType);
  //
  //   var tags = await tagRepository.GetAllByNameAsync(book.Tags);
  //   var authors = await authorRepository.GetAllByNameAsync(book.Authors);
  //   var artist = await artistRepository.GetAllByNameAsync(book.Artists);
  //   var translators = translatorRepository.GetAllByName(book.Translators);
  //
  //   var coverImage = await imageService.SaveCoverImageAsync(book.CoverImage, book.Name);
  //
  //   await bookRepository.AddAsync(new Book
  //   {
  //     Name = book.Name,
  //     Description = book.Description,
  //     BookType = bookType,
  //     User = user,
  //     CoverImage = coverImage,
  //     Tags = { tags },
  //     Authors = { authors },
  //     Artists = { artist },
  //     Translators = { translators }
  //   });
  //
  //   return Ok(new { Message = "Successful added" });
  // }
}