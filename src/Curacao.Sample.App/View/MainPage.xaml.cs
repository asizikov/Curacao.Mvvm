using System.Windows.Navigation;
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
            DataContext = ViewModelLocator.GetMainViewModel();
        }
    }
}