using Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Entities;

public class PersonDbContext : IdentityDbContext<ApplicationUser>
{
    public virtual DbSet<Person> People { get; set; }
    public virtual DbSet<Country> Countries { get; set; }

    public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasIndex(p => p.Email).IsUnique();
        modelBuilder.Entity<Person>().HasOne(p => p.Country).WithMany().HasForeignKey(p => p.CountryId);
        modelBuilder.Entity<Person>().Property(p => p.Email).HasMaxLength(100);
        modelBuilder.Entity<Person>().Property(p => p.Name).HasMaxLength(100);
        modelBuilder.Entity<Person>().Property(p => p.Surname).HasMaxLength(100);
        modelBuilder.Entity<Person>().Property(p => p.Address).HasMaxLength(100);

        modelBuilder.Entity<Country>().HasMany(c => c.People).WithOne(p => p.Country).HasForeignKey(p => p.CountryId);
        modelBuilder.Entity<Country>().Property(c => c.Name).HasMaxLength(100);

        base.OnModelCreating(modelBuilder);
    }
}
