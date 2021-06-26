using System;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xde.App.Models;
using Xde.App.Services;
using Xde.App.ViewModels;

namespace Xde.App
{
	public partial class App : Application
    {
		public static IServiceProvider ServiceProvider { get; set; }

		public void SetupServices()
		{
			var services = new ServiceCollection();

			services.AddTransient<IDataStore<Item>, MockDataStore>();

			services.AddSingleton<IAppSettings, SecureStorageAppSettings>();
			services.AddSingleton<IDbConnectionFactory, PgConnectionFactory>();

			services.AddTransient<ItemsViewModel>();
			services.AddTransient<NodeReviewViewModel>();
			services.AddTransient<ViewModels.Settings.MainViewModel>();

			ServiceProvider = services.BuildServiceProvider();

			DependencyResolver.ResolveUsing(type => ServiceProvider.GetService(type));
			DependencyResolver.ResolveUsing((type, dependencies) => ServiceProvider.GetService(type));
		}

		public App()
        {
            InitializeComponent();

            SetupServices();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
