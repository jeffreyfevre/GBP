using System.Collections.Generic;
using System.Windows;
using WpfApp1.modeles;
// j'ai faillit gerber mais c'est là
using WpfApp1.modèles;
using WpfApp1.wrappers;
using WpfApp1.wrapper;


namespace WpfApp1.views
{
    public partial class Home : Window
    {
        
        private List<Chantier> chantiers;
        private List<Facture> factures;
        private List<Devis> devis;
        private List<Compagnon> compagnons;
        public Home()
        {
            InitializeComponent();
            init();
        }
        private void OnLoaded()
        {
            init();
        }
        public void init()
        {
            manageBDD bddManager = new manageBDD();
            bddManager.test();

            //Fill chantier data grid
            WrapChantier WC = new WrapChantier();
            chantiers = WC.getAllChantier();
            ChantiersDataGrid.ItemsSource = chantiers;

            //WrapFacture WCF = new WrapFacture();
            //factures = WCF.getAllFacture();
            //datafacture.ItemsSource = factures;

            //WrapDevis WDD = new WrapDevis();
            //devis = WDD.getAllDevis();
            //datadevis.ItemsSource = devis;

            //Fill compagnons data grid
            WrapCompagnon wrapCompagnon = new WrapCompagnon();
            compagnons = wrapCompagnon.getAllCompagnon();
            CompagnonsDataGrid.ItemsSource = compagnons;
            //System.Console.WriteLine(CompagnonsDataGrid.Items[0]);
            //foreach(System.Windows.Controls.DataGridColumn col in CompagnonsDataGrid.Columns)
            //{
            //    System.Console.WriteLine( col.Header );
            //}
        }

        private void MainAddChantierButton_Click(object sender, RoutedEventArgs e)
        {
            OpenChantierWindow();
        }

        private void MainAddCompagnonButton_Click(object sender, RoutedEventArgs e)
        {
            OpenCompagnonWindow();
        }

        private void AddChantierButton_Click(object sender, RoutedEventArgs e)
        {
            OpenChantierWindow();
        }
        private void AddCompagnonButton_Click(object sender, RoutedEventArgs e)
        {
            OpenCompagnonWindow();
        }
        private void OpenChantierWindow()
        {
            // Open chantierWindow into mode "new"
            ChantierWindow chantierWindow = new ChantierWindow();
            chantierWindow.UpdateHandler += OnLoaded;
            chantierWindow.SetNewMode();
            chantierWindow.Show();
        }
        private void OpenCompagnonWindow()
        {
            // Open compagnonWindow into mode "new"
            CompagnonWindow compagnonWindow = new CompagnonWindow();
            compagnonWindow.UpdateHandler += OnLoaded;
            compagnonWindow.SetNewMode();
            compagnonWindow.Show();
        }
    }
}
