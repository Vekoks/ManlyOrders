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
    /// Interaction logic for ResultSteelWindows.xaml
    /// </summary>
    public partial class ResultSteelWindows : Window
    {
        private List<Knife> listKnife;

        public ResultSteelWindows(List<Recording> listRecording)
        {
            InitializeComponent();

            listKnife = OrderService.GetSteels(listRecording);

            for (int i = 0; i < listKnife.Count; i++)
            {
                var currentLine = "";

                if (listKnife[i].OneHand)
                {
                    currentLine = listKnife[i].Name + " ONE HAND" + " ".PadRight(9) + "D2->" + listKnife[i].D2 + " ".PadRight(7) + "CPM154->" + listKnife[i].CPM154 + 
                        " ".PadRight(7) + "CPMS90V->" + listKnife[i].CPMS90V + " ".PadRight(7) + "12C27->" + listKnife[i]._12C27 + " ".PadRight(7) + "14C28->" + listKnife[i]._14C28 ;
                }
                else if (listKnife[i].TwoHand)
                {
                    currentLine = listKnife[i].Name + " TWO HAND" + " ".PadRight(6) + "D2->" + listKnife[i].D2 + " ".PadRight(7) + "CPM154->" + listKnife[i].CPM154 + 
                        " ".PadRight(7) + "CPMS90V->" +listKnife[i].CPMS90V + " ".PadRight(7) + "12C27->" + listKnife[i]._12C27 + " ".PadRight(7) + "14C28->" +listKnife[i]._14C28;
                }
                else
                {
                    currentLine = listKnife[i].Name + " ".PadRight(10) + "D2->" +listKnife[i].D2 + " ".PadRight(10) + "CPM154->" + listKnife[i].CPM154 + 
                        " ".PadRight(10) + "CPMS90V->" + listKnife[i].CPMS90V + " ".PadRight(10) + "12C27->" + listKnife[i]._12C27 + " ".PadRight(10) + "14C28->" + listKnife[i]._14C28;
                }

                listBoxKnifeSteel.Items.Add(currentLine);

                currentLine = new string('-', 104);

                listBoxKnifeSteel.Items.Add(currentLine);
            }
        }

        private void buttonSaveFile_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
