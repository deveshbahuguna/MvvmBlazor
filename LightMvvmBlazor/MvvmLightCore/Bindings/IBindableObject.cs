using System.ComponentModel;
using System.Reflection;

namespace MvvmLightCore.Binder
{
    public interface IBindableObject
    {
        int GetHashcode { get; }
        PropertyInfo Property { get; set; }
        WeakReference<INotifyPropertyChanged>? ViewModel { get; set; }
        bool CheckIfBindingKeyAreSame(IBindableObject toCheckObject);
        bool CheckIfBindingAlreadyExist(IBindableObject toCheckObject);  

    }
}