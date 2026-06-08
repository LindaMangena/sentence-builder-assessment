using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentenceBuilder.Api.Data;

namespace SentenceBuilder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WordsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public WordsController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var words = await _db.Words.OrderBy(w => w.Type).ThenBy(w => w.Text).ToListAsync();
            var grouped = words.GroupBy(w => w.Type)
                .ToDictionary(g => g.Key, g => g.Select(w => new { w.Id, w.Text }).ToList());
            return Ok(grouped);
        }
    }
}
