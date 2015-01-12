using Curacao.Mvvm.Abstractions.Services;
using Curacao.Mvvm.ViewModel;

namespace Curacao.Mvvm.Tests.Navigation
{
    public sealed class TestViewModel : BaseViewModel
    {
        public TestViewModel(ISystemDispatcher dispatcher) : base(dispatcher)
        {
        }
    }
}