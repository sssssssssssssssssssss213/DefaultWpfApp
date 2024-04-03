using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DefaultWpfApp.Model;

namespace DefaultWpfApp.View.Pages
{
    public partial class InfoEventsPage : Page
    {
        private DefaultKDBEntities _context;

        public InfoEventsPage(Events selectedEvent, DefaultKDBEntities context = null)
        {
            InitializeComponent();

       
            _context = context ?? new DefaultKDBEntities();

        
            DataContext = selectedEvent;

          
            LoadAdditionalInfo(selectedEvent);

           
        }

        private void LoadAdditionalInfo(Events selectedEvent)
        {
           
            var additionalInfo = _context.Events.FirstOrDefault(e => e.EventId == selectedEvent.EventId);

            if (additionalInfo != null)
            {
               
                selectedEvent.Image = additionalInfo.Image;
                selectedEvent.EventName = additionalInfo.EventName;
                selectedEvent.StartDate = additionalInfo.StartDate;
                selectedEvent.Address = additionalInfo.Address;
                selectedEvent.MaxParticipants = additionalInfo.MaxParticipants;
                selectedEvent.NumberOfTeams = additionalInfo.NumberOfTeams;
                selectedEvent.AgeRestriction = additionalInfo.AgeRestriction;
                selectedEvent.Description = additionalInfo.Description;
            }
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBox.Show("Вы успешно занесены в список участников, все инструкции отправленны вам на почту!");
        }
    }
}
