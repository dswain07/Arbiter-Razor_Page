namespace Arbiter.Data.Entities;

public class PaperAuthor
{
    public int Id { get; set; }

    public int PaperId { get; set; } 

    public int AuthorId { get; set; }

    public int AuthorOrder { get; set; }

    public bool IsCorrespondingAuthor { get; set; } 
}