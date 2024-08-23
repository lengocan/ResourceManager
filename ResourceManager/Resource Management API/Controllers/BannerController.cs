using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RMAPI.Models.Banner;
using RMAPI.Models.Entity;

namespace RMAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannerController : ControllerBase
    {
        private readonly ResourceContext _context;

        public BannerController(ResourceContext context)
        {
            _context = context;
        }

        [HttpGet]

        public  IActionResult getListBanner()
        {
            var items =  _context.Banners.ToList();
            return StatusCode(StatusCodes.Status200OK, items);
        }

        [HttpPost]
        public IActionResult addBanner(InputBanner input)
        {
            if(!ModelState.IsValid)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var banner = new Banner
            {
                Id = Guid.NewGuid(),
                content = input.content,
                color = input.color,
                effect = input.effect,
                

            };
            _context.Banners.Add(banner);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public  IActionResult updateBanner( Banner banner)
        {
            var item = _context.Banners.Find(banner.Id);
            if (item == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);

            }
            item.content = banner.content;
            item.effect = banner.effect;
            item.isUse = banner.isUse;
            item.color = banner.color;
            _context.Banners.Update(item);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public IActionResult deleteBanner(Guid id)
        {
            var item = _context.Banners.FirstOrDefault(b => b.Id == id);    
            if (item == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);

            }
            _context.Banners.Remove(item);
            _context.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
