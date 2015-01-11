using System.Linq;
using Curacao.Mvvm.Abstractions.Services;
using Curacao.Mvvm.Navigation;
using Curacao.Mvvm.ViewModel;
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
//            new[] {"TestView", "TestPage"}
            var service = CreateService();
            ViewMappingProvider.Setup(provider => provider.GetPossibleMappings(It.IsAny<string>()))
                .Returns(Enumerable.Empty<string>);
            Assert.Throws<NavigationException>(service.NavigateTo<TestViewModel>);
        }

        private NavigationService CreateService()
        {
            return new NavigationService(ViewMappingProvider.Object, Serializer.Object, NavigationUriProvider.Object);
        }
    }

    public class TestViewModel : BaseViewModel
    {
        public TestViewModel(ISystemDispatcher dispatcher) : base(dispatcher)
        {
        }
    }
}