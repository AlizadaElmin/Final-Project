using FluentValidation;
using FluentValidation.AspNetCore;
using JobRecruitment.BL.ExternalServices.Implements;
using JobRecruitment.BL.ExternalServices.Interfaces;
using JobRecruitment.BL.Services.Implements;
using JobRecruitment.BL.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JobRecruitment.BL;

public static class ServiceRegistration
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAccoutService, AccountService>();
        services.AddScoped<ICandidateJobOfferService, CandidateJobOfferService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IJobOfferService, JobOfferService>();
        services.AddScoped<ISavedJobService, SavedJobService>();
        services.AddScoped<IJwtTokenHandler, JwtTokenHandler>();
        return services;
    }
    
    public static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining(typeof(ServiceRegistration));
        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceRegistration));
        return services;
    }
}