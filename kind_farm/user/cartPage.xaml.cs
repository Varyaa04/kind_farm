using kind_farm.Conn_DB;
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

namespace kind_farm.user
{
    /// <summary>
    /// Логика взаимодействия для cartPage.xaml
    /// </summary>
    public partial class cartPage : Page
    {
        public cartPage()
        {
            InitializeComponent();
            List<orders_table> products = new List<orders_table>();
            listProduct.ItemsSource = AppConn.modeldb.orders_table.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.GoBack();
        }

        private void btnDelCart_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
