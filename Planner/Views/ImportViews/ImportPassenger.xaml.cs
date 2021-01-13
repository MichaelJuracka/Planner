using Microsoft.Win32;
using Planner.Business.Interfaces;
using Planner.Data.Models;
using Planner.Views.ChooseViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace Planner.Views.ImportViews
{
    /// <summary>
    /// Interaction logic for ImportPassenger.xaml
    /// </summary>
    public partial class ImportPassenger : Window
    {
        private readonly IRouteManager routeManager;
        private readonly IOfficeManager officeManager;
        private readonly MainWindow mainWindow;
        private Route route;
        private string filePath;
        public ImportPassenger(IRouteManager routeManager, IOfficeManager officeManager, MainWindow mainWindow, Route route = null)
        {
            this.routeManager = routeManager;
            this.officeManager = officeManager;
            this.mainWindow = mainWindow;
            this.route = route;

            InitializeComponent();

            if (route != null)
            {
                routeTextBlock.Text = route.ToString();
                routeTextBlock.Visibility = Visibility.Visible;
            }
        }
        private void chooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "Excel Workbook (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*"
            };
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
                filePathTextBox.Text = openFileDialog.FileName;
            }
        }
        private void chooseRouteButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseRoute chooseRoute = new ChooseRoute(routeManager, mainWindow.Routes.Where(x => !x.BoardingRoute && !x.IsRealRoute), mainWindow);
            chooseRoute.ShowDialog();
            if (chooseRoute.route != null)
            {
                route = chooseRoute.route;
                routeTextBlock.Text = chooseRoute.route.ToString();
                routeTextBlock.Visibility = Visibility.Visible;
            }
        }

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (filePath != null)
            {
                try
                {
                    var importedPassengers = officeManager.ImportPassengers(filePath, route, mainWindow.Stations.Where(x => x.BoardingStation), mainWindow.Stations.Where(x => x.BoardingStation == false), mainWindow.Owners);
                    mainWindow.PassengersDictionary.TryGetValue(route, out var collection);
                    ObservableCollection<Passenger> passengers = new ObservableCollection<Passenger>(collection);
                    foreach (var p in importedPassengers)
                    {
                        mainWindow.Passengers.Add(p);
                        passengers.Add(p);
                    }
                    mainWindow.PassengersDictionary[route] = passengers;

                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Vyberte soubor");
        }
    }
}
