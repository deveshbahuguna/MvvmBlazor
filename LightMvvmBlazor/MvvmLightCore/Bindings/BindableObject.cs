using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightCore.Binder
{
    public class BindableObject : IBindableObject
    {
        public BindableObject(WeakReference<INotifyPropertyChanged>? viewModel)
        {
            ViewModel = viewModel;
        }

        public WeakReference<INotifyPropertyChanged>? ViewModel { get; set; }
        public HashSet<PropertyInfo?> Properties { get; set; } = new();

        public bool CheckIfBindingKeyAreSame(IBindableObject toCheckObject)
        {
            if (toCheckObject.ViewModel != null && toCheckObject.ViewModel.TryGetTarget(out INotifyPropertyChanged? keyObject)
             && this.ViewModel != null &&  this.ViewModel.TryGetTarget(out INotifyPropertyChanged? currentObjectVM))
            {                
                return keyObject.Equals(currentObjectVM);
            }
            return false;
        }

        public bool CheckIfBindingAlreadyExist(IBindableObject toCheckObject)
        {
            return CheckIfBindingKeyAreSame(toCheckObject) && this.Properties.Contains(toCheckObject.Properties.First());
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
    }
}
