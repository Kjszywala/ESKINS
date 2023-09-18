using ESKINS.DbServices.Models;
using ESKINS.DbServices.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ItemLocationsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ItemLocationsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ItemLocations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemLocations>>> GetItemLocations()
        {
          if (_context.ItemLocations == null)
          {
              return NotFound();
          }
            return await _context.ItemLocations.ToListAsync();
        }

        // GET: api/ItemLocations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemLocations>> GetItemLocations(int id)
        {
          if (_context.ItemLocations == null)
          {
              return NotFound();
          }
            var itemLocations = await _context.ItemLocations.FindAsync(id);

            if (itemLocations == null)
            {
                return NotFound();
            }

            return itemLocations;
        }

        // PUT: api/ItemLocations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemLocations(int id, ItemLocations itemLocations)
        {
            if (id != itemLocations.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemLocations).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemLocationsExists(id))
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

        // POST: api/ItemLocations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemLocations>> PostItemLocations(ItemLocations itemLocations)
        {
          if (_context.ItemLocations == null)
          {
              return Problem("Entity set 'DatabaseContext.ItemLocations'  is null.");
          }
            _context.ItemLocations.Add(itemLocations);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemLocations", new { id = itemLocations.Id }, itemLocations);
        }

        // DELETE: api/ItemLocations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemLocations(int id)
        {
            if (_context.ItemLocations == null)
            {
                return NotFound();
            }
            var itemLocations = await _context.ItemLocations.FindAsync(id);
            if (itemLocations == null)
            {
                return NotFound();
            }

            _context.ItemLocations.Remove(itemLocations);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemLocationsExists(int id)
        {
            return (_context.ItemLocations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
