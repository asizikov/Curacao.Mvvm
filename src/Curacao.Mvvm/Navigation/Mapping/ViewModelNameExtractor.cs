using System.Collections.Generic;
using System.Linq;

namespace Curacao.Mvvm.Navigation.Mapping
{
    internal class ViewModelNameExtractor : IViewModelNameExtractor
    {
        private ICollection<string> ViewModelSuffixes { get; set; }

        public ViewModelNameExtractor()
        {
            ViewModelSuffixes = new HashSet<string>(new[] { "ViewModel", "VM" });
        }

        public string Extract(string name)
        {
            var viewModelName = string.Empty;
            foreach (var viewModelSuffix in ViewModelSuffixes.Where(name.EndsWith))
            {
                viewModelName = name.Substring(0, name.Length - viewModelSuffix.Length);
                break;
            }
            return viewModelName;
        }
    }
}