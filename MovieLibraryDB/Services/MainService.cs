using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
    private readonly IFactory<UserMovie> _userMovieFactory;

    public MainService(IRepository repository, IConsoleService consoleService, IFactory<Movie> movieFactory, IFactory<MovieGenre> movieGenreFactory, IFactory<User> userFactory, IFactory<UserMovie> userMovieFactory)
    {
        _repository = repository;
        _consoleService = consoleService;
        _movieFactory = movieFactory;
        _movieGenreFactory = movieGenreFactory;
        _userFactory = userFactory;
        _userMovieFactory = userMovieFactory;
    }

    public void Invoke()
    {
        _consoleService.Write("Welcome to Sam's Movie Library Database Application" +
                              "\nSelect what you'd like to do from the menu below.");
        DisplayMenu();

        var userInput = _consoleService.GetString().ToLower();

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
                    AddUser();
                    break;
                case "6":
                    ViewAllUsers();
                    break;
                case "7":
                    EnterRating();
                    break;
                case "8":
                    ViewAllRatings();
                    break;
            }

            _consoleService.Write("Would you like to continue?");
            DisplayMenu();

            userInput = _consoleService.GetString().ToLower();

        } while (!userInput.Equals("x"));
    }

    private void EnterRating()
    {
        var newRating = _userMovieFactory.Create();
        
        ViewAllUsers();

        _consoleService.Write("Please enter the Id of the user rating the movie");

        var userId = _consoleService.GetInt();

        while (!_repository.GetUsers().Any(u => u.Id.Equals(userId)))
        {
            _consoleService.Write("Please enter a valid User Id: ");
            userId = _consoleService.GetInt();
        }

        newRating.User = _repository.GetUsers().First(u => u.Id.Equals(userId));
        
        _consoleService.Write("Search for the movie you'd like to rate.");
        SearchMovies();

        _consoleService.Write("Please enter the Id of the movie being rated: ");
        var movieId = _consoleService.GetInt();
        
        while (!_repository.GetMovies().Any(m => m.Id.Equals(movieId)))
        {
            _consoleService.Write("Please enter a valid Movie Id: ");
            movieId = _consoleService.GetInt();
        }

        newRating.Movie = _repository.GetMovies().First(m => m.Id.Equals(movieId));

        _consoleService.Write("Please enter a rating (1-5) for the movie: ");
        var rating = _consoleService.GetInt();

        while (rating is < 1 or > 5)
        {
            _consoleService.Write("Please enter an integer rating (1, 2, 3, 4, 5): ");
            rating = _consoleService.GetInt();
        }

        newRating.Rating = rating;

        _repository.Add(newRating);
    }

    private void ViewAllUsers()
    {
        var users = _repository.GetUsers().OrderByDescending(u => u.Id).ToList();

        for (int i = 0; i < 10; i++)
        {
            _consoleService.Write($"{users[i]}\n");
        }
    }

    void ViewAllMovies()
    {
        var movies = _repository.GetMovies().OrderByDescending(m => m.Id).ToList();

        for (int i = 0; i < 10; i++)
        {
            _consoleService.Write($"{movies[i]}\n");
        }
    }

    private void ViewAllRatings()
    {
        var userMovies = _repository.GetUserMovies().OrderByDescending(u => u.Id).ToList();

        for (int i = 0; i < 10; i++)
        {
            _consoleService.Write($"{userMovies[i]}\n");
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

        while (!_repository.GetMovies().Any(m => m.Id.Equals(editId)))
        {
            _consoleService.Write("That ID does not exist. Please enter another value: ");
            editId = _consoleService.GetInt();
        }

        var editSelection = _repository.GetMovies().First(m => m.Id.Equals(editId));

        var control = "";

        while (!control.Equals("x", StringComparison.CurrentCultureIgnoreCase))
        {
            _consoleService.Write($"What would you like to change about the following movie?\n{editSelection}" +
                                  $"\n(1) Title - (2) ReleaseDate - (3) Genres - (X) Exit: ");

            control = _consoleService.GetString();
            switch (control)
            {
                case "1":
                    _consoleService.Write("Please enter a title: ");
        
                    var titleInput = _consoleService.GetString();
        
                    while (_repository.GetMovies().Any(m => m.Title != null && m.Title.Equals(titleInput, StringComparison.CurrentCultureIgnoreCase)))
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
                        while (!_repository.GetGenres().Any(g => g.Name.Equals(currentInput, StringComparison.CurrentCultureIgnoreCase)))
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
        
        while (_repository.GetMovies().Any(m => m.Title != null && m.Title.Equals(titleInput, StringComparison.CurrentCultureIgnoreCase)))
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
            while (!_repository.GetGenres().Any(g => g.Name.Equals(currentInput, StringComparison.CurrentCultureIgnoreCase)))
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

    void AddUser()
    {
        var newUser = _userFactory.Create();
        
        _consoleService.Write("Please enter the user's age: ");
        var ageInput = _consoleService.GetInt();

        while (ageInput is <= 110 and < 0)
        {
            _consoleService.Write("Please enter a reasonable age: ");
            ageInput = _consoleService.GetInt();
        }

        newUser.Age = ageInput;

        _consoleService.Write("Please enter the user's gender: (M)/(F)");
        var genderInput = _consoleService.GetString();

        while ((!genderInput.Equals("M", StringComparison.CurrentCultureIgnoreCase) || genderInput.Equals("F", StringComparison.CurrentCultureIgnoreCase)))
        {
            _consoleService.Write("Please enter either (M) or (F): ");
            genderInput = _consoleService.GetString();
        }

        newUser.Gender = genderInput;

        _consoleService.Write("Please enter the user's zipcode: ");
        var zipInput = _consoleService.GetString();

        while (!(zipInput.Length is 5 && int.TryParse(zipInput, out _)))
        {
            _consoleService.Write("Ensure that you're entering a 5-digit integer: ");
            zipInput = _consoleService.GetString();
        }

        newUser.ZipCode = zipInput;
        
        _consoleService.Write("Please enter an occupation for the user. Here is a list of known occupations: \n" +
                              $"{_repository.GetOccupations().Aggregate("", (current, occupation) => current + $"{occupation.Name}, ")[..^2]}.");

        var occupationInput = _consoleService.GetString();
        
        while (!_repository.GetOccupations().Any(o => o.Name.Equals(occupationInput, StringComparison.CurrentCultureIgnoreCase)))
        {
            _consoleService.Write("That does not match one of the existing occupations. Please try again: ");
            occupationInput = _consoleService.GetString();
        }

        newUser.Occupation = _repository.GetOccupations()
            .First(o => o.Name.Equals(occupationInput, StringComparison.CurrentCultureIgnoreCase));

        _repository.Add(newUser);
    }
    void DisplayMenu()
    {
        _consoleService.Write("(1) View the 10 most recently cataloged movies" +
                              "\n(2) Search movies" +
                              "\n(4) Add a movie" +
                              "\n(3) Update a movie" +
                              "\n(5) Add a user" +
                              "\n(6) View the 10 most recently cataloged users" +
                              "\n(7) Enter a rating" +
                              "\n(8) View the 10 most recently cataloged ratings" +
                              "\n(X) Quit");
    }
}