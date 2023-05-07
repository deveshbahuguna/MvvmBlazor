using System.Text.Json;
using MvvmLightCore.Binder;

namespace MvvmLightCore.Exceptions;

public class ViewModelObjNotFoundException :Exception
{
    private readonly BindableObject _bindableObject;

    public ViewModelObjNotFoundException(BindableObject bindableObject): base(message: "ViewModel of bindable object not found")
    {
        _bindableObject = bindableObject;
    }
}