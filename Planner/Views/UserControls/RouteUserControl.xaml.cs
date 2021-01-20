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
using Planner.Views.AddViews;
using Planner.Views.DetailViews;
using Planner.Views.ImportViews;

namespace Planner.Views.UserControls
{
    /// <summary>
    /// Interaction logic for RouteUserControl.xaml
    /// </summary>
    public partial class RouteUserControl : UserControl
    {
        private IRouteManager routeManager;
        private IPassengerManager passengerManager;
        private IStationManager stationManager;
        private IOfficeManager officeManager;
        private IExportManager exportManager;
        private MainWindow mainWindow;

        private readonly Regex _regex = new Regex("[^0-9.-]+");
        public RouteUserControl()
        {
            InitializeComponent();

            fromDatePicker.SelectedDate = DateTime.Now;
        }
        public void InitManagers(IRouteManager routeManager, IPassengerManager passengerManager, IStationManager stationManager, IOfficeManager officeManager, IExportManager exportManager, MainWindow mainWindow)
        {
            this.routeManager = routeManager;
            this.mainWindow = mainWindow;
            this.passengerManager = passengerManager;
            this.stationManager = stationManager;
            this.officeManager = officeManager;
            this.exportManager = exportManager;
        }
        public void InitGrid()
        {
            routeDataGrid.ItemsSource = mainWindow.Routes;
            filterStateComboBox.ItemsSource = mainWindow.FilterStates;
            filterStateComboBox.SelectedIndex = 0;
            filterRegionComboBox.ItemsSource = mainWindow.FilterRegions;
            filterRegionComboBox.SelectedIndex = 0;
        }
        private void routeDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (routeDataGrid.SelectedItem is Route selectedRoute)
            {
                passengerCountTextBlock.Text = $"Počet osob: {mainWindow.PassengersDictionary[selectedRoute].Count()}";
                licensePlateTextBox.Text = selectedRoute.LicensePlate;
                busTypeComboBox.ItemsSource = mainWindow.BusTypes;
                busTypeComboBox.SelectedItem = mainWindow.BusTypes.SingleOrDefault(x => x.BusTypeId == selectedRoute.BusTypeId);
                busTypeComboBox.SelectedIndex = 0;
                providerComboBox.ItemsSource = mainWindow.Providers;
                providerComboBox.SelectedItem = mainWindow.Providers.SingleOrDefault(x => x.ProviderId == selectedRoute.ProviderId);
            }
            var route = (Route)routeDataGrid.SelectedItem;
            
            if (route != null)
            {
                if (route.IsRealRoute || route.BoardingRoute)
                {
                    addPassengerContextMenu.IsEnabled = false;
                    importPassengersContextMenu.IsEnabled = false;
                }
                else
                {
                    addPassengerContextMenu.IsEnabled = true;
                    importPassengersContextMenu.IsEnabled = true;
                }
            }
        }
        #region Update
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var route = (Route)routeDataGrid.SelectedItem;
            var provider = (Provider)providerComboBox.SelectedItem;
            var busType = (BusType)busTypeComboBox.SelectedItem;

            try
            {
                routeDataGrid.SelectedItem = routeManager.Update(route, provider, licensePlateTextBox.Text, busType);
                routeDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Filter
        private void filterIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            var filterState = (State)filterStateComboBox.SelectedItem;
            var filterRegion = (Region)filterRegionComboBox.SelectedItem;
            bool? filterOrderCreated;
            if (filterOrderCreatedRadioButton.IsChecked.Value)
                filterOrderCreated = true;
            else if (filterOrderNotCreatedRadionButton.IsChecked.Value)
                filterOrderCreated = false;
            else
                filterOrderCreated = null;
            bool? filterAgendaCreated;
            if (filterAgendaCreatedRadionButton.IsChecked.Value)
                filterAgendaCreated = true;
            else if (filterAgendaNotCreatedRadioButton.IsChecked.Value)
                filterAgendaCreated = false;
            else
                filterAgendaCreated = null;
            bool? filterBoardingRoute;
            if (filterBoardingRouteRadioButton.IsChecked.Value)
                filterBoardingRoute = true;
            else if (filterNotBoardingRouteRadioButton.IsChecked.Value)
                filterBoardingRoute = false;
            else
                filterBoardingRoute = null;

            routeDataGrid.ItemsSource = routeManager.FilterRoutes(mainWindow.Routes, fromDatePicker.SelectedDate, toDatePicker.SelectedDate, filterIdTextBox.Text,
                filterState, filterRegion, filterNotRouteBackCheckBox.IsChecked.Value, filterRouteBackCheckBox.IsChecked.Value, filterOrderCreated, filterAgendaCreated, filterBoardingRoute);
        }
        private void filterStateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterStateComboBox.SelectedIndex == 0)
                filterRegionComboBox.IsEnabled = true;
            else
                filterRegionComboBox.IsEnabled = false;
        }
        private void filterRegionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterRegionComboBox.SelectedIndex == 0)
                filterStateComboBox.IsEnabled = true;
            else
                filterStateComboBox.IsEnabled = false;
        }
        #endregion
        #region ContextMenu
        private void deleteContextMenu_Click(object sender, RoutedEventArgs e)
        {
            var route = (Route)routeDataGrid.SelectedItem;
            try
            {
                routeManager.Delete(route.RouteId);
                mainWindow.Routes.Remove(route);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void addPassengerContextMenu_Click(object sender, RoutedEventArgs e)
        {
            var route = (Route)routeDataGrid.SelectedItem;
            AddPassenger addPassenger = new AddPassenger(passengerManager, routeManager, stationManager, mainWindow, route);
            addPassenger.ShowDialog();
        }

        private void detailContextMenu_Click(object sender, RoutedEventArgs e)
        {
            var route = (Route)routeDataGrid.SelectedItem;
            RouteDetail routeDetail = new RouteDetail(passengerManager, stationManager, routeManager, officeManager, exportManager, mainWindow, route, routeDataGrid);
            routeDetail.Show();
        }
        private void importPassengersContextMenu_Click(object sender, RoutedEventArgs e)
        {
            var route = (Route)routeDataGrid.SelectedItem;
            ImportPassenger importPassenger = new ImportPassenger(routeManager, officeManager, mainWindow, route);
            importPassenger.ShowDialog();
        }
        #endregion
    }
}
