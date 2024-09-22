using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBravo.Models;

namespace WEBAPI_Bravo.Controllers.Users
{
    [Route("api/[controller]")]
    [ApiController]
    public class MsUsersController : ControllerBase
    {
        private readonly BravoContext _context;

        public MsUsersController(BravoContext context)
        {
            _context = context;
        }

        // GET: api/MsUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MsUser>>> GetMsUsers()
        {
            return await _context.MsUsers.ToListAsync();
        }

        // GET: api/MsUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MsUser>> GetMsUser(string id)
        {
            var msUser = await _context.MsUsers.FindAsync(id);

            if (msUser == null)
            {
                return NotFound();
            }

            return msUser;
        }

        // PUT: api/MsUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMsUser(string id, MsUser msUser)
        {
            if (id != msUser.Username)
            {
                return BadRequest();
            }

            _context.Entry(msUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MsUserExists(id))
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

        // POST: api/MsUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MsUser>> PostMsUser(MsUser msUser)
        {
            _context.MsUsers.Add(msUser);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MsUserExists(msUser.Username))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMsUser", new { id = msUser.Username }, msUser);
        }

        // DELETE: api/MsUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMsUser(string id)
        {
            var msUser = await _context.MsUsers.FindAsync(id);
            if (msUser == null)
            {
                return NotFound();
            }

            _context.MsUsers.Remove(msUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MsUserExists(string id)
        {
            return _context.MsUsers.Any(e => e.Username == id);
        }
    }
}
