using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatusManagement.API.Dtos;
using StatusManagement.API.Services;

namespace StatusManagement.API.Controllers
{
    [Route("api/managers")]
    public class ManagersController : Controller
    {
        private readonly IStatusManagementRepository _StatusManagementRepository;

        public ManagersController(IStatusManagementRepository StatusManagementRepository)
        {
            _StatusManagementRepository = StatusManagementRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetManagers()
        {
            var managersFromRepo = await _StatusManagementRepository.GetManagers();

            var managers = Mapper.Map<IEnumerable<Manager>>(managersFromRepo);

            return Ok(managers);
        }
    }
}
