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
        long newId;
        
        try
        {
            newId = _repository.GetAll().Last().Id + 1;
        }
        catch (Exception)
        {
            newId = 1;
        }

        return new Movie
        {
            Id = newId,
            Title = title,
            ReleaseDate = releaseDate
        };
    }
}