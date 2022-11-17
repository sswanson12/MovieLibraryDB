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
        throw new NotImplementedException();
    }

    public IEnumerable<Movie> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Movie> Search(string searchString)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}