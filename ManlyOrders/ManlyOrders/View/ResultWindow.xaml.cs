using ManlyOrders.Models;
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

namespace ManlyOrders
{
    /// <summary>
    /// Interaction logic for ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow(List<Recording> listRecording)
        {
            InitializeComponent();

            for (int i = 0; i < listRecording.Count; i++)
            {
                var currentLine = listRecording[i].Record + "           " + listRecording[i].Number +"бр.";

                listBoxItams.Items.Add(currentLine);
            }
            
        }
    }
}
