using System;
using System.Collections.Generic;
using System.Linq;
using Curacao.Mvvm.Navigation;
using Curacao.Mvvm.Navigation.Mapping;
using Curacao.Mvvm.Navigation.Serialization;
using Moq;
using NUnit.Framework;

namespace Curacao.Mvvm.Tests.Navigation
{
    [TestFixture]
    public class NavigationServiceTest
    {
        private Mock<IViewMappingProvider> ViewMappingProvider { get; set; }
        private Mock<ISerializer> Serializer { get; set; }
        private Mock<INavigationUriProvider> NavigationUriProvider { get; set; }

        [SetUp]
        public void SetUp()
        {
            ViewMappingProvider = new Mock<IViewMappingProvider>();
            Serializer = new Mock<ISerializer>();
            NavigationUriProvider = new Mock<INavigationUriProvider>();
        }

        [Test]
        public void NavigateThrowsWhenNoMappingGiven()
        { 
            var service = CreateService(new TestPlatformNavigationServiceImplementation());
            ViewMappingProvider.Setup(provider => provider.GetPossibleMappings(It.IsAny<string>()))
                .Returns(Enumerable.Empty<string>);
            Assert.Throws<NavigationException>(service.NavigateTo<TestViewModel>);
        }

        [Test]
        public void NavigateEncodesContext()
        {
            var navigationServiceImplementation = new TestPlatformNavigationServiceImplementation();
            var service = CreateService(navigationServiceImplementation);
            ViewMappingProvider.Setup(provider => provider.GetPossibleMappings(It.IsAny<string>()))
                .Returns(new[] { "TestView", "TestPage" });
            NavigationUriProvider.Setup(provider => provider.Get(It.IsAny<IEnumerable<string>>()))
                .Returns(new Uri("/View/MainPage.xaml", UriKind.Relative));
            service.NavigateTo<TestViewModel, NavigationData>(new NavigationData
            {
                Elements = new List<string> { "one"},
                Id = 123,
                MyProperty = "property"
            });
            var uri = navigationServiceImplementation.Uri;
        }



        private NavigationService CreateService(IPlatformNavigationService platformNavigationService)
        {
            return new NavigationService(ViewMappingProvider.Object, NavigationUriProvider.Object, platformNavigationService);
        }
    }
}