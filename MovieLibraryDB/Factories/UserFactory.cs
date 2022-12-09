using MovieLibraryDB.Models;

namespace MovieLibraryDB.Factories;

public class UserFactory : IFactory<User>
{
    public User Create()
    {
        return new User();
    }
}