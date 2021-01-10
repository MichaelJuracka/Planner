using Microsoft.Win32;
using Planner.Business.Interfaces;
using Planner.Data.Models;
using Planner.Views.ChooseViews;
using System;
using System.Collections.Generic;
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (filePath != null)
            {
                try
                {
                    officeManager.ImportPassengers(filePath, route, mainWindow.Stations.Where(x => x.BoardingStation), mainWindow.Stations.Where(x => x.BoardingStation == false)).ForEach(x => mainWindow.Passengers.Add(x));
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
        private void chooseRouteButton_Click(object sender, RoutedEventArgs e)
        {
            ChooseRoute chooseRoute = new ChooseRoute(routeManager, mainWindow);
            chooseRoute.routeDataGrid.ItemsSource = mainWindow.Routes.Where(x => !x.BoardingRoute && !x.IsRealRoute);
            chooseRoute.ShowDialog();
            if (chooseRoute.route != null)
            {
                route = chooseRoute.route;
                routeTextBlock.Text = chooseRoute.route.ToString();
                routeTextBlock.Visibility = Visibility.Visible;
            }
        }
    }
}
