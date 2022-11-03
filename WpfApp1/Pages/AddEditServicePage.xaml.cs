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
    /// Логика взаимодействия для AddEditServicePage.xaml
    /// </summary>
    public partial class AddEditServicePage : Page
    {
        Service service;
        public AddEditServicePage(Service _service)
        {
            InitializeComponent();
            service = _service;
            DataContext = service;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (service.ID == 0)
            {
                BDConnect.db.Service.Add(service);
            }
            BDConnect.db.SaveChanges();
            MessageBox.Show("Успешно выполнено!");
        }
    }
}
