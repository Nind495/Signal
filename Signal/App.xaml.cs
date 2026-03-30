using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SignalLib;
using System.Configuration;
using System.Data;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        readonly private ServiceProvider _serviceProvider;

        public App()
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(configure => 
            {
                configure.AddConsole();
                configure.SetMinimumLevel(LogLevel.Information);
            });
            services.AddSingleton<MainWindow>();
        }       
    }
}
