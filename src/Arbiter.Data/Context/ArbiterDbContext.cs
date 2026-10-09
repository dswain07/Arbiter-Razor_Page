using Arbiter.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Data.Context;

public class ArbiterDbContext : DbContext
{
    public ArbiterDbContext(DbContextOptions<ArbiterDbContext> options) : base(options)
    {
        
    }

    public DbSet<Paper> Papers => Set<Paper>();

    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<Track> Tracks => Set<Track>();

    public DbSet<PaperAuthor> PaperAuthors => Set<PaperAuthor>();
}