using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.Management;
using System.Windows.Media.Animation;
using Microsoft.SqlServer.Management.Smo;
using Quitaye_School.User_Interface;
using Quitaye_School.Models.Context;
using System.IO;
using DocumentFormat.OpenXml.Drawing.Charts;

namespace Quitaye_School.Models
{
    public class Ticket
    {
        PrintDocument pdoc = null;
        

        public Info_Entreprise Info_Entreprise { get; }
        public List<VenteList> VenteLists { get; }
        public Sales_Details Sales_Details { get; }
        public List<tbl_entreprise_autres_details> Autres_Details { get; }

        public Ticket()
        {

        }
        public Ticket(Info_Entreprise info_Entreprise, 
            List<VenteList> venteLists, 
            Sales_Details sales_Details, 
            List<Models.Context.tbl_entreprise_autres_details> autres_Details)
        {
            Info_Entreprise = info_Entreprise;
            VenteLists = venteLists;
            Sales_Details = sales_Details;
            Autres_Details = autres_Details;
        }
        public void print_Receip()
        {
            pdoc = new PrintDocument();
            pdoc.PrintPage += new PrintPageEventHandler(pdocPrint_Receip_PrintPage);

            // Use the default printer
            PrinterSettings ps = new PrinterSettings();
            pdoc.PrinterSettings = ps;

            // Start printing
            pdoc.Print();
        }

        public void print_Bilan()
        {
            pdoc = new PrintDocument();
            pdoc.PrintPage += new PrintPageEventHandler(pdocPrint_Bilan_PrintPage);

            // Use the default printer
            PrinterSettings ps = new PrinterSettings();
            pdoc.PrinterSettings = ps;

            // Start printing
            pdoc.Print();
        }
        public void pdocPrint_Receip_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int startY = 5;
            int Offset = 5;

            // Create rectangle for drawing.
            float x = 0;
            float y = 0;
            float width = e.PageBounds.Width;
            float height = 65;
            RectangleF drawRect = new RectangleF(x, y, width, height);

            // Draw rectangle to screen.
            Pen blackPen = new Pen(Brushes.White);
            // e.Graphics.DrawRectangle(blackPen, x, y, width, height);
            e.Graphics.DrawRectangle(blackPen, x, y, width, height);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            //e.Graphics.DrawString(Info_Entreprise.Nom, font, drawBrush, drawRect, drawFormat);
            string saveFilePath = $"C:\\Quitaye School\\logo.jpg";
            // Check if the file already exists
            if (File.Exists(saveFilePath))
            {
                // Delete the existing file
                Offset = Offset + 0;
                var image = Image.FromFile(saveFilePath);
                drawRect = new RectangleF(60, 20, 100, height);
                graphics.DrawImage(image, drawRect);

                Offset = Offset +50;
                drawRect = new RectangleF(x, Offset, width, height);
                graphics.DrawString($"{Info_Entreprise.Nom}", new Font("Courier New", 13, FontStyle.Bold),
                                    new SolidBrush(Color.Black), drawRect, drawFormat);
            }
            else if(File.Exists($"C:\\Quitaye School\\logo.png"))
            {
                saveFilePath = $"C:\\Quitaye School\\logo.png";
                Offset = Offset + 0;
                var image = Image.FromFile(saveFilePath);
                drawRect = new RectangleF(75, 20, 60, height);
                graphics.DrawImage(image, drawRect);

                Offset = Offset + 50;
                drawRect = new RectangleF(x, Offset, width, height);
                graphics.DrawString($"{Info_Entreprise.Nom}", new Font("Courier New", 13, FontStyle.Bold),
                                    new SolidBrush(Color.Black), drawRect, drawFormat);
            }
            else if (File.Exists($"C:\\Quitaye School\\logo.bmp"))
            {
                saveFilePath = $"C:\\Quitaye School\\logo.bmp";
                Offset = Offset + 0;
                var image = Image.FromFile(saveFilePath);
                drawRect = new RectangleF(x, Offset, width, height);
                graphics.DrawImage(image, drawRect);

                Offset = Offset + 50;
                drawRect = new RectangleF(x, Offset, width, height);
                graphics.DrawString($"{Info_Entreprise.Nom}", new Font("Courier New", 13, FontStyle.Bold),
                                    new SolidBrush(Color.Black), drawRect, drawFormat);
            }
            else
            {
                Offset = Offset + 0;
                drawRect = new RectangleF(x, Offset, width, height);
                graphics.DrawString($"{Info_Entreprise.Nom}", new Font("Courier New", 13, FontStyle.Bold),
                                    new SolidBrush(Color.Black), drawRect, drawFormat);
            }
            

            //Offset = Offset + 20;
            //graphics.DrawString($"           {Info_Entreprise.Telephone}  ", new Font("Courier New", 8),
            //                    new SolidBrush(Color.Black), startX, startY + Offset);


            Offset = Offset + 20;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString($"{Info_Entreprise.Adresse}", new Font("Courier New", 7, FontStyle.Bold),
                                new SolidBrush(Color.Black), drawRect, drawFormat);

            Offset = Offset + 15;
            drawRect = new RectangleF(x, Offset, width, height);

            graphics.DrawString($"Tel : {Info_Entreprise.Telephone}", new Font("Courier New", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), drawRect, drawFormat);


            foreach (var item in Autres_Details)
            {
                Offset = Offset + 15;
                drawRect = new RectangleF(x, Offset, width, height);
                graphics.DrawString($"{item.Key}: {item.Valeur}", new Font("Courier New", 8),
                                    new SolidBrush(Color.Black), drawRect, drawFormat);
            }

            Offset = Offset + 15;
            string underLine = "-------------------------------------------";
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString(underLine, new Font("Courier New", 8),
                     new SolidBrush(Color.Black), drawRect, drawFormat);


            Offset = Offset + 10; 
            graphics.DrawString("       ", new Font("Courier New", 8),
                                new SolidBrush(Color.Black), startX, startY + Offset);

            var ticket_date = DateTime.Now;
            //if(Sales_Details.Ticket_Date != null)
            {
                ticket_date = Convert.ToDateTime(Sales_Details.Ticket_Date);
            }
            Offset = Offset + 15;
            graphics.DrawString($"Date : {ticket_date}", new Font("Courier New", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 15;
            graphics.DrawString($"N° Ticket : {Sales_Details.Num_Vente}", new Font("Courier New", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 15;
            graphics.DrawString($"Caissier(ère) : {LogIn.profile}", new Font("Courier New", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            
            Offset = Offset + 15;
            graphics.DrawString($"Articles                              ", new Font("Courier New", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            

            Offset = Offset + 15;
            underLine = "----------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            
            {
                //foreach (var item in VenteLists)
                //{
                //    Offset = Offset + 15;
                //    string designation = "";

                //    if (!string.IsNullOrEmpty(item.Type))
                //    {
                //        designation = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                //    }
                //    else designation = $"{item.Marque} {item.Catégorie} {item.Taille}";
                //    var new_designation = "";
                //    if (designation.Length >= 28)
                //    {
                //        new_designation = designation.Substring(0, 28);
                //    }
                //    else new_designation = designation;
                //    graphics.DrawString($"{new_designation} ".PadRight(25, ' ') + alignedNumber(Convert.ToDecimal(item.Prix_Unitaire), 11), new Font("Courier New", 8),
                //         new SolidBrush(Color.Black), startX, startY + Offset);
                //}
            }

            
            foreach(var item in VenteLists)
            {
                Offset = Offset + 15;
                string designation = "";
                //if (Offset + 15 >= e.MarginBounds.Height)
                {
                    e.HasMorePages = true;  // Print next page
                }
                if (!string.IsNullOrEmpty(item.Type))
                {
                    designation = $"{item.Marque} {item.Catégorie} {item.Taille}-{item.Type}";
                }
                else
                {
                    designation = $"{item.Marque} {item.Catégorie} {item.Taille}";
                }

                var new_designation = "";
                if (designation.Length >= 38)
                {
                    new_designation = designation.Substring(0, 38);
                }
                else
                {
                    new_designation = designation;
                }

                
                graphics.DrawString($"{new_designation}", new Font("Courier New", 8, FontStyle.Bold),
                     new SolidBrush(Color.Black), startX, startY + Offset);
                //if (Offset + 15 >= e.MarginBounds.Height)
                {
                    e.HasMorePages = true;  // Print next page
                }
                Offset = Offset + 15;
                graphics.DrawString($"Quantité:{Convert.ToDecimal(item.Quantité).ToString("N0")} " + alignedNumberOne(Convert.ToDecimal(item.Montant), 13), new Font("Courier New", 8, FontStyle.Bold),
                     new SolidBrush(Color.Black), startX, startY + Offset);

                
                //if (Offset + 15 >= e.MarginBounds.Height)
                {
                    e.HasMorePages = true;  // Print next page
                }
            }

            if (Offset + 15 >= e.MarginBounds.Height)
            {
                e.HasMorePages = true;  // Print next page
            }

            Offset = Offset + 15;
            underLine = "-------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            
            Offset = Offset + 15;
            graphics.DrawString("Réduction: ".PadRight(25, ' ') + alignedNumber(Sales_Details.Réduction, 12), new Font("Courier New", 8, FontStyle.Bold),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            if (Offset + 15 >= e.MarginBounds.Height)
            {
                e.HasMorePages = true;  // Print next page
            }
            Offset = Offset + 15;
            graphics.DrawString("Total: ".PadRight(25, ' ') + alignedNumber(Sales_Details.Montant_Total, 12), new Font("Courier New", 8, FontStyle.Bold),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 15;
            graphics.DrawString("Montant Payé: ".PadRight(25, ' ') + alignedNumber(Sales_Details.Montant_Net_Payé, 12), new Font("Courier New", 8, FontStyle.Bold),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 15;
            decimal montant_retourné = 0;
            if (Sales_Details.Montant_Rétourné > 0)
                montant_retourné = Sales_Details.Montant_Rétourné;
            graphics.DrawString("Montant Retourné: ".PadRight(25, ' ') + alignedNumber(montant_retourné, 12), new Font("Courier New", 8, FontStyle.Bold),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 15;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString($"Merci, à bientôt", new Font("Courier New", 12, FontStyle.Bold),
                                new SolidBrush(Color.Black), drawRect, drawFormat);
            //if (Offset + 15 >= e.MarginBounds.Height)
            {
                e.HasMorePages = true;  // Print next page
            }
            Offset = Offset + 15;
            underLine = "------------------";
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString(underLine, new Font("Courier New", 8),
                     new SolidBrush(Color.Black), drawRect, drawFormat);

            Offset = Offset + 15;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString($"Verifiez vos articles", new Font("Courier New", 12, FontStyle.Bold),
                                new SolidBrush(Color.Black), drawRect, drawFormat);
            Offset = Offset + 15;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString($"avant de sortir", new Font("Courier New", 12, FontStyle.Bold),
                                new SolidBrush(Color.Black), drawRect, drawFormat);
            if (Offset + 15 >= e.MarginBounds.Height)
            {
                e.HasMorePages = true;  // Print next page
            }

            Offset = Offset + 20;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString($"Quitaye Technologie : +223 71 65 58 33", new Font("Courier New", 7, FontStyle.Bold),
                                new SolidBrush(Color.Black), drawRect, drawFormat);
            //e.HasMorePages = false;
            Offset = Offset + 80;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString("______________________________", new Font("Courier New", 8, FontStyle.Bold),
                                new SolidBrush(Color.Black), drawRect, drawFormat);
            e.HasMorePages = false;
        }

        
        public void pdocPrint_Bilan_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int startY = 5;
            int Offset = 5;

            // Create rectangle for drawing.
            float x = 0;
            float y = 0;
            float width = e.PageBounds.Width;
            float height = 65;
            RectangleF drawRect = new RectangleF(x, y, width, height);

            // Draw rectangle to screen.
            Pen blackPen = new Pen(Brushes.White);
            // e.Graphics.DrawRectangle(blackPen, x, y, width, height);
            e.Graphics.DrawRectangle(blackPen, x, y, width, height);

            // Set format of string.
            StringFormat drawFormat = new StringFormat();
            drawFormat.Alignment = StringAlignment.Center;
            drawFormat.LineAlignment = StringAlignment.Center;
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            Offset = Offset + 0;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString($"{Info_Entreprise.Nom}", new Font("Courier New", 13, FontStyle.Bold),
                                new SolidBrush(Color.Black), drawRect, drawFormat);

            Offset = Offset + 20;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString($"{Info_Entreprise.Adresse}", new Font("Courier New", 6),
                                new SolidBrush(Color.Black), drawRect, drawFormat);
            Offset = Offset + 20;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString($"{Info_Entreprise.Email}", new Font("Courier New", 8),
                                new SolidBrush(Color.Black), drawRect, drawFormat);
            Offset = Offset + 20;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString($"Telephone : {Info_Entreprise.Telephone}  ", new Font("Courier New", 8),
                                new SolidBrush(Color.Black), drawRect, drawFormat);

            Offset = Offset + 20;
            string underLine = "-------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 10;
            graphics.DrawString("       ", new Font("Courier New", 8),
                                new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString($"Date : {DateTime.Now}", new Font("Courier New", 8),
                                new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString($"Caissier(ère) : {LogIn.profile}", new Font("Courier New", 8),
                                new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 15;
            underLine = "----------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            
            Offset = Offset + 20;
            graphics.DrawString("Réduction: ".PadRight(25, ' ') + alignedNumber(Sales_Details.Réduction, 12), new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString("Total Vente: ".PadRight(25, ' ') + alignedNumber(Sales_Details.Montant_Total, 12), new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString("Total Dépenses: ".PadRight(25, ' ') + alignedNumber(Sales_Details.Total_Dépenses, 12), new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString("Total Net: ".PadRight(25, ' ') + alignedNumber(Sales_Details.Montant_Total-Sales_Details.Réduction-Sales_Details.Total_Dépenses, 12), new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 20;
            underLine = "------------------";
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString(underLine, new Font("Courier New", 8),
                     new SolidBrush(Color.Black), drawRect, drawFormat);

            Offset = Offset + 20;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString($"Quitaye Technologie : +223 71 65 58 33", new Font("Courier New", 7),
                                new SolidBrush(Color.Black), drawRect, drawFormat);

            Offset = Offset + 80;
            drawRect = new RectangleF(x, Offset, width, height);
            graphics.DrawString("______________________________", new Font("Courier New", 8),
                                new SolidBrush(Color.Black), drawRect, drawFormat);
            e.HasMorePages = false;
        }


        string alignedNumber(decimal number, int length)
        {
            return (number.ToString("N0") + " F").PadLeft(length+1, ' ');
        }

        string alignedNumberOne(decimal number, int length)
        {
            return ("Montant :" + number.ToString("N0") + " F").PadLeft(length + 1, ' ');
        }

        private bool IsPrinterConnected(string printerName)
        {
            string query = string.Format("SELECT * from Win32_Printer WHERE Name LIKE '%{0}'", printerName);
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (var printer in searcher.Get())
            {
                if (Convert.ToBoolean(printer["WorkOffline"]))
                    return false;
                if (printer["PrinterStatus"].ToString().Equals("Error"))
                    return false;
                return true;
            }
            return false;
        }
    }
}