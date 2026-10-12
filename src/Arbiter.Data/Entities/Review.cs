namespace Arbiter.Data.Entities;

public class Review
{
    public int Id { get; set; }

    public int PaperId { get; set; } 

    public string ReviewerName { get; set; } = string.Empty;

    public int Score { get; set; }

    public string Comments { get; set; } = string.Empty; 

    public DateTime ReviewDate { get; set; } 

    public Paper? Paper { get; set; }

}