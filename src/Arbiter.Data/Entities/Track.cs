namespace Arbiter.Data.Entities;

public class Track
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty; 

    public int MaxQuota { get; set; }

    public ICollection<Paper> Papers { get; set; } = new List<Paper>();

}