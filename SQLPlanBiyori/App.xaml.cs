using SQLPlanBiyori.Models;
using SQLPlanBiyori.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace SQLPlanBiyori
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            this.Config = AppConfig.Load();
            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            var mainWindow = new Views.MainWindow();
            mainWindow.DataContext = new MainWindowViewModel(this.Config);
            mainWindow.Closing += MainWindow_Closing;
            this.MainWindow = mainWindow;
            mainWindow.Show();
        }
        public AppConfig Config { get; private set; }
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Config.Save();
        }
    }
}
