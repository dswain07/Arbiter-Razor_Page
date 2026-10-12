using Arbiter.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Arbiter.Data.Context;

public class ArbiterDbContext : DbContext
{
    public ArbiterDbContext(DbContextOptions<ArbiterDbContext> options)
        : base(options)
    {
    }

    public DbSet<Paper> Papers => Set<Paper>();

    public DbSet<Author> Authors => Set<Author>();

    public DbSet<Review> Reviews => Set<Review>();

    public DbSet<Track> Tracks => Set<Track>();

    public DbSet<PaperAuthor> PaperAuthors => Set<PaperAuthor>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Paper>()
            .HasOne(p => p.Track)
            .WithMany(t => t.Papers)
            .HasForeignKey(p => p.TrackId);

        modelBuilder.Entity<Review>()
            .HasOne(r => r.Paper)
            .WithMany(p => p.Reviews)
            .HasForeignKey(r => r.PaperId);

        modelBuilder.Entity<PaperAuthor>()
            .HasOne(pa => pa.Paper)
            .WithMany(p => p.PaperAuthors)
            .HasForeignKey(pa => pa.PaperId);

        modelBuilder.Entity<PaperAuthor>()
            .HasOne(pa => pa.Author)
            .WithMany(a => a.PaperAuthors)
            .HasForeignKey(pa => pa.AuthorId);
    }
}