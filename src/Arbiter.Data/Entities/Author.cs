namespace Arbiter.Data.Entities;
using System.ComponentModel.DataAnnotations;

public class Author
{
    public int Id { get; set; }

    [Display(Name = "Full Name")]
    public string FullName { get; set; } = string.Empty;

    [Display(Name = "Email")]
    public string Email { get; set; } = string.Empty;

    [Display(Name = "Affiliation")]
    public string Affiliation { get; set; } = string.Empty;

    [Display(Name = "ORCID ID")]
    public string OrcidId { get; set; } = string.Empty;

    public ICollection<PaperAuthor> PaperAuthors { get; set; } = new List<PaperAuthor>();

}