using Microsoft.EntityFrameworkCore;
using PointsApp.DomainModel.Entities;

namespace PointsApp.DataAccess.EF;

public class AppDbContext : DbContext
{
    public DbSet<Point> Points { get; set; }
    
    public DbSet<Comment> Comments { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Point>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.X).IsRequired();
            entity.Property(p => p.Y).IsRequired();
            entity.Property(p => p.Radius).IsRequired();

            entity.HasMany(p => p.Comments)
                .WithOne(c => c.Point)
                .HasForeignKey(c => c.PointId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(c => c.Id);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseLazyLoadingProxies();
    }
}