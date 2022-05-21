﻿using System.Collections.Generic;
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
        public delegate void ViewUpdateEvent();
        public event ViewUpdateEvent UpdateHandler;
        private WrapChantier chantierController = new WrapChantier();
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
            this.Close();
        }

        private void ValidButton_Click(object sender, RoutedEventArgs e)
        {
            Chantier nChantier = new Chantier(0,
                Chantier.State.Inconnue,
                AdressBox.Text,
                NameBox.Text,
                CommantaryBox.Text,
                new List<Devis>(),
                new List<modèles.Facture>()
                );
            chantierController.createChantier( nChantier );
            UpdateHandler();
            this.Close();
        }
    }
}