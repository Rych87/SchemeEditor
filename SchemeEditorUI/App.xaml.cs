using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using SchemeEditorUI.Services;
using SchemeEditorUI.ViewModels;
using SchemeModel;

namespace SchemeEditorUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost Host { get; private set; }

        public App()
        {
            Host = Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IMessageBoxService, MessageBoxService>();
                    services.AddSingleton<IFileDialogService, JsonFileDialogService>();
                    services.AddSingleton<SchemeRepositoryJson>();

                    services.AddSingleton<MainViewModel>();

                    services.AddSingleton<MainWindow>();
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await Host.StartAsync();

            var window = Host.Services.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await Host.StopAsync();

            Host.Dispose();

            base.OnExit(e);
        }
    }

}
