using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManager.Data;
using ResourceManager.Models.Entities;
using System.Security.Claims;

namespace ResourceManager.Controllers
{
    public class TodoListController : Controller
    {
        private readonly ResourceContext _context;
        private readonly UserIdentityContext _userManager;

        public TodoListController(ResourceContext context, UserIdentityContext userManager)

        {
            _context = context;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult TodoList()
        {
            return View();
        }

        #region addTASK


        [Route("/TodoList/addTask/{projectId}")]
        [HttpPost]
        public async Task<IActionResult> addTask(Guid projectId, string taskName, string estimateHour)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();

            }
            var item = new TodoList
            {
                TodoListId = new Guid(),
                ProjectId = projectId,
                UserId = Guid.Parse(userId),
                taskName = taskName,
                estimateHour = estimateHour,
                isCompleted = false

            };
            await _context.TodoList.AddAsync(item);
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        [Route("/TodoList/getAllTask/{projectId}")]
        [HttpGet]
        public async Task<IActionResult> getAllTask(Guid projectId)
        {
            var todoLists = await _context.TodoList
                                      .Where(t => t.ProjectId == projectId)
                                      .ToListAsync();
            return Ok(todoLists);
        }
        [Route("/TodoList/deletetask/{taskId}")]
        [HttpDelete]
        public async Task<IActionResult> deletetask(Guid taskId)
        {
            try
            {
                var task = await _context.TodoList.FirstOrDefaultAsync(x => x.TodoListId == taskId);
                if (task == null) return NotFound();
                _context.TodoList.Remove(task);
                await _context.SaveChangesAsync();
                return StatusCode(202);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [Route("/TodoList/updateStatus/{taskId}")]
        [HttpPatch]
        public async Task<IActionResult> updateStatus(Guid taskId, bool iscomplete)
        {
            try
            {
                var newTask = await _context.TodoList.FirstOrDefaultAsync(x => x.TodoListId == taskId);
                if (newTask == null) return NotFound();

                newTask.isCompleted = iscomplete;

                _context.TodoList.Update(newTask);  
                await _context.SaveChangesAsync();
                return Ok(newTask);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }
        #endregion
    }
}
