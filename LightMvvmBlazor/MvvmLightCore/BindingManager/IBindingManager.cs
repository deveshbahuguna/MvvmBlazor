using System.ComponentModel;
using System.Reflection;

namespace MvvmLightCore.Binder
{
    public interface IBindingManager
    {
        void AddBinding(BindableObject bindableObject);
        bool ContainsBinding(BindableObject bindableObject);
        void RemoveBinding(BindableObject bindableObject);
        PropertyInfo? GetBindableProperty(BindableObject propertyInfo);

    }
}