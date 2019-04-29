using Microsoft.AspNetCore.Mvc;

namespace Syski.API.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {

        [HttpGet("/")]
        public ActionResult Get()
        {
            return Ok();
        }

    }
}
