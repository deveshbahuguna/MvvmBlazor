using System.Text.Json;
using MvvmLightCore.Binder;

namespace MvvmLightCore.Exceptions;

public class ViewModelObjNotFoundException :Exception
{
    private readonly IBindableObject _bindableObject;

    public ViewModelObjNotFoundException(IBindableObject bindableObject): base(message: "ViewModel of bindable object not found")
    {
        _bindableObject = bindableObject;
    }
}