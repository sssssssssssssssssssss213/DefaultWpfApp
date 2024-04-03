using DefaultWpfApp.View.Pages;
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

namespace DefaultWpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrm.Navigate(new AuthorizationPage());
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MainFrm.NavigationService.CanGoBack)
            {
                MainFrm.NavigationService.GoBack();
            }
        }

    }
}
