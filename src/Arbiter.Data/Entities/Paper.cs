
using System.ComponentModel.DataAnnotations;

namespace Arbiter.Data.Entities;

public class Paper
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Paper title is required.")]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "Abstract is required.")]
    public string Abstract { get; set; } = string.Empty;

    public string? Keywords { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Submission Date")]
    public DateTime SubmissionDate { get; set; } = DateTime.Today;

    [DataType(DataType.Date)]
    [Display(Name = "Last Modified Date")]
    public DateTime? LastModifiedDate { get; set; }

    [Required]
    public string Status { get; set; } = string.Empty;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Please select a track.")]
    [Display(Name = "Track")]
    public int TrackId { get; set; }

    public Track? Track { get; set; }

    public ICollection<Review> Reviews { get; set; }
        = new List<Review>();

    public ICollection<PaperAuthor> PaperAuthors { get; set; }
        = new List<PaperAuthor>();
}
