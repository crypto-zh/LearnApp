using LearnApp.Data;
using LearnApp.Tools;
using LearnApp.UI;
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

namespace LearnApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new ServicesPage());
            Manager.CurrentFrame = MainFrame;
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentFrame.GoBack();
        }

        private void MainFrame_ContentRendered(object sender, EventArgs e)
        {
            this.ResizeMode = ResizeMode.CanResize;
            
            BtnService.Visibility = Visibility.Visible;
            TxtUser.Visibility = Visibility.Visible;
            if (UserInfo.IsAdmin)
            {
                BtnEntries.Visibility = Visibility.Visible;
                TxtUser.Text = "Текущий пользователь: Администратор";
            }
            else
            {
                BtnEntries.Visibility = Visibility.Collapsed;
                TxtUser.Text = "Текущий пользователь: Клиент";
            }
            if (Manager.CurrentFrame.CanGoBack)
            {
                BtnBack.Visibility = Visibility.Visible;
            }
            else
            {
                BtnBack.Visibility = Visibility.Collapsed;
            }

        }

        private void BtnEntries_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentFrame.Navigate(new NextEntriesPage());
        }

        private void BtnService_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentFrame.Navigate(new ServicesPage());
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            AuthWindow authWindow = new AuthWindow();
            authWindow.Show();
            this.Close();
        }
    }
}
