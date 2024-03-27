using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BSR.Models;

public class HomeContext : IdentityDbContext<ApplicationUser>
{
    public HomeContext(DbContextOptions<HomeContext> options) : base(options) { }

    public DbSet<Home> Homes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "manager",
                    Name = "Manager"
                },
                new IdentityRole
                {
                    Id = "sales",
                    Name = "Sales"
                }
            );
    }
}



