using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManager.Areas.Identity.Data;
using ResourceManager.Models.Entities;

namespace ResourceManager.Controllers
{

    public class ProjectController : Controller
    {
        private readonly ResourceContext _context;
        public ProjectController(ResourceContext context)
        {
            _context = context;
        }



        #region CRUDproject

        

        public IActionResult Project()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> getAllProject()
        {
            var item = await _context.Projects.ToListAsync();
            return Ok(item);
        }

        [HttpPost]
        //DateTime createDay, DateTime dueday
        public async Task<IActionResult> addProject(string projectName, string status, DateTime createDay, DateTime dueday, string priority, string branch, string instruction)
        {
            
            var item1 = await _context.Projects.ToListAsync();
            int number = item1.Count() + 1;
            var turnTimeCal = dueday-createDay;

            var item = new Project
            {
                ProjectId = Guid.NewGuid(),
                projectName = projectName,
                //ProjectNumber
                Branch= branch,
                createDay = createDay,
                dueDay = dueday,
                priority = priority,
                instruction = instruction,
                status = status,
                turntime = $"{turnTimeCal.Days}d",


                projectNumber = number.ToString("D6"),
            };
            _context.Projects.Add(item);
            await _context.SaveChangesAsync();
            return Ok(item);
            
        }

        [Route("/Project/DeleteProject/{id}")]
        [HttpDelete]

        public async Task<IActionResult> DeleteProject(Guid id)
        {
            try
            {
                var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);
                if (project == null) return NotFound();
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                return StatusCode(202);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
            
        }

        [Route("/Project/UpdateProject/{id}")]
        [HttpPut]

        public async Task<IActionResult> UpdateProject(Guid id, string projectName, string status, DateTime createDay, DateTime dueday, string priority, string branch, string instruction)
        {
            var turnTimeCal = dueday - createDay;
            try
            {
                var item = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);
                if (item == null) return NotFound();

                
                item.projectName = projectName;
                item.Branch = branch;
                item.priority = priority;
                item.instruction = instruction;
                item.status = status;
                item.createDay = createDay;
                item.dueDay = dueday;
                item.turntime = $"{turnTimeCal.Days}d";


                _context.Projects.Update(item);
                await _context.SaveChangesAsync();
                return Ok(item);


            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }
        #endregion

        #region DetailProject

        [Route("/Project/DetailProject/{id}")]
        public IActionResult DetailProject(Guid id)
        {

            var item = _context.Projects.FirstOrDefault(x => x.ProjectId == id);
            if (item == null)
            {
                return NotFound(); // Return a 404 if the project is not found
            }
            ViewData["ProjectId"] = id;
            return View(item);
        }

        [Route("/Project/getProjectByID/{id}")]
        public async Task<IActionResult> getProjectByID(Guid id)
        {
            try
            {
                var item = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == id);
                if (item == null) return NotFound();
                return Ok(item);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
        }

        #endregion

        #region Assgin Project

        [HttpPost]
        [Route("/Project/Assign/{projectId}/{userId}")]

        public async Task<IActionResult> Assign( Guid projectId, Guid userId)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var projectAssign = new ProjectAssign
                    {
                        ProjectId = projectId,
                        UserEmployeeId = userId
                    };
                    _context.ProjectAssigns.Add(projectAssign);

                    await _context.SaveChangesAsync();
                    return Ok(projectAssign);
                }
                else
                {
                    // Log model state errors
                    var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new { errors });
                }
            }
            catch (Exception e)
            {

                return StatusCode(500, new { message = e.Message });
            }
        
            

            
        }

        #endregion

    }
}
