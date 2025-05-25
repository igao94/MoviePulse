using Application.Celebrities.DTOs;
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
                ParseDateOnlyFromString(src.ReleaseDate)));

        CreateMap<UpdateMovieDto, Movie>()
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src =>
                ParseDateOnlyFromString(src.ReleaseDate)));

        CreateMap<User, UserDto>();

        CreateMap<UpdateUserDto, User>();

        CreateMap<CreateCelebrityDto, Celebrity>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src =>
                ParseDateOnlyFromString(src.DateOfBirth)));

        CreateMap<Celebrity, CelebrityDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()));
    }

    private static DateOnly ParseDateOnlyFromString(string str)
    {
        return DateOnly.ParseExact(str, "yyyy-MM-dd");
    }
}
