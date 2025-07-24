using MiniFPAService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MiniFPAService.Repositories
{
    public interface IFinancialRecordRepository
    {
        Task AddRecordsAsync(IEnumerable<FinancialRecord> records);
        Task<List<FinancialRecord>> GetAllAsync();
        Task<List<FinancialRecord>> GetByTypeAsync(string type);
    }
}

