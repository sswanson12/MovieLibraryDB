using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryDB.Contexts;
using MovieLibraryDB.Daos;
using MovieLibraryDB.Factories;
using MovieLibraryDB.Models;
using MovieLibraryDB.Services;

namespace MovieLibraryDB;

public class Startup
{
    public IServiceProvider ConfigureServices()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddLogging(builder =>
        {
            builder.AddConsole();
            builder.AddFile("app.log");
        });

        services.AddSingleton<IMainService, MainService>();
        services.AddSingleton<IConsoleService, ConsoleService>();
        services.AddSingleton<IMovieFactory, MovieFactory>();
        services.AddSingleton<IRepository, Repository>();
        services.AddDbContextFactory<MovieLibraryContext>();

        return services.BuildServiceProvider();
    }
}