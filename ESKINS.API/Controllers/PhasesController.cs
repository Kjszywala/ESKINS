using ESKINS.DbServices.Models;
using ESKINS.DbServices.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PhasesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PhasesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Phases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phases>>> GetPhases()
        {
          if (_context.Phases == null)
          {
              return NotFound();
          }
            return await _context.Phases.ToListAsync();
        }

        // GET: api/Phases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phases>> GetPhases(int id)
        {
          if (_context.Phases == null)
          {
              return NotFound();
          }
            var phases = await _context.Phases.FindAsync(id);

            if (phases == null)
            {
                return NotFound();
            }

            return phases;
        }

        // PUT: api/Phases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhases(int id, Phases phases)
        {
            if (id != phases.Id)
            {
                return BadRequest();
            }

            _context.Entry(phases).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhasesExists(id))
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

        // POST: api/Phases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Phases>> PostPhases(Phases phases)
        {
          if (_context.Phases == null)
          {
              return Problem("Entity set 'DatabaseContext.Phases'  is null.");
          }
            _context.Phases.Add(phases);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhases", new { id = phases.Id }, phases);
        }

        // DELETE: api/Phases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhases(int id)
        {
            if (_context.Phases == null)
            {
                return NotFound();
            }
            var phases = await _context.Phases.FindAsync(id);
            if (phases == null)
            {
                return NotFound();
            }

            _context.Phases.Remove(phases);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhasesExists(int id)
        {
            return (_context.Phases?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
