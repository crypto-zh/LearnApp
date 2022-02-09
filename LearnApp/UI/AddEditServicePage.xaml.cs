using LearnApp.Data.Entities;
using LearnApp.Data.Model;
using LearnApp.Tools;
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

namespace LearnApp.UI
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        private ServiceModel CurrentService;
        public AddEditServicePage(ServiceModel service)
        {
            InitializeComponent();
            CurrentService = new ServiceModel();
            if (service != null)
            {
                CurrentService = service;
                TxtID.Visibility = Visibility.Visible;
                TxtID.Text = "Идентификатор - " + service.ID;
                TxtTitle.Text = "Изменение услуги";
            }
            else
            {
                TxtTitle.Text = "Добавление услуги";
                TxtID.Visibility = Visibility.Collapsed;
            }
            DataContext = CurrentService.ToServiceEntities();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (String.IsNullOrWhiteSpace(CurrentService.Title))
                stringBuilder.AppendLine("Введите название услуги");
            if (CurrentService.Cost <= 0)
                stringBuilder.AppendLine("Введите стоимость");
            if (CurrentService.DurationInMinute <= 0)
                stringBuilder.AppendLine("Введите продолжительность занятия");
            if (stringBuilder.Length != 0)
            {
                MessageBox.Show(stringBuilder.ToString(), "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                if (CurrentService.ID == 0)
                    LearnEntities.GetContext().Service.Add(CurrentService.ToServiceEntities());
                DataContext = CurrentService.ToServiceEntities();
                LearnEntities.GetContext().SaveChanges();
                MessageBox.Show("Данные сохранены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                Manager.CurrentFrame.GoBack();
            }
            catch
            {
                MessageBox.Show("Ошибка соединения с базой", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
