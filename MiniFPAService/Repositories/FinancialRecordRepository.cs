using MiniFPAService.Data;
using MiniFPAService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniFPAService.Repositories
{
    public class FinancialRecordRepository : IFinancialRecordRepository
    {
        private readonly ApplicationDbContext _context;

        public FinancialRecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddRecordsAsync(IEnumerable<FinancialRecord> records)
        {
            await _context.FinancialRecords.AddRangeAsync(records);
            await _context.SaveChangesAsync();
        }

        public async Task<List<FinancialRecord>> GetAllAsync()
        {
            return await _context.FinancialRecords.ToListAsync();
        }

        public async Task<List<FinancialRecord>> GetByTypeAsync(string type)
        {
            return await _context.FinancialRecords
                .Where(r => r.Type == type)
                .ToListAsync();
        }
    }
}

