using System;
using System.Collections.Generic;
using System.Linq;
using Curacao.Mvvm.Navigation.Mapping;
using NUnit.Framework;

namespace Curacao.Mvvm.Tests.Navigation
{
    [TestFixture]
    public class ViewMappingProviderTest
    {
        [TestCase("TestViewModel", "TestView")]
        [TestCase("TestViewModel", "TestPage")]
        [TestCase("TestVM", "TestView")]
        [TestCase("TestVM", "TestPage")]
        public void MyMethod(string viewModelName, string viewName)
        {
            var viewMappingProvider = new ViewMappingProvider();
            var possibleMappings = viewMappingProvider.GetPossibleMappings(viewModelName);
            Assert.IsTrue(possibleMappings.Contains(viewName));
        }
    }
}