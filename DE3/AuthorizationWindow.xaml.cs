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
using System.Windows.Shapes;

namespace DE3
{
    /// <summary>
    /// Логика взаимодействия для AuthorizationWindow.xaml
    /// </summary>
    public partial class AuthorizationWindow : Window
    {
        test1Entities test1Entities = new test1Entities();
        public AuthorizationWindow()
        {
            InitializeComponent();
        }

        private void authSubmit_Click(object sender, RoutedEventArgs e)
        {
            string authUserRole = Authorization(authLogin.Text, authPassword.Password);
            if (authUserRole == "USER")
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            } 
            else if (authUserRole == "")
            {
                MessageBox.Show("Неверные данные пользователя");
                authLogin.Text = string.Empty;
                authPassword.Password = string.Empty;
            }
        }

        private void toRegWindow_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow= new RegistrationWindow();
            registrationWindow.Show();
            this.Close();
        }

        private string Authorization(string login, string password)
        {
            var authQuery = (from user in test1Entities.Users
                            where user.username == login && user.password == password
                            select user).FirstOrDefault();

            if (authQuery == null)
            {
                return string.Empty;
            }

            if (authQuery.role_name == "USER" || authQuery.role_name == "ADMIN")
            {
                return authQuery.role_name;
            }

            return string.Empty;
        }
    }
}
