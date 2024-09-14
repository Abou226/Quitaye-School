using System.ComponentModel;

namespace Quitaye_School.Models
{
    public class Barcode : INotifyPropertyChanged
    {
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public int Id { get; set; }
        public string BarcodeText { get; set; }
        private string details;

        public string Detail { get; set; }
        public string Details
        {
            get { return details; }
            set 
            {
                value = $"{Marque} {Catégorie} {Taille}-{Type}";
                if (details != value)
                {
                    details = value;
                    OnPropertyChanged(nameof(Details));
                }
            }
        }

        public decimal? Price { get; set; }
        private string marque;

        public string Marque
        {
            get { return marque; }
            set 
            { 
                if(marque != value)
                {
                    marque = value;
                    Details = "";
                    OnPropertyChanged(nameof(Marque));
                }
            }
        }

        private string catégorie;

        public string Catégorie
        {
            get { return catégorie; }
            set 
            { 
                if(catégorie != value)
                {
                    catégorie = value;
                    Details = "";
                    OnPropertyChanged(nameof(Catégorie));
                }
            }
        }

        private string taille;

        public string Taille
        {
            get { return taille; }
            set 
            { 
                if(taille != value)
                {
                    taille = value;
                    Details = "";
                    OnPropertyChanged(nameof(Taille));
                }
            }
        }

        private string type;

        public string Type
        {
            get { return type; }
            set 
            {
                if(type != value)
                {
                    type = value;
                    Details = "";
                    OnPropertyChanged(nameof(Type));
                }
            }
        }
    }
}
