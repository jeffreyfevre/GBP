using System.Collections.Generic;
using System.Windows;
using WpfApp1.modèles;
using WpfApp1.wrappers;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        private void searchCompagnons_Click(object sender, RoutedEventArgs e)
        {

            Dictionary<string, string> dicCompagnon = new Dictionary<string, string>();
          
            WrapCompagnon WC = new WrapCompagnon();
            List<Compagnon> lch = WC.searchCompagnonsMultiParam(dicCompagnon);
            dataChantier.ItemsSource = lch;
        }

        private void SwapCreateCompagnon(object sender, RoutedEventArgs e)
        {
            CompagnonWindowC compagnonWindowC = new CompagnonWindowC();
            compagnonWindowC.Show();

        }
        private void createCompagnons_Click(object sender, RoutedEventArgs e)
        {
            //Compagnon chant = new Compagnon(0, name_create.Text, int.Parse(telephone_create.Text), int.Parse(cout_horaire_create.Text), date_embauche_create.Text, compagnon_com_create.Text);
            WrapCompagnon WC = new WrapCompagnon();
            //WC.createCompagnon(chant);
        }


    }
        public partial class CompagnonWindowC : Window
        {
            private void createCompagnon_Click(object sender, RoutedEventArgs e)
            {
                this.Hide();
            }

        }
}
