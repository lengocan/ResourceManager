﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ResourceManager.Areas.Identity.Data;
using ResourceManager.Data;
using ResourceManager.Models.Entities;
using System.Net.Mail;
using System.Security.Claims;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ResourceManager.Controllers
{

    public class ProjectController : Controller
    {
        private readonly ResourceContext _context;
        private readonly UserIdentityContext _userManager;
        public ProjectController(ResourceContext context, UserIdentityContext userManager)

        {
            _context = context;
            _userManager = userManager;
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

        [HttpGet]
        public async Task<IActionResult> getAllProjectAsCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();

            }
            var projectAssigns = await _context.ProjectAssigns
                .Where(t => t.UserEmployeeId.ToString() == userId).ToListAsync();

            var projectIds = projectAssigns.Select(pa => pa.ProjectId).ToList();

            var projects = new List<Project>();

            foreach (var item in projectIds)
            {
                var project = await _context.Projects.FirstOrDefaultAsync(x => x.ProjectId == item);

                if (project != null)
                {
                    projects.Add(project);
                }

            }

            return Ok(projects.Select(u => new
            {
                u.ProjectId,
                u.projectName,
                u.projectNumber,
                u.status,
                u.createDay,
                u.dueDay,
                u.turntime,
                u.Branch,
                u.priority,
                u.instruction
            }));
        }

        [HttpPost]
        //DateTime createDay, DateTime dueday
        public async Task<IActionResult> addProject(string projectName, string status, DateTime createDay, DateTime dueday, string priority, string branch, string instruction)
        {

            var item1 = await _context.Projects.ToListAsync();
            int number = item1.Count() + 1;
            var turnTimeCal = dueday - createDay;

            var item = new Project
            {
                ProjectId = Guid.NewGuid(),
                projectName = projectName,
                //ProjectNumber
                Branch = branch,
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
                return NotFound(); 
            }
            ViewData["ProjectName"] = id;
            ViewData["ProjectName"] = item.projectName;
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

        [Route("/Project/Assign")]
        [HttpPost]

        public async Task<IActionResult> Assign(string projectId, string userId)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id.ToString() == userId);
            if (user == null)
            {
                return BadRequest("User not found");
            }

            
            var project = await _context.Projects.FindAsync(Guid.Parse(projectId));
            if (project == null)
            {
                return BadRequest("Project not found");
            }

            // Create the project assignment
            var projectAssign = new ProjectAssign
            {
                ProjectId = Guid.Parse(projectId),
                UserEmployeeId = Guid.Parse(userId)
            };

            
            _context.ProjectAssigns.Add(projectAssign);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok();
            }

            return StatusCode(500, "Error assigning user to project");
        }
        [Route("/Project/AssignMultipleUser")]
        [HttpPost]

        public async Task<IActionResult> AssignMultipleUser(string projectId, List<string> userIds)
        {


            foreach (var userid in userIds)
            {
                var user = _userManager.Users.FirstOrDefault(x => x.Id.ToString() == userid);
                if (user == null)
                {
                    return BadRequest("User not found");
                }

               
                var project = await _context.Projects.FindAsync(Guid.Parse(projectId));
                if (project == null)
                {
                    return BadRequest("Project not found");
                }

                
                var projectAssign = new ProjectAssign
                {
                    ProjectId = Guid.Parse(projectId),
                    UserEmployeeId = Guid.Parse(userid)
                };

                
                _context.ProjectAssigns.Add(projectAssign);



            }
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return Ok();
            }
            return StatusCode(500, "Error assigning user to project");
        }


        [HttpGet]
        [Route("/Project/GetAssignee/{projectId}")]
        public async Task<IActionResult> GetAssignee(Guid projectId)
        {

            var projectAssigns = await _context.ProjectAssigns
                                    .Where(pa => pa.ProjectId == projectId)
                                    .ToListAsync();

            var assigneeIds = projectAssigns.Select(pa => pa.UserEmployeeId).ToList();

            var users = new List<UserEmployee>();

            foreach (var item in assigneeIds)
            {
                var user = await _userManager.Users.OfType<UserEmployee>().FirstOrDefaultAsync(x => x.Id == item.ToString());

                if (user != null)
                {
                    users.Add(user);
                }

            }
            return Ok(users.Select(u => new
            {
                u.Id,
                u.UserName,
                u.Email,
                u.dob,
                u.FullName,
                u.address,
                u.dayJoin,
                u.team,
                u.IsActive,
            }));
        }


        [HttpDelete]
        [Route("/Project/DeleteAssign/{projectId}/{userId}")]
        public async Task<IActionResult> DeleteAssign(Guid projectId, Guid userId)
        {
            var projectAssign = await _context.ProjectAssigns
            .FirstOrDefaultAsync(pa => pa.ProjectId == projectId && pa.UserEmployeeId == userId);

            if (projectAssign == null)
            {
                return NotFound("Assignment not found");
            }

            _context.ProjectAssigns.Remove(projectAssign);
            var result = await _context.SaveChangesAsync();

            if (result > 0)
            {
                return StatusCode(202);
            }

            return StatusCode(500, "Error removing user assignment from project");
        }
        #endregion


        #region
        [Route("/Project/UploadFile")]
        [HttpPost]
        public async Task<IActionResult> UploadFile(Guid projectId, List<IFormFile> files)
        {
            if (files == null || files.Count == 0)
            {
                return BadRequest("No files uploaded.");
            }

            var uploadedFiles = new List<AttachFile>();


            foreach (var file in files)
            {
                
                var fileName = Path.GetFileName(file.FileName);

                
                var directoryPath = Path.Combine("wwwroot", "filePath", projectId.ToString());

                // check the directory 
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                // Combine 
                var filePath = Path.Combine(directoryPath, fileName);

                // Save the file to the server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Create a new AttachFile entity
                var attachFile = new AttachFile
                {
                    Id = Guid.NewGuid(),
                    FileName = fileName,
                    FilePath = $"/filePath/{projectId}/{fileName}",
                };

                var projectAttachFile = new ProjectAttachFile
                {
                    ProjectId = projectId,
                    attachFileId = attachFile.Id
                };

                await _context.AttachFiles.AddAsync(attachFile);
                await _context.ProjectAttachFiles.AddAsync(projectAttachFile);
                uploadedFiles.Add(attachFile);
            }

            await _context.SaveChangesAsync();

            return Ok(uploadedFiles);


        }

        [HttpGet]
        [Route("/Project/GetProjectAttachments/{id}")]
        public async Task<IActionResult> GetProjectAttachments(Guid id)
        {
            
            var attachments = await _context.ProjectAttachFiles
                .Where(paf => paf.ProjectId == id)
                .Join(
                    _context.AttachFiles,
                    paf => paf.attachFileId,
                    af => af.Id,
                    (paf, af) => new
                    {
                        af.FileName,
                        af.FilePath,
                        af.Id
                    }
                )
                .ToListAsync();

            
            return Ok(attachments);
        }

        [HttpDelete]
        [Route("/Project/DeleteAttachFile/{attachId}")]
        public async Task<IActionResult> DeleteAttachFile(Guid attachId)
        {
            var attachFile = await _context.AttachFiles.FindAsync(attachId);
            if (attachFile == null)
            {
                return NotFound("Attachment not found.");
            }

            // Find the corresponding entry in ProjectAttachFile
            var projectAttachFile = await _context.ProjectAttachFiles
                .FirstOrDefaultAsync(paf => paf.attachFileId == attachId);
            if (projectAttachFile != null)
            {
                _context.ProjectAttachFiles.Remove(projectAttachFile);
            }

            // Remove the file from the server (optional)
            var filePath = Path.Combine("wwwroot", attachFile.FilePath.TrimStart('/'));
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            // Remove the attach file record from the database
            _context.AttachFiles.Remove(attachFile);
            await _context.SaveChangesAsync();

            return Ok("Attachment deleted successfully.");
        }
        #endregion
    }
}
