using DefaultWpfApp.Model;
using System;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DefaultWpfApp.View.Pages
{
    public partial class ProfilPage : Page
    {
        private Users _currentUser;
        private DefaultKDBEntities _context;

        public ProfilPage(Users user, DefaultKDBEntities context = null)
        {
            InitializeComponent();
            _currentUser = user;
            _context = context;
            FillUserData();
        }


        private void FillUserData()
        {
            if (_currentUser != null)
            {
                
                NameTb.Text = $"Ваше имя: {_currentUser.FullName}";
                EmailTb.Text = _currentUser.Email;
                DescriptionTb.Text = _currentUser.AboutMe;
                RoleTb.Text = $"Ваша роль: {_currentUser.Roles.RoleName}";
                FullNameTb.Text = _currentUser.FullName;


        
                if (_currentUser.BirthDate.HasValue)
                {
                    DateDp.SelectedDate = _currentUser.BirthDate.Value;
                }

               
                if (!string.IsNullOrEmpty(_currentUser.Image))
                {
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(_currentUser.Image);
                    image.EndInit();
                    Image.Source = image;
                }
                else
                {
                    
                    Image.Source = new BitmapImage(new Uri("\\DefaultWpfApp\\DefaultWpfApp\\Resources\\Icons\\user.png"));
                }
            }
            else
            {
                MessageBox.Show("Ошибка: Пользователь не определен.");
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.context == null)
                {
                    MessageBox.Show("Ошибка: Контекст базы данных не инициализирован.");
                    return;
                }

                var userToUpdate = App.enteredUser != null ? App.context.Users.FirstOrDefault(u => u.UserId == App.enteredUser.UserId) : null;



                if (userToUpdate != null)
                {
                    userToUpdate.AboutMe = DescriptionTb.Text;
                    userToUpdate.FullName = NameTb.Text;
                    userToUpdate.Email = EmailTb.Text;

                    if (DateTime.TryParse(DateDp.Text, out DateTime birthDate))
                    {
                        userToUpdate.BirthDate = birthDate;
                    }
                    else
                    {
                        MessageBox.Show("Неверный формат даты.");
                    }

                    App.context.SaveChanges();

                    MessageBox.Show("Изменения успешно сохранены.");
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }



    }
}
