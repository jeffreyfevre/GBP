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
            dicCompagnon.Add("id_compagnon", id_compagnon.Text);
            dicCompagnon.Add("name", name_create.Text);
            dicCompagnon.Add("telephone", telephone_create.Text);
            dicCompagnon.Add("cout_horaire", cout_horaire_create.Text);
            dicCompagnon.Add("date_embauche", date_embauche_create.Text);
            dicCompagnon.Add("compagnon_com", compagnon_com_create.Text);
            WrapCompagnon WC = new WrapCompagnon();
            List<Compagnon> lch = WC.searchCompagnonsMultiParam(dicCompagnon);
            dataChantier.ItemsSource = lch;
        }
        private void createCompagnons_Click(object sender, RoutedEventArgs e)
        {
            Compagnon chant = new Compagnon(0, name_create.Text, int.Parse(telephone_create.Text), int.Parse(cout_horaire_create.Text), date_embauche_create.Text, compagnon_com_create.Text);
            WrapCompagnon WC = new WrapCompagnon();
            WC.createCompagnon(chant);
        }
    }
}
