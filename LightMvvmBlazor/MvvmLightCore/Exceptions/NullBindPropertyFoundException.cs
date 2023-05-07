using MvvmLightCore.Binder;

namespace MvvmLightCore.Exceptions
{
    public class NullBindPropertyFoundException : Exception
    {
        private readonly IBindableObject _toAddBindingObj;

        public NullBindPropertyFoundException(IBindableObject toAddBindingObj) : base(
            message: $"Property of Bindable object  is null")

        {
            _toAddBindingObj = toAddBindingObj;
        }
    }    
}
