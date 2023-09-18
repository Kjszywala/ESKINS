using ESKINS.DbServices.Models;
using ESKINS.DbServices.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TargetsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TargetsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Targets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Targets>>> GetTargets()
        {
          if (_context.Targets == null)
          {
              return NotFound();
          }
            return await _context.Targets.ToListAsync();
        }

        // GET: api/Targets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Targets>> GetTargets(int id)
        {
          if (_context.Targets == null)
          {
              return NotFound();
          }
            var targets = await _context.Targets.FindAsync(id);

            if (targets == null)
            {
                return NotFound();
            }

            return targets;
        }

        // PUT: api/Targets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTargets(int id, Targets targets)
        {
            if (id != targets.Id)
            {
                return BadRequest();
            }

            _context.Entry(targets).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TargetsExists(id))
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

        // POST: api/Targets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Targets>> PostTargets(Targets targets)
        {
          if (_context.Targets == null)
          {
              return Problem("Entity set 'DatabaseContext.Targets'  is null.");
          }
            _context.Targets.Add(targets);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTargets", new { id = targets.Id }, targets);
        }

        // DELETE: api/Targets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTargets(int id)
        {
            if (_context.Targets == null)
            {
                return NotFound();
            }
            var targets = await _context.Targets.FindAsync(id);
            if (targets == null)
            {
                return NotFound();
            }

            _context.Targets.Remove(targets);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TargetsExists(int id)
        {
            return (_context.Targets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
