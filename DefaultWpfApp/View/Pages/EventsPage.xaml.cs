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
    /// Логика взаимодействия для EventsPage.xaml
    /// </summary>
    public partial class EventsPage : Page
    {
        private DefaultKDBEntities _context;
        private Users _user;

        public EventsPage(Users user, DefaultKDBEntities context = null)
        {
            InitializeComponent();
            _context = context ?? new DefaultKDBEntities();
            _user = user;

          
            RoleTb.Text = user.Roles.RoleName;
            image.Source = new BitmapImage(new Uri(user.Image)); 
            NameTb.Text = user.FullName;

            int eventCount = _context.Events.Count(e => e.OrganizerId == user.UserId);
            AmountEvent.Text = $"Количество мероприятий - {eventCount}";

        
            Random random = new Random();
            int randomNumber = random.Next(100);

           
            RatintTb.Text = $"Ваш рейтинг - {randomNumber}";
           
            LoadEvents();

            if (_user.RolesId != 2)
            {
               
                AddEventBtn.Visibility = Visibility.Collapsed;
            }
        }


        private void LoadEvents()
        {
       
            List<Events> events = _context.Events.ToList();

         
            basketLb.ItemsSource = events;
        }
        private void AddEventBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreateEventsPage(_user));
        }

        private void ProfilBtn_Click(object sender, RoutedEventArgs e)
        {
       
            ProfilPage profilPage = new ProfilPage(_user, App.context);
            NavigationService.Navigate(profilPage);
        }



        private void basketLb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          
            Events selectedEvent = basketLb.SelectedItem as Events;

            if (selectedEvent != null)
            {
           
                InfoEventsPage infoEventsPage = new InfoEventsPage(selectedEvent);
                NavigationService.Navigate(infoEventsPage);
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите мероприятие.");
            }
        }

    }
}
