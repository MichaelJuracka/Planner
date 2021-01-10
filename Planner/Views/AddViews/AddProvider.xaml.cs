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

namespace Planner.Views.AddViews
{
    /// <summary>
    /// Interaction logic for AddProvider.xaml
    /// </summary>
    public partial class AddProvider : Window
    {
        private IProviderManager providerManager;
        private MainWindow mainWindow;
        public AddProvider(IProviderManager providerManager, MainWindow mainWindow)
        {
            this.providerManager = providerManager;
            this.mainWindow = mainWindow;

            InitializeComponent();
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var provider = providerManager.Add(nameTextBox.Text);
                mainWindow.Providers.Add(provider);
                mainWindow.FilterProviders.Add(provider);
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
    }
}
