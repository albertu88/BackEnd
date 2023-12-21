using BackEnd.Infrastructure.Models.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        [HttpGet, Authorize(Roles = nameof(Role.Manager))]
        public IEnumerable<string> Get()
        {
            return new string[] { "John Doe", "Jane Doe" };
        }
    }
}
