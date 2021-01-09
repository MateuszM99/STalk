using Application.DTO;
using AutoMapper;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public class AddToContactRequestProfile : Profile
    {
        public AddToContactRequestProfile()
        {
            CreateMap<AddToContactRequest, AddToContactRequestDTO>()
                .ForMember(dest => dest.UserFrom, opt => opt.MapFrom(src => src.UserFrom));
                
        }
    }
}
