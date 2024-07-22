using System.Reflection.Emit;
using api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
        
    } 
    
    public DbSet<Stock> Stocks {get; set;}
    public DbSet<Comment> Comments {get; set;}

    public DbSet<Portoflio> portoflios {get; set;}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Portoflio>(x => x.HasKey(p => new { p.AppUserId , p.StockId}));

        builder.Entity<Portoflio>().
        HasOne(u => u.appUser).WithMany(x => x.portoflios).HasForeignKey(p => p.AppUserId);

        builder.Entity<Portoflio>().
        HasOne(u => u.stock).WithMany(x => x.portoflios).HasForeignKey(p => p.StockId);
    
        builder.Entity<AppUser>()
            .HasMany(c => c.comment)
            .WithOne(a => a.appUser)
            .HasForeignKey(c => c.appUserId)
            .OnDelete(DeleteBehavior.Restrict);


        List<IdentityRole> Roles = new List<IdentityRole> {

         new IdentityRole {
            Name = "Admin",
            NormalizedName = "ADMIN"
            
         },

         new IdentityRole {
            Name = "User",
            NormalizedName = "USER"
            
         }
        };

        builder.Entity<IdentityRole>().HasData(Roles);
    }
}
