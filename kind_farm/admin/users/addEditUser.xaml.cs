using kind_farm.Conn_DB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
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

namespace kind_farm.admin.users
{
    /// <summary>
    /// Логика взаимодействия для addEditUser.xaml
    /// </summary>
    public partial class addEditUser : Page
    {
        private users_table _currentUser = new users_table();
        public addEditUser(users_table user)
        {
            InitializeComponent();

            if (user != null)
            {
                _currentUser = user;
            }
            DataContext = _currentUser;

            cbRole.ItemsSource = Entities.GetContext().role_table.Select(x => x.role).ToList();

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

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_currentUser.id_user == 0)
            {
                if (AppConn.modeldb.users_table.Count(x => x.login == tbLogin.Text && x.email == tbEmail.Text) > 0)
                {
                    MessageBox.Show("Пользователь с таким логином и email'ом есть!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (string.IsNullOrWhiteSpace(tbPhone.Text) || tbPhone.Text.Length < 12)
                {
                    MessageBox.Show("Номер телефона должен быть в формате +7XXXXXXXXXX!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!Regex.IsMatch(tbEmail.Text, emailPattern))
                {
                    MessageBox.Show("Введите корректный email!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                AddUser();
            }
            else
            {
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (string.IsNullOrWhiteSpace(tbPhone.Text) || tbPhone.Text.Length < 12)
                {
                    MessageBox.Show("Номер телефона должен быть в формате +7XXXXXXXXXX!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!Regex.IsMatch(tbEmail.Text, emailPattern))
                {
                    MessageBox.Show("Введите корректный email!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                EditUser();
            }
        }

        private void AddUser()
        {
            if (!ValidateFields()) return;

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
                MessageBox.Show("Пользователь успешно добавлен!", "Уведомление!", MessageBoxButton.OK, MessageBoxImage.Information);
                AppFrame.frame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void EditUser()
        {
            if (!ValidateFields()) return;

            try
            {
                int userId = _currentUser.id_user;
                var userToUpdate = AppConn.modeldb.users_table.FirstOrDefault(u => u.id_user == userId);

                if (userToUpdate != null)
                {
                    userToUpdate.login = tbLogin.Text;
                    userToUpdate.password = tbPass.Text;
                    userToUpdate.email = tbEmail.Text;
                    userToUpdate.phone = tbPhone.Text;
                    userToUpdate.name = tbName.Text;
                    userToUpdate.surname = tbSurname;
                    userToUpdate.id_role = cbRole.SelectedIndex + 1;

                    AppConn.modeldb.SaveChanges();
                    MessageBox.Show("Данные пользователя успешно изменены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    AppFrame.frame.GoBack();
                }
                else
                {
                    ShowError("Пользователь не найден!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
        }


        private bool ValidateFields()
        {
            StringBuilder errors = new StringBuilder();

            if (string.IsNullOrEmpty(tbSurname.Text))
                errors.AppendLine("Введите фамилию.");
            if (string.IsNullOrEmpty(tbName.Text))
                errors.AppendLine("Введите имя.");
            if (string.IsNullOrEmpty(tbEmail.Text))
                errors.AppendLine("Введите email.");
            if (string.IsNullOrEmpty(tbPhone.Text))
                errors.AppendLine("Введите номер телефона.");
            if (string.IsNullOrEmpty(tbLogin.Text))
                errors.AppendLine("Введите логин.");;
            if (string.IsNullOrEmpty(tbPass.Text))
                errors.AppendLine("Введите пароль.");
            if (cbRole.SelectedIndex == -1)
                errors.AppendLine("Выберите роль.");
           

            if (errors.Length > 0)
            {
                ShowError(errors.ToString());
                return false;
            }
            return true;
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
                cbRole.Focus();
            }
        }


        private void cbRole_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                btnSave.Focus();
            }
        }

    }
}
