using QMangaAPI.Models.Impl;
using Image = SixLabors.ImageSharp.Image;

namespace QMangaAPI.Services.Impl;

public class ImageService : IImageService
{
  private readonly IWebHostEnvironment hostEnvironment;
  private readonly string imagesPath;

  public ImageService(IWebHostEnvironment hostEnvironment)
  {
    this.hostEnvironment = hostEnvironment;
    this.imagesPath = Path.Combine(hostEnvironment.ContentRootPath, "Resources\\Images");
  }

  public async Task<List<Models.Impl.Image>> SaveImagesAsync(List<IFormFile> imgFiles, string bookName)
  {
    var path = Path.Combine(imagesPath, bookName);
    
    if (!Directory.Exists(path)) 
      Directory.CreateDirectory(path);

    var images = new List<Models.Impl.Image>();
    
    for (var i = 0; i < imgFiles.Count; i++)
    {
      var file = imgFiles[i];
      var imgName = $"img_{bookName}_{i}.png";
      var imgPath = Path.Combine(path, imgName);

      await SaveFile(imgPath, file.OpenReadStream());

      images.Add(new Models.Impl.Image { Name = imgName, Path = imgPath });
    }

    return images;
  }

  public async Task<CoverImage> SaveImageAsync(IFormFile imgFile, string bookName)
  {
    var path = Path.Combine(imagesPath, bookName);

    if (!Directory.Exists(path)) 
      Directory.CreateDirectory(path);
    
    var imgName = $"cover_{bookName}.png";
    var imgPath = Path.Combine(path, imgName);

    await SaveFile(imgPath, imgFile.OpenReadStream());

    return new CoverImage { Name = imgName, Path = imgPath };
  }
  
  private static async Task SaveFile(string imgPath, Stream imgStream)
  {
    using var image = await Image.LoadAsync(imgStream);
    await image.SaveAsPngAsync(imgPath);
  }
}