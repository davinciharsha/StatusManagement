using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatusManagement.API.Dtos;
using StatusManagement.API.Services;

namespace StatusManagement.API.Controllers
{
    [Route("api/statuses/{statusId}/projects")]
    public class ProjectsController : Controller
    {
        private readonly IStatusManagementRepository _statusManagementRepository;

        public ProjectsController(IStatusManagementRepository statusManagementRepository)
        {
            _statusManagementRepository = statusManagementRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjects(Guid statusId)
        {
            var statusFromRepo = await _statusManagementRepository.GetStatus(statusId, true);

            if (!(await _statusManagementRepository.StatusExists(statusId)))
            {
                return NotFound();
            }

            var projectsFromRepo = await _statusManagementRepository.GetProjects(statusId);

            var projects = Mapper.Map<IEnumerable<Project>>(projectsFromRepo);
            return Ok(projects);
        }
    }
}
