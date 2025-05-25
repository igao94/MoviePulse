using Application.Celebrities.DTOs;
using Application.Extensions;
using Application.Movies.DTOs;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Movie, MovieDto>();

        CreateMap<CreateMovieDto, Movie>()
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src =>
                src.ReleaseDate.ParseDateOnlyFromString()));

        CreateMap<UpdateMovieDto, Movie>()
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src =>
                src.ReleaseDate.ParseDateOnlyFromString()));

        CreateMap<User, UserDto>();

        CreateMap<UpdateUserDto, User>();

        CreateMap<CreateCelebrityDto, Celebrity>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src =>
                src.DateOfBirth.ParseDateOnlyFromString()));

        CreateMap<Celebrity, CelebrityDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()));
    }
}
