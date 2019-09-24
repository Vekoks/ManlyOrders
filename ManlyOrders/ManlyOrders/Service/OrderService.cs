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
    }
}
