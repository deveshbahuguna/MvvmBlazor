using System.ComponentModel;
using System.Reflection;

namespace MvvmLightCore.Binder
{
    public class BindableObject : IEquatable<BindableObject>
    {
        public BindableObject(WeakReference<INotifyPropertyChanged> viewModel)
        {
            ViewModel = viewModel;
        }

        public WeakReference<INotifyPropertyChanged> ViewModel { get; set; }
        public bool CheckIfBindingKeyAreSame(BindableObject toCheckObject)
        {
            if (toCheckObject.ViewModel != null && toCheckObject.ViewModel.TryGetTarget(out INotifyPropertyChanged? keyObject)
             && this.ViewModel != null &&  this.ViewModel.TryGetTarget(out INotifyPropertyChanged? currentObjectVM))
            {                
                return keyObject.Equals(currentObjectVM);
            }
            return false;
        }

        public bool CheckIfBindingAlreadyExist(BindableObject toCheckObject)
        {
            return CheckIfBindingKeyAreSame(toCheckObject) && Property.Equals(toCheckObject.Property);
        }

        public int GetHashcode
        {
            get
            {
                if (this.ViewModel != null)
                {
                    INotifyPropertyChanged? vm;
                    return this.ViewModel.TryGetTarget(out vm).GetHashCode();
                }
                return -1;
            }
        }

        public PropertyInfo Property { get; set; }
        public bool Equals(BindableObject? other)
        {
            if (other == null) return false;
           return CheckIfBindingAlreadyExist(other);
        }
    }
}
