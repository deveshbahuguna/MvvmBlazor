using MvvmLightCore.Binder;

namespace MvvmLightCore.Exceptions
{
    public class NullBindPropertyFoundException : Exception
    {
        private readonly BindableObject _toAddBindingObj;

        public NullBindPropertyFoundException(BindableObject toAddBindingObj) : base(
            message: $"Property of Bindable object  is null")

        {
            _toAddBindingObj = toAddBindingObj;
        }
    }    
}
