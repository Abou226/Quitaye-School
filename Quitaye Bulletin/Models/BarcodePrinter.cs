using iTextSharp.text.pdf;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using Font = iTextSharp.text.Font;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Rectangle = iTextSharp.text.Rectangle;
using PrintAction;
using System.Windows.Forms;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.Wordprocessing;
using Document = iTextSharp.text.Document;
using PageSize = iTextSharp.text.PageSize;
using Paragraph = iTextSharp.text.Paragraph;
using Color = System.Drawing.Color;

namespace Quitaye_School.Models
{
    public class BarcodePrinter
    {
        public void PrintBarcode(string barcodeText, BarcodeFormat barcodeFormat, string printerName)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler((sender, e) => PrintBarcodePage(sender, e, barcodeText, barcodeFormat));
            pd.PrinterSettings.PrinterName = printerName;
            pd.Print();

            // You can also use the PrintPreviewDialog to preview the barcode before printing:
            //PrintPreviewDialog ppd = new PrintPreviewDialog();
            //ppd.Document = pd;
            //ppd.ShowDialog();
        }

        private void PrintBarcodePage(object sender, PrintPageEventArgs e, string barcodeText, BarcodeFormat barcodeFormat)
        {
            // Create a new BarcodeWriter instance and set its properties
            var barcodeWriter = new ZXing.BarcodeWriter();
            barcodeWriter.Format = barcodeFormat;
            barcodeWriter.Options.Width = e.PageBounds.Width - 100;
            barcodeWriter.Options.Height = e.PageBounds.Height - 100;
            barcodeWriter.Options.Margin = 10;

            // Generate the barcode bitmap and draw it on the page
            Bitmap barcodeBitmap = barcodeWriter.Write(barcodeText);
            e.Graphics.DrawImage(barcodeBitmap, 50, 50);

            // You can also add text or other elements to the page here
        }

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        public static async Task PrintBarcodeAsync(List<Barcode> barcodes, BarcodeFormat barcodeFormat, string pdfFilePath)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            // Create a new BarcodeWriter instance and set its properties
            var barcodeWriter = new ZXing.BarcodeWriter();
            barcodeWriter.Format = barcodeFormat;
            barcodeWriter.Options.Width = 250;
            barcodeWriter.Options.Height = 100;
            barcodeWriter.Options.Margin = 10;

            // Create a new PDF document and set its properties
            var document = new Document(new RectangleReadOnly(PageSize.A4.Width - 36, PageSize.A4.Height));
            document.SetMargins(18, 36, 18, 36);

            // Create a new table with 3 columns and 7 rows
            var table = new PdfPTable(3);
            if (barcodes.Count < 3)
                table = new PdfPTable(barcodes.Count);
            table.WidthPercentage = 100;
            table.DefaultCell.Padding = 10;

            // Generate the barcode and add it to the table along with the product details and price
            for (int i = 0; i < barcodes.Count; i++)
            {
                var barcodeBitmap = barcodeWriter.Write(barcodes[i].BarcodeText);
                var image = iTextSharp.text.Image.GetInstance(barcodeBitmap, System.Drawing.Imaging.ImageFormat.Bmp);

                // Create a new cell and add the barcode image, product details, and price
                var cell = new PdfPCell();
                cell.Border = 0;
                cell.AddElement(image);

                var detailsParagraph = new Paragraph(barcodes[i].Details, new Font(Font.FontFamily.TIMES_ROMAN, 8));

                var priceChunk = new Chunk($"{Convert.ToDecimal(barcodes[i].Price).ToString("N0")} FCFA", new Font(Font.FontFamily.TIMES_ROMAN, 8, Font.BOLD));
                var priceWidth = new Chunk("     "); // this creates some empty space to the left of the price
                var pricePhrase = new Phrase();
                pricePhrase.Add(priceWidth);
                pricePhrase.Add(priceChunk);

                var priceParagraph = new Paragraph(pricePhrase);
                priceParagraph.Alignment = Element.ALIGN_RIGHT;

                var combinedParagraph = new Paragraph();
                combinedParagraph.Add(detailsParagraph);
                combinedParagraph.Add(priceParagraph);

                cell.AddElement(combinedParagraph);

                // Add the cell to the table
                table.AddCell(cell);
            }

            // Add the table to the document and close it

            PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
            document.Open();
            document.Add(table);
            document.Close();
        }

        public static async Task PrintBarcodeOnLabelAsync(List<Barcode> barcodes, BarcodeFormat barcodeFormat, string pdfFilePath, BarcodePaperSize paperSize, int labelsPerPage)
        {
            // Create a new BarcodeWriter instance and set its properties
            var barcodeWriter = new BarcodeWriter
            {
                Format = barcodeFormat,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = (int)paperSize.Width,
                    Height = (int)paperSize.Height,
                    Margin = 10
                }
            };

            // Calculate the number of pages needed to print all the labels
            int pageCount = (int)Math.Ceiling((double)barcodes.Count / labelsPerPage);

            // Create a new PDF document and set its properties
            var document = new Document(new RectangleReadOnly(paperSize.Width + 10, paperSize.Height + 46));
            document.SetMargins(8, 8, 8, 8);

            // Create a new table with 1 column and as many rows as there are barcodes per page
            var table = new PdfPTable(1);
            table.WidthPercentage = 100;
            table.DefaultCell.Padding = 10;

            // Generate the barcodes and add them to the table along with the product details and price
            using (FileStream fileStream = new FileStream(pdfFilePath, FileMode.Create))
            {
                var writer = PdfWriter.GetInstance(document, fileStream);

                for (int i = 0; i < pageCount; i++)
                {
                    // Clear the table for the new labels
                    table.DeleteBodyRows();

                    // Add the labels to the table
                    for (int j = i * labelsPerPage; j < Math.Min((i + 1) * labelsPerPage, barcodes.Count); j++)
                    {
                        var barcodeBitmap = barcodeWriter.Write(barcodes[j].BarcodeText);

                        var image = iTextSharp.text.Image.GetInstance(barcodeBitmap, System.Drawing.Imaging.ImageFormat.Bmp);

                        // Create a new cell and add the barcode image, product details, and price
                        var cell = new PdfPCell();
                        cell.Border = Rectangle.NO_BORDER;
                        cell.AddElement(image);

                        var detailsParagraph = new Paragraph(barcodes[j].Details, new Font(Font.FontFamily.TIMES_ROMAN, 8));

                        var combinedParagraph = new Paragraph();
                        combinedParagraph.Add(detailsParagraph);

                        cell.AddElement(combinedParagraph);

                        // Add the cell to the table
                        table.AddCell(cell);
                    }

                    // Add the table to the document
                    document.Open();
                    document.Add(table);
                    document.Close();

                    foreach (var item in barcodes)
                    {
                        PrintBarcode(item);
                    }
                }
            }
        }


        public static async Task PrintBarcodeOnLabelAsync(List<Barcode> barcodes, BarcodeFormat barcodeFormat, string pdfFilePath, float labelWidth, float labelHeight, int labelsPerPage)
        {
            // Create a new BarcodeWriter instance and set its properties
            var barcodeWriter = new ZXing.BarcodeWriter();
            barcodeWriter.Format = barcodeFormat;
            barcodeWriter.Options.Width = (int)labelWidth;
            barcodeWriter.Options.Height = (int)labelHeight;
            barcodeWriter.Options.Margin = 10;

            // Calculate the number of pages needed to print all the labels
            int pageCount = (int)Math.Ceiling((double)barcodes.Count / labelsPerPage);

            // Create a new PDF document and set its properties
            var document = new Document(new RectangleReadOnly(labelWidth + 10, labelHeight + 46));
            document.SetMargins(8, 8, 8, 8);
            
            // Create a new table with 1 column and as many rows as there are barcodes per page
            var table = new PdfPTable(1);
            table.WidthPercentage = 100;
            table.DefaultCell.Padding = 10;

            // Generate the barcodes and add them to the table along with the product details and price
            for (int i = 0; i < pageCount; i++)
            {
                // Clear the table for the new labels
                table.DeleteBodyRows();

                // Add the labels to the table
                for (int j = i * labelsPerPage; j < Math.Min((i + 1) * labelsPerPage, barcodes.Count); j++)
                {
                    var barcodeBitmap = barcodeWriter.Write(barcodes[j].BarcodeText);

                    var image = iTextSharp.text.Image.GetInstance(barcodeBitmap, System.Drawing.Imaging.ImageFormat.Bmp);
                    image.Alignment = Element.ALIGN_RIGHT;
                    // Create a new cell and add the barcode image, product details, and price
                    var cell = new PdfPCell();
                    cell.Border = 0;
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.AddElement(image);

                    var detailsParagraph = new Paragraph(barcodes[j].Details, new Font(Font.FontFamily.TIMES_ROMAN, 8));

                    //var combinedParagraph = new Paragraph();
                    //combinedParagraph.Add(detailsParagraph);

                    //cell.AddElement(combinedParagraph);

                    // Add the cell to the table
                    table.AddCell(cell);
                }

                // Add the table to the document and close it
                PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
                document.Open();
                document.Add(table);
                document.Close();
                foreach (var item in barcodes)
                {
                    PrintBarcode(item);
                }
            }
        }

        public static void PrintBarcode(Barcode barcode)
        {
            var printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);

            // Create a new BarcodeWriter instance and set its properties
            var barcodeWriter = new BarcodeWriter();
            if (printDocument.PrinterSettings.PrinterName.Contains("Xprinter"))
            {
                barcodeWriter.Format = BarcodeFormat.CODE_128;
                barcodeWriter.Options.Width = 150;
                barcodeWriter.Options.Height = 25;
                barcodeWriter.Options.NoPadding = true;
                barcodeWriter.Options.Margin = 0;
                barcodeWriter.Options.PureBarcode = true;
            }else
            {
                barcodeWriter.Format = BarcodeFormat.CODE_128;
                barcodeWriter.Options.Width = 200;
                barcodeWriter.Options.Height = 30;
                barcodeWriter.Options.Margin = 5;
                barcodeWriter.Options.PureBarcode = true;
            }
            
           
            
            
            
            // Generate the barcode image using the barcode text
            var barcodeBitmap = barcodeWriter.Write(barcode.BarcodeText);
            
            
            // Create a new print document and add the barcode image and details to it
           
            printDocument.PrintPage += (sender, e) =>
            {
                if (printDocument.PrinterSettings.PrinterName.Contains("Xprinter"))
                {
                    // Draw the barcode image
                    e.PageSettings.Margins = new Margins(0, 0, 0, 0);
                    e.Graphics.DrawImage(barcodeBitmap, new Point(e.PageBounds.Width - 150, 20));
                    int yOffset = 20;
                    // Draw the barcode details
                    var detailsFont = new System.Drawing.Font("Arial", 5, FontStyle.Bold);
                    var detailsBrush = new SolidBrush(Color.Black);
                    var detailsRect = new System.Drawing.Rectangle(0, barcodeBitmap.Height + yOffset, e.PageBounds.Width, 15);
                    var detailsFormat = new StringFormat();
                    detailsFormat.Alignment = StringAlignment.Far;
                    detailsFormat.LineAlignment = StringAlignment.Center;
                    
                    // Draw the barcode details
                    e.Graphics.DrawString($"*{barcode.BarcodeText}*", detailsFont, detailsBrush, detailsRect, detailsFormat);

                    int maxLineLength = 35;
                    int totalLines = (int)Math.Ceiling((double)barcode.Details.Length / maxLineLength);

                    yOffset += 10;
                    for (int i = 0; i < totalLines; i++)
                    {
                        int startIndex = i * maxLineLength;
                        int length = Math.Min(maxLineLength, barcode.Details.Length - startIndex);
                        string lineText = barcode.Details.Substring(startIndex, length);

                        detailsRect = new System.Drawing.Rectangle(0, barcodeBitmap.Height + yOffset, e.PageBounds.Width, 40);
                        detailsFormat.Alignment = StringAlignment.Far;
                        e.Graphics.DrawString(lineText, detailsFont, detailsBrush, detailsRect, detailsFormat);
                        yOffset += 15;
                    }
                    e.HasMorePages = false;
                }
                else if (printDocument.PrinterSettings.PrinterName.Contains("BIXOLON"))
                {
                    // Draw the barcode image
                    e.Graphics.DrawImage(barcodeBitmap, new Point(2, 10));
                    // Draw the barcode details
                    var detailsFont = new System.Drawing.Font("Arial", 5, FontStyle.Bold);
                    var detailsBrush = new SolidBrush(Color.Black);
                    var detailsRect = new System.Drawing.Rectangle(0, barcodeBitmap.Height + 15, e.PageBounds.Width, 20);
                    var detailsFormat = new StringFormat();
                    detailsFormat.Alignment = StringAlignment.Center;
                    // Draw the barcode details
                    e.Graphics.DrawString($"*{barcode.BarcodeText}*", detailsFont, detailsBrush, detailsRect, detailsFormat);
                    int maxLineLength = 35;
                    int totalLines = (int)Math.Ceiling((double)barcode.Details.Length / maxLineLength);
                    int yOffset = 25;
                    for (int i = 0; i < totalLines; i++)
                    {
                        int startIndex = i * maxLineLength;
                        int length = Math.Min(maxLineLength, barcode.Details.Length - startIndex);
                        string lineText = barcode.Details.Substring(startIndex, length);
                        detailsRect = new System.Drawing.Rectangle(0, barcodeBitmap.Height + yOffset, e.PageBounds.Width, 40);
                        e.Graphics.DrawString(lineText, detailsFont, detailsBrush, detailsRect, detailsFormat);
                        yOffset += 15;
                    }
                }
            };

            // Use the default printer
            var ps = new PrinterSettings();
            printDocument.PrinterSettings = ps;

            // Start printing
            printDocument.Print();
        }

        #region
        //public void printbarcode()
        //{
        //    String FirstHumanReadable = txtFirstBarcodeReadable.Text;
        //    string GTIN14 = txtGTIN14.Text;
        //    string ExpiryDate = txtExpiryDate.Text;
        //    string BatchNo = cmbBatchId.Text;
        //    //string FirstBarcode = txtFirstBarcode.Text;
        //    string FirstBarcode = "01" + txtGTIN14.Text + "17" + txtExpiryDate.Text + "10>6" + cmbBatchId.Text + "21" + txtSerialNo.Text;
        //    int a = Convert.ToInt32(txtIntermediatePackSize.Text);
        //    int b = SecondaryPackSize;
        //    int Qty = a * b;
        //    StreamReader FSOP;
        //    string FileTextLine;
        //    string TempFileTextLine;
        //    if (File.Exists((Application.StartupPath + "\\Intermediate.txt"))) //prn file name
        //    {
        //        FSOP = new StreamReader((Application.StartupPath + "\\Intermediate.txt"));
        //        FileTextLine = FSOP.ReadToEnd();
        //        FSOP.Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("Pcl Command Not Found");
        //        return;
        //    }
        //    TempFileTextLine = FileTextLine;
        //    TempFileTextLine = TempFileTextLine.Replace("#Value1#", ("" + (Barcode + "")));
        //    TempFileTextLine = TempFileTextLine.Replace("#Value2#", ("" + (BarcodeNo + "")));
        //    TempFileTextLine = TempFileTextLine.Replace("#Value3#", ("" + (ExpiryDate + "")));
        //    TempFileTextLine = TempFileTextLine.Replace("#Value4#", ("" + (BatchNo + "")));
        //    RawPrinterHelper.SendStringToPrinter("ZDesigner ZT230-300dpi ZPL", TempFileTextLine); //  Printer Name
        //    MessageBox.Show("Barcode get Printed !");
        //}
        #endregion

        public static async Task PrintEtiquetteAsync(List<Barcode> barcodes, BarcodeFormat barcodeFormat, string pdfFilePath)
        {
            // Create a new BarcodeWriter instance and set its properties
            var barcodeWriter = new ZXing.BarcodeWriter();
            barcodeWriter.Format = barcodeFormat;
            barcodeWriter.Options.Width = 250;
            barcodeWriter.Options.Height = 100;
            barcodeWriter.Options.Margin = 10;

            // Create a new PDF document and set its properties
            var document = new Document(new RectangleReadOnly(PageSize.A4.Width - 36, PageSize.A4.Height));
            document.SetMargins(18, 36, 18, 36);

            // Create a new table with 3 columns and enough rows to fit all the barcodes
            var table = new PdfPTable(3);
            table.WidthPercentage = 100;
            table.DefaultCell.Padding = 10;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            // Generate the barcode and add it to the table along with the product details and price
            for (int i = 0; i < barcodes.Count; i++)
            {
                // Create a new cell and add the product details and price
                var cell = new PdfPCell();
                cell.Padding = 4;
                cell.PaddingTop = -30;
                cell.CellEvent = new CellSpacingEvent(2);
                
                // Add some space between the barcode and the product details/price
                var emptyParagraph = new Paragraph(" ", new Font(Font.FontFamily.TIMES_ROMAN, 6));
                emptyParagraph.Alignment = Element.ALIGN_CENTER;
                cell.AddElement(emptyParagraph);

                // Add the product details and price to the cell
                var detailsParagraph = new Paragraph(barcodes[i].Details, new Font(Font.FontFamily.TIMES_ROMAN, 8));
                detailsParagraph.Alignment = Element.ALIGN_CENTER;
                var priceChunk = new Chunk($"{Convert.ToDecimal(barcodes[i].Price).ToString("N0")}", new Font(Font.FontFamily.TIMES_ROMAN, 50, Font.BOLD));
                var currencyChunk = new Chunk($" FCFA", new Font(Font.FontFamily.TIMES_ROMAN, 6, Font.BOLD));

                var priceParagraph = new Paragraph(priceChunk);
                priceParagraph.Add(currencyChunk);
                priceParagraph.Alignment = Element.ALIGN_CENTER;

                var combinedParagraph = new Paragraph();
                combinedParagraph.Add(priceParagraph);
                combinedParagraph.Add(emptyParagraph);
                combinedParagraph.Add(detailsParagraph);

                cell.AddElement(combinedParagraph);

                // Add the cell to the table
                table.AddCell(cell);
            }


            int remainder = barcodes.Count % 3;
            if(remainder > 0)
            {
                for (int i = 0; i < remainder; i++)
                {
                    // Create a new cell and add the product details and price
                    var cell = new PdfPCell();
                    cell.Padding = 4;
                    cell.PaddingTop = -30;
                    cell.CellEvent = new CellSpacingEvent(2);

                    // Add some space between the barcode and the product details/price
                    var emptyParagraph = new iTextSharp.text.Paragraph(" ", new Font(Font.FontFamily.TIMES_ROMAN, 6));
                    emptyParagraph.Alignment = Element.ALIGN_CENTER;
                    cell.AddElement(emptyParagraph);

                    // Add the product details and price to the cell
                    var detailsParagraph = new iTextSharp.text.Paragraph("  ", new Font(Font.FontFamily.TIMES_ROMAN, 8));
                    detailsParagraph.Alignment = Element.ALIGN_CENTER;
                    var priceChunk = new Chunk($"  ", new Font(Font.FontFamily.TIMES_ROMAN, 50, Font.BOLD));
                    var currencyChunk = new Chunk($" ", new Font(Font.FontFamily.TIMES_ROMAN, 6, Font.BOLD));

                    var priceParagraph = new iTextSharp.text.Paragraph(priceChunk);
                    priceParagraph.Add(currencyChunk);
                    priceParagraph.Alignment = Element.ALIGN_CENTER;

                    var combinedParagraph = new iTextSharp.text.Paragraph();
                    combinedParagraph.Add(priceParagraph);
                    combinedParagraph.Add(emptyParagraph);
                    combinedParagraph.Add(detailsParagraph);

                    cell.AddElement(combinedParagraph);

                    // Add the cell to the table
                    table.AddCell(cell);
                }
            }
            // Add the table to the document and close it
            PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
            document.Open();
            document.Add(table);
            document.Close();
        }

        public static void PrintReceipt(Info_Entreprise entreprise, List<VenteList> list, Sales_Details sales_Details)
        {
            //using (new DonnéeDataContext(mycontrng))
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("           ");
                sb.AppendLine("           ");
                sb.AppendLine("            " + entreprise.Nom.ToUpper() + "          ");
                sb.AppendLine($" {entreprise.Telephone}");
                sb.AppendLine($" {entreprise.Adresse}");
                sb.AppendLine($" {entreprise.Email}");
                sb.AppendLine();

                sb.AppendLine("-------------------------------------");
                sb.AppendLine("Date : " + DateTime.Now.ToString());
                sb.AppendLine("Caisse " + entreprise.Nom + ":");
                sb.AppendLine();
                sb.AppendLine("Item                        Prix_Unité");
                sb.AppendLine("--------------------------------------");
                decimal num2 = default(decimal);
                foreach (VenteList item in list)
                {
                    for (int i = 0; i < item.Quantité; i++)
                    {
                        string designation = "";
                        if (!string.IsNullOrEmpty(item.Type))
                        {
                            designation = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                        }
                        else
                        {
                            designation = $"{item.Marque} {item.Catégorie} {item.Taille}";
                        }
                        sb.AppendLine(designation.Substring(0, 28).PadRight(30) + " " + Convert.ToDecimal(item.Prix_Unitaire).ToString("N0"));
                        num2 += Convert.ToDecimal(item.Prix_Unitaire);
                    }
                }

                sb.AppendLine("--------------------------------------");
                sb.AppendLine("Reduction :               " + sales_Details.Réduction.ToString("N0"));
                sb.AppendLine("Total :                   " + sales_Details.Montant_Total.ToString("N0"));
                sb.AppendLine("Montant Payé :            " + sales_Details.Montant_Net_Payé.ToString("N0"));
                sb.AppendLine("Montant Retourné :        " + sales_Details.Montant_Rétourné.ToString("N0"));
                sb.AppendLine();
                sb.AppendLine("    Merci , à bientôt chez   ");
                sb.AppendLine("        " + entreprise.Nom + "    ");
                sb.AppendLine();
                sb.AppendLine();
                sb.AppendLine("    " + entreprise.Telephone + "   ");
                sb.AppendLine();
                sb.AppendLine(" Quitaye Technologie : +223 71 65 58 33  ");

                Printer.Print(sb.ToString());
            }
        }

        public static async Task PrinteEtiquetteAsync(List<Barcode> barcodes, BarcodeFormat barcodeFormat, string pdfFilePath)
        {
            // Create a new BarcodeWriter instance and set its properties
            var barcodeWriter = new ZXing.BarcodeWriter();
            barcodeWriter.Format = barcodeFormat;
            barcodeWriter.Options.Width = 250;
            barcodeWriter.Options.Height = 100;
            barcodeWriter.Options.Margin = 10;

            // Create a new PDF document and set its properties
            var document = new Document(new RectangleReadOnly(PageSize.A4.Width - 36, PageSize.A4.Height));
            document.SetMargins(18, 36, 18, 36);

            // Create a new table with 1 column and add each barcode to its own cell with some space in between
            var table = new PdfPTable(1);
            table.WidthPercentage = 100;
            table.SpacingBefore = 20;
            table.DefaultCell.Padding = 10;
            table.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            table.DefaultCell.VerticalAlignment = Element.ALIGN_MIDDLE;

            for (int i = 0; i < barcodes.Count; i++)
            {
                // Create a new cell and add the barcode image and product details and price
                var cell = new PdfPCell();
                cell.Border = Rectangle.BOX;
                cell.PaddingBottom = 30;

                // Generate the barcode image

                // Add the barcode image to the cell


                var detailsParagraph = new Paragraph(barcodes[i].Details, new Font(Font.FontFamily.TIMES_ROMAN, 8));
                detailsParagraph.Alignment = Element.ALIGN_CENTER;
                var emptyParagraph = new Paragraph("                 ", new Font(Font.FontFamily.TIMES_ROMAN, 6));
                emptyParagraph.Alignment = Element.ALIGN_CENTER;

                var priceChunk = new Chunk($"{Convert.ToDecimal(barcodes[i].Price).ToString("N0")}", new Font(Font.FontFamily.TIMES_ROMAN, 44, Font.BOLD));
                var currencyChunk = new Chunk($" FCFA", new Font(Font.FontFamily.TIMES_ROMAN, 6, Font.BOLD));
                var priceParagraph = new Paragraph(priceChunk);

                priceParagraph.Add(currencyChunk);
                priceParagraph.Alignment = Element.ALIGN_CENTER;

                var combinedParagraph = new Paragraph();
                combinedParagraph.Add(priceParagraph);
                detailsParagraph.PaddingTop = 20;
                combinedParagraph.Add(emptyParagraph);
                combinedParagraph.Add(detailsParagraph);

                cell.VerticalAlignment = Element.ALIGN_CENTER;
                cell.ExtraParagraphSpace = 10;
                cell.AddElement(combinedParagraph);

                // Add the cell to the table
                table.AddCell(cell);
            }

            // Add the table to the document and close it
            PdfWriter.GetInstance(document, new FileStream(pdfFilePath, FileMode.Create));
            document.Open();
            document.Add(table);
            document.Close();
        }

        public async Task BarcodesToPdf(List<string> barcodeTexts, BarcodeFormat barcodeFormat, string pdfFilePath)
        {
            // Create a new document
            var document = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 50, 50, 50, 50);

            // Create a new MemoryStream to hold the preview version of the document
            var previewStream = new MemoryStream();

            // Create a new PdfWriter instance to write the preview version of the document to the MemoryStream
            var previewWriter = PdfWriter.GetInstance(document, previewStream);

            // Open the document
            document.Open();

            // Create a new BarcodeWriter instance and set its properties
            var barcodeWriter = new ZXing.BarcodeWriter();
            barcodeWriter.Format = barcodeFormat;
            barcodeWriter.Options.Width = 250;
            barcodeWriter.Options.Height = 100;
            barcodeWriter.Options.Margin = 10;

            // Create a new page for each barcode and add it to the document
            foreach (string barcodeText in barcodeTexts)
            {
                // Create a new page and add it to the document
                document.NewPage();

                // Generate the barcode bitmap
                var barcodeBitmap = barcodeWriter.Write(barcodeText);

                // Convert the barcode bitmap to a PDF image
                var barcodeImage = iTextSharp.text.Image.GetInstance(barcodeBitmap, ImageFormat.Bmp);

                // Set the position and size of the barcode image on the page
                barcodeImage.SetAbsolutePosition(50, 500);
                barcodeImage.ScaleAbsolute(200, 80);

                // Add the barcode image to the page
                document.Add(barcodeImage);

                // Dispose of the barcode bitmap
                barcodeBitmap.Dispose();

                // Allow other tasks to execute while the PDF document is being created
                await Task.Delay(10);
            }

            // Close the document
            document.Close();
            //PdfPreview(pdfFilePath, document, previewStream, previewWriter);
        }

        private static void PdfPreview(string pdfFilePath, iTextSharp.text.Document document, MemoryStream previewStream, PdfWriter previewWriter)
        {
            // Create a new document to hold the preview version of the document
            var previewDocument = new iTextSharp.text.Document();

            // Create a new PdfCopy instance to copy the preview version of the document to a file
            var pdfCopy = new PdfCopy(previewDocument, new FileStream(pdfFilePath, FileMode.Create));

            // Open the preview document
            previewDocument.Open();

            // Import each page of the document to a PdfTemplate object and add it to the preview document
            for (int i = 1; i <= document.PageNumber; i++)
            {
                var importedPage = previewWriter.GetImportedPage(new PdfReader(previewStream.GetBuffer()), i);
                var pageTemplate = previewWriter.DirectContent.CreateTemplate(importedPage.Width, importedPage.Height);
                pageTemplate.AddTemplate(importedPage, 0, 0);
                pdfCopy.AddPage(importedPage);
            }

            // Close the preview document
            previewDocument.Close();
        }
    }

    public class CellSpacingEvent : IPdfPCellEvent
    {
        private int cellSpacing;
        public CellSpacingEvent(int cellSpacing)
        {
            this.cellSpacing = cellSpacing;
        }
        void IPdfPCellEvent.CellLayout(PdfPCell cell, Rectangle position, PdfContentByte[] canvases)
        {
            //Grab the line canvas for drawing lines on
            PdfContentByte cb = canvases[PdfPTable.LINECANVAS];
            //Create a new rectangle using our previously supplied spacing
            cb.Rectangle(
                position.Left + this.cellSpacing,
                position.Bottom + this.cellSpacing,
                (position.Right - this.cellSpacing) - (position.Left + this.cellSpacing),
                (position.Top - this.cellSpacing) - (position.Bottom + this.cellSpacing)
                );
            //Set a color
            cb.SetColorStroke(BaseColor.BLACK);
            
            //Draw the rectangle
            cb.Stroke();
        }
    }
}
