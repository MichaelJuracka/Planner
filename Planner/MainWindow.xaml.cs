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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Planner.Business.Interfaces;
using Planner.Data.Models;
using System.Collections.ObjectModel;
using Planner.Views.AddViews;
using Planner.Views.ImportViews;
using System.ComponentModel;
using Microsoft.Win32;
using System.IO;
using Planner.Views.SettingViews;

namespace Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IStationManager stationManager;
        private readonly IRouteManager routeManager;
        private readonly IStateManager stateManager;
        private readonly IRegionManager regionManager;
        private readonly IProviderManager providerManager;
        private readonly IBusTypeManager busTypeManager;
        private readonly IPassengerManager passengerManager;
        private readonly IOfficeManager officeManager;
        private readonly IExportManager exportManager;
        private readonly IOwnerManager ownerManager;
        private readonly IEmailSender emailSender;

        #region Collections
        public ObservableCollection<Station> Stations { get; set; }
        public ObservableCollection<Route> Routes { get; set; }
        public ObservableCollection<State> States { get; set; }
        public ObservableCollection<State> FilterStates { get; set; }
        public ObservableCollection<Region> Regions { get; set; }
        public ObservableCollection<Region> FilterRegions { get; set; }
        public ObservableCollection<Provider> Providers { get; set; } 
        public ObservableCollection<Provider> FilterProviders { get; set; }
        public ObservableCollection<BusType> BusTypes { get; set; }
        public ObservableCollection<Passenger> Passengers { get; set; }
        public Dictionary<Route, IEnumerable<Passenger>> PassengersDictionary { get; set; }
        public ObservableCollection<Owner> Owners { get; set; }
        public ObservableCollection<Owner> FilterOwners { get; set; }
        #endregion

        private readonly BackgroundWorker backgroundWorker;
        public MainWindow(
            IStationManager stationManager,
            IRouteManager routeManager,
            IStateManager stateManager,
            IRegionManager regionManager,
            IProviderManager providerManager,
            IBusTypeManager busTypeManager,
            IPassengerManager passengerManager,
            IOfficeManager officeManager,
            IExportManager exportManager,
            IOwnerManager ownerManager,
            IEmailSender emailSender
            )
        {
            this.stationManager = stationManager;
            this.routeManager = routeManager;
            this.stateManager = stateManager;
            this.regionManager = regionManager;
            this.providerManager = providerManager;
            this.busTypeManager = busTypeManager;
            this.passengerManager = passengerManager;
            this.officeManager = officeManager;
            this.exportManager = exportManager;
            this.ownerManager = ownerManager;
            this.emailSender = emailSender;

            InitializeComponent();

            PassengersDictionary = new Dictionary<Route, IEnumerable<Passenger>>();
            backgroundWorker = new BackgroundWorker();

            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
        }
        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            FilterRegions.Insert(0, new Region() { Name = "Všechny", RegionId = 0, StateId = 7});
            FilterStates.Insert(0, new State() { Name = "Všechny", StateId = 0 });
            FilterProviders.Insert(0, new Provider() { Name = "Všichni", ProviderId = 0 });
            FilterOwners.Insert(0, new Owner() { Name = "Všichni", OwnerId = 0 });
            ucStation.InitGrid();
            uCStateRegionProvider.InitGrid();
            ucRoute.InitGrid();
            uCPassenger.InitGrid();
            uCOwner.InitGrid();
            foreach (var r in Routes)
                AddPassengersToDictionary(r);
        }
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Stations = stationManager.GetAll();
            Routes = routeManager.GetAll();
            States = stateManager.GetAll();
            FilterStates = stateManager.GetAll();
            Regions = regionManager.GetAll();
            FilterRegions = regionManager.GetAll();
            Providers = providerManager.GetAll();
            FilterProviders = providerManager.GetAll();
            BusTypes = busTypeManager.GetAll();
            Passengers = passengerManager.GetAll();
            Owners = ownerManager.GetAll();
            FilterOwners = ownerManager.GetAll();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            backgroundWorker.RunWorkerAsync();
            ucStation.InitManagers(stationManager, this);
            uCStateRegionProvider.InitManagers(stateManager, regionManager, providerManager, this);
            ucRoute.InitManagers(routeManager, passengerManager, stationManager, officeManager, exportManager, this);
            uCPassenger.InitManagers(passengerManager, stationManager, this);
            uCOwner.InitManagers(ownerManager, this);
        }
        private void SetUCsVisibility()
        {
            ucRoute.Visibility = Visibility.Hidden;
            ucStation.Visibility = Visibility.Hidden;
            uCStateRegionProvider.Visibility = Visibility.Hidden;
            uCPassenger.Visibility = Visibility.Hidden;
            uCOwner.Visibility = Visibility.Hidden;
            busImage.Visibility = Visibility.Hidden;
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            SetUCsVisibility();
            busImage.Visibility = Visibility.Visible;
        }
        #region Tab Show
        private void uCRouteMenu_Click(object sender, RoutedEventArgs e)
        {
            SetUCsVisibility();
            ucRoute.Visibility = Visibility.Visible;
        }
        private void uCStationMenu_Click(object sender, RoutedEventArgs e)
        {
            SetUCsVisibility();
            ucStation.Visibility = Visibility.Visible;
        }
        private void uCStateRegionProviderMenu_Click(object sender, RoutedEventArgs e)
        {
            SetUCsVisibility();
            uCStateRegionProvider.Visibility = Visibility.Visible;
        }
        private void uCPassengerMenu_Click(object sender, RoutedEventArgs e)
        {
            SetUCsVisibility();
            uCPassenger.Visibility = Visibility.Visible;
        }
        private void ucOwnerMenu_Click_1(object sender, RoutedEventArgs e)
        {
            SetUCsVisibility();
            uCOwner.Visibility = Visibility.Visible;
        }
        #endregion
        #region Tab New
        private void newStation_Click(object sender, RoutedEventArgs e)
        {
            AddStation addStation = new AddStation(stationManager, this);
            addStation.ShowDialog();
        }
        private void newRoute_Click(object sender, RoutedEventArgs e)
        {
            AddRoute addRoute = new AddRoute(routeManager, this);
            addRoute.ShowDialog();
        }
        private void newState_Click(object sender, RoutedEventArgs e)
        {
            AddState addState = new AddState(stateManager, this);
            addState.ShowDialog();
        }
        private void newRegion_Click(object sender, RoutedEventArgs e)
        {
            AddRegion addRegion = new AddRegion(regionManager, this);
            addRegion.ShowDialog();
        }
        private void newProvider_Click(object sender, RoutedEventArgs e)
        {
            AddProvider addProvider = new AddProvider(providerManager, this);
            addProvider.ShowDialog();
        }
        private void newPassenger_Click(object sender, RoutedEventArgs e)
        {
            AddPassenger addPassenger = new AddPassenger(passengerManager, routeManager, stationManager, this);
            addPassenger.ShowDialog();
        }
        private void importPassengersContextMenu_Click(object sender, RoutedEventArgs e)
        {
            ImportPassenger importPassenger = new ImportPassenger(routeManager, officeManager, this);
            importPassenger.ShowDialog();
        }
        private void importStationContextMenu_Click(object sender, RoutedEventArgs e)
        {
            ImportStation importStation = new ImportStation(officeManager, this);
            importStation.ShowDialog();
        }
        private void newOwner_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
        #region Tab Setting
        private void stationOrderTabControl_Click(object sender, RoutedEventArgs e)
        {
            StationOrder stationOrder = new StationOrder(stationManager, this);
            stationOrder.ShowDialog();
        }
        #endregion
        #region Helpers
        private void AddPassengersToDictionary(Route route)
        {
            if (route.IsRealRoute)
                PassengersDictionary.Add(route, Passengers.Where(x => x.RealRouteId == route.RouteId));
            else if (route.BoardingRoute)
                PassengersDictionary.Add(route, Passengers.Where(x => x.BoardingRouteId == route.RouteId));
            else
                PassengersDictionary.Add(route, Passengers.Where(x => x.RouteId == route.RouteId));
        }
        #endregion

    }
}