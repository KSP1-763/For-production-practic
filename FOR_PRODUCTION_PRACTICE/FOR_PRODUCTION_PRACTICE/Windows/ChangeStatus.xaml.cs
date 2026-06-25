using FOR_PRODUCTION_PRACTICE.Database;
using FOR_PRODUCTION_PRACTICE.UserWindow;
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



namespace FOR_PRODUCTION_PRACTICE.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChangeStatus.xaml
    /// </summary>
    public partial class ChangeStatus : Window
    {
        private RepairRequest _request;
        public ChangeStatus(RepairRequest request)

        {
            InitializeComponent();
            _request = request;
            RequestNumberText.Text = $"#{request.RequestNumber} - {request.Description}";

            foreach (ComboBoxItem item in StatusComboBox.Items)
            {
                if (item.Content.ToString() == request.Status)
                {
                    StatusComboBox.SelectedItem = item;
                    break;
                }
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (StatusComboBox.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string newStatus = (StatusComboBox.SelectedItem as ComboBoxItem).Content.ToString();

            if (DatabaseHelper.UpdateRequestStatus(_request.Id_request, newStatus))
            {
                MessageBox.Show("Статус обновлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        
        }
    }
}
