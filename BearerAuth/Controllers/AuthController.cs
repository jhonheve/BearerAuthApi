using Microsoft.AspNetCore.Mvc;
using IdentityService.Application.DTOs;
using IdentityService.Application.Services;

namespace BearerAuth.Controllers;

[ApiController]
[Route("auth-api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await authService.SignUpAsync(request);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return CreatedAtAction(nameof(SignUp), new { id = result.Data!.Id }, result.Data);
    }
}