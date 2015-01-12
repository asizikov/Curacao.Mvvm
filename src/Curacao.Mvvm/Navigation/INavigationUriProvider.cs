using System;
using System.Collections.Generic;

namespace Curacao.Mvvm.Navigation
{
    public interface INavigationUriProvider
    {
        Uri Get(IEnumerable<string> possibleDestinations);
    }
}