using ManlyOrders.Models;
using ManlyOrders.Service;
using ManlyOrders.View;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
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

namespace ManlyOrders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Order> listOrderFromFile;
        private List<Recording> listRecording;

        public MainWindow()
        {
            InitializeComponent();

            listOrderFromFile = OrderService.GetRecords();

            listRecording = new List<Recording>();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            var barcode = textBoxBarcode.Text.ToUpper();

            var number = textBoxNumber.Text;

            if (barcode == string.Empty || number == string.Empty)
            {
                MessageBox.Show("И двете полета трябва да са попълнени");
                return;
            }

            int numberCurrentOrder = 0;

            int.TryParse(number, out numberCurrentOrder);

            if (numberCurrentOrder == 0)
            {
                MessageBox.Show("Не сте въвели правилно число");
                return;
            }

            OrderService.SetRecord(listOrderFromFile, listRecording, barcode, numberCurrentOrder);

            textBoxBarcode.Text = "";

            textBoxNumber.Text = "";
        }

        private void buttonResult_Click(object sender, RoutedEventArgs e)
        {
            var resultWindow = new ResultWindow(listRecording);
            resultWindow.Show();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string msg = "Наистина ли искате да излезете, защото информацията ще се загуби?";

            MessageBoxResult result = MessageBox.Show( msg,"Изход", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            } 
        }

        private void buttonResulthandle_Click(object sender, RoutedEventArgs e)
        {
            var resultWindow = new ResultHandleWindow(listRecording);
            resultWindow.Show();
        }

        private void buttonResultSteel_Click(object sender, RoutedEventArgs e)
        {
            var resultWindow = new ResultSteelWindows(listRecording);
            resultWindow.Show();
        }

        private void buttonAddNewknife_Click(object sender, RoutedEventArgs e)
        {
            var newModelKnifetWindow = new NewModelKnifeWindow();
            newModelKnifetWindow.Show();
        }
    }
}
