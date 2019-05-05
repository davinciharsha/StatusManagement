using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatusManagement.API.Entities;


namespace StatusManagement.API.Services
{
    public interface IStatusManagementRepository
    {
        Task AddStatus(Status status);
        Task DeleteStatus(Status status);
        Task<Status> GetStatus(Guid statusId, bool includeProjects = false);
        Task<IEnumerable<Status>> GetStatuses(bool includeProjects = false);
        Task<IEnumerable<Status>> GetStatusesForManager(Guid managerId, bool includeProjects = false);
        Task<bool> IsStatusManager(Guid statusId, Guid managerId);
        Task<bool> SaveAsync();
        Task<bool> StatusExists(Guid statusId);
        Task UpdateStatus(Status status);
        Task<IEnumerable<Project>> GetProjects(Guid statusId);
        Task<IEnumerable<Project>> GetProjects(Guid statusId, IEnumerable<Guid> projectIds);
        Task AddProject(Guid statusId, Project project);
        Task<IEnumerable<Marshall>> GetMarshalls();
        Task<IEnumerable<Manager>> GetManagers();
    }
}