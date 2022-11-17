using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MovieLibraryDB.Models;

namespace MovieLibraryDB.Contexts;

public class MovieLibraryContext : DbContext
{
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Movie> Movie { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        optionsBuilder.UseSqlServer(
            configuration.GetConnectionString("MovieContext")
        );
    }
}