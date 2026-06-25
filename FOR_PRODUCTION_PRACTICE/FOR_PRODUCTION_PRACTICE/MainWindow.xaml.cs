using FOR_PRODUCTION_PRACTICE.Database;

using FOR_PRODUCTION_PRACTICE.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace FOR_PRODUCTION_PRACTICE
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _isVid = false;
        private int failed_pop = 0;
        private const int max_pop = 3;
        public MainWindow()
        {   
            InitializeComponent();
            _isVid = false;
            Name = "Victor";
            UnCheckpassword();

            Show_result.Foreground = Brushes.Gray;
            Show_result.Text = "Введите логин и пароль";
        }
        private string Getpassword()
        {
            if (_isVid)
            {
                return password_show.Text == "Введите пароль" ? "" : password_show.Text;
            }
            else
            {
                return password_entry.Password;
            }
        }
        private void Random_name()
        {
            Random rnd_name = new Random();
            string[] names = { "Олег", "Влад", "Александр" };
            int random_index = rnd_name.Next(names.Length);
            Name = names[random_index];
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Random_name();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if(login_entry.Text=="Введите логин")
            {
                login_entry.Text = "";
                login_entry.Foreground = Brushes.Black;
            }
        }
        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrWhiteSpace(login_entry.Text)) 
            {
                login_entry.Text = "Введите логин";
            }
        }
        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            if (password_show.Text == "Введите пароль")
            {
                password_show.Text = "";
                password_show.Foreground = Brushes.Black;
            }
        }

        private void password_entry_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(password_show.Text))
            {
                password_show.Text = "Введите пароль";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

  
        private void Checkpassword()
        {
            _isVid = true;
            if (!string.IsNullOrWhiteSpace(password_entry.Password))
            {
                password_show.Text = password_entry.Password;
                password_show.Foreground= Brushes.Black;
            }
            else
            {
                password_show.Text = "Введите пароль";
                password_show.Visibility = Visibility.Visible;
            }
            password_show.Visibility = Visibility.Visible;
            password_entry.Visibility= Visibility.Collapsed;
        }
        private void UnCheckpassword()
        {
            _isVid = false;
            if(password_show.Text!="Введите пароль"  && !string.IsNullOrWhiteSpace(password_show.Text))
            {
                password_entry.Password=password_show.Text;
            }
            else
            {
                password_entry.Password = "";
            }
            password_entry.Visibility=Visibility.Visible;
            password_show.Visibility = Visibility.Collapsed;
    }

        private void Show_Password_Checked(object sender, RoutedEventArgs e)
        {
            Checkpassword();
        }

        private void Show_Password_Unchecked(object sender, RoutedEventArgs e)
        {
            UnCheckpassword();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string login = login_entry.Text.Trim();
            string password = Getpassword();
            if (string.IsNullOrWhiteSpace(login) || login == "Введите логин")
            {
                Show_result.Text = "Введите логин";
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                Show_result.Text = "Введите пароль";
            }
            var dbhelped = new DatabaseHelper();
            if (dbhelped.Validate(login, password))
            {
                failed_pop = 0;
                Show_result.Foreground = Brushes.Green;
                Show_result.Text = "Вход успешный";
                UserWindow.Usermodel user = DatabaseHelper.GetUserinfo(login);
                if (user != null)
                {
                    OpenWindowByRole(user);
                }
                else
                {
                    Show_result.Text = "❌ Ошибка получения данных!";
                    Show_result.Foreground = Brushes.Red;
                }
            }
            else
            {
                failed_pop++;
                string attemptsLeft = $" (осталось: {max_pop - failed_pop})";

                if (failed_pop >= max_pop)
                {
                    Show_result.Text = $"Доступ заблокирован!{attemptsLeft}";
                    Show_result.Foreground = Brushes.Red;

                    MessageBox.Show("Превышено количество попыток входа!\nПриложение будет закрыто.",
                                  "Доступ заблокирован",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Warning);
                    Application.Current.Shutdown();
                }
                else
                {
                    Show_result.Text = $" Неверный логин или пароль!{attemptsLeft}";
                    Show_result.Foreground = Brushes.Red;


                    password_entry.Password = "";
                    password_show.Text = "Введите пароль";
                    password_show.Foreground = Brushes.Gray;
                }
            }
            
        }
            private void OpenWindowByRole(UserWindow.Usermodel user)
        {
            Window window = null;

            switch (user.Role)
            {
                case "Admin":
                    window = new Administrator(user);  
                    break;
                case "Chief":
                    window = new ChiefWindows(user);   
                    break;
                case "Engineer":
                    window = new Engineer(user);
                    break;
                case "User":
                    window = new UserWindowView(user);
                    break;
                default:
                    MessageBox.Show($"Неизвестная роль: {user.Role}", "Ошибка",
                                  MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }

            if (window != null)
            {
                this.Hide();
                window.ShowDialog();
                this.Show();
                ClearFields();
            }
        }

       
        private void ClearFields()
        {
            login_entry.Text = "Введите логин";
            login_entry.Foreground = Brushes.Gray;
            password_entry.Password = "";
            password_show.Text = "Введите пароль";
            password_show.Foreground = Brushes.Gray;
            password_entry.Visibility = Visibility.Visible;
            password_show.Visibility = Visibility.Collapsed;
            Show_Password.IsChecked = false;
            _isVid = false;
            Show_result.Text = "Введите логин и пароль";
            Show_result.Foreground = Brushes.Gray;
            failed_pop = 0;
        }

       
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите выйти?",
                                        "Выход",
                                        MessageBoxButton.YesNo,
                                        MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new RegisterWindow();
            window.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var window = new RegisterWindow();
            window.ShowDialog();
        }
    }
    
}
