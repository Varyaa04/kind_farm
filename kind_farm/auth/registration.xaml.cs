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

namespace kind_farm.auth
{
    /// <summary>
    /// Логика взаимодействия для registration.xaml
    /// </summary>
    public partial class registration : Page
    {
        public registration()
        {
            InitializeComponent();
            tbName.MaxLength = 50;
            tbSurname.MaxLength = 50;
            tbLogin.MaxLength = 50;
            tbEmail.MaxLength = 100;
            tbPhone.MaxLength = 16;
            tbPass.MaxLength = 50;
            tbSurname.Focus();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.GoBack();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            if(AppConn.modeldb.users_table.Any(x => x.email == tbEmail.Text && x.login == tbLogin.Text))
            {
                MessageBox.Show("Пользователь с таким логином и email'ом уже существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (!Regex.IsMatch(tbEmail.Text, emailPattern))
                {
                    MessageBox.Show("Введите корректный email!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if ((string.IsNullOrEmpty(tbSurname.Text) || string.IsNullOrWhiteSpace(tbSurname.Text)) || 
                    (string.IsNullOrEmpty(tbName.Text) || string.IsNullOrWhiteSpace(tbName.Text)) ||
                    (string.IsNullOrEmpty(tbEmail.Text) || string.IsNullOrEmpty(tbEmail.Text)) || 
                    (string.IsNullOrEmpty(tbPhone.Text) || string.IsNullOrWhiteSpace(tbPhone.Text)) ||
                    (string.IsNullOrEmpty(tbLogin.Text) || string.IsNullOrWhiteSpace(tbLogin.Text) )||
                    (string.IsNullOrEmpty(tbPass.Text) || string.IsNullOrWhiteSpace(tbPass.Text)))
                {
                    MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        users_table user_new = new users_table()
                        {
                            login = tbLogin.Text,
                            password = tbPass.Text,
                            phone = tbPhone.Text,
                            email = tbEmail.Text,
                            name = tbName.Text,
                            surname = tbSurname.Text,
                            id_role_user = 3
                        };
                        AppConn.modeldb.users_table.Add(user_new);
                        AppConn.modeldb.SaveChanges();
                        MessageBox.Show("Вы успешно зарегистрировались!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppFrame.frame.GoBack();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка: " + ex, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
              
            }
        }

        private void tbSurname_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.A || e.Key > Key.Z) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tbName.Focus();
            }
        }

        private void tbRep_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tbPass.Text != tbRep.Password || tbRep.Password == "")
            {
                btnReg.IsEnabled = false;
                tbRep.Background = Brushes.LightCoral;
                tbRep.BorderBrush = Brushes.Red;
            }
            else
            {
                btnReg.IsEnabled = true;
                tbRep.Background = Brushes.LightGreen;
                tbRep.BorderBrush = Brushes.Green;
            }
        }

        private void tbName_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.A || e.Key > Key.Z) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tbEmail.Focus();
            }
        }

        private void tbPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key < Key.D0 || e.Key > Key.D9) && e.Key != Key.Back)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tbLogin.Focus();
            }
        }

        private void tbPhone_TextChanged(object sender, TextChangedEventArgs e)
        { 
            if (!tbPhone.Text.StartsWith("+7"))
            {
                tbPhone.TextChanged -= tbPhone_TextChanged;
                var currentText = tbPhone.Text;
                tbPhone.Text = "+7" + currentText.TrimStart('+', '7');

                tbPhone.TextChanged += tbPhone_TextChanged;

                tbPhone.SelectionStart = tbPhone.Text.Length;
            }
        }

        private void tbPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbPass.Text != tbRep.Password || tbRep.Password == "")
            {
                btnReg.IsEnabled = false;
                tbRep.Background = Brushes.LightCoral;
                tbRep.BorderBrush = Brushes.Red;
            }
            else
            {
                btnReg.IsEnabled = true;
                tbRep.Background = Brushes.LightGreen;
                tbRep.BorderBrush = Brushes.Green;
            }
        }

        private void tbEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tbPhone.Focus();
            }
        }

        private void tbLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tbPass.Focus();
            }
        }

        private void tbPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tbRep.Focus();
            }
        }

        private void tbRep_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                btnReg.Focus();
            }
        }
    }
}
