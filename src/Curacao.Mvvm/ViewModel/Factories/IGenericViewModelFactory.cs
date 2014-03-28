using JetBrains.Annotations;

namespace Curacao.Mvvm.ViewModel.Factories
{
    public interface IGenericViewModelFactory
    {
        [NotNull, PublicAPI] BaseViewModel Create();
    }
}