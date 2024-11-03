using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiBravo.Models;

namespace WEBAPI_Bravo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MCustomersController : ControllerBase
    {
        private readonly BravoContext _context;

        public MCustomersController(BravoContext context)
        {
            _context = context;
        }

        // GET: api/MCustomers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MCustomer>>> GetMCustomers()
        {
            try
            {
                var customers = await _context.MCustomers.Take(100).ToListAsync();
                return Ok(customers); // Return 200 OK with the list of customers
            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                // Example: _logger.LogError(ex, "An error occurred while fetching customers.");

                return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error
            }
        }

        // GET: api/MCustomers/5
    //    [HttpGet("{id}")]
    //    public async Task<ActionResult<MCustomer>> GetMCustomer(string id)
    //    {
    //        var mCustomer = await _context.MCustomers.FindAsync(id);

    //        if (mCustomer == null)
    //        {
    //            return NotFound();
    //        }

    //        return mCustomer;
    //    }

    //    // PUT: api/MCustomers/5
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPut("{id}")]
    //    public async Task<IActionResult> PutMCustomer(string id, MCustomer mCustomer)
    //    {
    //        if (id != mCustomer.CustomerId)
    //        {
    //            return BadRequest();
    //        }

    //        _context.Entry(mCustomer).State = EntityState.Modified;

    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateConcurrencyException)
    //        {
    //            if (!MCustomerExists(id))
    //            {
    //                return NotFound();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return NoContent();
    //    }

    //    // POST: api/MCustomers
    //    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    //    [HttpPost]
    //    public async Task<ActionResult<MCustomer>> PostMCustomer(MCustomer mCustomer)
    //    {
    //        _context.MCustomers.Add(mCustomer);
    //        try
    //        {
    //            await _context.SaveChangesAsync();
    //        }
    //        catch (DbUpdateException)
    //        {
    //            if (MCustomerExists(mCustomer.CustomerId))
    //            {
    //                return Conflict();
    //            }
    //            else
    //            {
    //                throw;
    //            }
    //        }

    //        return CreatedAtAction("GetMCustomer", new { id = mCustomer.CustomerId }, mCustomer);
    //    }

    //    // DELETE: api/MCustomers/5
    //    [HttpDelete("{id}")]
    //    public async Task<IActionResult> DeleteMCustomer(string id)
    //    {
    //        var mCustomer = await _context.MCustomers.FindAsync(id);
    //        if (mCustomer == null)
    //        {
    //            return NotFound();
    //        }

    //        _context.MCustomers.Remove(mCustomer);
    //        await _context.SaveChangesAsync();

    //        return NoContent();
    //    }

    //    private bool MCustomerExists(string id)
    //    {
    //        return _context.MCustomers.Any(e => e.CustomerId == id);
    //    }
   }
}
