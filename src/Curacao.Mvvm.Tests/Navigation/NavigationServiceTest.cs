using System;
using System.Collections.Generic;
using Curacao.Mvvm.Navigation;
using Moq;
using NUnit.Framework;

namespace Curacao.Mvvm.Tests.Navigation
{
    [TestFixture]
    public class NavigationServiceTest
    {
        private Mock<INavigationUriProvider> NavigationUriProvider { get; set; }

        [SetUp]
        public void SetUp()
        {
            NavigationUriProvider = new Mock<INavigationUriProvider>();
        }

        [Test]
        public void NavigateEncodesContext()
        {
            var navigationServiceImplementation = new TestPlatformNavigationServiceImplementation();
            var service = CreateService(navigationServiceImplementation);
            NavigationUriProvider.Setup(provider => provider.Get<TestViewModel>())
                .Returns(new Uri("/View/MainPage.xaml", UriKind.Relative));
            service.NavigateTo<TestViewModel, NavigationData>(new NavigationData
            {
                Elements = new List<string> {"one"},
                Id = 123,
                MyProperty = "property"
            });
            var uri = navigationServiceImplementation.Uri;
        }


        private NavigationService CreateService()
        {
            return CreateService(new TestPlatformNavigationServiceImplementation());
        }

        private NavigationService CreateService(IPlatformNavigationService platformNavigationService)
        {
            var configuration = new NavigationServiceConfiguration
            {
                NavigationUriProvider = NavigationUriProvider.Object,
            };
            return new NavigationService(configuration, platformNavigationService);
        }
    }

    public class MyClass
    {
        public string Text { get; set; }
    }
}