namespace Arbiter.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class Review
{
    public int Id { get; set; }

    public int PaperId { get; set; } 

    [Required]
    [StringLength(100)]
    [Display(Name = "Reviewer Name")]
    public string ReviewerName { get; set; } = string.Empty;

    [Range(1, 5, ErrorMessage = "Score must be between 1 nad 5.")]
    [Display(Name = "Score")]
    public int Score { get; set; }

    [Display(Name = "Comment")]
    public string Comments { get; set; } = string.Empty; 

    [DataType(DataType.Date)]
    [Display(Name = "Review Date")]
    public DateTime ReviewDate { get; set; } = DateTime.Today;

    public Paper? Paper { get; set; }

}

