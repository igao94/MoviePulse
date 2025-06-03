using Application.Celebrities.DTOs;
using Application.Extensions;
using Application.Movies.DTOs;
using Application.UserMovieInteractions.DTOs;
using Application.Users.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Core;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Movie, MovieDto>()
            .ForMember(dest => dest.Genres, opt => opt.MapFrom(src => src.Genres.Select(g => g.Genre.Name)));

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

        CreateMap<UpdateCelebrityDto, Celebrity>()
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src =>
                src.DateOfBirth.ParseDateOnlyFromString()));

        CreateMap<Celebrity, CelebrityDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.GetFullName()));

        CreateMap<MovieRole, MovieRoleDto>();

        CreateMap<UserMovieInteraction, UserMovieIntercationDto>()
            .ForMember(dest => dest.MovieId, opt => opt.MapFrom(src => src.MovieId))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Movie.Title))
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Movie.Description))
            .ForMember(dest => dest.DurationInMinutes, opt => opt.MapFrom(src => src.Movie.DurationInMinutes))
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.Movie.Rating))
            .ForMember(dest => dest.ReleaseDate, opt => opt.MapFrom(src => src.Movie.ReleaseDate))
            .ForMember(dest => dest.Celebrities, opt =>
                opt.MapFrom(src => src.Movie.Celebrities.Select(c => c.Celebrity.GetFullName()).Distinct()));
    }
}
