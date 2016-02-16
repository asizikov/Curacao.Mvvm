using System;
using Curacao.Mvvm.Abstractions.Services;
using JetBrains.Annotations;

namespace Curacao.Mvvm.ViewModel.Factories
{
    [PublicAPI]
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class GenericViewModelFactory<TViewModel> : IGenericViewModelFactory where TViewModel : BaseViewModel
    {
// ReSharper disable MemberCanBePrivate.Global
        protected readonly ISystemDispatcher Dispatcher;
// ReSharper restore MemberCanBePrivate.Global

        public GenericViewModelFactory([NotNull] ISystemDispatcher dispatcher)
        {
            if (dispatcher == null) throw new ArgumentNullException("dispatcher");
            Dispatcher = dispatcher;
        }

        public virtual BaseViewModel Create()
        {
            return (TViewModel) Activator.CreateInstance(typeof (TViewModel), Dispatcher);
        }

        public virtual TViewModel CreateSpecific()
        {
            return (TViewModel) Activator.CreateInstance(typeof (TViewModel), Dispatcher);
        }
    }
}