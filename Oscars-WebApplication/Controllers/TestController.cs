using Microsoft.AspNetCore.Mvc;

namespace Oscars_WebApplication.Controllers;

public class TestController : Controller
{
    [HttpGet("api/user")]
    public IActionResult Get()
    {
        return Ok(new {name = "Oscar"});
    }
}