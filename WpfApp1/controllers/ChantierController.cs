using System.Collections.Generic;
using System.Windows;
using WpfApp1.modeles;
using WpfApp1.wrappers;
using System.Linq;


namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private void searchChantier_Click(object sender, RoutedEventArgs e)
        {

            Dictionary<string, string> dicChantier = new Dictionary<string, string>();
            WrapChantier WC = new WrapChantier();
            List<Chantier> lch = WC.getAllChantier().Where(x => x._Id == int.Parse(id_chantier.Text) || x._NomChantier== nom_chantier.Text || x._Adresse == adresse_chantier.Text ||
            x._telephone == telephone_chantier.Text).ToList();
            dataChantier.ItemsSource = lch;
        }


        private void SwapChantierClick(object sender, RoutedEventArgs e)
        {
            ChantierWindowC chantierWindow = new ChantierWindowC();
            chantierWindow.Show();


        }
    }

        public partial class ChantierWindowC : Window
        {
            private void createChantier_Click(object sender, RoutedEventArgs e)
            {
            WrapChantier WC = new WrapChantier();
            Dictionary<string, string> dicChantier = new Dictionary<string, string>();
                Chantier chant = new Chantier(0,Chantier.State.Encours, nomchantier.Text, addrchantier.Text, chantiercom.Text,null,null);
                WC.createChantier(chant);
            this.Hide();
            }
    }
}
