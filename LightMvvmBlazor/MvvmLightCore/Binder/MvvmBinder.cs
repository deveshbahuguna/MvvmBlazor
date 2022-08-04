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
    public class MvvmBinder
    {
        public TValue Bind<TInput, TValue>( INotifyPropertyChanged viewmodel,Expression<Func<TInput, TValue>> bindingExpression) where TInput : INotifyPropertyChanged
        {
            BindableObject bindableObj = new BindableObject();
            bindableObj.ViewModel = viewmodel;
            bindableObj.Property = ParseBindingExpression(bindingExpression);
            bindableObj.ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            INotifyPropertyChanged bindableVM = bindableObj.ViewModel;
            return (TValue)bindableObj.Property.GetValue(bindableObj.ViewModel, null) ;

        }

        private void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            
        }

        private PropertyInfo ParseBindingExpression<TInput, TValue>(Expression<Func<TInput, TValue>> bindingExpression) where TInput : INotifyPropertyChanged
        {
            if (bindingExpression.NodeType == ExpressionType.Lambda && bindingExpression.Body is MemberExpression)
            {
                return ((bindingExpression.Body as MemberExpression).Member) as PropertyInfo;
            }
            throw new NotSupportedException();
        }
    }
}
