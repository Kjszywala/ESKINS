using ESKINS.DbServices.Models;
using ESKINS.DbServices.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ItemPriceHistoriesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ItemPriceHistoriesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ItemPriceHistories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemPriceHistories>>> GetItemPriceHistories()
        {
          if (_context.ItemPriceHistories == null)
          {
              return NotFound();
          }
            return await _context.ItemPriceHistories.ToListAsync();
        }

        // GET: api/ItemPriceHistories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemPriceHistories>> GetItemPriceHistories(int id)
        {
          if (_context.ItemPriceHistories == null)
          {
              return NotFound();
          }
            var itemPriceHistories = await _context.ItemPriceHistories.FindAsync(id);

            if (itemPriceHistories == null)
            {
                return NotFound();
            }

            return itemPriceHistories;
        }

        // PUT: api/ItemPriceHistories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemPriceHistories(int id, ItemPriceHistories itemPriceHistories)
        {
            if (id != itemPriceHistories.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemPriceHistories).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemPriceHistoriesExists(id))
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

        // POST: api/ItemPriceHistories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemPriceHistories>> PostItemPriceHistories(ItemPriceHistories itemPriceHistories)
        {
          if (_context.ItemPriceHistories == null)
          {
              return Problem("Entity set 'DatabaseContext.ItemPriceHistories'  is null.");
          }
            _context.ItemPriceHistories.Add(itemPriceHistories);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemPriceHistories", new { id = itemPriceHistories.Id }, itemPriceHistories);
        }

        // DELETE: api/ItemPriceHistories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemPriceHistories(int id)
        {
            if (_context.ItemPriceHistories == null)
            {
                return NotFound();
            }
            var itemPriceHistories = await _context.ItemPriceHistories.FindAsync(id);
            if (itemPriceHistories == null)
            {
                return NotFound();
            }

            _context.ItemPriceHistories.Remove(itemPriceHistories);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemPriceHistoriesExists(int id)
        {
            return (_context.ItemPriceHistories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
