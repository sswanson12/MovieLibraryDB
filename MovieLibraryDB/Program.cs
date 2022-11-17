using Microsoft.Extensions.DependencyInjection;
using MovieLibraryDB;
using MovieLibraryDB.Services;

try
{
    var startup = new Startup();
    var serviceProvider = startup.ConfigureServices();
    var service = serviceProvider.GetService<IMainService>();

    service?.Invoke();
}
catch (Exception e)
{
    Console.Error.WriteLine(e);
}