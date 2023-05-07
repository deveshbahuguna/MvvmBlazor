using System.ComponentModel;
using System.Linq.Expressions;

namespace MvvmLightCore.Bindings
{
    public interface IBinder
    {
        /// <summary>
        /// Method to bind any control to subscribe UI change via Notify property change.
        /// </summary>
        /// <typeparam name="TInput">Input parameter type</typeparam>
        /// <typeparam name="TValue">Return value type</typeparam>
        /// <param name="viewmodel"></param>
        /// <param name="bindingExpression"></param>
        /// <returns></returns>
        TValue Bind<TInput, TValue>(INotifyPropertyChanged viewmodel,
            Expression<Func<TInput, TValue>> bindingExpression) where TInput : INotifyPropertyChanged;

    }

    public interface IMvvmBinder : IAsyncDisposable, IDisposable,IBinder
    {
        /// <summary>
        /// Event to update UI on property change. 
        /// </summary>
        event PropertyChangedEventHandler ViewModelPropertyChanged;

        /// <summary>
        /// Event to update UI on any add or update happen to collection.
        /// </summary>
        event CollectionChangeEventHandler OnCollectionChanged;
        
     }
}