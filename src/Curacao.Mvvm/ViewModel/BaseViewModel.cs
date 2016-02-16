using System.ComponentModel;
using JetBrains.Annotations;

namespace Curacao.Mvvm.ViewModel
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private bool _isLoading;



        [NotifyPropertyChangedInvocator, PublicAPI]
        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {     
                SmartDispatcher.BeginInvoke(() => handler(this, new PropertyChangedEventArgs(propertyName)));
            }
        }

        [UsedImplicitly(ImplicitUseKindFlags.Access), PublicAPI]
        public bool IsLoading
        {
            get { return _isLoading; }
            protected set
            {
                if (value.Equals(_isLoading)) return;
                _isLoading = value;
                OnPropertyChanged("IsLoading");
            }
        }
    }
}