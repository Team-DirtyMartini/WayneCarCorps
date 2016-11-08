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
using System.Windows.Navigation;
using System.Windows.Shapes;
using PDFWriter;
using WayneCarCorps.Data;
using WayneCarCorps.Data.Common;
using WayneCarCorps.Models;

namespace WayneCarCorps.WpfClient
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

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var context = new WayneCarCorpsContext();

            var pdfExporter = new PdfExporter(new EfRepository<Sale>(context));
              pdfExporter.CreatePdfTable();
        }
    }
}
