using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class Ajouter_Role : Form
    {
        public Ajouter_Role()
        {
            InitializeComponent();
        }

        public string ok;

        private void btnFermer_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            if(LogIn.expiré == false)
            {
                using (var donnée = new QuitayeContext())
                {
                    if(txtRole.Text != "")
                    {
                        role = txtRole.Text;

                        AddResponsabilitéAsync(donnée);
                        txtRole.Clear();
                        ok = "Oui";
                        Alert.SShow("Role ajouté avec succès !", Alert.AlertType.Sucess);
                    }
                }
            }
            
        }
        static string role;

        public static Task AddResponsabilitéAsync(QuitayeContext donnée)
        {
            return Task.Factory.StartNew(() => AddResponsabilité());
        }
        public static async void AddResponsabilité()
        {
            using(var donnée = new QuitayeContext())
            {
                var re = from d in donnée.tbl_responsabilité select d;
                int id = 0;
                if (re.Count() != 0)
                {
                    var res = (from d in donnée.tbl_responsabilité orderby d.Id descending select d).First();
                    id = res.Id;
                }
                var s = new Models.Context.tbl_responsabilité();
                s.Auteur = Principales.profile;
                s.Date_Ajout = DateTime.Now;
                s.Responsabilité = role;
                s.Id = id + 1;

                donnée.tbl_responsabilité.Add(s);
                await donnée.SaveChangesAsync();
            }
            
        }
    }
}
