using LearnApp.Data;
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

namespace LearnApp.UI
{
    /// <summary>
    /// Логика взаимодействия для AuthWindow.xaml
    /// </summary>
    public partial class AuthWindow : Window
    {
        public AuthWindow()
        {
            InitializeComponent();
        }

        private void BtnSignClient_Click(object sender, RoutedEventArgs e)
        {
            UserInfo.IsAdmin = false;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void BtnSign_Click(object sender, RoutedEventArgs e)
        {
            if (PwdText.Password.Equals("0000"))
            {
                MessageBox.Show("Вы успешно авторизованы", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                UserInfo.IsAdmin = true;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы ввели неверный пароль", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
