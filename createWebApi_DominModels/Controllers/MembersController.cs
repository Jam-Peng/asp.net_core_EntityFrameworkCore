using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace createWebApi_DominModels.Controllers
{
    // https://localhost:Port_Number/api/members
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllMembers()
        {
            string[] members = new string[] { "大白", "小黑", "中紅" };

            return Ok(members);
        }

    }
}
