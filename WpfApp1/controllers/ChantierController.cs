using System.Collections.Generic;
using System.Windows;
using WpfApp1.modeles;
using WpfApp1.wrappers;
using System.Linq;
using System.ComponentModel;

namespace WpfApp1.views
{
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
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = true; // this will prevent to close
            this.Hide(); // it'll hide the window
                         // here now you can call any method   
        }
    }
}
