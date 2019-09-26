using ManlyOrders.Models;
using Microsoft.Win32;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
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

                graph.DrawString(currentLine, font, XBrushes.Black, new XRect(0, numberLine, pdfPage.Width.Point, pdfPage.Height.Point), XStringFormats.TopLeft);
            }

            pdf.Save(fileNameAndPath);

            MessageBox.Show("Успешен запис на файл");
        }
    }
}
