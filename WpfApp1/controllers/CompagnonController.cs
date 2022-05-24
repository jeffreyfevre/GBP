using System;
using System.Collections.Generic;
using System.Windows;
using WpfApp1.modeles;
using WpfApp1.wrappers;

namespace WpfApp1.views
{
    public partial class Home : Window
    {
        //private void searchCompagnons_Click(object sender, RoutedEventArgs e)
        //{

        //    Dictionary<string, string> dicCompagnon = new Dictionary<string, string>();

        //    WrapCompagnon WC = new WrapCompagnon();
        //    List<Compagnon> lch = WC.searchCompagnonsMultiParam(dicCompagnon);

        //    dataChantier.ItemsSource = lch;
        //}

    }

    public partial class CompagnonWindow : Window
    {
        public delegate void ChatMsgDelegate();
        public event ChatMsgDelegate MessageUpdate;

        private void createCompagnon_Click(object sender, RoutedEventArgs e)
        {

            WrapCompagnon WC = new WrapCompagnon();
            //Compagnon compagnon = new Compagnon(0,nom.Text,telephone.Text, int.Parse(tarif.Text), dateembauche.Text,commentaire.Text,prenom.Text,new List<modeles.Chantier>()) ;
            //WC.createCompagnon(compagnon);
            MessageUpdate();
            this.Hide();
        }
    }
}
