public class Note
{
  public int NoteId { get; set; }
  public string Content { get; set; }
  public DateTime CreatedDate { get; set; }
  public int UserId { get; set; }
  public virtual User User { get; set; }
}