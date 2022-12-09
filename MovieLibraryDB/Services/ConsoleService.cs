namespace MovieLibraryDB.Services;

public class ConsoleService : IConsoleService
{
    public void Write(string message)
    {
        Console.WriteLine(message);
    }

    public void WriteNoBreak(string message)
    {
        Console.Write(message);
    }

    public string? GetString()
    {
        return Console.ReadLine();
    }

    public int GetInt()
    {
        int input;

        while (!int.TryParse(Console.ReadLine(), out input))
        {
            Console.WriteLine("Please ensure you're entering an integer, try again.");
        }

        return input;
    }

    public DateTime GetDate()
    {
        DateTime inputtedDate;

        while (!DateTime.TryParse(Console.ReadLine(), out inputtedDate))
        {
            Console.WriteLine("Please ensure you're entering a properly formatted date (Ex. 1-1-2000)");
        }

        return inputtedDate;
    }
}