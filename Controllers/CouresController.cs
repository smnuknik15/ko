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
    public class CouresController : ControllerBase
    {
        private readonly OnlinecourseContext _database;
        public CouresController (OnlinecourseContext db)
        {
            _database = db;
        }
        // GET: api/Coures
        [HttpGet]
        public IEnumerable<Coures> Get()
        {
            return _database.Coures;
        }

        // GET: api/Coures/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetCoures([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var coure = await _database.Coures.FindAsync(id);

            if (coure == null)
            {
                return NotFound();
            }

            return Ok(coure);
        }

        // POST: api/Coures
        [HttpPost]
        public async Task<IActionResult> PostCoures([FromBody] Coures c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _database.Coures.Add(c);

            try
            {
                await _database.SaveChangesAsync();
            }

            catch(Exception)
            {
                if (CouresExists(c.CourseId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw ;
                }
              
            }
            return CreatedAtAction("GetCoures", new { id = c.CourseId }, c);
        }

       
        // PUT: api/Coures/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCoures([FromRoute] string id, [FromBody] Coures c)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != c.CourseId)
            {
                return BadRequest();
            }
            _database.Entry(c).State = EntityState.Modified;
            try
            {
                await _database.SaveChangesAsync();
            }
            catch(Exception)
            {
                if (!CouresExists(id))
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
        private bool CouresExists(string id)
        {
            return _database.Coures.Any(e => e.CourseId == id);
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCoures([FromRoute]string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var c = await _database.Coures.FindAsync(id);
            if (c == null)
            {
                return NotFound();
            }
            _database.Coures.Remove(c);
            await _database.SaveChangesAsync();
            return Ok(c);
        }

    }
}
