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
using kind_farm.user;

namespace kind_farm.manager
{
    /// <summary>
    /// Логика взаимодействия для managePage.xaml
    /// </summary>
    public partial class managePage : Page
    {
        public managePage()
        {
            InitializeComponent();
            ListOrders.ItemsSource = AppConn.modeldb.order_manager_table.GroupBy(x => x.id_order).ToList();

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите вернуться назад?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                AppFrame.frame.Navigate(new auth.authorization());
            }
        }

        private void btnCatalog_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new productsPage((sender as Button).DataContext as users_table));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить выбранный заказ?", "Подтверждение удаления",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {

                ListOrders.ItemsSource = Entities.GetContext().order_manager_table.ToList();
                Button b = sender as Button;
                int ID = int.Parse(((b.Parent as StackPanel).Children[0] as TextBlock).Text);
                int IDOrder = int.Parse(((b.Parent as StackPanel).Children[1] as TextBlock).Text);
                AppConn.modeldb.order_manager_table.Remove(
                    AppConn.modeldb.order_manager_table.Where(x => x.id_order_manager == ID).First());

                AppConn.modeldb.orders_table.Remove(
                    AppConn.modeldb.orders_table.Where(x => x.id_order == IDOrder).First());
                AppConn.modeldb.SaveChanges();
                AppFrame.frame.GoBack();
                AppFrame.frame.Navigate(new managePage());
            }

        }

        private void btnMore(object sender, RoutedEventArgs e)
        {
            Button b = sender as Button;
            int IDOrder = int.Parse(((b.Parent as StackPanel).Children[1] as TextBlock).Text);
            order_manager_table selectedOrder = Entities.GetContext().order_manager_table
                .FirstOrDefault(om => om.id_order == IDOrder);
            if (selectedOrder != null)
            {
                AppFrame.frame.Navigate(new singleOrder(selectedOrder));
            }
            else
            {
                MessageBox.Show("Заказ с таким ID не найден.");
            }
        }

        private void pageVisible(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                ListOrders.ItemsSource = Entities.GetContext().order_manager_table.GroupBy(x => x.id_order).ToList();
            }
        }
    }
}
