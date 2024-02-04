using System.ComponentModel.DataAnnotations;

public class SignUpDto
{
  [Required]
  [EmailAddress]
  public string Email { get; set; }

  [Required]
  [DataType(DataType.Password)]
  public string Password { get; set; }

  [Required]
  public string Name { get; set; }

  [Required]
  public string Surname { get; set; }
}