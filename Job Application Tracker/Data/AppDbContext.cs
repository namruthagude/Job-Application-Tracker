using Job_Application_Tracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Job_Application_Tracker.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<JobApplication> JobApplications {  get; set; }
        public DbSet<User> Users { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        
    }
}
