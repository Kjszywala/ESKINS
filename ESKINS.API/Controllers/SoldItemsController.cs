using ESKINS.API.Models;
using ESKINS.API.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SoldItemsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public SoldItemsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/SoldItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SoldItems>>> GetSoldItems()
        {
          if (_context.SoldItems == null)
          {
              return NotFound();
          }
            return await _context.SoldItems.ToListAsync();
        }

        // GET: api/SoldItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SoldItems>> GetSoldItems(int id)
        {
          if (_context.SoldItems == null)
          {
              return NotFound();
          }
            var soldItems = await _context.SoldItems.FindAsync(id);

            if (soldItems == null)
            {
                return NotFound();
            }

            return soldItems;
        }

        // PUT: api/SoldItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSoldItems(int id, SoldItems soldItems)
        {
            if (id != soldItems.Id)
            {
                return BadRequest();
            }

            _context.Entry(soldItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SoldItemsExists(id))
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

        // POST: api/SoldItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SoldItems>> PostSoldItems(SoldItems soldItems)
        {
          if (_context.SoldItems == null)
          {
              return Problem("Entity set 'DatabaseContext.SoldItems'  is null.");
          }
            _context.SoldItems.Add(soldItems);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSoldItems", new { id = soldItems.Id }, soldItems);
        }

        // DELETE: api/SoldItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSoldItems(int id)
        {
            if (_context.SoldItems == null)
            {
                return NotFound();
            }
            var soldItems = await _context.SoldItems.FindAsync(id);
            if (soldItems == null)
            {
                return NotFound();
            }

            _context.SoldItems.Remove(soldItems);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SoldItemsExists(int id)
        {
            return (_context.SoldItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
