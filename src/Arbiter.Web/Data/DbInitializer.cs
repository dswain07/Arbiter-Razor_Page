using Arbiter.Data.Context;
using Arbiter.Data.Entities;

namespace Arbiter.Web.Data;

public static class DbInitializer
{
    public static void Initialize(ArbiterDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Tracks.Any())
            return;

        var tracks = new[]
        {
            new Track
            {
                Name = "Artificial Intelligence",
                Description = "AI and Machine Learning",
                MaxQuota = 100
            },
            new Track
            {
                Name = "Systems",
                Description = "Operating Systems",
                MaxQuota = 80
            },
            new Track
            {
                Name = "Cybersecurity",
                Description = "Security and Privacy",
                MaxQuota = 75
            },
            new Track
            {
                Name = "Data Science",
                Description = "Analytics and Data Engineering",
                MaxQuota = 90
            },
            new Track
            {
                Name = "Software Engineering",
                Description = "Software Design",
                MaxQuota = 120
            }
        };

        context.Tracks.AddRange(tracks);
        context.SaveChanges();
    }
}