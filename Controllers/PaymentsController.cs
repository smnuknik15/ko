using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineCourseWeb.Models;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Cors;


namespace OnlineCourseWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class PaymentsController : ControllerBase
    {
        private readonly OnlinecourseContext _database;

        public PaymentsController(OnlinecourseContext db)
        {
            _database = db;
        }

        // GET: api/Payments
        [HttpGet]
        public IEnumerable<Payments> GetPayments()
        {
            return _database.Payments;
        }

        // GET: api/Payments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayments([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payments = await _database.Payments.FindAsync(id);

            if (payments == null)
            {
                return NotFound();
            }

            return Ok(payments);
        }

        // PUT: api/Payments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPayments([FromRoute] string id, [FromBody] Payments pym)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pym.PmtId)
            {
                return BadRequest();
            }

            _database.Entry(pym).State = EntityState.Modified;

            try
            {
                await _database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentsExists(id))
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

        // POST: api/Payments
        [HttpPost]
        public async Task<IActionResult> PostPayments([FromBody] Payments pym)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _database.Payments.Add(pym);
            try
            {
                await _database.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PaymentsExists(pym.PmtId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPayments", new { id = pym.PmtId }, pym);
        }

        // DELETE: api/Payments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayments([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var payments = await _database.Payments.FindAsync(id);
            if (payments == null)
            {
                return NotFound();
            }

            _database.Payments.Remove(payments);
            await _database.SaveChangesAsync();

            return Ok(payments);
        }

        private bool PaymentsExists(string id)
        {
            return _database.Payments.Any(e => e.PmtId == id);
        }
    }
}