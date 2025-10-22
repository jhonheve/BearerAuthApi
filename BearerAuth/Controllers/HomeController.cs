using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BearerAuth.Controllers;

[Route("auth-api/[controller]")]
[ApiController]
public class HomeController(IWebHostEnvironment environment) : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(environment.EnvironmentName);
    }
}   