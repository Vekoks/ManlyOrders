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
    public static class OrderFileService
    {
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

                if (numberLine / 756 == 1)
                {
                    pdfPage = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(pdfPage);

                    j = 2;
                    numberLine = j * 14;
                }

                graph.DrawString(currentLine, font, XBrushes.Black, new XRect(0, numberLine, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            }

            pdf.Save(fileNameAndPath);

            MessageBox.Show("Успешен запис на файл");
        }

        public static void SaveSteels(List<Knife> listKnife)
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
            XFont font = new XFont("Verdana", 12);

            graph.DrawString("Поръчка стомана", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);

            for (int i = 0, j = 2; i < 100; i++, j += 2)
            {
                var currentLine = "";

                if (listKnife[i].OneHand)
                {
                    currentLine = listKnife[i].Name + " ONE HAND" + " ".PadRight(9) + "D2->" + listKnife[i].D2 + " ".PadRight(7) + "CPM154->" + listKnife[i].CPM154 +
                        " ".PadRight(7) + "CPMS90V->" + listKnife[i].CPMS90V + " ".PadRight(7) + "12C27->" + listKnife[i]._12C27 + " ".PadRight(7) + "14C28->" + listKnife[i]._14C28;
                }
                else if (listKnife[i].TwoHand)
                {
                    currentLine = listKnife[i].Name + " TWO HAND" + " ".PadRight(6) + "D2->" + listKnife[i].D2 + " ".PadRight(7) + "CPM154->" + listKnife[i].CPM154 +
                        " ".PadRight(7) + "CPMS90V->" + listKnife[i].CPMS90V + " ".PadRight(7) + "12C27->" + listKnife[i]._12C27 + " ".PadRight(7) + "14C28->" + listKnife[i]._14C28;
                }
                else
                {
                    currentLine = listKnife[i].Name + " ".PadRight(7) + "D2->" + listKnife[i].D2 + " ".PadRight(10) + "CPM154->" + listKnife[i].CPM154 +
                        " ".PadRight(10) + "CPMS90V->" + listKnife[i].CPMS90V + " ".PadRight(10) + "12C27->" + listKnife[i]._12C27 + " ".PadRight(10) + "14C28->" + listKnife[i]._14C28;
                }

                var numberLine = j * 12;

                if (numberLine / 768 == 1)
                {
                    pdfPage = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(pdfPage);

                    j = 2;
                    numberLine = j * 12;
                }

                graph.DrawString(i.ToString(), font, XBrushes.Black, new XRect(0, numberLine, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                currentLine = new string('-', 110);

                numberLine = (j += 2) * 12;

                graph.DrawString(currentLine, font, XBrushes.Black, new XRect(0, numberLine, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            }

            pdf.Save(fileNameAndPath);

            MessageBox.Show("Успешен запис на файл");
        }

        public static void SaveHandles(List<string> listHandle)
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
            XFont font = new XFont("Verdana", 12);

            graph.DrawString("Поръчка чирени", font, XBrushes.Black, new XRect(0, 0, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopCenter);

            for (int i = 0, j = 2; i < listHandle.Count; i++, j += 2)
            {
                var currentLine = listHandle[i];

                var numberLine = j * 12;

                if (numberLine / 768 == 1)
                {
                    pdfPage = pdf.AddPage();
                    graph = XGraphics.FromPdfPage(pdfPage);

                    j = 2;
                    numberLine = j * 14;
                }

                var currentLineSplit = currentLine.Split(' ');

                if (!currentLineSplit[0].Contains("-"))
                {
                    graph.DrawString(currentLineSplit[0] + ":", font, XBrushes.Black, new XRect(0, numberLine, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);

                    for (int k = 1; k < currentLineSplit.Count(); k++)
                    {

                        if (currentLineSplit[k] == string.Empty)
                        {
                            continue;
                        }

                        if (currentLineSplit[k + 1] != string.Empty)
                        {
                            k += 1;

                            j += 2;
                            numberLine = j * 12;

                            graph.DrawString(currentLineSplit[k - 1] + " " + currentLineSplit[k], font, XBrushes.Black, new XRect(0, numberLine, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        }
                        else
                        {
                            j += 2;
                            numberLine = j * 12;

                            graph.DrawString(currentLineSplit[k], font, XBrushes.Black, new XRect(0, numberLine, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                        }
                    }
                }
                else
                {
                    graph.DrawString(currentLine, font, XBrushes.Black, new XRect(0, numberLine, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
                }
            }

            pdf.Save(fileNameAndPath);

            MessageBox.Show("Успешен запис на файл");
        }

        public static void SaveNewModelKnife(string barcode, string modelKnife)
        {
            var barcodeUp = barcode.ToUpper();

            var modelKnifeUP = modelKnife.ToUpper();

            string filePath = Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory);

            var filePathSplit = filePath.Split('\\');

            var myFilePath = "";

            for (int i = 0; i < filePathSplit.Length - 2; i++)
            {
                myFilePath += filePathSplit[i] + "\\";
            }

            myFilePath += "Resources\\Codes.txt";

            string[] informationFromFile = File.ReadAllLines(myFilePath);

            try
            {
                StreamWriter sw = new StreamWriter(myFilePath);

                for (int i = 0; i < informationFromFile.Count(); i++)
                {
                    sw.WriteLine(informationFromFile[i]);
                }

                sw.WriteLine(barcodeUp + "-" + modelKnifeUP);

                sw.Close();
            }
            catch (Exception e)
            {
                var error = e.Message;
            }

            MessageBox.Show("Успешено добавяне на нов модел нож");
        }
    }
}
