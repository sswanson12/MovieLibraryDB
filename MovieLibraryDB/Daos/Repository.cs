using Microsoft.EntityFrameworkCore;
using MovieLibraryDB.Contexts;
using MovieLibraryDB.Models;

namespace MovieLibraryDB.Daos;

public class Repository : IRepository, IDisposable
{
    private readonly IDbContextFactory<MovieLibraryContext> _contextFactory;

    private readonly MovieLibraryContext _context;

    public Repository(IDbContextFactory<MovieLibraryContext> contextFactory)
    {
        _contextFactory = contextFactory;
        _context = _contextFactory.CreateDbContext();
    }
    
    public void Add(Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();
    }

    public void Add(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void Add(Occupation occupation)
    {
        _context.Occupations.Add(occupation);
        _context.SaveChanges();
    }

    public void Add(UserMovie userMovie)
    {
        _context.UserMovies.Add(userMovie);
        _context.SaveChanges();
    }

    public void Update(Movie update)
    {
        var movie = _context.Movies.First(m => m.Id.Equals(update.Id));

        movie.Title = update.Title;
        
        movie.ReleaseDate = update.ReleaseDate;
        
        movie.MovieGenres = update.MovieGenres;
        
        movie.UserMovies = update.UserMovies;

        _context.SaveChanges();
    }

    public void Update(UserMovie update)
    {
        var userMovie = _context.UserMovies.First(m => m.Id.Equals(update.Id));

        userMovie.Rating = update.Rating;
        
        userMovie.RatedAt = update.RatedAt;
        
        userMovie.User = update.User;
        
        userMovie.Movie = update.Movie;

        _context.SaveChanges();
    }

    public IEnumerable<Movie> GetMovies()
    {
        return _context.Movies;
    }

    public IEnumerable<Genre> GetGenres()
    {
        return _context.Genres;
    }

    public IEnumerable<User> GetUsers()
    {
        return _context.Users;
    }

    public IEnumerable<Occupation> GetOccupations()
    {
        return _context.Occupations;
    }

    public IEnumerable<Movie> Search(string? searchString)
    {
        var allMovies = _context.Movies;
        var listOfMovies = allMovies.ToList();
        return listOfMovies.Where(x => x.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}