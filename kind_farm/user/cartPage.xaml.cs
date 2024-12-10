using kind_farm.admin.products;
using kind_farm.Conn_DB;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;

namespace kind_farm.user
{
    /// <summary>
    /// Логика взаимодействия для cartPage.xaml
    /// </summary>
    public partial class cartPage : Page
    {
        int idusercart = Convert.ToInt32(App.Current.Properties["idUser"].ToString());
        public cartPage()
        {
            InitializeComponent();

            var orderobj = Entities.GetContext().orders_table
                                 .Where(x => x.id_user == idusercart)
                                 .Select(x => x.id_order)
                                 .ToList();

            var cartobj = Entities.GetContext().cart_table
                               .Where(c => orderobj.Contains(c.id_order))
                               .Select(x => x.id_product)
                               .ToList();
            var goodsInCart = Entities.GetContext().products_table
                                         .Where(x => cartobj.Contains(x.id_product))
                                         .ToList();
            listProduct.ItemsSource = goodsInCart;

            var cartt = Entities.GetContext().cart_table.ToArray();

            if (cartt.Count() > 0)
            {
                btnOrder.IsEnabled = true;
                tbCounter.Text = "Всего в корзине " + cartt.Count() + " товаров";
            }
            else
            {
                btnOrder.IsEnabled = false;
                btnOrder.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F6C5DC"));
                tbCounter.Text = "Ваша корзина пустая!";
            }

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            int us = Convert.ToInt32(App.Current.Properties["idRole"].ToString());
            if (us == 1)
            {
                AppFrame.frame.Navigate(new adminPageProducts((sender as Button).DataContext as users_table));
            }
            else if (us == 2)
            {
                AppFrame.frame.Navigate(new productsPage((sender as Button).DataContext as users_table));
            }
            else if (us == 3)
            {
                AppFrame.frame.Navigate(new productsPage((sender as Button).DataContext as users_table));
            }
        }

        private void btnDelCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MessageBox.Show("Вы точно хотите удалить выбранный товар из заказа?", "Подтверждение удаления",
               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    listProduct.ItemsSource = Entities.GetContext().cart_table.ToList();
                    Button b = sender as Button;
                    int ID = int.Parse(((b.Parent as StackPanel).Children[0] as TextBlock).Text);
                    Console.WriteLine(ID);
                    AppConn.modeldb.cart_table.Remove(
                    AppConn.modeldb.cart_table.Where(x => x.id_cart == ID).First());
                    AppConn.modeldb.SaveChanges();
                    AppFrame.frame.GoBack();
                    AppFrame.frame.Navigate(new cartPage());
                }
            }
           catch(Exception ex)
            {
                MessageBox.Show("Произошла ошибка при удалении: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void RemoveItemsFromCart()
        {

            var context = Entities.GetContext();
            var itemsToDelete = context.cart_table
            .Where(x => x.orders_table.id_user == idusercart)
            .ToList();

            if (itemsToDelete.Any())
            {
                context.cart_table.RemoveRange(itemsToDelete);
                context.SaveChanges();
            }
        }

        public void addManager()
        {
            int idUsers = Convert.ToInt32(App.Current.Properties["idUser"].ToString());
            var order = Entities.GetContext().orders_table.FirstOrDefault(o => o.id_user == idUsers);
            var cartItems = Entities.GetContext().cart_table.Where(c => c.orders_table.id_user == idUsers);

            order = new orders_table()
            {
                id_user = idUsers,
                id_status = 1
            };

            Entities.GetContext().orders_table.Add(order);
            Entities.GetContext().SaveChanges();

            foreach (var cartItem in cartItems)
            {
                var cartnew = new order_manager_table()
                {
                    id_order = order.id_order,
                    id_product = cartItem.id_product
                };

                Entities.GetContext().order_manager_table.Add(cartnew);
            }

            Entities.GetContext().SaveChanges();
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                listProduct.ItemsSource = Entities.GetContext().cart_table.ToList();
            }
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите оформить заказ?", "Внимание!",
               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    addManager();
                    MessageBox.Show("Спасибо за Ваш заказ! В ближайшее время мы с Вами свяжемся! PDF документ был успешно загружен!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    RemoveItemsFromCart();
                    int us = Convert.ToInt32(App.Current.Properties["idRole"].ToString());
                    if (us == 1)
                    {
                        AppFrame.frame.Navigate(new adminPageProducts((sender as Button).DataContext as users_table));
                    }
                    else if (us == 2)
                    {
                        AppFrame.frame.Navigate(new productsPage((sender as Button).DataContext as users_table));
                    }
                    else if (us == 3)
                    {
                        AppFrame.frame.Navigate(new productsPage((sender as Button).DataContext as users_table));
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
