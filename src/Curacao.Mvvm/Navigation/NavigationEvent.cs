using System;

namespace Curacao.Mvvm.Navigation
{
    internal class NavigationEvent
    {
        public string Context { get; set; }
        public Uri Destination { get; set; }
    }
}