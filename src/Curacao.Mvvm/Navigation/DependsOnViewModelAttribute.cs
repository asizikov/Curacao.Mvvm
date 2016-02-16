using System;

namespace Curacao.Mvvm.Navigation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class DependsOnViewModelAttribute : Attribute
    {
        public Type ViewModelType { get; }

        public DependsOnViewModelAttribute(Type viewModelType)
        {
            ViewModelType = viewModelType;
        }
    }
}