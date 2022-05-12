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

namespace WpfApp1
{
    /// <summary>
    /// Logique d'interaction pour ChantierWindow.xaml
    /// </summary>
    public partial class ChantierWindowC : Window
    {
        public ChantierWindowC()
        {
            InitializeComponent();
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

        private void ChangePage(object sender, MouseButtonEventArgs e)
        {
            this.Content = new views.Home();
        }


    }
}
