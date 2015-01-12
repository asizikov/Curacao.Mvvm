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
        private IPlatformNavigationService PlatformNavigationService { get; set; }
        private string Key { get; set; }

        public NavigationService([NotNull] IViewMappingProvider viewMappingProvider,
            [NotNull] INavigationUriProvider navigationUriProvider, IPlatformNavigationService platformNavigationService)
            : this(viewMappingProvider, new NavigationSerializer(), navigationUriProvider, platformNavigationService)
        {
        }

        public NavigationService([NotNull] IViewMappingProvider viewMappingProvider, [NotNull] ISerializer serializer,
            [NotNull] INavigationUriProvider navigationUriProvider, IPlatformNavigationService platformNavigationService)
        {
            if (viewMappingProvider == null) throw new ArgumentNullException("viewMappingProvider");
            if (serializer == null) throw new ArgumentNullException("serializer");
            if (navigationUriProvider == null) throw new ArgumentNullException("navigationUriProvider");
            ViewMappingProvider = viewMappingProvider;
            Serializer = serializer;
            NavigationUriProvider = navigationUriProvider;
            PlatformNavigationService = platformNavigationService;
            Key = "NavigationContext";
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

            var navigationContext = NavigationContext.Create(viewModelName, possibleMappings);
            var navigationEvent = BuildNavigationEvent(navigationContext, uri);
            NavigateInternal(navigationEvent);
        }


        public void NavigateTo<TViewModel, TData>(TData data) where TViewModel : BaseViewModel
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

            var navigationContext = NavigationContext.Create(viewModelName, possibleMappings, data);
            var navigationEvent = BuildNavigationEvent(navigationContext, uri);
            NavigateInternal(navigationEvent);
        }

        private NavigationEvent BuildNavigationEvent<TData>(NavigationContext<TData> navigationContext, Uri uri)
        {
            var serializedContext = Serializer.Serialize(navigationContext);
            var encoded = Base64Encode(serializedContext);

            var navigationEvent = new NavigationEvent
            {
                Context = encoded,
                Destination = uri
            };
            return navigationEvent;
        }


        private NavigationEvent BuildNavigationEvent(NavigationContext navigationContext, Uri uri)
        {
            var serializedContext = Serializer.Serialize(navigationContext);
            var encoded = Base64Encode(serializedContext);

            var navigationEvent = new NavigationEvent
            {
                Context = encoded,
                Destination = uri
            };
            return navigationEvent;
        }

        private void NavigateInternal(NavigationEvent navigationEvent)
        {
            var uriString = string.Format("{0}?{1}={2}",navigationEvent.Destination.OriginalString,Key, navigationEvent.Context);
            var path = new Uri(uriString, UriKind.Relative);
            PlatformNavigationService.Navigate(path);
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }

    public static class NavigationQueryExtension
    {
        private static string Key = "NavigationContext";

        public static NavigationContext RestoreContext(this IDictionary<string, string> query)
        {
            string encodedContext;
            if (query.TryGetValue(Key, out encodedContext))
            {
                var json = Base64Decode(encodedContext);
                var navigationSerializer = new NavigationSerializer();
                return navigationSerializer.Deserialize<NavigationContext>(json);
            }
            throw new Exception("Can't restore context");
        }

        public static NavigationContext<TData> RestoreContext<TData>(this IDictionary<string, string> query)
        {
            string encodedContext;
            if (query.TryGetValue(Key, out encodedContext))
            {
                var json = Base64Decode(encodedContext);
                var navigationSerializer = new NavigationSerializer();
                return navigationSerializer.Deserialize<NavigationContext<TData>>(json);
            }
            throw new Exception("Can't restore context");
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }
    }
}