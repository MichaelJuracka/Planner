using Planner.Business.Interfaces;
using Planner.Data.Models;
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

namespace Planner.Views.SettingViews
{
    /// <summary>
    /// Interaction logic for StationOrder.xaml
    /// </summary>
    public partial class StationOrder : Window
    {
        private readonly IStationManager stationManager;
        private readonly MainWindow mainWindow;

        private readonly Regex _regex = new Regex("[^0-9.-]+");
        public StationOrder(IStationManager stationManager, MainWindow mainWindow)
        {
            this.stationManager = stationManager;
            this.mainWindow = mainWindow;

            InitializeComponent();

            stateComboBox.ItemsSource = mainWindow.States;
        }
        private void stateComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var state = (State)stateComboBox.SelectedItem;
            regionComboBox.ItemsSource = mainWindow.Regions.Where(x => x.StateId == state.StateId);
        }
        private void regionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var region = (Region)regionComboBox.SelectedItem;
            if (region != null)
            {
                RenderStations(mainWindow.Stations.Where(x => x.RegionId == region.RegionId));
            }
        }
        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<TextBox> textBoxes = new List<TextBox>();
                foreach (StackPanel stackpanel in stationStackPanel.Children)
                {
                    foreach (var item in stackpanel.Children)
                    {
                        if (item is TextBox textBox)
                            textBoxes.Add(textBox);
                    }
                }
                foreach (TextBox textBox in textBoxes)
                {
                    stationManager.UpdateOrder(int.Parse(textBox.Name.Replace("textBox", "")), int.Parse(textBox.Text));
                }
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RenderStations(IEnumerable<Station> stations)
        {
            stationStackPanel.Children.Clear();

            foreach (var s in stations)
            {
                StackPanel stackPanel = new StackPanel()
                {
                    Orientation = Orientation.Horizontal,
                    Margin = new Thickness(0, 3, 0, 0)
                };
                TextBlock stationTextBlock = new TextBlock()
                {
                    Text = s.Name + ":",
                    Margin = new Thickness(0, 0, 3, 0)
                };
                stackPanel.Children.Add(stationTextBlock);
                TextBox orderTextBox = new TextBox()
                {
                    Name = "textBox" + s.StationId.ToString(),
                    Width = 20
                };
                if (s.Order != null)
                    orderTextBox.Text = s.Order.ToString();

                orderTextBox.PreviewTextInput += OrderTextBox_PreviewTextInput;

                stackPanel.Children.Add(orderTextBox);
                NameScope.SetNameScope(stationStackPanel, new NameScope());
                stationStackPanel.RegisterName(orderTextBox.Name, orderTextBox);
                stationStackPanel.Children.Add(stackPanel);
            }
        }
        private void OrderTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
    }
}
