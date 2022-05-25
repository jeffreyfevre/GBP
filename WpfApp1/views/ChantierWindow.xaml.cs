﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Text.RegularExpressions;
using WpfApp1.wrappers;
using WpfApp1.modeles;
using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        private WrapTraceComptable chantierTraceComptable = new WrapTraceComptable();
        List<TraceComptable> traceComptables = new List<TraceComptable>();

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
            this.Hide();
        }
        bool _shown;

        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            WrapTraceComptable wrapDevis = new WrapTraceComptable();

            DevisList.ItemsSource = wrapDevis.getAllDevis();
            FactureList.ItemsSource = wrapDevis.getAllFacture();

            // Your code here.
        }

        private void DevisDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void DevisList_TouchDown(object sender, TouchEventArgs e)
        {
        }

        private void DevisList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            JObject json = JObject.Parse(DevisList.SelectedItem.ToString());

            TraceComptable t = chantierTraceComptable.readTraceComptable(int.Parse(json["_Id"].ToString()));           
            traceComptables.Add(t);
   
            DevisDataGrid.ItemsSource = traceComptables;
            DevisDataGrid.Items.Refresh();
        }
    }
}
