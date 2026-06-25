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
    /// Логика взаимодействия для Engineer.xaml
    /// </summary>
    public partial class Engineer : Window
    {
        private Usermodel _currentUser;
        public Engineer(Usermodel user)
        {
            InitializeComponent();
            _currentUser = user;
            Title = $"Инженер - {user.Fullname}";

            RefreshData();
        }

        private void RefreshData()
        {
            RequestsGrid.ItemsSource = DatabaseHelper.GetAllRequests();
            EquipmentGrid.ItemsSource = DatabaseHelper.GetAllEquipment();
        }

        private void AssignToMe_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsGrid.SelectedItem is RepairRequest request)
            {
                if (request.Status == "Новая" || request.Status == "Принята")
                {
                    DatabaseHelper.UpdateRequestStatus(request.Id_request, "В работе", _currentUser.Id_people);
                    RefreshData();
                }
            }
        }

        private void ChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (RequestsGrid.SelectedItem is RepairRequest request)
            {
                new ChangeStatus(request).ShowDialog();
                RefreshData();
            }
        }

        private void RefreshRequests_Click(object sender, RoutedEventArgs e) => RefreshData();

        private void LogoutButton_Click(object sender, RoutedEventArgs e) => Close();
    }
}

