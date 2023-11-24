using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Eventing.Reader;
using System.Security.Claims;
using System.Threading.Tasks;
using TaskAndUsers.Data;
using TaskAndUsers.Models;

namespace TaskAndUsers.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksAndUsersController : ControllerBase
    {
        private readonly AppDbContextClass _context;
        private readonly UserManager<IdentityUser> _userManager;

        public TasksAndUsersController(AppDbContextClass appDbContextClass, UserManager<IdentityUser> userManager)
        {
            _context = appDbContextClass;
            _userManager = userManager; 

        }
        [HttpGet]

        public async Task<ActionResult<IEnumerable<TaskUsersModel>>>GetTasks() 
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var tasks = await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
            return tasks;
        }


        [HttpPost]
        public async Task<ActionResult<TaskUsersModel>> PostTask(TaskUsersModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.UserId = userId;

            model.CreatedAt = DateTime.Now;
            _context.Tasks.Add(model);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetTasks), new { id = model.Id }, model); new {id=model.Id},model);
        }
        [HttpPut("{id}")]
        
        public async Task <IActionResult> PutTask(int id,TaskUsersModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var existingTask = await _context.Tasks.FindAsync(id);

            if (existingTask == null || existingTask.UserId != userId)
            {
                return NotFound();
            }

            _context.Entry(model).State=EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!TaskExists(id))
                    return NotFound();

                else

                        throw;
                }
                return NoContent();
                
            }
        

        [HttpDelete("{id}")]

        public async Task <IActionResult> DeleteTask(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var task =await _context.Tasks.FindAsync(id);

            if(task == null||task.UserId!=userId)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id) {
            return _context.Tasks.Any(e => e.Id == id);

        }


    }
}
