using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentenceBuilder.Api.Data;
using SentenceBuilder.Api.Models;

namespace SentenceBuilder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SentencesController : ControllerBase
    {
        private readonly AppDbContext _db;
        public SentencesController(AppDbContext db) { _db = db; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _db.Sentences.OrderByDescending(s => s.CreatedAt).ToListAsync();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var sentence = await _db.Sentences.FindAsync(id);
            return sentence == null ? NotFound() : Ok(sentence);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SentenceRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var text = request.Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) return BadRequest("Sentence text is required.");

            var sentence = new Sentence { Text = text };
            _db.Sentences.Add(sentence);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = sentence.Id }, sentence);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SentenceRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var existing = await _db.Sentences.FindAsync(id);
            if (existing == null) return NotFound();
            var text = request.Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) return BadRequest("Sentence text is required.");

            existing.Text = text;
            await _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _db.Sentences.FindAsync(id);
            if (existing == null) return NotFound();

            _db.Sentences.Remove(existing);
            await _db.SaveChangesAsync();
            return NoContent();
        }
    }
}
