using kind_farm.Conn_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace kind_farm.admin.products
{
    /// <summary>
    /// Логика взаимодействия для addEditProduct.xaml
    /// </summary>
    public partial class addEditProduct : Page
    {
        private products_table _currentProduct = new products_table();
        public addEditProduct(products_table currentProducts)
        {
            InitializeComponent();


            if (currentProducts != null)
            {
                _currentProduct = currentProducts;
            }
            DataContext = _currentProduct;

            cbType.ItemsSource = Entities.GetContext().type_product_table.ToArray();
            cbKind.ItemsSource = Entities.GetContext().kind_product_table.ToArray();
            cbUnit.ItemsSource = Entities.GetContext().unit_table.ToArray();
            cbAller.ItemsSource = Entities.GetContext().allergens_table.ToArray();

            tbweight.MaxLength = 2;
            tbCost.MaxLength = 5;
            cbName.Focus();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new adminPageProducts((sender as Button).DataContext as users_table));
        }
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
                if (_currentProduct.id_product == 0)
                {
                    if (AppConn.modeldb.products_table.Any(x => x.name_product == cbName.Text))
                    {
                        MessageBox.Show("Продукт с таким наименованием уже существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    else
                    {
                        AddProduct(cbName.Text, cbType.SelectedIndex + 1, cbKind.SelectedIndex + 1,Convert.ToInt32(tbweight.Text),
                            cbUnit.SelectedIndex + 1, Convert.ToInt32(tbCost.Text), tbDesc.Text, cbAller.SelectedIndex + 1);
                    }
                }
                else
                {
                    EditProduct();
                }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Произошла ошибка: " + ex, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            //}

        }

        public bool AddProduct(string name, int type, int kind, int weight,int unit,  int cost, string desc, int aller)
        {
            if (!ValidateFields()) return true;

            try
            {
                    products_table product_new = new products_table()
                    {
                        name_product = name,
                        id_type_product = type,
                        id_kind_product = kind,
                        weight =weight,
                        id_unit =unit,
                        cost = cost,
                        picture = null,
                        description = desc,
                        id_allergens =aller
                    };
                    AppConn.modeldb.products_table.Add(product_new);
                    AppConn.modeldb.SaveChanges();
                    MessageBox.Show("Продукт успешно добавлен!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.frame.GoBack();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return true;
        }

        private void EditProduct()
        {
            if (!ValidateFields()) return ;

            try
            {
                int productId = _currentProduct.id_product;
                var productToUpdate = AppConn.modeldb.products_table.FirstOrDefault(u => u.id_product == productId);

                if (productToUpdate != null)
                {
                    productToUpdate.name_product = cbName.Text;
                    productToUpdate.id_type_product = Convert.ToInt32(cbType.SelectedIndex + 1);
                    productToUpdate.id_kind_product = Convert.ToInt32(cbKind.SelectedIndex + 1);
                    productToUpdate.weight = Convert.ToInt32(tbweight.Text);
                    productToUpdate.id_unit = Convert.ToInt32(cbUnit.SelectedIndex + 1);
                    productToUpdate.cost = Convert.ToInt32(tbCost.Text);
                    productToUpdate.picture = productToUpdate.picture;
                    productToUpdate.description = tbDesc.Text;
                    productToUpdate.id_allergens = Convert.ToInt32(cbAller.SelectedIndex + 1);

                    AppConn.modeldb.SaveChanges();
                    MessageBox.Show("Данные продукта успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.frame.GoBack();
                }
                else
                {
                    ShowError("Продукт не найден!");
                }
        }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                throw ex;
            }


}

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private bool ValidateFields()
        {
            StringBuilder errors = new StringBuilder();
            //if(Convert.ToInt32(tbCost.Text) < 0) errors.AppendLine("Введите цену.");
            Console.WriteLine($"'{tbCost.Text}'");
            Console.WriteLine($"'{string.IsNullOrEmpty(tbCost.Text)}'");

            if (string.IsNullOrEmpty(tbCost.Text) || string.IsNullOrWhiteSpace(tbCost.Text))
                errors.AppendLine("Введите цену продукта.");
            if (errors.Length > 0)
            {
                ShowError(errors.ToString());
                return false;
            }
            if (string.IsNullOrEmpty(tbweight.Text))
                errors.AppendLine("Введите вес продукта.");
             if (Convert.ToInt32(tbweight.Text) == 0)
                errors.AppendLine("Вес продукта не может быть равен 0.");

            if (Convert.ToInt32(tbCost.Text) == 0)
                errors.AppendLine("Цена продукта не может быть равен 0.");
            if (Convert.ToInt32(tbCost.Text) > 0 && Convert.ToInt32(tbCost.Text) < 50)
                errors.AppendLine("Цена продукта не может быть меньше 50.");
            if (string.IsNullOrEmpty(cbName.Text) || string.IsNullOrWhiteSpace(cbName.Text))
                errors.AppendLine("Введите наименование.");
            if (cbType.SelectedIndex == -1)
                errors.AppendLine("Выберите тип продукта.");
            if (cbKind.SelectedIndex == -1)
                errors.AppendLine("Выберите вид продукта.");
            if (cbUnit.SelectedIndex == -1)
                errors.AppendLine("Выберите единицу измерения.");
            if (errors.Length > 0)
            {
                ShowError(errors.ToString());
                return false;
            }
            return true;
        }

        private void tbweight_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                cbUnit.Focus();
            }
        }

        private void tbCost_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tbDesc.Focus();
            }
        }

        private void cbName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                cbType.Focus();
            }
        }

        private void cbType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                cbKind.Focus();
            }
        }

        private void cbKind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tbweight.Focus();
            }
        }

        private void cbUnit_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                cbAller.Focus();
            }
        }

        private void cbAller_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tbCost.Focus();
            }
        }

        private void tbDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                btnSave.Focus();
            }
        }

        private void tbPass_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void tbCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbCost.Text == null || tbCost.Text == " " )
            {
                tbCost.Text = "0";
            }
            }

        }
    }
