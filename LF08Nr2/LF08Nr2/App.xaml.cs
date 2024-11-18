using LF08Nr2.Model;
using LF08Nr2.View;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Windows;

namespace LF08Nr2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DatabaseModel databaseModel = new DatabaseModel();
            databaseModel.checkIfDbExist();
        }
    }

}
