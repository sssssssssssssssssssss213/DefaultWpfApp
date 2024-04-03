using DefaultWpfApp.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace DefaultWpfApp.View.Pages
{
    public partial class AuthorizationPage : Page
    {
        private DefaultKDBEntities _context;

        public AuthorizationPage()
        {
            InitializeComponent();
            _context = new DefaultKDBEntities(); 
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
          
            string email = LoginTb.Text;
            string password = PasswordPb.Password;

            try
            {
               
                var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

                if (user != null)
                {
                   
                    App.enteredUser = user;
                    NavigationService.Navigate(new EventsPage(user));
                }
                else
                {
                  
                    MessageBox.Show("Пользователь с указанным email и паролем не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при авторизации: {ex.Message}");
            }
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            
            NavigationService.Navigate(new RegiztrationPage());
        }

        private void PasswordRemove_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NavigationService.Navigate(new RemovePasswordPage());
        }
    }
}
