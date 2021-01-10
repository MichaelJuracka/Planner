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
    /// Interaction logic for AddStation.xaml
    /// </summary>
    public partial class AddStation : Window
    {
        private IStationManager stationManager;
        private MainWindow mainWindow;
        public AddStation(IStationManager stationManager, MainWindow mainWindow)
        {
            this.stationManager = stationManager;
            this.mainWindow = mainWindow;

            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var region = (Region)regionCombobox.SelectedItem;
            
            try
            {
                mainWindow.Stations.Add(stationManager.Add(nameTextbox.Text, departureTimeTextbox.Text, departurePlaceTextbox.Text, descriptionTextbox.Text, gpsTextbox.Text, isInCzeCheckBox.IsChecked.Value, region));
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

        private void stateCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            State state = (State)stateCombobox.SelectedItem;
            regionCombobox.ItemsSource = mainWindow.Regions.Where(x => x.StateId == state.StateId);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stateCombobox.ItemsSource = mainWindow.States;
        }
    }
}
