using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Quitaye_School.User_Interface
{
    public partial class Type_Sauvegarde : Form
    {
        public Type_Sauvegarde()
        {
            InitializeComponent();
            
            Calltype();
            btnFermer.Click += BtnFermer_Click;
            timer.Enabled = false;
            timer.Interval = 10;
            timer.Start();
            timer.Tick += Timer_Tick;
            btnContinue.Click += BtnContinue_Click;
        }

        private void BtnContinue_Click(object sender, EventArgs e)
        {
            if (Custom_List_de_Sauvegarde.currentobject.Id == 1)
            {
                this.Hide();
                Program.RestartInscription();
            }else
            {
                this.Hide();
                XDocument doc = XDocument.Load(Environment.CurrentDirectory + "\\Entreprise.xml");

                var data = (from s in doc.Descendants("Entreprise")
                            where s.Element("Id").Value == "1"
                            select s).First();
                data.Element("Type").Value = "Hors Ligne";
                doc.Save(Environment.CurrentDirectory + "\\Entreprise.xml");
                Program.RestartLogIn();
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if(Custom_List_de_Sauvegarde.typeupdate == true)
            {
                btnContinue.Visible = true;
            }
        }

        private void BtnFermer_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        List<Custom_List_de_Sauvegarde> list = new List<Custom_List_de_Sauvegarde>();

        //List<ObjectProperty> listobject = new List<ObjectProperty>();
        Timer timer = new Timer();
        
        private async void Calltype()
        {

            var result = await FillTypeAsync();
            foreach (var item in result)
            {
                flowLayoutPanel1.Controls.Add(item);
            }
        }
        private Task<List<Custom_List_de_Sauvegarde>> FillTypeAsync()
        {
            return Task.Factory.StartNew(() => Filltype());
        }
        private List<Custom_List_de_Sauvegarde> Filltype()
        { 
            
            list.Add(new Custom_List_de_Sauvegarde()
            {
                Id = 1,
                Titre = "Sauvegarde en ligne",
                Description = "Gestion et traitement des donnée à l'aide\n d'internet. \nAvec la possibilité d'accéder à vos\n donnée de n'importe quel appareil.",
            });

            list.Add(new Custom_List_de_Sauvegarde()
            {
                Id = 2,
                Titre = "Sauvegarde hors ligne",
                Description = "Gestion et traitement des donnée en interne,\n sans internet. \nLes données sont stockées sur un serveur\n local. Et ne peuvent etre accéssible sur\n internet.",
            });
            return list;
        }
    }

    public class ObjectProperty
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private string titre;

        public string Titre
        {
            get { return titre; }
            set { titre = value; }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set { description = value; }
        }


    }
}
