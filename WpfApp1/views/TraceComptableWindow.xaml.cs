using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using WpfApp1.wrappers;
using WpfApp1.modeles;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace WpfApp1.views
{
    /// <summary>
    /// Logique d'interaction pour ChantierWindow.xaml
    /// </summary>
    public partial class TraceComptableWindow : Window
    {
        public delegate void ViewUpdateEvent();
        public event ViewUpdateEvent UpdateHandler;
        public  int type = -1;
        private WrapChantier chantierController = new WrapChantier();
        private List<Chantier> chantiers = new List<Chantier>();

        public TraceComptableWindow()
        {
            InitializeComponent();
        }
        public void SetEditMode()
        {
            ContentGrid.IsEnabled = true;
            ValidButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Visible;
            EditButton.Visibility = Visibility.Hidden;
        }

            public void SetReadMode()
        {
            ContentGrid.IsEnabled = false;
            ValidButton.Visibility = Visibility.Hidden;
            CancelButton.Visibility = Visibility.Hidden;
            EditButton.Visibility = Visibility.Visible;
        }
        public void SetNewMode()
        {
            //EditButton.Visibility = Visibility.Collapsed;
        }
        private static readonly Regex _regex = new Regex("[0-9]");
        private static bool IsTextAllowed(string text)
        {
            bool result = _regex.IsMatch(text);
            return result;
        }
        private void TelNumberTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox rateInput = sender as TextBox;
            if (!IsTextAllowed(e.Text) || rateInput.Text.Length > 9)
            {
                // event no longuer go further
                e.Handled = true;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            SetEditMode();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
        bool _shown;

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            WrapTraceComptable wrapDevis = new WrapTraceComptable();

            ChantierComboBox.ItemsSource = chantierController.getAllChantier();

            // Your code here.
        }

        private void DevisDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ChantierComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            JObject json = JObject.Parse(ChantierComboBox.SelectedItem.ToString());

            Chantier t = chantierController.readChantier(int.Parse(json["_Id"].ToString()));
            chantiers.Add(t);

            ChantierDataGrid.ItemsSource = chantiers;
            ChantierDataGrid.Items.Refresh();

        }
    }
}
