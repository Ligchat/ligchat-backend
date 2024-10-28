using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using tests_.src.Application.Services;
using System.Collections.Generic;

namespace YourNamespace.Web.Controllers
{
    [Route("api/businessHour")]
    [ApiController]
    public class BusinessDayController: ControllerBase
    {
        private readonly BusinessDayService _businessDayService;

        public BusinessDayController(BusinessDayService businessDayService)
        {
            _businessDayService = businessDayService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdateBusinessDay([FromBody] BusinessHoursDto businessDayDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            var result = await _businessDayService.CreateOrUpdateBusinessDay(businessDayDto);

            return Ok(result); 
        }

        [HttpGet("{sectorId}")]
        public async Task<IActionResult> GetBusinessDaysByUser(int sectorId)
        {
            var businessDays = await _businessDayService.GetBusinessDaysByUser(sectorId);

            if (businessDays == null || businessDays.Count == 0)
            {
                return NotFound("No business days found for the specified user.");
            }

            return Ok(businessDays);
        }
    }
}
