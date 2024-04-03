using DefaultWpfApp.Model;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace DefaultWpfApp.View.Pages
{
    public partial class RegiztrationPage : Page
    {
        private DefaultKDBEntities _context;

        public RegiztrationPage()
        {
            InitializeComponent();
            _context = new DefaultKDBEntities(); 
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(LoginTb.Text) || string.IsNullOrEmpty(PasswordPb.Password) || string.IsNullOrEmpty(RepeatPasswordPb.Password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.");
                return;
            }

        
            if (!IsValidEmail(LoginTb.Text))
            {
                MessageBox.Show("Неверный формат адреса электронной почты.");
                return;
            }

         
            if (_context.Users.Any(u => u.Email == LoginTb.Text))
            {
                MessageBox.Show("Пользователь с такой почтой уже зарегистрирован.");
                return;
            }

           
            string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$";
            if (!Regex.IsMatch(PasswordPb.Password, passwordPattern))
            {
                MessageBox.Show("Пароль должен содержать минимум 8 символов, включая хотя бы одну заглавную букву, одну строчную букву и одну цифру.");
                return;
            }

           
            if (PasswordPb.Password != RepeatPasswordPb.Password)
            {
                MessageBox.Show("Пароли не совпадают. Пожалуйста, повторите ввод.");
                return;
            }

          
            int userType;
            if (PlayerRadioButton.IsChecked == true)
            {
                userType = 1;
            }
            else if (OrganizerRadioButton.IsChecked == true)
            {
                userType = 2;
            }
            else
            {
                MessageBox.Show("Выберите тип пользователя.");
                return;
            }

            var user = new Users
            {
                Email = LoginTb.Text,
                Password = PasswordPb.Password,
                RolesId = userType
            };

            try
            {
              
                _context.Users.Add(user);

            
                _context.SaveChanges();

                MessageBox.Show("Пользователь успешно зарегистрирован.");

            
                EventsPage eventsPage = new EventsPage(user);

                
                NavigationService.Navigate(eventsPage);
            }

            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации пользователя: {ex.Message}");
            }
        }


      
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
