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
using Planner.Business.Interfaces;
using Planner.Data.Models;

namespace Planner.Views.AddViews
{
    /// <summary>
    /// Interaction logic for AddState.xaml
    /// </summary>
    public partial class AddState : Window
    {
        private IStateManager stateManager;
        private MainWindow mainWindow;
        public AddState(IStateManager stateManager, MainWindow mainWindow)
        {
            this.stateManager = stateManager;
            this.mainWindow = mainWindow;

            InitializeComponent();
        }
        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var state = stateManager.Add(nameTextBox.Text);
                mainWindow.States.Add(state);
                mainWindow.FilterStates.Add(state);
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
