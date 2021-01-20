using Planner.Business.Interfaces;
using Planner.Data.Models;
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

namespace Planner.Views.DetailViews.ClearanceViews
{
    /// <summary>
    /// Interaction logic for DepartureTimeView.xaml
    /// </summary>
    public partial class DepartureTimeView : Window
    {
        private readonly IPassengerManager passengerManager;
        private readonly MainWindow mainWindow;
        private readonly Route route;
        public DepartureTimeView(IPassengerManager passengerManager, MainWindow mainWindow, Route route)
        {
            this.passengerManager = passengerManager;
            this.mainWindow = mainWindow;
            this.route = route;

            InitializeComponent();

            RenderStations(mainWindow.PassengersDictionary[route], route.RouteBack);

            if (route.RouteBack)
                sendEmailsButton.Visibility = Visibility.Hidden;
        }
        private void RenderStations(IEnumerable<Passenger> passengers, bool routeBack)
        {
            var stations = new List<Station>();

            foreach (var p in passengers)
            {
                if (!stations.Contains(p.BoardingStation) && !routeBack)
                    stations.Add(p.BoardingStation);
                if (!stations.Contains(p.ExitStation) && routeBack)
                    stations.Add(p.ExitStation);
            }
            stations.Sort((x, y) => x.Name.CompareTo(y.Name));

            foreach (var s in stations)
            {
                StackPanel stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                departureTimeStackPanel.Children.Add(stackPanel);
                TextBlock textBlock = new TextBlock()
                {
                    Text = $"{s.Name}:",
                    Margin = new Thickness(0, 0, 3, 0)
                };
                stackPanel.Children.Add(textBlock);
                TextBox textBox = new TextBox()
                {
                    Name = "textBox" + s.StationId.ToString(),
                    Width = 80
                };
                if (!routeBack)
                    textBox.Text = passengers.FirstOrDefault(x => x.BoardingStationId == s.StationId).DepartureTime;
                else
                    textBox.Text = passengers.FirstOrDefault(x => x.ExitStationId == s.StationId).DepartureTime;
                stackPanel.Children.Add(textBox);
            }
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            List<TextBox> textBoxes = new List<TextBox>();
            foreach (StackPanel stackPanel in departureTimeStackPanel.Children)
            {
                foreach (var item in stackPanel.Children)
                {
                    if (item is TextBox textBox)
                        textBoxes.Add(textBox);
                }
            }

            foreach (var p in mainWindow.PassengersDictionary[route])
            {
                if (!route.RouteBack)
                    passengerManager.UpdateDepartureTime(p, textBoxes.FirstOrDefault(x => int.Parse(x.Name.Replace("textBox", "")) == p.BoardingStationId).Text);
                else
                    passengerManager.UpdateDepartureTime(p, textBoxes.FirstOrDefault(x => int.Parse(x.Name.Replace("textBox", "")) == p.ExitStationId).Text);
            }
        }
        private void sendEmailsButton_Click(object sender, RoutedEventArgs e)
        {
            if (route.RouteBack)
            {
                MessageBox.Show("na cestu zpět nelze odeslat emaily");
                return;
            }
            SendEmailView sendEmailView = new SendEmailView(mainWindow.PassengersDictionary[route].Where(x => !x.IsCleared));
            sendEmailView.ShowDialog();
        }
    }
}
