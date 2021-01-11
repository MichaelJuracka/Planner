using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Planner.Business.Interfaces;
using Planner.Data.Models;
using Planner.Views.UserControls;

namespace Planner.Views.AddViews
{
    /// <summary>
    /// Interaction logic for AddRoute.xaml
    /// </summary>
    public partial class AddRoute : Window
    {
        private IRouteManager routeManager;
        private MainWindow mainWindow;
        public AddRoute(IRouteManager routeManager, MainWindow mainWindow)
        {
            this.routeManager = routeManager;
            this.mainWindow = mainWindow;

            InitializeComponent();
        }
        #region Chechboxes
        private void routeBackCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            stateTextBlock.Text = "Výchozí stát:";
            regionTextBlock.Text = "Výchozí oblast:";
        }
        private void routeBackCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            stateTextBlock.Text = "Cílový stát:";
            regionTextBlock.Text = "Cílová oblast:";
        }

        private void boardingRouteCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            stateComboBox.ItemsSource = mainWindow.States.Where(x => x.StateId == 7);
            stateComboBox.SelectedIndex = 0;
            stateComboBox.IsEnabled = false;
            regionComboBox.ItemsSource = mainWindow.Regions.Where(x => x.StateId == 7);
            providerComboBox.ItemsSource = mainWindow.Providers.Where(x => x.ProviderId == 8);
            providerComboBox.SelectedIndex = 0;
            providerComboBox.IsEnabled = false;
            licensePlateTextBox.IsEnabled = false;
            busTypeComboBox.IsEnabled = false;
            realRouteCheckbox.IsEnabled = false;
            realRouteCheckbox.IsChecked = false;
        }

        private void boardingRouteCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            stateComboBox.ItemsSource = mainWindow.States;
            stateComboBox.IsEnabled = true;
            providerComboBox.ItemsSource = mainWindow.Providers;
            providerComboBox.IsEnabled = true;
            licensePlateTextBox.IsEnabled = true;
            busTypeComboBox.IsEnabled = true;
            realRouteCheckbox.IsEnabled = true;
        } 
        #endregion
        private void stateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var state = (State)stateComboBox.SelectedItem;
            if (state != null)
                regionComboBox.ItemsSource = mainWindow.Regions.Where(x => x.StateId == state.StateId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stateComboBox.ItemsSource = mainWindow.States;
            providerComboBox.ItemsSource = mainWindow.Providers;
            busTypeComboBox.ItemsSource = mainWindow.BusTypes;
            busTypeComboBox.SelectedIndex = 0;
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var state = (State)stateComboBox.SelectedItem;
            var region = (Region)regionComboBox.SelectedItem;
            var provider = (Provider)providerComboBox.SelectedItem;
            var busType = (BusType)busTypeComboBox.SelectedItem;

            try
            {
                var route = routeManager.Add(departureDatePicker.SelectedDate, routeBackCheckbox.IsChecked.Value, boardingRouteCheckbox.IsChecked.Value, realRouteCheckbox.IsChecked.Value, state, region, provider, licensePlateTextBox.Text, busType);
                mainWindow.Routes.Add(route);
                mainWindow.PassengersDictionary.Add(route, new ObservableCollection<Passenger>());
                
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
