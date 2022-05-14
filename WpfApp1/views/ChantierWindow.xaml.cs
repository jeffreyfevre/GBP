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
using System.Windows.Shapes;
using System.Text.RegularExpressions;

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
        }
        public void SetReadMode()
        {
        }
        public void SetNewMode()
        {
            UIElement l_facture = this.FindName( "LabelFacture" ) as UIElement;
            UIElement d_facture = this.FindName( "FacturesDataGrid" ) as UIElement;
            UIElement l_devis = this.FindName( "LabelDevis" ) as UIElement;
            UIElement d_devis = this.FindName( "DevisDataGrid" ) as UIElement;
            List<UIElement> to_hide = new List<UIElement>() {l_facture, l_devis, d_facture, d_devis };
            foreach (UIElement element in to_hide)
            {
                element.Visibility = Visibility.Hidden;
            }
        }
        private static readonly Regex _regex = new Regex("[0-9]");
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
                // event no longuer go further
                e.Handled = true;
            }
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
    }
}
