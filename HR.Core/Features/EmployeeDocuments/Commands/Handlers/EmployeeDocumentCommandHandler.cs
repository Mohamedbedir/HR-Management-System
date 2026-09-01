using HR.Core.Bases;
using HR.Core.Features.EmployeeDocuments.Commands.Models;
using HR.Data.Entities;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;

namespace HR.Core.Features.EmployeeDocuments.Commands.Handlers
{
    public class EmployeeDocumentCommandHandler: ResponseHandler,
          IRequestHandler<AddEmployeeDocumentCommand,Response<string>>,
          IRequestHandler<UpdateEmployeeDocumentCommand,Response<string>>,
            IRequestHandler<DeleteEmployeeDocumentCommand, Response<string>>
    {
        private readonly IEmployeeDocumentService documentService;
        private readonly IFileService fileService;

        public EmployeeDocumentCommandHandler(
            IEmployeeDocumentService documentService,
            IFileService fileService,
            IStringLocalizer<SharedResources> localizer)
            : base(localizer)
        {
            this.documentService = documentService;
            this.fileService = fileService;
        }

        public async Task<Response<string>> Handle(AddEmployeeDocumentCommand request,CancellationToken cancellationToken)
        {
            string? filePath = null;

            try
            {
                // 1. Upload File
                filePath = await fileService.UploadFileAsync(request.File,"EmployeeDocuments");

                // 2. Create Entity
                var document = new EmployeeDocument
                {
                    EmployeeId = request.EmployeeId,
                    DocumentType = request.DocumentType,
                    FileName = request.File.FileName,
                    FilePath = filePath,
                    UploadedAt = DateTime.UtcNow,
                    ExpiryDate = request.ExpiryDate
                };

                // 3. Save DB
                await documentService.AddAsync(document);

                return Created<string>( document.Id.ToString());
            }
            catch
            {
                // لو DB فشلت بعد رفع الملف
                // نحذف الملف عشان مايبقاش orphan file
                if (!string.IsNullOrEmpty(filePath))
                {
                    await fileService.DeleteFileAsync(filePath);
                }

                throw;
            }
        }

        public async Task<Response<string>> Handle(DeleteEmployeeDocumentCommand request,CancellationToken cancellationToken)
        {
            var document = await documentService.GetDocumentByIdAsync(request.Id);

            if (document == null)
                return NotFound<string>("Document not found");

            var filePath = document.FilePath;

            try
            {
                await documentService.DeleteAsync(document);

                if (!string.IsNullOrEmpty(filePath))
                {
                    await fileService.DeleteFileAsync(filePath);
                }

                return Success<string>(entity:null,Message:"Document deleted successfully");
            }
            catch
            {
                throw;
            }
        }

        public async Task<Response<string>> Handle(UpdateEmployeeDocumentCommand request,
        CancellationToken cancellationToken)
        {
            var document = await documentService
                .GetDocumentByIdAsync(request.Id);

            if (document == null)
                return NotFound<string>("Document not found");

            var oldFilePath = document.FilePath;

            // Update normal data
            document.DocumentType = request.DocumentType;
            document.ExpiryDate = request.ExpiryDate;

            // If user uploaded a new file
            if (request.File != null)
            {
                var newFilePath = await fileService.UploadFileAsync(
                    request.File,
                    "EmployeeDocuments");

                document.FileName = request.File.FileName;
                document.FilePath = newFilePath;
            }

            try
            {
                await documentService.UpdateAsync(document);

                // Delete old file only if a new file was uploaded
                if (request.File != null && !string.IsNullOrEmpty(oldFilePath))
                {
                    await fileService.DeleteFileAsync(oldFilePath);
                }

                return Success<string>(entity:null,Message:"Document updated successfully");
            }
            catch
            {
                // If DB update failed after uploading new file,
                // delete the newly uploaded file to avoid orphan file
                if (request.File != null &&
                    document.FilePath != oldFilePath)
                {
                    await fileService.DeleteFileAsync(document.FilePath);
                }

                throw;
            }
        }
    }

}