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

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServersListPage.xaml
    /// </summary>
    public partial class ServicesListPage : Page
    {
        int actualPage = 0;
        public ServicesListPage()
        {
            InitializeComponent();
            if(Navigation.AuthUser.RoleId == 2)
                AddServiceBtn.Visibility = Visibility.Collapsed;
            ServiceList.ItemsSource = BDConnect.db.Service.Where(x => x.IsDelete != true).ToList();
            GeneralCount.Text = BDConnect.db.Service.Count().ToString();
        }
        private void CreateBtn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Refresh()
        {
            IEnumerable<Service> filterServices = BDConnect.db.Service.Where(x => x.IsDelete != true);
            if(SortCb.SelectedIndex > 0)
            {
                if (SortCb.SelectedIndex == 1)
                    filterServices = filterServices.OrderBy(x => x.CostDiscount);
                else
                    filterServices = filterServices.OrderByDescending(x => x.CostDiscount);
            }
          

            var DiscountCb = DiscountSortCb.SelectedItem as ComboBoxItem;
            if (DiscountCb != null)
            {
                if (DiscountCb.Tag.ToString() == "1")
                    filterServices = BDConnect.db.Service;
                else if (DiscountCb.Tag.ToString() == "2")
                    filterServices = filterServices.Where(x => (x.Discount >= 0 && x.Discount < 5) || x.Discount == null);
                else if (DiscountCb.Tag.ToString() == "3")
                    filterServices = filterServices.Where(x => x.Discount >= 5 && x.Discount < 15);
                else if (DiscountCb.Tag.ToString() == "4")
                    filterServices = filterServices.Where(x => x.Discount >= 15 && x.Discount < 30);
                else if (DiscountCb.Tag.ToString() == "5")
                    filterServices = filterServices.Where(x => x.Discount >= 30 && x.Discount < 70);
                else if (DiscountCb.Tag.ToString() == "6")
                    filterServices = filterServices.Where(x => x.Discount >= 70 && x.Discount < 100);
            }


            if (NameDisSearchTb.Text.Length > 0)
                filterServices = filterServices.Where(x => x.Title.ToLower().StartsWith(NameDisSearchTb.Text.ToLower()) || x.Description.ToLower().StartsWith(NameDisSearchTb.Text.ToLower()));

            if(CountCb.SelectedIndex > -1 && filterServices.Count() > 0)
            {
                int selCount = Convert.ToInt32((CountCb.SelectedItem as ComboBoxItem).Content);
                filterServices = filterServices.Skip(selCount * actualPage).Take(selCount);
                if(filterServices.Count() == 0)
                {
                    actualPage--;
                    Refresh();
                }
            }


            ServiceList.ItemsSource = filterServices.ToList();
            FoundCount.Text = filterServices.Count().ToString() + " из ";
        }


        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void DiscountSortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void NameDisSearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            Refresh();
        }


        private void AddServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            Navigation.NextPage(new Nav("Добавление услуги", new AddEditServicePage(new Service())));
        }
        private void CreateBtn_Click(object sender, RoutedEventArgs e)
        {
            var selService = (sender as Button).DataContext as Service;
            Navigation.NextPage(new Nav("Редактирование услуги", new AddEditServicePage(selService)));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var selService = (sender as Button).DataContext as Service;
            if (MessageBox.Show("Вы точно хотите удалить эту запись?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                selService.IsDelete = true;

                MessageBox.Show("Запись удалена");
                BDConnect.db.SaveChanges();
                ServiceList.ItemsSource = BDConnect.db.Service.Where(x => x.IsDelete != true).ToList();

            }
        }

        private void LeftBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage--;
            if(actualPage < 0)
                actualPage = 0;
            Refresh();

        }

        private void RightBtn_Click(object sender, RoutedEventArgs e)
        {
            actualPage++;
            Refresh();
        }

        private void CountCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            actualPage = 0;
            Refresh();
        }

        private void OrderServiceBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
