using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RepairDesk.ApiClient;
using RepairDesk.WpfClient.Helpers;
using RepairDesk.WpfClient.ViewModels;
using RepairDesk.WpfClient.Views;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;


namespace RepairDesk.WpfClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public readonly MainViewModel MainViewModel = null;

        public static IHost DIHost { get; private set; }

        public IConfiguration Configuration { get; private set; }

        public App()
        {
            // config
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            Configuration = builder.Build();

            // globalization
            System.Globalization.CultureInfo customCulture = 
                (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = customCulture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = customCulture;

            // di
            DIHost = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
					StartupEx.ConfigureServices(services);
				})
                .Build();

			MainViewModel = new MainViewModel();
		}

		protected override async void OnStartup(StartupEventArgs e)
        {
            await DIHost.StartAsync();

            var mainWindow = DIHost.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using (DIHost)
            {
                await DIHost.StopAsync();
            }

            base.OnExit(e);
        }
    }

    public class StartupEx
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            var app = (App)Application.Current;

            // config
            string repairDeskApiBaseAddress = app.Configuration.GetValue<string>("ApiConfig:RepairDeskApiBaseAddress");

            // register services
            services.AddSingleton(typeof(KeyedService<string, string>), 
                new KeyedService<string, string>("RepairDeskApiBaseAddressKey", repairDeskApiBaseAddress));

            services.AddTransient<JwtHandler>();
            services.AddHttpClient<IRepairDeskApiClient, RepairDeskApiClient>(client =>
            {
                client.BaseAddress = new Uri(repairDeskApiBaseAddress);
            })
            .AddHttpMessageHandler<JwtHandler>();  // dodavanje JwtHandler-a

            services.AddHttpClient<RepairDeskApiClient>("RepairDeskApiClientHttpClient")
                .AddHttpMessageHandler<JwtHandler>();  // dodavanje JwtHandler-a

            services.AddTransient(sp =>
            {
                var allKeyedServices = sp.GetServices<KeyedService<string, string>>();
                var baseUrlKeyedService = allKeyedServices.FirstOrDefault(ks => ks.Key == "RepairDeskApiBaseAddressKey");
                string baseUrl = baseUrlKeyedService.Value;
                var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient("RepairDeskApiClientHttpClient");
                httpClient.BaseAddress = new Uri(baseUrl);

                return new RepairDeskApiClient(baseUrl, httpClient);
            });

            // views & viewmodels
            //services.AddTransient<_MainWindowViewModel>();
            //services.AddSingleton<MainViewModel>();
            services.AddTransient<MainWindow>();
            //services.AddTransient<ProductsViewModel>();
            //services.AddTransient<ProductsView>();
            //services.AddTransient<ProductDetailsViewModel>();
            //services.AddTransient<ProductDetailsView>();

        }
    }

    public class KeyedService<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; }

        public KeyedService(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
