using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StatusManagement.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace StatusManagement.API.Services
{
    public class StatusManagementRepository : IStatusManagementRepository
    {
        private StatusManagementContext _context;

        public StatusManagementRepository(StatusManagementContext context)
        {
            _context = context;
        }

        public async Task<bool> StatusExists(Guid statusId)
        {
            return await _context.Statuses.AnyAsync(t => t.StatusId == statusId);
        }

        public async Task<IEnumerable<Status>> GetStatuses(bool includeProjects = false)
        {
            if (includeProjects)
            {
                return await _context.Statuses.Include(t => t.Marshall).Include(t => t.Projects).ToListAsync();
            }
            else
            {
                return await _context.Statuses.Include(t => t.Marshall).ToListAsync();
            }
        }

        public async Task<IEnumerable<Status>> GetStatusesForManager(Guid managerId, bool includeProjects = false)
        {
            if (includeProjects)
            {
                return await _context.Statuses.Where(t => t.ManagerId == managerId)
                    .Include(t => t.Marshall).Include(t => t.Projects).ToListAsync();
            }
            else
            {
                return await _context.Statuses.Where(t => t.ManagerId == managerId)
                    .Include(t => t.Marshall).ToListAsync();
            }
        }

        public async Task<Status> GetStatus(Guid statusId, bool includeProjects = false)
        {
            if (includeProjects)
            {
                return await _context.Statuses.Include(t => t.Marshall).Include(t => t.Projects)
                    .Where(t => t.StatusId == statusId).FirstOrDefaultAsync();
            }
            else
            {
                return await _context.Statuses.Include(t => t.Marshall)
                    .Where(t => t.StatusId == statusId).FirstOrDefaultAsync();
            }
        }

        public async Task<bool> IsStatusManager(Guid statusId, Guid managerId)
        {
            return await _context.Statuses.AnyAsync(t => t.StatusId == statusId && t.ManagerId == managerId);
        }

        public async Task AddStatus(Status status)
        {
            await _context.Statuses.AddAsync(status);
        }

#pragma warning disable 1998
        // disable async warning - no code 
        public async Task UpdateStatus(Status status)
        {
            // no code in this implementation
        }
#pragma warning restore 1998

#pragma warning disable 1998
        // disable async warning - no RemoveAsync available
        public async Task DeleteStatus(Status status)
        {
            _context.Statuses.Remove(status);
        }
#pragma warning restore 1998

        public async Task<IEnumerable<Project>> GetProjects(Guid statusId)
        {
            return await _context.Projects.Where(s => s.StatusId == statusId).ToListAsync();
        }

        public async Task<IEnumerable<Project>> GetProjects(Guid statusId, IEnumerable<Guid> projectIds)
        {
           return await _context.Projects
                .Where(s => s.StatusId == statusId && projectIds.Contains(s.ProjectId)).ToListAsync();
        }


        public async Task AddProject(Guid statusId, Project project)
        {
            var status = await GetStatus(statusId);
            if (status == null)
            {
                // throw an exception - this is a race condition
                // that's mostly caught by checking if the status exists
                // right before calling into this method - if that method is not
                // called the condition can happen, otherwise the status
                // will already be loaded on the context
                throw new Exception($"Cannot add project to status with id {statusId}: status not found.");
            }
            status.Projects.Add(project);
        }

        public async Task<IEnumerable<Marshall>> GetMarshalls()
        {
            return await _context.Marshalls.ToListAsync();
        }

        public async Task<IEnumerable<Manager>> GetManagers()
        {
            return await _context.Managers.ToListAsync();
        }

        public async Task<bool> SaveAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }           
    }
}
