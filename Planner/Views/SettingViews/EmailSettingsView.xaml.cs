using Planner.Business.Interfaces;
using Planner.Data.Models.Email;
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
    /// Interaction logic for EmailSettingsView.xaml
    /// </summary>
    public partial class EmailSettingsView : Window
    {
        private readonly IEmailManager emailManager;
        private readonly Regex _regex = new Regex("[^0-9.-]+");
        public EmailSettingsView(IEmailManager emailManager)
        {
            this.emailManager = emailManager;

            InitializeComponent();

            emailTemplateDataGrid.ItemsSource = emailManager.GetAllEmailTemplates();
            emailUserDataGrid.ItemsSource = emailManager.GetAllEmailUsers();
        }
        #region Template
        private void addTemplateTextBox_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                emailManager.AddTemplate(templateNameTextBox.Text, templateSubjectTextBox.Text, templateBodyTextBox.Text);
                emailTemplateDataGrid.ItemsSource = emailManager.GetAllEmailTemplates();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void updateTemplateTextBox_Click(object sender, RoutedEventArgs e)
        {
            var template = (EmailTemplate)emailTemplateDataGrid.SelectedItem;

            try
            {
                emailTemplateDataGrid.SelectedItem = emailManager.UpdateTemplate(template, templateNameTextBox.Text, templateSubjectTextBox.Text, templateBodyTextBox.Text);
                emailTemplateDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void emailTemplateDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (emailTemplateDataGrid.SelectedItem is EmailTemplate template)
            {
                updateTemplateTextBox.IsEnabled = true;
                templateNameTextBox.Text = template.Name;
                templateSubjectTextBox.Text = template.Subject;
                templateBodyTextBox.Text = template.Body;
            }
        } 
        #endregion
        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                emailManager.AddUser(emailUserNameTextBox.Text, emailPasswordTextBox.Text, smtpServerTextBox.Text, smtpPortTextBox.Text, userDefaultCheckbox.IsChecked.Value);
                emailUserDataGrid.ItemsSource = emailManager.GetAllEmailUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void updateUserButton_Click(object sender, RoutedEventArgs e)
        {
            var user = (EmailUser)emailUserDataGrid.SelectedItem;

            try
            {
                emailUserDataGrid.SelectedItem = emailManager.UpdateUser(user, emailUserNameTextBox.Text, emailPasswordTextBox.Text, smtpServerTextBox.Text, smtpPortTextBox.Text, userDefaultCheckbox.IsChecked.Value);
                emailUserDataGrid.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void emailUserDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (emailUserDataGrid.SelectedItem is EmailUser user)
            {
                updateUserButton.IsEnabled = true;
                emailUserNameTextBox.Text = user.UserName;
                emailPasswordTextBox.Text = user.PassWord;
                smtpServerTextBox.Text = user.SmtpServer;
                smtpPortTextBox.Text = user.SmtpPort.ToString();
                userDefaultCheckbox.IsChecked = user.Default;
            }
        }
        private void smtpPortTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _regex.IsMatch(e.Text);
        }
    }
}
