using kind_farm.auth;
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

namespace kind_farm.admin
{
    /// <summary>
    /// Логика взаимодействия для adminPage.xaml
    /// </summary>
    public partial class adminPage : Page
    {
        public adminPage()
        {
            InitializeComponent();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите вернуться назад?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AppFrame.frame.Navigate(new authorization());
            }
        }

        private void btnUsers_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new users.usersPage());
        }

        private void btnProducts_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new admin.products.adminPageProducts((sender as Button).DataContext as users_table));

        }
    }
}
