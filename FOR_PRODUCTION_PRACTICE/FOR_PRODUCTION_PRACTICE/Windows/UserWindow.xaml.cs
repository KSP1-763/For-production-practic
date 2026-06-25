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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindowView : Window
    {
        private Usermodel _currentUser;
        public UserWindowView(Usermodel user)
        {
            InitializeComponent();
            _currentUser = user;
            TitleText.Text = $" Сотрудник - {user.Fullname}";
            LoadRequests();
        }

        private void LoadRequests()
        {
            var all = DatabaseHelper.GetAllRequests();
            RequestsGrid.ItemsSource = all.FindAll(r => r.Id_requester == _currentUser.Id_people);
        }

        private void NewRequest_Click(object sender, RoutedEventArgs e)
        {
            var window = new CreateRequest(_currentUser);
            window.ShowDialog();
            LoadRequests();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
