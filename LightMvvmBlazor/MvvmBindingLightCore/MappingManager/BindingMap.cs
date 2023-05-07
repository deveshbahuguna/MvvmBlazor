using System.Reflection;
using MvvmLightCore.Binder;
using MvvmLightCore.Exceptions;

namespace MvvmLightCore.BindingMapper
{
    public class BindingMap : IBindingMap
    {
        /// <summary>
        /// Mapping of VM with object id and object VM with corresponding property.
        /// </summary>
        private readonly Dictionary<int, List<BindableObject>> _objIdBindableObjMap;

        public BindingMap()
        {
            _objIdBindableObjMap = new Dictionary<int, List<BindableObject>>();
        }

        public void AddBinding(BindableObject toAddBindingObj)
        {
            if (toAddBindingObj.Property == null) throw new NullBindPropertyFound(toAddBindingObj);
            if (toAddBindingObj.ViewModel == null) throw new ViewModelObjNotFoundException(toAddBindingObj);
            if (!_objIdBindableObjMap.ContainsKey(toAddBindingObj.GetHashcode))
            {
                var bindingObj = new BindableObject(toAddBindingObj.ViewModel);
                _objIdBindableObjMap.Add(toAddBindingObj.GetHashcode, new List<BindableObject>());
                _objIdBindableObjMap[toAddBindingObj.GetHashcode].Add(bindingObj);
            }
            else
            {
                //If prop and vm already exist
                if (CheckIfBindingAlreadyExist(toAddBindingObj))
                {
                    return;
                }
                else
                {
                    //Update the prop info in the view model.
                    var existingViewModel = _objIdBindableObjMap[toAddBindingObj.GetHashcode]
                        .Where(x=>x.CheckIfBindingKeyAreSame(toAddBindingObj)).FirstOrDefault();
                    existingViewModel.Property = toAddBindingObj.Property;
                }
            }
        }

        public bool CheckIfBindingAlreadyExist(BindableObject toFindBindObject)
        {
            // Below checks for objects having same id as well as same object if id are same.
            return this._objIdBindableObjMap.ContainsKey(toFindBindObject.GetHashcode) &&
                   this._objIdBindableObjMap[toFindBindObject.GetHashcode]
                       .Any(obj => obj.Equals(toFindBindObject));
        }

        public void RemoveBinding(BindableObject IBindableObject)
        {
            //todo:db Will do later.
            //if (IBindableObject.ViewModel != null && propVmMapping.ContainsKey(IBindableObject.ViewModel)
            //    && propVmMapping[IBindableObject.ViewModel].ContainsKey(IBindableObject.Property))
            //{
            //    propVmMapping[IBindableObject.ViewModel].Remove(IBindableObject.Property);
            //    if (propVmMapping[IBindableObject.ViewModel].Count() == 0)
            //    {
            //        propVmMapping.Remove(IBindableObject.ViewModel);
            //    }
            //}
        }
    }

    public class NullBindPropertyFound : Exception
    {
        private readonly BindableObject _toAddBindingObj;

        public NullBindPropertyFound(BindableObject toAddBindingObj) : base(
            message: $"Property of Bindable object  is null")

        {
            _toAddBindingObj = toAddBindingObj;
        }
    }
}