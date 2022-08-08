using System.ComponentModel;
using System.Reflection;

namespace MvvmLightCore.Binder
{
    public interface IBindableObject
    {
        int GetHashcode { get; }
        HashSet<PropertyInfo?> Properties { get; set; }
        WeakReference<INotifyPropertyChanged>? ViewModel { get; set; }
        bool CheckIfBindingKeyAreSame(IBindableObject toCheckObject);
        bool CheckIfBindingAlreadyExist(IBindableObject toCheckObject);  

    }
}