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
    /// Логика взаимодействия для CreateRequest.xaml
    /// </summary>
    public partial class CreateRequest : Window
    {
        private Usermodel _currentUser;

        
        public CreateRequest(Usermodel user)
        {
            InitializeComponent();
            _currentUser = user;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionBox.Text))
            {
                MessageBox.Show("Введите описание неисправности!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var request = new RepairRequest
            {
                RequestNumber = $"ЗАЯВКА-{DateTime.Now:yyyyMMdd-HHmmss}",
                DateReceived = DateTime.Now,
                EquipmentType = (TypeCombo.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Компьютер",
                EquipmentId = 1,
                Description = DescriptionBox.Text,
                Id_requester = _currentUser.Id_people,
                Status = "Новая",
                Priority = "Обычный"
            };

            if (DatabaseHelper.AddRequest(request))
            {
                MessageBox.Show("Заявка создана!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                DialogResult = true;
                Close();
            }
            else
            {
                MessageBox.Show("Ошибка при создании заявки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
