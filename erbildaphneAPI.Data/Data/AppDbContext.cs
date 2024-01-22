
using erbildaphneAPI.DataAccess.Identity.Models;
using erbildaphneAPI.Entity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace erbildaphneAPI.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int>
    {
        public AppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Rumor> Rumors { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<GNews> GNews { get; set; }
        public DbSet<GNewsSource> GNewsSources { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SecondArticle> SecondArticles { get; set; }        
        public DbSet<MainNews> MainNews { get; set; }




        protected override void OnModelCreating(ModelBuilder builder)
        {
            //Seed Data
            builder.Entity<Rumor>().Property(rumor => rumor.PictureUrl).HasDefaultValue("/uploads/rumors/rumorPp.png");

            //builder.Entity<Author>().HasData(
            //    new Author { Id = 1, Name = "Erbil Gunasti", Bio = "Erbil Gunasti Biografi", PictureUrl = "/uploads/authors/1.png", Facebook = "facebook.com", Instagram = "instagram.com", Description = "açıklama", Linkedin = "linkedin.com", Website = "google.com" },
            //    new Author { Id = 2, Name = "Daphne Barak", Bio = "Daphne Barak Biografi", PictureUrl = "/uploads/authors/2.png", Facebook = "facebook.com", Instagram = "instagram.com", Description = "açıklama", Linkedin = "linkedin.com", Website = "google.com" });


            //builder.Entity<Article>().HasData(
            //    new Article { Id = 1, Title = "Türkiye Amerikayı Devralıyor", AuthorId = 1, Description = "Türkiye hakkında", Content = "test content", PictureUrl = "/uploads/articles/2.png" },
            //    new Article { Id = 2, Title = "Çini Açıp Kapatmak", AuthorId = 1, Description = "Türkiye hakkında", Content = "test content", PictureUrl = "/uploads/articles/2.png" });



            //builder.Entity<Rumor>().HasData(
            //    new Rumor { Id = 1, Title = "Erdoğan Trump İle Görüştü", Description = "Trump ile erdoğanın görüşmesi...", Content = "Trump ile erdoğanın görüştü ve vs" },
            //    new Rumor { Id = 2, Title = "Erdoğan Trump İle Görüşmedi", Description = "Trump ile erdoğanın görüşmemesi...", Content = "Trump ile erdoğanın görüştü ve vs" },
            //    new Rumor { Id = 3, Title = "Erdoğan Trump İle Görüşmek istiyor", Description = "Trump ile erdoğanın görüşmek istemesi...", Content = "Trump ile erdoğanın görüştü ve vs" }
            //    );











            base.OnModelCreating(builder);
        }
    }
}
