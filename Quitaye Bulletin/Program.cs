using Quitaye_School.User_Interface;
using Secure_Quitaye_Bulletin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Quitaye_School.Models;

namespace Quitaye_School
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        private static Models.ExceptionHandler exceptionHandler;
        /// </summary>
        [STAThread]
        static void Main()
        {
            exceptionHandler = new Models.ExceptionHandler();
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.ThreadException += HandleThreadException; 
            Application.SetCompatibleTextRenderingDefault(false);
            if (EntrepriseEmail() != null)
            {
                Application.Run(new LogIn(GetConnectionsStrings.GetSConnectionString()));
            }
            else if (EntrepriseType() != null) {

                //string abc = @"Software\Tanmay\Protection";
                //string pass, productid;
                //Secure scr = new Secure();
                //productid = scr.ProductID();
                //pass = scr.Password(productid + 12 + "MerveilleTechnologique");
                //bool logic = scr.Algorithm(pass, abc, "Quitaye School");
                //if (logic == true)

                    Application.Run(new LogIn()); 
            }
            else Application.Run(new User_Interface.Type_Sauvegarde());
        }

#pragma warning disable CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        private static async void HandleThreadException(object sender, ThreadExceptionEventArgs e)
#pragma warning restore CS1998 // Cette méthode async n'a pas d'opérateur 'await' et elle s'exécutera de façon synchrone
        {
            var ex = (Exception)e.Exception;
            exceptionHandler.HandleException(ex);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            exceptionHandler.HandleException(ex);
        }
        public static void RestartLogInInternet()
        {
            LogIn log = new LogIn(GetConnectionsStrings.GetSConnectionString());
            log.ShowDialog();
        }

        public static void RestartLogIn()
        {
            //string abc = @"Software\Tanmay\Protection";
            //string pass, productid;
            //Secure scr = new Secure();
            //productid = scr.ProductID();
            //pass = scr.Password(productid + 12 + "MerveilleTechnologique");
            //bool logic = scr.Algorithm(pass, abc, "Quitaye School");
            //if (logic == true)
            {
                LogIn log = new LogIn();
                log.ShowDialog();
            }
        }
        public static void RestartInscription()
        {
            Inscription_Entreprise log = new Inscription_Entreprise();
            log.ShowDialog();
        }
        public static string EntrepriseEmail()
        {
            XDocument doc = XDocument.Load(Environment.CurrentDirectory + "\\Entreprise.xml");

            var data = (from s in doc.Descendants("Entreprise")
                        where s.Element("Id").Value == "1" && s.Element("Email").Value != ""
                        select new
                        {
                            Id = int.Parse(s.Element("Id").Value),
                            Name = s.Element("Email").Value,
                        });
            if (data.Count() != 0)
            {
                var d = (from s in doc.Descendants("Entreprise")
                         where s.Element("Id").Value == "1" && s.Element("Email").Value != ""
                         select new
                         {
                             Id = int.Parse(s.Element("Id").Value),
                             Name = s.Element("Email").Value,
                         }).First();
                return d.Name;
            }
            else return null;
        }
        public static string EntrepriseType()
        {
            XDocument doc = XDocument.Load(Environment.CurrentDirectory + "\\Entreprise.xml");

            var data = (from s in doc.Descendants("Entreprise")
                        where s.Element("Id").Value == "1" && s.Element("Type").Value != ""
                        select new
                        {
                            Id = int.Parse(s.Element("Id").Value),
                            Name = s.Element("Type").Value,
                        });
            if (data.Count() != 0)
            {
                var d = (from s in doc.Descendants("Entreprise")
                         where s.Element("Id").Value == "1" && s.Element("Type").Value != ""
                         select new
                         {
                             Id = int.Parse(s.Element("Id").Value),
                             Name = s.Element("Type").Value,
                         }).First();
                return d.Name;
            }
            else return null;
        }
    }
}
