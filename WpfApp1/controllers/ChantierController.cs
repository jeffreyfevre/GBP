using System.Collections.Generic;
using System.Windows;
using WpfApp1.modeles;
using WpfApp1.wrappers;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private void searchChantier_Click(object sender, RoutedEventArgs e)
        {

            Dictionary<string, string> dicChantier = new Dictionary<string, string>();
            dicChantier.Add("id_chantier", id_chantier.Text);
            dicChantier.Add("nom_chantier", nom_chantier.Text);
            dicChantier.Add("adresse", adresse_chantier.Text);
            dicChantier.Add("chantier_com", chantier_com.Text);
            WrapChantier WC = new WrapChantier();
            List<Chantier> lch = WC.searchChantierMultiParam(dicChantier);
            dataChantier.ItemsSource = lch;
        }
    }
}
