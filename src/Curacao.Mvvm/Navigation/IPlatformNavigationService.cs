using System;

namespace Curacao.Mvvm.Navigation
{
    public interface IPlatformNavigationService
    {
        void Navigate(Uri path);
    }
}