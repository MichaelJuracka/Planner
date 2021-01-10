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
            if (stateIdTextBox.Text.Length == 0 && stateNameTextBox.Text.Length == 0)
                dataGridStates.ItemsSource = mainWindow.States;
            else
            {
                IEnumerable<State> filteredStates = mainWindow.States;
                int? filterId = null;
                string filterName = null;

                if (stateIdTextBox.Text.Length != 0)
                    filterId = int.Parse(stateIdTextBox.Text);
                if (stateNameTextBox.Text.Length != 0)
                    filterName = stateNameTextBox.Text.ToLower();

                if (filterId != null)
                    filteredStates = filteredStates.Where(x => x.StateId == filterId);
                if (filterName != null)
                    filteredStates = filteredStates.Where(x => x.Name.ToLower().Contains(filterName));

                dataGridStates.ItemsSource = filteredStates;
            }
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
            if (regionIdTextBox.Text.Length == 0 && regionNameTextBox.Text.Length == 0 && state.StateId == 0)
                dataGridRegions.ItemsSource = mainWindow.Regions;

            IEnumerable<Region> filteredRegions = mainWindow.Regions;
            int? filterId = null;
            string filterName = null;
            int? stateId = null;

            if (regionIdTextBox.Text.Length != 0)
                filterId = int.Parse(regionIdTextBox.Text);
            if (regionNameTextBox.Text.Length != 0)
                filterName = regionNameTextBox.Text.ToLower();
            stateId = state.StateId;

            if (filterId != null)
                filteredRegions = filteredRegions.Where(x => x.RegionId == filterId);
            if (filterName != null)
                filteredRegions = filteredRegions.Where(x => x.Name.ToLower().Contains(filterName));
            if (stateId != 0)
                filteredRegions = filteredRegions.Where(x => x.StateId == stateId);

            dataGridRegions.ItemsSource = filteredRegions;
        }
        #endregion
        #region ProviderFilter
        private void providerIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
        private void filterProviderButton_Click(object sender, RoutedEventArgs e)
        {
            if (providerIdTextBox.Text.Length == 0 && providerNameTextBox.Text.Length == 0)
                dataGridProviders.ItemsSource = mainWindow.Providers;
            else
            {
                IEnumerable<Provider> filteredProviders = mainWindow.Providers;
                int? filterId = null;
                string filterName = null;

                if (providerIdTextBox.Text.Length != 0)
                    filterId = int.Parse(providerIdTextBox.Text);
                if (providerNameTextBox.Text.Length != 0)
                    filterName = providerNameTextBox.Text.ToLower();

                if (filterId != null)
                    filteredProviders = filteredProviders.Where(x => x.ProviderId == filterId);
                if (filterName != null)
                    filteredProviders = filteredProviders.Where(x => x.Name.ToLower().Contains(filterName));

                dataGridProviders.ItemsSource = filteredProviders;
            }
        }
        #endregion
    }
}
