using System;
using System.ComponentModel;
using Xunit;
using MvvmLightCore.Binder;

namespace MvvmLightCoreTest.BindingManager;

public class BindingManagerTest
{
    
    [Theory]

    [Fact]
    public void AddBinding_ViewModel_CallsINotifyEvent()
    {
        TestViewModel testViewModel = new TestViewModel();
        BindableObject bindableObject = new BindableObject(new WeakReference<INotifyPropertyChanged>(testViewModel));
        MvvmLightCore.Binder.BindingManager bindingManager = new MvvmLightCore.Binder.BindingManager();
        bindingManager.AddBinding(bindableObject);
        bindingManager.AddBinding();
        
    }
}