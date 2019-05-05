using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using StatusManagement.API.Dtos;
using StatusManagement.API.Services;

namespace StatusManagement.API.Controllers
{
    [Route("api/marshalls")]
    public class MarshallsController : Controller
    {
        private readonly IStatusManagementRepository _statusManagementRepository;

        public MarshallsController(IStatusManagementRepository statusManagementRepository)
        {
            _statusManagementRepository = statusManagementRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetMarshalls()
        {
            var marshallsFromRepo = await _statusManagementRepository.GetMarshalls();

            var marshalls = Mapper.Map<IEnumerable<Marshall>>(marshallsFromRepo);

            return Ok(marshalls);
        }
    }
}
