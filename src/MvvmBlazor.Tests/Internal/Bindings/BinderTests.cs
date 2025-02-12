﻿using Binder = MvvmBlazor.Internal.Bindings.Binder;

namespace MvvmBlazor.Tests.Internal.Bindings;

public class BinderTests : UnitTest
{
    public BinderTests(ITestOutputHelper outputHelper) : base(outputHelper) { }

    protected override void RegisterServices(IServiceCollection services)
    {
        services.StrictMock<IBindingFactory>();
        services.StrictMock<IWeakEventManager>();
        services.Provide<TestViewModel>();
        services.Provide<Binder>();
    }

    [Fact]
    public void Bind_ThrowsWhenCallbackIsNull()
    {
        var viewModel = Services.GetRequiredService<TestViewModel>();
        var binder = Services.GetRequiredService<Binder>();

        Should.Throw<BindingException>(() => binder.Bind(viewModel, x => x.Test));
    }

    [Fact]
    public void Bind_ThrowsWhenViewModelIsNull()
    {
        var binder = Services.GetRequiredService<Binder>();

        var callback = (IBinding b, EventArgs a) => { };
        binder.ValueChangedCallback = callback;

        Should.Throw<BindingException>(() => binder.Bind<TestViewModel, string>(null!, x => x.Test));
    }

    [Fact]
    public void Bind_ThrowsWhenPropertyExpressionIsNull()
    {
        var viewModel = Services.GetRequiredService<TestViewModel>();
        var binder = Services.GetRequiredService<Binder>();

        var callback = (IBinding b, EventArgs a) => { };
        binder.ValueChangedCallback = callback;

        Should.Throw<BindingException>(() => binder.Bind<TestViewModel, string>(viewModel, null!));
    }

    [Fact]
    public void Bind_ThrowsWhenPropertyExpressionIsNotProperty()
    {
        var viewModel = Services.GetRequiredService<TestViewModel>();
        var binder = Services.GetRequiredService<Binder>();

        var callback = (IBinding b, EventArgs a) => { };
        binder.ValueChangedCallback = callback;

        Should.Throw<BindingException>(() => binder.Bind(viewModel, x => x.PublicField));
    }

    [Fact]
    public void Bind_BindsProperty()
    {
        var bindingFactory = Services.GetMock<IBindingFactory>();
        var weakEventManager = Services.GetMock<IWeakEventManager>();
        var viewModel = Services.GetRequiredService<TestViewModel>();
        var binding = new Mock<IBinding>();

        bindingFactory.Setup(x => x.Create(viewModel, It.IsAny<PropertyInfo>(), weakEventManager.Object))
            .Returns(binding.Object)
            .Verifiable();

        binding.Setup(x => x.GetValue()).Returns("test").Verifiable();
        weakEventManager.Setup(
                x => x.AddWeakEventListener(
                    binding.Object,
                    nameof(Binding.BindingValueChanged),
                    It.IsAny<Action<IBinding, EventArgs>>()
                )
            )
            .Verifiable();

        var callback = (IBinding b, EventArgs a) => { };

        var binder = Services.GetRequiredService<Binder>();
        binder.ValueChangedCallback = callback;

        var res = binder.Bind(viewModel, x => x.Test);
        res.ShouldBe("test");

        bindingFactory.Verify();
        binding.Verify();
        weakEventManager.Verify();
    }

    [Fact]
    public void Bind_BindsPropertyExactlyOnce()
    {
        var bindingFactory = Services.GetMock<IBindingFactory>();
        var weakEventManager = Services.GetMock<IWeakEventManager>();
        var viewModel = Services.GetRequiredService<TestViewModel>();
        var binding = new Mock<IBinding>();

        bindingFactory.Setup(x => x.Create(viewModel, It.IsAny<PropertyInfo>(), weakEventManager.Object))
            .Returns(binding.Object)
            .Verifiable();

        binding.Setup(x => x.GetValue()).Returns("test").Verifiable();
        weakEventManager.Setup(
                x => x.AddWeakEventListener(
                    binding.Object,
                    nameof(Binding.BindingValueChanged),
                    It.IsAny<Action<IBinding, EventArgs>>()
                )
            )
            .Verifiable();

        var callback = (IBinding b, EventArgs a) => { };

        var binder = Services.GetRequiredService<Binder>();
        binder.ValueChangedCallback = callback;

        var res = binder.Bind(viewModel, x => x.Test);
        res.ShouldBe("test");

        res = binder.Bind(viewModel, x => x.Test);
        res.ShouldBe("test");

        weakEventManager.Verify(
            x => x.AddWeakEventListener(
                binding.Object,
                nameof(Binding.BindingValueChanged),
                It.IsAny<Action<IBinding, EventArgs>>()
            ),
            Times.Once
        );
        binding.Verify();
        bindingFactory.Verify();
        weakEventManager.Verify();
    }

    [Fact]
    public void Bind_ThrowsWhenDisposed()
    {
        var viewModel = Services.GetRequiredService<TestViewModel>();

        var binder = Services.GetRequiredService<Binder>();
        binder.Dispose();

        Should.Throw<ObjectDisposedException>(() => binder.Bind(viewModel, x => x.Test));
    }

    public class TestViewModel : ViewModelBase
    {
        public string PublicField;
        public string Test { get; set; }
    }
}