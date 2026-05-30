using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Mime;

namespace Job_Application_Tracker.Models
{
    public class JobApplication
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public User? User { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle {  get; set; } = string.Empty;
        public string Status { get; set; } = "Applied";

        public string JobBoard {  get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        public decimal? SalaryExpected { get; set; }

        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
    }
}
