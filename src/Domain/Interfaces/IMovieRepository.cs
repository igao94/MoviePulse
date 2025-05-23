﻿using Domain.Entities;

namespace Domain.Interfaces;

public interface IMovieRepository
{
    Task<IEnumerable<Movie>> GetAllMoviesAsync();
    Task<Movie?> GetMovieByIdAsync(string id);
    void RemoveMovie(Movie movie);
    void AddMovie(Movie movie);
    Task<bool> MovieExistAsync(string title);
}
