public interface IUserService
{
  UserDto Authenticate(string email, string password);
  Task<UserDto> RegisterAsync(SignUpDto registerModel);
  string GenerateJwtToken(string email);
}