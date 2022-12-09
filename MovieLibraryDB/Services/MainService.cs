using MovieLibraryDB.Daos;
using MovieLibraryDB.Factories;
using MovieLibraryDB.Models;

namespace MovieLibraryDB.Services;

public class MainService : IMainService
{
    private readonly IRepository _repository;
    private readonly IConsoleService _consoleService;
    private readonly IFactory<Movie> _movieFactory;
    private readonly IFactory<MovieGenre> _movieGenreFactory;
    private readonly IFactory<User> _userFactory;

    public MainService(IRepository repository, IConsoleService consoleService, IFactory<Movie> movieFactory, IFactory<MovieGenre> movieGenreFactory, IFactory<User> userFactory)
    {
        _repository = repository;
        _consoleService = consoleService;
        _movieFactory = movieFactory;
        _movieGenreFactory = movieGenreFactory;
        _userFactory = userFactory;
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
        _consoleService.Write("Search for a movie you'd like to edit.");
        
        SearchMovies();
        
        _consoleService.Write("Enter the ID of the movie you'd like to edit: ");
        
        var editId = _consoleService.GetInt();

        while (!_repository.GetAll().Any(m => m.Id.Equals(editId)))
        {
            _consoleService.Write("That ID does not exist. Please enter another value: ");
            editId = _consoleService.GetInt();
        }

        var editSelection = _repository.GetAll().First(m => m.Id.Equals(editId));

        var control = "";

        while (control != null && !control.Equals("x", StringComparison.CurrentCultureIgnoreCase))
        {
            _consoleService.Write($"What would you like to change about the following movie?\n{editSelection}" +
                                  $"\n(1) Title - (2) ReleaseDate - (3) Genres - (X) Exit: ");

            control = _consoleService.GetString();
            switch (control)
            {
                case "1":
                    _consoleService.Write("Please enter a title: ");
        
                    var titleInput = _consoleService.GetString();
        
                    while (_repository.GetAll().Any(m => m.Title.Equals(titleInput, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        _consoleService.Write("That title already exists. Enter a different title: ");
                        titleInput = _consoleService.GetString();
                    }

                    editSelection.Title = titleInput;
                    break;
                case "2":
                    editSelection.ReleaseDate = _consoleService.GetDate();
                    break;
                case "3":
                    editSelection.MovieGenres.Clear();
                    
                    _consoleService.Write("Any genres the movie previously had have been cleared. Enter all genres pertaining to the movie.\n" +
                                          $"Available genres include: {_repository.GetGenres().Aggregate("", (current, genre) => current + $"{genre.Name}, ")[..^2]}.");

                    _consoleService.Write("When done entering values, enter (X):");

                    var currentInput = _consoleService.GetString();
                    
                    while (!currentInput.Equals("x", StringComparison.OrdinalIgnoreCase))
                    {
                        while (_repository.GetGenres().All(g => g.Name.Equals(currentInput, StringComparison.CurrentCultureIgnoreCase)))
                        {
                            _consoleService.Write("That does not match one of the existing genres. Please try again: ");
                            currentInput = _consoleService.GetString();
                        }

                        var newMovieGenre = _movieGenreFactory.Create();

                        newMovieGenre.Genre = _repository.GetGenres()
                            .First(g => g.Name.Equals(currentInput, StringComparison.CurrentCultureIgnoreCase));

                        newMovieGenre.Movie = editSelection;

                        editSelection.MovieGenres.Add(newMovieGenre);
            
                        _consoleService.WriteNoBreak("Genre successfully added. Add another or enter (X) to finish: ");

                        currentInput = _consoleService.GetString();
                    }
                    break;
            }
        }
    }

    void AddMovie()
    {
        var newMovie = _movieFactory.Create();
        
        _consoleService.Write("Please enter a title: ");
        
        var titleInput = _consoleService.GetString();
        
        while (_repository.GetAll().Any(m => m.Title.Equals(titleInput, StringComparison.CurrentCultureIgnoreCase)))
        {
            _consoleService.Write("That title already exists. Enter a different title: ");
            titleInput = _consoleService.GetString();
        }

        newMovie.Title = titleInput;

        _consoleService.Write("Please enter the movie's release date: ");

        newMovie.ReleaseDate = _consoleService.GetDate();

        _consoleService.Write("Enter any genres pertaining to the movie.\n" +
                              $"Available genres include: {_repository.GetGenres().Aggregate("", (current, genre) => current + $"{genre.Name}, ")[..^2]}.");

        _consoleService.Write("When done entering values, enter (X):");

        var currentInput = _consoleService.GetString();

        while (!currentInput.Equals("x", StringComparison.OrdinalIgnoreCase))
        {
            while (_repository.GetGenres().All(g => g.Name.Equals(currentInput, StringComparison.CurrentCultureIgnoreCase)))
            {
                _consoleService.Write("That does not match one of the existing genres. Please try again: ");
                currentInput = _consoleService.GetString();
            }

            var newMovieGenre = _movieGenreFactory.Create();

            newMovieGenre.Genre = _repository.GetGenres()
                .First(g => g.Name.Equals(currentInput, StringComparison.CurrentCultureIgnoreCase));

            newMovieGenre.Movie = newMovie;

            newMovie.MovieGenres.Add(newMovieGenre);
            
            _consoleService.WriteNoBreak("Genre successfully added. Add another or enter (X) to finish: ");

            currentInput = _consoleService.GetString();
        }

        _repository.Add(newMovie);
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