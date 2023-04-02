using ESKINS.API.Models;
using ESKINS.API.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SellersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Sellers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sellers>>> GetSellers()
        {
          if (_context.Sellers == null)
          {
              return NotFound();
          }
            return await _context.Sellers.ToListAsync();
        }

        // GET: api/Sellers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sellers>> GetSellers(int id)
        {
          if (_context.Sellers == null)
          {
              return NotFound();
          }
            var sellers = await _context.Sellers.FindAsync(id);

            if (sellers == null)
            {
                return NotFound();
            }

            return sellers;
        }

        // PUT: api/Sellers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSellers(int id, Sellers sellers)
        {
            if (id != sellers.Id)
            {
                return BadRequest();
            }

            _context.Entry(sellers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SellersExists(id))
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

        // POST: api/Sellers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sellers>> PostSellers(Sellers sellers)
        {
          if (_context.Sellers == null)
          {
              return Problem("Entity set 'DatabaseContext.Sellers'  is null.");
          }
            _context.Sellers.Add(sellers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSellers", new { id = sellers.Id }, sellers);
        }

        // DELETE: api/Sellers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSellers(int id)
        {
            if (_context.Sellers == null)
            {
                return NotFound();
            }
            var sellers = await _context.Sellers.FindAsync(id);
            if (sellers == null)
            {
                return NotFound();
            }

            _context.Sellers.Remove(sellers);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SellersExists(int id)
        {
            return (_context.Sellers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
