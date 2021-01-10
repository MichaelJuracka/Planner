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
using Planner.Data.Models;
using Planner.Business.Interfaces;
using System.Collections.ObjectModel;

namespace Planner.Views.AddViews
{
    /// <summary>
    /// Interaction logic for AddRegion.xaml
    /// </summary>
    public partial class AddRegion : Window
    {
        private IRegionManager regionManager;
        private MainWindow mainWindow;
        public AddRegion(IRegionManager regionManager, MainWindow mainWindow)
        {
            this.regionManager = regionManager;
            this.mainWindow = mainWindow;

            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            var state = (State)stateComboBox.SelectedItem;

            try
            {
                var region = regionManager.Add(nameTextBox.Text, state);
                mainWindow.Regions.Add(region);
                mainWindow.FilterRegions.Add(region);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            stateComboBox.ItemsSource = mainWindow.States;
        }
    }
}
