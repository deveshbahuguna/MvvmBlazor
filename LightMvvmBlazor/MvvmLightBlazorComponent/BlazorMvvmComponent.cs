using Microsoft.AspNetCore.Components;
using MvvmLightCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightBlazorComponent
{
    public abstract class BlazorMvvmComponent : ComponentBase
    {
        private readonly IMvvmBinder mvvmBinder;

        public BlazorMvvmComponent(IMvvmBinder mvvmBinder)
        {
            this.mvvmBinder = mvvmBinder;
        }

        protected TValue Bind<TInput, TValue>(INotifyPropertyChanged viewmodel, Expression<Func<TInput, TValue>> bindingExpression) where TInput : INotifyPropertyChanged
        {
            this.mvvmBinder.ViewModelPropertyChanged -= PropertyChangedEventHandler;
            this.mvvmBinder.ViewModelPropertyChanged += PropertyChangedEventHandler;
            return this.mvvmBinder.Bind(viewmodel, bindingExpression);
        }

        public void PropertyChangedEventHandler(object? sender, PropertyChangedEventArgs e)
        {
            this.InvokeAsync(()=>this.StateHasChanged());
        }

    }
}
