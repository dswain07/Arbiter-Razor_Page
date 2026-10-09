namespace Arbiter.Data.Entities;

public class Paper
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Abstract { get; set; } = string.Empty;

    public string? Keywords { get; set; }

    public DateTime SubmissionDate { get; set; } 

    public DateTime? LastModifiedDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public int TrackId { get; set; }

    // public Track Track { get; set; }
}