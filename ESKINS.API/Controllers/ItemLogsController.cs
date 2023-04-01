using ESKINS.API.Models;
using ESKINS.API.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemLogsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ItemLogsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ItemLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemLogs>>> GetItemLogs()
        {
          if (_context.ItemLogs == null)
          {
              return NotFound();
          }
            return await _context.ItemLogs.ToListAsync();
        }

        // GET: api/ItemLogs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemLogs>> GetItemLogs(int id)
        {
          if (_context.ItemLogs == null)
          {
              return NotFound();
          }
            var itemLogs = await _context.ItemLogs.FindAsync(id);

            if (itemLogs == null)
            {
                return NotFound();
            }

            return itemLogs;
        }

        // PUT: api/ItemLogs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemLogs(int id, ItemLogs itemLogs)
        {
            if (id != itemLogs.Id)
            {
                return BadRequest();
            }

            _context.Entry(itemLogs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemLogsExists(id))
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

        // POST: api/ItemLogs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemLogs>> PostItemLogs(ItemLogs itemLogs)
        {
          if (_context.ItemLogs == null)
          {
              return Problem("Entity set 'DatabaseContext.ItemLogs'  is null.");
          }
            _context.ItemLogs.Add(itemLogs);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemLogs", new { id = itemLogs.Id }, itemLogs);
        }

        // DELETE: api/ItemLogs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemLogs(int id)
        {
            if (_context.ItemLogs == null)
            {
                return NotFound();
            }
            var itemLogs = await _context.ItemLogs.FindAsync(id);
            if (itemLogs == null)
            {
                return NotFound();
            }

            _context.ItemLogs.Remove(itemLogs);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemLogsExists(int id)
        {
            return (_context.ItemLogs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
