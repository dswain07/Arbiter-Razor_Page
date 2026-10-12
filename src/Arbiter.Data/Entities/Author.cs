namespace Arbiter.Data.Entities;

public class Author
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Affiliation { get; set; } = string.Empty; 

    public string OrcidId { get; set; } = string.Empty;

    public ICollection<PaperAuthor> PaperAuthors { get; set; } = new List<PaperAuthor>();

}