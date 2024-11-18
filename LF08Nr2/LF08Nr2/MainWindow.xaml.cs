using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LF08Nr2.View;

namespace LF08Nr2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ImportButtonClick(object sender, RoutedEventArgs e)
        {
            ImportView importView = new ImportView();
            //Sollte noch mehr Logik hinzukommen wird dies in Eine ViewModel Ausgelagert 
            importView.Show();
        }

        private void ExportButtonClick(object sender, RoutedEventArgs e)
        {
            ExportView exportView = new ExportView();
            //Sollte noch mehr Logik hinzukommen wird dies in Eine ViewModel Ausgelagert 
            exportView.Show();
        }
    }
}