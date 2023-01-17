using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SSRentApi.Data;

namespace SSRentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        protected readonly ApiDbContext _dbContext;
        public CategoriesController(IConfiguration configuration)
        {
            _dbContext = new ApiDbContext(configuration);
        }

        [HttpGet]
        //[Authorize]
        public IActionResult Get()
        {
            return Ok(_dbContext.Categories);
        }
    }
}