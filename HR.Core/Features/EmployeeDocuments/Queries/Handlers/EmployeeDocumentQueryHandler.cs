using AutoMapper;
using HR.Core.Bases;
using HR.Core.Features.EmployeeDocuments.Queries.Models;
using HR.Core.Features.EmployeeDocuments.Queries.Responses;
using HR.Core.Features.Employees.Queries.Models;
using HR.Core.Features.Employees.Queries.Responses;
using HR.Infrastructure.Repositories.Contract;
using HR.Service.Services;
using HR.Service.Services.Contract;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolProject.Core.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.Core.Features.EmployeeDocuments.Queries.Handlers
{
    public class EmployeeDocumentQueryHandler : ResponseHandler,
        IRequestHandler<GetDocumentsForEmployeeQuery, Response<List<GetDocumentsForEmployeeResponse>>>,
        IRequestHandler<GetEmployeeDocumentByIdQuery,Response<GetEmployeeDocumentByIdResponse>>
        
    {
        private readonly IEmployeeDocumentService documentService;
        private readonly IMapper mapper;
        private readonly IEmployeeService employeeService;
        private readonly IStringLocalizer<SharedResources> localizer;

        public EmployeeDocumentQueryHandler(IEmployeeDocumentService documentService,
            IMapper mapper,
            IEmployeeService employeeService
            , IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this.documentService = documentService;
            this.mapper = mapper;
            this.employeeService = employeeService;
            this.localizer = localizer;
        }
        public async Task<Response<GetEmployeeDocumentByIdResponse>> Handle(GetEmployeeDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var doc = await documentService.GetDocumentByIdAsync(request.Id);
            if (doc == null)
                return NotFound<GetEmployeeDocumentByIdResponse>();
            var doc_Mapped = mapper.Map<GetEmployeeDocumentByIdResponse>(doc);
            return Success(doc_Mapped);
        }

        public async Task<Response<List<GetDocumentsForEmployeeResponse>>> Handle(GetDocumentsForEmployeeQuery request, CancellationToken cancellationToken)
        {
            var isEmployeeExist = await employeeService.IsEmployeeExistById(request.EmployeeId);
            if (!isEmployeeExist)
                return NotFound<List<GetDocumentsForEmployeeResponse>>();
            var docs = await documentService.GetEmployeeDocumentsAsync(request.EmployeeId);
            var docs_Mapped = mapper.Map<List<GetDocumentsForEmployeeResponse>>(docs);
            return Success(docs_Mapped);
        }
    }
}
