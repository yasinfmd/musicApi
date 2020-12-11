using AutoMapper;
using MusicApp.Entity;
using MusicApp.Entity.ParameterModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace MusicApp.Dto
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<MusicTypesDto, MusicTypes>();
            CreateMap<FilesDto, Files>();
            CreateMap<MusicTypes, MusicTypesDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();
        }
    }
}
