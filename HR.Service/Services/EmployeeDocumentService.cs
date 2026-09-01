using HR.Data.Entities;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.Services.Contract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Service.Services
{
    public class EmployeeDocumentService: IEmployeeDocumentService
    {
        private readonly IEmployeeDocumentRepo documentRepo;

        public EmployeeDocumentService(
            IEmployeeDocumentRepo documentRepo)
        {
            this.documentRepo = documentRepo;
        }

        public async Task<EmployeeDocument?> GetDocumentByIdAsync(long id)
        {
            return await documentRepo
                .GetTableNoTracking()
                .Include(e=>e.Employee)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<EmployeeDocument>> GetEmployeeDocumentsAsync(int employeeId)
        {
            return await documentRepo
                .GetTableNoTracking()
                .Where(x => x.EmployeeId == employeeId)
                .Include(e => e.Employee)
                .ToListAsync();
        }

        public async Task<string> AddAsync(EmployeeDocument document)
        {
            await documentRepo.AddAsync(document);
            await documentRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> UpdateAsync(EmployeeDocument document)
        {
            documentRepo.UpdateAsync(document);
            await documentRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<string> DeleteAsync(EmployeeDocument document)
        {
            documentRepo.DeleteAsync(document);
            await documentRepo.SaveChangesAsync();
            return "Success";
        }

        public async Task<bool> IsDocumentExistAsync(long id)
        {
            return await documentRepo
                .GetTableNoTracking()
                .AnyAsync(x => x.Id == id);
        }

        public async Task<bool> IsEmployeeDocumentExistAsync(
            int employeeId,
            string documentType)
        {
            return await documentRepo
                .GetTableNoTracking()
                .AnyAsync(x =>
                    x.EmployeeId == employeeId &&
                    x.DocumentType == documentType);
        }
    }
}
