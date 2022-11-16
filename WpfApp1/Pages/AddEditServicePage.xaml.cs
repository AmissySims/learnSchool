using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using WpfApp1.Components;
using Microsoft.Win32;
using System.IO;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        Service service;
        public AddEditServicePage(Service _service)
        {
            InitializeComponent();
            service = _service;
            service.DurationInSeconds /= 60;
            DataContext = service;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            //if (service.ID == 0)
            //{
            //    BDConnect.db.Service.Add(service);
            //}
            //BDConnect.db.SaveChanges();
            //MessageBox.Show("Успешно выполнено!");
            service.DurationInSeconds *= 60;
            if (service.ID == 0)
                BDConnect.db.Service.Add(service);
            BDConnect.db.SaveChanges();
            MessageBox.Show("Успешно выполнено!");
            Navigation.NextPage(new Nav("Список услуг", new ServicesListPage()));
        }

        private void AddImagePage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg",
            };
            if(openFile.ShowDialog().GetValueOrDefault())
            {
                service.MainImagePath = File.ReadAllBytes(openFile.FileName);
                ServiceImage.Source = new BitmapImage(new Uri(openFile.FileName));
            }
        }
    }
}

