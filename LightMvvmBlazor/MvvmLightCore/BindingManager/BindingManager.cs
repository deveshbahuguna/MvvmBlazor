using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightCore.Binder
{
    internal class BindingManager : IBindingManager
    {
        private readonly Dictionary<WeakReference<INotifyPropertyChanged>, HashSet<PropertyInfo>> propVmMapping;
        private readonly Dictionary<int, bool> vmExistMapping;
        public BindingManager()
        {
            propVmMapping = new Dictionary<WeakReference<INotifyPropertyChanged>, HashSet<PropertyInfo>>();
            vmExistMapping = new Dictionary<int, bool>();
        }

        public void AddBinding(BindableObject bindableObject)
        {
            if (bindableObject.ViewModel != null && !propVmMapping.ContainsKey(bindableObject.ViewModel))
            {
                propVmMapping.Add(bindableObject.ViewModel, new HashSet<PropertyInfo>());
            }
            if (!propVmMapping[bindableObject.ViewModel].Contains(bindableObject.Property))
            {
                propVmMapping[bindableObject.ViewModel].Add(bindableObject.Property);
            }
        }

        public bool ContainsBinding(BindableObject bindableObject)
        {
            if (bindableObject != null)
            {
                return propVmMapping.ContainsKey(bindableObject);
            }
            return false;
        }

        public void RemoveBinding(BindableObject bindableObject)
        {
            if (bindableObject.ViewModel != null && propVmMapping.ContainsKey(bindableObject.ViewModel)
                && propVmMapping[bindableObject.ViewModel].ContainsKey(bindableObject.Property))
            {
                propVmMapping[bindableObject.ViewModel].Remove(bindableObject.Property);
                if (propVmMapping[bindableObject.ViewModel].Count() == 0)
                {
                    propVmMapping.Remove(bindableObject.ViewModel);
                }
            }
        }
    }
}
