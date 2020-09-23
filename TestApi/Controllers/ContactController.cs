using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly DataContext _context;

        public ContactController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Contact
        [HttpGet("{userId}")]
        [Route("GetUserContacts/{userId}")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Contact>>> GetUserContacts(int userId)
        {
            return await _context.Contact.Include(x=>x.Phones).Where(x => x.UserID == userId).ToListAsync();
        }

        // GET: api/Contact/5
        [HttpGet("{id}")]
        [Route("GetContact/{id}")]
        [Authorize]
        public async Task<ActionResult<Contact>> GetContact(int id)
        {
            var contact = await _context.Contact.Include(x => x.Phones).Include(x=>x.User).Where(x => x.Id == id).SingleOrDefaultAsync();
            
            if (contact == null)
            {
                return NotFound();
            }

            if(contact.User.Username != this.User.FindFirst(ClaimTypes.NameIdentifier).Value)
                return StatusCode(403);

            return contact;
        }

        // PUT: api/Contact/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutContact(int id, Contact contact)
        {
            if (id != contact.Id)
            {
                return BadRequest();
            }

            _context.Update(contact);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactExists(id))
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

        // POST: api/Contact
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Contact>> PostContact(Contact contact)
        {
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContact", new { id = contact.Id }, contact);
        }

        // DELETE: api/Contact/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Contact>> DeleteContact(int id)
        {
            var contact = await _context.Contact.FindAsync(id);
            if (contact == null)
            {
                return NotFound();
            }

            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        private bool ContactExists(int id)
        {
            return _context.Contact.Any(e => e.Id == id);
        }
    }
}
