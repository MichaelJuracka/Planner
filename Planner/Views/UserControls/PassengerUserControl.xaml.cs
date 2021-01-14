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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Planner.Views.UserControls
{
    /// <summary>
    /// Interaction logic for PassengerUserControl.xaml
    /// </summary>
    public partial class PassengerUserControl : UserControl
    {
        private IPassengerManager passengerManager;
        private IStationManager stationManager;
        private MainWindow mainWindow;
        private readonly Regex _regex = new Regex("[^0-9.-]+");

        private Station boardingStation;
        private Station exitStation;
        public PassengerUserControl()
        {
            InitializeComponent();
        }
        public void InitManagers(IPassengerManager passengerManager, IStationManager stationManager, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.passengerManager = passengerManager;
            this.stationManager = stationManager;
        }
        public void InitGrid()
        {
            passengerDataGrid.ItemsSource = mainWindow.Passengers;
            ownerComboBox.ItemsSource = mainWindow.Owners;
            filterOwnerComboBox.ItemsSource = mainWindow.FilterOwners;
            filterOwnerComboBox.SelectedIndex = 0;
        }
        #region Filter
        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            var owner = (Owner)filterOwnerComboBox.SelectedItem;
            passengerDataGrid.ItemsSource = passengerManager.FilterPassengers(mainWindow.Passengers, filterIdTextBox.Text, filterBusinessCaseTextBox.Text, filterFirstNameTextBox.Text, filterSecondNameTextBox.Text, owner, fromDatePicker.SelectedDate, toDatePicker.SelectedDate);
        }
        private void filterNameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
        private void filterBusinessCaseTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
        #endregion
        #region Update
        private void passengerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (passengerDataGrid.SelectedItem is Passenger passenger)
            {
                firstNameTextBox.Text = passenger.FirstName;
                secondNameTextBox.Text = passenger.SecondName;
                phoneTextBox.Text = passenger.Phone;
                emailTextBox.Text = passenger.Email;
                additionalInfoTextBox.Text = passenger.AdditionalInformation;
                boardingStationTextBlock.Text = passenger.BoardingStation.ToString();
                boardingStationTextBlock.Visibility = Visibility.Visible;
                exitStationTextBlock.Text = passenger.ExitStation.ToString();
                exitStationTextBlock.Visibility = Visibility.Visible;
                boardingStation = passenger.BoardingStation;
                exitStation = passenger.ExitStation;
                ownerComboBox.SelectedItem = mainWindow.Owners.SingleOrDefault(x => x.OwnerId == passenger.OwnerId);
            }
        }
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var passenger = (Passenger)passengerDataGrid.SelectedItem;
            var owner = (Owner)ownerComboBox.SelectedItem;

            try
            {
                passengerDataGrid.SelectedItem = passengerManager.Update(passenger, firstNameTextBox.Text, secondNameTextBox.Text, phoneTextBox.Text, emailTextBox.Text, additionalInfoTextBox.Text, boardingStation, exitStation, owner);
                passengerDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
            }
        }
        private void chooseExitStationButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseStation chooseStation = new ChooseStation(stationManager, mainWindow.Stations.Where(x => !x.BoardingStation), mainWindow);
            chooseStation.ShowDialog();
            if (chooseStation.station != null)
            {
                exitStation = chooseStation.station;
                exitStationTextBlock.Text = chooseStation.station.ToString();
            }
        }
        #endregion
    }
}
