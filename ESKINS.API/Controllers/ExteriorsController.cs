using ESKINS.API.Models;
using ESKINS.API.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ExteriorsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ExteriorsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Exteriors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Exteriors>>> GetExteriors()
        {
          if (_context.Exteriors == null)
          {
              return NotFound();
          }
            return await _context.Exteriors.ToListAsync();
        }

        // GET: api/Exteriors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Exteriors>> GetExteriors(int id)
        {
          if (_context.Exteriors == null)
          {
              return NotFound();
          }
            var exteriors = await _context.Exteriors.FindAsync(id);

            if (exteriors == null)
            {
                return NotFound();
            }

            return exteriors;
        }

        // PUT: api/Exteriors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExteriors(int id, Exteriors exteriors)
        {
            if (id != exteriors.Id)
            {
                return BadRequest();
            }

            _context.Entry(exteriors).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExteriorsExists(id))
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

        // POST: api/Exteriors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Exteriors>> PostExteriors(Exteriors exteriors)
        {
          if (_context.Exteriors == null)
          {
              return Problem("Entity set 'DatabaseContext.Exteriors'  is null.");
          }
            _context.Exteriors.Add(exteriors);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetExteriors", new { id = exteriors.Id }, exteriors);
        }

        // DELETE: api/Exteriors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExteriors(int id)
        {
            if (_context.Exteriors == null)
            {
                return NotFound();
            }
            var exteriors = await _context.Exteriors.FindAsync(id);
            if (exteriors == null)
            {
                return NotFound();
            }

            _context.Exteriors.Remove(exteriors);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExteriorsExists(int id)
        {
            return (_context.Exteriors?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
