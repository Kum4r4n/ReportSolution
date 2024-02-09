
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Interfaces.IServices;
using Payroll.Application.Models;
using System.Formats.Asn1;
using System.Globalization;

namespace Payroll.API.Controllers
{
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
                var reader = new StreamReader(formFile.OpenReadStream());
                await _allowanceService.ImportData(reader);
                return Ok(new ImportStatusModel(true, "CSV allowance data imported successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ImportStatusModel(false, "CSV allowance data import failure"));
            }
        }
    }
}
