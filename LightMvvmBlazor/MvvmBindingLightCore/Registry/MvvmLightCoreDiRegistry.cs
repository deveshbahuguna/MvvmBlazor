using Microsoft.Extensions.DependencyInjection;
using MvvmLightCore.Binder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmLightCore;
using MvvmLightCore.Bindings;

namespace MvvmBindingLightCore.Registry
{
    public static class MvvmLightCoreDiRegistry
    {
        public static void AddMvvm(this IServiceCollection serviceProvider)
        {
            serviceProvider.AddTransient<IMvvmBinder, MvvmBinder>();
        }
    }
}
