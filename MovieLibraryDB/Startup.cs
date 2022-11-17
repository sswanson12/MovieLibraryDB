using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MovieLibraryDB.Contexts;
using MovieLibraryDB.Daos;
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


        services.AddTransient<IMainService, MainService>();
        services.AddSingleton<ILibrarian, Librarian>();
        services.AddSingleton<IRepository, Repository>();
        services.AddDbContextFactory<MovieLibraryContext>();

        return services.BuildServiceProvider();
    }
}