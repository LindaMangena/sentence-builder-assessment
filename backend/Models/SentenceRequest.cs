using System.ComponentModel.DataAnnotations;

namespace SentenceBuilder.Api.Models
{
    public class SentenceRequest
    {
        [Required]
        [StringLength(300)]
        public string Text { get; set; } = string.Empty;
    }
}
