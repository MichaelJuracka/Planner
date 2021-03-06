﻿using Planner.Business.Interfaces;
using Planner.Data.Models;
using Planner.Views.ChooseViews;
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

            ownerComboBox.ItemsSource = mainWindow.Owners;
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
            ChooseRoute chooseRoute = new ChooseRoute(routeManager, mainWindow.Routes.Where(x => x.BoardingRoute == false && x.IsRealRoute == false), mainWindow);
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
        private void chooseBoardingStationButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseStation chooseStation = new ChooseStation(stationManager, mainWindow.Stations.Where(x => x.BoardingStation), mainWindow);
            chooseStation.ShowDialog();
            if (chooseStation.station != null)
            {
                boardingStation = chooseStation.station;
                boardingStationTextBlock.Text = chooseStation.station.ToString();
                boardingStationTextBlock.Visibility = Visibility.Visible;
            }
        }
        private void chooseExitStationButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseStation chooseStation = new ChooseStation(stationManager, mainWindow.Stations.Where(x => !x.BoardingStation), mainWindow);
            chooseStation.stationDataGrid.ItemsSource = mainWindow.Stations.Where(x => x.BoardingStation == false);
            chooseStation.ShowDialog();
            if (chooseStation.station != null)
            {
                exitStation = chooseStation.station;
                exitStationTextBlock.Text = chooseStation.station.ToString();
                exitStationTextBlock.Visibility = Visibility.Visible;
            }
        }
        #endregion
        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            var owner = (Owner)ownerComboBox.SelectedItem;

            try
            {
                var passenger = passengerManager.Add(
                    businesCaseTextBox.Text,
                    firstNameTextBox.Text,
                    secondNameTextBox.Text,
                    phoneTextBox.Text,
                    emailTextBox.Text,
                    additionInfoTextBox.Text,
                    owner,
                    route,
                    null,
                    boardingStation,
                    exitStation
                    );
                mainWindow.Passengers.Add(passenger);

                mainWindow.PassengersDictionary.TryGetValue(route, out var collection);
                ObservableCollection<Passenger> passengers = new ObservableCollection<Passenger>(collection);
                passengers.Add(passenger);
                mainWindow.PassengersDictionary[route] = passengers;

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
