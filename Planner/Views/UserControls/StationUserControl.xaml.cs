using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Planner.Business.Interfaces;
using Planner.Data.Models;

namespace Planner.Views.UserControls
{
    /// <summary>
    /// Interaction logic for StationUserControl.xaml
    /// </summary>
    public partial class StationUserControl : UserControl
    {
        private MainWindow mainWindow;
        private IStationManager stationManager;

        private readonly Regex _regex = new Regex("[^0-9.-]+");
        public StationUserControl()
        {
            InitializeComponent();
        }
        public void InitGrid()
        {
            dataGridStation.ItemsSource = mainWindow.Stations;
            filterRegionCombobox.ItemsSource = mainWindow.FilterRegions;
            filterRegionCombobox.SelectedIndex = 0;
        }
        public void InitManagers(IStationManager stationManager, MainWindow mainWindow)
        {
            this.stationManager = stationManager;
            this.mainWindow = mainWindow;

        }
        #region Update
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var station = (Station)dataGridStation.SelectedItem;
            var region = (Region)regionCombobox.SelectedItem;
            
            try
            {
                dataGridStation.SelectedItem = stationManager.Update(station, nameTextbox.Text, departureTimeTextbox.Text, departurePlaceTextbox.Text, descriptionTextbox.Text, gpsTextbox.Text, isInCzeCheckBox.IsChecked.Value, region);
                dataGridStation.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridStation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridStation.SelectedItem is Station)
            {
                var station = (Station)dataGridStation.SelectedItem;
                nameTextbox.Text = station.Name;
                departureTimeTextbox.Text = station.DepartureTime;
                departurePlaceTextbox.Text = station.DeparturePlace;
                descriptionTextbox.Text = station.Description;
                gpsTextbox.Text = station.Gps;
                isInCzeCheckBox.IsChecked = station.BoardingStation;
                stateCombobox.ItemsSource = null;
                stateCombobox.ItemsSource = mainWindow.States;
                stateCombobox.SelectedItem = mainWindow.States.SingleOrDefault(x => x.StateId == station.Region.StateId);
                var state = (State)stateCombobox.SelectedItem;
                regionCombobox.ItemsSource = mainWindow.Regions.Where(x => x.StateId == state.StateId);
                regionCombobox.SelectedItem = mainWindow.Regions.SingleOrDefault(x => x.RegionId == station.RegionId);
            }
        }
        private void stateCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var state = (State)stateCombobox.SelectedItem;

            if (stateCombobox.ItemsSource != null)
                regionCombobox.ItemsSource = mainWindow.Regions.Where(x => x.StateId == state.StateId);
        } 
        #endregion
        #region Filter
        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            var region = (Region)filterRegionCombobox.SelectedItem;
            bool? filterIsBoardingStation;
            if (boardingFilterCheckbox.IsChecked.Value)
                filterIsBoardingStation = true;
            else if (exitFilterCheckbox.IsChecked.Value)
                filterIsBoardingStation = false;
            else
                filterIsBoardingStation = null;

            dataGridStation.ItemsSource = stationManager.FilterStations(mainWindow.Stations, filterIdTextbox.Text, filterNameTextbox.Text, region, filterIsBoardingStation);
        }
        private void filterIdTextbox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        } 
        #endregion
    }
}
