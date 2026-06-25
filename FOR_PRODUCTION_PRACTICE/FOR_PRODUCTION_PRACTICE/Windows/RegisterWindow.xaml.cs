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
    /// Логика взаимодействия для RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private Usermodel _currentUser;
        public RegisterWindow(Usermodel user=null)
        {
            InitializeComponent();
            _currentUser = user;

            
            if (user != null && user.Role == "Admin")
            {
                RoleCombo.Visibility = Visibility.Visible;
            }
        }

        private void ShowPassword_Checked(object sender, RoutedEventArgs e)
        {
            VisiblePasswordBox.Text = PasswordBox.Password;
            VisiblePasswordBox.Visibility = Visibility.Visible;
            PasswordBox.Visibility = Visibility.Collapsed;
        }

        private void ShowPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = VisiblePasswordBox.Text;
            PasswordBox.Visibility = Visibility.Visible;
            VisiblePasswordBox.Visibility = Visibility.Collapsed;
        }

        private string GetPassword()
        {
            if (ShowPasswordCheck.IsChecked == true)
                return VisiblePasswordBox.Text;
            else
                return PasswordBox.Password;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
           
            if (string.IsNullOrWhiteSpace(LastNameBox.Text))
            {
                StatusText.Text = "Введите фамилию!";
                StatusText.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(FirstNameBox.Text))
            {
                StatusText.Text = "Введите имя!";
                StatusText.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(LoginBox.Text))
            {
                StatusText.Text = "Введите логин!";
                StatusText.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            string password = GetPassword();
            if (string.IsNullOrWhiteSpace(password) || password.Length < 4)
            {
                StatusText.Text = "Пароль должен быть не менее 4 символов!";
                StatusText.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            if (string.IsNullOrWhiteSpace(PhoneBox.Text))
            {
                StatusText.Text = "Введите номер телефона!";
                StatusText.Foreground = System.Windows.Media.Brushes.Red;
                return;
            }

            
            string role = "User";
            if (RoleCombo.Visibility == Visibility.Visible && RoleCombo.SelectedItem != null)
            {
                role = (RoleCombo.SelectedItem as ComboBoxItem).Tag.ToString();
            }

            
            var user = new Usermodel
            {
                LastName = LastNameBox.Text.Trim(),
                FirstName = FirstNameBox.Text.Trim(),
                MiddleName = MiddleNameBox.Text.Trim(),
                position = PositionBox.Text.Trim(),
                PhoneNumber = PhoneBox.Text.Trim(),
                Email = EmailBox.Text.Trim(),
                Login = LoginBox.Text.Trim(),
                Password = password,
                Role = role
            };

            if (DatabaseHelper.AddUser(user))
            {
                StatusText.Text = "Пользователь зарегистрирован!";
                StatusText.Foreground = System.Windows.Media.Brushes.Green;
                MessageBox.Show($"Пользователь {user.Fullname} успешно зарегистрирован!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.DialogResult = true;
                this.Close();
            }
            else
            {
                StatusText.Text = "Ошибка регистрации!";
                StatusText.Foreground = System.Windows.Media.Brushes.Red;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
