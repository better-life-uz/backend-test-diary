using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("sign-up")]
public class SignUpController : ControllerBase
{
  private readonly IUserService _userService;

  public SignUpController(IUserService userService)
  {
    _userService = userService;
  }

  [HttpPost()]
  public async Task<IActionResult> Register([FromBody] SignUpDto model)
  {
    if (!ModelState.IsValid)
    {
      return BadRequest(ModelState);
    }

    var user = await _userService.RegisterAsync(model);

    if (user == null)
    {
      return BadRequest("User could not be registered.");
    }

    user.Token = _userService.GenerateJwtToken(user.Email);

    return Ok(user);
  }
}
