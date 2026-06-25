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
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        public Administrator(Usermodel user)
        {
            InitializeComponent();
            Title = $"Админ - {user.Fullname}";

            UsersGrid.ItemsSource = DatabaseHelper.GetAllUsers();

            var requests = DatabaseHelper.GetAllRequests();
            var equipment = DatabaseHelper.GetAllEquipment();

            TotalUsers.Text = $" Пользователей: {DatabaseHelper.GetAllUsers().Count}";
            TotalRequests.Text = $" Заявок: {requests.Count}";
            TotalEquipment.Text = $" Оборудования: {equipment.Count}";
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
