using Microsoft.EntityFrameworkCore;
using StatusManagement.API.Entities;

namespace StatusManagement.API.Migrations
{
    public class StatusManagementDbContext : DbContext
    {
        public DbSet<Manager>Manager    { get; set; }
        public DbSet<Marshall> Marshall { get; set; }
        public DbSet<Project> Projects  { get; set; }
        public DbSet<Status> Status     { get; set; }
    }
}
