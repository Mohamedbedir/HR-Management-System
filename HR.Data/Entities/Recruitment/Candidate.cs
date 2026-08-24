using HR.Data.Entities.Common;

namespace HR.Data.Entities.Recruitment
{
    public class Candidate : BaseEntity
    {
        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Phone { get; set; }

        public string? CVPath { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation
        public ICollection<Application> Applications { get; set; }
            = new HashSet<Application>();
    }
}