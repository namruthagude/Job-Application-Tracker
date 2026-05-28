using System.ComponentModel.DataAnnotations;

namespace Job_Application_Tracker.DTOs
{
    public class CreateJobApplicationDto
    {
        [Required]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        public string JobBoard { get; set; } = string.Empty;

        public decimal? SalaryExpected { get; set; }

        public string Notes { get; set; } = string.Empty;

    }
}
