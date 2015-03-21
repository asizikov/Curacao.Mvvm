namespace Curacao.Mvvm.ViewModel
{
    public abstract class PageViewModel : BaseViewModel
    {
    }

    public abstract class PageViewModel<TData> : BaseViewModel
    {
        public abstract void Initialize(TData navigationParameter);
    }
}