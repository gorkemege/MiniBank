using Microsoft.AspNetCore.Mvc;

namespace MiniBank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    
    public class TestController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Returning from TestController Get Method";
        }
    }
}