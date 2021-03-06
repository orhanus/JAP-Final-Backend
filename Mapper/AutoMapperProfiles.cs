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
            CreateMap<AddMediaDto, Media>();
            CreateMap<Media, MediaDto>();
            CreateMap<UpdateMediaDto, Media>();

            CreateMap<Actor, ActorDto>();
            CreateMap<ActorDto, Actor>();

            CreateMap<Rating, RatingDto>();

            CreateMap<Screening, ScreeningDto>();
            CreateMap<AddScreeningDto, Screening>();

            CreateMap<User, SpectatorDto>();

            CreateMap<Address, AddressDto>();
            CreateMap<AddressDto, Address>();
        }
    }
}
