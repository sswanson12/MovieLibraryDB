using MovieLibraryDB.Daos;
using MovieLibraryDB.Models;

namespace MovieLibraryDB.Factories;

public class MovieFactory : IMovieFactory
{
    private readonly IRepository _repository;

    public MovieFactory(IRepository repository)
    {
        _repository = repository;
    }

    public Movie CreateMovie(string? title, DateTime releaseDate)
    {
        return new Movie
        {
            Title = title,
            ReleaseDate = releaseDate
        };
    }
}