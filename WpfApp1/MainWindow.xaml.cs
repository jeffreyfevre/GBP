using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.modeles;
using WpfApp1.modèles;
using WpfApp1.tests;
using WpfApp1.wrappers;
using WpfApp1.wrapper;


namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Chantier> chants = new List<Chantier>();
        List<person> people = new List<person>();
        List<Facture> factures = new List<Facture>();
        List<Devis> devis = new List<Devis>();
        List<Compagnon> compagnons = new List<Compagnon>();
        CompagnonWindowC compagnonWindowC = new CompagnonWindowC();
        ChantierWindowC chantierWindowC = new ChantierWindowC();
        public MainWindow()
        {

            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            init();
            compagnonWindowC.MessageUpdate += OnLoaded;
            chantierWindowC.MessageUpdate += OnLoaded;
        }

        private void OnLoaded()
        {
            init();
        }
        public void init()
        {
            manageBDD bddManager = new manageBDD();
            bddManager.test();

            WrapChantier WC = new WrapChantier();
            chants = WC.getAllChantier();         
            dataChantier.ItemsSource = chants;

            WrapFacture WCF = new WrapFacture();
            factures = WCF.getAllFacture();
            datafacture.ItemsSource = factures;

            WrapDevis WDD = new WrapDevis();
            devis = WDD.getAllDevis();
            datadevis.ItemsSource = devis;

            WrapCompagnon wrapCompagnon = new WrapCompagnon();
            compagnons = wrapCompagnon.getAllCompagnon();
            dataCompagnon.ItemsSource = compagnons;
        }

        private List<person> GetPeople()
        {

            List<person> people = new List<person>();
            people.Add(new person() { PersonId = 1, Name = "jeffrey", Surname = "fevre", Profession = "dev" });
            people.Add(new person() { PersonId = 2, Name = "flo", Surname = "chav", Profession = "dev" });
            people.Add(new person() { PersonId = 3, Name = "arnal", Surname = "florez", Profession = "dev" });
            return people;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("coucou");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void datagridjeff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void RichTextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }


    }

    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}
