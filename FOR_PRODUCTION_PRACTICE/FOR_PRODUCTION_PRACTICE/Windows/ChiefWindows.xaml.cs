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
    /// Логика взаимодействия для ChiefWindows.xaml
    /// </summary>
    public partial class ChiefWindows : Window
    {
        private Usermodel _currentUser;
        public ChiefWindows(Usermodel user)
        {
            InitializeComponent();
            _currentUser = user;
            TitleText.Text = $" Начальник - {user.Fullname}";
            LoadData();
        }

        private void LoadData()
        {
            // Все заявки
            var requests = DatabaseHelper.GetAllRequests();
            RequestsGrid.ItemsSource = requests;

            // Все пользователи
            UsersGrid.ItemsSource = DatabaseHelper.GetAllUsers();

            // Статистика
            TotalRequests.Text = $" Всего заявок: {requests.Count}";

            int newCount = requests.FindAll(r => r.Status == "Новая").Count;
            NewRequests.Text = $" Новых заявок: {newCount}";

            int completedCount = requests.FindAll(r => r.Status == "Выполнена").Count;
            CompletedRequests.Text = $" Выполнено заявок: {completedCount}";

            var users = DatabaseHelper.GetAllUsers();
            TotalUsers.Text = $" Всего сотрудников: {users.Count}";
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
