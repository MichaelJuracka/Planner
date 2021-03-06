﻿using Microsoft.Win32;
using Planner.Business.Interfaces;
using Planner.Data.Models;
using Planner.Views.AddViews;
using Planner.Views.ChooseViews;
using Planner.Views.DetailViews.ClearanceViews;
using Planner.Views.ImportViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Planner.Views.DetailViews
{
    /// <summary>
    /// Interaction logic for RouteDetail.xaml
    /// </summary>
    public partial class RouteDetail : Window
    {
        private readonly IPassengerManager passengerManager;
        private readonly IStationManager stationManager;
        private readonly IRouteManager routeManager;
        private readonly IOfficeManager officeManager;
        private readonly IExportManager exportManager;
        private readonly MainWindow mainWindow;
        private readonly Route route;
        private readonly DataGrid dataGrid;

        private readonly Regex _regex = new Regex("[^0-9.-]+");

        private Station boardingStation;
        private Station exitStation;
        private IEnumerable<Passenger> Passengers { get; set; }
        public RouteDetail(IPassengerManager passengerManager, IStationManager stationManager, IRouteManager routeManager, IOfficeManager officeManager, IExportManager exportManager, MainWindow mainWindow, Route route, DataGrid dataGrid)
        {
            this.passengerManager = passengerManager;
            this.stationManager = stationManager;
            this.routeManager = routeManager;
            this.officeManager = officeManager;
            this.exportManager = exportManager;
            this.mainWindow = mainWindow;
            this.route = route;
            this.dataGrid = dataGrid;

            InitializeComponent();

            #region Display
            Title = route.ToString();
            routeTextBlock.Text = route.ToString();

            Passengers = GetPassengerCollection(route);
            passengerDataGrid.ItemsSource = Passengers;

            if (route.IsRealRoute || route.BoardingRoute)
            {
                sortOutButton.IsEnabled = false;
                boardingRouteAddButton.IsEnabled = false;
                passengerImportButton.IsEnabled = false;
                passengerAddButton.IsEnabled = false;

                if (route.BoardingRoute)
                {
                    makeAgendaButton.IsEnabled = false;
                    clearanceButton.IsEnabled = false;
                    boardingRouteColumn.Visibility = Visibility.Hidden;
                    seatNumberColumn.Visibility = Visibility.Hidden;
                    removeRealRouteButton.Visibility = Visibility.Hidden;
                }
                if (route.RouteBack)
                {
                    if (route.BoardingRoute)
                        clearanceButton.IsEnabled = false;
                    makeOrderButton.IsEnabled = false;
                    routeColumn.Visibility = Visibility.Hidden;
                    realRouteColumn.Visibility = Visibility.Hidden;
                    removeBoardingRouteButton.Visibility = Visibility.Hidden;
                }
            }
            else
            {
                if (route.RouteBack)
                    clearanceButton.IsEnabled = false;
                makeAgendaButton.IsEnabled = false;
                makeOrderButton.IsEnabled = false;
                emailTabControl.Visibility = Visibility.Visible;
                routeColumn.Visibility = Visibility.Hidden;
                departureTimeColumn.Visibility = Visibility.Hidden;
            }
            #endregion
        }
        #region WindowLoad, Grid
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            numberOfPassengers.Text = passengerDataGrid.Items.Count.ToString();
            filterRegionComboBox.ItemsSource = mainWindow.FilterRegions.Where(x => x.StateId == 7);
            filterRegionComboBox.SelectedIndex = 0;
            ownerComboBox.ItemsSource = mainWindow.Owners;
            filterOwnerComboBox.ItemsSource = mainWindow.FilterOwners;
            filterOwnerComboBox.SelectedIndex = 0;

            RenderStationCounts();
            RenderExports();
        }
        private void passengerDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var passengers = passengerDataGrid.SelectedItems;
            numberOfMarked.Text = passengers.Count.ToString();

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
                ownerComboBox.SelectedItem = mainWindow.Owners.FirstOrDefault(x => x.OwnerId == passenger.OwnerId);
            }
        }
        #endregion
        #region Filter
        private void filterButton_Click(object sender, RoutedEventArgs e)
        {
            var region = (Region)filterRegionComboBox.SelectedItem;
            if (region.RegionId == 0)
                region = null;
            var owner = (Owner)filterOwnerComboBox.SelectedItem;
            passengerDataGrid.ItemsSource = SortPassengersByStations(route, passengerManager.FilterPassengers(Passengers, filterIdTextBox.Text, filterBusinessCaseTextBox.Text, filterNameTextBox.Text, filterSecondNameTextBox.Text, owner, region: region));
        }
        private void filterBusinessCaseTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
        private void filterIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
        #endregion
        #region Update
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
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var passenger = (Passenger)passengerDataGrid.SelectedItem;
            var owner = (Owner)ownerComboBox.SelectedItem;

            try
            {
                passenger = passengerManager.Update(passenger, firstNameTextBox.Text, secondNameTextBox.Text, phoneTextBox.Text, emailTextBox.Text, additionalInfoTextBox.Text, boardingStation, exitStation, owner);
                passengerDataGrid.SelectedItem = passenger;
                passengerDataGrid.Items.Refresh();
                RenderStationCounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region RemoveRouteButtons
        private void removeRealRouteButton_Click(object sender, RoutedEventArgs e)
        {
            var passengers = passengerDataGrid.SelectedItems;

            if (passengers.Count == 0)
            {
                MessageBox.Show("Vyberte cestující");
                return;
            }

            try
            {
                foreach (Passenger p in passengers)
                    passengerManager.RemoveRoute(p, true, false);
                passengerDataGrid.ItemsSource = GetPassengerCollection(route);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void removeBoardingRouteButton_Click(object sender, RoutedEventArgs e)
        {
            var passengers = passengerDataGrid.SelectedItems;
            if (passengers.Count == 0)
            {
                MessageBox.Show("Vyberte cestující");
                return;
            }

            try
            {
                foreach (Passenger p in passengers)
                    passengerManager.RemoveRoute(p, false, true);
                passengerDataGrid.ItemsSource = GetPassengerCollection(route);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            passengerDataGrid.Items.Refresh();
        }
        #endregion
        #region Add, Import Passengers
        private void passengerImportButton_Click(object sender, RoutedEventArgs e)
        {
            if (!route.IsRealRoute || !route.BoardingRoute)
            {
                ImportPassenger importPassenger = new ImportPassenger(routeManager, officeManager, mainWindow, route);
                importPassenger.ShowDialog();
                var collection = GetPassengerCollection(route);
                passengerDataGrid.ItemsSource = collection;
                Passengers = collection;
                RenderStationCounts();
                numberOfMarked.Text = Passengers.Count().ToString();
            }
        }
        private void passengerAddButton_Click(object sender, RoutedEventArgs e)
        {
            if (!route.IsRealRoute || !route.BoardingRoute)
            {
                AddPassenger addPassenger = new AddPassenger(passengerManager, routeManager, stationManager, mainWindow, route);
                addPassenger.ShowDialog();
                var collection = GetPassengerCollection(route);
                passengerDataGrid.ItemsSource = collection;
                Passengers = collection;
                RenderStationCounts();
                numberOfMarked.Text = Passengers.Count().ToString();
            }
        }
        #endregion
        #region SortOut, Clearance
        private void sortOutButton_Click(object sender, RoutedEventArgs e)
        {
            var passengers = passengerDataGrid.SelectedItems;
            if (passengers.Count == 0)
            {
                MessageBox.Show("Vyberte cestující");
                return;
            }

            ChooseRoute chooseRoute = new ChooseRoute(routeManager, mainWindow.Routes.Where(x => x.IsRealRoute == true && x.DepartureDate == route.DepartureDate), mainWindow);
            chooseRoute.ShowDialog();

            if (chooseRoute.route != null && chooseRoute.route.BusTypeId != null)
            {
                if (chooseRoute.route.BusType.NumberOfSeats < passengers.Count + GetPassengerCollection(chooseRoute.route).Count())
                {
                    MessageBox.Show("V této jízdě je příliš cestujících");
                    return;
                }
                ObservableCollection<Passenger> collection = new ObservableCollection<Passenger>(mainWindow.PassengersDictionary[chooseRoute.route]);
                foreach (Passenger p in passengers)
                {
                    passengerManager.UpdateRoute(p, chooseRoute.route, true, false);
                    collection.Add(p);
                }
                mainWindow.PassengersDictionary[chooseRoute.route] = collection;
                passengerDataGrid.Items.Refresh();
            }

        }
        private void boardingRouteAddButton_Click(object sender, RoutedEventArgs e)
        {
            var passengers = passengerDataGrid.SelectedItems;
            if (passengers.Count == 0)
            {
                MessageBox.Show("Vyberte cestující");
                return;
            }

            ChooseRoute chooseRoute = new ChooseRoute(routeManager, mainWindow.Routes.Where(x => x.BoardingRoute == true && x.DepartureDate == route.DepartureDate), mainWindow);
            chooseRoute.ShowDialog();

            if (chooseRoute.route != null)
            {
                ObservableCollection<Passenger> collection = new ObservableCollection<Passenger>(mainWindow.PassengersDictionary[chooseRoute.route]);
                foreach (Passenger p in passengers)
                {
                    passengerManager.UpdateRoute(p, chooseRoute.route, false, true);
                    collection.Add(p);
                }
                mainWindow.PassengersDictionary[chooseRoute.route] = collection;
                passengerDataGrid.Items.Refresh();
            }
        }
        private void clearanceButton_Click(object sender, RoutedEventArgs e)
        {
            DepartureTimeView departureTimeView = new DepartureTimeView(passengerManager, mainWindow, route);
            departureTimeView.ShowDialog();
            //MessageBox.Show("Not implemented yet");
        }
        #endregion
        #region Create
        private void makeListButton_Click(object sender, RoutedEventArgs e)
        {
            string fileName;
            fileName = route.DepartureDate.ToShortDateString();
            fileName += " " + route.Region.ToString();
            if (route.RouteBack)
                fileName += " Zpět";
            else
                fileName += " Tam";
            if (route.IsRealRoute)
                fileName += $", {route.LicensePlate}";

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Excel Workbook (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*"
            };
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.AddExtension = true;
            saveFileDialog.FileName = fileName;

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    if (File.Exists(saveFileDialog.FileName))
                        File.Delete(saveFileDialog.FileName);

                    officeManager.RouteList(Passengers, saveFileDialog.FileName, route.RouteId, fileName, route.IsRealRoute);
                    RenderExports();
                    /* až u zasedáků
                    routeManager.UpdateAgenda(route, true);
                    mainWindow.Routes.FirstOrDefault(x => x.RouteId == route.RouteId).AgendaCreated = true;
                    dataGrid.Items.Refresh();
                    */
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void makeAgendaButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("NotImplementedYet");
        }
        private void makeOrderButton_Click(object sender, RoutedEventArgs e)
        {
            if (route.IsRealRoute)
            {
                string fileName = "Objednávka autobusu " + route.LicensePlate + " " + route.DepartureDate.ToShortDateString();

                ChooseRoute chooseRoute = new ChooseRoute(routeManager, mainWindow.Routes.Where(x => x.IsRealRoute && x.DepartureDate.AddDays(1) == route.DepartureDate), mainWindow);
                chooseRoute.ShowDialog();
                if (chooseRoute.route == null)
                    return;

                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "Excel Workbook (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*"
                };
                saveFileDialog.DefaultExt = "xls";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        officeManager.UpdateOrder(saveFileDialog.FileName, fileName, route, Passengers, GetPassengerCollection(chooseRoute.route).Reverse());
                        routeManager.UpdateOrder(route, true);
                        mainWindow.Routes.FirstOrDefault(x => x.RouteId == route.RouteId).OrderCreated = true;
                        dataGrid.Items.Refresh();
                        RenderExports();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if (route.BoardingRoute)
            {
                string departureDate = route.DepartureDate.ToShortDateString().Substring(0, 5).Replace(".", "");
                string routeBack = "Svoz ";
                if (route.RouteBack)
                {
                    departureDate = route.DepartureDate.AddDays(1).ToShortDateString().Substring(0, 5).Replace(".", "");
                    routeBack = "Rozvoz ";
                }
                string fileName = routeBack + route.Provider + " " + route.Region.ToString() + " " + departureDate;

                SaveFileDialog saveFileDialog = new SaveFileDialog()
                {
                    Filter = "Excel Workbook (*.doc;*.docx)|*.doc;*.docx|All files (*.*)|*.*"
                };
                saveFileDialog.DefaultExt = "doc";
                saveFileDialog.AddExtension = true;
                saveFileDialog.FileName = fileName;

                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        officeManager.UpdateOrderWord(saveFileDialog.FileName, fileName, route, Passengers);
                        routeManager.UpdateOrder(route, true);
                        mainWindow.Routes.FirstOrDefault(x => x.RouteId == route.RouteId).OrderCreated = true;
                        dataGrid.Items.Refresh();
                        RenderExports();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        #endregion
        #region ContextMenu
        private void deleteContextMenu_Click(object sender, RoutedEventArgs e)
        {
            var passengers = passengerDataGrid.SelectedItems;
            if (passengers.Count == 0)
            {
                MessageBox.Show("Vyberte cestující");
                return;
            }
            try
            {
                foreach (Passenger p in passengers)
                {
                    passengerManager.Delete(p.PassengerId);
                    mainWindow.Passengers.Remove(p);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #region Helpers
        #region RenderTabControls
        private void RenderStationCounts()
        {
            boardingTabControl.Children.Clear();
            exitTabControl.Children.Clear();

            List<Station> boardingStations = new List<Station>();
            List<Station> exitStations = new List<Station>();
            var boardingRegions = mainWindow.Regions.Where(x => x.StateId == 7);

            foreach (var p in Passengers)
            {
                if (!boardingStations.Contains(p.BoardingStation))
                    boardingStations.Add(p.BoardingStation);
                if (!exitStations.Contains(p.ExitStation))
                    exitStations.Add(p.ExitStation);
            }
            foreach (var s in boardingStations)
            {
                TextBlock text = new TextBlock
                {
                    Text = $"{s}: {Passengers.Where(x => x.BoardingStationId == s.StationId).Count()}",
                    Margin = new Thickness(0, 3, 0, 0)
                };
                boardingTabControl.Children.Add(text);
            }
            foreach (var s in exitStations)
            {
                TextBlock text = new TextBlock
                {
                    Text = $"{s}: {Passengers.Where(x => x.ExitStationId == s.StationId).Count()}",
                    Margin = new Thickness(0, 3, 0, 0)
                };
                exitTabControl.Children.Add(text);
            }

            foreach (var r in boardingRegions)
            {
                TextBlock text = new TextBlock
                {
                    Text = $"{r}: {Passengers.Where(x => x.BoardingStation.RegionId == r.RegionId).Count()}",
                    Margin = new Thickness(0, 3, 0, 0)
                };
                regionBoardingTabControl.Children.Add(text);
            }
        }
        public void RenderExports()
        {
            var items = exportManager.GetFilesByRoute(route.RouteId);

            exportsTabControl.Children.Clear();

            foreach (var f in items)
            {
                StackPanel stackPanel = new StackPanel
                {
                    Orientation = Orientation.Horizontal
                };


                Image image = new Image();
                if (f.Name.EndsWith(".xlsx") || f.Name.EndsWith(".xls"))
                {
                    Uri uri = new Uri("/Images/excelIcon.ico", UriKind.Relative);
                    image.Source = new BitmapImage(uri);
                }
                else if (f.Name.EndsWith(".docx") || f.Name.EndsWith(".doc"))
                {
                    Uri uri = new Uri("/Images/wordIcon.ico", UriKind.Relative);
                    image.Source = new BitmapImage(uri);
                }
                image.Width = 17;
                image.Height = 17;
                image.Margin = new Thickness(0, 0, 4, 0);
                image.ToolTip = f.Name;

                TextBlock textBlock = new TextBlock
                {
                    Text = f.Name,
                    ToolTip = f.Name
                };
                textBlock.IsMouseDirectlyOverChanged += (s, e) =>
                {
                    if (textBlock.IsMouseDirectlyOver)
                        textBlock.TextDecorations = TextDecorations.Underline;
                    else
                        textBlock.TextDecorations = null;
                };

                stackPanel.Children.Add(image);
                stackPanel.Children.Add(textBlock);

                TextBlock timeTextBlock = new TextBlock
                {
                    Text = f.Created.ToString(),
                    FontWeight = FontWeights.Light,
                    FontSize = 11,
                    Margin = new Thickness(0, 0, 0, 5)
                };

                image.MouseDown += (s, e) =>
                {
                    DownLoadFile(f.Name, f.Data);
                };
                textBlock.MouseDown += (s, e) =>
                {
                    DownLoadFile(f.Name, f.Data);
                };

                exportsTabControl.Children.Add(stackPanel);
                exportsTabControl.Children.Add(timeTextBlock);
            }
        }
        private void DownLoadFile(string fileName, byte[] data)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "Excel Workbook (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*"
            };
            saveFileDialog.FileName = fileName;

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    if (File.Exists(saveFileDialog.FileName))
                        File.Delete(saveFileDialog.FileName);

                    File.WriteAllBytes(saveFileDialog.FileName, data);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        #endregion
        private IEnumerable<Passenger> GetPassengerCollection(Route route)
        {
            if (mainWindow.PassengersDictionary.TryGetValue(route, out var collection))
                return SortPassengersByStations(route, collection);
            else
                throw new Exception();
        }
        private IEnumerable<Passenger> SortPassengersByStations(Route route, IEnumerable<Passenger> collection)
        {
            IEnumerable<Passenger> orderedCollection;
            if (route.BoardingRoute)
            {
                if (!route.RouteBack)
                    orderedCollection = collection.OrderBy(x => x.BoardingStation.Order).ThenByDescending(x => x.BusinessCase);
                else
                    orderedCollection = collection.OrderByDescending(x => x.BoardingStation.Order).ThenByDescending(x => x.BusinessCase);
            }
            else
            {
                if (!route.RouteBack)
                    orderedCollection = collection.OrderBy(x => x.ExitStation.Order).ThenByDescending(x => x.BusinessCase);
                else
                    orderedCollection = collection.OrderByDescending(x => x.ExitStation.Order).ThenByDescending(x => x.BusinessCase);
            }
            return orderedCollection;
        }
        #endregion
    }
}
