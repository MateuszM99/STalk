using Application.DTO;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.Files.FirstOrDefault(x => x.Id == src.FileAvatarId).FileContent));

        }
    }
}
