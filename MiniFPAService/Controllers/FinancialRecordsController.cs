using Microsoft.AspNetCore.Mvc;
using MiniFPAService.DTOs;
using MiniFPAService.Models;
using MiniFPAService.Repositories;
using MiniFPAService.Services;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MiniFPAService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialRecordsController : ControllerBase
    {
        private readonly IExcelService _excelService;
        private readonly IFinancialRecordService _service;

        public FinancialRecordsController(IExcelService excelService, IFinancialRecordService service)
        {
            _excelService = excelService;
            _service = service;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel(
            [FromQuery] string scenario = "Default",
            [FromQuery] string version = null,
            [FromQuery] string userName = "System")
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
                return BadRequest("No file uploaded.");

            // Set default version if not provided
            if (string.IsNullOrEmpty(version))
            {
                version = DateTime.UtcNow.ToString("yyyy-MM-dd-HHmmss");
            }

            using var stream = file.OpenReadStream();
            var dtos = await _excelService.ParseExcelAsync(stream);
            
            // Use enhanced service method for processing with scenario, version, and user tracking
            await _service.ProcessExcelUploadAsync(dtos, scenario, version, userName);
            
            return Ok(new { 
                Count = dtos.Count,
                Scenario = scenario,
                Version = version,
                UserName = userName,
                Message = "Excel data uploaded successfully with scenario, version tracking, and audit trail"
            });
        }

        [HttpGet]
        public async Task<ActionResult<List<FinancialRecord>>> GetAll()
        {
            var records = await _service.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("type/{type}")]
        public async Task<ActionResult<List<FinancialRecord>>> GetByType(string type)
        {
            var records = await _service.GetByTypeAsync(type);
            return Ok(records);
        }
    }
}

