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
        public WeakReference<INotifyPropertyChanged>? ViewModel { get; set; }
        public PropertyInfo Property { get; set; }    
    }
}
