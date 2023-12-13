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
    public class TasksController : ControllerBase
    {
        private readonly TasksContext _context;

        public TasksController(TasksContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskDto>>> GetTasks()
        {
            List<TaskDto> tasks = await _context.Tasks
                .Include(t => t.Status)
                .Select(t => new TaskDto
                {
                    ID = t.ID,
                    Name = t.Name,
                    Description = t.Description,
                    Status_ID = t.Status_ID,
                    StatusName = t.Status.Status_name
                })
                .ToListAsync();

            if (tasks == null)
            {
                return NotFound();
            }

            return tasks;
        }

        // GET: api/Tasks/x
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetTask(int id)
        {
            var task = await _context.Tasks
                .Include(t => t.Status)
                .Where(t => t.ID == id)
                .Select(t => new TaskDto
                {
                    ID = t.ID,
                    Name = t.Name,
                    Description = t.Description,
                    Status_ID = t.Status_ID,
                    StatusName = t.Status.Status_name
                })
                .FirstOrDefaultAsync();

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Tasks/x
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskDto>> PutTask(int id, UpdateTaskDto updateTaskDto)
        {
            if (updateTaskDto == null)
            {
                return BadRequest("Incorrect data");
            }

            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return NotFound("Task not found");
            }

            var existingStatus = await _context.Statuses.FindAsync(updateTaskDto.Status_ID);
            if (existingStatus == null)
            {
                return BadRequest("Status not found at the database");
            }

            existingTask.Name = updateTaskDto.Name;
            existingTask.Description = updateTaskDto.Description;
            existingTask.Status_ID = updateTaskDto.Status_ID;

            await _context.SaveChangesAsync();

            TaskDto updatedTaskDto = new TaskDto
            {
                ID = existingTask.ID,
                Name = existingTask.Name,
                Description = existingTask.Description,
                Status_ID = existingTask.Status_ID,
                StatusName = existingStatus.Status_name
            };

            return Ok(updatedTaskDto);
        }

        // POST: api/Tasks/
        [HttpPost]
        public async Task<ActionResult<TaskDto>> PostTask(PostTaskDto createTaskDto)
        {
            if (createTaskDto == null)
            {
                return BadRequest("incorrect data");
            }

            var existingStatus = await _context.Statuses.FindAsync(createTaskDto.Status_ID);
            if (existingStatus == null)
            {
                return BadRequest("status not found at the database");
            }

            TaskDb newTask = new TaskDb
            {
                Name = createTaskDto.Name,
                Description = createTaskDto.Description,
                Status_ID = createTaskDto.Status_ID
            };

            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();

            TaskDto createdTaskDto = new TaskDto
            {
                ID = newTask.ID,
                Name = newTask.Name,
                Description = newTask.Description,
                Status_ID = newTask.Status_ID,
                StatusName = existingStatus.Status_name
            };

            return CreatedAtAction(nameof(GetTask), new { id = createdTaskDto.ID }, createdTaskDto);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskDb(int id)
        {
            if (_context.Tasks == null)
            {
                return NotFound();
            }
            var taskDb = await _context.Tasks.FindAsync(id);
            if (taskDb == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(taskDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TasksExists(int id)
        {
            return (_context.Tasks?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
