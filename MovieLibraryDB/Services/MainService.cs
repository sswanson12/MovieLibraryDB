using MovieLibraryDB.Daos;
using MovieLibraryDB.Factories;

namespace MovieLibraryDB.Services;

public class MainService : IMainService
{
    private readonly IRepository _repository;
    private readonly IConsoleService _consoleService;
    private readonly IMovieFactory _movieFactory;

    public MainService(IRepository repository, IConsoleService consoleService, IMovieFactory movieFactory)
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
        var movies = _repository.GetAll().ToList();

        foreach (var movie in movies)
        {
            _consoleService.Write($"Id: {movie.Id} - Title: {movie.Title} - Release Date: {movie.ReleaseDate}");
        }
    }

    void SearchMovies()
    {
        _consoleService.Write("Enter your search below: ");
        
        var input = _consoleService.GetString();

        var results = _repository.Search(input);
        
        foreach (var movie in results)
        {
            _consoleService.Write($"Id: {movie.Id} - Title: {movie.Title} - Release Date: {movie.ReleaseDate}");
        }
    }

    void UpdateMovie()
    {
        
    }

    void AddMovie()
    {
        _consoleService.Write("Please enter the movie title: ");
        var title = _consoleService.GetString();

        _consoleService.Write("Please enter the release date: ");
        var releaseDate = _consoleService.GetDate();
        
        _repository.Add(_movieFactory.CreateMovie(title, releaseDate));
    }

    void DeleteMovie()
    {
        
    }

    void DisplayMenu()
    {
        _consoleService.Write("(1) View all movies (Literally all)" +
                              "\n(2) Search movies" +
                              "\n(3) Update a movie" +
                              "\n(4) Add a movie" +
                              "\n(5) Delete a movie" +
                              "\n(X) Quit");
    }
}