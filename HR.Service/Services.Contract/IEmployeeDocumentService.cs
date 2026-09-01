using HR.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services.Contract
{
    public interface IEmployeeDocumentService
    {
        Task<EmployeeDocument?> GetDocumentByIdAsync(long id);

        Task<List<EmployeeDocument>> GetEmployeeDocumentsAsync(int employeeId);

        Task<string> AddAsync(EmployeeDocument document);

        Task<string> UpdateAsync(EmployeeDocument document);

        Task<string> DeleteAsync(EmployeeDocument document);

        Task<bool> IsDocumentExistAsync(long id);

        Task<bool> IsEmployeeDocumentExistAsync(int employeeId,
            string documentType);
    }
}
