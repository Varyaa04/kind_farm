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
using kind_farm.Conn_DB;

namespace kind_farm.user
{
    /// <summary>
    /// Логика взаимодействия для productsPage.xaml
    /// </summary>
    public partial class productsPage : Page
    {
        public productsPage()
        {
            InitializeComponent();
            List<products_table> products = new List<products_table>();
            listProduct.ItemsSource = AppConn.modeldb.products_table.ToList();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("Вы точно хотите вернуться назад?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AppFrame.frame.GoBack();
            }
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new cartPage());
        }
    }
}
