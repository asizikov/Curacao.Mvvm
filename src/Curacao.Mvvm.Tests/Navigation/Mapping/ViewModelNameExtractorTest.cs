using System;
using Curacao.Mvvm.Navigation.Mapping;
using NUnit.Framework;

namespace Curacao.Mvvm.Tests.Navigation.Mapping
{
    [TestFixture]
    public class ViewModelNameExtractorTest
    {
        [Test]
        public void ExtractsNameWhenCorrectViewModelTypeGiven()
        {
            var nameExtractor = new ViewModelNameExtractor();
            var result = nameExtractor.Extract(typeof(TestViewModel));
            StringAssert.AreEqualIgnoringCase("Test", result);
        }

        [Test]
        public void ReturnsNullWhenIncorrectViewModelTyeGiven()
        {
            var nameExtractor = new ViewModelNameExtractor();
            var result = nameExtractor.Extract(typeof(string));
            Assert.IsNullOrEmpty(result);
        }

        [Test]
        public void ThrowsExceptionWhenNullGiven()
        {
            var nameExtractor = new ViewModelNameExtractor();
// ReSharper disable once AssignNullToNotNullAttribute
            Assert.Throws<ArgumentNullException>(() => nameExtractor.Extract(null));
        }
    }
}
