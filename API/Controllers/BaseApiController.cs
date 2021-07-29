using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    public class BaseApiController : ControllerBase
    {


    }
}