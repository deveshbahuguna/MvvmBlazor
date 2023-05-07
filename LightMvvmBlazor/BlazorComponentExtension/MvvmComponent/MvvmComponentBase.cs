using Microsoft.AspNetCore.Components;
using System.ComponentModel;
using System.Linq.Expressions;
using MvvmLightCore.BindingNotifications;
using MvvmLightCore.Bindings;
using IMvvmBinder = MvvmLightCore.IMvvmBinder;

namespace BlazorComponentExtension.MvvmComponent
{
    
    public abstract class MvvmComponentBase : ComponentBase, IBinder
    {
        [Inject]
        private IMvvmBinder MvvmBinder { get; set; }

        private BindingNotification _blazorMvvmNotificationHandler;
        public MvvmComponentBase()
        {
            _blazorMvvmNotificationHandler = new(PropertyChangedEventHandler, CollectionChangeEventHandler);
        }
        
        
        /// <summary>
        /// This method can be overriden thus the blazor components can override and handle event as per their need.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void PropertyChangedEventHandler(object? sender, PropertyChangedEventArgs e)
        {
            InvokeAsync(()=>StateHasChanged());
        }

        protected virtual void CollectionChangeEventHandler(object? sender, CollectionChangeEventArgs e)
        {
            //todo: will add code later
        }

        public TValue Bind<TInput, TValue>(INotifyPropertyChanged viewmodel, Expression<Func<TInput, TValue>> bindingExpression) where TInput : INotifyPropertyChanged
        {
           return MvvmBinder.Bind(viewmodel, bindingExpression);
        }
    }
}
