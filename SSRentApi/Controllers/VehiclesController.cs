using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SSRentApi.Data;
using SSRentApi.Models;
using System.Security.Claims;

namespace SSRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController
    : ControllerBase
    {
        private readonly ApiDbContext _dbContext;

        public VehiclesController(IConfiguration config)
        {
            _dbContext = new ApiDbContext(config);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Post([FromBody] Vehicle vehicle)
        {
            if (vehicle == null)
            {
                return NoContent();
            }
            else
            {
                var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                var user = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user == null)
                    return NotFound();

                vehicle.IsTrending = false;
                vehicle.UserId = user.Id;
                _dbContext.Vehicles.Add(vehicle);
                _dbContext.SaveChanges();
                
                return StatusCode(StatusCodes.Status201Created);
            }
        }

        [HttpGet("List")]
        [Authorize]
        public IActionResult GetByCategory(int categoryId)
        {
            var propertiesResult = _dbContext.Vehicles.Where(c => c.CategoryId == categoryId);
            if (propertiesResult == null)
            {
                return NotFound();
            }
            return Ok(propertiesResult);
        }


        [HttpGet("Detail")]
        [Authorize]
        public IActionResult GetDetail(int id)
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var user = _dbContext.Users.FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
                return NotFound();

            var vehicle = _dbContext.Vehicles.Find(id);

            if (vehicle != null)
            {
                var result = _dbContext
                    .Vehicles.Include(v => v.Port)
                    .Include(v => v.Bookmarks).Where(v => v.Id == id)
                    .Select(v => new
                    {
                        v.Id,
                        v.Name,
                        v.Detail,
                        v.Port,
                        v.Price,
                        v.ImageUrl,
                        v.User.Email,
                        Bookmark = v.Bookmarks.FirstOrDefault(u => u.UserId == user.Id)
                    }).FirstOrDefault();

                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("Trendings")]
        [Authorize]
        public IActionResult GetTrendings()
        {
            var result = _dbContext.Vehicles.Where(v => v.IsTrending == true);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("Search")]
        [Authorize]
        public IActionResult Search(string portName)
        {
            var result = _dbContext
                        .Vehicles
                        .Include(v => v.Port)
                        .Where(v => v.Port.Name.Contains(portName));
                        
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
    }
}
