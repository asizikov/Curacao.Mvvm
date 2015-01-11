namespace Curacao.Mvvm.Navigation
{
    public interface ISerializer
    {
        string Serialize<T>(T item);
        T Deserialize<T>(string encodedItem);
    }
}