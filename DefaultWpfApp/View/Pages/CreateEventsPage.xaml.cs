using DefaultWpfApp.Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DefaultWpfApp.View.Pages
{
    public partial class CreateEventsPage : Page
    {
        private DefaultKDBEntities _context;
        private Users _organizer;

        public CreateEventsPage(Users organizer, DefaultKDBEntities context = null)
        {
            InitializeComponent();
            _context = context ?? new DefaultKDBEntities(); 
            _organizer = organizer; 
            LoadComboBoxes(); 
        }

      
        private void LoadComboBoxes()
        {
           
            for (int i = 0; i <= 100; i++)
            {
                LimitAgeCmb.Items.Add(i);
            }

           
            for (int i = 1; i <= 100; i++)
            {
                LimitPeopleCmb.Items.Add(i);
            }

         
            var gameTypes = _context.GameTypes.ToList();
            foreach (var type in gameTypes)
            {
                TypeCmb.Items.Add(type.TypeName);
            }
        }

        private void AddEventBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
    
                if (string.IsNullOrWhiteSpace(NameTb.Text) ||
                    string.IsNullOrWhiteSpace(AdressTb.Text) ||
                    string.IsNullOrWhiteSpace(DescriptionTb.Text) ||
                    string.IsNullOrWhiteSpace(Image.Text) ||
                    DateDp.SelectedDate == null ||
                    LimitAgeCmb.SelectedItem == null ||
                    LimitPeopleCmb.SelectedItem == null ||
                    TypeCmb.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }

       
                if (DateDp.SelectedDate < DateTime.Now)
                {
                    MessageBox.Show("Выберите корректную дату для мероприятия.");
                    return;
                }

     
                if (!int.TryParse(LimitPeopleCmb.SelectedItem.ToString(), out int maxParticipants) ||
                    !int.TryParse(LimitAgeCmb.SelectedItem.ToString(), out int ageRestriction))
                {
                    MessageBox.Show("Пожалуйста, выберите корректные значения для возрастного ограничения и максимального количества участников.");
                    return;
                }

            
                Events newEvent = new Events
                {
                    OrganizerId = _organizer.UserId,
                    Image = Image.Text,
                    EventName = NameTb.Text,
                    MaxParticipants = maxParticipants,
                    TypeId = TypeCmb.SelectedIndex + 1, 
                    NumberOfTeams = maxParticipants, 
                    StartDate = DateDp.SelectedDate,
                    AgeRestriction = ageRestriction,
                    Address = AdressTb.Text,
                    Description = DescriptionTb.Text
                };

               
                _context.Events.Add(newEvent);
                _context.SaveChanges();

                MessageBox.Show("Мероприятие успешно добавлено.");

        
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении мероприятия: {ex.Message}");
            }
        }

       
        private void ClearFields()
        {
            NameTb.Clear();
            AdressTb.Clear();
            DescriptionTb.Clear();
            Image.Clear();
            DateDp.SelectedDate = null;
            LimitAgeCmb.SelectedItem = null;
            LimitPeopleCmb.SelectedItem = null;
            TypeCmb.SelectedItem = null;
        }
    }
}
