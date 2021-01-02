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
            CreateMap<MusicTypes, MusicTypesDto>()
         .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
         .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
         .ReverseMap();
            CreateMap<MusicDto, Music>().ReverseMap();
            CreateMap<AlbumFilesDto, AlbumsFiles>().ForPath(dest => dest.File.Id, opt => opt.MapFrom(src => src.Id)).ForPath(dest => dest.File.Path, opt => opt.MapFrom(src => src.Path)).ReverseMap();
            //.ForMember(dest=>dest.AlbumsFiles,opt=>opt.MapFrom(src=>src.Images)) 
            CreateMap<AlbumDto, Albums>().ForMember(dest => dest.Artist, act => act.MapFrom(src => src.Artist)).ForMember(dest=>dest.Musics,act=>act.MapFrom(src=>src.Musics))
                .ForMember(dest => dest.AlbumsFiles, opt => opt.MapFrom(src => src.Images)).ReverseMap();
            CreateMap<ArtistDto, Artist>().ForMember(dest => dest.File, opt => opt.MapFrom(src => src.File)).ReverseMap();
            CreateMap<FilesDto, Files>().ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.Path)).ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)).ReverseMap();

//            CreateMap<AlbumsFiles, string>().ConvertUsing(new CustomTypeConverter());
            // CreateMap<AlbumsFiles, AlbumFilesDto>().ForMember(dest => dest.Path, opt => opt.MapFrom(src => src.File.Path));
            // CreateMap<AlbumFilesDto, AlbumsFiles>().ForMember(dest => dest.File.Path, opt => opt.MapFrom(src => src.Path));


            //  CreateMap<AlbumFilesDto, AlbumsFiles>().ForMember(dest => dest.File, opt => opt.MapFrom(src => src)).ForMember(dest => dest.File.Path, opt => opt.MapFrom(src => src));

            //CreateMap<AlbumDto, Albums>().ForMember(dest => dest.Artist.File, opt => opt.MapFrom(src => src.Artist.File))
            // .ForMember(dest => dest.Artist, opt => opt.MapFrom(src => src.Artist))
            // .ForPath(dest => dest.AlbumsFiles, opt => opt.MapFrom(src => src.Images)).ReverseMap();
       //     CreateMap<MusicTypesDto, MusicTypes>();
          //  CreateMap<FilesDto, Files>().ForMember(dest => dest., opt => opt.MapFrom(src => src.File));
            CreateMap<MusicTypes, MusicTypesDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ReverseMap();



           // CreateMap<AlbumDto, Album>().ForMember(dest => dest.Artist, act => act.MapFrom(src => src.ArtistDto)).ReverseMap();
           // CreateMap<ArtistDto, Artist>().ForMember(dest => dest.File, act => act.MapFrom(src => src.FileDto)).ReverseMap();
           // CreateMap<FileDto, File>().ForMember(dest => dest.Path, act => act.MapFrom(src => src.PathDto)).ReverseMap();

        }

        public class CustomTypeConverter : ITypeConverter<AlbumsFiles, string>
        {
  

            string ITypeConverter<AlbumsFiles, string>.Convert(AlbumsFiles source, string destination, ResolutionContext context)
            {
                return source.File.Path;
            }
        }
    }
}
