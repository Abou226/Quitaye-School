using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quitaye_School.Models
{
    public class Sales_Details
    {

        public List<VenteList> VenteLists { get; set; }

        public string Auteur { get; set; }
        public decimal Montant_Payé { get; set; }
        public decimal Réduction { get; set; }
        public decimal Montant_Rétourné { get; set; }
        public decimal Montant_Net_Payé { get; set; }
        public decimal Montant_Total { get; set; }
        public string Num_Vente { get; set; }
        public DateTime Ticket_Date { get; set; }
        public decimal Total_Dépenses { get; set; }
    }
}
