namespace MovieLibraryDB.Services;

public interface IConsoleService
{
    void Write(string message);

    void WriteNoBreak(string message);

    string? GetString();

    int GetInt();

    DateTime GetDate();
}