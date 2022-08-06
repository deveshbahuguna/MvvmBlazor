using MvvmLightCore.Binder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightCore
{
    public class MvvmBinder : IDisposable, IMvvmBinder
    {
        private readonly IBindingManager bindingManager;

        public event PropertyChangedEventHandler? ViewModelPropertyChanged;

        public MvvmBinder(IBindingManager bindingManager)
        {
            this.bindingManager = bindingManager;
        }

        public TValue Bind<TInput, TValue>(INotifyPropertyChanged viewmodel, Expression<Func<TInput, TValue>> bindingExpression) where TInput : INotifyPropertyChanged
        {
            var bindableProperty = ParseBindingExpression(bindingExpression);
            PropertyInfo  bindableProperty = null;
            if (!this.bindingManager.ContainsBinding(viewmodel, bindableProperty.Name))
            {
                var bindableObj = new BindableObject();
                bindableObj.Property = bindableProperty;
                bindableObj.ViewModel = new WeakReference<INotifyPropertyChanged>(viewmodel);
                this.bindingManager.AddBinding(bindableObj);
            }
            return (TValue)bindableObj.Property.GetValue(viewmodel, null);
        }

        private PropertyInfo ParseBindingExpression<TInput, TValue>(Expression<Func<TInput, TValue>> bindingExpression) where TInput : INotifyPropertyChanged
        {
            if (bindingExpression.NodeType == ExpressionType.Lambda && bindingExpression.Body is MemberExpression && (bindingExpression.Body as MemberExpression).Member is PropertyInfo)
            {
                return ((bindingExpression.Body as MemberExpression).Member) as PropertyInfo;
            }
            throw new NotSupportedException();
        }

        public void Dispose()
        {

        }
    }
}
