using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LF08Nr2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);
        const int DWWMA_CAPTION_COLOR = 35;

        public MainWindow()
        {
            InitializeComponent();
            IntPtr hWnd = new WindowInteropHelper(this).EnsureHandle();
            int[] colorstr = new int[] { 0xFF00FF };
            DwmSetWindowAttribute(hWnd, DWWMA_CAPTION_COLOR, colorstr, 4);

            TextBlock myTextBlock = new TextBlock();
            myTextBlock.FontSize = 18;
            myTextBlock.FontWeight = FontWeights.Bold;
            myTextBlock.FontStyle = FontStyles.Italic;
            myTextBlock.Text = "Hello, world!";
        }

    }
}