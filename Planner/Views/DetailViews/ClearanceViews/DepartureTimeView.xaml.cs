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

            RenderStations(mainWindow.Passengers.Where(x => x.RouteId == route.RouteId), route.RouteBack);
        }
        private void RenderStations(IEnumerable<Passenger> passengers, bool routeBack)
        {
            var stations = new List<Station>();

            foreach (var p in passengers)
            {
                if (stations.Contains(p.BoardingStation) && !routeBack)
                    stations.Add(p.BoardingStation);
                if (stations.Contains(p.ExitStation) && routeBack)
                    stations.Add(p.ExitStation);
            }
            
            foreach (var s in stations)
            {
                StackPanel stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal
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
                    Width = 50
                };
                if (s.DepartureTime != null)
                    textBox.Text = s.DepartureTime;
                stackPanel.Children.Add(textBox);
                NameScope.SetNameScope(departureTimeStackPanel, new NameScope());
                
            }
        }
    }
}
