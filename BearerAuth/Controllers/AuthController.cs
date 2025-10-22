using Microsoft.AspNetCore.Mvc;
using IdentityService.Application.DTOs;
using IdentityService.Application.Services;

namespace BearerAuth.Controllers;

[ApiController]
[Route("auth-api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
 {
        _authService = authService;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto request)
    {
        if (!ModelState.IsValid)
        {
      return BadRequest(ModelState);
        }

    var result = await _authService.SignUpAsync(request);

        if (result.IsSuccess)
 {
return CreatedAtAction(nameof(SignUp), new { id = result.Data!.Id }, result.Data);
}

        return BadRequest(new { message = result.Message, errors = result.Errors });
    }
}