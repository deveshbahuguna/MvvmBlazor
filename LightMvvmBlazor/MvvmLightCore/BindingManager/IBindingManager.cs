using System.ComponentModel;
using System.Reflection;

namespace MvvmLightCore.Binder
{
    public interface IBindingManager
    {
        void AddBinding(IBindableObject bindableObject);
        bool CheckIfBindingAlreadyExist(IBindableObject bindableObject);
        void RemoveBinding(IBindableObject bindableObject);
        PropertyInfo? GetBindableProperty(IBindableObject propertyInfo);

    }
}