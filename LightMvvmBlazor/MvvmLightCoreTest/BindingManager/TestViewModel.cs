using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MvvmLightCoreTest.BindingManager;

public class TestViewModel : INotifyPropertyChanged
{
    private string? _sampleProperty;
    public event PropertyChangedEventHandler? PropertyChanged;

    public string? SampleProperty
    {
        get => _sampleProperty;
        set => SetField(ref _sampleProperty,value);
    }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}