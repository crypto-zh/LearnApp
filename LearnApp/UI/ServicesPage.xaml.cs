using LearnApp.Data;
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
    /// Логика взаимодействия для ServicesPage.xaml
    /// </summary>
    public partial class ServicesPage : Page
    {
        public ServicesPage()
        {
            InitializeComponent();
            //заполнение выпадающих списков
            ComboFilter.Items.Add("Все");
            ComboFilter.Items.Add("От 0 до 5%");
            ComboFilter.Items.Add("От 5% до 15%");
            ComboFilter.Items.Add("От 15% до 30%");
            ComboFilter.Items.Add("От 30% до 70%");
            ComboFilter.Items.Add("От 70% до 100%");
            ComboFilter.SelectedIndex = 0;
            ComboSort.Items.Add("По возрастанию");
            ComboSort.Items.Add("По убыванию");
            ComboSort.SelectedIndex = 0;
            UpdateData();
            
            if (UserInfo.IsAdmin)
            {
                BtnAddService.Visibility = Visibility.Visible;
                BtnWriteClient.Visibility = Visibility.Visible;
            }
            else
            {
                BtnAddService.Visibility = Visibility.Collapsed;
                BtnWriteClient.Visibility = Visibility.Collapsed;
            }

        }


        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin)
                Manager.CurrentFrame.Navigate(new AddEditServicePage((sender as Button).DataContext as ServiceModel));
            else
                MessageBox.Show("Войдите как администратор", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void BtnDel_Click(object sender, RoutedEventArgs e)
        {
            if (UserInfo.IsAdmin)
            {
                try
                {
                    if (MessageBox.Show("Вы действительно хотите удалить данную услугу", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                    {
                        Service service = ((sender as Button).DataContext as ServiceModel).ToServiceEntities();
                        if (service.ClientService.Count > 0)
                            MessageBox.Show("Нельзя удалить данную услугу", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                        else
                        {
                            service = LearnEntities.GetContext().Service.Where(p => p.ID == service.ID).First();
                            LearnEntities.GetContext().Service.Remove(service);
                            LearnEntities.GetContext().SaveChanges();
                            MessageBox.Show("Услуга удалена", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
                            UpdateData();
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("Ошибка соединения с базой", "Внимание", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("Войдите как администратор", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void TxtBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateData();
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateData();
        }

        private void UpdateData()
        {
            try
            {
                List<Service> temp = LearnEntities.GetContext().Service.ToList();
                List<ServiceModel> currentServices = new List<ServiceModel>();
                foreach (Service service in temp)
                {
                    currentServices.Add(service.ToServiceModel());
                }

                if (ComboSort.SelectedIndex == 0)
                {
                    currentServices = currentServices.OrderBy(p => p.CostWithDiscount).ToList();

                }
                else if (ComboSort.SelectedIndex == 1)
                {
                    currentServices = currentServices.OrderByDescending(p => p.CostWithDiscount).ToList();
                }

                currentServices = currentServices.Where(p => p.Title.ToLower().Contains(TxtBoxSearch.Text.ToLower()) || p.Description != null && p.Description.ToLower().Contains(TxtBoxSearch.Text.ToLower())).ToList();


                if (ComboFilter.SelectedIndex == 1)
                    currentServices = currentServices.Where(p => p.Discount >= 0 && p.Discount < 5).ToList();
                else if (ComboFilter.SelectedIndex == 2)
                    currentServices = currentServices.Where(p => p.Discount >= 5 && p.Discount < 15).ToList();
                else if (ComboFilter.SelectedIndex == 3)
                    currentServices = currentServices.Where(p => p.Discount >= 15 && p.Discount < 30).ToList();
                else if (ComboFilter.SelectedIndex == 4)
                    currentServices = currentServices.Where(p => p.Discount >= 30 && p.Discount < 70).ToList();
                else if (ComboFilter.SelectedIndex == 5)
                    currentServices = currentServices.Where(p => p.Discount >= 70 && p.Discount < 100).ToList();
                LViewServices.ItemsSource = currentServices;
                TxtCountServices.Text = "Выведено " + currentServices.Count + " из " + LearnEntities.GetContext().Service.ToList().Count + " записей";
            }
            catch
            {
                MessageBox.Show("Ошибка получения данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnAddService_Click(object sender, RoutedEventArgs e)
        {
            Manager.CurrentFrame.Navigate(new AddEditServicePage(null));
        }

        private void BtnWriteClient_Click(object sender, RoutedEventArgs e)
        {
            if (LViewServices.SelectedItem == null)
                MessageBox.Show("Выберите услугу", "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            else
            {
                AddClientServiceWindow addClientServiceWindow = new AddClientServiceWindow((LViewServices.SelectedItem as ServiceModel).ToServiceEntities());
                addClientServiceWindow.ShowDialog();
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateData();
        }
    }
}
