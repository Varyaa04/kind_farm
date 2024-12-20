﻿using kind_farm.Conn_DB;
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
using System.Xml.Linq;

namespace kind_farm.auth
{
    /// <summary>
    /// Логика взаимодействия для authorization.xaml
    /// </summary>
    public partial class authorization : Page
    {
        public authorization()
        {
            InitializeComponent();

            tbLog.MaxLength = 20;
            tpPass.MaxLength = 25;
            tbLog.Focus();
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new registration());
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            Auth(tbLog.Text, tpPass.Password);
        }

        public bool Auth(string loginTXT, string passwordTXT)
        {
            try
            {
                var userobj = AppConn.modeldb.users_table.FirstOrDefault(x => x.login ==loginTXT && x.password == passwordTXT);

                if (userobj != null)
                {
                    try
                    {
                        switch (userobj.id_role_user)
                        {
                            case 1:
                                MessageBox.Show("Здравствуйте, Администратор " + userobj.surname + " " + userobj.name + "!", "Добро пожаловать!", MessageBoxButton.OK, MessageBoxImage.Information);
                                App.Current.Properties["idUser"] = userobj.id_user;
                                App.Current.Properties["idRole"] = userobj.id_role_user;
                                AppFrame.frame.Navigate(new admin.adminPage());
                                break;
                            case 2:
                                MessageBox.Show("Здравствуйте, Менеджер " + userobj.surname + " " + userobj.name + "!", "Добро пожаловать!", MessageBoxButton.OK, MessageBoxImage.Information);
                                App.Current.Properties["idUser"] = userobj.id_user;
                                App.Current.Properties["idRole"] = userobj.id_role_user;
                                AppFrame.frame.Navigate(new manager.managePage());
                                break;
                            case 3:
                                MessageBox.Show("Здравствуйте, " + userobj.surname + " " + userobj.name + "!", "Добро пожаловать!", MessageBoxButton.OK, MessageBoxImage.Information);
                                App.Current.Properties["idUser"] = userobj.id_user;
                                App.Current.Properties["idRole"] = userobj.id_role_user;
                                AppFrame.frame.Navigate(new user.productsPage());
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Произошла ошибка: " + ex, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }
                else
                {
                    MessageBox.Show("Такого пользователя не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return true;
        }
        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                tpPass.Focus();
            }
        }

        private void tpPass_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab || e.Key == Key.Enter)
            {
                btnAuth.Focus();
            }
        }
    }
}
