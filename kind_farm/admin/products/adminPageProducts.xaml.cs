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

namespace kind_farm.admin.products
{
    /// <summary>
    /// Логика взаимодействия для adminPageProducts.xaml
    /// </summary>
    public partial class adminPageProducts : Page
    {
        public adminPageProducts()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new addEditProduct());
        }
    }
}
