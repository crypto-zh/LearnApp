using LearnApp.Data.Entities;
using LearnApp.Data.Model;
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
    /// Логика взаимодействия для NextEntriesPage.xaml
    /// </summary>
    public partial class NextEntriesPage : Page
    {
        public NextEntriesPage()
        {
            InitializeComponent();
            startTimerAsync(5);
        }

        async void startTimerAsync(int days)
        {
            var time = DateTime.Now + TimeSpan.FromDays(days);
            while (DateTime.Now < time)
            {
                Update();
                await Task.Delay(TimeSpan.FromSeconds(30));
            }
        }
        private void Update()
        {
            try
            {
                var temp = LearnEntities.GetContext().ClientService.Where(p => p.StartTime >= DateTime.Now).ToList();
                List<ClientServiceModel> data = new List<ClientServiceModel>();
                foreach (ClientService clientService in temp)
                {
                    ClientServiceModel clientServiceModel = clientService.ToClientServiceModel();
                    data.Add(clientServiceModel);
                }
                DGridEntries.ItemsSource = data;
                if (data.Count == 0)
                {
                    DGridEntries.Visibility = Visibility.Collapsed;
                    TxtNoData.Visibility = Visibility.Visible;
                }
                else
                {
                    DGridEntries.Visibility = Visibility.Visible;
                    TxtNoData.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {
                DGridEntries.Visibility = Visibility.Collapsed;
                TxtNoData.Visibility = Visibility.Visible;
            }
        }
    }
}
