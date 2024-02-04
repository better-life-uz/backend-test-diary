
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("sign-in")]
public class SignInController : ControllerBase
{
  private readonly IUserService _userService;

  public SignInController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpPost()]
  public IActionResult SignIn([FromBody] SignInDto model)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var user = _userService.Authenticate(model.Email, model.Password);

    if (user == null)
    {
      return Unauthorized("Invalid credentials.");
    }

    user.Token = _userService.GenerateJwtToken(user.Email);

    return Ok(user);
  }
}