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
using System.Data.Common;

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
            Update();
            ExcessImage.ItemsSource = BDConnect.db.ServicePhoto.ToList();
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
        int numberPage = 1;
        int count = 3;
       
         private void Update()
        {
            //var servicePhotoList = BDConnect.db.ServicePhoto;
            //if (servicePhotoList.Count() % 2 == 0)
            //    max = servicePhotoList.Count() / count;
            //else max = (servicePhotoList.Count() + 1) / count;

            //ExcessImage.ItemsSource = BDConnect.db.ServicePhoto.ToList().Where(x => x.ServiceID == service.ID);
            IEnumerable<ServicePhoto> servicePhoto = BDConnect.db.ServicePhoto.Where(x => x.ServiceID == service.ID);
            servicePhoto = servicePhoto.Skip(count * numberPage).Take(count);
            ExcessImage.ItemsSource = servicePhoto;
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            numberPage--;
            if (numberPage < 0)
                numberPage = 0;
            Update();
        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {

             numberPage++;
            if (ExcessImage.Items.Count < 3)
                numberPage--;
            Update();


        }

        private void AddAddBtn_Click(object sender, RoutedEventArgs e)
        {
            ServicePhoto servicePhoto = new ServicePhoto();
            OpenFileDialog openFile = new OpenFileDialog()
            {
                Filter = "*.png|*.png|*.jpeg|*.jpeg|*.jpg|*.jpg",
            };
            if (openFile.ShowDialog().GetValueOrDefault())
            {
                servicePhoto.PhotoPath = File.ReadAllBytes(openFile.FileName);
                servicePhoto.ServiceID = service.ID;
                BDConnect.db.ServicePhoto.Add(servicePhoto);
                BDConnect.db.SaveChanges();
            }
            Update();
        }

        private void ClearAddBtn_Click(object sender, RoutedEventArgs e)
        {
            var sel = ExcessImage.SelectedItem as ServicePhoto;
            if(sel != null)
            {
                if (MessageBox.Show("Точно хотите удалить?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    BDConnect.db.ServicePhoto.Remove(sel);
                    BDConnect.db.SaveChanges();
                }
                else
                    MessageBox.Show("Выберите изображение");
                Update();
            }
        }

        private void RemoveAddBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

