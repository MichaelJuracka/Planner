using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Planner.Data;
using Planner.Data.Interfaces;
using Planner.Data.Repositories;
using Planner.Business.Interfaces;
using Planner.Business.Managers;
using Planner.Views.UserControls;
using Planner.Business.Services;

namespace Planner
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; set; }
        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(typeof(MainWindow));
            services.AddSingleton<ApplicationDbContext>();

            services.AddSingleton<IRegionRepository, RegionRepository>();
            services.AddSingleton<IStationRepository, StationRepository>();
            services.AddSingleton<IRouteRepository, RouteRepository>();
            services.AddSingleton<IStateRepository, StateRepository>();
            services.AddSingleton<IProviderRepository, ProviderRepository>();
            services.AddSingleton<IBusTypeRepository, BusTypeRepository>();
            services.AddSingleton<IPassengerRepository, PassengerRepository>();
            services.AddSingleton<IExportRepository, ExportRepository>();
            services.AddSingleton<IOwnerRepository, OwnerRepository>();
            services.AddSingleton<IEmailUserRepository, EmailUserRepository>();
            services.AddSingleton<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddSingleton<IEmailRepository, EmailRepository>();
            services.AddSingleton<IEmailAttachmentRepository, EmailAttachmentRepository>();

            services.AddSingleton<IRegionManager, RegionManager>();
            services.AddSingleton<IStationManager, StationManager>();
            services.AddSingleton<IRouteManager, RouteManager>();
            services.AddSingleton<IStateManager, StateManager>();
            services.AddSingleton<IProviderManager, ProviderManager>();
            services.AddSingleton<IBusTypeManager, BusTypeManager>();
            services.AddSingleton<IPassengerManager, PassengerManager>();
            services.AddSingleton<IOfficeManager, OfficeManager>();
            services.AddSingleton<IExportManager, ExportManager>();
            services.AddSingleton<IOwnerManager, OwnerManager>();
            services.AddSingleton<IEmailManager, EmailManager>();

            services.AddSingleton<IEmailSender, EmailSender>();
        }
    }
}
