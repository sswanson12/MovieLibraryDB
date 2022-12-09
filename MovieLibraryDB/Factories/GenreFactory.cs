using MovieLibraryDB.Models;

namespace MovieLibraryDB.Factories;

public class GenreFactory : IFactory<Genre>
{
    public Genre Create()
    {
        return new Genre();
    }
}