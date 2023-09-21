using Microsoft.EntityFrameworkCore;
using QMangaAPI.Models;
using QMangaAPI.Models.ManyToMany;

namespace QMangaAPI.Data.Context;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions options) : base(options) { }
  
  public DbSet<Artist> Artists { get; set; }
  public DbSet<Author> Authors { get; set; }
  public DbSet<Book> Books { get; set; }
  public DbSet<BookType> BookTypes { get; set; }
  public DbSet<Comment> Comments { get; set; }
  public DbSet<Role> Roles { get; set; }
  public DbSet<Tag> Tags { get; set; }
  public DbSet<Translator> Translators { get; set; }
  public DbSet<User> Users { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // Many To Many
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
    
    // Many To One
    modelBuilder.Entity<User>()
      .HasMany(e => e.Comments)
      .WithOne(e => e.User)
      .HasForeignKey(e => e.UserId)
      .IsRequired();

    modelBuilder.Entity<User>()
      .HasMany(e => e.Books)
      .WithOne(e => e.User)
      .HasForeignKey(e => e.UserId)
      .IsRequired();

    modelBuilder.Entity<Role>()
      .HasMany(e => e.Users)
      .WithOne(e => e.Role)
      .HasForeignKey(e => e.RoleId)
      .IsRequired();

    modelBuilder.Entity<Book>()
      .HasMany(e => e.Comments)
      .WithOne(e => e.Book)
      .HasForeignKey(e => e.BookId)
      .IsRequired();

    modelBuilder.Entity<BookType>()
      .HasMany(e => e.Books)
      .WithOne(e => e.BookType)
      .HasForeignKey(e => e.BookTypeId)
      .IsRequired();
    
    // One To One
    modelBuilder.Entity<User>()
      .HasOne(e => e.Translator)
      .WithOne(e => e.User)
      .HasForeignKey<Translator>(e => e.UserId)
      .IsRequired();
  }
}