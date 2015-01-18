using System;
using System.Collections.Generic;
using System.Reflection;

namespace Curacao.Mvvm.Navigation
{
    internal class NavigationUriProvider : INavigationUriProvider
    {
        public Uri Get(IEnumerable<string> possibleDestinations)
        {
            var assemblies = LoadAssemblies();

        }

        private IEnumerable<Assembly> LoadAssemblies()
        {
            throw new NotImplementedException();
        }


        private Uri GetUri(Type viewType,  UriKind uriKind)
        {
            var assembly = viewType.Assembly;
            var name = assembly.FullName;
            var uri = viewType.FullName.Replace(name, string.Empty).Replace(".", "/");
            return new Uri(string.Format("/{0};component{1}.xaml", name, uri), uriKind);
        }
    }
}