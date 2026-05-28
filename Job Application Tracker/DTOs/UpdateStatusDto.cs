using System.ComponentModel.DataAnnotations;

namespace Job_Application_Tracker.DTOs
{
    public class UpdateStatusDto
    {
        [Required]
        public string Status { get; set; } = string.Empty;
    }
}
