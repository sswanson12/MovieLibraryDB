namespace MovieLibraryDB.Models;

public class UserMovie
{
    public long Id { get; set; }
    public long Rating { get; set; }
    public DateTime RatedAt { get; set; }

    public virtual User User { get; set; }
    public virtual Movie Movie { get; set; }

    public override string ToString()
    {
        return $"Id: {Id} - Rating: {Rating} - User: {User.Occupation.Name}{User.Id} - Movie: {Movie.Title}";
    }
}