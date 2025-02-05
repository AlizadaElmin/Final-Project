using JobRecruitment.Core.Repositories;
using JobRecruitment.DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JobRecruitment.DAL;

public static class ServiceRegistration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IJobOfferRepository, JobOfferRepository>();
        services.AddScoped<ISavedJobRepository, SavedJobRepository>();
        return services;
    }
}