using System.Collections.Generic;
using System.Windows;
using WpfApp1.modeles;
// j'ai faillit gerber mais c'est là
using System.Windows.Forms;
using WpfApp1.wrappers;
using WpfApp1.wrapper;
using System.Windows.Controls;
using System;

namespace WpfApp1.views
{
    public partial class Home : Window
    {

        private List<Chantier> chantiers;
        private List<Facture> factures;
        private List<Devis> devis;
        private List<Compagnon> compagnons;
        CompagnonWindow compagnonWindow = new CompagnonWindow();
        ChantierWindow chantierWindow = new ChantierWindow();
        TraceComptableWindow traceComptableWindow = new TraceComptableWindow();
        public Home()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.WindowStyle = WindowStyle.None;
            init();

            compagnonWindow.MessageUpdate += OnLoaded;
            chantierWindow.MessageUpdate += OnLoaded;
            traceComptableWindow.MessageUpdate += OnLoaded;
        }
        private void OnLoaded()
        {
            init();
        }
        public void init()
        {
            ManageBDD bddManager = new ManageBDD();
            bddManager.initBDD();

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
            //DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
            //DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn();
            //DataTemplate dataTemplate = new DataTemplate();
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
            chantierWindow.UpdateHandler += OnLoaded;
            chantierWindow.SetNewMode();
            chantierWindow.Show();
        }
        private void OpenCompagnonWindow()
        {
            // Open compagnonWindow into mode "new"
            compagnonWindow.UpdateHandler += OnLoaded;
            compagnonWindow.SetNewMode();
            compagnonWindow.Show();
        }

        private void MainAddDevisButton_Click(object sender, RoutedEventArgs e)
        {

            traceComptableWindow.Show();
        }
    }
  
}
