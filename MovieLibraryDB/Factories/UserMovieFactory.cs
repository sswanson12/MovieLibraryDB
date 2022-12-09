using MovieLibraryDB.Models;

namespace MovieLibraryDB.Factories;

public class UserMovieFactory : IFactory<UserMovie>
{
    public UserMovie Create()
    {
        return new UserMovie()
        {
            RatedAt = DateTime.Now
        };
    }
}