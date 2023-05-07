using MvvmLightCore.Binder;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Extensions.Logging;
using MvvmLightCore.Bindings;

namespace MvvmLightCore
{
    public class MvvmBinder : IMvvmBinder
    {
        private readonly IBindingManager _bindingManager;
        private readonly ILogger<MvvmBinder> _logger;
        private bool _isDisposeInvoked = false;
        private bool _isAsyncDisposeInvoked = false;

        public event PropertyChangedEventHandler? ViewModelPropertyChanged;

        public MvvmBinder(IBindingManager bindingManager, ILogger<MvvmBinder> logger)
        {
            this._bindingManager = bindingManager;
            _logger = logger;
        }

        ~MvvmBinder()
        {
            Dispose(true);
        }

        public TValue Bind<TInput, TValue>(INotifyPropertyChanged viewmodel, Expression<Func<TInput, TValue>> bindingExpression) where TInput : INotifyPropertyChanged
        {
            var bindableProperty = ParseBindingExpression(bindingExpression) ?? throw new NullReferenceException("Can't find PropertyName");
            var bindableObj = new BindableObject(new WeakReference<INotifyPropertyChanged>(viewmodel));
            if (!this._bindingManager.CheckIfBindingAlreadyExist(bindableObj))
            {
                viewmodel.PropertyChanged -= this.ViewModelPropertyChanged;
                viewmodel.PropertyChanged += this.ViewModelPropertyChanged;
                this._bindingManager.AddBinding(bindableObj);
            }
            return (TValue)(bindableProperty.GetValue(viewmodel) ?? 
                    throw new InvalidOperationException("Can't GetValue from ViewModel"));
        }

        private PropertyInfo? ParseBindingExpression<TInput, TValue>(Expression<Func<TInput, TValue>> bindingExpression) where TInput : INotifyPropertyChanged
        {
            if (bindingExpression.NodeType == ExpressionType.Lambda && bindingExpression.Body is MemberExpression && (bindingExpression.Body as MemberExpression).Member is PropertyInfo)
            {
                return (bindingExpression?.Body as MemberExpression)?.Member as PropertyInfo;
            }
            throw new NotSupportedException();
        }
    
        protected virtual async ValueTask DisposeAsyncCore()
        {
            if (_isAsyncDisposeInvoked)
                await ValueTask.CompletedTask;
            _isAsyncDisposeInvoked = true;
            //Dispose objects asynchronously thus after this call no need to reclear the managed objects.
        }
        
        /// <summary>
        /// This will cleanup any managed objects.
        /// </summary>
        /// <param name="isInvokedFromGCFinalizer"></param>
        public virtual void Dispose(bool isInvokedFromGCFinalizer)
        {
            _logger.Log(LogLevel.Debug, $"Disposed invoked by {isInvokedFromGCFinalizer}");
            if (_isDisposeInvoked)
            {
                return;
            }

            if (!isInvokedFromGCFinalizer)
            {
             //Free managed resources if any.
            }
            //Free unmanaged resources if any.
            _isDisposeInvoked = true;
            Dispose();
        }
        
        public void Dispose()
        {
            Dispose(false);
        }

        public async ValueTask DisposeAsync()
        {
            _logger.Log(LogLevel.Debug, $"DisposedAsync invoked");
            await DisposeAsyncCore();
            // sending false as expecting that DisposeAsyncCore would already dispose all managed objects.
            Dispose(true);
            await ValueTask.CompletedTask;
        }
    }
}
