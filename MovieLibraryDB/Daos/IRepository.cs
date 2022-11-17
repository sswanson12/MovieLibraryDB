using MovieLibraryDB.Models;

namespace MovieLibraryDB.Daos;

public interface IRepository
{
    void Add(Movie movie);

    IEnumerable<Movie> GetAll();

    IEnumerable<Movie> Search(string searchString);
}