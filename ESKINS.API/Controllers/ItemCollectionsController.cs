using ESKINS.API.Models;
using ESKINS.API.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemCollectionsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ItemCollectionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ItemCollections
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCollections>>> GetItemCollections()
        {
          if (_context.ItemCollections == null)
          {
              return NotFound();
          }
            return await _context.ItemCollections.ToListAsync();
        }

        // GET: api/ItemCollections/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemCollections>> GetItemCollections(int id)
        {
          if (_context.ItemCollections == null)
          {
              return NotFound();
          }
            var itemCollections = await _context.ItemCollections.FindAsync(id);

            if (itemCollections == null)
            {
                return NotFound();
            }

            return itemCollections;
        }

        // PUT: api/ItemCollections/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemCollections(int id, ItemCollections itemCollections)
        {
            if (id != itemCollections.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemCollections).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemCollectionsExists(id))
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

        // POST: api/ItemCollections
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemCollections>> PostItemCollections(ItemCollections itemCollections)
        {
          if (_context.ItemCollections == null)
          {
              return Problem("Entity set 'DatabaseContext.ItemCollections'  is null.");
          }
            _context.ItemCollections.Add(itemCollections);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemCollections", new { id = itemCollections.Id }, itemCollections);
        }

        // DELETE: api/ItemCollections/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemCollections(int id)
        {
            if (_context.ItemCollections == null)
            {
                return NotFound();
            }
            var itemCollections = await _context.ItemCollections.FindAsync(id);
            if (itemCollections == null)
            {
                return NotFound();
            }

            _context.ItemCollections.Remove(itemCollections);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemCollectionsExists(int id)
        {
            return (_context.ItemCollections?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
