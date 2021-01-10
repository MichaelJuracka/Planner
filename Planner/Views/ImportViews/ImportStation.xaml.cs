using Microsoft.Win32;
using Planner.Business.Interfaces;
using Planner.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Planner.Views.ImportViews
{
    /// <summary>
    /// Interaction logic for ImportStation.xaml
    /// </summary>
    public partial class ImportStation : Window
    {
        private readonly IOfficeManager officeManager;
        private readonly MainWindow mainWindow;

        private string filePath;
        public ImportStation(IOfficeManager officeManager, MainWindow mainWindow)
        {
            this.officeManager = officeManager;
            this.mainWindow = mainWindow;

            InitializeComponent();
        }

        private void chooseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "Excel Workbook (*.xls;*.xlsx)|*.xls;*.xlsx|All files (*.*)|*.*"
            };
            if (ofd.ShowDialog() == true)
            {
                filePath = ofd.FileName;
                filePathTextBox.Text = ofd.FileName;
            }
        }
        private void importButton_Click(object sender, RoutedEventArgs e)
        {
            if (filePath != null)
            {
                try
                {
                    officeManager.ImportStations(filePath, mainWindow.Regions).ForEach(x => mainWindow.Stations.Add(x));
                    Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                MessageBox.Show("Vyberte soubor");
        }
    }
}
