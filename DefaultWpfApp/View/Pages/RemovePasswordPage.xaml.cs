using DefaultWpfApp.Model;
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

namespace DefaultWpfApp.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для RemovePasswordPage.xaml
    /// </summary>
    public partial class RemovePasswordPage : Page
    {
        public RemovePasswordPage()
        {
            InitializeComponent();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTb.Text;
            string password = PasswordPb.Password;
            string repeatPassword = RepeatPasswordPb.Password;


            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(repeatPassword))
            {
                MessageBox.Show("Все поля должны быть заполнены.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            if (password != repeatPassword)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new DefaultKDBEntities())
                {

                    var user = context.Users.FirstOrDefault(u => u.Email == login);


                    if (user == null)
                    {
                        MessageBox.Show("Пользователь с указанным логином не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    user.Password = password;


                    context.SaveChanges();
                }

                MessageBox.Show("Пароль успешно изменен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
             
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении пароля: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
