using System;
using Microsoft.Extensions.DependencyInjection;
using Xamarin.Forms;
using Xde.App.Models;
using Xde.App.Services;

namespace Xde.App
{
	public partial class App : Application
    {
		public static IServiceProvider ServiceProvider { get; set; }

		public void SetupServices()
		{
			var services = new ServiceCollection();

			services.AddTransient<IDataStore<Item>, MockDataStore>();

			services.AddSingleton<PgSettings>();
			services.AddSingleton<IDbConnectionFactory, PgConnectionFactory>();

			ServiceProvider = services.BuildServiceProvider();
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
