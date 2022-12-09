namespace MovieLibraryDB.Factories;

public interface IFactory<T>
{
    T Create();
}