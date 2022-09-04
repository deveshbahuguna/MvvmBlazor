using Microsoft.AspNetCore.Components;
using MvvmLightCore;
using System.ComponentModel;
using System.Linq.Expressions;

namespace MvvmLightBlazorComponent
{
    public abstract class BlazorMvvmComponent : ComponentBase
    {
        [Inject]
        public IMvvmBinder mvvmBinder { get; set; }

        //public BlazorMvvmComponent()
        //{
        //    //this.mvvmBinder = mvvmBinder;
        //}

        protected TValue? Bind<TInput, TValue>(INotifyPropertyChanged viewmodel, Expression<Func<TInput, TValue?>> bindingExpression) where TInput : INotifyPropertyChanged
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
