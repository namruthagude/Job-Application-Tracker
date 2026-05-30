using Job_Application_Tracker.Data;
using Job_Application_Tracker.DTOs;
using Job_Application_Tracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Job_Application_Tracker.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class JobApplicationsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public JobApplicationsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(string? status = null, string? company = null)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var query =  _context.JobApplications.Where(x => x.UserId == userId).AsQueryable();

            if(!string.IsNullOrEmpty(status))
            {
                query = query.Where(x => x.Status == status);
            }

            if (!string.IsNullOrEmpty(company))
            {
                query = query.Where(x => x.CompanyName.Contains(company));
            }

            var applications = await query.ToListAsync();
            return Ok(applications);

            
        }

        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var totalCount = await _context.JobApplications.CountAsync();

            var Interviews = await _context.JobApplications.CountAsync(x => x.Status == "Interview");

            var Selected = await _context.JobApplications.CountAsync(x => x.Status == "Selected");

            var Rejected = await _context.JobApplications.CountAsync(x => x.Status == "Rejected");

            var responseRate = (Interviews + Selected) / totalCount * 100;

            return Ok(new
            {
                TotalApplications = totalCount,
                TotalIterveiws = Interviews,
                TotalOffers = Selected,
                TotalRejections = Rejected,
                ReponseRate = $"{responseRate}%"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateJobApplicationDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var application = new JobApplication
            {
                CompanyName = dto.CompanyName,
                JobTitle = dto.JobTitle,
                JobBoard = dto.JobBoard,
                SalaryExpected = dto.SalaryExpected,
                AppliedDate = DateTime.Now,
                Status = "Applied",
                Notes = dto.Notes,
                UserId = userId,

            };

            await _context.JobApplications.AddAsync(application);
            await _context.SaveChangesAsync();
            return Ok(application);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateStatusDto statusdto)
        {
            var application = await _context.JobApplications.FindAsync(id);
            if(application == null)
            {
                return NotFound();
            }
            else
            {
                application.Status = statusdto.Status;
                await _context.SaveChangesAsync();
                return Ok(application);
            }
            
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var application = await _context.JobApplications.FindAsync(id);

            if (application == null)
                return NotFound();
            _context.JobApplications.Remove(application);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var application = await _context.JobApplications.FindAsync(id);
            if(application == null)
                return NotFound();
            return Ok(application);
        }

    }
}
