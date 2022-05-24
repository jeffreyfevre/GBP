using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using WpfApp1.modeles;
using WpfApp1.wrappers;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        //private void searchFacture_Click(object sender, RoutedEventArgs e)
        //{

        //    Dictionary<string, string> dicChantier = new Dictionary<string, string>();
        //    dicChantier.Add("id_chantier", id_chantier.Text);
        //    dicChantier.Add("nom_chantier", nom_chantier.Text);
        //    dicChantier.Add("adresse", adresse_chantier.Text);
        //    dicChantier.Add("chantier_com", chantier_com.Text);
        //    WrapFacture WC = new WrapFacture();
        //    List<Facture> lch = WC.searchFacturesMultiParam(dicChantier);
        //    dataChantier.ItemsSource = lch;
        //}

        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true; // this will prevent to close
            this.Hide(); // it'll hide the window
                         // here now you can call any method   
        }
    }
}
