using Lace.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lace.Application.DbContexts;

public class LaceDbContext: DbContext
{
    public DbSet<Category> Categories { get; set; }

    public DbSet<DictionaryElement> DictionaryElements { get; set; }

    public DbSet<Profile> Profiles { get; set; }

    public DbSet<ProfileAttribute> ProfileAttributes { get; set; }

    public DbSet<User> Users { get; set; }
    
    public LaceDbContext(DbContextOptions options) : base(options)
    { }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("lace_schema");

        modelBuilder.Entity<User>()
            .HasOne(x => x.Profile)
            .WithOne(x => x.User)
            .HasForeignKey<Profile>(x => x.UserId);
        
        base.OnModelCreating(modelBuilder);
    }
}