using System.Windows.Navigation;
using Curacao.Mvvm.Navigation;
using Curacao.Sample.App.Models;
using Microsoft.Phone.Controls;

namespace Curacao.Sample.App.View
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var navigationContext = NavigationContext.QueryString.RestoreContext<Data>();
        }
    }
}