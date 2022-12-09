using MovieLibraryDB.Models;

namespace MovieLibraryDB.Daos;

public interface IRepository
{
    void Add(Movie movie);

    void Add(User user);

    void Add(Occupation occupation);

    void Add(UserMovie userMovie);

    void Update(Movie update);

    void Update(UserMovie update);

    IEnumerable<Movie> GetMovies();

    IEnumerable<Genre> GetGenres();

    IEnumerable<User> GetUsers();

    IEnumerable<Occupation> GetOccupations();

    IEnumerable<Movie> Search(string? searchString);
}