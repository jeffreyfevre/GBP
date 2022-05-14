using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using WpfApp1.wrappers;
using WpfApp1.modeles;

namespace WpfApp1.views
{
    /// <summary>
    /// Logique d'interaction pour ChantierWindow.xaml
    /// </summary>
    public partial class ChantierWindow : Window
    {

        public ChantierWindow()
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
            EditButton.Visibility = Visibility.Collapsed;
            //List<UIElement> to_hide = new List<UIElement>() {LabelFacture, LabelDevis, FacturesDataGrid, DevisDataGrid, EditButton };
            //foreach (UIElement element in to_hide)
            //{
            //    element.Visibility = Visibility.Collapsed;
            //}
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
            SetReadMode();
        }

        private void ValidButton_Click(object sender, RoutedEventArgs e)
        {
            SetReadMode();
        }
    }
}
