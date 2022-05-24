using System.Collections.Generic;
using System.Windows;
using WpfApp1.modeles;
using WpfApp1.wrappers;
using System.Linq;


namespace WpfApp1.views
{
    public partial class Home : Window
    {
        //private void searchChantier_Click(object sender, RoutedEventArgs e)
        //{

        //    Dictionary<string, string> dicChantier = new Dictionary<string, string>();
        //    WrapChantier WC = new WrapChantier();
        //    List<Chantier> lch = WC.getAllChantier().Where(x => x._NomChantier == int.Parse(Nam.Text) || x._NomChantier == nom_chantier.Text || x._Adresse == adresse_chantier.Text ||
        //    x._Telephone == telephone_chantier.Text).ToList();
        //    dataChantier.ItemsSource = lch;
        //}


    }
    public partial class ChantierWindow : Window
    {
        public delegate void ChatMsgDelegate();
        public event ChatMsgDelegate MessageUpdate;
        private void ValidButton_Click(object sender, RoutedEventArgs e)
        {

            WrapChantier WC = new WrapChantier();
            Chantier chant = new Chantier(0,"1",PhoneBox.Text, StartDate.SelectedDate.Value, StartDate.SelectedDate.Value, Chantier.State.Encours, AdressBox.Text, ZipCodeBox.Text, NameBox.Text, CommantaryBox.Text, null, null, null);
            WC.createChantier(chant);
            MessageUpdate();
            this.Hide();
        }
    }
}
