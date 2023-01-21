using Microsoft.AspNetCore.Mvc;
using SSRentApi.Data;
using SSRentApi.Models;
using System.Security.Claims;

namespace SSRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarksController : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public BookmarksController(IConfiguration config)
        {
            _dbContext = new ApiDbContext(config);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Bookmark item)
        {
            item.Status = true;
            _dbContext.Bookmarks.Add(item);
            _dbContext.SaveChanges();
            return Ok("Bookmark eklendi");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
                return NotFound();

            var bookmarks = from b in _dbContext.Bookmarks.Where(b => b.UserId == user.Id)
                            join v in _dbContext.Vehicles on b.VehicleId equals v.Id
                            join p in _dbContext.Ports on v.PortId equals p.Id
                            where b.Status == true
                            select new
                            {
                                b.Id,
                                v.Name,
                                v.Price,
                                v.ImageUrl,
                                v.Port,
                                p.Location,
                                b.Status,
                                b.UserId,
                                b.VehicleId

                            };
            return Ok(bookmarks);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var bookmark = _dbContext.Bookmarks.FirstOrDefault(b => b.Id == id);
            if (bookmark == null)
            {
                return NotFound();
            }
            else
            {
                var email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == email);

                if (user == null)
                    return NotFound();

                if (bookmark.UserId == user.Id)
                {
                    _dbContext.Bookmarks.Remove(bookmark);
                    _dbContext.SaveChanges();
                    return Ok("Bookmark silindi");
                }

                return BadRequest();
            }
        }
    }
}
