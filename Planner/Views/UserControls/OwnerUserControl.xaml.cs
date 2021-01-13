using Planner.Business.Interfaces;
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
        private readonly IOwnerManager ownerManager;
        private readonly MainWindow mainWindow;

        private readonly Regex regex = new Regex("[^0-9.-]+");
        public OwnerUserControl(IOwnerManager ownerManager, MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.ownerManager = ownerManager;

            InitializeComponent();

            dataGridOwners.ItemsSource = mainWindow.Owners;
        }
        private void ownerIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = regex.IsMatch(e.Text);
        }
        private void filterOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                dataGridOwners.ItemsSource = ownerManager.FilterOwners(mainWindow.Owners, ownerNameTextBox.Text, ownerIdTextBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
