using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Features.Hotel;
using System.Security.Cryptography.X509Certificates;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<Hotel> Hotel { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Hotel>()
            .Property(x => x.Name).HasMaxLength(120);

        modelBuilder.Entity<Hotel>()
            .HasData(
            new Hotel { Id = 1, Name = "Hotel A", Address = "123 Main St" },
            new Hotel { Id = 2, Name = "Hotel B", Address = "456 Oak St" }
            );
    }
}
