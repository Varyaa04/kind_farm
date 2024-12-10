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

namespace kind_farm.admin.users
{
    /// <summary>
    /// Логика взаимодействия для usersPage.xaml
    /// </summary>
    public partial class usersPage : Page
    {
        public usersPage()
        {
            InitializeComponent();
            List<users_table> users = AppConn.modeldb.users_table.ToList();

            if (users.Count > 0)
            {
                tbCounter.Text = "Найдено " + users.Count + " пользователей";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
        }

        private void pageVisible(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Entities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                listUsers.ItemsSource = Entities.GetContext().users_table.ToList();
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.GoBack();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new addEditUser(null));
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frame.Navigate(new addEditUser((sender as Button).DataContext as users_table));
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы точно хотите удалить выбранного пользователя?", "Подтверждение удаления",
               MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                listUsers.ItemsSource = Entities.GetContext().users_table.ToList();
                Button b = sender as Button;
                int ID = int.Parse(((b.Parent as StackPanel).Children[0] as TextBlock).Text);
                AppConn.modeldb.users_table.Remove(
                    AppConn.modeldb.users_table.Where(x => x.id_user == ID).First());
                AppConn.modeldb.SaveChanges();
                AppFrame.frame.GoBack();
                AppFrame.frame.Navigate(new usersPage());
            }

            var userDel = listUsers.SelectedItems.Cast<users_table>().ToList();
        }

        public void findUsers()
        {
            List<users_table> user = AppConn.modeldb.users_table.ToList();

            if (tbSearch != null)
            {
                user = user.Where(x => x.login.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            }

            if (user.Count > 0)
            {
                tbCounter.Text = "Найдено " + user.Count + " пользователей";
            }
            else
            {
                tbCounter.Text = "Ничего не найдено";
            }
            listUsers.ItemsSource = user;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            findUsers();
        }
    }
}
