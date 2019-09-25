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


             for (int i = 0; i < listHandle.Count; i++)
            {
                var currentLine = listHandle[i].NameKnife + " ".PadRight(5) + listHandle[i].HandleColor + " ".PadRight(5) + listHandle[i].Count.ToString() + " ".PadRight(5);

                listBoxHandles.Items.Add(currentLine);

                currentLine = new string('-', 40);

                listBoxHandles.Items.Add(currentLine);
            }
        }
    }
}
