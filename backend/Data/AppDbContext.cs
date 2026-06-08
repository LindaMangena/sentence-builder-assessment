using Microsoft.EntityFrameworkCore;
using SentenceBuilder.Api.Models;
using System.Text.Json;

namespace SentenceBuilder.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Word> Words { get; set; }
        public DbSet<Sentence> Sentences { get; set; }
    }

    public static class DbSeeder
    {
        public static void Seed(AppDbContext db, IWebHostEnvironment environment)
        {
            if (db.Words.Any()) return;

            var seedPath = Path.Combine(environment.ContentRootPath, "Data", "words.json");
            var seedJson = File.ReadAllText(seedPath);
            var words = JsonSerializer.Deserialize<List<Word>>(seedJson, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<Word>();

            db.Words.AddRange(words);
            db.SaveChanges();
        }
    }
}
