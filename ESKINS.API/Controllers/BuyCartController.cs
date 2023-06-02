using ESKINS.API.Models;
using ESKINS.API.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ESKINS.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BuyCartController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public BuyCartController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/BuyCarts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BuyCart>>> GetBuyCarts()
        {
            if (_context.BuyCarts == null)
            {
                return NotFound();
            }
            return await _context.BuyCarts.ToListAsync();
        }

        // GET: api/BuyCarts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BuyCart>> GetBuyCarts(int id)
        {
            if (_context.BuyCarts == null)
            {
                return NotFound();
            }
            var BuyCarts = await _context.BuyCarts.FindAsync(id);

            if (BuyCarts == null)
            {
                return NotFound();
            }

            return BuyCarts;
        }

        // PUT: api/BuyCarts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBuyCarts(int id, BuyCart BuyCarts)
        {
            if (id != BuyCarts.Id)
            {
                return BadRequest();
            }

            _context.Entry(BuyCarts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BuyCartsExists(id))
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

        // POST: api/BuyCarts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BuyCart>> PostBuyCarts(BuyCart BuyCarts)
        {
            try
            {
                if (_context.BuyCarts == null)
                {
                    return Problem("Entity set 'DatabaseContext.BuyCarts'  is null.");
                }
                _context.BuyCarts.Add(BuyCarts);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetBuyCarts", new { id = BuyCarts.Id }, BuyCarts);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        // DELETE: api/BuyCarts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBuyCarts(int id)
        {
            if (_context.BuyCarts == null)
            {
                return NotFound();
            }
            var buyCarts = await _context.BuyCarts.FindAsync(id);
            if (buyCarts == null)
            {
                return NotFound();
            }

            _context.BuyCarts.Remove(buyCarts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BuyCartsExists(int id)
        {
            return (_context.BuyCarts?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
