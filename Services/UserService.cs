using Dairy.Data;

public class UserService(ApplicationDbContext context, IConfiguration configuration) : IUserService
{
  private readonly ApplicationDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
  private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

  public UserDto Authenticate(string email, string password)
  {
    var user = _context.Users.FirstOrDefault(u => u.Email == email);
    if (user == null)
    {
      return null;
    }

    bool isVerified = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
    if (isVerified)
    {
      return new UserDto
      {
        Email = user.Email,
        Id = user.Id,
        Name = user.Name,
        Surname = user.Surname
      };
    }

    return null;
  }

  public async Task<UserDto> RegisterAsync(SignUpDto model)
  {
    if (_context.Users.Any(u => u.Email == model.Email))
    {
      return null;
    }

    var user = new User
    {
      Email = model.Email,
      PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password),
      Name = model.Name,
      Surname = model.Surname,
      UpdatedAt = DateTime.UtcNow
    };

    _context.Users.Add(user);
    await _context.SaveChangesAsync();

    return new UserDto
    {
      Id = user.Id,
      Email = user.Email,
      Name = user.Name,
      Surname = user.Surname,
    };
  }

  public string GenerateJwtToken(string email)
  {
    return JwtTokenHelper.GenerateToken(email, _configuration);
  }
}