using System.Collections.Generic;
using System.Windows;
using WpfApp1.modeles;
// j'ai faillit gerber mais c'est là
using WpfApp1.modèles;
using WpfApp1.wrappers;


namespace WpfApp1.views
{
    public partial class Home : Window
    {
        public Home()
        {
            InitializeComponent();
            //Fill chantier data grid
            WrapChantier chantiers_manager = new WrapChantier();
            List<Chantier> chants = chantiers_manager.getAllChantier();
            ChantiersDataGrid.ItemsSource = chants;
            //Fill compagnons data grid
            WrapCompagnon compagnons_manager = new WrapCompagnon();
            List<Compagnon> compagnons = compagnons_manager.getAllCompagnon();
            CompagnonsDataGrid.ItemsSource = compagnons;
        }

        private void MainAddChantierButton_Click(object sender, RoutedEventArgs e)
        {
            // Open chantierWindow into mode "new"
            ChantierWindow chantierWindow = new views.ChantierWindow();
            chantierWindow.SetNewMode();
            chantierWindow.Show();
        }

        private void MainAddCompagnonButton_Click(object sender, RoutedEventArgs e)
        {
            // Open compagnonWindow into mode "new"
            CompagnonWindow compagnonWindow = new views.CompagnonWindow();
            compagnonWindow.SetNewMode();
            compagnonWindow.Show();
        }
    }
}
