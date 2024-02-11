
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces.IServices;
using Payroll.Application.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace Payroll.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IAllowanceService _allowanceService;

        public PayrollController(IAllowanceService allowanceService)
        {
            _allowanceService = allowanceService;
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile formFile)
        {
            try
            {
                await _allowanceService.ImportDataAsync(formFile.OpenReadStream());
                return Ok(new ImportStatusModel(true, "CSV allowance data imported successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ImportStatusModel(false, "CSV allowance data import failure"));
            }
        }

        [HttpGet("GetData")]
        public async Task<IActionResult> GetDataAsync()
        {
            try
            {
                var data = await _allowanceService.GetDataAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Something went wrong, please try again later!");
            }
        }
    }
}
