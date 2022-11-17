using Microsoft.EntityFrameworkCore;
using MovieLibraryDB.Contexts;
using MovieLibraryDB.Models;

namespace MovieLibraryDB.Daos;

public class Repository : IRepository, IDisposable
{
    private readonly MovieLibraryContext _context;

    public Repository(IDbContextFactory<MovieLibraryContext> contextFactory)
    {
        _context = contextFactory.CreateDbContext();
    }
    
    public void Add(Movie movie)
    {
        _context.Movies.Add(movie);
        _context.SaveChanges();
        
    }

    public IEnumerable<Movie> GetAll()
    {
        return _context.Movies;
    }

    public IEnumerable<Movie> Search(string? searchString)
    {
        var results = _context.Movies.ToList();
        
        foreach (var movie in _context.Movies.ToList()
                     .Where(movie => !movie.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)))
        {
            results.Remove(movie);
        }

        return results;
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}