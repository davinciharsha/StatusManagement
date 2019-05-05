using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using StatusManagement.API.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace StatusManagement.API.Services
{
    public class StatusManagementContext : DbContext
    {
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Marshall> Marshalls { get; set; }
        public DbSet<Manager> Managers { get; set; }

        private readonly IUserInfoService _userInfoService;

        //public StatusManagementContext(DbContextOptions<StatusManagementContext> options )
        //  : base(options)
        //{ 

        //}
        public StatusManagementContext(DbContextOptions<StatusManagementContext> options,
            IUserInfoService userInfoService)
           : base(options)
        {
            // userInfoService is a required argument
            _userInfoService = userInfoService ?? throw new ArgumentNullException(nameof(userInfoService));
        }
        
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // get added or updated entries
            var addedOrUpdatedEntries = ChangeTracker.Entries()
                    .Where(x => (x.State == EntityState.Added || x.State == EntityState.Modified));

            // fill out the audit fields
            foreach (var entry in addedOrUpdatedEntries)
            {
                var entity = entry.Entity as AuditableEntity;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedBy = _userInfoService.UserId;
                    entity.CreatedOn = DateTime.UtcNow;
                }

                entity.UpdatedBy = _userInfoService.UserId;
                entity.UpdatedOn = DateTime.UtcNow;
            }

            return base.SaveChangesAsync(cancellationToken);
        }        
    }
}
