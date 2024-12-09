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
        users_table _authOrd = new users_table();
        private orders_table order = new orders_table();
        public productsPage(users_table authUser)
        {
            InitializeComponent();
            List<products_table> products = new List<products_table>();
            listProduct.ItemsSource = AppConn.modeldb.products_table.ToList();

            cbSort.Items.Add("Сортировка");
            cbSort.Items.Add("По возрастанию цены");
            cbSort.Items.Add("По убыванию цены");
            cbSort.Items.Add("По названию от А-Я");
            cbSort.Items.Add("По названию от Я-А");

            if (products.Count > 0)
            {
                tbCounter.Text = "Найдено " + products.Count + " продуктов";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            var productTypes = Entities.GetContext().type_product_table.Select(x => x.type_product).ToList();

            productTypes.Insert(0, "Фильтрация");
            cbFilter.ItemsSource = productTypes;

            cbFilter.SelectedIndex = 0;
            cbSort.SelectedIndex = 0;
            if (authUser != null)
            {
                _authOrd = authUser;
            }
            DataContext = _authOrd;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var userObj = AppConn.modeldb.users_table;
            int us = Convert.ToInt32(App.Current.Properties["idRole"].ToString());
            if (us == 2)
            {
                if (MessageBox.Show("Вы точно хотите вернуться в главное меню?", "Подтверждение",
              MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppFrame.frame.Navigate(new auth.authorization());
                }
            }
            else if (us == 3)
            {
                if (MessageBox.Show("Вы точно хотите вернуться назад?", "Подтверждение",
              MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    AppFrame.frame.Navigate(new manager.managePage());
                }
            }
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new cartPage());
        }

             private void pageVisible(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                listProduct.ItemsSource = Entities.GetContext().users_table.ToList();
            }
        }

        products_table[] findGoods()
        {

            List<products_table> products = AppConn.modeldb.products_table.ToList();

            if (tbSearch != null)
            {
                products = products.Where(x => x.name_product_table.name_product.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }

            if (cbFilter.SelectedIndex >= 0)
            {
                switch (cbFilter.SelectedIndex)
                {
                    case 0:
                        products = products.Where(x => x.id_type_product == 1).ToList();
                        break;
                    case 1:
                        products = products.Where(x => x.id_type_product == 2).ToList();
                        break;
                    case 2:
                        products = products.Where(x => x.id_type_product == 3).ToList();
                        break;
                }
            }


            if (cbSort.SelectedIndex >= 0)
            {
                switch (cbSort.SelectedIndex)
                {
                    case 0:
                        products = products.OrderBy(x => x.cost).ToList();
                        break;
                    case 1:
                        products = products.OrderByDescending(x => x.cost).ToList();
                        break;
                    case 2:
                        products = products.OrderBy(x => x.name_product_table.name_product).ToList();
                        break;
                    case 3:
                        products = products.OrderByDescending(x => x.name_product_table.name_product).ToList();
                        break;
                }
            }
            if (products.Count > 0)
            {
                tbCounter.Text = "Найдено " + products.Count + " продуктов";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            listProduct.ItemsSource = products;
            return products.ToArray();

        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            findGoods();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cbFilter.SelectedIndex == 0)
            {
                findGoods();
            }
            findGoods();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbFilter.SelectedIndex == 0)
            {
                findGoods();
            }
            findGoods();
        }

        private void btnAddCart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                listProduct.ItemsSource = Entities.GetContext().products_table.ToList();
                Button b = sender as Button;
                int ID = int.Parse(((b.Parent as StackPanel).Children[0] as TextBlock).Text);
                int idUsers = Convert.ToInt32(App.Current.Properties["idUser"].ToString());

                int selectedGoodsId = ID;
                var order = Entities.GetContext().orders_table.FirstOrDefault(o => o.id_user == idUsers);
                if (order == null)
                {
                    order = new orders_table()
                    {
                        id_user = idUsers,
                        id_status = 1
                    };
                    Entities.GetContext().orders_table.Add(order);
                    Entities.GetContext().SaveChanges();
                }

                var cartnew = new cart_table()
                {
                    id_order = order.id_order,
                    id_product = selectedGoodsId
                };

                Entities.GetContext().cart_table.Add(cartnew);
                Entities.GetContext().SaveChanges();


                MessageBox.Show("Продукт успешно добавлен в корзину!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frame.Navigate(new cartPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка при добавлении продукта в корзину: " + ex.ToString(), "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSingle_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new singleProduct((sender as Button).DataContext as products_table));
        }
    }
}
