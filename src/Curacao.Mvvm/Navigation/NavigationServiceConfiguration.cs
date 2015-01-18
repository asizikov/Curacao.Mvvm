using Curacao.Mvvm.Navigation.Mapping;
using Curacao.Mvvm.Navigation.Serialization;
using JetBrains.Annotations;

namespace Curacao.Mvvm.Navigation
{
    public sealed class NavigationServiceConfiguration
    {
        public NavigationServiceConfiguration()
        {
            Serializer = new NavigationSerializer();
            NavigationUriProvider = new NavigationUriProvider();
            ViewMappingProvider = new ViewMappingProvider();
        }

        [PublicAPI]
        public IViewMappingProvider ViewMappingProvider { get; set; }

        [PublicAPI]
        public ISerializer Serializer { get; set; }

        [PublicAPI]
        public INavigationUriProvider NavigationUriProvider { get; set; }
    }
}