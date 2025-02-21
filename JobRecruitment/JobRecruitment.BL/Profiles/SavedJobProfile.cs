using AutoMapper;
using JobRecruitment.BL.DTOs.SavedJobDtos;
using JobRecruitment.Core.Entities;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace JobRecruitment.BL.Profiles;

public class SavedJobProfile:Profile
{
    public SavedJobProfile()
    {
        CreateMap<SavedJobCreateDto, SavedJob>();
        CreateMap<SavedJobUpdateDto, SavedJob>();
    }
}