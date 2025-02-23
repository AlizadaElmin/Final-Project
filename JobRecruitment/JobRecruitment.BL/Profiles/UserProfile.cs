using AutoMapper;
using JobRecruitment.BL.DTOs.CategoryDtos;
using JobRecruitment.BL.DTOs.UserDtos;
using JobRecruitment.Core.Entities;

namespace JobRecruitment.BL.Profiles;

public class UserProfile:Profile    
{
    public UserProfile()
    {
        CreateMap<RegisterDto, User>();
        CreateMap<User, UserGetDto>();
    }
}