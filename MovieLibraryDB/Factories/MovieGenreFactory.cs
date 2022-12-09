using MovieLibraryDB.Models;

namespace MovieLibraryDB.Factories;

public class MovieGenreFactory : IFactory<MovieGenre>
{
    public MovieGenre Create()
    {
        return new MovieGenre();
    }
}