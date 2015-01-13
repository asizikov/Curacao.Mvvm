using System.Collections.Generic;
using JetBrains.Annotations;

namespace Curacao.Mvvm.Navigation.Mapping
{
    public interface IViewMappingProvider
    {
        [NotNull]
        IEnumerable<string> GetPossibleMappings([NotNull] string name);
    }
}