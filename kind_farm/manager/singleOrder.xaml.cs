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

namespace kind_farm.manager
{
    /// <summary>
    /// Логика взаимодействия для singleOrder.xaml
    /// </summary>
    public partial class singleOrder : Page
    {
        private order_manager_table _currentOrders = new order_manager_table();
        public singleOrder(order_manager_table selectedorder)
        {
            InitializeComponent();
            if (selectedorder != null)
            {
                _currentOrders = selectedorder;
                DataContext = _currentOrders;

                var currentOrderId = _currentOrders.id_order;

                var goodsInOrder = Entities.GetContext().products_table
                    .Where(bg => bg.order_manager_table.Any(om => om.id_order == currentOrderId))
                    .ToList();
                listgoodsorder.ItemsSource = goodsInOrder;

                var user = Entities.GetContext().users_table
                    .FirstOrDefault(u => u.orders_table.Any(o => o.id_order == currentOrderId));

                labeluser.Content = user?.surname;
                labelName.Content = user?.name;
                labelPhone.Content = user?.phone;
                labelEmai.Content = user?.email;

                var statusOrder = Entities.GetContext().status_table
                    .FirstOrDefault(s => s.orders_table.Any(o => o.id_order == currentOrderId));
                comboStatus.ItemsSource = Entities.GetContext().status_table.ToList();
                comboStatus.SelectedValue = statusOrder?.id_status;
            }
            else
            {
                _currentOrders = new order_manager_table();
                DataContext = _currentOrders;
            }
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new managePage());
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Вы точно хотите поменять статус заказа?", "Внимание",
               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    int Id = _currentOrders.id_order;
                    var StatusToUpdate = AppConn.modeldb.order_manager_table.FirstOrDefault(u => u.id_order == Id);
                    if (comboStatus.SelectedItem != null)
                    {
                        StatusToUpdate.orders_table.id_status = Convert.ToInt32(comboStatus.SelectedIndex + 1);

                        AppConn.modeldb.SaveChanges();
                        MessageBox.Show("Статус заказа успешно изменен!",
                            "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}