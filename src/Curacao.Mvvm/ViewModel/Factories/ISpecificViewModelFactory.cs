using JetBrains.Annotations;

namespace Curacao.Mvvm.ViewModel.Factories
{
    public interface ISpecificViewModelFactory<out T> where T : BaseViewModel
    {
        [NotNull, PublicAPI] T Create();
    }
}