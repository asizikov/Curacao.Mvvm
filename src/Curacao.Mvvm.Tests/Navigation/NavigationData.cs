using System.Collections.Generic;

namespace Curacao.Mvvm.Tests.Navigation
{
    public class NavigationData
    {
        public string MyProperty { get; set; }
        public int Id { get; set; }
        public IEnumerable<string> Elements { get; set; }
    }
}