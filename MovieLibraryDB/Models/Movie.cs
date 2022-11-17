namespace MovieLibraryDB.Models;

public class Movie
{
    public long Id { get; set; }
    public string Title { get; set; }
    public DateTime ReleaseDate { get; set; }


    public virtual ICollection<Genre> MovieGenres { get; set; }
}