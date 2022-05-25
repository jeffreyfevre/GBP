using System.Collections.Generic;
using System.Windows;
using WpfApp1.modeles;
using WpfApp1.wrappers;
using System.Linq;
using System.ComponentModel;

namespace WpfApp1.views
{
    public partial class TraceComptableWindow : Window
    {
        //private void searchDevis_Click(object sender, RoutedEventArgs e)
        //{

        //    Dictionary<string, string> dicChantier = new Dictionary<string, string>();
        //    WrapDevis WC = new WrapDevis();
        //    //List<Devis> lch = WC.getAllDevis().Where(x => x._Id== int.Parse(id_devis.Text)&& tempsprevuDevis.Text==x._TempsPrevu && coutPrevuDevis.Text == x._CoutPrevu ).ToList();
        //    // dataChantier.ItemsSource = lch;
        //}
        public delegate void ChatMsgDelegate();
        public event ChatMsgDelegate MessageUpdate;
        private void ValidButton_Click(object sender, RoutedEventArgs e)
        {

            WrapTraceComptable wrapTraceComptable = new WrapTraceComptable();

            wrapTraceComptable.createTraceComptable(new TraceComptable(0, float.Parse(PriceBox.Text), float.Parse(TimeBox.Text), System.DateTime.Now, (TraceComptable.Types)type , CommantaryBox.Text, new List<Compagnon>(), new List<Materiaux>(), new List<Chantier>()));
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
