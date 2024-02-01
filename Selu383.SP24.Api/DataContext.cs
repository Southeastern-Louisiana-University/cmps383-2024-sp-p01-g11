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
        
    }
}
