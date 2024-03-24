using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BSR.Models;

public class HomeContext : IdentityDbContext<ApplicationUser> 
{
    public HomeContext(DbContextOptions<HomeContext> options) : base(options) { }

    public DbSet<Home> Homes { get; set; }
    public DbSet<ApplicationUser> AspNetUsers { get; set; }
}