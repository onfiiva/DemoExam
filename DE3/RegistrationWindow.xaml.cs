using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {

        test1Entities test1Entities = new test1Entities();
        public RegistrationWindow()
        {
            InitializeComponent();
        }

        private void regConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (Registration(regLogin.Text, regPassword.Password, regPasswordConfirm.Password))
            {
                MessageBox.Show("Успешная регистрация");
                AuthorizationWindow authorizationWindow = new AuthorizationWindow();
                authorizationWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Некорректные данные");
                regLogin.Text = string.Empty;
                regPassword.Password = string.Empty;
                regPasswordConfirm.Password = string.Empty;
            }
        }

        private void toAuthWindow_Click(object sender, RoutedEventArgs e)
        {
            AuthorizationWindow authorizationWindow = new AuthorizationWindow();
            authorizationWindow.Show();
            this.Close();
        }

        private bool Registration(string login, string password, string confirmPassword) 
        {
            if (login == "" && password == "" && confirmPassword == "")
            {
                return false;
            }

            if (password != confirmPassword)
            {
                return false;
            }

            int id = 1;

            if (test1Entities.Users.Any())
            {
                id = test1Entities.Users.Max(x => x.ID) + 1;
            }

            Users newUser = new Users
            {
                ID = id,
                username= login,
                password= password,
                role_name= "USER"
            };

            test1Entities.Users.Add(newUser);

            try
            {
                test1Entities.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
    }
}
