using ESKINS.API.Models;
using ESKINS.API.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class QualitiesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public QualitiesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Qualities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Qualities>>> GetQualities()
        {
          if (_context.Qualities == null)
          {
              return NotFound();
          }
            return await _context.Qualities.ToListAsync();
        }

        // GET: api/Qualities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Qualities>> GetQualities(int id)
        {
          if (_context.Qualities == null)
          {
              return NotFound();
          }
            var qualities = await _context.Qualities.FindAsync(id);

            if (qualities == null)
            {
                return NotFound();
            }

            return qualities;
        }

        // PUT: api/Qualities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQualities(int id, Qualities qualities)
        {
            if (id != qualities.Id)
            {
                return BadRequest();
            }

            _context.Entry(qualities).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualitiesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Qualities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Qualities>> PostQualities(Qualities qualities)
        {
          if (_context.Qualities == null)
          {
              return Problem("Entity set 'DatabaseContext.Qualities'  is null.");
          }
            _context.Qualities.Add(qualities);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQualities", new { id = qualities.Id }, qualities);
        }

        // DELETE: api/Qualities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQualities(int id)
        {
            if (_context.Qualities == null)
            {
                return NotFound();
            }
            var qualities = await _context.Qualities.FindAsync(id);
            if (qualities == null)
            {
                return NotFound();
            }

            _context.Qualities.Remove(qualities);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QualitiesExists(int id)
        {
            return (_context.Qualities?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
