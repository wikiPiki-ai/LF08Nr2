using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LF08Nr2.Model;
using LF08Nr2.ViewModel;

namespace LF08Nr2.View
{
    /// <summary>
    /// Interaction logic for ImportView.xaml
    /// </summary>
    public partial class ImportView : Window
    {

        private readonly ImportViewModel importviewModel= new ImportViewModel();

        public ImportView()
        {
            InitializeComponent();

            

            List<ImportModel> import= new List<ImportModel>();
            
            import.Add( new ImportModel() {Id = 1, Name = "TestName",Lastname ="testLastname2"});

            DummyName.ItemsSource = import;
        }

        private void ColoseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddDater(object sender, RoutedEventArgs e)
        {
            //hier der aufruf zum code behind, wo logik verfasst wird.
            importviewModel.AddDataFromExplorer();
        }
    }
}
