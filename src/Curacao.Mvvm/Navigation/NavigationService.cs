using System;
using System.Linq;
using Curacao.Mvvm.Navigation.Mapping;
using Curacao.Mvvm.Navigation.Serialization;
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

        public NavigationService([NotNull] NavigationServiceConfiguration configuration,
            [NotNull] IPlatformNavigationService platformNavigationService)
        {
            if (configuration == null) throw new ArgumentNullException("configuration");
            if (platformNavigationService == null) throw new ArgumentNullException("platformNavigationService");

            ViewMappingProvider = configuration.ViewMappingProvider;
            Serializer = configuration.Serializer;
            NavigationUriProvider = configuration.NavigationUriProvider;
            PlatformNavigationService = platformNavigationService;
            Key = "NavigationContext";
        }

        [PublicAPI]
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

        [PublicAPI]
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
            var uriString = string.Format("{0}?{1}={2}", navigationEvent.Destination.OriginalString, Key,
                navigationEvent.Context);
            var path = new Uri(uriString, UriKind.Relative);
            PlatformNavigationService.Navigate(path);
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
    }
}