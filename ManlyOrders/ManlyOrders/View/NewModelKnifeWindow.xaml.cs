using ManlyOrders.Service;
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

namespace ManlyOrders.View
{
    /// <summary>
    /// Interaction logic for NewModelKnifeWindow.xaml
    /// </summary>
    public partial class NewModelKnifeWindow : Window
    {
        public NewModelKnifeWindow()
        {
            InitializeComponent();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var barcode = textBoxBarcode.Text;

            var nameNewKnife = textBoxNameNewKnife.Text;

            OrderFileService.SaveNewModelKnife(barcode, nameNewKnife);

            textBoxBarcode.Text = "";

            textBoxNameNewKnife.Text = "";
        }
    }
}
