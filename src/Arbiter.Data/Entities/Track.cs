
using System.ComponentModel.DataAnnotations;

namespace Arbiter.Data.Entities;

public class Track
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Track name is required.")]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = string.Empty;

    [Range(1, 1000, ErrorMessage = "Quota must be between 1 and 1000.")]
    [Display(Name = "Maximum Quota")]
    public int MaxQuota { get; set; }

    public ICollection<Paper> Papers { get; set; }
        = new List<Paper>();
}
