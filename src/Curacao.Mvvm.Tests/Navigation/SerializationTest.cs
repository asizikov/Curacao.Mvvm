using System;
using Curacao.Mvvm.Navigation;
using Curacao.Mvvm.Navigation.Serialization;
using NUnit.Framework;

namespace Curacao.Mvvm.Tests.Navigation
{
    [TestFixture]
    public class SerializationTest
    {
        private ISerializer Serializer { get; set; }

        [SetUp]
        public void Setup()
        {
            Serializer = new NavigationSerializer();
        }

        [TestCase("aaadfdfasdf")]
        [TestCase("привет")]
        [TestCase("прив ет")]
        public void CanSerializeAndDeserialize(string input)
        {
            var text = input;
            var navigationContext = NavigationContext.Create("to", new MyClass {Text = text});

            var serializedContext = Serializer.Serialize(navigationContext);
            var encoded = Base64Encode(serializedContext);

            var decode = Base64Decode(encoded);

            var context = Serializer.Deserialize<NavigationContext<MyClass>>(decode);

            StringAssert.AreEqualIgnoringCase(text, context.Body.Text);
        }

        private string Decode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        private static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes, 0, base64EncodedBytes.Length);
        }
    }

    public class MyClass
    {
        public string Text { get; set; }
    }
}