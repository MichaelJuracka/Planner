using Planner.Business.Interfaces;
using Planner.Data.Models;
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

namespace Planner.Views.ChooseViews
{
    /// <summary>
    /// Interaction logic for ChooseStation.xaml
    /// </summary>
    public partial class ChooseStation : Window
    {
        private IStationManager stationManager;
        private MainWindow mainWindow;
        public Station station = null;
        public ChooseStation(IStationManager stationManager, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.stationManager = stationManager;

            InitializeComponent();
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (stationDataGrid.SelectedItem != null)
            {
                var station = (Station)stationDataGrid.SelectedItem;
                this.station = station;
                Close();
            }
            else
                MessageBox.Show("Vyberte stanici");
        }
        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            var region = (Region)filterRegionComboBox.SelectedItem;
            stationDataGrid.ItemsSource = stationManager.FilterStations(mainWindow.Stations, "", filterNameTextBox.Text, region);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            filterRegionComboBox.ItemsSource = mainWindow.FilterRegions;
            filterRegionComboBox.SelectedIndex = 0;
        }
    }
}
