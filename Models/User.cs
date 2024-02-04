public class User
{
  public User()
  {
    CreatedAt = DateTime.UtcNow;
    Notes = new List<Note>();
  }

  public int Id { get; set; }
  public string Name { get; set; }
  public string Surname { get; set; }
  public string Email { get; set; }
  public string PasswordHash { get; set; }

  public DateTime CreatedAt { get; private set; }
  public DateTime? UpdatedAt { get; set; }

  public virtual ICollection<Note> Notes { get; set; }
}
