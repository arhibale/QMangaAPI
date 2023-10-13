using Microsoft.EntityFrameworkCore;
using QMangaAPI.Models.Impl;
using QMangaAPI.Models.Impl.To;
using Image = QMangaAPI.Models.Impl.Image;

namespace QMangaAPI.Repositories.Context;

/// <summary>
/// Контекст БД.
/// </summary>
public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions options) : base(options)
  {
  }

  public DbSet<Artist> Artists { get; set; }
  public DbSet<Author> Authors { get; set; }
  public DbSet<Book> Books { get; set; }
  public DbSet<BookType> BookTypes { get; set; }
  public DbSet<Image> Images { get; set; }
  public DbSet<Role> Roles { get; set; }
  public DbSet<Tag> Tags { get; set; }
  public DbSet<User> Users { get; set; }
  
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Artist>()
      .HasMany(e => e.Books)
      .WithMany(e => e.Artists)
      .UsingEntity<ArtistBook>();
    modelBuilder.Entity<Author>()
      .HasMany(e => e.Books)
      .WithMany(e => e.Authors)
      .UsingEntity<AuthorBook>();
    modelBuilder.Entity<Tag>()
      .HasMany(e => e.Books)
      .WithMany(e => e.Tags)
      .UsingEntity<TagBook>();
    modelBuilder.Entity<Book>()
      .HasOne(e => e.UploadedByUser)
      .WithMany(e => e.BookUploads)
      .HasForeignKey(e => e.UploadedByUserId)
      .IsRequired();
    modelBuilder.Entity<Book>()
      .HasOne(e => e.BookType)
      .WithMany(e => e.Books)
      .HasForeignKey(e => e.BookTypeId)
      .IsRequired();
    modelBuilder.Entity<User>()
      .HasOne(e => e.Role)
      .WithMany(e => e.Users)
      .HasForeignKey(e => e.RoleId)
      .IsRequired();
    modelBuilder.Entity<Book>()
      .HasMany(e => e.Images)
      .WithOne(e => e.Book)
      .HasForeignKey(e => e.BookId)
      .IsRequired();
    modelBuilder.Entity<Book>()
      .HasOne(e => e.CoverImage)
      .WithOne(e => e.Book)
      .HasForeignKey<CoverImage>(e => e.BookId)
      .IsRequired();
  }
}