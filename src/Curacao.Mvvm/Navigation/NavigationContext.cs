using System.Collections.Generic;

namespace Curacao.Mvvm.Navigation
{
    public class NavigationContext<TData>
    {
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }
        public TData Body { get; set; }
    }

    public class NavigationContext
    {
        public string From { get; set; }
        public IEnumerable<string> To { get; set; }

        public static NavigationContext Create(string from, IEnumerable<string> to)
        {
            return new NavigationContext {From = from, To = to};
        }

        public static NavigationContext<TData> Create<TData>(string from, IEnumerable<string> to, TData data)
        {
            return new NavigationContext<TData> {From = from, To = to, Body = data};
        }
    }
}