using System.ComponentModel;
using System.Reflection;
using MvvmLightCore.Binder;

namespace MvvmLightCore.BindingMapper
{
    public interface IBindingMap
    {
        void AddBinding(BindableObject bindableObject);
        bool CheckIfBindingAlreadyExist(BindableObject bindableObject);
    }
}