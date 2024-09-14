using FontAwesome.Sharp;
using Quitaye_School.Models.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quitaye_School.User_Interface
{
    public partial class PopUps : Form
    {
        private string mycontrng = LogIn.mycontrng;
        public string text;
        public string ok;
        public PopUps() => InitializeComponent();

        private void ClosePanelContext(object sender, EventArgs e)
        {
            if (panelContext.Visible)
            {
                panelContext.Visible = false;
                ClosePanel.IconChar = IconChar.Forward;
            }
            else
            {
                panelContext.Visible = true;
                ClosePanel.IconChar = IconChar.Backward;
            }
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e) => Delete(text);

        public async void Delete(string text)
        {
            if (text == "Vente")
            {
                using (var financeDataContext = new QuitayeContext())
                {
                    MsgBox msgBox = new MsgBox();
                    var st = financeDataContext.tbl_vente.Where(s => (Decimal)s.Id == produitInfo2.Ref).First<tbl_vente>();
                    financeDataContext.tbl_Users.Where((s => s.Prenom + " " + s.Nom == st.Auteur)).First();
                    if (!(produitInfo2.lblStkmax.Text == "Auteur vente : " + Principales.profile) && !(Principales.type_compte.Contains("Administrateur")))
                        return;
                    msgBox.show("Voulez-vous supprimer cet élément ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Information);
                    int num = (int)msgBox.ShowDialog();
                    if (msgBox.clicked == "Non" || !(msgBox.clicked == "Oui"))
                        return;
                    tbl_vente entity = financeDataContext.tbl_vente.SingleOrDefault((x => (Decimal)x.Id == produitInfo2.Ref));
                    financeDataContext.tbl_produits.SingleOrDefault(x => (Decimal)x.Id == produitInfo2.Ref);
                    financeDataContext.tbl_vente.Remove(entity);
                    await financeDataContext.SaveChangesAsync();
                    Alert.SShow("Vente supprimé avec succès.", Alert.AlertType.Sucess);
                    ok = "Oui";
                    Close();
                }
            }
            else
            {
                using (var financeDataContext = new QuitayeContext())
                {
                    MsgBox msgBox = new MsgBox();
                    if (Principales.type_compte.Contains("Administrateur"))
                    {
                        msgBox.show("Voulez-vous supprimer cet élément ?", "Suppression", MsgBox.MsgBoxButton.OuiNon, MsgBox.MsgBoxIcon.Information);
                        int num = (int)msgBox.ShowDialog();
                        if (msgBox.clicked == "Non" || !(msgBox.clicked == "Oui"))
                            return;
                        tbl_arrivée entity = financeDataContext.tbl_arrivée.SingleOrDefault(x => (Decimal)x.Id == produitInfo2.Ref);
                        financeDataContext.tbl_produits.SingleOrDefault((x => (Decimal)x.Id == produitInfo2.Ref));
                        financeDataContext.tbl_arrivée.Remove(entity);
                        await financeDataContext.SaveChangesAsync();
                        Alert.SShow("Achat supprimé avec succès.", Alert.AlertType.Sucess);
                        ok = "Oui";
                        Close();
                    }
                }
            }
        }

        private void btnFermer_Click(object sender, EventArgs e) => Close();

        private void btnModifier_Click(object sender, EventArgs e)
        {
        }
    }
}
