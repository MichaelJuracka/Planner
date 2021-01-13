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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Planner.Views.UserControls
{
    /// <summary>
    /// Interaction logic for StateRegionProviderUserControl.xaml
    /// </summary>
    public partial class StateRegionProviderUserControl : UserControl
    {
        private IStateManager stateManager;
        private IRegionManager regionManager;
        private IProviderManager providerManager;
        private MainWindow mainWindow;

        private readonly Regex regex = new Regex("[^0-9.-]+");
        public StateRegionProviderUserControl()
        {
            InitializeComponent();
        }
        public void InitGrid()
        {
            dataGridStates.ItemsSource = mainWindow.States;
            dataGridRegions.ItemsSource = mainWindow.Regions;
            dataGridProviders.ItemsSource = mainWindow.Providers;
            regionStateComboBox.ItemsSource = mainWindow.FilterStates;
            regionStateComboBox.SelectedIndex = 0;
        }
        public void InitManagers(IStateManager stateManager, IRegionManager regionManager, IProviderManager providerManager, MainWindow mainWindow)
        {
            this.stateManager = stateManager;
            this.regionManager = regionManager;
            this.providerManager = providerManager;
            this.mainWindow = mainWindow;
        }
        #region StateFilter
        private void stateIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
        private void filterStateButton_Click(object sender, RoutedEventArgs e)
        {
            dataGridStates.ItemsSource = stateManager.FilterStates(mainWindow.States, stateIdTextBox.Text, stateNameTextBox.Text);
        }
        #endregion
        #region RegionFilter
        private void regionIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
        private void filterRegionsButton_Click(object sender, RoutedEventArgs e)
        {
            var state = (State)regionStateComboBox.SelectedItem;
            dataGridRegions.ItemsSource = regionManager.FilterRegion(mainWindow.Regions, regionIdTextBox.Text, regionNameTextBox.Text, state);
        }
        #endregion
        #region ProviderFilter
        private void providerIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
        private void filterProviderButton_Click(object sender, RoutedEventArgs e)
        {
            dataGridProviders.ItemsSource = providerManager.FilterProviders(mainWindow.Providers, providerIdTextBox.Text, providerNameTextBox.Text);
        }
        #endregion
    }
}
