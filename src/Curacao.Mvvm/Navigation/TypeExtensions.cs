using System;

namespace Curacao.Mvvm.Navigation
{
    public static class TypeExtensions
    {
        public static Uri GetUri(this Type viewType)
        {
            var assembly = viewType.Assembly;
            var name = assembly.GetName().Name;
            var uri = viewType.FullName.Replace(name, string.Empty).Replace(".", "/");
            return new Uri($"/{name};component{uri}.xaml", UriKind.Relative);
        }
    }
}