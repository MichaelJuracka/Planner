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
    /// Interaction logic for OwnerUserControl.xaml
    /// </summary>
    public partial class OwnerUserControl : UserControl
    {
        private IOwnerManager ownerManager;
        private MainWindow mainWindow;

        private readonly Regex regex = new Regex("[^0-9.-]+");
        public OwnerUserControl()
        {
            InitializeComponent();
        }
        public void InitGrid()
        {
            dataGridOwners.ItemsSource = mainWindow.Owners;
        }
        public void InitManagers(IOwnerManager ownerManager, MainWindow mainWindow)
        {
            this.ownerManager = ownerManager;
            this.mainWindow = mainWindow;
        }
        #region Filter
        private void ownerIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
        private void filterOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            dataGridOwners.ItemsSource = ownerManager.FilterOwners(mainWindow.Owners, ownerNameTextBox.Text, ownerIdTextBox.Text, ownerEmailTextBox.Text);
        }
        #endregion
        #region Update
        private void updateButton_Click(object sender, RoutedEventArgs e)
        {
            var owner = (Owner)dataGridOwners.SelectedItem;

            try
            {
                dataGridOwners.SelectedItem = ownerManager.UpdateOwner(owner, nameTextBox.Text, emailTextBox.Text);
                dataGridOwners.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridOwners_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGridOwners.SelectedItem is Owner owner)
            {
                nameTextBox.Text = owner.Name;
                emailTextBox.Text = owner.Email;
            }
        }
        #endregion
    }
}
