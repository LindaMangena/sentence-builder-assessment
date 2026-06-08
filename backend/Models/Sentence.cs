using System.ComponentModel.DataAnnotations;

namespace SentenceBuilder.Api.Models
{
    public class Sentence
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
