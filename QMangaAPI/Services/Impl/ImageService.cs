using QMangaAPI.Models.Impl;

namespace QMangaAPI.Services.Impl;

public class ImageService : IImageService
{
  private readonly IWebHostEnvironment hostEnvironment;

  public ImageService(IWebHostEnvironment hostEnvironment)
  {
    this.hostEnvironment = hostEnvironment;
  }

  public async Task<List<Image>> SaveImagesAsync(List<IFormFile> imgFiles, string bookName)
  {
    throw new NotImplementedException();
  }

  public async Task<CoverImage> SaveImageAsync(IFormFile imgFile, string bookName)
  {
    var path = Path.Combine(hostEnvironment.ContentRootPath, "Resources/Images", bookName);

    if (!Directory.Exists(path)) 
      Directory.CreateDirectory(path);

    var imgName = new string(Path.GetFileNameWithoutExtension(imgFile.FileName).Take(10).ToArray()).Replace(' ', '-');
    var imgPath = Path.Combine(path, imgName);

    await SaveFile(imgPath, imgFile);

    return new CoverImage { Name = imgName, Path = imgPath };
  }
  
  private static async Task SaveFile(string imgPath, IFormFile imgFile)
  {
    await using var fileStream = new FileStream(imgPath, FileMode.Create);
    await imgFile.CopyToAsync(fileStream);
  }
}