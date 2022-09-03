using Microsoft.Extensions.DependencyInjection;
using SampleViewModel.Counter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleViewModel.Registry
{
    public static class ViewModelDIRegistry
    {
        public static void AddViewModels(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<CounterViewModel>();
        }
    }
}
