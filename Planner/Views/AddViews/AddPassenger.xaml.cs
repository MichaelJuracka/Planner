using Planner.Business.Interfaces;
using Planner.Data.Models;
using Planner.Views.ChooseViews;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Planner.Views.AddViews
{
    /// <summary>
    /// Interaction logic for AddPassenger.xaml
    /// </summary>
    public partial class AddPassenger : Window
    {
        private readonly IPassengerManager passengerManager;
        private readonly IRouteManager routeManager;
        private readonly IStationManager stationManager;
        private readonly MainWindow mainWindow;

        private readonly Regex _regex = new Regex("[^0-9.-]+");

        #region Variables
        private Route route = null;
        private Route boardingRoute = null;
        private Station boardingStation = null;
        private Station exitStation = null; 
        #endregion
        public AddPassenger(IPassengerManager passengerManager, IRouteManager routeManager, IStationManager stationManager, MainWindow mainWindow, Route route = null)
        {
            this.passengerManager = passengerManager;
            this.routeManager = routeManager;
            this.stationManager = stationManager;
            this.mainWindow = mainWindow;

            this.route = route;
            InitializeComponent();
            
            if (route != null)
            {
                if (!route.BoardingRoute)
                {
                    routeTextBlock.Text = route.ToString();
                    routeTextBlock.Visibility = Visibility.Visible;
                }
                else
                {
                    boardingRouteTextBlock.Text = route.ToString();
                    boardingRouteTextBlock.Visibility = Visibility.Visible;
                }
            }
        }
        private void bussinesCaseTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        #region Choose
        private void chooseRouteButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseRoute chooseRoute = new ChooseRoute(routeManager, mainWindow);
            chooseRoute.routeDataGrid.ItemsSource = mainWindow.Routes.Where(x => x.BoardingRoute == false && x.IsRealRoute == false);
            chooseRoute.ShowDialog();
            route = chooseRoute.route;
            if (chooseRoute.route != null)
            {
                routeTextBlock.Text = chooseRoute.route.ToString();
                routeTextBlock.Visibility = Visibility.Visible;
                if (chooseRoute.route.RouteBack == true)
                {
                    boardingTextBlock.Text = "Výstupní stanice:";
                    exitTextBlock.Text = "Nástupní stanice:";
                    chooseBoardingStationButton.Content = "Vybrat výstupní stanici";
                    chooseExitStationButton.Content = "Vybrat nástupní stanici";
                }
            }
        }
        private void chooseBoardingRouteButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseRoute chooseRoute = new ChooseRoute(routeManager, mainWindow);
            chooseRoute.routeDataGrid.ItemsSource = mainWindow.Routes.Where(x => x.BoardingRoute == true);
            chooseRoute.ShowDialog();
            boardingRoute = chooseRoute.route;
            if (chooseRoute.route != null)
            {
                boardingRouteTextBlock.Text = chooseRoute.route.ToString();
                boardingRouteTextBlock.Visibility = Visibility.Visible;
            }
        }
        private void chooseBoardingStationButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseStation chooseStation = new ChooseStation(stationManager, mainWindow);
            chooseStation.stationDataGrid.ItemsSource = mainWindow.Stations.Where(x => x.BoardingStation == true);
            chooseStation.ShowDialog();
            boardingStation = chooseStation.station;
            if (chooseStation.station != null)
            {
                boardingStationTextBlock.Text = chooseStation.station.ToString();
                boardingStationTextBlock.Visibility = Visibility.Visible;
            }
        }
        private void chooseExitStationButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseStation chooseStation = new ChooseStation(stationManager, mainWindow);
            chooseStation.stationDataGrid.ItemsSource = mainWindow.Stations.Where(x => x.BoardingStation == false);
            chooseStation.ShowDialog();
            exitStation = chooseStation.station;
            if (chooseStation.station != null)
            {
                exitStationTextBlock.Text = chooseStation.station.ToString();
                exitStationTextBlock.Visibility = Visibility.Visible;
            }
        } 
        #endregion
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainWindow.Passengers.Add(passengerManager.Add(
                    businesCaseTextBox.Text,
                    firstNameTextBox.Text,
                    secondNameTextBox.Text,
                    phoneTextBox.Text,
                    emailTextBox.Text,
                    additionInfoTextBox.Text,
                    ownerTextBox.Text,
                    route,
                    boardingRoute,
                    boardingStation,
                    exitStation
                    ));
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
