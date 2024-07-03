using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace TenisoveTurnaje.Models {
    public class ApplicationDbContext : IdentityDbContext<User> {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<Court> Courts { get; set; }


    }

}
   

