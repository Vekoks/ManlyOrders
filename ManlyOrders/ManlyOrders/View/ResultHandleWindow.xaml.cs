using ManlyOrders.Models;
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
    /// Interaction logic for ResultHandleWindow.xaml
    /// </summary>
    public partial class ResultHandleWindow : Window
    {
        private List<Handle> listHandle;

        public ResultHandleWindow(List<Recording> listRecording)
        {
            InitializeComponent();

            listHandle = OrderService.GetHandles(listRecording);

            var listStringForView =  OrderService.GetHandlesReadyForView(listHandle);

            for (int i = 0; i < listStringForView.Count(); i++)
            {
                listBoxHandles.Items.Add(listStringForView[i]);
            }
        }
    }
}
