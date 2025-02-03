namespace JobRecruitment.Core.Entities;

public class Employer:User
{
    public string CompanyName { get; set; }
    public ICollection<JobOffer> PostedJobs { get; set; }
}