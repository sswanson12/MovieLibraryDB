using MovieLibraryDB.Models;

namespace MovieLibraryDB.Factories;

public interface IMovieFactory
{
    Movie CreateMovie(string? title, DateTime releaseDate);
}