using MovieLibraryDB.Daos;
using MovieLibraryDB.Factories;
using MovieLibraryDB.Models;

namespace MovieLibraryDB.Services;

public class MainService : IMainService
{
    private readonly IRepository _repository;
    private readonly IConsoleService _consoleService;
    private readonly IFactory<Movie> _movieFactory;

    public MainService(IRepository repository, IConsoleService consoleService, IFactory<Movie> movieFactory)
    {
        _repository = repository;
        _consoleService = consoleService;
        _movieFactory = movieFactory;
    }

    public void Invoke()
    {
        _consoleService.Write("Welcome to Sam's Movie Library Database Application" +
                              "\nSelect what you'd like to do from the menu below.");
        DisplayMenu();

        var userInput = _consoleService.GetString()?.ToLower();

        do
        {
            switch (userInput)
            {
                case "1":
                    ViewAllMovies();
                    break;
                case "2":
                    SearchMovies();
                    break;
                case "3":
                    UpdateMovie();
                    break;
                case "4":
                    AddMovie();
                    break;
                case "5":
                    DeleteMovie();
                    break;
            }

            _consoleService.Write("Would you like to continue?");
            DisplayMenu();

            userInput = _consoleService.GetString()?.ToLower();

        } while (userInput != null && !userInput.Equals("x"));
    }

    void ViewAllMovies()
    {
        var movies = _repository.GetAll().OrderByDescending(m => m.Id).ToList();

        for (int i = 0; i < 10; i++)
        {
            _consoleService.Write($"{movies[i]}\n");
        }
    }

    void SearchMovies()
    {
        _consoleService.Write("Enter your search below: ");
        
        var input = _consoleService.GetString();

        var results = _repository.Search(input);
        
        foreach (var movie in results)
        {
            _consoleService.Write($"{movie}\n");
        }
    }

    void UpdateMovie()
    {
        
    }

    void AddMovie()
    {
        _consoleService.Write("Please enter a title: ");
        
        var titleInput = _consoleService.GetString();
        
        while (_repository.GetAll().Any(m => m.Title.Equals(titleInput, StringComparison.CurrentCultureIgnoreCase)))
        {
            _consoleService.Write("That title already exists. Enter a different title: ");
            titleInput = _consoleService.GetString();
        }

        _consoleService.Write("Please enter the movie's release date: ");

        var dateInput = _consoleService.GetDate();
        
        _consoleService.Write("Enter any genres pertaining to the movie.\n" +
                              $"Available genres include: {_repository.GetGenres().Aggregate("", (current, genre) => current + $"{genre.Name}, ")[..^2]}.");
        
        
    }

    void DeleteMovie()
    {
        
    }

    void DisplayMenu()
    {
        _consoleService.Write("(1) View the 10 most recently cataloged movies" +
                              "\n(2) Search movies" +
                              "\n(3) Update a movie" +
                              "\n(4) Add a movie" +
                              "\n(5) Delete a movie" +
                              "\n(X) Quit");
    }
}