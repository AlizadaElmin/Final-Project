using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.DTOs.UserDtos;

public class EmployerGetDto
{
    public string CompanyName { get; set; }
    public ICollection<JobOffer> PostedJobs { get; set; }   
}