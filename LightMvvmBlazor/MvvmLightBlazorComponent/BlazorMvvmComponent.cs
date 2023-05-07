using Microsoft.AspNetCore.Components;
using MvvmLightCore;
using System.ComponentModel;
using System.Linq.Expressions;

namespace MvvmLightBlazorComponent
{
    public abstract class BlazorMvvmComponent : ComponentBase
    {
        [Inject]
        public IMvvmBinder MvvmBinder { get; set; }
        
        protected TValue? Bind<TInput, TValue>(INotifyPropertyChanged viewmodel, Expression<Func<TInput, TValue?>> bindingExpression) where TInput : INotifyPropertyChanged
        {
            MvvmBinder.ViewModelPropertyChanged -= PropertyChangedEventHandler;
            MvvmBinder.ViewModelPropertyChanged += PropertyChangedEventHandler;
            return MvvmBinder.Bind(viewmodel, bindingExpression);
        }

        public void PropertyChangedEventHandler(object? sender, PropertyChangedEventArgs e)
        {
            InvokeAsync(()=>StateHasChanged());
        }

    }
}
