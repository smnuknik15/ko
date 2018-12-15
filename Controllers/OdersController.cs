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

    public class OdersController : ControllerBase
    {
        private readonly OnlinecourseContext _database;

        public OdersController(OnlinecourseContext db)
        {
            _database = db;
        }

        // GET: api/Oders
        [HttpGet]
        public IEnumerable<Oders> GetOders()
        {
            return _database.Oders;
        }

        // GET: api/Oders/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOders([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var odr = await _database.Oders.FindAsync(id);

            if (odr == null)
            {
                return NotFound();
            }

            return Ok(odr);
        }

        // PUT: api/Oders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOders([FromRoute] string id, [FromBody] Oders odr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != odr.OderId)
            {
                return BadRequest();
            }

            _database.Entry(odr).State = EntityState.Modified;

            try
            {
                await _database.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OdersExists(id))
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

        // POST: api/Oders
        [HttpPost]
        public async Task<IActionResult> PostOders([FromBody] Oders odr)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _database.Oders.Add(odr);
            try
            {
                await _database.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OdersExists(odr.OderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOders", new { id = odr.OderId }, odr);
        }

        // DELETE: api/Oders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOders([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var odr = await _database.Oders.FindAsync(id);
            if (odr == null)
            {
                return NotFound();
            }

            _database.Oders.Remove(odr);
            await _database.SaveChangesAsync();

            return Ok(odr);
        }

        private bool OdersExists(string id)
        {
            return _database.Oders.Any(e => e.OderId == id);
        }
    }
}