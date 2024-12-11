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

namespace kind_farm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppFrame.frame = frm;
            AppConn.modeldb = new Entities();

            AppFrame.frame.Navigate(new auth.authorization());
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var dbContext = Entities.GetContext();

            try
            {
                if (dbContext.cart_table.Any())
                {
                    var allCartRecords = dbContext.cart_table.ToList();
                    dbContext.cart_table.RemoveRange(allCartRecords);
                    dbContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Произошла ошибка: " + ex, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
