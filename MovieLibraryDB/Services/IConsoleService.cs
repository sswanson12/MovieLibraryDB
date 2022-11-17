namespace MovieLibraryDB.Services;

public interface IConsoleService
{
    void Write(string message);

    string? GetString();

    int GetInt();

    DateTime GetDate();
}