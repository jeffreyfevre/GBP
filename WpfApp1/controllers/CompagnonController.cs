using System;
using System.Collections.Generic;
using System.Windows;
using WpfApp1.modeles;
using WpfApp1.wrappers;

namespace WpfApp1.views
{
    public partial class CompagnonWindow : Window
    {
        public delegate void ChatMsgDelegate();
        public event ChatMsgDelegate MessageUpdate;

        private void createCompagnon_Click(object sender, RoutedEventArgs e)
        {

            WrapCompagnon WC = new WrapCompagnon();
            Compagnon compagnon = new Compagnon(0,NameBox.Text,PhoneBox.Text, int.Parse(TarifHorraireBox.Text), StartDate.SelectedDate.Value,CommantaryBox.Text,SurnameBox.Text,new List<Chantier>(),new List<TraceComptable>(),new List<TraceComptable>()) ;
            WC.createCompagnon(compagnon);
            MessageUpdate();
            this.Hide();
        }
    }
}
