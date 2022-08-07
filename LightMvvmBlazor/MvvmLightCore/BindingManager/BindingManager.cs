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
        /// <summary>
        /// Mapping of VM with object id and object VM with corresponding property.
        /// </summary>
        private readonly Dictionary<int, Dictionary<WeakReference<INotifyPropertyChanged>, HashSet<PropertyInfo>>> propVmMapping;
        public BindingManager()
        {
            propVmMapping = new Dictionary<int, Dictionary<WeakReference<INotifyPropertyChanged>, HashSet<PropertyInfo>>>();
        }

        public void AddBinding(BindableObject bindableObject)
        {
            if (bindableObject.ViewModel != null && bindableObject.Property != null)
            {
                if (!propVmMapping.ContainsKey(bindableObject.GetHashCode()))
                {
                    propVmMapping.Add(bindableObject.GetHashCode(), new Dictionary<WeakReference<INotifyPropertyChanged>, HashSet<PropertyInfo>>());
                    propVmMapping[bindableObject.ViewModel.GetHashCode()].Add(bindableObject.ViewModel, new HashSet<PropertyInfo>());
                    propVmMapping[bindableObject.ViewModel.GetHashCode()][bindableObject.ViewModel].Add(bindableObject.Property);
                }
                else
                {
                    //If prop and vm already exist
                    if (CheckBindableObjectAlreadyExist(bindableObject))
                    {
                        return;
                    }
                    else
                    {
                        //Update the prop info in the view model.
                        var vm = this.propVmMapping[bindableObject.GetHashcode][bindableObject.ViewModel];
                        vm.Add(bindableObject.Property);
                    }

                }
            }
        }

        private bool CheckBindableObjectAlreadyExist(BindableObject bindableObject)
        {
            if (propVmMapping.ContainsKey(bindableObject.GetHashcode) && bindableObject.ViewModel != null && bindableObject.Property != null)
            {
                return propVmMapping[bindableObject.GetHashcode].ContainsKey(bindableObject.ViewModel)
                        &&
                       propVmMapping[bindableObject.GetHashcode][bindableObject.ViewModel].Contains(bindableObject.Property);
            }
            return false;
        }

        public bool ContainsBinding(BindableObject bindableObject)
        {
            if (bindableObject != null)
            {
                return CheckBindableObjectAlreadyExist(bindableObject);
            }
            return false;
        }

        public void RemoveBinding(BindableObject bindableObject)
        {
            //Will do later.
            //if (bindableObject.ViewModel != null && propVmMapping.ContainsKey(bindableObject.ViewModel)
            //    && propVmMapping[bindableObject.ViewModel].ContainsKey(bindableObject.Property))
            //{
            //    propVmMapping[bindableObject.ViewModel].Remove(bindableObject.Property);
            //    if (propVmMapping[bindableObject.ViewModel].Count() == 0)
            //    {
            //        propVmMapping.Remove(bindableObject.ViewModel);
            //    }
            //}
        }

        public PropertyInfo? GetBindableProperty(BindableObject bindableObject)
        {
            if (bindableObject.ViewModel != null && bindableObject.Property != null &&
                propVmMapping.ContainsKey(bindableObject.GetHashcode) &&
                propVmMapping[bindableObject.GetHashcode].ContainsKey(bindableObject.ViewModel) &&
                propVmMapping[bindableObject.GetHashcode][bindableObject.ViewModel].Contains(bindableObject.Property)
                )
            {
                PropertyInfo? property = null;
                propVmMapping[bindableObject.GetHashcode][bindableObject.ViewModel].TryGetValue(bindableObject.Property,
                    out property);
                return property;
            }
            throw new KeyNotFoundException($"Bindable object does not exist " + bindableObject.GetHashcode + "Property " + bindableObject.GetPropName);
        }
    }
}
