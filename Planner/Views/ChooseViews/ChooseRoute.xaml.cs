using Planner.Business.Interfaces;
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
using Planner.Data.Models;
using Planner.Views.AddViews;

namespace Planner.Views.ChooseViews
{
    /// <summary>
    /// Interaction logic for ChooseRoute.xaml
    /// </summary>
    public partial class ChooseRoute : Window
    {
        private readonly IRouteManager routeManager;
        private readonly MainWindow mainWindow;
        private readonly IEnumerable<Route> routes;
        public Route route = null;

        public ChooseRoute(IRouteManager routeManager, IEnumerable<Route> routes, MainWindow mainWindow)
        {
            this.routeManager = routeManager;
            this.mainWindow = mainWindow;
            this.routes = routes;
            
            InitializeComponent();

            fromDatePicker.SelectedDate = DateTime.Now;
            routeDataGrid.ItemsSource = routes;
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (routeDataGrid.SelectedItem != null)
            {
                var route = (Route)routeDataGrid.SelectedItem;
                this.route = route;
                Close();
            }
            else
                MessageBox.Show("Vyberte jízdu");
        }
        private void filterStateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterStateComboBox.SelectedIndex != 0)
                filterRegionComboBox.IsEnabled = false;
            else
                filterRegionComboBox.IsEnabled = true;
        }
        private void filterRegionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (filterRegionComboBox.SelectedIndex != 0)
                filterStateComboBox.IsEnabled = false;
            else
                filterStateComboBox.IsEnabled = true;
        }
        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            var state = (State)filterStateComboBox.SelectedItem;
            var region = (Region)filterRegionComboBox.SelectedItem;
            routeDataGrid.ItemsSource = routeManager.FilterRoutes(routes, fromDatePicker.SelectedDate, toDatePicker.SelectedDate, "", state, region, filterNotRouteBackCheckBox.IsChecked.Value, filterRouteBackCheckBox.IsChecked.Value);
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            filterStateComboBox.ItemsSource = mainWindow.FilterStates;
            filterStateComboBox.SelectedIndex = 0;
            filterRegionComboBox.ItemsSource = mainWindow.FilterRegions;
            filterRegionComboBox.SelectedIndex = 0;
        }
    }
}
