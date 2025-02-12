﻿using BlazorSample.ViewModels.Navbar;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorSample.ViewModels.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddViewModels(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<WeatherForecastsViewModel>();
        serviceCollection.AddTransient<CounterViewModel>();
        serviceCollection.AddTransient<ClockViewModel>();
        serviceCollection.AddTransient<ParametersViewModel>();
        serviceCollection.AddScoped<NavbarViewModel>();

        return serviceCollection;
    }
}