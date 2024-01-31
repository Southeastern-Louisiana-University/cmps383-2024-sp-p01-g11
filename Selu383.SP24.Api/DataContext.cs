using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Models;
using System;
using System.Collections.Generic;

public class DataContext : DbContext
{
    public virtual DbSet<Hotel> Hotel { get; set; }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Hotel>().Property(h => h.Name).HasMaxLength(150);
        modelBuilder.Entity<Hotel>().HasData(
            new Hotel { Id = 1, Name = "Hotel A", Address = "123 Main St" },
            new Hotel { Id = 2, Name = "Hotel B", Address = "456 Oak St" },
            new Hotel { Id = 3, Name = "Hotel C", Address = "789 Pine St" }
        );
    }
}
