using AutoMapper;
using Core.Entities;
using Core.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapper
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Media, MediaDto>();
            CreateMap<Actor, ActorDto>();
            CreateMap<Rating, RatingDto>();
            CreateMap<Screening, ScreeningDto>();
            CreateMap<User, SpectatorDto>();
            CreateMap<AddScreeningDto, Screening>();
        }
    }
}
