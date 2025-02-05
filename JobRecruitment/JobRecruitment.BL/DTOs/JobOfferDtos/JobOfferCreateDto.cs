namespace JobRecruitment.BL.DTOs.JobOfferDtos;

public class JobOfferCreateDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public string EmployerId { get; set; }
}