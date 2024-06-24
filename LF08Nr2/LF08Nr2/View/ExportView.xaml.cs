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
    /// Interaction logic for ExportView.xaml
    /// </summary>
    public partial class ExportView : Window
    {
        private readonly ExportViewModelcs exportViewModelcs;

        public ExportView()
        {
            exportViewModelcs = new ExportViewModelcs();
            DataContext = exportViewModelcs;
            InitializeComponent();
        }

        private void CloseWindow(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CsvExport(object sender, RoutedEventArgs e)
        {
            ExportModel export = new ExportModel();
            export.csvExport();
        }

        private void PDFExport(object sender, RoutedEventArgs e)
        {
            ExportModel export = new ExportModel();
            export.pdfExport();            
        }
    }
}
