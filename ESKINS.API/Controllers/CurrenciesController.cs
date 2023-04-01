using ESKINS.API.Models;
using ESKINS.API.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CurrenciesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Currencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Currencies>>> GetCurrencies()
        {
          if (_context.Currencies == null)
          {
              return NotFound();
          }
            return await _context.Currencies.ToListAsync();
        }

        // GET: api/Currencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Currencies>> GetCurrencies(int id)
        {
          if (_context.Currencies == null)
          {
              return NotFound();
          }
            var currencies = await _context.Currencies.FindAsync(id);

            if (currencies == null)
            {
                return NotFound();
            }

            return currencies;
        }

        // PUT: api/Currencies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurrencies(int id, Currencies currencies)
        {
            if (id != currencies.Id)
            {
                return BadRequest();
            }

            _context.Entry(currencies).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CurrenciesExists(id))
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

        // POST: api/Currencies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Currencies>> PostCurrencies(Currencies currencies)
        {
          if (_context.Currencies == null)
          {
              return Problem("Entity set 'DatabaseContext.Currencies'  is null.");
          }
            _context.Currencies.Add(currencies);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCurrencies", new { id = currencies.Id }, currencies);
        }

        // DELETE: api/Currencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCurrencies(int id)
        {
            if (_context.Currencies == null)
            {
                return NotFound();
            }
            var currencies = await _context.Currencies.FindAsync(id);
            if (currencies == null)
            {
                return NotFound();
            }

            _context.Currencies.Remove(currencies);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CurrenciesExists(int id)
        {
            return (_context.Currencies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
