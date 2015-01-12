using System;
using System.Windows.Input;
using Curacao.Mvvm.Abstractions.Services;
using Curacao.Mvvm.Commands;
using Curacao.Mvvm.Navigation;
using Curacao.Mvvm.ViewModel;
using Curacao.Sample.App.Models;
using JetBrains.Annotations;

namespace Curacao.Sample.App.ViewModels
{
    internal sealed class MainViewModel : BaseViewModel
    {
        private NavigationService NavigationService { get; set; }

        public MainViewModel(ISystemDispatcher dispatcher, [NotNull] NavigationService navigationService)
            : base(dispatcher)
        {
            if (navigationService == null) throw new ArgumentNullException("navigationService");
            NavigationService = navigationService;
            Go = new RelayCommand(Navigate);
        }

        private void Navigate()
        {
            NavigationService.NavigateTo<Page1ViewModel, Data>(new Data{Id = 1, Value = "value"});
        }

        public ICommand Go { get; set; }
    }
}