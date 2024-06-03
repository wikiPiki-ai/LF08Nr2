using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Media;
using System.Diagnostics;

namespace LF08Nr2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>  
    public partial class App : Application
    {
            void App_Startup(object sender, StartupEventArgs e)
            {
                Trace.WriteLine(System.Environment.CurrentDirectory + "\\DB");


                MainWindow mainWindow = new MainWindow();

                mainWindow.WindowState = WindowState.Minimized;
                //mainWindow.

                mainWindow.Show();

            }
    }
}