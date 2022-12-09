using MovieLibraryDB.Daos;
using MovieLibraryDB.Models;

namespace MovieLibraryDB.Factories;

public class MovieFactory : IFactory<Movie>
{
    public Movie Create()
    {
        return new Movie()
        {
            MovieGenres = new List<MovieGenre>()
        };
    }
}