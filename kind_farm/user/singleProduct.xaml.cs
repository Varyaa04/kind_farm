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
    /// Логика взаимодействия для singleProduct.xaml
    /// </summary>
    public partial class singleProduct : Page
    {
        private products_table _currentProduct = new products_table();
        public singleProduct(products_table currentProduct)
        {
            InitializeComponent();
            if (currentProduct != null)
            {
                _currentProduct = currentProduct;
            }
            DataContext = _currentProduct;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.GoBack();
        }

        private void addBasket_Click(object sender, RoutedEventArgs e)
        {
            try
            {
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
    }
}
