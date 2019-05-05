using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatusManagement.API.Dtos;
using StatusManagement.API.Services;

namespace StatusManagement.API.Controllers
{
    [Route("api/statuses")]
    public class StatusesController : Controller
    {
        private readonly IStatusManagementRepository _statusManagementRepository;

        public StatusesController(IStatusManagementRepository statusManagementRepository)
        {
            _statusManagementRepository = statusManagementRepository;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetStatuses()
        {
            var statusesFromRepo = await _statusManagementRepository.GetStatuses();

            var statuses = Mapper.Map<IEnumerable<Status>>(statusesFromRepo);
            return Ok(statuses);
        }


        [HttpGet("{statusId}", Name = "Getstatus")]
        public async Task<IActionResult> GetStatus(Guid statusId)
        {
            var statusFromRepo = await _statusManagementRepository.GetStatus(statusId);

            if (statusFromRepo == null)
            {
                return BadRequest();
            }

            var status = Mapper.Map<Status>(statusFromRepo);

            return Ok(status);
        }        
    }
}
