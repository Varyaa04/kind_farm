﻿using kind_farm.Conn_DB;
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
            tbName.MaxLength = 20;
            tbSurname.MaxLength = 30;
            tbLogin.MaxLength = 20;
            tbEmail.MaxLength = 45;
            tbPhone.MaxLength = 15;
            tbPass.MaxLength = 25;
            tbRep.MaxLength = 25;
            tbSurname.Focus();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.GoBack();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            if (AppConn.modeldb.users_table.Any(x => x.email == tbEmail.Text && x.login == tbLogin.Text))
            {
                MessageBox.Show("Пользователь с таким логином и email'ом уже существует!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                return ;
            }
            else
            {
                Register(tbSurname.Text, tbName.Text, tbLogin.Text, tbPass.Text, tbEmail.Text, tbPhone.Text, 3);
            }
        }

        public bool Register(string surname, string name, string login, string password, string email, string phone, int role)
        {
            
                string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

                if (!Regex.IsMatch(tbEmail.Text, emailPattern))
                {
                    MessageBox.Show("Введите корректный email!",
                        "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                    return true;
                }

                if ((string.IsNullOrEmpty(surname) || string.IsNullOrWhiteSpace(surname)) ||
                    (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name)) ||
                    (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email)) ||
                    (string.IsNullOrEmpty(phone) || string.IsNullOrWhiteSpace(phone)) ||
                    (string.IsNullOrEmpty(login) || string.IsNullOrWhiteSpace(login)) ||
                    (string.IsNullOrEmpty(password) || string.IsNullOrWhiteSpace(password)))
                {
                    MessageBox.Show("Все поля должны быть заполнены!", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    try
                    {
                        users_table user_new = new users_table()
                        {
                            login = login,
                            password = password,
                            phone = phone,
                            email = email,
                            name = name,
                            surname = surname,
                            id_role_user = role
                        };
                        AppConn.modeldb.users_table.Add(user_new);
                        AppConn.modeldb.SaveChanges();
                        MessageBox.Show("Вы успешно зарегистрировались!", "Успех!", MessageBoxButton.OK, MessageBoxImage.Information);
                        AppFrame.frame.Navigate(new authorization());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка: " + ex, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
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
