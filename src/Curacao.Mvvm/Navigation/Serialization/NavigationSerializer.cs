using Newtonsoft.Json;

namespace Curacao.Mvvm.Navigation.Serialization
{
    internal class NavigationSerializer : ISerializer
    {
        public string Serialize<T>(T item)
        {
            return JsonConvert.SerializeObject(item);
        }

        public T Deserialize<T>(string encodedItem)
        {
            return JsonConvert.DeserializeObject<T>(encodedItem);
        }
    }
}