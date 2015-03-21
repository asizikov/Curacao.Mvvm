using System;
using Curacao.Mvvm.ViewModel;
using JetBrains.Annotations;

namespace Curacao.Mvvm.Navigation
{
    public interface INavigationUriProvider
    {
        [NotNull]
        Uri Get<TViewModel>() where TViewModel : BaseViewModel;
    }
}