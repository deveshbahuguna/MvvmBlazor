using Microsoft.Extensions.DependencyInjection;
using MvvmLightCore.Binder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmLightCore.Registry
{
    public static class MvvmLightCoreDiRegistry
    {
        public static void AddMvvm(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddTransient<IBindingManager, BindingManager>();
            serviceProvider.AddTransient<IMvvmBinder, MvvmBinder>();
        }
    }
}
