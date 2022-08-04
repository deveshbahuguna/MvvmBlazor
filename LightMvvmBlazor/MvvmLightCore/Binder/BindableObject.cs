using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightCore.Binder
{
    internal class BindableObject
    {
        public INotifyPropertyChanged ViewModel { get; set; }
        public PropertyInfo Property { get; set; }    
    }
}
