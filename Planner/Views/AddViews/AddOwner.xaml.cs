using Planner.Business.Interfaces;
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
    /// Interaction logic for AddOwner.xaml
    /// </summary>
    public partial class AddOwner : Window
    {
        private readonly IOwnerManager ownerManager;
        private readonly MainWindow mainWindow;
        public AddOwner(IOwnerManager ownerManager, MainWindow mainWindow)
        {
            this.ownerManager = ownerManager;
            this.mainWindow = mainWindow;

            InitializeComponent();
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mainWindow.Owners.Add(ownerManager.Add(nameTextBox.Text, emailTextBox.Text));
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
