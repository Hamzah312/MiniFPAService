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
        private readonly IFinancialRecordRepository _repository;

        public FinancialRecordsController(IExcelService excelService, IFinancialRecordRepository repository)
        {
            _excelService = excelService;
            _repository = repository;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel()
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
                return BadRequest("No file uploaded.");

            using var stream = file.OpenReadStream();
            var dtos = await _excelService.ParseExcelAsync(stream);
            var records = dtos.Select(dto => new FinancialRecord
            {
                Type = dto.Type,
                Account = dto.Account,
                Department = dto.Department,
                Year = dto.Year,
                Month = dto.Month,
                Amount = dto.Amount
            }).ToList();
            await _repository.AddRecordsAsync(records);
            return Ok(new { Count = records.Count });
        }

        [HttpGet]
        public async Task<ActionResult<List<FinancialRecord>>> GetAll()
        {
            var records = await _repository.GetAllAsync();
            return Ok(records);
        }

        [HttpGet("type/{type}")]
        public async Task<ActionResult<List<FinancialRecord>>> GetByType(string type)
        {
            var records = await _repository.GetByTypeAsync(type);
            return Ok(records);
        }
    }
}

