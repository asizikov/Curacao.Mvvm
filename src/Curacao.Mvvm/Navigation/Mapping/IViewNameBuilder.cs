using System.Collections.Generic;

namespace Curacao.Mvvm.Navigation.Mapping
{
    public interface IViewNameBuilder
    {
        IEnumerable<string> Build(string viewModelName);
    }
}