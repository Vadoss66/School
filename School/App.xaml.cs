using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using School.Model;
using School.ViewModel;

namespace School
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<AppContext>();
            services.AddSingleton<SchoolEntities>();
            services.AddSingleton<ViewModelMain>();
            services.AddSingleton<MainWindow>();
            services.AddTransient<ClassViewModel>();
            services.AddTransient<LessonTimeViewModel>();
            services.AddTransient<LessonNameViewModel>();
            services.AddTransient<PupilViewModel>();
            services.AddTransient<PersonViewModel>();
            services.AddTransient<PositionViewModel>();
            services.AddTransient<RoomViewModel>();
            services.AddSingleton<StartVM>();
            services.AddTransient<ScheduleViewModel>();
        }

        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = _serviceProvider.GetService<MainWindow>();
            if (mainWindow is null) return;
            mainWindow.DataContext = _serviceProvider.GetService<ViewModelMain>();
            mainWindow.ShowDialog();
        }
    }
}
