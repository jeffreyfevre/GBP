
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using WpfApp1.modeles;
using WpfApp1.wrappers;


namespace WpfApp1.views
{
    /// <summary>
    /// Logique d'interaction pour BuildWindow.xaml
    /// </summary>
    public partial class CompagnonWindow : Window
    {
        public delegate void ViewUpdateEvent();
        public event ViewUpdateEvent UpdateHandler;
        private WrapCompagnon compagnonController = new WrapCompagnon();
        private WrapChantier chantierController = new WrapChantier();

        public CompagnonWindow()
        {
            InitializeComponent();
            List<Chantier> chantiers = chantierController.getAllChantier();
            ChantiersDataGrid.ItemsSource = chantiers;
        }
        private static readonly Regex _regex = new Regex("[0-9]");

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
            EditButton.Visibility = Visibility.Collapsed;
        }
        private static bool IsTextAllowed(string text)
        {
            bool result = _regex.IsMatch(text);
            return result;
        }
        private void HourRateTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox rateInput = sender as TextBox;
            if (!IsTextAllowed(e.Text) || rateInput.Text.Length > 3)
            {
                e.Handled = true;
            }

        }
        private void TelNumberTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox rateInput = sender as TextBox;
            if (!IsTextAllowed(e.Text) || rateInput.Text.Length > 9)
            {
                e.Handled = true;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            SetEditMode();
        }

        private void ValidButton_Click(object sender, RoutedEventArgs e)
        {
            int tarif = 0;
            if (int.TryParse(TarifHorraireBox.Text, out tarif))
            {
                //Compagnon nCompagnon = new Compagnon(
                //    0,
                //    NameBox.Text,
                //    PhoneBox.Text,
                //    tarif,
                //    StartDate.Text,
                //    CommantaryBox.Text,
                //    SurnameBox.Text,
                //    new List<Chantier>()
                //    );
                //compagnonController.createCompagnon(nCompagnon);
            }
            UpdateHandler();
            this.Hide();
        }
    }
}
