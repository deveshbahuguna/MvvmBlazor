using System.ComponentModel;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using MvvmLightCore.Bindings;

namespace MvvmLightCore.BindingNotifications;

public  class BindingNotification
{
    private readonly PropertyChangedEventHandler _propertyChangedEventHandler;
    private readonly CollectionChangeEventHandler _collectionChangeEventHandler;

    [Inject]
    public IMvvmBinder MvvmBinder { get; set; }

    public BindingNotification(PropertyChangedEventHandler propertyChangedEventHandler,CollectionChangeEventHandler collectionChangeEventHandler )
    {
        _propertyChangedEventHandler = propertyChangedEventHandler;
        _collectionChangeEventHandler = collectionChangeEventHandler;
    }
        
    protected TValue? Bind<TInput, TValue>(INotifyPropertyChanged viewmodel, Expression<Func<TInput, TValue?>> bindingExpression) where TInput : INotifyPropertyChanged
    {
        MvvmBinder.ViewModelPropertyChanged -= _propertyChangedEventHandler;
        MvvmBinder.ViewModelPropertyChanged += _propertyChangedEventHandler;
        MvvmBinder.OnCollectionChanged -= _collectionChangeEventHandler;
        MvvmBinder.OnCollectionChanged += _collectionChangeEventHandler;
        return MvvmBinder.Bind(viewmodel, bindingExpression);
    }
}