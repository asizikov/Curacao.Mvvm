using System;
using JetBrains.Annotations;

namespace Curacao.Mvvm.Navigation.Mapping
{
    public interface IViewModelNameExtractor
    {
        [CanBeNull]
        string Extract([NotNull] Type viewModelType);
    }
}