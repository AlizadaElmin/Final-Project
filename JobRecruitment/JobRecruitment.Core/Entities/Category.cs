using JobRecruitment.Core.Entities.Common;

namespace JobRecruitment.Core.Entities;

public class Category:BaseEntity
{
    public string Name { get; set; }
    public ICollection<JobOffer> JobOffers { get; set; }
}
