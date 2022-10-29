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
using WpfApp1.Components;

namespace WpfApp1.Pages
{
    /// <summary>
    /// Логика взаимодействия для ServersListPage.xaml
    /// </summary>
    public partial class ServicesListPage : Page
    {
        public ServicesListPage()
        {
            InitializeComponent();
            ServiceList.ItemsSource = BDConnect.db.Service.ToList();
        }
        private void CreateBtn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Refresh()
        {
           IEnumerable<Service> filterServices = BDConnect.db.Service;
            if (SortCb.Text == "По возрастанию")
            {
                filterServices = filterServices.OrderByDescending(x => x.Cost);
            }
            else 
            {
                filterServices = filterServices.OrderBy(x => x.Cost);
            }

            var DiscountCb = DiscountSortCb.SelectedItem as ComboBoxItem;
            if (DiscountCb.Tag.ToString() == "1")
                filterServices = filterServices.Where(x => x.Discount >= 0 && x.Discount < 5);
            else if (DiscountCb.Tag.ToString() == "2")
                filterServices = filterServices.Where(x => x.Discount >= 5 && x.Discount < 15);
            else if (DiscountCb.Tag.ToString() == "3")
                filterServices = filterServices.Where(x => x.Discount >= 15 && x.Discount < 30);
            else if (DiscountCb.Tag.ToString() == "4")
                filterServices = filterServices.Where(x => x.Discount >= 30 && x.Discount < 70);
            else if (DiscountCb.Tag.ToString() == "5")
                filterServices = filterServices.Where(x => x.Discount >= 70 && x.Discount < 100);

            ServiceList.ItemsSource = filterServices.ToList();
        }

        private void SortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }

        private void DiscountSortCb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Refresh();
        }
    }
}
