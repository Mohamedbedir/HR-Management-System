using FluentValidation;
using HR.Core.Features.EmployeeDocuments.Commands.Models;
using HR.Service.Services.Contract;
using Microsoft.AspNetCore.Http;

namespace HR.Core.Features.EmployeeDocuments.Commands.Validators
{
    public class AddEmployeeDocumentValidator
        : AbstractValidator<AddEmployeeDocumentCommand>
    {
        private readonly IEmployeeService employeeService;

        public AddEmployeeDocumentValidator(
            IEmployeeService employeeService)
        {
            this.employeeService = employeeService;

            ApplyValidationRules();
            ApplyCustomValidationRules();
        }

        private void ApplyValidationRules()
        {
            RuleFor(x => x.EmployeeId)
                .GreaterThan(0)
                .WithMessage("EmployeeId must be greater than 0.");

            RuleFor(x => x.DocumentType)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Document type is required.");

            RuleFor(x => x.File)
                .NotNull()
                .WithMessage("File is required.");

            RuleFor(x => x.File)
                .Must(IsValidExtension)
                .When(x => x.File != null)
                .WithMessage(
                    "Only PDF, JPG, JPEG and PNG files are allowed.");

            RuleFor(x => x.File)
                .Must(IsValidFileSize)
                .When(x => x.File != null)
                .WithMessage(
                    "File size cannot exceed 5 MB.");

            RuleFor(x => x.ExpiryDate)
                .GreaterThan(DateTime.UtcNow)
                .When(x => x.ExpiryDate.HasValue)
                .WithMessage(
                    "Expiry date must be in the future.");
        }

        private void ApplyCustomValidationRules()
        {
            RuleFor(x => x.EmployeeId)
                .MustAsync(async (employeeId, cancellationToken) =>
                    await employeeService
                        .IsEmployeeExistById(employeeId))
                .WithMessage("Employee does not exist.");
        }

        private bool IsValidExtension(IFormFile file)
        {
            var allowedExtensions = new[]
            {
                ".pdf",
                ".jpg",
                ".jpeg",
                ".png"
            };

            var extension =
                Path.GetExtension(file.FileName)
                    .ToLowerInvariant();

            return allowedExtensions.Contains(extension);
        }

        private bool IsValidFileSize(IFormFile file)
        {
            const long maxSize = 5 * 1024 * 1024;

            return file.Length <= maxSize;
        }
    }
}