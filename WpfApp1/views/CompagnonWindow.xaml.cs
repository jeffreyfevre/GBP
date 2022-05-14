
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
        public CompagnonWindow()
        {
            InitializeComponent();
            WrapChantier WC = new WrapChantier();
            List<Chantier> chants = WC.getAllChantier();
            ChantiersDataGrid.ItemsSource = chants;
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
            //List<UIElement> to_hide = new List<UIElement>() { LabelFacture, ChantiersDataGrid, FacturesDataGrid, FacturesDataGrid, EditButton };
            //foreach(UIElement element in to_hide)
            //{
            //    element.Visibility = Visibility.Collapsed;
            //}
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
    }
}
