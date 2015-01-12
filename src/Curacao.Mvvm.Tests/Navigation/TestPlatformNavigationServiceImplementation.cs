using System;
using Curacao.Mvvm.Navigation;

namespace Curacao.Mvvm.Tests.Navigation
{
    public class TestPlatformNavigationServiceImplementation : IPlatformNavigationService
    {
        public  Uri Uri { get; set; }
        public void Navigate(Uri path)
        {
            Uri = path;
        }
    }
}