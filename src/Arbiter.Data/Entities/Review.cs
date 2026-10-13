namespace Arbiter.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class Review
{
    public int Id { get; set; }

    public int PaperId { get; set; } 

    [Display(Name = "Reviewer Name")]
    public string ReviewerName { get; set; } = string.Empty;

    [Display(Name = "Score")]
    public int Score { get; set; }

    [Display(Name = "Comment")]
    public string Comments { get; set; } = string.Empty; 

    [Display(Name = "Review Date")]
    public DateTime ReviewDate { get; set; } 

    public Paper? Paper { get; set; }

}

