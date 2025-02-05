using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.DTOs.CategoryDtos;

public class CategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<JobOffer> JobOffers { get; set; }
}