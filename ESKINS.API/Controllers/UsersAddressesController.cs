using ESKINS.API.Models;
using ESKINS.API.Models.CMS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ESKINS.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersAddressesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersAddressesController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/UsersAddresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersAddresses>>> GetUsersAddresses()
        {
          if (_context.UsersAddresses == null)
          {
              return NotFound();
          }
            return await _context.UsersAddresses.ToListAsync();
        }

        // GET: api/UsersAddresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsersAddresses>> GetUsersAddresses(int id)
        {
          if (_context.UsersAddresses == null)
          {
              return NotFound();
          }
            var usersAddresses = await _context.UsersAddresses.FindAsync(id);

            if (usersAddresses == null)
            {
                return NotFound();
            }

            return usersAddresses;
        }

        // PUT: api/UsersAddresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsersAddresses(int id, UsersAddresses usersAddresses)
        {
            if (id != usersAddresses.Id)
            {
                return BadRequest();
            }

            _context.Entry(usersAddresses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersAddressesExists(id))
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

        // POST: api/UsersAddresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UsersAddresses>> PostUsersAddresses(UsersAddresses usersAddresses)
        {
          if (_context.UsersAddresses == null)
          {
              return Problem("Entity set 'DatabaseContext.UsersAddresses'  is null.");
          }
            _context.UsersAddresses.Add(usersAddresses);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUsersAddresses", new { id = usersAddresses.Id }, usersAddresses);
        }

        // DELETE: api/UsersAddresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsersAddresses(int id)
        {
            if (_context.UsersAddresses == null)
            {
                return NotFound();
            }
            var usersAddresses = await _context.UsersAddresses.FindAsync(id);
            if (usersAddresses == null)
            {
                return NotFound();
            }

            _context.UsersAddresses.Remove(usersAddresses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersAddressesExists(int id)
        {
            return (_context.UsersAddresses?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
