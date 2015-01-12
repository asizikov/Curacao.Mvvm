using Curacao.Mvvm.Abstractions.Services;
using Curacao.Mvvm.ViewModel;
using JetBrains.Annotations;

namespace Curacao.Sample.App.ViewModels
{
    internal sealed class Page1ViewModel : BaseViewModel
    {
        public Page1ViewModel([NotNull] ISystemDispatcher dispatcher) : base(dispatcher)
        {
        }
    }
}