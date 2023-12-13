using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_softline_test;
using api_softline_test.DTO;

namespace api_softline_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly TasksContext _context;

        public StatusController(TasksContext context)
        {
            _context = context;
        }

        // GET: api/Status
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Status>>> GetStatuses()
        {
          if (_context.Statuses == null)
          {
              return NotFound();
          }
            return await _context.Statuses.ToListAsync();
        }

        // GET: api/Status/x
        [HttpGet("{id}")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
          if (_context.Statuses == null)
          {
              return NotFound();
          }
            var status = await _context.Statuses.FindAsync(id);

            if (status == null)
            {
                return NotFound();
            }

            return status;
        }

        // POST: api/Status
        [HttpPost]
        public async Task<ActionResult<Status>> PostStatus(PostStatusDTO statusInput)
        {
            if (_context.Statuses == null)
            {
                return NotFound();
            }

            Status status = new Status
            {
                Status_ID = statusInput.Status_ID,
                Status_name = statusInput.Status_name
            };

            _context.Statuses.Add(status);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatus", new { id = status.Status_ID }, status);
        }
       
        // DELETE: api/Status/x
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatus(int id)
        {
            if (_context.Statuses == null)
            {
                return NotFound();
            }
            var status = await _context.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }

            _context.Statuses.Remove(status);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusExists(int id)
        {
            return (_context.Statuses?.Any(e => e.Status_ID == id)).GetValueOrDefault();
        }
    }
}
