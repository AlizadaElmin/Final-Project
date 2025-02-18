using FluentValidation.AspNetCore;
using JobRecruitment;
using JobRecruitment.BL.Extensions;
using JobRecruitment.BL;
using JobRecruitment.Core.Entities;
using JobRecruitment.DAL;
using JobRecruitment.DAL.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement 
    { 
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddDbContext<JobRecruitmentDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("MSSql"));
});

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = false;
    opt.SignIn.RequireConfirmedEmail = false;
    opt.Password.RequiredLength = 3; 
    opt.Password.RequireDigit = false; 
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Lockout.MaxFailedAccessAttempts = 3;
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
}).AddDefaultTokenProviders().AddEntityFrameworkStores<JobRecruitmentDbContext>();

builder.Services.AddAuth(builder.Configuration);
builder.Services.AddOptions(builder.Configuration);
builder.Services.AddRepositories(); 
builder.Services.AddServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddFluentValidation();
builder.Services.AddAutoMapper();
builder.Services.AddMemoryCache();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
            
app.UseHttpsRedirection();
app.UseUserSeed();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

