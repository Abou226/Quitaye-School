using iTextSharp.text.pdf;
using iTextSharp.text;
using PrintAction;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Packaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quitaye_School.User_Interface;
using System.Net.Http.Headers;
using Quitaye_School.Models.Context;

namespace Quitaye_School.Models
{
    public class Custom_Pdf
    {
        public Custom_Pdf(string name, float[] widths = null, string repertoire = "Quitaye School", string connection_string = null)
        {
            Name = name;
            Widths = widths;
            Repertoire = repertoire;
            if(connection_string != null)
            {
                Connection_String = connection_string;
            }
            else
            Connection_String = LogIn.mycontrng;
        }
        private float[] Widths;
        public string Name { get; }
        public string Repertoire { get; }
        public string Connection_String { get; }
        public string EntrepriseName { get; set; }

        public Task PrintFactureToPdfAsync(DataGridView dgw, string title, string soustitre, string descriptSousTitre, Detail_Facture facture, bool paysage = false, bool ecole = false)
        {
            return Task.Factory.StartNew(() => FactureToPdf(dgw, title, soustitre, descriptSousTitre, facture, paysage, ecole));
        }
        private void FactureToPdf(DataGridView dgw, string title, string soustitre, string descriptSousTitre, Detail_Facture facture, bool paysage = false, bool ecole = false)
        {
            BaseColor col = BaseColor.BLUE;
            using (var donnée = new QuitayeContext())
            {
                
                var couleur = "";
                var entreprise = donnée.tbl_entreprise.FirstOrDefault();
                if (entreprise != null)
                {
                    EntrepriseName = entreprise.Nom;
                    couleur = entreprise.Couleur;
                    if (couleur == "Noire")
                    {
                        col = BaseColor.BLACK;
                    }
                    else if (couleur == "Blanc")
                    {
                        col = BaseColor.WHITE;
                    }
                    else if (couleur == "Jaune")
                    {
                        col = BaseColor.YELLOW;
                    }
                    else if (couleur == "Rouge")
                    {
                        col = BaseColor.RED;
                    }
                    else if (couleur == "Rose")
                    {
                        col = BaseColor.PINK;
                    }
                    else if (couleur == "Cyan")
                    {
                        col = BaseColor.CYAN;
                    }
                    else if (couleur == "Vert")
                    {
                        col = BaseColor.GREEN;
                    }
                    else if (couleur == "Gris")
                    {
                        col = BaseColor.GRAY;
                    }
                    else if (couleur == "Gris Foncé")
                    {
                        col = BaseColor.DARK_GRAY;
                    }
                    else if (couleur == "Bleu")
                    {
                        col = BaseColor.BLUE;
                    }
                    else
                    {
                        col = BaseColor.BLACK;
                    }
                }
            }
            Document document = ((!paysage) ? new Document(PageSize.A4, 45f, 45f, 50f, 200f) : new Document(PageSize.A4.Rotate(), 45f, 45f, 50f, 50f));
            string text = "C:\\\\" + Repertoire + "\\" + Name + ".pdf";
            FileStream fileStream = new FileStream(text, FileMode.Create);
            PdfWriter instance = PdfWriter.GetInstance(document, fileStream);
            instance.PageEvent = new HeaderFooterFacture(Connection_String, Repertoire, facture, ecole);
            BaseFont bf = BaseFont.CreateFont("Times-Roman", "Cp1250", embedded: true);
            iTextSharp.text.Font font = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 20f, 1, col);
            PdfPTable pdfPTable = new PdfPTable(dgw.Columns.Count);
            pdfPTable.DefaultCell.Padding = 3f;
            pdfPTable.WidthPercentage = 100f;
            pdfPTable.HorizontalAlignment = 0;
            pdfPTable.DefaultCell.BorderWidth = 1f;
            if (Widths != null)
            {
                pdfPTable.SetWidths(Widths);
            }

            iTextSharp.text.Font font2 = new iTextSharp.text.Font(bf, 10f, 0);
            iTextSharp.text.Font font3 = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 16f, 1, BaseColor.BLACK);
            PdfPCell pdfPCell = new PdfPCell(new Phrase(EntrepriseName, font));
            pdfPCell.BorderWidth = 0f;
            PdfPCell pdfPCell2 = new PdfPCell(new Phrase(title.ToUpper(), font3));
            pdfPCell2.BorderWidth = 0f;
            pdfPCell2.HorizontalAlignment = 0;
            PdfPTable pdfPTable2 = new PdfPTable(3);
            pdfPTable2.WidthPercentage = 100f;
            pdfPTable2.DefaultCell.BorderWidth = 0f;
            pdfPTable2.AddCell(pdfPCell);
            pdfPTable2.AddCell(" ");
            pdfPTable2.HorizontalAlignment = 0;
            pdfPTable2.DefaultCell.HorizontalAlignment = 2;
            pdfPTable2.AddCell(pdfPCell2);
            iTextSharp.text.Font font4 = FontFactory.GetFont("\u202aC:\\Windows\\Fonts\\calibrib.ttf", 9f, 1, BaseColor.BLACK);
            PdfPCell pdfPCell3 = new PdfPCell(new Phrase(soustitre + " : ", font4));
            PdfPCell pdfPCell4 = new PdfPCell(new Phrase("Date :", font4));
            iTextSharp.text.Font font5 = FontFactory.GetFont("\u202aC:\\Windows\\Fonts\\calibrib.ttf", 9f, 0, BaseColor.BLACK);
            PdfPCell pdfPCell5 = new PdfPCell(new Phrase(DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss"), font5));
            pdfPCell5.BorderWidth = 0f;
            pdfPCell5.Colspan = 2;
            PdfPCell pdfPCell6 = new PdfPCell(new Phrase(descriptSousTitre, font5));
            pdfPCell6.BorderWidth = 0f;
            pdfPCell4.BorderWidth = 0f;
            pdfPCell4.HorizontalAlignment = 2;
            pdfPCell3.BorderWidth = 0f;
            PdfPTable pdfPTable3 = new PdfPTable(7);
            pdfPTable3.WidthPercentage = 100f;
            pdfPTable3.DefaultCell.BorderWidth = 0f;
            pdfPTable3.AddCell(pdfPCell3);
            pdfPTable3.AddCell(pdfPCell6);
            pdfPTable3.AddCell(" ");
            pdfPTable3.AddCell(" ");
            pdfPTable3.AddCell(pdfPCell4);
            pdfPTable3.AddCell(pdfPCell5);
            iTextSharp.text.Font font6 = FontFactory.GetFont(iTextSharp.text.Font.FontFamily.TIMES_ROMAN.ToString(), 7f, 0, BaseColor.BLACK);
            PdfPCell pdfPCell7 = new PdfPCell(new Phrase("Adresse pour colis :", font4));
            pdfPCell7.BorderWidth = 0f;
            pdfPCell7.BorderWidthBottom = 1f;
            PdfPCell pdfPCell8 = new PdfPCell(new Phrase("Fournisseur :", font4));
            pdfPCell8.BorderWidth = 0f;
            pdfPCell8.BorderWidthBottom = 1f;
            PdfPCell pdfPCell9 = new PdfPCell(new Phrase("                    ", font4));
            pdfPCell9.BorderWidth = 0f;
            pdfPCell9.BorderWidthBottom = 1f;
            PdfPTable pdfPTable4 = new PdfPTable(5);
            pdfPTable4.WidthPercentage = 100f;
            pdfPTable4.DefaultCell.BorderWidth = 0f;
            pdfPTable4.AddCell(pdfPCell7);
            pdfPTable4.AddCell(" ");
            pdfPTable4.AddCell(" ");
            pdfPTable4.AddCell(pdfPCell8);
            pdfPTable4.AddCell(pdfPCell9);
            Paragraph element = new Paragraph("   ");
            foreach (DataGridViewColumn column in dgw.Columns)
            {
                PdfPCell pdfPCell10 = new PdfPCell(new Phrase(column.HeaderText, font2));
                pdfPCell10.BackgroundColor = new BaseColor(240, 240, 240);
                pdfPTable.AddCell(pdfPCell10);
            }

            foreach (DataGridViewRow item in (IEnumerable)dgw.Rows)
            {
                foreach (DataGridViewCell cell in item.Cells)
                {
                    if (cell.Value == null)
                    {
                        pdfPTable.AddCell(new Phrase("", font2));
                    }
                    else
                    {
                        pdfPTable.AddCell(new Phrase(cell.Value.ToString(), font2));
                    }
                }
            }

            document.Open();
            document.Add(pdfPTable2);
            document.Add(element);
            document.Add(element);
            document.Add(pdfPTable3);
            document.Add(element);
            document.Add(element);
            document.Add(pdfPTable);
            document.Add(element);
            document.Close();
            fileStream.Close();
            Process.Start(text);
        }



    }

    internal class HeaderFooterFacture : PdfPageEventHelper
    {
        private string mycontrng;

        private string repertoire;

        private string slogan;

        private Detail_Facture facture;

        private bool ecole;

        private string recu;

        public HeaderFooterFacture(string connection, string repertoires, Detail_Facture _Facture, bool _ecole, string recu = null)
        {
            mycontrng = connection;
            repertoire = repertoires;
            facture = _Facture;
            ecole = _ecole;
            this.recu = recu;
            var donnéeDataContext = new DonnéeDataContext(mycontrng);
            var tbl_entreprise2 = donnéeDataContext.tbl_entreprise.Where(d => d.Id == 1).First();
            slogan = tbl_entreprise2.Slogan;
        }

        public override void OnEndPage(PdfWriter writer, Document document)
        {
            PdfPTable pdfPTable = new PdfPTable(1);
            pdfPTable.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            pdfPTable.DefaultCell.Border = 0;
            Font font = FontFactory.GetFont("\u202aC:\\Windows\\Fonts\\calibrib.ttf", 7f, 2, BaseColor.BLUE);
            pdfPTable.AddCell(new Paragraph());
            PdfPCell pdfPCell = new PdfPCell(new Phrase(repertoire, font));
            pdfPCell.Border = 0;
            pdfPCell.HorizontalAlignment = 2;
            pdfPTable.AddCell(pdfPCell);
            pdfPTable.AddCell(new Paragraph());
            pdfPTable.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetTop(document.TopMargin) + 40f, writer.DirectContent);
            PdfPTable pdfPTable2 = new PdfPTable(5);
            pdfPTable2.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
            pdfPTable2.DefaultCell.Border = 0;
            if (facture != null)
            {
                font = FontFactory.GetFont("\u202aC:\\Windows\\Fonts\\calibrib.ttf", 8f, 0, BaseColor.BLACK);
                Paragraph paragraph = new Paragraph();
                paragraph.Alignment = 0;
                paragraph.Font = font;
                pdfPCell = new PdfPCell();
                pdfPCell.Border = 0;
                pdfPCell.BorderWidthTop = 1f;
                pdfPCell.PaddingTop = 10f;
                pdfPCell.HorizontalAlignment = 0;
                pdfPCell.Colspan = 3;
                pdfPCell.AddElement(paragraph);
                pdfPTable2.HorizontalAlignment = 0;
                pdfPTable2.AddCell(pdfPCell);
                font = FontFactory.GetFont("\u202aC:\\Windows\\Fonts\\calibrib.ttf", 10f, 1, BaseColor.BLACK);
                if (!ecole)
                {
                    paragraph = new Paragraph();
                    paragraph.Add("Montant\n");
                }
                else if (facture.Type_Operation == null)
                {
                    paragraph = new Paragraph();
                    paragraph.Add("Montant Total");
                }
                else if (facture.Type_Operation == "Scolarité")
                {
                    paragraph = new Paragraph();
                    paragraph.Add("Total Scolarité\n");
                    paragraph.Add("Payement du Jour\n");
                    paragraph.Add("Cumul Payement(s)\n");
                    paragraph.Add("Restant\n");
                }
                else
                {
                    paragraph = new Paragraph();
                    paragraph.Add("Montant Inscription");
                }

                paragraph.Font = font;
                pdfPCell = new PdfPCell();
                pdfPCell.BorderWidth = 0f;
                pdfPCell.BorderWidthTop = 1f;
                pdfPCell.PaddingTop = 10f;
                pdfPCell.HorizontalAlignment = 0;
                pdfPCell.AddElement(paragraph);
                pdfPTable2.AddCell(pdfPCell);
                paragraph = new Paragraph();
                if (!ecole)
                {
                    paragraph.Add(facture.MontantHT.ToString("N0") + " FCFA\n");
                }
                else if (facture.Type_Operation == null)
                {
                    paragraph.Add(facture.MontantHT.ToString("N0") + " FCFA\n");
                }
                else if (facture.Type_Operation == "Scolarité")
                {
                    paragraph.Add(facture.Scolarité.ToString("N0") + " FCFA\n");
                    paragraph.Add(facture.PayementJour.ToString("N0") + " FCFA\n");
                    paragraph.Add(facture.MontantPayée.ToString("N0") + " FCFA\n");
                    paragraph.Add(facture.Restant.ToString("N0") + " FCFA\n");
                }
                else if (facture.Type_Operation == "Inscription")
                {
                    paragraph.Add(facture.MontantPayée.ToString("N0") + " FCFA\n");
                }

                paragraph.Font = font;
                paragraph.Alignment = 2;
                pdfPCell = new PdfPCell();
                pdfPCell.BorderWidth = 0f;
                pdfPCell.BorderWidthTop = 1f;
                pdfPCell.PaddingTop = 10f;
                pdfPCell.HorizontalAlignment = 2;
                pdfPCell.AddElement(paragraph);
                pdfPTable2.AddCell(pdfPCell);
            }

            pdfPTable2.WriteSelectedRows(0, -1, document.LeftMargin, writer.PageSize.GetBottom(document.BottomMargin) - 5f, writer.DirectContent);
        }
    }
}
