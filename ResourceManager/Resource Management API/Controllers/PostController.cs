using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RMAPI.Models.Entity;
using RMAPI.Models.Post;
using System.Security.Claims;

namespace RMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly ResourceContext _context;
        
        

        public PostController(ResourceContext context)
        {
            _context = context;
        }
        [HttpGet]

        public IActionResult getListPost()
        {
            var items = _context.Posts.ToList();
            return StatusCode(StatusCodes.Status200OK, items);
        }
        [HttpGet("{id}")]
        public IActionResult GetPostById(Guid id)
        {
            var post = _context.Posts.FirstOrDefault(p => p.Id == id);
            if (post == null)
            {
                return StatusCode(StatusCodes.Status404NotFound, "Post not found.");
            }
            return StatusCode(StatusCodes.Status200OK, post);
        }

        [HttpPost]
        public IActionResult addPost(InputPost input)
        {
            if (!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            
            var post = new Post
            {
                Id = Guid.NewGuid(),
                createdBy = input.createdBy,
                content = input.content,
                created = input.created
            };

            _context.Posts.Add(post);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public IActionResult updatePost(Post post)
        {
            var item = _context.Posts.Find(post.Id);
            if (item == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);

            }
            item.content = post.content;
            item.created = post.created;

            _context.Posts.Update(item);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);
        }
        [HttpDelete("{id}")]
        public IActionResult deleteBanner(Guid id)
        {
            var item = _context.Posts.FirstOrDefault(b => b.Id == id);
            if (item == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);

            }
            _context.Posts.Remove(item);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
