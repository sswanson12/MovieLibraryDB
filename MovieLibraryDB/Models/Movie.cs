namespace MovieLibraryDB.Models;

public class Movie
{
    public long Id { get; set; }
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public virtual ICollection<MovieGenre> MovieGenres { get; set; }
    public virtual ICollection<UserMovie> UserMovies { get; set; }

    public override string ToString()
    {
        return $"Id: {Id} - Title: {Title} - Release Date: {ReleaseDate} \n" +
               $"Movie Genres: {MovieGenres.Aggregate("", (current, genre) => current + $"{genre.Genre.Name}, ")}"[..^2] + ".";
    }
}