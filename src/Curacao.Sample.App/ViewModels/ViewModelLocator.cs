﻿using System;
using Curacao.Mvvm.Navigation;
using Curacao.Phone.Toolkit.Services;

namespace Curacao.Sample.App.ViewModels
{
    internal static class ViewModelLocator
    {
        internal static MainViewModel GetMainViewModel()
        {
            return new MainViewModel(new SystemDispatcher(),
                NavigationService());
        }

        private static NavigationService NavigationService()
        {
            var configuration = new NavigationServiceConfiguration();
            return new NavigationService(configuration, new PlatformNavigation());
        }
    }

    public class PlatformNavigation : IPlatformNavigationService
    {
        public void Navigate(Uri path)
        {
            App.RootFrame.Navigate(path);
        }
    }
}
