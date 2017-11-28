using Microsoft.Extensions.DependencyInjection;

using SolidConsoleStorage.Models;
using SolidConsoleStorage.Controllers;
using SolidConsoleStorage.Views;

namespace SolidConsoleStorage
{
    class Program
    {
        public static ServiceProvider Services;

        static void Main(string[] args)
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddTransient<IStorageContext>((_) => new StorageContext());
            Services = serviceCollection.BuildServiceProvider();

            new ConsoleView(new StorageController()).Run();
        }
    }
}
