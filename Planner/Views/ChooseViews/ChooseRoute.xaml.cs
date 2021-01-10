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
        private IRouteManager routeManager;
        private MainWindow mainWindow;
        public Route route = null;

        public ChooseRoute(IRouteManager routeManager, MainWindow mainWindow)
        {
            this.routeManager = routeManager;
            this.mainWindow = mainWindow;
            
            InitializeComponent();

            fromDatePicker.SelectedDate = DateTime.Now;
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
            DateTime? fromDate = fromDatePicker.SelectedDate;
            DateTime? toDate = toDatePicker.SelectedDate;
            var state = (State)filterStateComboBox.SelectedItem;
            var region = (Region)filterRegionComboBox.SelectedItem;
            bool routeTo = filterNotRouteBackCheckBox.IsChecked.Value;
            bool routeFrom = filterRouteBackCheckBox.IsChecked.Value;
            routeDataGrid.ItemsSource = routeManager.FilterRoutes(mainWindow.Routes, fromDate, toDate, "", state, region, routeTo, routeFrom);
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
