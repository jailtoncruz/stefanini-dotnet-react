using Microsoft.EntityFrameworkCore;
using StefaniniDotNetReactChallenge.Domain.Entities;

namespace StefaniniDotNetReactChallenge.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Person> People { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Name).IsRequired().HasMaxLength(150);
            entity.Property(p => p.Gender);
            entity.Property(p => p.Email);
            entity.Property(p => p.BirthDay);
            entity.Property(p => p.Nationality);
            entity.Property(p => p.PlaceOfBirth);
            entity.Property(p => p.CPF).IsRequired();

            entity.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.UpdatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
        });

        base.OnModelCreating(modelBuilder);
    }
}
