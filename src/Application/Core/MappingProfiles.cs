using Application.Movies.DTOs;
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
                DateOnly.ParseExact(src.ReleaseDate, "yyyy-MM-dd")));
    }
}
