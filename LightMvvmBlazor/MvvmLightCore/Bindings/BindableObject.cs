using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightCore.Binder
{
    public class BindableObject
    {
        public BindableObject(WeakReference<INotifyPropertyChanged>? viewModel, PropertyInfo? property)
        {
            ViewModel = viewModel;
            Property = property;
        }

        public WeakReference<INotifyPropertyChanged>? ViewModel { get; set; }
        public PropertyInfo? Property { get; set; }

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

        public string GetPropName
        {
            get
            {
                if(this.Property != null)
                    return this.Property.Name;
                return string.Empty;
            }
        }

    }
}
