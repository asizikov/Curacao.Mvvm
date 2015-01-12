using System.Windows.Navigation;
using Curacao.Phone.Toolkit.Services;
using Curacao.Sample.App.ViewModels;
using Microsoft.Phone.Controls;

namespace Curacao.Sample.App.View
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            DataContext = new MainViewModel(new SystemDispatcher(),
                new Curacao.Mvvm.Navigation.NavigationService(null, null, null));
        }
    }
}