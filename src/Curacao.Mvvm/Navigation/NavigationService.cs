using System;
using System.Collections.Generic;
using System.Linq;
using Curacao.Mvvm.ViewModel;
using JetBrains.Annotations;

namespace Curacao.Mvvm.Navigation
{
    public class NavigationService
    {
        private IViewMappingProvider ViewMappingProvider { get; set; }
        private ISerializer Serializer { get; set; }
        private INavigationUriProvider NavigationUriProvider { get; set; }

        public NavigationService([NotNull] IViewMappingProvider viewMappingProvider, [NotNull] ISerializer serializer,
            [NotNull] INavigationUriProvider navigationUriProvider)
        {
            if (viewMappingProvider == null) throw new ArgumentNullException("viewMappingProvider");
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (navigationUriProvider == null) throw new ArgumentNullException("navigationUriProvider");
            ViewMappingProvider = viewMappingProvider;
            Serializer = serializer;
            NavigationUriProvider = navigationUriProvider;
        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            var viewModelType = typeof (TViewModel);
            var viewModelName = viewModelType.Name;
            var possibleMappings = ViewMappingProvider.GetPossibleMappings(viewModelName).ToList();
            if (!possibleMappings.Any())
            {
                throw new NavigationException("Can't find suitable mapping");
            }
            var uri = NavigationUriProvider.Get(possibleMappings);
            if (uri == null)
            {
                throw new NavigationException("Can't find suitable destination");
            }

            var navigationContext = new NavigationContext {From = viewModelName, To = possibleMappings};
            var serializedContext = Serializer.Serialize(navigationContext);
            var encoded = Base64Encode(serializedContext);

            NavigateInternal(new NavigationEvent
            {
                Context = encoded,
                Destination = uri
            });
        }

        private void NavigateInternal(NavigationEvent navigationEvent)
        {
            ;
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }
    }

    public interface INavigationUriProvider
    {
        Uri Get(IEnumerable<string> possibleDestinations);
    }

    internal class NavigationEvent
    {
        public string Context { get; set; }
        public Uri Destination { get; set; }
    }

    public class NavigationContext
    {
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
    }
}