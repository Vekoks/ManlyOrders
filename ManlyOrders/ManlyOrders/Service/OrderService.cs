using ManlyOrders.Models;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManlyOrders.Service
{
    public static class OrderService
    {
        public static List<Order> GetRecords()
        {
            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 2 ; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\Codes.txt";

            string[] informationFromFile = File.ReadAllLines(myFilePath);


            var listOrders = new List<Order>();

            for (int i = 0; i < informationFromFile.Length; i++)
            {
                var recordAllInfo = informationFromFile[i].Split('-');

                var currentOrder = new Order();

                currentOrder.Barcode = recordAllInfo[0];
                currentOrder.Detail = recordAllInfo[1];

                listOrders.Add(currentOrder);
            }

            return listOrders;
        }

        public static void SetRecord(List<Order> listOrder, List<Recording> listRecording, string barcode, string number)
        {
            int numberCurrentOrder = 0;

            int.TryParse(number, out numberCurrentOrder);

            var kfineAndDetail = "";

            for (int i = 0; i < listOrder.Count; i++)
            {
               
                if (listOrder[i].Barcode.Equals(barcode))
                {
                    kfineAndDetail = listOrder[i].Detail;
                    break;
                }
            }

            if (kfineAndDetail == string.Empty)
            {
                kfineAndDetail = barcode;
            }

            var addingOldRecording = true;

            for (int i = 0; i < listRecording.Count; i++)
            {
                if (listRecording[i].Record.Equals(kfineAndDetail))
                {
                    listRecording[i].Number += numberCurrentOrder;
                    addingOldRecording = false;
                    break;
                }
            }

            if (addingOldRecording)
            {
                listRecording.Add(new Recording
                {
                    Record = kfineAndDetail,
                    Number = numberCurrentOrder
                });
            }
        }

        public static void SaveRecord(List<Recording> listRecording)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();

            string fileNameAndPath = "";

            if (saveFileDialog.ShowDialog() == true)
            {
                fileNameAndPath = saveFileDialog.FileName + ".pdf";
            }

            if (fileNameAndPath == string.Empty)
            {
                return;
            }

            PdfDocument pdf = new PdfDocument();
            PdfPage pdfPage = pdf.AddPage();
            XGraphics graph = XGraphics.FromPdfPage(pdfPage);
            XFont font = new XFont("Verdana", 16);

            graph.DrawString("Поръчка", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);

            for (int i = 0, j = 2; i < listRecording.Count; i++, j += 2)
            {
                var currentLine = listRecording[i].Record + "           " + listRecording[i].Number + " бр.";

                var numberLine = j * 14;

                graph.DrawString(currentLine, font, XBrushes.Black, new XRect(0, numberLine, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            }

            pdf.Save(fileNameAndPath);

            MessageBox.Show("Успешен запис на файл");
        }

        public static List<Knife> GetSteels(List<Recording> listRecording)
        {
            var listKnifeAndSteel = new List<Knife>();

            for (int i = 0; i < listRecording.Count(); i++)
            {
                var currentFullNameRecord = listRecording[i].Record.Split(' ');

                var currentNameKnife = currentFullNameRecord[0];

                var currentNameSteel = GetSteelFromRecord(currentFullNameRecord);

                var currentModelBlade= GetModelBladeFromRecord(currentFullNameRecord);

                var currentNumber = listRecording[i].Number;

                var findKnife = false;

                for (int j = 0; j < listKnifeAndSteel.Count; j++)
                {
                    if (listKnifeAndSteel[j].Name.Equals(currentNameKnife))
                    {
                        if (listKnifeAndSteel[j].OneHand && currentModelBlade.Equals("ONE"))
                        {
                            AddNumberOnSpecificSteel(currentNameSteel, listKnifeAndSteel[j], currentNumber);
                            findKnife = true;
                            break;
                        }

                        else if (listKnifeAndSteel[j].TwoHand && currentModelBlade.Equals("TWO"))
                        {
                            AddNumberOnSpecificSteel(currentNameSteel, listKnifeAndSteel[j], currentNumber);
                            findKnife = true;
                            break;
                        }
                        else if(!listKnifeAndSteel[j].OneHand && !listKnifeAndSteel[j].TwoHand)
                        {
                            AddNumberOnSpecificSteel(currentNameSteel, listKnifeAndSteel[j], currentNumber);
                            findKnife = true;
                            break;
                        }
                    }
                }
                
                if (currentModelBlade.Equals("ONE") && !findKnife)
                {
                    var currentKnife = new Knife
                    {
                        Name = currentNameKnife,
                        OneHand = true
                    };

                    AddNumberOnSpecificSteel(currentNameSteel, currentKnife, currentNumber);
                    listKnifeAndSteel.Add(currentKnife);
                }

                if (currentModelBlade.Equals("TWO") && !findKnife)
                {

                    var currentKnife = new Knife
                    {
                        Name = currentNameKnife,
                        TwoHand = true
                    };

                    AddNumberOnSpecificSteel(currentNameSteel, currentKnife, currentNumber);
                    listKnifeAndSteel.Add(currentKnife);
                }

                if (!findKnife && !currentModelBlade.Equals("TWO") && !currentModelBlade.Equals("ONE"))
                {

                    var currentKnife = new Knife
                    {
                        Name = currentNameKnife,
                    };

                    AddNumberOnSpecificSteel(currentNameSteel, currentKnife, currentNumber);
                    listKnifeAndSteel.Add(currentKnife);
                }
            }

            return listKnifeAndSteel;
        }

        public static void GetHandles(List<Recording> listRecording)
        {
            var listHandles = new List<Handle>();

            for (int i = 0; i < listRecording.Count(); i++)
            {
                var currentFullNameRecord = listRecording[i].Record.Split(' ');

                var currentNameKnife = currentFullNameRecord[0];

                var currentHandleColor = GetHandleColorFromRecord(currentFullNameRecord);

                var currentNumber = listRecording[i].Number;

                var findHandle = false;

                for (int j = 0; j < listHandles.Count(); j++)
                {
                    if (listHandles[j].NameKnife.Equals(currentNameKnife) && listHandles[j].HandleColor.Equals(currentHandleColor))
                    {
                        listHandles[j].Count += currentNumber;
                        findHandle = true;
                        break;
                    }
                }

                if (!findHandle)
                {
                    var currentHandle = new Handle
                    {
                        NameKnife = currentNameKnife,
                        HandleColor = currentHandleColor,
                        Count = currentNumber
                    };

                    listHandles.Add(currentHandle);
                }
            }
        }

        private static string GetHandleColorFromRecord(String[] record)
        {
            for (int i = 0; i < record.Count(); i++)
            {
                switch (record[i])
                {
                    case "MILITARY": return "MILITARY GREEN";
                    case "DESERT": return "DESERT IRONWOOD";
                    default:
                        break;
                }
            }

            return record[1];
        }

        private static string GetSteelFromRecord(String[] record)
        {
            for (int i = 0; i < record.Count(); i++)
            {
                switch (record[i])
                {
                    case "D2": return "D2";
                    case "CPM154": return "CPM154";
                    case "CPMS90V": return "CPMS90V";
                    case "12C27": return "12C27";
                    case "14C28N": return "14C28N";
                    default:
                        break;
                }
            }
            return "";
        }

        private static string GetModelBladeFromRecord(String[] record)
        {
            for (int i = 0; i < record.Count(); i++)
            {
                switch (record[i])
                {
                    case "TWO": return "TWO";
                    case "ONE": return "ONE";
                    default:
                        break;
                }
            }

            return "";
        }

        private static void AddNumberOnSpecificSteel(string nameSteel, Knife knife, int number)
        {
            switch (nameSteel)
            {
                case "D2": knife.D2 += number; break;
                case "CPM154": knife.CPM154 += number; break;
                case "CPMS90V": knife.CPMS90V += number; break;
                case "12C27": knife._12C27 += number; break;
                case "14C28N": knife._14C28 += number; break;
                default:
                    break;
            }
        }
    }
}
